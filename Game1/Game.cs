using System;
using System.Windows.Forms;


public class Map //A map which is useful in Console Application
{
	private int x,y;
	public int X{get{return x;}}
	public int Y{get{return y;}}
	
	public const char 
		NoneChar='.',
		MoverChar='M',
		PlayerChar='P',
		EnemyChar='E',
		WeaponChar='W',
		GhostChar='C',
		GhoulChar='G',
		BatChar='B',
		BlockChar='#';	//To be continued...
	public char[,] place;	
	public Map()
	{
		x=10;
		y=10;
		place=new char[x,y];	
		for (int i=0;i<x;++i)
			for (int j=0;j<y;++j) place[i,j]=NoneChar;			
	}
	public Map(int x0,int y0)
	{
		x=x0;
		y=y0;
		place=new char[x,y];
		for (int i=0;i<x;++i)
			for (int j=0;j<y;++j) place[i,j]=NoneChar;		
	}	
	public bool EmptyPlace(int x0,int y0)
	{
		if (x0<0||x0>=x) return false;
		if (y0<0||y0>=y) return false;
		if (place[x0,y0]!=NoneChar) return false;
		return true;
	}
	
}

public class Game  //"Game" controls everything

{
	public Map map;

	public Player player;
	public Enemy[] enemy;
	public int cntEnemy;

    public int maxLevel = 5;
	public void NewLevel(int level) // set info,map,player,enemy
	{
		switch (level)
		{
			case 1:
			{
				InfoBox.Info("\n\n***** Welcome to Level 1 ***** \n"
							  +"The bats move randomly,\n"
							  +" and would attack you when they get nearby.\n"
							  +"Press '2' to get your Bow and clear them all!");
				map=new Map();

                Player.weaponGet[2] = true;
				player=new Player();
				player.SetPlace(map);
                player.weapon = new BareHand();               

				cntEnemy=5; 
				enemy=new Enemy[cntEnemy];								
				for (int i=0;i<cntEnemy;++i)
				{
					enemy[i]=new Bat();
					enemy[i].SetPlace(map);	
				}
				break;
			}
            case 2:
            {
                InfoBox.Info("\n\n***** Welcome to Level 2 ***** \n"
                              + "You've got a new Sword\n"
                              + " which is very powerful !\n"
                              + "Well... Just enjoy it!");
                map = new Map();

           //     Player.weaponGet[1] = true;
                player = new Player();
                player.SetPlace(map);
                player.ChangeWeapon(1);

                cntEnemy = 20;
                enemy = new Enemy[cntEnemy];
                for (int i = 0; i < cntEnemy; ++i)
                {
                    enemy[i] = new Bat();
                    enemy[i].SetPlace(map);
                }
                break;               
            }
            case 3:
            {
                InfoBox.Info("\n\n***** Welcome to Level 3 ***** \n"
                              + "Cautious!\n"
                              + "The Ghoul looks hungry..\n"
                              + "Are you brave enough to face them ?!");
                map = new Map();

                player = new Player();
                player.SetPlace(map);
                player.ChangeWeapon(1);

                cntEnemy = 10;
                enemy = new Enemy[cntEnemy];
                int i;
                for (i = 0; i < 2; ++i)
                {
                    enemy[i] = new Ghoul();
                    enemy[i].SetPlace(map);
                }
                for (; i < cntEnemy; ++i)
                {
                    enemy[i] = new Bat();
                    enemy[i].SetPlace(map);
                }
                break;
            }
            case 4:
            {
                InfoBox.Info("\n\n***** Welcome to Level 4 ***** \n"
                              + "More Ghouls..\n"
                              + "But you've got a Mace!\n"
                              + "Clear everything nearby!");
                map = new Map();

             //   Player.weaponGet[3] = true;
                player = new Player();
                player.SetPlace(map);
                player.ChangeWeapon(3);

                cntEnemy = 15;
                enemy = new Enemy[cntEnemy];
                int i;
                for (i = 0; i < 4; ++i)
                {
                    enemy[i] = new Ghoul();
                    enemy[i].SetPlace(map);
                }
                for (; i < cntEnemy; ++i)
                {
                    enemy[i] = new Bat();
                    enemy[i].SetPlace(map);
                }
                break;
            }
            case 5:
            {
                InfoBox.Info("\n\n***** Welcome to Level 5 ***** \n"
                              + "What's that?\n"
                              + "A wonderful Shield!\n"
                              + "It'd defend the bats perfectly! ");
                map = new Map();

            //    Player.weaponGet[4] = true;
                player = new Player();
                player.SetPlace(map);
                player.ChangeWeapon(4);

                cntEnemy = 20;
                enemy = new Enemy[cntEnemy];
                int i;
                for (i = 0; i < 5; ++i)
                {
                    enemy[i] = new Ghoul();
                    enemy[i].SetPlace(map);
                }
                for (; i < cntEnemy; ++i)
                {
                    enemy[i] = new Bat();
                    enemy[i].SetPlace(map);
                }
                break;
            }
		}
//		map.Show();
	}	

	public bool Playing() //Check whether it's playing or pausing.
	{
		if (!player.Alive) return false;
		
		bool flag=false;
		for (int i=0;i<cntEnemy;++i) 
			if (enemy[i].Alive) { flag=true; break; }
		return flag;	
	}		
	public bool Action(char c) //Act from the Form. Everyone acts. 
	{
        bool bo;
		bo=player.Action(c,map,enemy,cntEnemy);
		for (int i=0;i<cntEnemy;++i) enemy[i].Action(map,player);
        if (player.Attacking) 
//            if (player.weapon is Sword||player.weapon is Mace)
                player.Action(c, map, enemy, cntEnemy);
        return bo;
//		map.Show();
	}
}


