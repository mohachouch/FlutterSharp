namespace FlutterSharp.UI
{
    /// Defines how a list of points is interpreted when drawing a set of triangles.
    ///
    /// Used by [Canvas.drawVertices].
    // These enum values must be kept in sync with SkVertices::VertexMode.
    public enum VertexMode
    {
        /// Draw each sequence of three points as the vertices of a triangle.
        Triangles,

        /// Draw each sliding window of three points as the vertices of a triangle.
        TriangleStrip,

        /// Draw the first point and each sliding window of two points as the vertices of a triangle.
        TriangleFan,
    }
}
