using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.IO;

namespace EatBall
{
    class Menu
    {
        Game game = null;
        Player player = null;

        public Menu(Game _game)
        {
            game = _game;
            Console.Title = "Eat Ball";
            Console.BufferWidth = ((int)WindowSize.Width);
            Console.BufferHeight = ((int)WindowSize.Height);
            Console.SetWindowSize(((int)WindowSize.Width), ((int)WindowSize.Height));
            Console.SetWindowPosition(0, 0);
        }

        public void StartMenu()
        {
            Console.Clear();
            

            int cont = 0;
            string[] str = { "", "", "" };
            ConsoleKey key;

            do
            {
                for (int i = 0; i < str.Length; i++)
                {
                    if (cont == i)
                        Console.ForegroundColor = ConsoleColor.Red;
                    else
                        Console.ForegroundColor = ConsoleColor.Green;

                  PaintManu(i);                
                }

                key = Console.ReadKey(true).Key;
                Console.Clear();


                if (key == ConsoleKey.UpArrow)
                {
                    cont--;
                    if (cont < 0)
                        cont = str.Length - 1;
                }
                if (key == ConsoleKey.DownArrow)
                {
                    cont++;
                    if (cont > str.Length - 1)
                        cont = 0;
                }



            } while (key != ConsoleKey.Enter);


            GetMenu(cont);
        }


        public void GetMenu(int cont)
        {

            switch (cont)
            {
                case 0:
                    {
                        mStartGame();
                        Console.BackgroundColor = ConsoleColor.Black;
                        game.CleanBall();
                        StartMenu();
                        break;
                    }
                case 1:
                    {
                        Console.Clear();
                        player = game.BackPlayer();
                        player.PrintTable();
                        Console.ReadKey();
                        StartMenu();
                        break;
                    }
                case 2:
                    break;

            }

        }

        public void mStartGame() => game.StarGame();

        public void PaintManu(int cont)
        {
            if (cont == (int)_menu.Game)
                mGame();

             if (cont == (int)_menu.Table)
                mTable();

            if (cont == (int)_menu.Exit)
                mExit();
        }

        public void mGame()
        {
            int x = 20, y = 0;
            string[] str = { " ******       ***      *****  *****  *******" ,
                             "**     *     ** **     **  ****  **  **     " ,
                             "*           **   **    **   **   **  *******" ,
                             "*   ****   *********   **        **  **     " ,
                             " ******   **       **  **        **  *******" };

            for(int i =0; i<str.Length;i++)
            {
                Console.SetCursorPosition(x, y++);
                Console.WriteLine(str[i]);
            }
            

        }

        public void mTable()
        {
            int x = 20, y = 7;
            string[] str = { "********      ***      ******     **        *******" ,
                             "   **        ** **     **   **    **        **     " ,
                             "   **       **   **    ********   **        *******" ,
                             "   **      *********   **    **   **        **     " ,
                             "   **     **       **  ********   ********  *******" };

            for (int i = 0; i < str.Length; i++)
            {
                Console.SetCursorPosition(x, y++);
                Console.WriteLine(str[i]);
            }


        }

        public void mExit()
        {
            int x = 20, y = 14;
            string[] str = { "*******  **   **  **  ******** " ,
                             "**        ** **          **    " ,
                             "*******    ***    **     **    " ,
                             "**        ** **   **     **    " ,
                             "*******  **   **  **     **    " };

            for (int i = 0; i < str.Length; i++)
            {
                Console.SetCursorPosition(x, y++);
                Console.WriteLine(str[i]);
            }


        }

    }
}
