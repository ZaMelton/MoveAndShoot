using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace MovingObjectsDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            Move();
            Console.ReadLine();
        }

        public static void Move()
        {
            bool game = true;
            int sides = 35;
            int verticals = 10;
            string direction = " ";
            ConsoleKeyInfo keyInfo;
            Random rand = new Random();

            List<(int, int)> positionList = new List<(int, int)>();
            List<(int, int)> projectilePositions = new List<(int, int)>();

            int leftNum = rand.Next(1, 71);
            int topNum = rand.Next(1, 19);

            PrintBorder();

            do
            {
                keyInfo = Console.ReadKey(true);
                //Console.Clear();

                Console.SetCursorPosition(leftNum, topNum);
                Console.Write("X");

                switch (keyInfo.Key)
                {
                    case ConsoleKey.UpArrow:
                        direction = "up";
                        verticals--;

                        //stops you from going out of bounds
                        if (verticals <= 1)
                        {
                            verticals = 1;
                            positionList.Remove((sides, verticals));
                        }

                        //check for running into own trail
                        if (positionList.Contains((sides, verticals)))
                        {
                            Console.SetCursorPosition(50, 10);
                            Console.WriteLine("GAME OVER!");
                            game = false;
                        }

                        positionList.Add((sides, verticals));

                        //you win if you get to the X
                        if (sides == leftNum && verticals == topNum)
                        {
                            Console.SetCursorPosition(50, 10);
                            Console.Write("YOU WON!");
                            game = false;
                        }

                        Console.SetCursorPosition(sides, verticals);
                        Console.Write("^");
                        break;

                    case ConsoleKey.DownArrow:
                        direction = "down";
                        verticals++;

                        //stops you from going out of bounds
                        if (verticals >= 19)
                        {
                            verticals = 19;
                            positionList.Remove((sides, verticals));
                        }

                        //check for running into own trail
                        if (positionList.Contains((sides, verticals)))
                        {
                            Console.SetCursorPosition(50, 10);
                            Console.WriteLine("GAME OVER!");
                            game = false;
                        }

                        positionList.Add((sides, verticals));

                        if (sides == leftNum && verticals == topNum)
                        {
                            Console.SetCursorPosition(50, 10);
                            Console.Write("YOU WON!");
                            game = false;
                        }

                        Console.SetCursorPosition(sides, verticals);
                        Console.Write("V");
                        break;

                    case ConsoleKey.RightArrow:
                        direction = "right";
                        sides++;

                        //stops you from going out of bounds
                        if (sides >= 74)
                        {
                            sides = 74;
                            positionList.Remove((sides, verticals));
                        }

                        //check for running into own trail
                        if (positionList.Contains((sides, verticals)))
                        {
                            Console.SetCursorPosition(50, 10);
                            Console.WriteLine("GAME OVER!");
                            game = false;
                        }

                        positionList.Add((sides, verticals));

                        if (sides == leftNum && verticals == topNum)
                        {
                            Console.SetCursorPosition(50, 10);
                            Console.Write("YOU WON!");
                            game = false;
                        }

                        Console.SetCursorPosition(sides, verticals);
                        Console.Write(">");
                        break;

                    case ConsoleKey.LeftArrow:
                        direction = "left";
                        sides--;

                        //stops you from going out of bounds
                        if (sides <= 1)
                        {
                            sides = 1;
                            positionList.Remove((sides, verticals));
                        }

                        //Game over if run into own trail
                        if (positionList.Contains((sides, verticals)))
                        {
                            Console.SetCursorPosition(50, 10);
                            Console.WriteLine("GAME OVER!");
                            game = false;
                        }

                        positionList.Add((sides, verticals));

                        if (sides == leftNum && verticals == topNum)
                        {
                            Console.SetCursorPosition(50, 10);
                            Console.Write("YOU WON!");
                            game = false;
                        }

                        Console.SetCursorPosition(sides, verticals);
                        Console.Write("<");
                        break;

                    case ConsoleKey.Spacebar:

                        int temporaryLeft = sides;
                        int temporaryTop = verticals;
                        switch (direction)
                        {
                            case "left":
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
                                foreach ((int, int) spot in positionList)
                                {
                                    Console.SetCursorPosition(spot.Item1, spot.Item2);
                                    Console.Write("-");
                                }
                                PrintBorder();
                                Console.SetCursorPosition(sides, verticals);
                                break;

                            case "right":
                                //Shoots right
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
                                foreach ((int, int) spot in positionList)
                                {
                                    Console.SetCursorPosition(spot.Item1, spot.Item2);
                                    Console.Write("-");
                                }
                                PrintBorder();
                                Console.SetCursorPosition(sides, verticals);
                                break;

                            case "up":
                                //shoots up
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
                                    Thread.Sleep(30);
                                    temporaryTop--;
                                }
                                Console.Clear();
                                foreach ((int, int) spot in positionList)
                                {
                                    Console.SetCursorPosition(spot.Item1, spot.Item2);
                                    Console.Write("-");
                                }
                                PrintBorder();
                                Console.SetCursorPosition(sides, verticals);
                                break;

                            case "down":
                                //shoots down
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
                                foreach((int, int) spot in positionList)
                                {
                                    Console.SetCursorPosition(spot.Item1, spot.Item2);
                                    Console.Write("-");
                                }
                                PrintBorder();
                                Console.SetCursorPosition(sides, verticals);
                                break;
                        }
                        break;
                }

            } while (game);
        }

        public static void PrintBorder()
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
