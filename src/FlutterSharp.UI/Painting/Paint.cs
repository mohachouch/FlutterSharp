using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text;
using static FlutterSharp.UI.UITypes;

namespace FlutterSharp.UI
{
    /// A description of the style to use when drawing on a [Canvas].
    ///
    /// Most APIs on [Canvas] take a [Paint] object to describe the style
    /// to use for that operation.
    public class Paint
    {
        // Paint objects are encoded in two buffers:
        //
        // * _data is binary data in four-byte fields, each of which is either a
        //   uint32_t or a float. The default value for each field is encoded as
        //   zero to make initialization trivial. Most values already have a default
        //   value of zero, but some, such as color, have a non-zero default value.
        //   To encode or decode these values, XOR the value with the default value.
        //
        // * _objects is a list of unencodable objects, typically wrappers for native
        //   objects. The objects are simply stored in the list without any additional
        //   encoding.
        //
        // The binary format must match the deserialization code in paint.cc.

        internal ByteData _data = new ByteData(_kDataByteCount);
        private static readonly int _kIsAntiAliasIndex = 0;
        private static readonly int _kColorIndex = 1;
        private static readonly int _kBlendModeIndex = 2;
        private static readonly int _kStyleIndex = 3;
        private static readonly int _kStrokeWidthIndex = 4;
        private static readonly int _kStrokeCapIndex = 5;
        private static readonly int _kStrokeJoinIndex = 6;
        private static readonly int _kStrokeMiterLimitIndex = 7;
        private static readonly int _kFilterQualityIndex = 8;
        private static readonly int _kColorFilterIndex = 9;
        private static readonly int _kColorFilterColorIndex = 10;
        private static readonly int _kColorFilterBlendModeIndex = 11;
        private static readonly int _kMaskFilterIndex = 12;
        private static readonly int _kMaskFilterBlurStyleIndex = 13;
        private static readonly int _kMaskFilterSigmaIndex = 14;
        private static readonly int _kInvertColorIndex = 15;

        private static readonly int _kIsAntiAliasOffset = _kIsAntiAliasIndex << 2;
        private static readonly int _kColorOffset = _kColorIndex << 2;
        private static readonly int _kBlendModeOffset = _kBlendModeIndex << 2;
        private static readonly int _kStyleOffset = _kStyleIndex << 2;
        private static readonly int _kStrokeWidthOffset = _kStrokeWidthIndex << 2;
        private static readonly int _kStrokeCapOffset = _kStrokeCapIndex << 2;
        private static readonly int _kStrokeJoinOffset = _kStrokeJoinIndex << 2;
        private static readonly int _kStrokeMiterLimitOffset = _kStrokeMiterLimitIndex << 2;
        private static readonly int _kFilterQualityOffset = _kFilterQualityIndex << 2;
        private static readonly int _kColorFilterOffset = _kColorFilterIndex << 2;
        private static readonly int _kColorFilterColorOffset = _kColorFilterColorIndex << 2;
        private static readonly int _kColorFilterBlendModeOffset = _kColorFilterBlendModeIndex << 2;
        private static readonly int _kMaskFilterOffset = _kMaskFilterIndex << 2;
        private static readonly int _kMaskFilterBlurStyleOffset = _kMaskFilterBlurStyleIndex << 2;
        private static readonly int _kMaskFilterSigmaOffset = _kMaskFilterSigmaIndex << 2;
        private static readonly int _kInvertColorOffset = _kInvertColorIndex << 2;

        // If you add more fields, remember to update _kDataByteCount.
        private static readonly int _kDataByteCount = 75;

        // Binary format must match the deserialization code in paint.cc.
        internal List<dynamic> _objects;
        private static readonly int _kShaderIndex = 0;
        private static readonly int _kColorFilterMatrixIndex = 1;
        private static readonly int _kObjectCount = 2; // Must be one larger than the largest index.

