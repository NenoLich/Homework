using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace Homework
{
    /// <summary>
    /// Выполнил: Шляпугин Андрей
    /// C# уровень 2
    /// Уроки: 1.
    /// 
    /// Часть функционала ввиде главного меню и вспомогательного класса Utility перекочевала из предыдущего курса (С# Уровень 1).
    /// В ходе разработки алгоритма проверки наличия свободного места на экране (SpaceObjectFactory.GetLegalPoint) для отрисовки изображений SpaceObject
    /// возникли трудности с долгим выполнением этого блока программы. 
    /// В связи с чем пришлось отказаться от идеи повторной генерации начальной точки позиционирования изображения и проверки на "легальность" (SpaceObjectFactory.HasAvailableSpace).
    /// </summary>
    static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
        }
    }
}
