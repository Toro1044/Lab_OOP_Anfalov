using System;

public class Program
{
    public static void Main()
    {
        // Начало и конец диапазона
        var startX = Math.PI / 5.0;
        var endX = 9.0 * Math.PI / 5.0;

        // Количество точек и членов ряда
        var numberOfPoints = 10;
        var numberOfTerms = 40;

        // Заданная точность
        var epsilon = 0.0001;

        // Шаг между точками
        var step = (endX - startX) / (numberOfPoints - 1);

        Console.WriteLine("Вычисление функции");
        Console.WriteLine();

        // Цикл по десяти значениям x
        for (var pointNumber = 0; pointNumber < numberOfPoints; pointNumber++)
        {
            var x = startX + pointNumber * step;

            // Вычисление суммы первых 40 членов ряда
            var sumForNumberOfTerms = 0.0;

            for (var termNumber = 1;
                 termNumber <= numberOfTerms;
                 termNumber++)
            {
                var term = Math.Cos(termNumber * x) / termNumber;
                sumForNumberOfTerms += term;
            }

            // Вычисление суммы с заданной точностью
            var sumForEpsilon = 0.0;
            var termNumberForEpsilon = 1;

            while (true)
            {
                var term = Math.Cos(termNumberForEpsilon * x)
                    / termNumberForEpsilon;

                sumForEpsilon += term;

                if (Math.Abs(term) < epsilon)
                {
                    break;
                }

                termNumberForEpsilon++;
            }

            // Точное значение функции
            var exactValue = -Math.Log(
                Math.Abs(2.0 * Math.Sin(x / 2.0)));

            // Вывод результата
            Console.WriteLine(
                $"X = {x:F8}; " +
                $"SN = {sumForNumberOfTerms:F8}; " +
                $"SE = {sumForEpsilon:F8}; " +
                $"Y = {exactValue:F8}");
        }
    }
}