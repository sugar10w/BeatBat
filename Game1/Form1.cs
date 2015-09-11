using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Game1
{
    public partial class Form1 : Form
    {
        private Game game;
        private int level = 1;
        private int cntTimer = 0;
        const int cntLimit = 16;
        private bool UserAct = false;

        public Form1()
        {
            InitializeComponent();           
        }

        public enum PictId { Iplayer, Ibat, Ighoul, Isword, Ibow, Imace, Ishield,Isparkred, Isparkblue };
        private void PictAndId(Pict pict,PictId id) //Match PictId and the picture resource
        {
            if (pict == null) return;
            switch (id)
            {
                case PictId.Iplayer:
                    pict.Image = Properties.Resources.player;
                    break;
                case PictId.Ibat:
                    pict.Image = Properties.Resources.bat;
                    break;
                case PictId.Ighoul:
                    pict.Image = Properties.Resources.ghoul;
                    break;
                case PictId.Isword: //1
                    pict.Image = Properties.Resources.sword;
                    break;               
                case PictId.Ibow: //2
                    pict.Image = Properties.Resources.bow;
                    break;
                case PictId.Imace:  //3
                    pict.Image = Properties.Resources.mace;
                    break;
                case PictId.Ishield:  //4
                    pict.Image = Properties.Resources.shield;
                    break;
                case PictId.Isparkblue:
                    pict.Image = Properties.Resources.sparkblue;
                    pict.Front = true;
                    pict.Visible = false;
                    break;
                case PictId.Isparkred:
                    pict.Image = Properties.Resources.sparkred;
                    pict.Front = true;
                    pict.Visible = false;
                    break;
            }
        }
        public Pict CreatePict(Mover m) //Give mover a pict
        {
            Pict pict;
            pict = new Pict();
            PictAndId(pict,m.Id);
            pict.Height = pict.Image.Size.Height; 
            pict.Width = pict.Image.Size.Width;            
            return pict;
        }

        private void NewGame() //Prepare for everything. Start a game 
        {
            InfoBox.Text = "";
            game = new Game();
            game.NewLevel(level);
            game.player.pict = CreatePict(game.player);
            for (int i = 0; i < game.player.weapon.cntSpark; ++i)
                game.player.weapon.spark[i].pict = CreatePict(game.player.weapon.spark[i]);
            for (int i = 0; i < game.cntEnemy; ++i)
            {
                game.enemy[i].pict = CreatePict(game.enemy[i]);
                game.enemy[i].spark.pict = CreatePict(game.enemy[i].spark);
            }
            UpdateAll();
            timer2.Enabled = true;
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            NewGame();            
        }

        private void UpdateFinal(Mover m)//Work with timer1. get there and stop moving  
        {
            Pict pict = m.pict;
            int TTop = pictureBox1.Height / game.map.X * m.X;
            int TLeft = pictureBox1.Width / game.map.Y * m.Y;
            if (m is Spark)
            {
                TTop += 20;
                TLeft += 20;
            }
            if (Math.Abs(pict.Top - TTop) <= 2 && Math.Abs(pict.Left - TLeft) <= 2) return;
            pict.Top = TTop;
            pict.Left = TLeft;
        }
        private bool HaveUpdateFinal(Mover m) //if has been there 
        {
            Pict pict = m.pict;
            int TTop = pictureBox1.Height / game.map.X * m.X;
            int TLeft = pictureBox1.Width / game.map.Y * m.Y;
            if (m is Spark)
            {
                TTop += 20;
                TLeft += 20;
            }
            return (Math.Abs(pict.Top - TTop) <= 2 && Math.Abs(pict.Left - TLeft) <= 2);
        }
        private void Update(Mover m) //Work with timer1. Move the picts 
        {
            double k=0.66666;
            if (m is Spark) k = 0.5;
            Pict pict = m.pict;
            if (pict == null) return;
            int Ttop = pictureBox1.Height / game.map.X * m.X;
            int Tleft = pictureBox1.Width / game.map.Y * m.Y;
            if (m is Spark)
            {
                Ttop += 20;
                Tleft += 20;
            }
            pict.Top = (int)(k * pict.Top + (1 - k) * Ttop);
            pict.Left = (int)(k * pict.Left + (1 - k) * Tleft);
        }
        private void timer1_Tick(object sender, EventArgs e) //Moving the pictures 
        {
            Update(game.player);
            for (int i = 0; i < game.cntEnemy; ++i)
            {
                Update(game.enemy[i]);
            }
            for (int i = 0; i < game.player.weapon.cntSpark; ++i)
            {
                Update(game.player.weapon.spark[i]);
            }
            for (int i = 0; i < game.cntEnemy; ++i) if (game.enemy[i].spark.pict.Visible)
            {
                Update(game.enemy[i].spark);
            }
            for (int i = 0; i < game.cntEnemy; ++i)
                if (!game.enemy[i].Alive)
                    if (HaveUpdateFinal(game.enemy[i]))
                        game.enemy[i].pict.Visible = false;
            for (int i = 0; i < game.cntEnemy; ++i)
                if (game.enemy[i].spark.pict != null)
                    if (HaveUpdateFinal(game.enemy[i].spark))
                        game.enemy[i].spark.pict.Visible = false;
            for (int i = 0; i < game.player.weapon.cntSpark; ++i)
                if (game.player.weapon.spark[i].pict != null)
                    if (HaveUpdateFinal(game.player.weapon.spark[i]))
                        game.player.weapon.spark[i].pict.Visible = false;

            ++cntTimer;

            if (cntTimer == cntLimit)
            {
                UpdateFinal(game.player);
                for (int i = 0; i < game.cntEnemy; ++i)
                {
                    if (game.enemy[i].Alive) UpdateFinal(game.enemy[i]);
                    else if (game.enemy[i].pict != null) game.enemy[i].pict.Visible = false;
                }
                for (int i = 0; i < game.cntEnemy; ++i)
                    if (game.enemy[i].spark.pict != null)
                        game.enemy[i].spark.pict.Visible = false;
                for (int i = 0; i < game.player.weapon.cntSpark; ++i)
                    if (game.player.weapon.spark[i].pict != null)
                        game.player.weapon.spark[i].pict.Visible = false;
                timer1.Enabled = false;
            }
        }
     
        private void UpdateAll() //Renew Controls and Enable timer1. Check the end of a game
        {
            pictureBox1.Width = this.Width - 20;
            pictureBox1.Height = this.Height - 120;
            label1.Text = InfoBox.Text;

            WeaponPict.Left = this.Width - 78;
            Pict wp = new Pict();
            PictAndId(wp, game.player.weapon.Id);
            WeaponPict.Image = wp.Image;

            cntTimer = 0;
            timer1.Enabled = true;

            if (!game.Playing())
            {
                timer2.Enabled = false;
                if (!game.player.Alive)
                {
                    Bat.cntKilled = 0;
                    MessageBox.Show("Ooops, you failed!", "Game Over");
                }
                else
                {
                    MessageBox.Show("Congratulations, you win!", "Game Over");
                    switch (level)
                    {
                        case 1: Player.weaponGet[1] = true; break;
                        case 3: Player.weaponGet[3] = true; break;
                        case 4: Player.weaponGet[4] = true; break;
                    }
                    ++level;
                    if (level > game.maxLevel)
                    {
                        Bat.cntKilled = 0;
                        MessageBox.Show("You've passed all the levels. Well done!", "All Pass!");
                        level = 1;
                    }
                }
                NewGame();
            }
        }

        private void ShowSpark() //attack makes sparks 
        {
            Spark m;
            Pict pict;
            if (game.player.Attacking)
               for (int i = 0; i < game.player.weapon.cntSpark; ++i)
                {
                    m = game.player.weapon.spark[i];
                    pict = m.pict;
                    if (pict == null) continue;
                    pict.Top = pictureBox1.Height / game.map.X * m.X0 + 20;
                    pict.Left = pictureBox1.Width / game.map.Y * m.Y0 + 20;
                    pict.Visible = true;
                    pict.Front = true;
                }
            for (int i = 0; i < game.cntEnemy; ++i) if (game.enemy[i].Attacking)                
            {
                m = game.enemy[i].spark;
                pict = m.pict;
                if (pict == null) continue;
                pict.Top = pictureBox1.Height / game.map.X * m.X0 + 20;
                pict.Left = pictureBox1.Width / game.map.Y * m.Y0 + 20;
                pict.Visible = true;
                pict.Front = true;
            }
        }

        private void Form1_KeyPress(object sender, KeyPressEventArgs e) //Interact with the user via the keyboard 
        {
            UserAct = true;
            char c = e.KeyChar;
            switch (c)
            {
                case '!':
                    level = 1;
                    NewGame();
                    return;
                case '@':
                    level = 2;
                    NewGame();
                    return;
                case '#':
                    level = 3;
                    NewGame();
                    return;
                case '$':
                    level = 4;
                    NewGame();
                    return;
                case '%':
                    level = 5;
                    NewGame();
                    return;
            }

            timer2.Start();

            if (game.Playing())
            {
                if (game.Action(c))
                {
                    for (int i = 0; i < game.player.weapon.cntSpark; ++i)
                        game.player.weapon.spark[i].pict = CreatePict(game.player.weapon.spark[i]);
                }
                ShowSpark();
                UpdateAll();
            }
        }
        private void Form1_ResizeEnd(object sender, EventArgs e) //Adapt to the new size instantly 
        {
            UpdateAll();
        }
        private void timer2_Tick(object sender, EventArgs e) //Enemy acts automatically 
        {
            if (!UserAct)
            {
                game.Action(' ');
                ShowSpark();
                UpdateAll();
            }
            UserAct = false;
        }

        private void timer3_Tick(object sender, EventArgs e)
        {
            pictureBox1.Invalidate();
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            Pict pict;

            g.FillRectangle(SystemBrushes.Control, 0, 0, pictureBox1.Width, pictureBox1.Height);

            pict = game.player.pict;
            if (game.player.Alive) g.DrawImage(pict.Image, pict.Left, pict.Top);

            for (int i = 0; i < game.cntEnemy; ++i)
            {
                pict = game.enemy[i].pict;
                if (pict.Visible) g.DrawImage(pict.Image, pict.Left, pict.Top);
                pict = game.enemy[i].spark.pict;
                if (pict.Visible) g.DrawImage(pict.Image, pict.Left, pict.Top);
            }

            for (int i = 0; i < game.player.weapon.cntSpark; ++i)
            {
                pict = game.player.weapon.spark[i].pict;
                if (pict.Visible) g.DrawImage(pict.Image, pict.Left, pict.Top);
            }
        }

        private void Form1_SizeChanged(object sender, EventArgs e)
        {
            pictureBox1.Width = this.Width - 20;
            pictureBox1.Height = this.Height - 120;
        }

    }
}
