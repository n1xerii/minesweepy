using System.Numerics;

namespace minesweepy;

public class Cell
{
    public bool revealed{ get; set; }
    public bool isBomb{ get; set; }
    public int neighbors { get; set; }
    public bool flagged { get; set; }
}