        /// Whether to apply anti-aliasing to lines and images drawn on the
        /// canvas.
        ///
        /// Defaults to true.
        public bool IsAntiAlias
        {
            get
            {
                return _data.GetInt32(_kIsAntiAliasOffset, _kFakeHostEndian) == 0;
            }
            set
            {
                // We encode true as zero and false as one because the default value, which
                // we always encode as zero, is true.
                int encoded = value ? 0 : 1;
                _data.SetInt32(_kIsAntiAliasOffset, encoded, _kFakeHostEndian);
            }
        }

        // Must be kept in sync with the default in paint.cc.
        private static readonly uint _kColorDefault = 0xFF000000;

        /// The color to use when stroking or filling a shape.
        ///
        /// Defaults to opaque black.
        ///
        /// See also:
        ///
        ///  * [style], which controls whether to stroke or fill (or both).
        ///  * [colorFilter], which overrides [color].
        ///  * [shader], which overrides [color] with more elaborate effects.
        ///
        /// This color is not used when compositing. To colorize a layer, use
        /// [colorFilter].
        public Color Color
        {
            get
            {
                int encoded = _data.GetInt32(_kColorOffset, _kFakeHostEndian);
                return new Color(encoded ^ _kColorDefault);
            }
            set
            {
                Debug.Assert(value != null);
                var encoded = value.Value ^ _kColorDefault;
                _data.SetInt32(_kColorOffset, (int)encoded, _kFakeHostEndian); // TODO : Encoded check value type (long, int wtf) 
            }
        }


        // Must be kept in sync with the default in paint.cc.
        private static readonly int _kBlendModeDefault = (int)BlendMode.SrcOver;

        /// A blend mode to apply when a shape is drawn or a layer is composited.
        ///
        /// The source colors are from the shape being drawn (e.g. from
        /// [Canvas.drawPath]) or layer being composited (the graphics that were drawn
        /// between the [Canvas.saveLayer] and [Canvas.restore] calls), after applying
        /// the [colorFilter], if any.
        ///
        /// The destination colors are from the background onto which the shape or
        /// layer is being composited.
        ///
        /// Defaults to [BlendMode.srcOver].
        ///
        /// See also:
        ///
        ///  * [Canvas.saveLayer], which uses its [Paint]'s [blendMode] to composite
        ///    the layer when [restore] is called.
        ///  * [BlendMode], which discusses the user of [saveLayer] with [blendMode].
        public BlendMode BlendMode
        {
            get
            {
                int encoded = _data.GetInt32(_kBlendModeOffset, _kFakeHostEndian);
                return (BlendMode)(encoded ^ _kBlendModeDefault);
            }
            set
            {
                int encoded = (int)value ^ _kBlendModeDefault;
                _data.SetInt32(_kBlendModeOffset, encoded, _kFakeHostEndian);
            }
        }


        /// Whether to paint inside shapes, the edges of shapes, or both.
        ///
        /// Defaults to [PaintingStyle.fill].
        public PaintingStyle Style
        {
            get => (PaintingStyle)_data.GetInt32(_kStyleOffset, _kFakeHostEndian);
            set
            {
                int encoded = (int)value;
                _data.SetInt32(_kStyleOffset, encoded, _kFakeHostEndian);
            }
        }

        /// How wide to make edges drawn when [style] is set to
        /// [PaintingStyle.stroke]. The width is given in logical pixels measured in
        /// the direction orthogonal to the direction of the path.
        ///
        /// Defaults to 0.0, which correspond to a hairline width.
        public double StrokeWidth
        {
            get => _data.GetFloat32(_kStrokeWidthOffset, _kFakeHostEndian);
            set
            {
                double encoded = value;
                _data.SetFloat32(_kStrokeWidthOffset, encoded, _kFakeHostEndian);
            }
        }

