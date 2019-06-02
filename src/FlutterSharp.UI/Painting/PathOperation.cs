namespace FlutterSharp.UI
{
    /// Strategies for combining paths.
    ///
    /// See also:
    ///
    /// * [Path.combine], which uses this enum to decide how to combine two paths.
    // Must be kept in sync with SkPathOp
    public enum PathOperation
    {
        /// Subtract the second path from the first path.
        ///
        /// For example, if the two paths are overlapping circles of equal diameter
        /// but differing centers, the result would be a crescent portion of the
        /// first circle that was not overlapped by the second circle.
        ///
        /// See also:
        ///
        ///  * [reverseDifference], which is the same but subtracting the first path
        ///    from the second.
        Difference,
        /// Create a new path that is the intersection of the two paths, leaving the
        /// overlapping pieces of the path.
        ///
        /// For example, if the two paths are overlapping circles of equal diameter
        /// but differing centers, the result would be only the overlapping portion
        /// of the two circles.
        ///
        /// See also:
        ///  * [xor], which is the inverse of this operation
        Intersect,
        /// Create a new path that is the union (inclusive-or) of the two paths.
        ///
        /// For example, if the two paths are overlapping circles of equal diameter
        /// but differing centers, the result would be a figure-eight like shape
        /// matching the outer boundaries of both circles.
        Union,
        /// Create a new path that is the exclusive-or of the two paths, leaving
        /// everything but the overlapping pieces of the path.
        ///
        /// For example, if the two paths are overlapping circles of equal diameter
        /// but differing centers, the figure-eight like shape less the overlapping parts
        ///
        /// See also:
        ///  * [intersect], which is the inverse of this operation
        Xor,
        /// Subtract the first path from the second path.
        ///
        /// For example, if the two paths are overlapping circles of equal diameter
        /// but differing centers, the result would be a crescent portion of the
        /// second circle that was not overlapped by the first circle.
        ///
        /// See also:
        ///
        ///  * [difference], which is the same but subtracting the second path
        ///    from the first.
        ReverseDifference,
    }
}