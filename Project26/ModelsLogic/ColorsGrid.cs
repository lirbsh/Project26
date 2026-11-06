using Project26.Models;

namespace Project26.ModelsLogic
{
    internal class ColorsGrid : ColorsGridModel
    {
        public ColorsGrid(Grid grdColors) : base(grdColors)
        {
            Color strokeColor;
            for (int row = 0; row < GridColors.GetLength(0); row++)
                for (int col = 0; col < GridColors.GetLength(1); col++)
                {
                    ColoredButton btn = new (50, GridColors[row, col]);
                    btn.Clicked += OnButtonClicked;
                    strokeColor = (row == DefaultColorRow && col == DefaultColorColumn) ?
                         Colors.SteelBlue : Colors.Transparent;
                    Border brd = new () { StrokeThickness = 5, Stroke = strokeColor, Content = btn };
                    colorsGrid.Add(brd, col, row);
                    if (row == DefaultColorRow && col == DefaultColorColumn)
                        prevBorder = brd;
                }
        }

        public override void UnselectColor()
        {
            prevBorder?.Stroke = Colors.Transparent;
            prevBorder = null;
        }

        private void OnButtonClicked(object? sender, EventArgs e)
        {
            ColoredButton btn = (ColoredButton)sender!;
            Border brd = (Border)btn.Parent;
            brd.Stroke = Colors.SteelBlue;
            if (prevBorder != null && prevBorder != brd)
                prevBorder.Stroke = Colors.Transparent;
            prevBorder = brd;
            SelectedColor = btn.Color;
            base.OnColorSelected();
        }
    }
}
