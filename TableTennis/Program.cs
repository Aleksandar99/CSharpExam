using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TableTennis
{
    class Program
    {
        static int padSizeFirstPlayer = 5;
        static int padSizeSecondPlayer = 5;
        static int ballPositionX = 0;
        static int ballPositionY = 0;
        static bool ballDirectionUp = true;
        static bool ballDirectionRight = false;
        static int firstPlayerPosition = 0;
        static int secondPlayerPosition = 0;
        static int resultFirstPlayer = 0;
        static int resultSecondPlayer = 0;
        static Random randomGenerator = new Random();

        static void ScrollBarsRemover()
        {
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.Green;
            Console.BufferHeight = Console.WindowHeight;
            Console.BufferWidth = Console.WindowWidth;
        }

        static void DrawFirstPlayer()
        {
            for (int y = firstPlayerPosition; y < firstPlayerPosition + padSizeFirstPlayer; y++)
            {
                PrintAtPosition(0, y, '|');
                PrintAtPosition(1, y, '|');
            }
        }

        static void PrintAtPosition(int x, int y, char symbol)
        {
            Console.SetCursorPosition(x, y);
            Console.Write(symbol);

        }

        static void DrawSecondPlayer()
        {
            for (int y = secondPlayerPosition; y < secondPlayerPosition + padSizeSecondPlayer; y++)
            {
                PrintAtPosition(Console.WindowWidth - 1, y, '|');
                PrintAtPosition(Console.WindowWidth - 2, y, '|');
            }
        }

        static void SetInitialPositions()
        {
            firstPlayerPosition = Console.WindowHeight / 2 - padSizeFirstPlayer / 2;
            secondPlayerPosition = Console.WindowHeight / 2 - padSizeSecondPlayer / 2;
            SetBallAtTheMiddleOfTheGameField();
        }

        static void SetBallAtTheMiddleOfTheGameField()
        {
            ballPositionX = Console.WindowWidth / 2;
            ballPositionY = Console.WindowHeight / 2;
        }

        static void DrawBall()
        {
            PrintAtPosition(ballPositionX, ballPositionY, 'O');
        }

        static void PrintResult()
        {
            Console.SetCursorPosition(Console.WindowWidth / 2 - 1, 0);
            Console.Write("{0}-{1}", resultFirstPlayer, resultSecondPlayer);
        }

        static void MoveFirstPlayerDown()
        {
            if (firstPlayerPosition < Console.WindowHeight - padSizeFirstPlayer)
            {
                firstPlayerPosition++;
            }
        }

        static void MoveFirstPlayerUp()
        {
            if (firstPlayerPosition > 0)
            {
                firstPlayerPosition--;
            }
        }

        static void MoveSecondPlayerDown()
        {
            if (secondPlayerPosition < Console.WindowHeight - padSizeSecondPlayer)
            {
                secondPlayerPosition++;
            }
        }

        static void MoveSecondPlayerUp()
        {
            if (secondPlayerPosition > 0)
            {
                secondPlayerPosition--;
            }
        }

        static void SecondPlayerAIMove()
        {
            int randomNumber = randomGenerator.Next(1, 101);

            if (randomNumber <= 70)
            {
                if (ballDirectionUp == true)
                {
                    MoveSecondPlayerUp();
                }
                else
                {
                    MoveSecondPlayerDown();
                }
            }
        }

        private static void MoveBall()
        {
            if (ballPositionY == 0)
            {
                ballDirectionUp = false;
            }
            if (ballPositionY == Console.WindowHeight - 1)
            {
                ballDirectionUp = true;
            }
            if (ballPositionX == Console.WindowWidth - 1)
            {
                SetBallAtTheMiddleOfTheGameField();
                ballDirectionRight = false;
                ballDirectionUp = true;
                resultFirstPlayer++;
                Console.SetCursorPosition(Console.WindowWidth / 2, Console.WindowHeight / 2);
                Console.WriteLine("First player wins!");
                Console.ReadKey();
            }
            if (ballPositionX == 0)
            {
                SetBallAtTheMiddleOfTheGameField();
                ballDirectionRight = true;
                ballDirectionUp = true;
                resultSecondPlayer++;
                Console.SetCursorPosition(Console.WindowWidth / 2, Console.WindowHeight / 2);
                Console.WriteLine("Second player wins!");
                Console.ReadKey();
            }

            if (ballPositionX < 3)
            {
                if (ballPositionY >= firstPlayerPosition
                    && ballPositionY < firstPlayerPosition + padSizeFirstPlayer)
                {
                    ballDirectionRight = true;
                }
            }

            if (ballPositionX >= Console.WindowWidth - 3 - 1)
            {
                if (ballPositionY >= secondPlayerPosition
                    && ballPositionY < secondPlayerPosition + padSizeSecondPlayer)
                {
                    ballDirectionRight = false;
                }
            }

            if (ballDirectionUp)
            {
                ballPositionY--;
            }
            else
            {
                ballPositionY++;
            }


            if (ballDirectionRight)
            {
                ballPositionX++;
            }
            else
            {
                ballPositionX--;
            }
        }

        static void Main(string[] args)
        {
            ScrollBarsRemover();
            SetInitialPositions();
            while (true)
            {
                if (Console.KeyAvailable)
                {
                    ConsoleKeyInfo keyInfo = Console.ReadKey();
                    if (keyInfo.Key == ConsoleKey.UpArrow)
                    {
                        MoveFirstPlayerUp();
                    }
                    if (keyInfo.Key == ConsoleKey.DownArrow)
                    {
                        MoveFirstPlayerDown();
                    }
                }
                SecondPlayerAIMove();
                MoveBall();
                Console.Clear();
                DrawFirstPlayer();
                DrawSecondPlayer();
                DrawBall();
                PrintResult();
                Thread.Sleep(60);
            }
        }
    }
}