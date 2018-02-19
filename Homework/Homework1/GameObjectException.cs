using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Homework
{
    /// <summary>
    /// Создается при попытке задания неверных параметров игрового обьекта
    /// </summary>
    class GameObjectException: Exception
    {
        public GameObjectException(string message)
        {
            MessageBox.Show(message);
        }
    }
}
