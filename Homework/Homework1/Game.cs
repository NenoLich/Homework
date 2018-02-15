using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Homework
{
    static class Game
    {
        #region Vars and Props

        private static Random randomize = new Random();
        private static BufferedGraphicsContext context;
        public static BufferedGraphics Buffer;
        public static SpaceObject[] spaceObjects;

        private const int minStaticObjectSize = 25;
        private const int maxStaticObjectSize = 100;
        private const int formWidth = 800;
        private const int formHeight = 600;
        private const int starXmaxDirection = 40;

        private static readonly Point starSize = new Point(10,10);

        // Свойства
        // Ширина и высота игрового поля
        public static int Width { get; set; }
        public static int Height { get; set; }

        #endregion

        #region Initialization

        static Game()
        {
        }
        public static void Init(Form form)
        {
            // Графическое устройство для вывода графики

            // предоставляет доступ к главному буферу графического контекста для текущего приложения
            context = BufferedGraphicsManager.Current;
            Graphics graphics = form.CreateGraphics();// Создаём объект - поверхность рисования и связываем его с формой
            // Запоминаем размеры формы
            Width = form.Width;
            Height = form.Height;
            // Связываем буфер в памяти с графическим объектом.
            // для того, чтобы рисовать в буфере
            Buffer = context.Allocate(graphics, new Rectangle(0, 0, Width, Height));
            Load();
            Timer timer = new Timer { Interval = 100 };
            timer.Start();
            timer.Tick += Timer_Tick;
        }

        private static void Timer_Tick(object sender, EventArgs e)
        {
            Draw();
            Update();
        }

        public static void Load()
        {
            List<string> imageList = Utility.GetFiles(@"Homework1\Static objects", "*.jpeg|*.png").ToList();

            int iMax = imageList.Count;
            imageList.AddRange(Utility.GetFiles(@"Homework1\Stars", "*.jpeg|*.png"));
            if (imageList.Count == 0)
            {
                throw new NullReferenceException("Файлы повреждены или отсутсвуют");
            }

            int size;
            spaceObjects = new SpaceObject[imageList.Count];
            for (int i = 0; i < iMax; i++)
            {
                size = randomize.Next(minStaticObjectSize, maxStaticObjectSize);
                spaceObjects[i] = new SpaceObject(new Point(randomize.Next(0, formWidth), randomize.Next(0, formHeight)), new Point(), new Size(size, size), new Bitmap(imageList[i]));
            }

            for (int i = iMax; i < spaceObjects.Length; i++)
                spaceObjects[i] = new Star(new Point(randomize.Next(0, formWidth), randomize.Next(0, formHeight)), new Point(randomize.Next(0, starXmaxDirection), 0), new Size(starSize), new Bitmap(imageList[i]));
        }

        #endregion

        #region Update every 100ms

        public static void Draw()
        {
            // Проверяем вывод графики
            Buffer.Graphics.Clear(Color.Black);
            //Buffer.Graphics.DrawRectangle(Pens.White, new Rectangle(100, 100, 200, 200));
            //Buffer.Graphics.FillEllipse(Brushes.Wheat, new Rectangle(100, 100, 200, 200));
            //Buffer.Render();
            //Buffer.Graphics.Clear(Color.Black);
            foreach (SpaceObject obj in spaceObjects)
                obj.Draw();
            try
            {
                Buffer.Render();
            }
            catch (ArgumentException)
            {
            }
        }
        public static void Update()
        {
            foreach (SpaceObject obj in spaceObjects)
                obj.Update();
        }

        #endregion
    }
}

