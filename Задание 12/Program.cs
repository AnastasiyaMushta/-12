using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Задание_12
{
    class Program
    {
        static void Main(string[] args)
        {
            //массивы для сортировки
            int[] mas1 = { 1, 3, 5, 6, 7, 9, 10, 12, 15, 20 };    //возрастание
            int[] mas2 = new int[10];    //убывание
            int[] mas3 = { 9, 3, 15, 10, 12, 6, 20, 1, 5, 7 };    //вразброс
            int[] mas4 = RandomMas(10000);                       //случайные числа
            double[] mas_double = RandomMas(2000,true);

            Console.WriteLine("\nРезультаты сортировки упорядоченного по возрастанию массива:");
            Sort_1(mas_double);
            Sort_2(mas1);
            Array.Sort(mas1);
            Array.Reverse(mas1);
            Console.WriteLine("\nРезультаты сортировки упорядоченного по убыванию массива:");
            Sort_1(mas_double);
            Sort_2(mas1);

            Console.WriteLine("\nРезультаты сортировки неупорядоченного массива:");
            Sort_1(mas_double);
            Sort_2(mas3);

            Console.WriteLine(String.Format($"\nРезультаты сортировки массива с большим количеством элементов ({mas4.Length} элементов):"));
            Sort_1(mas_double);
            Sort_2(mas4);

            Console.ReadKey();
        }

        #region Блочная сортировка

        /// <summary>
        /// Блочная сортировка
        /// </summary>
        /// <param name="mas">Массив для сортировки</param>
        static void Sort_1(double[] mas)
        {
            int comparisons = 0;    //сравнения
            int moves = 0;          //перестановки

            List<double>[] aux = new List<double>[mas.Length]; //список карзин 
            Stopwatch sw = new Stopwatch();
            sw.Start();
            for (int i = 0; i < aux.Length; ++i) // инициализация каждой карзины
                aux[i] = new List<double>();

            

            for (int i = 0; i < mas.Length; i++)
            {
                int bcktIdx = (int)Math.Truncate(mas[i]*mas.Length);                   // вычисление индекса корзины
                aux[bcktIdx].Add(mas[i]);                                              // добавление элемента в соответствующую корзину

            }

            // сортировка корзин
            for (int i = 0; i < aux.Length; i++)
            {
                aux[i].Sort();
                moves++;
            }
            // собираем отсортированные элементы обратно в изначальный массив
            int idx = 0;

            for (int i = 0; i < aux.Length; ++i)
            {
                for (int j = 0; j < aux[i].Count; ++j)
                    mas[idx++] = aux[i][j];
            }
            sw.Stop();
            //результаты
            Console.WriteLine("\nБлочная сортировка:");
            Console.WriteLine($"Количество сравнений: {comparisons}");
            Console.WriteLine($"Количество перестановок: {moves}");
            Console.WriteLine($"Количество времени в тиках: {sw.ElapsedTicks}");
        }

        #endregion

        #region Поразрядная сортировка

        /// <summary>
        /// Поразрядная сортировка
        /// </summary>
        /// <param name="mas">Массив для сортировки</param>
        static void Sort_2(int[] mas)
        {
            int comparisons = 0;    //сравнения
            int moves = 0;          //перестановки

            Stopwatch sw = new Stopwatch();
            sw.Start();
            int digitCount = (int)Math.Log10(mas.Max()) + 1;    //количество разрядов максимального элемента

            //сортировка
            for (int i = 0; i < digitCount; i++)
            {
                List<int>[] digits = new List<int>[10];     //массив с числами, распределёнными по разрядам
                for (int j = 0; j < 10; j++)
                {
                    digits[j] = new List<int>();
                }

                foreach (var a in mas)                      //распределение чисел в массив discharge
                {
                    digits[GetDigit(a, i)].Add(a);
                    moves++;
                }

                List<int> tempList = new List<int>();       //сбор всех чисел воедино
                for (int j = 0; j < 10; j++)
                {
                    tempList.AddRange(digits[j]);
                    moves += digits[j].Count;
                }

                mas = tempList.ToArray();                   //возвращение чисел в массив
            }
            sw.Stop();

            //результаты
            Console.WriteLine("\nПоразрядная сортировка:");
            Console.WriteLine($"Количество сравнений: {comparisons}");
            Console.WriteLine($"Количество перестановок: {moves}");
            Console.WriteLine($"Количество времени в тиках: {sw.ElapsedTicks}");
        }

        /// <summary>
        /// Получение цифры числа
        /// </summary>
        /// <param name="a">Элемент массива</param>
        /// <param name="discharge">Необходимый разряд</param>
        /// <returns></returns>
        static int GetDigit(int a, int discharge)
        {
            a = (int)(a / Math.Pow(10, discharge));
            a = a % 10;

            return a;
        }

        /// <summary>
        /// Генерация массива из случайных чисел
        /// </summary>
        /// <param name="size">Размерность массива</param>
        /// <returns></returns>
        static int[] RandomMas(int size)
        {
            int[] mas = new int[size];  //массив
            Random rnd = new Random();  //генератор случайных чисел

            //заполнение массива
            for (int i = 0; i < size; i++)
            {
                mas[i] = rnd.Next(100000);
            }

            return mas;
        }
       /// <summary>
       /// Генерация массива типа double
       /// </summary>
       /// <param name="size">Длина массива</param>
       /// <param name="status"></param>
       /// <returns></returns>
        static double[] RandomMas(int size, bool status = false)
        {
            double[] mas = new double[size];  //массив
            Random rnd = new Random();  //генератор случайных чисел

            //заполнение массива
            for (int i = 0; i < size; i++)
            {
                mas[i] = rnd.NextDouble();
            }

            return mas;
        }
        #endregion
    }
} 