        /// The kind of finish to place on the end of lines drawn when
        /// [style] is set to [PaintingStyle.stroke].
        ///
        /// Defaults to [StrokeCap.butt], i.e. no caps.
        public StrokeCap StrokeCap
        {
            get => (StrokeCap)(_data.GetInt32(_kStrokeCapOffset, _kFakeHostEndian));
            set
            {
                int encoded = (int)value;
                _data.SetInt32(_kStrokeCapOffset, encoded, _kFakeHostEndian);
            }
        }

        /// The kind of finish to place on the joins between segments.
        ///
        /// This applies to paths drawn when [style] is set to [PaintingStyle.stroke],
        /// It does not apply to points drawn as lines with [Canvas.drawPoints].
        ///
        /// Defaults to [StrokeJoin.miter], i.e. sharp corners.
        ///
        /// Some examples of joins:
        ///
        /// {@animation 300 300 https://flutter.github.io/assets-for-api-docs/assets/dart-ui/miter_4_join.mp4}
        ///
        /// {@animation 300 300 https://flutter.github.io/assets-for-api-docs/assets/dart-ui/round_join.mp4}
        ///
        /// {@animation 300 300 https://flutter.github.io/assets-for-api-docs/assets/dart-ui/bevel_join.mp4}
        ///
        /// The centers of the line segments are colored in the diagrams above to
        /// highlight the joins, but in normal usage the join is the same color as the
        /// line.
        ///
        /// See also:
        ///
        ///  * [strokeMiterLimit] to control when miters are replaced by bevels when
        ///    this is set to [StrokeJoin.miter].
        ///  * [strokeCap] to control what is drawn at the ends of the stroke.
        ///  * [StrokeJoin] for the definitive list of stroke joins.
        public StrokeJoin StrokeJoin
        {
            get => (StrokeJoin)(_data.GetInt32(_kStrokeJoinOffset, _kFakeHostEndian));
            set
            {
                int encoded = (int)value;
                _data.SetInt32(_kStrokeJoinOffset, encoded, _kFakeHostEndian);
            }
        }

        // Must be kept in sync with the default in paint.cc.
        private static readonly double _kStrokeMiterLimitDefault = 4.0;

        /// The limit for miters to be drawn on segments when the join is set to
        /// [StrokeJoin.miter] and the [style] is set to [PaintingStyle.stroke]. If
        /// this limit is exceeded, then a [StrokeJoin.bevel] join will be drawn
        /// instead. This may cause some 'popping' of the corners of a path if the
        /// angle between line segments is animated, as seen in the diagrams below.
        ///
        /// This limit is expressed as a limit on the length of the miter.
        ///
        /// Defaults to 4.0.  Using zero as a limit will cause a [StrokeJoin.bevel]
        /// join to be used all the time.
        ///
        /// {@animation 300 300 https://flutter.github.io/assets-for-api-docs/assets/dart-ui/miter_0_join.mp4}
        ///
        /// {@animation 300 300 https://flutter.github.io/assets-for-api-docs/assets/dart-ui/miter_4_join.mp4}
        ///
        /// {@animation 300 300 https://flutter.github.io/assets-for-api-docs/assets/dart-ui/miter_6_join.mp4}
        ///
        /// The centers of the line segments are colored in the diagrams above to
        /// highlight the joins, but in normal usage the join is the same color as the
        /// line.
        ///
        /// See also:
        ///
        ///  * [strokeJoin] to control the kind of finish to place on the joins
        ///    between segments.
        ///  * [strokeCap] to control what is drawn at the ends of the stroke.
        public double StrokeMiterLimit
        {
            get => _data.GetFloat32(_kStrokeMiterLimitOffset, _kFakeHostEndian);
            set
            {
                double encoded = value - _kStrokeMiterLimitDefault;
                _data.SetFloat32(_kStrokeMiterLimitOffset, encoded, _kFakeHostEndian);
            }
        }

