using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homework
{
    /// <summary>
    /// Статические обьекты звездного неба
    /// </summary>
    class StaticObject: SpaceObject
    {
        public StaticObject(Point pos, Point dir, Size size) : base(pos, dir, size)
        {
            HasCollider = false;
        }
        public StaticObject(Point pos, Point dir, Size size, Image image) : base(pos, dir, size, image)
        {
            HasCollider = false;
        }

        public override void Update()
        {
            
        }
    }
}