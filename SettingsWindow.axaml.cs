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
        
        if (string.IsNullOrEmpty(rowBox.Text) || 
            string.IsNullOrEmpty(columnBox.Text))
        {
            return;
        }
        
        int rowsNum = int.Parse(rowBox.Text);
        int colsNum = int.Parse(columnBox.Text);
        
        if (rowsNum > 50 || colsNum > 50)
        {
            Console.WriteLine("**Too many cells!");
            return;
        }
        
        double customMinePercentage;
        if (string.IsNullOrEmpty(mineBox.Text) || Convert.ToInt32(mineBox.Text) <= 0)
        {
            Console.WriteLine("**No mine percentage given, using default: " + main.defaultMinePercentage);
            customMinePercentage = rowsNum * colsNum * main.defaultMinePercentage;
        }
        else if (Convert.ToInt32(mineBox.Text) >= rowsNum * colsNum)
        {
            Console.WriteLine("**Too many mines!");
            return;
        }
        else
        {
            customMinePercentage = double.Parse(mineBox.Text); 
        }
        
        main.NewGame(rowsNum, colsNum, customMinePercentage);
    }

    }
}