        /// A mask filter (for example, a blur) to apply to a shape after it has been
        /// drawn but before it has been composited into the image.
        ///
        /// See [MaskFilter] for details.
        public MaskFilter MaskFilter
        {
            get
            {
                switch (_data.GetInt32(_kMaskFilterOffset, _kFakeHostEndian))
                {
                    case MaskFilter._TypeNone:
                        return null;
                    case MaskFilter._TypeBlur:
                        return MaskFilter.Blur(
                            (BlurStyle)(_data.GetInt32(_kMaskFilterBlurStyleOffset, _kFakeHostEndian)),
                            _data.GetFloat32(_kMaskFilterSigmaOffset, _kFakeHostEndian));
                }
                return null;
            }
            set
            {
                if (value == null)
                {
                    _data.SetInt32(_kMaskFilterOffset, MaskFilter._TypeNone, _kFakeHostEndian);
                    _data.SetInt32(_kMaskFilterBlurStyleOffset, 0, _kFakeHostEndian);
                    _data.SetFloat32(_kMaskFilterSigmaOffset, 0.0, _kFakeHostEndian);
                }
                else
                {
                    // For now we only support one kind of MaskFilter, so we don't need to
                    // check what the type is if it's not null.
                    _data.SetInt32(_kMaskFilterOffset, MaskFilter._TypeBlur, _kFakeHostEndian);
                    _data.SetInt32(_kMaskFilterBlurStyleOffset, (int)(value._style), _kFakeHostEndian);
                    _data.SetFloat32(_kMaskFilterSigmaOffset, value._sigma, _kFakeHostEndian);
                }
            }
        }

        /// Controls the performance vs quality trade-off to use when applying
        /// filters, such as [maskFilter], or when drawing images, as with
        /// [Canvas.drawImageRect] or [Canvas.drawImageNine].
        ///
        /// Defaults to [FilterQuality.none].
        // TODO(ianh): verify that the image drawing methods actually respect this
        public FilterQuality FilterQuality
        {
            get => (FilterQuality)(_data.GetInt32(_kFilterQualityOffset, _kFakeHostEndian));
            set
            {
                int encoded = (int)value;
                _data.SetInt32(_kFilterQualityOffset, encoded, _kFakeHostEndian);
            }
        }

        /// The shader to use when stroking or filling a shape.
        ///
        /// When this is null, the [color] is used instead.
        ///
        /// See also:
        ///
        ///  * [Gradient], a shader that paints a color gradient.
        ///  * [ImageShader], a shader that tiles an [Image].
        ///  * [colorFilter], which overrides [shader].
        ///  * [color], which is used if [shader] and [colorFilter] are null.
        public Shader Shader
        {
            get
            {
                if (_objects == null)
                    return null;
                return null; //_objects[_kShaderIndex]; TODO: Show how it work dynamic list
            }
            set
            {
                if (_objects == null)
                    _objects = new List<dynamic>(_kObjectCount);
                _objects[_kShaderIndex] = value;
            }
        }

