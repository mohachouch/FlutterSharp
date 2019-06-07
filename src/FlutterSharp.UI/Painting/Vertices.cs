using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace FlutterSharp.UI
{
    /// A set of vertex data used by [Canvas.drawVertices].
    public class Vertices : NativeFieldWrapperClass2
    {
        public Vertices(VertexMode mode, List<Offset> positions, List<Offset> textureCoordinates = null,
            List<Color> colors = null, List<int> indices = null)
        {
            Debug.Assert(mode != null);
            Debug.Assert(positions != null);

            if (textureCoordinates != null && textureCoordinates.Count != positions.Count)
                throw new ArgumentException("'positions' and 'textureCoordinates' lengths must match.");
            if (colors != null && colors.Count != positions.Count)
                throw new ArgumentException("'positions' and 'colors' lengths must match.");
            if (indices != null && indices.Any(i => i < 0 || i >= positions.Count))
                throw new ArgumentException("'indices' values must be valid indices in the positions list.");

            Float32List encodedPositions = EncodePointList(positions);
            Float32List encodedTextureCoordinates = (textureCoordinates != null)
              ? EncodePointList(textureCoordinates)
              : null;
            Int32List encodedColors = colors != null
              ? EncodeColorList(colors)
              : null;
            Uint16List encodedIndices = indices != null
              ? Uint16List.FromList(indices)
              : null;

            Constructor();
            if (!Init((int)mode, encodedPositions, encodedTextureCoordinates, encodedColors, encodedIndices))
                throw new ArgumentException("Invalid configuration for vertices.");
        }

        // Constuctor for raw static method
        public Vertices(VertexMode mode, Float32List positions, Float32List textureCoordinates = null,
            Int32List colors = null, Uint16List indices = null)
        {
            Debug.Assert(mode != null);
            Debug.Assert(positions != null);

            if (textureCoordinates != null && textureCoordinates.Count != positions.Count)
                throw new ArgumentException("'positions' and 'textureCoordinates' lengths must match.");
            if (colors != null && colors.Count != positions.Count)
                throw new ArgumentException("'positions' and 'colors' lengths must match.");
            if (indices != null && indices.Any(i => i < 0 || i >= positions.Count))
                throw new ArgumentException("'indices' values must be valid indices in the positions list.");

            Constructor();
            if (!Init((int)mode, positions, textureCoordinates, colors, indices))
                throw new ArgumentException("Invalid configuration for vertices.");
        }

        public static Vertices Raw(VertexMode mode, Float32List positions, Float32List textureCoordinates = null,
            Int32List colors = null, Uint16List indices = null)
        {
            return new Vertices(mode, positions, textureCoordinates, colors, indices);
        }

        private void Constructor()
        {
            // TODO : native 'Vertices_constructor';
        }

        private bool Init(int mode,
                 Float32List positions,
                 Float32List textureCoordinates,
                 Int32List colors,
                 Uint16List indices)
        {
            // TODO :  native 'Vertices_init';
            return false;
        }
    }
}
