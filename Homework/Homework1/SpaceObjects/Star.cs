using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homework
{
    /// <summary>
    /// Летящие на заднем фоне звезды
    /// </summary>
    class Star: SpaceObject
    {
        public Star(Point pos, Point dir, Size size) : base(pos, dir, size)
        {
            HasCollider = false;
        }
        public Star(Point pos, Point dir, Size size,Image image) : base(pos, dir, size,image)
        {
            HasCollider = false;
        }

        /// <summary>
        /// При выходе за игровую зону переносится в зону видимости, сохраняя то же значение по оси Y
        /// </summary>
        public override void Update()
        {
            position.X = position.X - Direction.X;
            if (position.X < 0) Relocate(position.Y);
        }

        public override void Relocate()
        {
            position.X= Game.Width + size.Width;
            position.X = Game.Width + size.Width;
            position.Y = Game.randomizer.Next(0, Game.Height - size.Height);
        }

        public override void Relocate(int PositionHeight)
        {
            position.X = Game.Width + size.Width;
            position.Y = PositionHeight;
        }
    }
}
