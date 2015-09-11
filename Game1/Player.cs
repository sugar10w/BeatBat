using System;
using System.Windows.Forms;

public class Player:Mover
{
    public int HPP = 10, DFC = 0, MOV = 1;

	public Weapon weapon;
    private bool bo=false;
    public static bool[] weaponGet={false,false,false,false,false,false};

	public override void SetPlace(Map map)
	{
		base.SetPlace(map);
		hp=HPP;
        Id = Game1.Form1.PictId.Iplayer;
		map.place[x,y]=Map.PlayerChar;
	}
    public override void Move(Direction to, int step, Map map)
    {
        base.Move(to, step, map);
        Attacking = false;
    }

    public void Attack(Direction to, Map map, Enemy[] enemy, int cnt)
    {
        weapon.Attack(this,to,map,enemy,cnt);
        Attacking = true;
    }
    public void ChangeWeapon(int id)
    {
        switch (id)
        {
            case 1: 
                if (!(weapon is Sword)) 
                {
                    weapon = new Sword(); bo = true;
                    DFC = 0; MOV = 1;
                    InfoBox.Info("You're equiped with a Sword now!");
                } 
                break;
            case 2: 
                if (!(weapon is Bow)) 
                { 
                    weapon = new Bow(); bo = true;
                    DFC = 0; MOV = 1;
                    InfoBox.Info("You're equiped with a Bow now!");
                } 
                break;
            case 3: 
                if (!(weapon is Mace)) 
                {
                    weapon = new Mace(); bo = true;
                    DFC = 0; MOV = 1;
                    InfoBox.Info("You're equiped with a Mace now!");
                } 
                break;
            case 4:
                if (!(weapon is Shield))
                {
                    weapon = new Shield(); bo = true;
                    DFC = 1; MOV = 1;
                    InfoBox.Info("You're equiped with a Shield now!");
                }
                break;
        }
        
    }

	public bool Action(char c,Map map,Enemy[] enemy,int cnt)
	{
        c = char.ToLower(c);
        bo = false;
        Direction to = (Direction)random.Next(0, 4);
		switch (c)
		{
            case '1': 
                if (weaponGet[1]) ChangeWeapon(1);
                else InfoBox.Info("You haven't got the Sword!"); 
                Attack(to,map,enemy,cnt);
                break;
            case '2': 
                if (weaponGet[2]) ChangeWeapon(2); 
                else InfoBox.Info("You haven't got the Bow!"); 
                Attack(to,map,enemy,cnt);
                break;
            case '3': 
                if (weaponGet[3]) ChangeWeapon(3); 
                else InfoBox.Info("You haven't got the Mace!"); 
                Attack(to,map,enemy,cnt);
                break;
            case '4':
                if (weaponGet[4]) ChangeWeapon(4);
                else InfoBox.Info("You haven't got the Shield!");
                Attack(to, map, enemy, cnt);
                break;
			case 'a':   Move(Direction.Left,MOV,map);     break;
			case 's':   Move(Direction.Down,MOV,map);     break;
			case 'd':   Move(Direction.Right,MOV,map);    break;
			case 'w':   Move(Direction.Up,MOV,map);		break;		
			case 'j':   Attack(Direction.Left,map,enemy,cnt);   break;
			case 'k':   Attack(Direction.Down,map,enemy,cnt);   break;
			case 'l':   Attack(Direction.Right,map,enemy,cnt);  break;
			case 'i':   Attack(Direction.Up,map,enemy,cnt);     break;
            default:    Move(Direction.None, 0, map);   break;
		}
        return bo;
	}

	public override void Beaten(int num,Map map)
	{
        int nn = num - DFC;
        if (nn > 0)
        {
            base.Beaten(nn, map);
            InfoBox.Info("Ooops! You're being attacked! Your HP is " + hp + " now.");
        }
	}
	
}