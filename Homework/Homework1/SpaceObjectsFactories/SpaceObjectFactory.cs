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
        protected ScreenSpaceController screenSpaceController;

        protected int size;
        protected Image image;

        //protected const int maxIterationCount = 50;

        public SpaceObjectFactory(ScreenSpaceController screenSpaceController, Image image)
        {
            this.screenSpaceController = screenSpaceController;
            this.image = image;
        }

        public abstract SpaceObject Create();

    }
}
