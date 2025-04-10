using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace EatBall
{
    class Ball
    {
        private string str = string.Empty;
        private int[] xCord = { 1, 3, 5, 5, 7, 7, 5, 5, 3, 1 };
        private int[] xLength = { 4, 8, 12, 12, 16, 16, 12, 12, 8, 4 };
        private Point myPoint = new Point();
        private Point Center = new Point();
        public ConsoleColor UpPaint { get; set; }
        public ConsoleColor DownPaint { get; set; }
        public bool MoveLeft { get; set; }
        public bool MoveRight { get; set; }
        public bool MoveUp { get; set; }
        public bool MoveDown { get; set; }
        
        public Ball()
        {

        }
 
        public void SetPosBall(int x ,int y)
        {
            myPoint.X = x;
            myPoint.Y = y;
            SetCenterPos();
        }

        public void SetCenterPos()
        {
            Center.X = myPoint.X + 1;
            Center.Y = myPoint.Y - 5;
        }
        public Point GetCenterPos()
        {
            return Center;
        }

        public void SetStartPos()
        {
            myPoint.X = 80;
            myPoint.Y = 30;
            SetCenterPos();
        }

        public Point InertiaBall(Speed s)
        {
            if (MoveLeft == true)
                myPoint.X -= (int)s;

            if (MoveRight == true)
                myPoint.X += (int)s;

            if (MoveUp == true)
                myPoint.Y -= (int)s;

            if (MoveDown == true)
                myPoint.Y += (int)s;

            SetCenterPos();
            return myPoint;
        }

        public void RandomColorBall(bool Whot)
        {
            int random = Game.rand.Next(3);
            int TwoColor = Game.rand.Next(100);//% що буде двохкольоровий

            ConsoleColor[] colors = { ConsoleColor.Green, ConsoleColor.Red, ConsoleColor.Blue };

            if (Whot == true)
            {
                UpPaint = colors[random];
                DownPaint = colors[random];
            }
            else
            {
                UpPaint = colors[random];
                if (TwoColor <= 10)
                {
                    if (random == 2) 
                        DownPaint = colors[--random];
                    else
                        DownPaint = colors[++random];

                }
                else
                    DownPaint = colors[random];
                   
            }


        }

        public void PaintBall()
        {
            Point temp = new Point();
            temp = myPoint;

            Console.BackgroundColor = DownPaint;
            for (int i = 0; i < xCord.Length; i++)
            {
                Console.SetCursorPosition(temp.X - xCord[i], temp.Y--);
                Console.WriteLine(str.PadLeft(xLength[i]));
                if(xCord.Length/2-1 == i)
                    Console.BackgroundColor = UpPaint;
            }

        }

        public void Clean()
        {
            Point temp = new Point();
            temp = myPoint;

            Console.BackgroundColor = ConsoleColor.Black;
            for (int i = 0; i < xCord.Length; i++)
            {
                Console.SetCursorPosition(temp.X - xCord[i], temp.Y--);
                Console.WriteLine(str.PadLeft(xLength[i]));
            }
        }



    }

}