        /// A color filter to apply when a shape is drawn or when a layer is
        /// composited.
        ///
        /// See [ColorFilter] for details.
        ///
        /// When a shape is being drawn, [colorFilter] overrides [color] and [shader].
        public ColorFilter ColorFilter
        {
            get
            {
                switch (_data.GetInt32(_kColorFilterOffset, _kFakeHostEndian))
                {
                    case ColorFilter._TypeNone:
                        return null;
                    case ColorFilter._TypeMode:
                        return ColorFilter.Mode(
                            new Color(_data.GetInt32(_kColorFilterColorOffset, _kFakeHostEndian)),
                            (BlendMode)(_data.GetInt32(_kColorFilterBlendModeOffset, _kFakeHostEndian)));
                    case ColorFilter._TypeMatrix:
                        return null; //ColorFilter.Matrix(_objects[_kColorFilterMatrixIndex]); TODO: Show how it work dynamic list
                    case ColorFilter._TypeLinearToSrgbGamma:
                        return ColorFilter.LinearToSrgbGamma();
                    case ColorFilter._TypeSrgbToLinearGamma:
                        return ColorFilter.SrgbToLinearGamma();
                }

                return null;
            }
            set
            {
                if (value == null)
                {
                    _data.SetInt32(_kColorFilterOffset, ColorFilter._TypeNone, _kFakeHostEndian);
                    _data.SetInt32(_kColorFilterColorOffset, 0, _kFakeHostEndian);
                    _data.SetInt32(_kColorFilterBlendModeOffset, 0, _kFakeHostEndian);

                    if (_objects != null)
                    {
                        _objects[_kColorFilterMatrixIndex] = null;
                    }
                }
                else
                {
                    _data.SetInt32(_kColorFilterOffset, value._type, _kFakeHostEndian);

                    if (value._type == ColorFilter._TypeMode)
                    {
                        Debug.Assert(value._color != null);
                        Debug.Assert(value._blendMode != null);

                        _data.SetInt32(_kColorFilterColorOffset, value._color.Value, _kFakeHostEndian);
                        _data.SetInt32(_kColorFilterBlendModeOffset, (int)value._blendMode, _kFakeHostEndian);
                    }
                    else if (value._type == ColorFilter._TypeMatrix)
                    {
                        Debug.Assert(value._matrix != null);

                        if (_objects == null)
                            _objects = new List<dynamic>(_kObjectCount);
                        _objects[_kColorFilterMatrixIndex] = Float32List.FromList(value._matrix);
                    }
                }
            }
        }

        /// Whether the colors of the image are inverted when drawn.
        ///
        /// Inverting the colors of an image applies a new color filter that will
        /// be composed with any user provided color filters. This is primarily
        /// used for implementing smart invert on iOS.
        public bool InvertColors
        {
            get => _data.GetInt32(_kInvertColorOffset, _kFakeHostEndian) == 1;
            set => _data.SetInt32(_kInvertColorOffset, value ? 1 : 0, _kFakeHostEndian);
        }

        public override string ToString()
        {
            StringBuilder result = new StringBuilder();
            string semicolon = "";
            result.Append("Paint(");
            if (Style == PaintingStyle.Stroke)
            {
                result.Append(Style);
                if (StrokeWidth != 0.0)
                    result.Append($" {StrokeWidth.ToStringAsFixed(1)}");
                else
                    result.Append(" hairline");
                if (StrokeCap != StrokeCap.Butt)
                    result.Append($" {StrokeCap}");
                if (StrokeJoin == StrokeJoin.Miter)
                {
                    if (StrokeMiterLimit != _kStrokeMiterLimitDefault)
                        result.Append($" $strokeJoin up to ${StrokeMiterLimit.ToStringAsFixed(1)}");
                }
                else
                {
                    result.Append($" {StrokeJoin}");
                }
                semicolon = "; ";
            }
            if (IsAntiAlias != true)
            {
                result.Append($"{semicolon}antialias off");
                semicolon = "; ";
            }
            if (Color != new Color(_kColorDefault))
            {
                if (Color != null)
                    result.Append($"{semicolon}{Color}");
                else
                    result.Append($"{semicolon}no color");
                semicolon = "; ";
            }
            if ((int)BlendMode != _kBlendModeDefault)
            {
                result.Append($"{semicolon}{BlendMode}");
                semicolon = "; ";
            }
            if (ColorFilter != null)
            {
                result.Append($"{semicolon}colorFilter: {ColorFilter}");
                semicolon = "; ";
            }
            if (MaskFilter != null)
            {
                result.Append($"{semicolon}maskFilter: {MaskFilter}");
                semicolon = "; ";
            }
            if (FilterQuality != FilterQuality.None)
            {
                result.Append($"{semicolon}FilterQuality: {FilterQuality}");
                semicolon = "; ";
            }
            if (Shader != null)
            {
                result.Append($"{semicolon}shader: {Shader}");
                semicolon = "; ";
            }
            if (InvertColors)
                result.Append($"{semicolon}invert: {InvertColors}");
            result.Append(")");
            return result.ToString();
        }
    }
}