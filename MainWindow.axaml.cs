using System;
using Avalonia.Controls;
using Avalonia.Interactivity;

namespace minesweepy;

public partial class MainWindow : Window
{
    private Cell[,]? cells;
    //private Button[,] buttons;
    
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
        
        cells = new Cell[rows, cols];

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
                cells[r, c] = new Cell();
                var button = new Button();
                
                Grid.SetRow(button, r);
                Grid.SetColumn(button, c);
                
                button.HorizontalAlignment = Avalonia.Layout.HorizontalAlignment.Stretch;
                button.VerticalAlignment = Avalonia.Layout.VerticalAlignment.Stretch;
                
                BoardGrid.Children.Add(button);
            }
        }
        
        Console.WriteLine("Amount of cells: " + cells.Length);
    }
    
    //private void CellClicked(object? sender, RoutedEventArgs e)
    //{
    //    if (sender is Button btn)
    //        btn.Content = "•";
    //}
    
    private void Settings_Click(object? sender, RoutedEventArgs e)
    {
        var window = new SettingsWindow();
        window.Show();
    }
}