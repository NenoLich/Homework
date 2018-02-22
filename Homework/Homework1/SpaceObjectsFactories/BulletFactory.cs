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
        private static List<Image> bulletImages;

        private const int SizeWidth = 40;
        private const int SizeHeight = 10;
        private const int Speed = 80;

        public BulletFactory()
        {
            if (bulletImages.Count>0)
            {
                image = bulletImages[Game.randomizer.Next(0, bulletImages.Count)];

            }
        }

        public static void Init(string path)
        {
            bulletImages = ImagesLoad(path);
        }

        public override SpaceObject Create()
        {
            if (image is null)
            {
                return null;
            }

            return new Bullet(Game.player.GunPoint, new Point(Speed, 0), new Size(SizeWidth, SizeHeight), image);
        }
    }
}
