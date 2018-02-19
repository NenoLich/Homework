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
        /// 2. Переделать виртуальный метод Update в BaseObject в абстрактный и реализовать его в наследниках.
        /// 3. Сделать так, чтобы при столкновениях пули с астероидом пуля и астероид регенерировались в разных концах экрана;
        /// 4. Сделать проверку на задание размера экрана в классе Game.
        ///     Если высота или ширина(Width, Height) больше 1000 или принимает отрицательное значение, то выбросить исключение ArgumentOutOfRangeException().
        /// 5. *Создать собственное исключение GameObjectException, 
        ///     которое появляется при попытке создать объект с неправильными характеристиками(например, отрицательные размеры, 
        ///     слишком большая скорость или позиция).
        /// 
        /// Класс Bullet находится в разработке, поэтому в проекте пока не представлен и не тестирован
        /// </summary>
        public void Asteroids()
        {
            Form asteroidsForm = new Form ();
            asteroidsForm.Width = 800;
            asteroidsForm.Height = 600;
            if (Game.Awake(asteroidsForm))
            {
                asteroidsForm.ShowDialog();
            }
        }
    }
}
