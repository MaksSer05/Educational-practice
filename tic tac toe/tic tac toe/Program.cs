using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tic_tac_toe
{
    internal class Program
    {
        //Основной блок с кодом игры
        static void Main()
        {
            //Создание двумерного массива
            string[,] gamePlace = new string[3, 3] { { " ", " ", " " }, { " ", " ", " " }, { " ", " ", " " } };

            while (true)
            {
                Console.Clear();
                Console.WriteLine("Крестики Нолики");
                Console.Write("Выберите режим игры(1-Игра с человеком, 2-Игра с компьютером): ");
                string line = Console.ReadLine();
                int result;
                int.TryParse(line, out result);

                // Кейсы, открывающиеся в зависимости что выбрал игрок
                switch (result)
                {
                    case 1:
                        gamePlace = new string[3, 3] { { " ", " ", " " }, { " ", " ", " " }, { " ", " ", " " } };
                        GameOfPlayer(gamePlace);
                        break;
                    case 2:
                        gamePlace = new string[3, 3] { { " ", " ", " " }, { " ", " ", " " }, { " ", " ", " " } };
                        GameOfBot(gamePlace);
                        break;

                }
            }
        }

        //Игра с компьютером
        private static void GameOfBot(string[,] gamePlace)
        {
            bool isGameOver = false;
            bool isBot = false;
            while (!isGameOver)
            {

                int a = 0, b = 0;
                string line;

                Console.Clear();
                PrintPlace(gamePlace);
                //Вывод результата
                var result = Checkwinner(gamePlace);
                if (result.Item1 && result.Item2 != " ")
                {
                    Console.WriteLine($"Игра окончена. Победил: {result.Item2}");
                    Console.ReadLine();
                    isGameOver = true;
                    continue;
                }
                //Ввод данных и проверка их
                if (!isBot)
                {
                    Console.Write("Введите координаты ячейки через запятую (Пример:0,1): ");
                    line = Console.ReadLine();

                    bool parseA = false;

                    if (line != "")
                    {
                        parseA = char.IsNumber(line[0]);
                    }

                    if (parseA == false)
                    {
                        Console.WriteLine("Первой ячейки не существует, введите существующее значение");
                        Console.ReadLine();
                        continue;
                    }
                    else if (a < 0 || a > 2)
                    {
                        Console.WriteLine("Первой ячейки не существует, введите существующее значение");
                        Console.ReadLine();
                        continue;
                    }
                    else
                    {
                        a = Convert.ToInt16(line[0].ToString());
                    }

                    bool isNext = false;

                    for (int i = 1; i < line.Length; i++)
                    {

                        if (line[i].ToString() == ",")
                        {
                            isNext = true;
                        }
                        if (isNext && line[i].ToString() != " ")
                        {
                            bool parseB = char.IsNumber(line[i]);
                            if (parseB)
                            {
                                b = Convert.ToInt16(line[i].ToString());
                                break;
                            }

                        }
                    }

                    if (b < 0 || b > 2)
                    {
                        Console.WriteLine("Второй ячейки не существует, введите существующее значение");
                        Console.ReadLine();
                        continue;
                    }
                }

                if (isBot)
                    line = "O";
                else
                    line = "X";


                if (gamePlace[a, b] == " " && line != " " && isBot == false)
                {
                    gamePlace[a, b] = line;
                    isBot = true;
                }
                else if (isBot)
                {
                    while (true)
                    {
                        int x, y;
                        x = BotStep();
                        y = BotStep();

                        if (gamePlace[x, y] == " ")
                        {
                            gamePlace[x, y] = line;
                            isBot = false;
                            break;
                        }
                    }
                }
                else
                {
                    Console.WriteLine("Это поле занято или вы ничего не ввели");
                    Console.ReadLine();
                    continue;
                }
            }
            Console.ReadLine();
        }
        //Генерация случаного числа для хода компьютера
        private static int BotStep()
        {
            Random bot = new Random();
            return bot.Next(0, 3);
        }
        //Метод проверки побеждающей комбинации
        private static (bool, string) Checkwinner(string[,] place)
        {
            //по строке
            if ((place[0, 0] == place[0, 1]) && (place[0, 1] == place[0, 2]) && place[0, 0] != " ")
                return (true, place[0, 0]);

            if ((place[1, 0] == place[1, 1]) && (place[1, 1] == place[1, 2]) && place[1, 0] != " ")
                return (true, place[1, 0]);


            if ((place[2, 0] == place[2, 1]) && (place[2, 1] == place[2, 2]) && place[2, 0] != " ")
                return (true, place[2, 0]);

            // по столбцу
            if ((place[0, 0] == place[1, 0]) && (place[1, 0] == place[2, 0]) && place[0, 0] != " ")
                return (true, place[0, 0]);


            if ((place[0, 1] == place[1, 1]) && (place[1, 1] == place[2, 1]) && place[0, 1] != " ")
                return (true, place[0, 1]);

            if ((place[0, 2] == place[1, 2]) && (place[1, 2] == place[2, 2]) && place[0, 2] != " ")
                return (true, place[0, 2]);

            // по диагонали
            if ((place[0, 0] == place[1, 1]) && (place[1, 1] == place[2, 2]) && place[0, 0] != " ")
                return (true, place[0, 0]);


            if ((place[0, 2] == place[1, 1]) && (place[1, 1] == place[2, 0]) && place[0, 2] != " ")
                return (true, place[0, 2]);

            return (false, " ");
        }
        //Отрисовка игрового поля
        private static void PrintPlace(string[,] place)
        {

            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    Console.Write(place[i, j] + "|");
                }
                Console.WriteLine();
                Console.WriteLine("______");
            }
        }
        //Игра со вторым игроком 
        private static void GameOfPlayer(string[,] gamePlace)
        {
            bool isGameOver = false;

            while (!isGameOver)
            {

                int a = 0, b = 0;
                string line;

                Console.Clear();
                PrintPlace(gamePlace);
                //Вывод результата
                var result = Checkwinner(gamePlace);
                if (result.Item1 && result.Item2 != " ")
                {
                    Console.WriteLine($"Игра окончена. Победил: {result.Item2}");
                    Console.ReadLine();
                    isGameOver = true;
                    continue;
                }
                //Ввод данных и проверка их
                Console.Write("Введите координаты ячейки через запятую (Пример:0,1): ");
                line = Console.ReadLine();

                bool parseA = false;

                if (line != "")
                {
                    parseA = char.IsNumber(line[0]);
                }

                if (parseA == false)
                {
                    Console.WriteLine("Первой ячейки не существует, введите существующее значение");
                    Console.ReadLine();
                    continue;
                }
                else if (a < 0 || a > 2)
                {
                    Console.WriteLine("Первой ячейки не существует, введите существующее значение");
                    Console.ReadLine();
                    continue;
                }
                else
                {
                    a = Convert.ToInt16(line[0].ToString());
                }

                bool isNext = false;

                for (int i = 1; i < line.Length; i++)
                {

                    if (line[i].ToString() == ",")
                    {
                        isNext = true;
                    }
                    if (isNext && line[i].ToString() != " ")
                    {
                        bool parseB = char.IsNumber(line[i]);
                        if (parseB)
                        {
                            b = Convert.ToInt16(line[i].ToString());
                            break;
                        }

                    }
                }

                if (b < 0 || b > 2)
                {
                    Console.WriteLine("Второй ячейки не существует, введите существующее значение");
                    Console.ReadLine();
                    continue;
                }

                Console.Write("Введите значение X или O: ");
                line = Console.ReadLine();
                line = line.ToUpper();
                if (gamePlace[a, b] == " " && line != " ")
                {
                    gamePlace[a, b] = line;
                }
                else
                {
                    Console.WriteLine("Это поле занято или вы ничего не ввели");
                    Console.ReadLine();
                    continue;
                }
            }
            Console.ReadLine();
        }
    }
}
