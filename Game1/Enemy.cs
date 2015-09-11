using System;

public abstract class Enemy:Mover
{
    public Spark spark;
	public override void SetPlace(Map map)
	{
		base.SetPlace(map);
        spark = new Spark();    
        spark.Id = Game1.Form1.PictId.Isparkred;
		map.place[x,y]=Map.EnemyChar;		
	}
	public virtual void Action(Map map,Player player)
	{
        Attacking = false;
        spark.X0 = x;
        spark.Y0 = y;
	}
	public virtual void Attack(Map map,Player player,int num)
	{
        if (Math.Abs(player.X - x) <= 1 && Math.Abs(player.Y - y) <= 1)
        {
            player.Beaten(num, map);
            Attacking = true;
            spark.X = player.X;
            spark.Y = player.Y;
        }
	}
    public void Wasted(Map map)
    {
        int xx=-1, yy=-1;
        do
        {
           int t1 = random.Next(0,4);
           int t2 = random.Next(0,map.X<map.Y?map.X:map.Y);
           switch (t1)
           {
               case 0: xx = -1; yy = t2; break;
               case 1: xx = map.X; yy = t2; break;
               case 2: xx = t2; yy = -1; break;
               case 3: xx = t2; yy = map.Y; break;
           }
        }while ((x-xx)*(x-xx)+(y-yy)*(y-yy)<25);
        x = xx;
        y = yy;
    }
}

public class Bat:Enemy
{
    const int HPP = 2, MOV = 2, ATK = 1;
    public static int cntKilled = 0;
	public override void SetPlace(Map map)
	{
		base.SetPlace(map);
		hp=HPP;
        Id = Game1.Form1.PictId.Ibat;
		map.place[x,y]=Map.BatChar;
	}
    public override void Action(Map map, Player player)
    {
        if (!Alive)
        {
            Attacking = false;
            return;
        }
        base.Action(map, player);
        Direction to = (Direction)random.Next(0, 4);
        Move(to, MOV, map);
        Attack(map, player, ATK);
    }
	public override void Beaten(int num,Map map)
	{
		base.Beaten(num,map);
        if (!Alive)
        {
            ++cntKilled;
            Wasted(map);
            if (cntKilled == 1) InfoBox.Info("Good! That's your first bat.");
            else if (cntKilled % 5 == 0) InfoBox.Info(cntKilled + " bats have been killed!");
        }        
	}
}

public class Ghoul:Enemy
{
    const int HPP = 5, MOV = 1, ATK = 2;
	public override void SetPlace(Map map)
	{
		base.SetPlace(map);
		hp=HPP;
        Id = Game1.Form1.PictId.Ighoul;
		map.place[x,y]=Map.GhoulChar;		
	}
    public override void Action(Map map, Player player)
    {       
        if (!Alive)
        {
            Attacking = false;
            return;
        }
        base.Action(map, player);
        int lx = player.X - x, ly = player.Y - y;
        Direction to;

        if (Math.Abs(lx) > Math.Abs(ly))
        {
            if (lx > 0) to = Direction.Down; else to = Direction.Up;
        }
        else
        {
            if (ly > 0) to = Direction.Right; else to = Direction.Left;
        }
        Move(to, MOV, map);
        Attack(map, player, ATK);
    }
    public override void Beaten(int num, Map map)
    {
        base.Beaten(num, map);
        if (!Alive)
        {
            Wasted(map);
        }
        else InfoBox.Info("Wow! The ghoul's HP is "+hp+" now !");
    }
}