using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Drawing;


namespace EatBall
{

    class Game
    {
        private Ball myBall = new Ball();
        private Player myPlayer = new Player();

        private List<Ball> Enemy = new List<Ball>();
        private List<Point> RandPosForEnemy = new List<Point>();
        private ConsoleKeyInfo key = new ConsoleKeyInfo();
        private Thread myThread;
        private Timer myTimer;
        public static Random rand = new Random();
        private bool TheEndGame = new Boolean();

        public Game()
        {
            CreatePointRandom();
            
        }

        public void StarGame()
        {
            Console.Clear();
            Line();

            myPlayer.SetPosBalu(0,1, myPlayer.Bal);//виводим к-сть балів
            myBall.RandomColorBall(true);
            myBall.SetStartPos();
            myBall.PaintBall();

            myTimer = new Timer(TimerTick, new object(), 0, 5000);
            myThread = new Thread(new ThreadStart(Inertia));
            myThread.Start();

            
            do
            {
                if (TheEndGame == true)
                    break;

                key = Console.ReadKey(true);

            } while (key.Key != ConsoleKey.Escape);

            myTimer.Dispose();
            myThread.Abort();
            myThread.Join();
            TheEndGame = false;

            TheEndGameInfo();


            myPlayer.SortTable();
        }

        public void PlayerBall()
        {
            myBall.Clean();
            for (int i = 0; i < Enemy.Count; i++)
            {
                if (Rule.Rule_EatBall(myBall, Enemy[i]))
                {
                    DeterminesColor(Enemy[i]);
                    TheEndGame = BaluSuma(Enemy[i]);
                    myPlayer.SetPosBalu(0, 1, myPlayer.Bal);//виводим к-сть балів
                    
                    Enemy[i].Clean();
                    Enemy[i] = null;
                    Enemy.RemoveAt(i);
                }
            }

            switch (key.Key)
            {
                case ConsoleKey.LeftArrow:
                    {
                        myBall.MoveLeft = true;
                        myBall.MoveRight = false;
                        myBall.MoveUp = false;
                        myBall.MoveDown = false;

                        break;
                    }
                case ConsoleKey.RightArrow:
                    {
                        myBall.MoveRight = true;
                        myBall.MoveLeft = false;
                        myBall.MoveUp = false;
                        myBall.MoveDown = false;

                        break;
                    }
                case ConsoleKey.UpArrow:
                    {
                        myBall.MoveRight = false;
                        myBall.MoveLeft = false;
                        myBall.MoveUp = true;
                        myBall.MoveDown = false;

                        break;
                    }
                case ConsoleKey.DownArrow:
                    {
                        myBall.MoveRight = false;
                        myBall.MoveLeft = false;
                        myBall.MoveUp = false;
                        myBall.MoveDown = true;

                        break;
                    }

            }

            Rule.Rule_SizeGame(myBall);
            myBall.InertiaBall(Speed.ball_player);
            myBall.PaintBall();

            Console.SetCursorPosition(myBall.GetCenterPos().X - 4, myBall.GetCenterPos().Y - 1);
            Console.ForegroundColor = ConsoleColor.Black;
            Console.WriteLine("++    ++");
            Console.SetCursorPosition(myBall.GetCenterPos().X - 4, myBall.GetCenterPos().Y);
            Console.WriteLine("++    ++");
        }

        public void EnemyBall()
        {
            for (int i = 0; i < Enemy.Count; i++) //Enemy
            {
                try
                {
                    Enemy[i].Clean();
                    
                    Rule.Rule_SizeGame(Enemy[i]);
                    Rule.Rule_EnemyApproaching(myBall, Enemy[i]);

                    if (Rule.Rule_BallTouchBall(myBall, Enemy, i) == true)
                    {
                        //i++;
                    }
                    else
                    {
                        Enemy[i].InertiaBall(Speed.ball_enemy);
                        Enemy[i].PaintBall();
                    }




                }
                catch (Exception)
                {
                    continue;
                }

            }
        }

        private void TimerTick(object state)
        {
            CreateEnemy();
        }

        private void Inertia()
        {
            try
            {
                while (true)
                {
                    EnemyBall();
                    PlayerBall();
                    Thread.Sleep(100);
                }
            }
            catch(Exception ex)
            {
                Console.Clear();
                Console.WriteLine($"Game.Inertia  \nMeseg:{ex.Message}");
            }


        }

