using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homework
{
    class StarFactory : SpaceObjectFactory
    {
        private const int starXmaxDirection = 40;
        private const int starSize = 10;

        public StarFactory(Image image) : base(image)
        {
        }

        public override SpaceObject Create()
        {
            size = starSize;

            return new Star(GetLegalPoint(), new Point(randomize.Next(0, starXmaxDirection), 0), new Size(size, size), image);
        }
    }
}
