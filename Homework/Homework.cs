using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Homework
{
    abstract class Homework
    {
        /// <summary>
        /// Менеджер заданий. Запускает задания, проверяет выполнение, запускает повторно при надобности
        /// </summary>
        /// <param name="method">Выбранный метод</param>
        internal void ExecuteHomework(MethodInfo method, object[] parameters)
        {
            Utility.ShowConsole();
            method.Invoke(this, parameters);
            if (Utility.ConsoleIsHided)
            {
                return;
            }

            Console.ReadKey();
            Utility.HideConsole();
        }

        /// <summary>
        /// Вызывает отпределенный метод, запрашивая значения его параметров у пользователя
        /// </summary>
        /// <param name="method">Иполняемый метод</param>
        /// <param name="target">Обьект, для которого будет вызван метод</param>
        protected void MethodInvoke(MethodInfo method, object target)
        {
            ParameterInfo[] parametersInfo = method.GetParameters();
            object[] parameters;
            if (parametersInfo?.Length != 0)
            {
                parameters = new object[parametersInfo.Length];
                Console.WriteLine("Укажите параметры метода:");
                for (int i = 0; i < parametersInfo.Length; i++)
                {
                    while (!Utility.TryRead(string.Format("{0} = ", parametersInfo[i].Name),
                        out parameters[i]))
                    {
                    }
                }
            }
            else
            {
                parameters = null;
            }

            method.Invoke(target, parameters);
        }
    }
}
