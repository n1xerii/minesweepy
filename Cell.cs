using System.Collections.Generic;
using System.Numerics;

namespace minesweepy;

public class Cell
{
    public bool revealed{ get; set; }
    public bool isBomb{ get; set; }
    public List<Cell> neighbors = new List<Cell>();
    public bool flagged { get; set; }
}