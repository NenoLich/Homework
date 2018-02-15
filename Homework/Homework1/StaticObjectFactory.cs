using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homework
{
    class StaticObjectFactory: SpaceObjectFactory
    {
        private const int minStaticObjectSize = 25;
        private const int maxStaticObjectSize = 100;

        public StaticObjectFactory(Image image) : base(image)
        {
        }

        public override SpaceObject Create()
        {
            size = randomize.Next(minStaticObjectSize, maxStaticObjectSize);

            return new StaticObject(GetLegalPoint(), new Point(), new Size(size, size), image);
        }

    }
}
