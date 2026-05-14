using Avalonia.Controls;

namespace minesweepy;

public partial class MainWindow : Window
{
    private Cell[,] cells;
    
    public MainWindow()
    {
        InitializeComponent();
    }
}