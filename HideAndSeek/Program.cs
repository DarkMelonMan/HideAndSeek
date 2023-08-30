using System;

namespace HideAndSeek
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                var gameController = new GameController();
                while (!gameController.GameOver)
                {
                    Console.WriteLine(gameController.Status);
                    Console.Write(gameController.Prompt);
                    Console.WriteLine(gameController.ParseInput(Console.ReadLine()));
                }

                Console.WriteLine($"Ты победил за {gameController.MoveNumber} ходов");
                Console.WriteLine("Нажмите P, чтобы сыграть снова, любую другую клавишу, чтобы выйти.");
                if (Console.ReadKey(true).KeyChar.ToString().ToUpper() != "P") return;
            }
        }
    }
}
