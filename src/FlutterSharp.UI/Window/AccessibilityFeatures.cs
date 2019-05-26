using System.Collections.Generic;

namespace FlutterSharp.UI
{
    /// Additional accessibility features that may be enabled by the platform.
    ///
    /// It is not possible to enable these settings from Flutter, instead they are
    /// used by the platform to indicate that additional accessibility features are
    /// enabled.
    //
    // When changes are made to this class, the equivalent APIs in each of the
    // embedders *must* be updated.
    public class AccessibilityFeatures
    {
        public AccessibilityFeatures(int _index)
        {
            this.Index = _index;
        }

        public const int _kAccessibleNavigation = 1 << 0;
        public const int _kInvertColorsIndex = 1 << 1;
        public const int _kDisableAnimationsIndex = 1 << 2;
        public const int _kBoldTextIndex = 1 << 3;
        public const int _kReduceMotionIndex = 1 << 4;

        // A bitfield which represents each enabled feature.
        public readonly int Index;

        /// Whether there is a running accessibility service which is changing the
        /// interaction model of the device.
        ///
        /// For example, TalkBack on Android and VoiceOver on iOS enable this flag.
        public bool AccessibleNavigation => (_kAccessibleNavigation & this.Index) != 0;

        /// The platform is inverting the colors of the application.
        public bool InvertColors => (_kInvertColorsIndex & this.Index) != 0;

        /// The platform is requesting that animations be disabled or simplified.
        public bool DisableAnimations => (_kDisableAnimationsIndex & this.Index) != 0;

        /// The platform is requesting that text be rendered at a bold font weight.
        ///
        /// Only supported on iOS.
        public bool BoldText => (_kBoldTextIndex & this.Index) != 0;

        /// The platform is requesting that certain animations be simplified and
        /// parallax effects removed.
        ///
        /// Only supported on iOS.
        public bool ReduceMotion => (_kReduceMotionIndex & this.Index) != 0;

        public override string ToString()
        {
            List<string> features = new List<string>();
            if (this.AccessibleNavigation)
                features.Add("accessibleNavigation");
            if (this.InvertColors)
                features.Add("invertColors");
            if (this.DisableAnimations)
                features.Add("disableAnimations");
            if (this.BoldText)
                features.Add("boldText");
            if (this.ReduceMotion)
                features.Add("reduceMotion");

            return $"AccessibilityFeatures{string.Join(", ", features)}";
        }
        
        public override bool Equals(object obj)
        {
            if (obj is AccessibilityFeatures accessibilityFeatures)
                return this.Index == accessibilityFeatures.Index;

            return false;
        }

        public override int GetHashCode()
        {
            return this.Index.GetHashCode();
        }
    }
}
