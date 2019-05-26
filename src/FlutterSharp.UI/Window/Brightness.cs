﻿namespace FlutterSharp.UI
{
    /// Describes the contrast of a theme or color palette.
    public enum Brightness
    {
        /// The color is dark and will require a light text color to achieve readable
        /// contrast.
        ///
        /// For example, the color might be dark grey, requiring white text.
        Dark,

        /// The color is light and will require a dark text color to achieve readable
        /// contrast.
        ///
        /// For example, the color might be bright white, requiring black text.
        Light,
    }

}
