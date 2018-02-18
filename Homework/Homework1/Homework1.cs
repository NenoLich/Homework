using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace Homework
{
    class Homework1: Homework
    {
        /// <summary>
        /// 1. Добавить свои объекты в иерархию объектов, чтобы получился красивый задний фон, похожий на полёт в звёздном пространстве.
        /// 2. *Заменить кружочки картинками, используя метод DrawImage.
        /// 3. **Разработать собственный класс заставка SplashScreen, 
        /// аналогичный классу Game в котором создайте собственную иерархию объектов и задайте их движение.
        /// Предусмотреть кнопки - Начало игры, Рекорды, Выход. Добавьте на заставку имя автора.
        /// </summary>
        public void Asteroids()
        {
            Form asteroidsForm = new Form ();
            asteroidsForm.Width = 800;
            asteroidsForm.Height = 600;
            Game.Awake(asteroidsForm);
            asteroidsForm.ShowDialog();
        }
    }
}
