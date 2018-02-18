using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Homework
{
    static class Game
    {
        #region Vars and Props

        public static Random randomizer=new Random();

        private static BufferedGraphicsContext context;
        public static BufferedGraphics Buffer;

        private static int width;
        private static int height;
        private static List<Bullet> bullets;
        private static List<SpaceObject> spaceObjects;
        private static ScreenSpaceController screenSpaceController;
        private static Overlay overlay;

        // Свойства
        // Ширина и высота игрового поля
        public static int Width
        {
            get=> width;
            set
            {
                if (value<0 || value>1000)
                {
                    throw new ArgumentOutOfRangeException($"Недопустимое значение ширины игрового поля {nameof(Form.Width)}");
                }
                width = value;
            }
        }
        public static int Height
        {
            get => height;
            set
            {
                if (value < 0 || value > 1000)
                {
                    throw new ArgumentOutOfRangeException($"Недопустимое значение ширины игрового поля {nameof(Form.Height)}");
                }
                height = value;
            }
        }


        #endregion

        #region Initialization

        static Game()
        {
        }

        public static bool Awake(Form form)
        {
            // Запоминаем размеры формы
            try
            {
                Width = form.Width;
                Height = form.Height;
            }
            catch (ArgumentOutOfRangeException e)
            {
                MessageBox.Show(e.Message);
                form.Close();
                return false;
            }
           
            // Графическое устройство для вывода графики

            // предоставляет доступ к главному буферу графического контекста для текущего приложения
            context = BufferedGraphicsManager.Current;
            Graphics graphics = form.CreateGraphics();// Создаём объект - поверхность рисования и связываем его с формой
            // Связываем буфер в памяти с графическим объектом.
            // для того, чтобы рисовать в буфере
            Buffer = context.Allocate(graphics, new Rectangle(0, 0, Width, Height));
            Timer timer = new Timer { Interval = 100 };
            timer.Start();
            timer.Tick += Timer_Tick;

            overlay=new Overlay(form);

            return true;
        }

        private static void Timer_Tick(object sender, EventArgs e)
        {
            Draw();
            Update();
        }

        #endregion

        #region Start

        /// <summary>
        /// Создание обьектов иерархии SpaceObject
        /// </summary>
        public static void Start()
        {
            bullets=new List<Bullet>();
            screenSpaceController = new ScreenSpaceController();

            List<string> imageList = Utility.GetFiles(@"Homework1\Stars", "*.jpeg|*.png").ToList();

            int iMax = imageList.Count;
            imageList.AddRange(Utility.GetFiles(@"Homework1\StaticObjects", "*.jpeg|*.png"));
            if (imageList.Count == 0)
            {
                throw new NullReferenceException("Файлы повреждены или отсутсвуют");
            }

            spaceObjects = new List<SpaceObject>();
            try
            {
                spaceObjects.Add(new Star(new Point(23,44),new Point(-1,-4),new Size()  ));
                for (int i = 0; i < iMax; i++)
                {
                    spaceObjects.Add(new StarFactory(screenSpaceController, new Bitmap(imageList[i])).Create());
                }

                for (int i = iMax; i < imageList.Count; i++)
                    spaceObjects.Add(new StaticObjectFactory(screenSpaceController, new Bitmap(imageList[i])).Create());

            }
            catch (GameObjectException)
            {
                
            }
        }

        #endregion

        #region Update every 100ms

        public static void Draw()
        {
            Buffer.Graphics.Clear(Color.Black);

            if (spaceObjects !=null)
            {
                foreach (SpaceObject obj in spaceObjects)
                    obj?.Draw();
            }
            
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
            //Добавление астероидов

            if (bullets != null)
            {
                foreach (Bullet bullet in bullets)
                    bullet?.Update();
            }

            if (spaceObjects != null)
            {
                foreach (SpaceObject obj in spaceObjects)
                {
                    obj?.Update();
                    if (obj!=null && obj.HasCollider && bullets != null)
                    {
                        CheckCollision(obj);
                    }
                }
            }
        }

        private static void CheckCollision(SpaceObject obj)
        {
            for (int i = 0; i < bullets.Count; i++)
            {
                if (bullets[i]!=null && bullets[i].Collide(obj))
                {
                    System.Media.SystemSounds.Hand.Play();
                    bullets[i].Dispose();
                    bullets.RemoveAt(i);
                    i--;

                    obj.Relocate();
                    //Движение астероида
                }
            }
        }

        #endregion
    }
}

