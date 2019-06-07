using System.Diagnostics;

namespace FlutterSharp.UI
{
    public class PathMeasure : NativeFieldWrapperClass2
    {
        public PathMeasure(Path path, bool forceClosed)
        {
            CurrentContourIndex = -1; // nextContour will increment this to the zero based index.
            Constructor(path, forceClosed);
        }

        private void Constructor(Path path, bool forceClosed)
        {
            // TODO : native 'PathMeasure_constructor';
        }

        public double Length(int contourIndex)
        {
            Debug.Assert(contourIndex <= CurrentContourIndex, $"Iterator must be advanced before index {contourIndex} can be used.");
            return 0.0;
            // TODO : native 'PathMeasure_getLength';
        }

        public Tangent GetTangentForOffset(int contourIndex, double distance)
        {
            Debug.Assert(contourIndex <= CurrentContourIndex, $"Iterator must be advanced before index {contourIndex} can be used.");
            Float32List posTan = GetPosTan(contourIndex, distance);
            // first entry == 0 indicates that Skia returned false
            if (posTan[0] == 0.0)
            {
                return null;
            }
            else
            {
                return new Tangent(
                  new Offset(posTan[1], posTan[2]),
                  new Offset(posTan[3], posTan[4])
                );
            }
        }

        private Float32List GetPosTan(int contourIndex, double distance)
        {
            // TODO :  native 'PathMeasure_getPosTan';
            return null;
        }

        public Path ExtractPath(int contourIndex, double start, double end, bool startWithMoveTo = true)
        {
            Debug.Assert(contourIndex <= CurrentContourIndex, $"Iterator must be advanced before index {contourIndex} can be used.");
            return null;
            // TODO : native 'PathMeasure_getSegment';
        }

        public bool IsClosed(int contourIndex)
        {
            Debug.Assert(contourIndex <= CurrentContourIndex, $"Iterator must be advanced before index {contourIndex} can be used.");
            return false;
            // TODO :  native 'PathMeasure_isClosed';
        }

        // Move to the next contour in the path.
        //
        // A path can have a next contour if [Path.moveTo] was called after drawing began.
        // Return true if one exists, or false.
        private bool NextContour()
        {
            bool next = NativeNextContour();
            if (next)
            {
                CurrentContourIndex++;
            }
            return next;
        }

        private bool NativeNextContour()
        {
            // TODO : native 'PathMeasure_nextContour';
            return false;
        }

        public int CurrentContourIndex;
    }
}
