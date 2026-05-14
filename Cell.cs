using System.Numerics;

namespace minesweepy;

public class Cell
{
    public int id { get; set; }
    public int x { get; set; }
    public int y { get; set; }
    
    public bool revealed{ get; set; }
    public bool isBomb{ get; set; }
    public int neighbors { get; set; }
    public bool isFlagged { get; set; }
}