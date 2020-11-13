using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace PROAtas.Assets.Styles
{
    public static class GridStyle
    {
        public static TGrid Standard<TGrid>(this TGrid grid) where TGrid : Grid
        {
            grid.RowSpacing = 0;
            grid.ColumnSpacing = 0;

            return grid;
        }

        public static TGrid Transparent<TGrid>(this TGrid grid) where TGrid : Grid
        {
            grid.CascadeInputTransparent = false;
            grid.InputTransparent = true;

            return grid;
        }
    }
}
