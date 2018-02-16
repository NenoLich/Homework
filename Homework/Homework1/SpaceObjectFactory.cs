using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homework
{ 
    /// <summary>
    /// Иерархия фабрик для создания SpaceObjects
    /// </summary>
    abstract class SpaceObjectFactory
    {
        protected static Random randomize = new Random();

        /// <summary>
        /// Словарь содержащий все возможные точки на экране и их занятость обьектами иерархии SpaceObjects
        /// </summary>
        protected static Dictionary<Point, bool> freeSqreenSpace = 
            FillPointList(new Point(0,0), Game.Width, Game.Height).ToDictionary(x => x, x => true);

        protected int size;
        protected Image image;

        //protected const int maxIterationCount = 50;

        public SpaceObjectFactory(Image image)
        {
            this.image = image;
        }

        private static IEnumerable<Point> FillPointList(Point leftTopPoint,int width, int height)
        {
            for (int i = leftTopPoint.X; i <= leftTopPoint.X + width; i++)
            {
                for (int j = leftTopPoint.Y; j <= leftTopPoint.Y + height; j++)
                {
                    yield return new Point(i, j);
                }
            }
        }

        public abstract SpaceObject Create();

        /*protected virtual Point? GetLegalPoint()
        {
            List<Point> imagePoints = new List<Point>(size * size);
            Point leftTopImagePoint = new Point();
            int iterationCount = 0;

            do
            {
                leftTopImagePoint = new Point(randomize.Next(0, Game.Width - size), randomize.Next(0, Game.Height - size));
                imagePoints.AddRange(FillPointList(leftTopImagePoint, size, size));
                if (++iterationCount == maxIterationCount)
                {
                    return null;
                }
            }
            while (!HasAvailableSpace(imagePoints));
            
            ReserveSpace(imagePoints);
            return leftTopImagePoint;
        }*/

        /// <summary>
        /// Создание точки для расположения изображения на экране
        /// </summary>
        /// <returns></returns>
        protected virtual Point? GetLegalPoint()
        {
            List<Point> imagePoints=new List<Point>(size*size);
            Point leftTopImagePoint = new Point(randomize.Next(0, Game.Width - size), randomize.Next(0, Game.Height - size));

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
            if (freeSqreenSpace.AsParallel().Join(imagePoints.AsParallel(),x=>x.Key,y=>y,(x,y)=>x.Value).Contains(false))
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
