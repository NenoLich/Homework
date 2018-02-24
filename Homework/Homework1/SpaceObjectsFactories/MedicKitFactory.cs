using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homework
{
    class MedicKitFactory: SpaceObjectFactory
    {
        private static List<Image> medicKitImages;

        private const int minMedicKitSize = 15;
        private const int maxMedicKitSize = 50;

        /// <summary>
        /// Изображение аптечки генерируется случайным образом из общего списка
        /// </summary>
        /// <param name="screenSpaceController"></param>
        public MedicKitFactory(ScreenSpaceController screenSpaceController)
        {
            this.screenSpaceController = screenSpaceController;

            if (medicKitImages.Count > 0)
            {
                image = medicKitImages[Game.randomizer.Next(0, medicKitImages.Count)];
            }
        }

        public static void Init(string path)
        {
            medicKitImages = ImagesLoad(path);
        }

        /// <summary>
        /// Аптечка создается в любой точке игрового поля вне зависимости от присутствия там другого объекта
        /// </summary>
        /// <returns></returns>
        public override SpaceObject Create()
        {
            size = Game.randomizer.Next(minMedicKitSize, maxMedicKitSize);

            Point? legalPoint = screenSpaceController.GetLegalPoint(size, SpawnType.AnywhereOnscreen);
            if (legalPoint is null)
            {
                return null;
            }
            return new MedicKit((Point)legalPoint, new Point(), new Size(size, size), image);
        }
    }
}
