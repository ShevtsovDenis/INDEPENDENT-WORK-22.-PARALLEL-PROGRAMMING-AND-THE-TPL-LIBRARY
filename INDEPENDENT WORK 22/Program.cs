using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
/*1.    Сформировать массив случайных целых чисел (размер  задается пользователем). 
 * Вычислить сумму чисел массива и максимальное число в массиве.  Реализовать  решение  задачи  с  использованием  механизма  задач продолжения.*/

namespace INDEPENDENT_WORK_22
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Введите размерность массива");//запрашиваем размерность массива
            int n = Convert.ToInt32(Console.ReadLine());

            Func<object, int[]> func = new Func<object, int[]>(GetArray);//создаем экземпляр делегата, принимающего число размерности и возвращающего массив созданный 1 методом
            Task<int[]> task1 = new Task<int[]>(func, n);//создаем экземпляр класса задачи. В качестве аргументов исползуем экземпляр делегата и размерность

            Action<Task<int[]>> action = new Action<Task<int[]>>(PrintSumm_Max);//экземпляр делегата, принимающего 1 задачу и связанный с методом 2. Данный делегат ничего не возвращает
            Task task2 = task1.ContinueWith(action);//экземпляр класса задачи, продолжающей 1 задачу

            task1.Start();//запускаем задачу 1

            Console.ReadKey();
        }

        static int[] GetArray(object a)//создаем метод формаирования массива. В качестве оргумента используем тип object для возможности создать экземпляр делегата Func.
        {
            int n = (int)a;
            int[] array = new int[n];
            Random random = new Random();
            Console.WriteLine("Сформированный массив:");
            for (int i = 0; i < n; i++)
            {
                array[i] = random.Next(0, 50);
                Console.Write($"{array[i]} ");
            }
            Console.WriteLine();
            return array;
        }

        static void PrintSumm_Max(Task<int[]> task)//создаем метод, который находит сумму чисел и максимальное число. В качестве аргумента принимает первую задачу
        {
            int[] array = task.Result;
            int sum = 0;
            for (int i = 0; i < array.Count(); i++)
            {
                sum += array[i];
            }
            Console.WriteLine("Сумма чисел в массиве равна {0}", sum);

            int max = array[0];
            foreach (int a in array)
            {
                if (a > max)
                    max = a;
            }
            Console.WriteLine("Максимальное число в массиве {0}", max);
        }
    }
}
