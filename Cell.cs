using System.Collections.Generic;
using Avalonia.Controls;

namespace minesweepy;

public class Cell
{
    public int myRow { get; set; }
    public int myCol { get; set; }
    public bool revealed{ get; set; }
    public bool isBomb{ get; set; }
    public List<Cell> neighbors = new List<Cell>();
    public bool flagged { get; set; }
}