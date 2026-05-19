using System;
using System.Dynamic;
using Avalonia.Controls;
using Avalonia.Interactivity;
using static minesweepy.MainWindow;

namespace minesweepy;

public partial class SettingsWindow : Window
{
    private MainWindow main;
    
    public SettingsWindow(MainWindow mainWindow)
    {
        InitializeComponent();
        
        main = mainWindow;
    }

    private void NewGame_Click(object sender, RoutedEventArgs e)
    {
        TextBox rowBox = RowBox;
        TextBox columnBox = ColumnBox;
        TextBox mineBox = MineBox;

        if (rowBox.Text == "" ||
            columnBox.Text == "" ||
            mineBox.Text == "" ||
            rowBox.Text == null ||
            columnBox.Text == null ||
            mineBox.Text == null)
        {
            return;
        }
        
        main.Rows = int.Parse(rowBox.Text);
        main.Columns = int.Parse(columnBox.Text);
        main.MineCount = int.Parse(mineBox.Text);
        if (main.MineCount > main.Rows * main.Columns)
        {
            Console.WriteLine("**Too many mines!");
            return;
        }
        if (main.Rows > 50 || main.Columns > 50)
        {
            Console.WriteLine("**Too many cells!");
            return;
        }
        
        main.SetGameData(main.Rows, main.Columns, main.MineCount);
        main.MakeBoard();
        main.MakeMines(main.Rows, main.Columns,  main.MineCount);
    }
}