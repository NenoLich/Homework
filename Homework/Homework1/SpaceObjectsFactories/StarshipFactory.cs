using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homework
{
    class StarshipFactory: SpaceObjectFactory
    {
        private static List<Image> starshipImages;

        public StarshipFactory()
        {
            image = starshipImages[Game.randomizer.Next(0, starshipImages.Count)];
        }

        public static void Init(string path)
        {
            starshipImages = ImagesLoad(path);
        }

        public override SpaceObject Create()
        {
            size = 80;

            return new Starship(new Point(100,260), new Point(5,5), new Size(size, size), image);
        }
    }
}
