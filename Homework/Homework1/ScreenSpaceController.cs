using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Homework
{
    /// <summary>
    /// Управляет доступом к игровому полю
    /// </summary>
    class ScreenSpaceController
    {
        /// <summary>
        /// Словарь содержащий все возможные точки на экране и их занятость обьектами иерархии SpaceObjects
        /// </summary>
        public Dictionary<Point, bool> freeSqreenSpace;

        private SpawnType spawnType;
        private static Random randomize = new Random();

        public ScreenSpaceController(SpawnType spawnType)
        {
            this.spawnType = spawnType;

            switch (this.spawnType)
            {
                case SpawnType.OnScreen:
                    freeSqreenSpace = FillPointList(new Point(0, 0), Game.Width, Game.Height).ToDictionary(x => x, x => true);
                    break;
                case SpawnType.OutOfScreen:
                    freeSqreenSpace = FillPointList(new Point(Game.Width, 0), 0, Game.Height).ToDictionary(x => x, x => true);
                    break;
            }
        }

        private static IEnumerable<Point> FillPointList(Point leftTopPoint, int width, int height)
        {
            for (int i = leftTopPoint.X; i <= leftTopPoint.X + width; i++)
            {
                for (int j = leftTopPoint.Y; j <= leftTopPoint.Y + height; j++)
                {
                    yield return new Point(i, j);
                }
            }
        }

        /// <summary>
        /// Создание точки для расположения изображения на экране
        /// </summary>
        /// <returns></returns>
        public virtual Point? GetLegalPoint(int size)
        {
            List<Point> imagePoints = new List<Point>(size * size);
            Point leftTopImagePoint=new Point(); 

            switch (spawnType)
            {
                case SpawnType.OnScreen:
                    leftTopImagePoint = new Point(randomize.Next(0, Game.Width - size), randomize.Next(0, Game.Height - size));
                    break;
                case SpawnType.OutOfScreen:
                    leftTopImagePoint = new Point(Game.Width, randomize.Next(0, Game.Height - size));
                    break;
            }

            imagePoints.AddRange(FillPointList(leftTopImagePoint, size, size));
            if (HasAvailableSpace(imagePoints))
            {
                ReserveSpace(imagePoints);
                return leftTopImagePoint;
            }

            return null;
        }

        /// <summary>
        /// Проверка точки на "занятость" другим обьектом
        /// </summary>
        /// <param name="imagePoints"></param>
        /// <returns></returns>
        private bool HasAvailableSpace(List<Point> imagePoints)
        {
            if (freeSqreenSpace.AsParallel().Join(imagePoints.AsParallel(), x => x.Key, y => y, (x, y) => x.Value).Contains(false))
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Бронирование пространства за обьектом
        /// </summary>
        /// <param name="imagePoints"></param>
        private void ReserveSpace(List<Point> imagePoints)
        {
            foreach (Point imagePoint in imagePoints)
            {
                freeSqreenSpace[imagePoint] = false;
            }
        }
    }
}
