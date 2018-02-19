using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homework
{
    /// <summary>
    /// Описывает обьекты имеющие "физическое" тело
    /// </summary>
    interface ICollidable
    {
        bool Collide(ICollidable obj);

        Rectangle Rect { get; }
    }
}
