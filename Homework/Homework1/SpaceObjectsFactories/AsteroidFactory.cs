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
        private static List<Image> asteroidImages= FillImageList();

        private const int asteroidXminDirection = 10;
        private const int asteroidXmaxDirection = 50;
        private const int minAsteroidSize = 20;
        private const int maxAsteroidSize = 80;

        /// <summary>
        /// Заполняет коллекцию изображений астероидов, делая прозрачным цвет фона
        /// </summary>
        /// <returns></returns>
        private static List<Image> FillImageList()
        {
            List<Image> images=new List<Image>();
            try
            {
                List<string> imagePaths = Utility.GetFiles(@"Homework1\Asteroids", "*.jpeg|*.png").ToList();
                if (imagePaths != null)
                {
                    for (int i = 0; i < imagePaths.Count; i++)
                    {
                        Bitmap image = new Bitmap(imagePaths[i]);
                        Color backgroundColor = image.GetPixel(1, 1);
                        image.MakeTransparent(backgroundColor);
                        images.Add(image);
                    }
                }
            }
            catch (FileNotFoundException e)
            {
                MessageBox.Show(e.Message);
            }

            return images;
        }

        /// <summary>
        /// Изображение астероида генерируется случайным образом из общего списка
        /// </summary>
        /// <param name="screenSpaceController"></param>
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
