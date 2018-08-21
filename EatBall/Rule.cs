using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace EatBall
{
    //OLL Rule Game
    static class Rule
    {
        //1.
        static public void Rule_SizeGame(Ball ball)
        {
            if (ball.GetCenterPos().X < 11)
                ball.MoveLeft = false;

            if (ball.GetCenterPos().X > 330)
                ball.MoveRight = false;

            if (ball.GetCenterPos().Y < 15)
                ball.MoveUp = false;

            if (ball.GetCenterPos().Y > 105)
                ball.MoveDown = false;

        }

        //2.
        static public bool Rule_BallTouchBall(Ball myball, List<Ball> enemy, int next)
        {
            int Line1, Line2;
            for (int i = 0; i < enemy.Count; i++) 
            {
                if (i != next)
                {
                   if (Math.Sqrt(Math.Pow(enemy[next].GetCenterPos().X - enemy[i].GetCenterPos().X, 2) + Math.Pow(enemy[next].GetCenterPos().Y - enemy[i].GetCenterPos().Y, 2)) <= 12)
                    {
                        //толкаєм шарік в якій врізались
                        Line1 = (int)Math.Sqrt(Math.Pow(enemy[next].GetCenterPos().X - myball.GetCenterPos().X, 2) + Math.Pow(enemy[next].GetCenterPos().Y - myball.GetCenterPos().Y, 2));
                        Line2 = (int)Math.Sqrt(Math.Pow(enemy[i].GetCenterPos().X - myball.GetCenterPos().X, 2) + Math.Pow(enemy[i].GetCenterPos().Y - myball.GetCenterPos().Y, 2));

                        if (Line1 > Line2)
                        {
                            enemy[i].Clean();
                            enemy[i].MoveLeft = enemy[next].MoveLeft;
                            enemy[i].MoveRight = enemy[next].MoveRight;
                            enemy[i].MoveUp = enemy[next].MoveUp;
                            enemy[i].MoveDown = enemy[next].MoveDown;

                            enemy[next].MoveLeft = false;
                            enemy[next].MoveRight = false;
                            enemy[next].MoveUp = false;
                            enemy[next].MoveDown = false;

                            Rule_SizeGame(enemy[i]);
                            enemy[i].InertiaBall(Speed.ball_enemy);
                        }
                        else
                        {
                            enemy[next].Clean();
                            enemy[next].MoveLeft = enemy[i].MoveLeft;
                            enemy[next].MoveRight = enemy[i].MoveRight;
                            enemy[next].MoveUp = enemy[i].MoveUp;
                            enemy[next].MoveDown = enemy[i].MoveDown;

                            enemy[i].MoveLeft = false;
                            enemy[i].MoveRight = false;
                            enemy[i].MoveUp = false;
                            enemy[i].MoveDown = false;

                            Rule_SizeGame(enemy[next]);
                            enemy[next].InertiaBall(Speed.ball_enemy);
                        }

                        if(Line1 == Line2)
                        {
                            enemy[i].Clean();
                            enemy[next].MoveLeft = false;
                            enemy[next].MoveRight = false;
                            enemy[next].MoveUp = false;
                            enemy[next].MoveDown = false;
                            enemy[i].MoveLeft = enemy[next].MoveLeft;
                            enemy[i].MoveRight = enemy[next].MoveRight;
                            enemy[i].MoveUp = enemy[next].MoveUp;
                            enemy[i].MoveDown = enemy[next].MoveDown;

                            Rule_SizeGame(enemy[i]);
                            enemy[i].InertiaBall(Speed.ball_enemy);
                        }
                        enemy[i].PaintBall();
                        enemy[next].PaintBall();

                        return true;
                    }

                }
     
            }

            return false;
        }

        //3.
        static public void Rule_EnemyApproaching(Ball ball, Ball enemy)
        {
            if (ball.GetCenterPos().X < enemy.GetCenterPos().X)
                enemy.MoveLeft = true;
            else
                enemy.MoveLeft = false;

            if (ball.GetCenterPos().X > enemy.GetCenterPos().X)
                enemy.MoveRight = true;
            else
                enemy.MoveRight = false;

            if (ball.GetCenterPos().Y > enemy.GetCenterPos().Y)
                enemy.MoveDown = true;
            else
                enemy.MoveDown = false;

            if (ball.GetCenterPos().Y < enemy.GetCenterPos().Y)
                enemy.MoveUp = true;
            else
                enemy.MoveUp = false;

        }

        //4.
        static public bool Rule_EatBall(Ball ball, Ball enemy)
        {
            if ((ball.GetCenterPos().X - enemy.GetCenterPos().X) <= 3 && (ball.GetCenterPos().X - enemy.GetCenterPos().X) >= -3)
            {
                if ((ball.GetCenterPos().Y - enemy.GetCenterPos().Y) <= 3 && (ball.GetCenterPos().Y - enemy.GetCenterPos().Y) >= -3)
                    return true;
                else
                    return false;
            } 
           else
               return false;

        }

        //5.
        static public bool Rule_UnitFree(Ball ball, List<Ball> enemy, Point randPoint)
        {
            //Player chak
            if ((randPoint.X - ball.GetCenterPos().X) <= 8 && (randPoint.X - ball.GetCenterPos().X) >= -8)
                if ((randPoint.Y - ball.GetCenterPos().Y) <= 10 && (randPoint.Y - ball.GetCenterPos().Y) >= -10)
                    return false;

            //Enemyz chak
            for (int i = 0; i < enemy.Count; i++)
            {
                if ((randPoint.X - enemy[i].GetCenterPos().X) <= 8 && (randPoint.X - enemy[i].GetCenterPos().X) >= -8)
                    if ((randPoint.Y - enemy[i].GetCenterPos().Y) <= 10 && (randPoint.Y - enemy[i].GetCenterPos().Y) >= -10)
                    {
                        return false;
                    }
                            
            }


            return true;
        }




    }
}
