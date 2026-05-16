using System;
using System.Data.Common;
using Avalonia.Controls;
using Avalonia.Interactivity;

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
                
                Grid.SetRow(button, r);
                Grid.SetColumn(button, c);
                
                button.HorizontalAlignment = Avalonia.Layout.HorizontalAlignment.Stretch;
                button.VerticalAlignment = Avalonia.Layout.VerticalAlignment.Stretch;
                
                BoardGrid.Children.Add(button);
            }
        }
        
        Console.WriteLine("Amount of cells: " + cells.Length);
    }

    private void FindNeighbors(int row, int col)
    {
        var cell = cells[row, col];
        if (cell == null) { return; }
        
        Console.WriteLine("Selected cell: " + row + "R " + col + "C");
    }

    private void RevealCell(int atRow, int atCol)
    {
        if (cells == null) { return; }
        cells[atRow, atCol].revealed = true;
    }

    private void FlagCell(Cell cell, int atRow, int atCol)
    {
        if (cells == null) { return; }
        cells[atRow, atCol].flagged = true;
    }
    
    //private void CellClicked(object? sender, RoutedEventArgs e)
    //{
    //    if (sender is Button btn)
    //        btn.Content = "•";
    //}

    private void SetBoardGridDefinitions()
    {
        for (int r = 0; r < Rows; r++) { BoardGrid.RowDefinitions.Add(new RowDefinition(GridLength.Star)); }
        for (int c = 0; c < Columns; c++) { BoardGrid.ColumnDefinitions.Add(new ColumnDefinition(GridLength.Star)); }
    }
    
    private void Settings_Click(object? sender, RoutedEventArgs e)
    {
        var window = new SettingsWindow();
        window.Show();
    }
}