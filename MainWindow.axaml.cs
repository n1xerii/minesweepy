using System;
using System.Collections.Generic;
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
                cells[r, c].myRow = r;
                cells[r, c].myCol = c;
                
                var button = new Button();
                buttons[r, c] = button;
                    
                button.Tag = (r, c);
                button.Name =  $"{r}_{c}";
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

    private void RecursiveCellReveal(int row, int col)
    {
        if (cells == null) return;
        
        // BOUNDS
        if (row < 0 || row >= Rows ||
            col < 0 || col >= Columns)
            return;

        Cell cell = cells[row, col];

        if (cell.revealed) return;
        if (cell.flagged)
        {
            Console.WriteLine("**Unflag before clicking.");
            return;
        }

        cell.revealed = true;

        Button button = buttons[row, col];
        
        if (cell.isBomb)
        {
            button.Background = Brushes.Red;
            return;
        }

        button.Background = Brushes.LightGreen;
        
        int bombCount = CountAdjacentMines(row, col);
        
        if (bombCount > 0)
        {
            button.Content = bombCount.ToString();
            return;
        }
        
        for (int rowOffset = -1; rowOffset <= 1; rowOffset++)
        {
            for (int colOffset = -1; colOffset <= 1; colOffset++)
            {
                if (rowOffset == 0 && colOffset == 0)
                    continue;

                RecursiveCellReveal(row + rowOffset, col + colOffset);
            }
        }
    }

    private int CountAdjacentBombs(int row, int col)
    {
        int count = 0;

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
                    continue;

                if (cells[neighborRow, neighborCol].isBomb)
                    count++;
            }
        }

        return count;
    }
    
    // FLAGGING
    private void FlagCell(int row, int col)
    {
        if (cells == null) { return; }
        
        Cell thisCell = cells[row, col];
        Button cellBtn = buttons[row, col];
        
        thisCell.flagged = !thisCell.flagged;

        if (thisCell.flagged) { cellBtn.Background = Brushes.Orange; }
        else { cellBtn.Background = Brushes.DimGray; }
    }
    
    // CLICKS
    private void Cell_Click(object? sender, RoutedEventArgs e)
    {
        if (sender is not Button button)
            return;
        
        Console.WriteLine("Left click");
        
        var (r, c) = ((int, int))button.Tag;

        RecursiveCellReveal(r, c);
    }
    private void Cell_RightClick(object? sender, RoutedEventArgs e)
    {
        if (sender is not Button button)
            return;
        
        var (r, c) = ((int, int))button.Tag;
        
        Console.WriteLine("Right click");
        
        FlagCell(r, c);
    }
    
    // BOARD UI
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