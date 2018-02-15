using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homework
{ 
    abstract class SpaceObjectFactory
    {
        protected static Random randomize = new Random();
        protected static Dictionary<Point, bool> freeSqreenSpace = 
            FillPointList(new Point(0,0), Game.Width, Game.Height).ToDictionary(x => x, x => true);

        protected int size;
        protected Image image;

        protected int maxIterationCount = 50;

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

        protected virtual Point GetLegalPoint()
        {
            List<Point> imagePoints=new List<Point>(size*size);
            Point leftTopImagePoint =new Point();
            int iterationCount = 0;

            do
            {
                leftTopImagePoint = new Point(randomize.Next(0, Game.Width - size), randomize.Next(0, Game.Height - size));
                imagePoints.AddRange(FillPointList(leftTopImagePoint,size, size));
                if (++iterationCount == maxIterationCount)
                {
                    throw new TimeoutException();
                }
            }
            while (!HasAvailableSpace(imagePoints));

            

            ReserveSpace(imagePoints);
            return leftTopImagePoint;
        }

        private bool HasAvailableSpace(List<Point> imagePoints)
        {
            if (freeSqreenSpace.Join(imagePoints,x=>x.Key,y=>y,(x,y)=>x.Value).Contains(false))
            {
                return false;
            }

            return true;
        }

        private void ReserveSpace(List<Point> imagePoints)
        {
            foreach (Point imagePoint in imagePoints)
            {
                freeSqreenSpace[imagePoint] = false;
            }
        }
    }
}
