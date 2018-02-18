using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homework
{
    class StaticObject: SpaceObject
    {
        public StaticObject(Point pos, Point dir, Size size) : base(pos, dir, size)
        {

        }
        public StaticObject(Point pos, Point dir, Size size, Image image) : base(pos, dir, size, image)
        {

        }
    }
}