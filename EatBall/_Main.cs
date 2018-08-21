using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Drawing;


namespace EatBall
{
    enum _menu { Game, Table, Exit };
    enum WindowSize { Width = 340, Height = 115};
    enum Speed { ball_enemy= 1, ball_player = 3};
    enum Expirion { colorTOcolor = 1000, colorNOTcolor = -500, twoColor = 100 };

    class _Main
    {
        static void Main(string[] args)
        {
            Game _EatBall = new Game();
            Menu myMenu = new Menu(_EatBall);
            myMenu.StartMenu();
           
        }

    }

}
