using System;
using System.IO;
using System.Text;

public class Program
{
    private static readonly Random random = new Random();

    public static void Main()
    {
        Console.InputEncoding = Encoding.UTF8;
        Console.OutputEncoding = Encoding.UTF8;
        try
        {
            int choice;
            do
            {
                choice = ReadInt("\n1. Матрица\n2. Рваный массив\n3. Строка"
                    + "\n0. Выход\nВыбор: ", 0, 3);
                if (choice == 1)
                    RunMatrix();
                if (choice == 2)
                    RunJaggedArray();
                if (choice == 3)
                    RunText();
            } while (choice != 0);
        }
        catch (EndOfStreamException)
        {
            Console.WriteLine("Ввод завершён. Программа закрыта.");
        }
    }

    public static string ReadLine()
    {
        string text = Console.ReadLine();
        if (text == null)
            throw new EndOfStreamException();
        return text;
    }

    public static int ReadInt(string message, int min = int.MinValue,
        int max = int.MaxValue)
    {
        while (true)
        {
            Console.Write(message);
            int number;
            if (int.TryParse(ReadLine(), out number) && number >= min && number <= max)
                return number;
            Console.WriteLine("Ошибка: введите целое число от " + min
                + " до " + max + ".");
        }
    }

    public static int ReadMode(string second = "Заполнить случайно")
    {
        return ReadInt("\n1. Ввести вручную\n2. " + second
            + "\n0. Назад\nВыбор: ", 0, 2);
    }

    public static void RunMatrix()
    {
        int mode = ReadMode();
        if (mode == 0)
            return;
        int[,] matrix = CreateMatrix(mode == 2);
        PrintArray(matrix);
        if (matrix.GetLength(0) == 0)
        {
            Console.WriteLine("Ошибка: в массиве нет строк.");
            return;
        }
        int number = ReadInt("Номер нового столбца: ", 1, matrix.GetLength(1) + 1);
        int[] values = ReadValues(matrix.GetLength(0));
        PrintArray(AddColumn(matrix, number - 1, values));
    }

    public static int[,] CreateMatrix(bool isRandom)
    {
        int rowCount = ReadInt("Количество строк (0-20): ", 0, 20);
        int columnCount = ReadInt("Количество столбцов (0-20): ", 0, 20);
        int[,] matrix = new int[rowCount, columnCount];
        if (isRandom)
            FillRandom(matrix);
        else
        {
            for (int i = 0; i < rowCount; i++)
                for (int j = 0; j < columnCount; j++)
                    matrix[i, j] = ReadInt("Элемент [" + (i + 1)
                        + ", " + (j + 1) + "]: ");
        }
        return matrix;
    }

    public static void FillRandom(int[,] matrix)
    {
        for (int i = 0; i < matrix.GetLength(0); i++)
            for (int j = 0; j < matrix.GetLength(1); j++)
                matrix[i, j] = random.Next(-9, 10);
    }

    public static int[] ReadValues(int count)
    {
        int[] values = new int[count];
        for (int i = 0; i < count; i++)
            values[i] = ReadInt("Элемент " + (i + 1) + ": ");
        return values;
    }

    public static void PrintArray(int[,] matrix)
    {
        int rowCount = matrix.GetLength(0);
        int columnCount = matrix.GetLength(1);
        Console.WriteLine("Размер: " + rowCount + " x " + columnCount);
        if (matrix.Length == 0)
        {
            Console.WriteLine("Массив пуст.");
            return;
        }
        for (int i = 0; i < rowCount; i++)
        {
            for (int j = 0; j < columnCount; j++)
                Console.Write(matrix[i, j] + "\t");
            Console.WriteLine();
        }
    }

    public static int[,] AddColumn(int[,] matrix, int columnIndex, int[] values)
    {
        int rowCount = matrix.GetLength(0);
        int columnCount = matrix.GetLength(1);
        if (rowCount == 0 || columnIndex < 0 || columnIndex > columnCount
            || values.Length != rowCount)
            throw new ArgumentException("Невозможно добавить столбец.");
        int[,] result = new int[rowCount, columnCount + 1];
        for (int i = 0; i < rowCount; i++)
        {
            for (int j = 0; j <= columnCount; j++)
            {
                if (j < columnIndex)
                    result[i, j] = matrix[i, j];
                else if (j == columnIndex)
                    result[i, j] = values[i];
                else
                    result[i, j] = matrix[i, j - 1];
            }
        }
        return result;
    }

