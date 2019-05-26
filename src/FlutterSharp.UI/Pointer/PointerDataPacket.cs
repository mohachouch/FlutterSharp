using System.Collections.Generic;

namespace FlutterSharp.UI
{
    /// A sequence of reports about the state of pointers.  
    public class PointerDataPacket
    {
        /// Creates a packet of pointer data reports.
        public PointerDataPacket(List<PointerData> data = null)
        {
            this.Data = data ?? new List<PointerData>();
        }

        /// Data about the individual pointers in this packet.
        ///
        /// This list might contain multiple pieces of data about the same pointer.
        public readonly List<PointerData> Data;
    }
}
