using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Homework
{ 
    /// <summary>
    /// Иерархия фабрик для создания SpaceObjects
    /// </summary>
    abstract class SpaceObjectFactory
    {
        private static string searchPattern= "*.jpg|*.jpeg|*.png";

        protected ScreenSpaceController screenSpaceController;

        protected int size;
        protected Image image;

        //protected const int maxIterationCount = 50;

        public SpaceObjectFactory(ScreenSpaceController screenSpaceController, Image image)
        {
            this.screenSpaceController = screenSpaceController;
            this.image = image;
        }

        protected SpaceObjectFactory()
        {
            
        }

        /// <summary>
        /// Заполняет коллекцию изображений SpaceObhects, делая прозрачным цвет фона
        /// </summary>
        /// <returns></returns>
        public static List<Image> ImagesLoad(string path)
        {
            List<Image> images = new List<Image>();
            try
            {
                List<string> files = Utility.GetFiles(path, searchPattern);

                foreach (string file in files)
                {
                    Bitmap image = new Bitmap(file);
                    Color backgroundColor = image.GetPixel(1, 1);
                    image.MakeTransparent(backgroundColor);
                    images.Add(image);
                }
            }
            catch (FileNotFoundException e)
            {
                MessageBox.Show(e.Message);
            }

            return images;
        }

        public abstract SpaceObject Create();
    }
}
