using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace MovingObjectsDemo
{
    class Game
    {
        bool game;
        int sides;
        int verticals;

        //these will be used for shooting mechanics
        int temporaryLeft;
        int temporaryTop;

        //used to determine a win if this position is ever driven on or shot
        int leftNum;
        int topNum;

        string direction;
        ConsoleKeyInfo keyInfo;
        Random rand = new Random();
        List<(int, int)> positionList = new List<(int, int)>();

        public Game()
        {
            game = true;
            sides = 35;
            verticals = 10;
            direction = " ";
            leftNum = rand.Next(1, 71);
            topNum = rand.Next(1, 19);
        }

        public void Play()
        {
            PrintBorder();
            do
            {
                keyInfo = Console.ReadKey(true);

                Console.SetCursorPosition(leftNum, topNum);
                Console.Write("X");

                switch (keyInfo.Key)
                {
                    case ConsoleKey.UpArrow:
                        MoveUp();
                        CheckForWin();
                        break;

                    case ConsoleKey.DownArrow:
                        MoveDown();
                        CheckForWin();
                        break;

                    case ConsoleKey.RightArrow:
                        MoveRight();
                        CheckForWin();
                        break;

                    case ConsoleKey.LeftArrow:
                        MoveLeft();
                        CheckForWin();
                        break;

                    case ConsoleKey.Spacebar:

                        temporaryLeft = sides;
                        temporaryTop = verticals;

                        switch (direction)
                        {
                            case "left":
                                ShootLeft();
                                break;

                            case "right":
                                ShootRight();
                                break;

                            case "up":
                                ShootUp();
                                break;

                            case "down":
                                ShootDown();
                                break;
                        }
                        break;
                }
            } while (game);
        }

        public void MoveUp()
        {
            direction = "up";
            verticals--;

            //stops you from going out of bounds
            if (verticals <= 1)
            {
                verticals = 1;
                positionList.Remove((sides, verticals));
            }

            CheckForHittingOwnTrail();

            positionList.Add((sides, verticals));
            Console.SetCursorPosition(sides, verticals);
            Console.Write("^");
        }

        public void MoveDown()
        {
            direction = "down";
            verticals++;

            //stops you from going out of bounds
            if (verticals >= 19)
            {
                verticals = 19;
                positionList.Remove((sides, verticals));
            }

            CheckForHittingOwnTrail();

            positionList.Add((sides, verticals));
            Console.SetCursorPosition(sides, verticals);
            Console.Write("V");
        }

        public void MoveRight()
        {
            direction = "right";
            sides++;

            //stops you from going out of bounds
            if (sides >= 74)
            {
                sides = 74;
                positionList.Remove((sides, verticals));
            }

            CheckForHittingOwnTrail();

            positionList.Add((sides, verticals));
            Console.SetCursorPosition(sides, verticals);
            Console.Write(">");
        }

        public void MoveLeft()
        {
            direction = "left";
            sides--;

            //stops you from going out of bounds
            if (sides <= 1)
            {
                sides = 1;
                positionList.Remove((sides, verticals));
            }

            CheckForHittingOwnTrail();

            positionList.Add((sides, verticals));
            Console.SetCursorPosition(sides, verticals);
            Console.Write("<");
        }

        public void ShootUp()
        {
            for (int i = temporaryTop; i > 0; i--)
            {
                Console.SetCursorPosition(temporaryLeft, temporaryTop - 1);
                Console.Write("@");
                if ((temporaryLeft) == leftNum && temporaryTop - 1 == topNum)
                {
                    Console.SetCursorPosition(50, 10);
                    Console.WriteLine("YOU WIN!");
                    Console.ReadLine();
                    game = false;
                    break;
                }
                Thread.Sleep(20);
                temporaryTop--;
            }
            Console.Clear();
            PrintTracks();
            PrintBorder();
            Console.SetCursorPosition(sides, verticals);
        }

        public void ShootDown()
        {
            for (int i = temporaryTop; i < 20; i++)
            {
                Console.SetCursorPosition(temporaryLeft, temporaryTop + 1);
                Console.Write("@");
                if ((temporaryLeft) == leftNum && temporaryTop + 1 == topNum)
                {
                    Console.SetCursorPosition(50, 10);
                    Console.WriteLine("YOU WIN!");
                    Console.ReadLine();
                    game = false;
                    break;
                }
                Thread.Sleep(30);
                temporaryTop++;
            }
            Console.Clear();
            PrintTracks();
            PrintBorder();
            Console.SetCursorPosition(sides, verticals);
        }

        public void ShootRight()
        {
            for (int i = temporaryLeft; i < 75; i++)
            {
                Console.SetCursorPosition(temporaryLeft + 1, temporaryTop);
                Console.Write("@");
                if ((temporaryLeft + 1) == leftNum && temporaryTop == topNum)
                {
                    Console.SetCursorPosition(50, 10);
                    Console.WriteLine("YOU WIN!");
                    Console.ReadLine();
                    game = false;
                    break;
                }
                Thread.Sleep(30);
                temporaryLeft++;
            }
            Console.Clear();
            PrintTracks();
            PrintBorder();
            Console.SetCursorPosition(sides, verticals);
        }

        public void ShootLeft()
        {
            for (int i = temporaryLeft; i > 0; i--)
            {
                Console.SetCursorPosition(temporaryLeft - 1, temporaryTop);
                Console.Write("@");
                if ((temporaryLeft - 1) == leftNum && temporaryTop == topNum)
                {
                    Console.SetCursorPosition(50, 10);
                    Console.WriteLine("YOU WIN!");
                    Console.ReadLine();
                    game = false;
                    break;
                }
                Thread.Sleep(30);
                temporaryLeft--;
            }
            Console.Clear();
            PrintTracks();
            PrintBorder();
            Console.SetCursorPosition(sides, verticals);
        }

        public void CheckForHittingOwnTrail()
        {
            if (positionList.Contains((sides, verticals)))
            {
                Console.SetCursorPosition(50, 10);
                Console.WriteLine("GAME OVER!");
                game = false;
            }
        }

        public void CheckForWin()
        {
            if (sides == leftNum && verticals == topNum || ((temporaryLeft) == leftNum && temporaryTop - 1 == topNum))
            {
                Console.SetCursorPosition(50, 10);
                Console.Write("YOU WON!");
                game = false;
            }
        }

        public void PrintTracks()
        {
            foreach ((int, int) spot in positionList)
            {
                Console.SetCursorPosition(spot.Item1, spot.Item2);
                Console.Write("-");
            }
        }

        public void PrintBorder()
        {
            for (int i = 0; i <= 20; i++)
            {
                Console.SetCursorPosition(0, i);
                Console.Write("#");
            }
            for (int i = 0; i <= 20; i++)
            {
                Console.SetCursorPosition(75, i);
                Console.Write("#");
            }

            for (int i = 0; i <= 75; i++)
            {
                Console.SetCursorPosition(i, 0);
                Console.Write("#");
            }
            for (int i = 0; i <= 75; i++)
            {
                Console.SetCursorPosition(i, 20);
                Console.Write("#");
            }
        }
    }
}