    public static void RunJaggedArray()
    {
        int mode = ReadMode();
        if (mode == 0)
            return;
        int[][] rows = CreateJaggedArray(mode == 2);
        PrintArray(rows);
        if (rows.Length == 0)
        {
            Console.WriteLine("Ошибка: массив пуст, удалять нечего.");
            return;
        }
        int index = FindShortestRow(rows);
        Console.WriteLine("Удалена строка " + (index + 1) + ".");
        PrintArray(RemoveRow(rows, index));
    }

    public static int[][] CreateJaggedArray(bool isRandom)
    {
        int rowCount = ReadInt("Количество строк (0-20): ", 0, 20);
        int[][] rows = new int[rowCount][];
        for (int i = 0; i < rowCount; i++)
        {
            int length = ReadInt("Длина строки " + (i + 1) + " (0-20): ", 0, 20);
            if (isRandom)
            {
                rows[i] = new int[length];
                FillRandom(rows[i]);
            }
            else
                rows[i] = ReadValues(length);
        }
        return rows;
    }

    public static void FillRandom(int[] values)
    {
        for (int i = 0; i < values.Length; i++)
            values[i] = random.Next(-9, 10);
    }

    public static void PrintArray(int[][] rows)
    {
        if (rows.Length == 0)
        {
            Console.WriteLine("Массив пуст.");
            return;
        }
        for (int i = 0; i < rows.Length; i++)
        {
            Console.Write((i + 1) + ": ");
            if (rows[i].Length == 0)
                Console.Write("(пустая строка)");
            for (int j = 0; j < rows[i].Length; j++)
                Console.Write(rows[i][j] + "\t");
            Console.WriteLine();
        }
    }

    public static int FindShortestRow(int[][] rows)
    {
        if (rows.Length == 0)
            throw new ArgumentException("В массиве нет строк.");
        int shortestIndex = 0;
        // Строгое сравнение оставляет первую строку при равных длинах.
        for (int i = 1; i < rows.Length; i++)
            if (rows[i].Length < rows[shortestIndex].Length)
                shortestIndex = i;
        return shortestIndex;
    }

    public static int[][] RemoveRow(int[][] rows, int rowIndex)
    {
        if (rowIndex < 0 || rowIndex >= rows.Length)
            throw new ArgumentException("Неверный номер строки.");
        int[][] result = new int[rows.Length - 1][];
        for (int i = 0; i < rowIndex; i++)
            result[i] = rows[i];
        for (int i = rowIndex + 1; i < rows.Length; i++)
            result[i - 1] = rows[i];
        return result;
    }

    public static void RunText()
    {
        int mode = ReadMode("Выбрать пример");
        if (mode == 0)
            return;
        string text;
        if (mode == 1)
        {
            Console.Write("Введите строку: ");
            text = ReadLine();
        }
        else
        {
            string[] examples = { "Один два.", "Три четыре.", "", "Текст." };
            for (int i = 0; i < examples.Length; i++)
                Console.WriteLine((i + 1) + ". " + examples[i]);
            int number = ReadInt("Номер: ", 1, examples.Length);
            text = examples[number - 1];
        }
        PrintText(text);
        PrintText(ReverseEvenWords(text));
    }

    public static void PrintText(string text)
    {
        if (text.Length == 0)
            Console.WriteLine("Строка пустая.");
        else
            Console.WriteLine("Строка: «" + text + "»");
    }

    public static bool IsSeparator(char symbol)
    {
        return char.IsWhiteSpace(symbol) || ",;:.!?".IndexOf(symbol) >= 0;
    }

    public static string ReverseEvenWords(string text)
    {
        char[] letters = text.ToCharArray();
        int wordNumber = 0;
        int position = 0;
        while (position < letters.Length)
        {
            if (IsSeparator(letters[position]))
            {
                position++;
                continue;
            }
            int wordStart = position;
            while (position < letters.Length && !IsSeparator(letters[position]))
                position++;
            wordNumber++;
            if (wordNumber % 2 == 0)
                Array.Reverse(letters, wordStart, position - wordStart);
        }
        return new string(letters);
    }
}
