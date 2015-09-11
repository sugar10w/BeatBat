
public abstract class Weapon:Prop
{
    public Spark[] spark;
    public int cntSpark;
	public abstract void Attack(Player player,Direction to,Map map,Enemy[] enemy,int cnt);
}

public class BareHand:Weapon
{
    const int ATK = 2, SPK = 1;
    public BareHand()
    {
        Id = Game1.Form1.PictId.Iplayer;
        cntSpark = SPK;
        spark = new Spark[cntSpark];
        for (int i = 0; i < cntSpark; ++i) spark[i] = new Spark();
        for (int i = 0; i < cntSpark; ++i) spark[i].Id = Game1.Form1.PictId.Isparkblue;
    }
	public override void Attack(Player player,Direction to,Map map,Enemy[] enemy,int cnt)
	{
		int x=player.X;
		int y=player.Y;
		switch (to)
		{
			case Direction.Up:		--x;	break;
			case Direction.Down:	++x;	break;
			case Direction.Left:	--y;	break;
			case Direction.Right:	++y;	break;	
		}
        spark[0].X0 = player.X;
        spark[0].Y0 = player.Y;
        spark[0].X = x;
        spark[0].Y = y;
		for (int i=0;i<cnt;++i)
			if (enemy[i].X==x&&enemy[i].Y==y&&enemy[i].Alive)
				enemy[i].Beaten(ATK,map);
	}
}
public class Sword:Weapon
{
    const int ATK0 = 3, ATK1 = 2, SPK = 4;
    public Sword()
    {
        Id = Game1.Form1.PictId.Isword;
        cntSpark = SPK;
        spark = new Spark[cntSpark];
        for (int i = 0; i < cntSpark; ++i) spark[i] = new Spark();
        for (int i = 0; i < cntSpark; ++i) spark[i].Id = Game1.Form1.PictId.Isparkblue;
    }
	public override void Attack(Player player,Direction to,Map map,Enemy[] enemy,int cnt)
	{
		{
		int x=player.X;
		int y=player.Y;
		switch (to)
		{
			case Direction.Up:		--x;	break;
			case Direction.Down:	++x;	break;
			case Direction.Left:	--y;	break;
			case Direction.Right:	++y;	break;			
		}
        spark[0].X0 = player.X;
        spark[0].Y0 = player.Y;
        spark[0].X = x;
        spark[0].Y = y;
		for (int i=0;i<cnt;++i)
			if (enemy[i].X==x&&enemy[i].Y==y&&enemy[i].Alive)
				enemy[i].Beaten(ATK0,map);
		}
		{
		int x=player.X;
		int y=player.Y;
		switch (to)
		{
			case Direction.Up:		x-=2;	break;
			case Direction.Down:	x+=2;	break;
			case Direction.Left:	y-=2;	break;
			case Direction.Right:	y+=2;	break;			
		}
        spark[1].X0 = player.X;
        spark[1].Y0 = player.Y;
        spark[1].X = x;
        spark[1].Y = y;
		for (int i=0;i<cnt;++i)
			if (enemy[i].X==x&&enemy[i].Y==y&&enemy[i].Alive)
				enemy[i].Beaten(ATK1,map);
		}
        {
            int x = player.X;
            int y = player.Y;
            switch (to)
            {
                case Direction.Up: --x; ++y; break;
                case Direction.Down: ++x; --y; break;
                case Direction.Left: --x; --y; break;
                case Direction.Right: ++x; ++y; break;
            }
            spark[2].X0 = player.X;
            spark[2].Y0 = player.Y;
            spark[2].X = x;
            spark[2].Y = y;
            for (int i = 0; i < cnt; ++i)
                if (enemy[i].X == x && enemy[i].Y == y && enemy[i].Alive)
                    enemy[i].Beaten(ATK1, map);
        }
        {
            int x = player.X;
            int y = player.Y;
            switch (to)
            {
                case Direction.Up: --x; --y; break;
                case Direction.Down: ++x; ++y; break;
                case Direction.Left: ++x; --y; break;
                case Direction.Right: --x; ++y; break;
            }
            spark[3].X0 = player.X;
            spark[3].Y0 = player.Y;
            spark[3].X = x;
            spark[3].Y = y;
            for (int i = 0; i < cnt; ++i)
                if (enemy[i].X == x && enemy[i].Y == y && enemy[i].Alive)
                    enemy[i].Beaten(ATK1, map);
        }
	}	
}
public class Bow : Weapon
{
    const int ATK = 2, SPK = 1;
    public Bow()
    {
        Id = Game1.Form1.PictId.Ibow;
        cntSpark = SPK;
        spark = new Spark[cntSpark];
        for (int i = 0; i < cntSpark; ++i) spark[i] = new Spark();
        for (int i = 0; i < cntSpark; ++i) spark[i].Id = Game1.Form1.PictId.Isparkblue;
    }
	public override void Attack(Player player,Direction to,Map map,Enemy[] enemy,int cnt)
	{
		int x=player.X;
		int y=player.Y;
        int dx = 0, dy = 0;
        switch (to)
        {
            case Direction.Up:		dx=-1;	break;
			case Direction.Down:	dx=1;	break;
			case Direction.Left:	dy=-1;	break;
			case Direction.Right:	dy=1;	break;	
        }
        while (x >= 0 && x < map.X && y >= 0 && y < map.Y)
        {
            x += dx;
            y += dy;
            for (int i = 0; i < cnt; ++i)
                if (enemy[i].X == x && enemy[i].Y == y && enemy[i].Alive)
                {
                    enemy[i].Beaten(ATK, map);
                    spark[0].X0 = player.X;
                    spark[0].Y0 = player.Y;
                    spark[0].X = x;
                    spark[0].Y = y;
                    return;
                }
        }
        spark[0].X0 = player.X;
        spark[0].Y0 = player.Y;
        spark[0].X = x;
        spark[0].Y = y;
	}
}
public class Mace : Weapon
{
    const int ATK = 5, SPK = 8;
    public Mace()
    {
        Id = Game1.Form1.PictId.Imace;
        cntSpark = SPK;
        spark = new Spark[cntSpark];
        for (int i = 0; i < cntSpark; ++i) spark[i] = new Spark();
        for (int i = 0; i < cntSpark; ++i) spark[i].Id = Game1.Form1.PictId.Isparkblue;
    }
	public override void Attack(Player player,Direction to,Map map,Enemy[] enemy,int cnt)
	{
		{
		    int x=player.X;
	    	int y=player.Y;
            --x; --y;
            spark[0].X0 = player.X;
            spark[0].Y0 = player.Y;
            spark[0].X = x;
            spark[0].Y = y;
    		for (int i=0;i<cnt;++i)
			    if (enemy[i].X==x&&enemy[i].Y==y&&enemy[i].Alive)
	    			enemy[i].Beaten(ATK,map);
		}
        {
            int x = player.X;
            int y = player.Y;
            --x; 
            spark[1].X0 = player.X;
            spark[1].Y0 = player.Y;
            spark[1].X = x;
            spark[1].Y = y;
            for (int i = 0; i < cnt; ++i)
                if (enemy[i].X == x && enemy[i].Y == y && enemy[i].Alive)
                    enemy[i].Beaten(ATK, map);
        }
        {
            int x = player.X;
            int y = player.Y;
            --x; ++y;
            spark[2].X0 = player.X;
            spark[2].Y0 = player.Y;
            spark[2].X = x;
            spark[2].Y = y;
            for (int i = 0; i < cnt; ++i)
                if (enemy[i].X == x && enemy[i].Y == y && enemy[i].Alive)
                    enemy[i].Beaten(ATK, map);
        }
        {
            int x = player.X;
            int y = player.Y;
                 --y;
            spark[3].X0 = player.X;
            spark[3].Y0 = player.Y;
            spark[3].X = x;
            spark[3].Y = y;
            for (int i = 0; i < cnt; ++i)
                if (enemy[i].X == x && enemy[i].Y == y && enemy[i].Alive)
                    enemy[i].Beaten(ATK, map);
        }
        {
            int x = player.X;
            int y = player.Y;
            ++x; --y;
            spark[4].X0 = player.X;
            spark[4].Y0 = player.Y;
            spark[4].X = x;
            spark[4].Y = y;
            for (int i = 0; i < cnt; ++i)
                if (enemy[i].X == x && enemy[i].Y == y && enemy[i].Alive)
                    enemy[i].Beaten(ATK, map);
        }
        {
            int x = player.X;
            int y = player.Y;
                 ++y;
            spark[5].X0 = player.X;
            spark[5].Y0 = player.Y;
            spark[5].X = x;
            spark[5].Y = y;
            for (int i = 0; i < cnt; ++i)
                if (enemy[i].X == x && enemy[i].Y == y && enemy[i].Alive)
                    enemy[i].Beaten(ATK, map);
        }
        {
            int x = player.X;
            int y = player.Y;
            ++x; ++y;
            spark[6].X0 = player.X;
            spark[6].Y0 = player.Y;
            spark[6].X = x;
            spark[6].Y = y;
            for (int i = 0; i < cnt; ++i)
                if (enemy[i].X == x && enemy[i].Y == y && enemy[i].Alive)
                    enemy[i].Beaten(ATK, map);
        }
        {
            int x = player.X;
            int y = player.Y;
            ++x;     
            spark[7].X0 = player.X;
            spark[7].Y0 = player.Y;
            spark[7].X = x;
            spark[7].Y = y;
            for (int i = 0; i < cnt; ++i)
                if (enemy[i].X == x && enemy[i].Y == y && enemy[i].Alive)
                    enemy[i].Beaten(ATK, map);
        }
	}	
}
public class Shield : Weapon
{
    const int ATK = 1, SPK = 1;
    public Shield()
    {
        Id = Game1.Form1.PictId.Ishield;
        cntSpark = SPK;
        spark = new Spark[cntSpark];
        for (int i = 0; i < cntSpark; ++i) spark[i] = new Spark();
        for (int i = 0; i < cntSpark; ++i) spark[i].Id = Game1.Form1.PictId.Isparkblue;
    }
    public override void Attack(Player player, Direction to, Map map, Enemy[] enemy, int cnt)
    {
        int x = player.X;
        int y = player.Y;
        switch (to)
        {
            case Direction.Up: --x; break;
            case Direction.Down: ++x; break;
            case Direction.Left: --y; break;
            case Direction.Right: ++y; break;
        }
        spark[0].X0 = player.X;
        spark[0].Y0 = player.Y;
        spark[0].X = x;
        spark[0].Y = y;
        for (int i = 0; i < cnt; ++i)
            if (enemy[i].X == x && enemy[i].Y == y && enemy[i].Alive)
                enemy[i].Beaten(ATK, map);
    }
}