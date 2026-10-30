namespace Project26.Models
{
    internal class ColorsGridModel
    {
        protected Border? prevBorder;
        protected Color[,] GridColors =
        {
            { Colors.Red, Colors.Green, Colors.Blue, Colors.Pink },
            { Colors.Yellow, Colors.Orange, Colors.Purple, Colors.Lime },
            { Colors.Cyan, Colors.Magenta, Colors.Brown, Colors.Teal },
            { Colors.Gray, Colors.Black, Colors.Gold, Colors.Silver }
        };
        protected readonly Grid colorsGrid;
        public Color SelectedColor = Colors.Black;
        public EventHandler? ColorSelected;
        public ColorsGridModel(Grid grdColors)
        {
            colorsGrid = grdColors;
            for (int row = 0; row < GridColors.GetLength(0); row++)
                grdColors.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
            for (int col = 0; col < GridColors.GetLength(1); col++)
                grdColors.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Auto });
        }
    }
}
