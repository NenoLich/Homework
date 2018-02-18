using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homework
{
    class BulletFactory: SpaceObjectFactory
    {
        private const int SizeWidth = 40;
        private const int SizeHeight = 10;

        public BulletFactory(ScreenSpaceController screenSpaceController, Image image) : base(screenSpaceController, image)
        {
        }

        public override SpaceObject Create()
        {
            Point? legalPoint = screenSpaceController.GetLegalPoint(size);
            if (legalPoint is null)
            {
                return null;
            }
            return new StaticObject((Point)legalPoint, new Point(), new Size(SizeWidth, SizeHeight), image);
        }
    }
}
