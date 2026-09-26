using System;

class Program
{
    static void Main()
    {
        // Задание 1

        Console.Write("Введите m: ");
        int m = int.Parse(Console.ReadLine());

        Console.Write("Введите n: ");
        int n = int.Parse(Console.ReadLine());

        int oldM = m;
        int oldN = n;

        int answer1 = ++n * ++m;

        Console.WriteLine("++n * ++m = " + answer1);
        Console.WriteLine("m = " + m + ", n = " + n);

        m = oldM;
        n = oldN;

        bool answer2 = m++ < n;

        Console.WriteLine("m++ < n = " + answer2);
        Console.WriteLine("m = " + m + ", n = " + n);

        m = oldM;
        n = oldN;

        bool answer3 = n++ > m;

        Console.WriteLine("n++ > m = " + answer3);
        Console.WriteLine("m = " + m + ", n = " + n);

        Console.Write("Введите x: ");
        double x = double.Parse(Console.ReadLine());

        if (x == -1 || x == 0 || x == 1)
        {
            Console.WriteLine("Выражение вычислить нельзя");
        }
        else
        {
            double answer4 = x + 1 / (x * x * x - x) - 2;
            Console.WriteLine("Результат = " + answer4);
        }

        // Задание 2

        Console.WriteLine();
        Console.WriteLine("Задание 2");

        Console.Write("Введите координату x: ");
        double pointX = double.Parse(Console.ReadLine());

        Console.Write("Введите координату y: ");
        double pointY = double.Parse(Console.ReadLine());

        if (pointX >= -7 && pointX <= 0 &&
            pointY <= 0)
        {
            Console.WriteLine("Точка находится в закрашенной области");
        }
        else
        {
            Console.WriteLine("Точка не находится в закрашенной области");
        }

        // Задание 3

        Console.WriteLine();
        Console.WriteLine("Задание 3");

        float a1 = 1000f;
        float b1 = 0.0001f;

        float resultFloat =
            ((a1 - b1) * (a1 - b1)
            - (a1 * a1 - 2 * a1 * b1))
            / (b1 * b1);

        double a2 = 1000;
        double b2 = 0.0001;

        double resultDouble =
            ((a2 - b2) * (a2 - b2)
            - (a2 * a2 - 2 * a2 * b2))
            / (b2 * b2);

        Console.WriteLine("Результат для float = " + resultFloat);
        Console.WriteLine("Результат для double = " + resultDouble);
    }
}
