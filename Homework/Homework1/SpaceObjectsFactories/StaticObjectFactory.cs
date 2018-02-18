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

        public StaticObjectFactory(ScreenSpaceController screenSpaceController, Image image) : base(screenSpaceController, image)
        {
        }

        public override SpaceObject Create()
        {
            size = randomize.Next(minStaticObjectSize, maxStaticObjectSize);

            Point? legalPoint = screenSpaceController.GetLegalPoint(size);
            if (legalPoint is null)
            {
                return null;
            }
            return new StaticObject((Point)legalPoint, new Point(), new Size(size, size), image);
        }

    }
}
