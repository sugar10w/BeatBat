using System;
using System.Windows.Forms;

public abstract class Mover:Prop
{
	protected int x,y;
    public int X { get { return x; } set { x = value; } }
    public int Y { get { return y; } set { y = value; } }

    public bool Attacking;
	protected Random random=new Random();

	public virtual void SetPlace(Map map)
	{
		int xx,yy;
		do
		{
			xx=random.Next(0,map.X);
			yy=random.Next(0,map.Y);
		}while (!map.EmptyPlace(xx,yy));
		x=xx;
		y=yy;
		map.place[x,y]=Map.MoverChar;
	}	
	public virtual void Move(Direction to,int step,Map map)
	{
		char MyChar=map.place[x,y];
		int xx=x,yy=y;
		map.place[x,y]=Map.NoneChar;
		for (int i=0;i<step;++i)
			{
				switch (to)
				{
					case Direction.Up: 		--xx; break;
					case Direction.Down:	++xx; break;
					case Direction.Left:	--yy; break;
					case Direction.Right:	++yy; break;
				}
				if (map.EmptyPlace(xx,yy)) {x=xx;y=yy;} else break;
			}
		map.place[x,y]=MyChar;
	}
	
	protected int hp;
	public int HP{get{return hp;}}
	public bool Alive{get{return (hp>0);}}
	
	public virtual void Beaten(int num,Map map)
	{
		hp-=num;
		if (hp<=0)
		{
			hp=0;
			map.place[x,y]=Map.NoneChar;
		}
	}
}

public class Spark : Mover
{
    public int X0, Y0;
}