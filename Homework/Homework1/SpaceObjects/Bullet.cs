using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Win32.SafeHandles;

namespace Homework
{
    /// <summary>
    /// Представляет собой выпущенные боевые снаряды
    /// </summary>
    class Bullet : SpaceObject
    {

        public Bullet(Point position, Point direction, Size size) : base(position, direction, size)
        {
            HasCollider = true;
        }

        public Bullet(Point position, Point direction, Size size, Image image) : base(position, direction, size, image)
        {
            HasCollider = true;
        }

        /// <summary>
        /// При выходе за игровую область обьект ликвидируется
        /// </summary>
        public override void Update()
        {
            if (position.X > Game.Width)
            {
                this.Dispose();
            }

            position.X = position.X + Direction.X;
        }

    }
}
