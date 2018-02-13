using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Homework
{
    public class Utility
    {
        [DllImport("kernel32.dll")]
        static extern IntPtr GetConsoleWindow();

        [DllImport("user32.dll")]
        static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        const int SwHide = 0;
        const int SwShow = 5;

        static readonly IntPtr handle = GetConsoleWindow();

        public static bool ConsoleIsHided;

        /// <summary>
        /// Дополнительные критерии для метода TryRead<T>
        /// </summary>
        [Flags]
        public enum TryReadConditions
        {
            None = 1,
            /// <summary>
            /// Метод TryRead возвращает ложь без сообщения об ошибке
            /// </summary>
            WithoutExceptionMessage = 2,
            /// <summary>
            /// Метод TryRead принимает только положительное число 
            /// </summary>
            OnlyPositiveSign = 4,
            /// <summary>
            /// Метод TryRead принимает конечное значение без запроса через консоль
            /// </summary>
            WithoutConsoleRequest = 8
        }

        /// <summary>
        /// Позволяет обрабатывать "." в качестве разделителя целой и дробной части числа
        /// </summary>
        private static readonly IFormatProvider Formatter = new NumberFormatInfo { NumberDecimalSeparator = "." };
        /// <summary>
        /// Метод пытается присвоить введенную в консоль строку в переменную обобщенного типа
        /// </summary>
        /// <typeparam name="T">Тип, в который будет конвертироваться вводимая в консоль строка</typeparam>
        /// <param name="userRequest">Выводимое в консоль сообщение</param>
        /// <param name="data">Переменная, в которую будет записана входная строка консоли</param>
        /// <returns>Истина, если конвертация прошла успешно</returns>
        public static bool TryRead<T>(string userRequest, out T data)
        {
            Console.WriteLine(userRequest);
            var response = Console.ReadLine();
            try
            {
                data = (T)Convert.ChangeType(response, typeof(T), Formatter);
                return true;
            }
            catch (Exception ex)
            {
                data = default(T);
                Console.WriteLine(ex.Message);
                return false;
            }
        }
        /// <summary>
        /// Метод пытается присвоить введенную в консоль строку в переменную обобщенного типа
        /// </summary>
        /// <typeparam name="T">Тип, в который будет конвертироваться вводимая в консоль строка</typeparam>
        /// <param name="userRequest">Выводимое в консоль сообщение</param>
        /// <param name="data">Переменная, в которую будет записана входная строка консоли</param>
        /// <param name="condition">Элемент перечисления TryReadConditions, можно использовать побитовую комбинацию</param>
        /// <returns>Истина, если конвертация прошла успешно</returns>
        public static bool TryRead<T>(string userRequest, out T data, TryReadConditions condition)
        {
            double minThreshold = 0d;
            string response;
            if ((condition & TryReadConditions.WithoutConsoleRequest) != TryReadConditions.WithoutConsoleRequest)
            {
                Console.WriteLine(userRequest);
                response = Console.ReadLine();
            }
            else
            {
                response = userRequest;
            }

            try
            {
                data = (T)Convert.ChangeType(response, typeof(T), Formatter);
                if ((condition & TryReadConditions.OnlyPositiveSign) == TryReadConditions.OnlyPositiveSign && minThreshold.CompareTo(Convert.ToDouble(data)) > 0)
                {
                    throw new Exception("Недопустимое значение");
                }
                return true;
            }
            catch (Exception ex)
            {
                data = default(T);
                if ((condition & TryReadConditions.WithoutExceptionMessage) != TryReadConditions.WithoutExceptionMessage)
                {
                    Console.WriteLine(ex.Message);
                }
                return false;
            }
        }
        /// <summary>
        /// Метод пытается присвоить введенную в консоль строку в переменную обобщенного типа
        /// </summary>
        /// <typeparam name="T">Тип, в который будет конвертироваться вводимая в консоль строка</typeparam>
        /// <param name="userRequest">Выводимое в консоль сообщение</param>
        /// <param name="data">Переменная, в которую будет записана входная строка консоли</param>
        /// <param name="criterion">Метод, содержащий дополнительные условия для входной строки</param>
        /// <returns>Истина, если конвертация прошла успешно</returns>
        public static bool TryRead<T>(string userRequest, out T data, Predicate<T> criterion)
        {
            Console.WriteLine(userRequest);
            var response = Console.ReadLine();
            try
            {
                data = (T)Convert.ChangeType(response, typeof(T), Formatter);
                if (!criterion(data))
                {
                    throw new Exception("Недопустимое значение");
                }
                return true;
            }
            catch (Exception ex)
            {
                data = default(T);
                Console.WriteLine(ex.Message);
                return false;
            }
        }
        /// <summary>
        /// Метод пытается присвоить введенную в консоль строку в переменную обобщенного типа
        /// </summary>
        /// <typeparam name="T">Тип, в который будет конвертироваться вводимая в консоль строка</typeparam>
        /// <param name="userRequest">Выводимое в консоль сообщение</param>
        /// <param name="data">Переменная, в которую будет записана входная строка консоли</param>
        /// <param name="condition">Элемент перечисления TryReadConditions, можно использовать побитовую комбинацию</param>
        /// <param name="criterion">Метод, содержащий дополнительные условия для входной строки</param>
        /// <returns>Истина, если конвертация прошла успешно</returns>
        public static bool TryRead<T>(string userRequest, out T data, TryReadConditions condition, Predicate<T> criterion)
        {
            double minThreshold = 0d;
            string response;
            if ((condition & TryReadConditions.WithoutConsoleRequest) != TryReadConditions.WithoutConsoleRequest)
            {
                Console.WriteLine(userRequest);
                response = Console.ReadLine();
            }
            else
            {
                response = userRequest;
            }
            try
            {
                data = (T)Convert.ChangeType(response, typeof(T), Formatter);
                if (((condition & TryReadConditions.OnlyPositiveSign) == TryReadConditions.OnlyPositiveSign && minThreshold.CompareTo(Convert.ToDouble(data)) > 0)
                    || !criterion(data))
                {
                    throw new Exception("Недопустимое значение");
                }
                return true;
            }
            catch (Exception ex)
            {
                data = default(T);
                if ((condition & TryReadConditions.WithoutExceptionMessage) != TryReadConditions.WithoutExceptionMessage)
                {
                    Console.WriteLine(ex.Message);
                }
                return false;
            }
        }

        /// <summary>
        /// Метод пытается присвоить введенную в консоль строку в переменную обобщенного типа
        /// </summary>
        /// <typeparam name="T">Тип, в который будет конвертироваться вводимая в консоль строка</typeparam>
        /// <param name="userRequest">Выводимое в консоль сообщение</param>
        /// <param name="data">Переменная, в которую будет записана входная строка консоли</param>
        /// <param name="errorLabel">Метка для вывода сообщения об ошибке</param>
        /// <returns>Истина, если конвертация прошла успешно</returns>
        public static bool TryRead<T>(string userRequest, out T data, Label errorLabel)
        {
            var response = userRequest;
            try
            {
                data = (T)Convert.ChangeType(response, typeof(T), Formatter);
                return true;
            }
            catch (Exception ex)
            {
                data = default(T);
                errorLabel.Text = ex.Message;
                return false;
            }
        }

        /// <summary>
        /// Метод создает запрос у пользователя на выбор элемента колекции MethodInfo и возвращает его
        /// </summary>
        /// <param name="description">Описание коллекции</param>
        /// <param name="collection">Коллекция обьектов MethodInfo</param>
        /// <param name="request">Запрос для пользователя</param>
        /// <returns>Элемент коллекции, выбранный пользователем</returns>
        public static T GetUserSelection<T>(string description, T[] collection, string request) where T : MemberInfo
        {
            int number;
            Console.WriteLine(description);
            for (int i = 0; i < collection.Length; i++)
            {
                Console.WriteLine($"{i + 1} - {collection[i].Name}");
            }
            while (!TryRead(request, out number, (x) => collection[x - 1] != null)) { }
            return collection[number - 1];
        }
        /// <summary>
        /// Запрашивает критерий завершения текущего блока
        /// </summary>
        /// <param name="caller">Ссылка на вызывающий метод</param>
        public static bool TryToStopLoop([System.Runtime.CompilerServices.CallerMemberName] string caller = "")
        {
            Console.WriteLine("1 - Продолжить\n0 - Закончить выполнение задания {0}", caller);
            switch (Console.ReadLine())
            {
                case "1":
                    return true;
                case "0":
                    return false;
                default:
                    Console.WriteLine("Некорректный ввод, воспринят как выход");
                    Console.ReadKey();
                    return false;
            }
        }

        public static void HideConsole()
        {
            ShowWindow(handle, SwHide);
            ConsoleIsHided = true;
        }
        public static void ShowConsole()
        {
            ShowWindow(handle, SwShow);
            ConsoleIsHided = false;
        }
    }
}
