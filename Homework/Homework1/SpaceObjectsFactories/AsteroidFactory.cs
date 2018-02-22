using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Homework;

namespace Homework
{ 
    class AsteroidFactory: SpaceObjectFactory
    {
        private static List<Image> asteroidImages;

        private const int asteroidXminDirection = 10;
        private const int asteroidXmaxDirection = 50;
        private const int minAsteroidSize = 20;
        private const int maxAsteroidSize = 80;

        /// <summary>
        /// Изображение астероида генерируется случайным образом из общего списка
        /// </summary>
        /// <param name="screenSpaceController"></param>
        public AsteroidFactory(ScreenSpaceController screenSpaceController)
        {
            this.screenSpaceController = screenSpaceController;

            if (asteroidImages.Count>0)
            {
                image = asteroidImages[Game.randomizer.Next(0, asteroidImages.Count)];
            }
        }

        public static void Init(string path)
        {
            asteroidImages = ImagesLoad(path);
        }

        public override SpaceObject Create()
        {
            size = Game.randomizer.Next(minAsteroidSize, maxAsteroidSize);

            Point? legalPoint = screenSpaceController.GetLegalPoint(size);
            if (legalPoint is null || image is null)
            {
                return null;
            }
            return new Asteroid((Point)legalPoint, new Point(Game.randomizer.Next(asteroidXminDirection, asteroidXmaxDirection), 0), new Size(size, size), image);
        }
    }
}
