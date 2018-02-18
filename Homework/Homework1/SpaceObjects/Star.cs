using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homework
{
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
