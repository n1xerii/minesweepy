using System.Numerics;

namespace minesweepy;

public class Cell
{
    private int id;
    private Vector2 position;
    private bool revealed;
    private bool isBomb;
}