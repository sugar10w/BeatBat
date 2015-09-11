
using System.Windows.Forms;
using System.Drawing;

public class Pict
{
    public int Top = 0, Left = 0, Height = 50, Width = 50;
    public Bitmap Image;
    public bool Visible = true, Front = false;
}

public abstract class Prop
{
	public enum Direction{Up,Down,Left,Right,None};
    public Pict pict;
    public Game1.Form1.PictId Id;
}