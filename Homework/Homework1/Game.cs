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

        private static BufferedGraphicsContext context;
        public static BufferedGraphics Buffer;
        public static SpaceObject[] spaceObjects;

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
            List<string> imageList = Utility.GetFiles(@"Homework1\Stars", "*.jpeg|*.png").ToList();

            int iMax = imageList.Count;
            imageList.AddRange(Utility.GetFiles(@"Homework1\StaticObjects", "*.jpeg|*.png"));
            if (imageList.Count == 0)
            {
                throw new NullReferenceException("Файлы повреждены или отсутсвуют");
            }
            
            spaceObjects = new SpaceObject[imageList.Count];
            try
            {
                for (int i = 0; i < iMax; i++)
                {
                    spaceObjects[i] = new StarFactory(new Bitmap(imageList[i])).Create();
                }

                for (int i = iMax; i < spaceObjects.Length; i++)
                    spaceObjects[i] = new StaticObjectFactory(new Bitmap(imageList[i])).Create();

            }
            catch (TimeoutException)
            {
                MessageBox.Show("Too many SpaceObjects");
            }
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
                obj?.Draw();
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
                obj?.Update();
        }

        #endregion
    }
}

