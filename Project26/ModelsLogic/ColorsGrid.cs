using Project26.Models;

namespace Project26.ModelsLogic
{
    internal class ColorsGrid : Models.ColorsGridModel
    {
        public ColorsGrid(Grid grdColors) : base(grdColors)
        {
            for (int row = 0; row < GridColors.GetLength(0); row++)
                for (int col = 0; col < GridColors.GetLength(1); col++)
                {
                    ColoredButton btn = new (50, GridColors[row, col]);
                    btn.Clicked += OnButtonClicked;
                    colorsGrid.Add(new Border() {StrokeThickness = 5, Stroke = Colors.Transparent, Content = btn }, col, row);
                }
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
            ColorSelected?.Invoke(this, EventArgs.Empty);
        }
    }
}
