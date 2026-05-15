using Avalonia.Controls;

namespace minesweepy;

public partial class MainWindow : Window
{
    //private Cell[,] cells;
    private int rows = 10;
    private int cols = 10;
    
    
    public MainWindow()
    {
        InitializeComponent();
        SetBoard(rows, cols);
    }

    public void SetBoard(int givenRows, int givenCols)
    {
        BoardGrid.RowDefinitions.Clear();
        BoardGrid.ColumnDefinitions.Clear();
        BoardGrid.Children.Clear();

        for (int r = 0; r < rows; r++)
        {
            BoardGrid.RowDefinitions.Add(new RowDefinition(GridLength.Star));
        }
        for (int c = 0; c < cols; c++)
        {
            BoardGrid.ColumnDefinitions.Add(new ColumnDefinition(GridLength.Star));
        }

        for (int r = 0; r < rows; r++)
        {
            for (int c = 0; c < cols; c++)
            {
                var button = new Button();
                
                Grid.SetRow(button, r);
                Grid.SetColumn(button, c);
                
                button.HorizontalAlignment = Avalonia.Layout.HorizontalAlignment.Stretch;
                button.VerticalAlignment = Avalonia.Layout.VerticalAlignment.Stretch;
                
                BoardGrid.Children.Add(button);
            }
        }
    }
    
    //private void CellClicked(object? sender, RoutedEventArgs e)
    //{
    //    if (sender is Button btn)
    //        btn.Content = "•";
    //}
}