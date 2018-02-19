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
        private static List<Image> asteroidImages= FillImageList();

        private const int asteroidXminDirection = 10;
        private const int asteroidXmaxDirection = 50;
        private const int minAsteroidSize = 20;
        private const int maxAsteroidSize = 80;

        private static List<Image> FillImageList()
        {
            List<Image> images=new List<Image>();
            List<string> imagePaths = Utility.GetFiles(@"Homework1\Asteroids", "*.jpeg|*.png").ToList();
            for (int i = 0; i < imagePaths.Count; i++)
            {
                Bitmap image= new Bitmap(imagePaths[i]);
                Color backgroundColor = image.GetPixel(1, 1);
                image.MakeTransparent(backgroundColor);
                images[i]=image;
            }

            return images;
        }

        public AsteroidFactory(ScreenSpaceController screenSpaceController)
        {
            this.screenSpaceController = screenSpaceController;
            image = asteroidImages[Game.randomizer.Next(0, asteroidImages.Count)];
        }

        public override SpaceObject Create()
        {
            size = Game.randomizer.Next(minAsteroidSize, maxAsteroidSize);

            Point? legalPoint = screenSpaceController.GetLegalPoint(size);
            if (legalPoint is null)
            {
                return null;
            }
            return new Asteroid((Point)legalPoint, new Point(Game.randomizer.Next(asteroidXminDirection, asteroidXmaxDirection), 0), new Size(size, size), image);
        }
    }
}
