namespace FlutterSharp.UI
{
    /// Styles to use for blurs in [MaskFilter] objects.
    // These enum values must be kept in sync with SkBlurStyle.
    public enum BlurStyle
    {
        // These mirror SkBlurStyle and must be kept in sync.

        /// Fuzzy inside and outside. This is useful for painting shadows that are
        /// offset from the shape that ostensibly is casting the shadow.
        Normal,

        /// Solid inside, fuzzy outside. This corresponds to drawing the shape, and
        /// additionally drawing the blur. This can make objects appear brighter,
        /// maybe even as if they were fluorescent.
        Solid,

        /// Nothing inside, fuzzy outside. This is useful for painting shadows for
        /// partially transparent shapes, when they are painted separately but without
        /// an offset, so that the shadow doesn't paint below the shape.
        Outer,

        /// Fuzzy inside, nothing outside. This can make shapes appear to be lit from
        /// within.
        Inner,
    }
}