        public bool DeterminesColor(Ball enBall)
        {
            if (enBall.UpPaint == myBall.UpPaint)
            {
                myBall.UpPaint = enBall.DownPaint;
                myBall.DownPaint = enBall.DownPaint;
                return true;
            }
            if (enBall.DownPaint == myBall.DownPaint)
            {
                myBall.UpPaint = enBall.UpPaint;
                myBall.DownPaint = enBall.UpPaint;
                return true;
            }

            return false;
        }

        public bool BaluSuma(Ball enBall)
        {
            if (myBall.UpPaint == enBall.UpPaint && myBall.UpPaint == enBall.DownPaint)
                myPlayer.Bal += (int)Expirion.colorTOcolor;

            if (enBall.UpPaint != enBall.DownPaint)     
               myPlayer.Bal += (int)Expirion.twoColor;

            if ((enBall.UpPaint == enBall.DownPaint) && (myBall.UpPaint != enBall.UpPaint))
                myPlayer.Bal += (int)Expirion.colorNOTcolor;

            if (myPlayer.Bal < 0 || myPlayer.Bal > Int32.MaxValue - 1000)///////////////кінець гри////////////////////
                return true;

            return false;
        }

        public Player BackPlayer()
        {
            return myPlayer;
        }

        public void CreateEnemy()
        {


            try
            {
                int random;
                random = rand.Next(RandPosForEnemy.Count);

                if (Rule.Rule_UnitFree(myBall, Enemy, RandPosForEnemy[random]))
                {
                    Enemy.Add(new Ball());
                    Enemy[Enemy.Count - 1].RandomColorBall(false);
                    Enemy[Enemy.Count - 1].SetPosBall(RandPosForEnemy[random].X, RandPosForEnemy[random].Y);
                }
            }
            catch(Exception ex)
            {
                
                Console.Clear();
                Console.WriteLine($"Game.CreateEnemy(Rule.Rule_UnitFree(Ball, List<Ball>))  \nMeseg:{ex.Message}");
            }

        }

        public void CreatePointRandom()
        {

            RandPosForEnemy.Add(new Point(20, 17));
            RandPosForEnemy.Add(new Point(170, 17));
            RandPosForEnemy.Add(new Point(320, 17));

            RandPosForEnemy.Add(new Point(20, 50));
            RandPosForEnemy.Add(new Point(320, 50));

            RandPosForEnemy.Add(new Point(20, 100));
            RandPosForEnemy.Add(new Point(170, 100));
            RandPosForEnemy.Add(new Point(320, 100));
        }

        public void CleanBall()
        {
            for (int i = 0; i < Enemy.Count; i++)
            {
                Enemy[i] = null;
            }
            Enemy.Clear();
            myBall = null;
            myBall = new Ball();
        }

        public void TheEndGameInfo()
        {

            myPlayer.SetPosBalu(170, 1, myPlayer.Bal);
            TheEnd();
            Thread.Sleep(2000);
        }

        public List<Ball> EatOneBall(bool temp, Ball ball, int i)
        {
            if (temp == true)
            {
                Enemy.RemoveAt(i);
                ball = null;
            }

            return Enemy;
        }

        public void Line()
        {
            string str = "";
            Console.SetCursorPosition(0, 7);
            Console.BackgroundColor = ConsoleColor.White;
            Console.Write(str.PadLeft(340));
            Console.BackgroundColor = ConsoleColor.Black;
        }

        public void TheEnd()
        {
           
            Console.ForegroundColor = ConsoleColor.Cyan;
            int x = 100, y = 1;
            string[] str = { "*****        ***    ********  **  ***    **   ******      " ,
                             "**   *      ** **      **         ****   **  **     *   **" ,
                             "*****      **   **     **     **  ** **  **  *            " ,
                             "** **     *********    **     **  **  ** **  *     **   **" ,
                             "**   **  **       **   **     **  **    ***   ******      " };

            for (int i = 0; i < str.Length; i++)
            {
                Console.SetCursorPosition(x, y++);
                Console.WriteLine(str[i]);
            }


        }

    }
}
