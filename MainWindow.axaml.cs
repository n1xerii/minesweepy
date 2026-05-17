using System;
using System.Data.Common;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Media;

namespace minesweepy;

public partial class MainWindow : Window
{
    private Cell[,]? cells;
    private Button[,]? buttons;
    
    private int rows;
    private int columns;
    private int mineCount;
    
    public int Rows
    {
        get { return rows; }
        set 
        { 
            if (value <= 0) { rows = 10; }
            else { rows = value; }
        }
    }
    public int Columns
    {
        get { return columns; }
        set 
        {
            if (value <= 0) { columns = 10; }
            else { columns = value; }
        }
    }
    public int MineCount
    {
        get { return mineCount; }
        set 
        {
            if (value <= 0) { mineCount = 1; }
            else { mineCount = value; }
        }
    }
    
    
    public MainWindow()
    {
        InitializeComponent();
        
        SetGameData(10, 10, 1);
        MakeBoard(rows, columns);
        FindNeighbors(1,1);
    }

    private void SetGameData(int amountOfRows, int amountOfCols, int amountOfMines)
    {
        Rows = amountOfRows;
        Columns = amountOfCols;
        mineCount = amountOfMines;
    }
    public void MakeBoard(int givenRows, int givenCols)
    {
        BoardGrid.RowDefinitions.Clear();
        BoardGrid.ColumnDefinitions.Clear();
        BoardGrid.Children.Clear();
        SetBoardGridDefinitions();
        
        cells = new Cell[Rows, Columns];
        buttons = new Button[Rows, Columns];

        for (int r = 0; r < Rows; r++)
        {
            for (int c = 0; c < Columns; c++)
            {
                cells[r, c] = new Cell();
                
                var button = new Button();
                buttons[r, c] = button;
                    
                button.Tag = (r, c);
                button.Name =  $"{r}_{c}";
                //button.Click += Cell_Click;
                button.Click += Cell_Click;
                button.ContextRequested += Cell_RightClick;
                
                Grid.SetRow(button, r);
                Grid.SetColumn(button, c);
                
                button.HorizontalAlignment = Avalonia.Layout.HorizontalAlignment.Stretch;
                button.VerticalAlignment = Avalonia.Layout.VerticalAlignment.Stretch;
                
                button.Background = Brushes.DimGray;
                
                BoardGrid.Children.Add(button);
            }
        }
        
        Console.WriteLine("Amount of cells: " + cells.Length);
    }
    
    private void FindNeighbors(int row, int col)
    {
        if (cells == null) return;

        Console.WriteLine($"Selected cell: {row}R {col}C");

        for (int rowOffset = -1; rowOffset <= 1; rowOffset++)
        {
            for (int colOffset = -1; colOffset <= 1; colOffset++)
            {
                if (rowOffset == 0 && colOffset == 0)
                    continue;

                int neighborRow = row + rowOffset;
                int neighborCol = col + colOffset;
                
                if (neighborRow < 0 || neighborRow >= Rows ||
                    neighborCol < 0 || neighborCol >= Columns)
                {
                    continue;
                }

                var neighbor = cells[neighborRow, neighborCol];

                Console.WriteLine(
                    $"Neighbor at {neighborRow}R {neighborCol}C");
            }
        }
    }

    private void RevealCell(Button cellBtn)
    {
        if (cells == null) { return; }

        var (r, c) = ((int, int))cellBtn.Tag;
        Cell thisCell = cells[r, c];

        thisCell.revealed = true;
        if (thisCell.isBomb)
        {
            cellBtn.Background = Brushes.Red;
            //GameOver();
            return;
        }
    }
    private void FlagCell(Button cellBtn)
    {
        if (cells == null) { return; }
        
        var (r, c) = ((int, int))cellBtn.Tag;
        Cell thisCell = cells[r, c];
        
        thisCell.flagged = !thisCell.flagged;

        if (thisCell.flagged) { cellBtn.Background = Brushes.Orange; }
        else { cellBtn.Background = Brushes.DimGray; }
    }
    
    private void Cell_Click(object? sender, RoutedEventArgs e)
    {
        if (sender is not Button button)
            return;
        
        Console.WriteLine("Left click");
        
        var (r, c) = ((int, int))button.Tag;

        button.Background = Brushes.CornflowerBlue;
        RevealCell(button);
    }
    private void Cell_RightClick(object? sender, RoutedEventArgs e)
    {
        if (sender is not Button button)
            return;
        
        Console.WriteLine("Right click");
        
        FlagCell(button);
    }
    
    
    private void SetBoardGridDefinitions()
    {
        for (int r = 0; r < Rows; r++) { BoardGrid.RowDefinitions.Add(new RowDefinition(GridLength.Star)); }
        for (int c = 0; c < Columns; c++) { BoardGrid.ColumnDefinitions.Add(new ColumnDefinition(GridLength.Star)); }
    }
    
    // SETTINGS
    private void Settings_Click(object? sender, RoutedEventArgs e)
    {
        var window = new SettingsWindow();
        window.Show();
    }
}