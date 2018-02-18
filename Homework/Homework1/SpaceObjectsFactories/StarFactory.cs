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

        public StarFactory(ScreenSpaceController screenSpaceController, Image image) : base(screenSpaceController, image)
        {
        }

        public override SpaceObject Create()
        {
            size = starSize;

            Point? legalPoint = screenSpaceController.GetLegalPoint(size);
            if (legalPoint is null)
            {
                return null;
            }
            return new Star((Point)legalPoint, new Point(randomize.Next(0, starXmaxDirection), 0), new Size(size, size), image);
        }

        
    }
}
