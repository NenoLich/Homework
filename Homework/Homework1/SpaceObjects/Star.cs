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

        }
        public Star(Point pos, Point dir, Size size,Image image) : base(pos, dir, size,image)
        {

        }
        //public override void Draw()
        //{
           
        //}

        public override void Update()
        {
            Position.X = Position.X - Direction.X;
            if (Position.X < 0) Position.X = Game.Width + Size.Width;
        }
    }
}
