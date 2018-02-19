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
    /// <summary>
    /// Управляет игровым процессом
    /// </summary>
    static class Game
    {
        #region Vars and Props

        public static Random randomizer=new Random();

        private static BufferedGraphicsContext context;
        public static BufferedGraphics Buffer;

        /// <summary>
        /// Счетчик срабатываний метода Update, 
        /// осуществляет замену конструкции с использованием дополнительных Timer'ов и методов Update
        /// </summary>
        private static int updateCount;

        private static int width;
        private static int height;
        private static List<Bullet> bullets;
        private static List<SpaceObject> spaceObjects;
        private static ScreenSpaceController screenSpaceController;
        private static Overlay overlay;

        private const int updateRate = 100;         //Интервал срабатывания обновления состояния игры
        private const int spawnInterval = 1000;     //Интервал создания новой партии астероидов
        private const int spawnCount = 5;           //Количество астероидов в партии

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
            Timer timer = new Timer { Interval = updateRate };
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
            screenSpaceController = new ScreenSpaceController(SpawnType.OnScreen);

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

            updateCount = 0;
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
            
            if (updateCount>=spawnInterval/updateRate)
            {
                SpawnAsteroids();

                updateCount = 0;
            }

            if (bullets != null)
            {
                foreach (Bullet bullet in bullets)
                    bullet?.Update();
            }

            if (spaceObjects != null)
            {
                for (int i = 0; i < spaceObjects.Count; i++)
                {
                    spaceObjects[i]?.Update();
                    if (spaceObjects[i] != null && spaceObjects[i].HasCollider && bullets != null)
                    {
                        i-=CheckCollision(spaceObjects[i]);
                    }
                }
            }

            updateCount++;
        }

        /// <summary>
        /// Добавление в коллекцию космических обьектов партии астероидов
        /// </summary>
        private static void SpawnAsteroids()
        {
            ScreenSpaceController asteroidSpawnController = new ScreenSpaceController(SpawnType.OutOfScreen);
            for (int i = 0; i < spawnCount; i++)
            {
                spaceObjects.Add(new AsteroidFactory(asteroidSpawnController).Create());
            }
        }

        /// <summary>
        /// Проверка попадания снаряда по астероиду и их последующее взаимоуничтожение
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        private static int CheckCollision(SpaceObject obj)
        {
            int collisions = 0;
            for (int i = 0; i < bullets.Count; i++)
            {
                if (bullets[i]!=null && bullets[i].Collide(obj))
                {
                    System.Media.SystemSounds.Hand.Play();
                    bullets[i].Dispose();
                    bullets.RemoveAt(i);
                    i--;

                    obj.Dispose();
                    spaceObjects.Remove(obj);
                    collisions++;
                    //obj.Relocate();
                    //Движение астероида
                }
            }

            return collisions;
        }

        #endregion
    }
}

