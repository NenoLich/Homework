using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Homework;

namespace Homework
{ 
    class AsteroidFactory: SpaceObjectFactory
    {
        private const int asteroidXminDirection = 10;
        private const int asteroidXmaxDirection = 50;
        private const int minAsteroidSize = 20;
        private const int maxAsteroidSize = 80;

        public AsteroidFactory(ScreenSpaceController screenSpaceController, Image image) : base(screenSpaceController, image)
        {
        }

        public override SpaceObject Create()
        {
            size = Game.randomizer.Next(minAsteroidSize, maxAsteroidSize);

            Point? legalPoint = screenSpaceController.GetLegalPoint(size);
            if (legalPoint is null)
            {
                return null;
            }
            return new StaticObject((Point)legalPoint, new Point(Game.randomizer.Next(asteroidXminDirection, asteroidXmaxDirection), 0), new Size(size, size), image);
        }
    }
}
