using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Media;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
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

        public static Random randomizer = new Random();

        public static BufferedGraphics Buffer;

        private static Starship player;
        private static BufferedGraphicsContext context;
        private static Timer updateTimer = new Timer { Interval = updateRate };
        private static Timer spawnTimer = new Timer { Interval = spawnInterval };
        private static int width;
        private static int height;
        private static List<SpaceObject> spaceObjects;
        private static List<Bullet> bullets = new List<Bullet>();
        private static ScreenSpaceController screenSpaceController;
        private static Overlay overlay;
        private static SoundPlayer asteroidHitPlayer;
        private static SoundPlayer bulletHitPlayer;

        private const int updateRate = 100;         //Интервал срабатывания обновления состояния игры
        private const int spawnInterval=1000;     //Интервал создания новой партии астероидов
        private const int spawnCount = 5;           //Количество астероидов в партии

        // Свойства
        // Ширина и высота игрового поля
        public static int Width
        {
            get => width;
            set
            {
                if (value < 0 || value > 1000)
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
            
            updateTimer.Start();
            updateTimer.Tick += Timer_Tick;

            form.KeyDown += Form_KeyDown;

            overlay = new Overlay(form);

            return true;
        }

        private static void Timer_Tick(object sender, EventArgs e)
        {
            Draw();
            Update();
        }

        private static void Form_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.ControlKey:
                    bullets.Add((Bullet)new BulletFactory(player).Create());
                    break;
                case Keys.Up:
                    player.MoveUp();
                    break;
                case Keys.Down:
                    player.MoveDown();
                    break;
                case Keys.Left:
                    player.MoveLeft();
                    break;
                case Keys.Right:
                    player.MoveRight();
                    break;
            }
        }


        #endregion

        #region Start

        /// <summary>
        /// Инициализация Фабрик
        /// </summary>
        public static void Init(GameMode gameMode)
        {
            screenSpaceController = new ScreenSpaceController(SpawnType.OnScreen);

            AsteroidFactory.Init(@"Homework1\Asteroids");

            string shipsPath=string.Empty;
            string bulletsPath= string.Empty;

            switch (gameMode)
            {
                case GameMode.Matches:
                    shipsPath = @"Homework1\Ships\Matchboxes";
                    bulletsPath = @"Homework1\Bullets\Matches";
                    break;
                case GameMode.Starships:
                    shipsPath = @"Homework1\Ships\Starships";
                    bulletsPath = @"Homework1\Bullets\Shells";
                    break;
            }
            StarshipFactory.Init(shipsPath);
            BulletFactory.Init(bulletsPath);

            asteroidHitPlayer = new SoundPlayer(@"Homework1\ShipExplosion.wav");
            bulletHitPlayer = new SoundPlayer(@"Homework1\AsretoidExplosion.wav");
            
            Start();
        }

        /// <summary>
        /// Создание обьектов иерархии SpaceObject
        /// </summary>
        public static void Start()
        {
            player = (Starship)new StarshipFactory().Create();
            overlay.CreateHpBar();

            List<Image> imageList = SpaceObjectFactory.ImagesLoad(@"Homework1\Stars");

            int iMax = imageList.Count;
            imageList.AddRange(SpaceObjectFactory.ImagesLoad(@"Homework1\StaticObjects"));
            if (imageList.Count == 0)
            {
                throw new NullReferenceException("Файлы повреждены или отсутсвуют");
            }

            spaceObjects = new List<SpaceObject>();
            try
            {
                for (int i = 0; i < iMax; i++)
                {
                    spaceObjects.Add(new StarFactory(screenSpaceController, imageList[i]).Create());
                }

                for (int i = iMax; i < imageList.Count; i++)
                    spaceObjects.Add(new StaticObjectFactory(screenSpaceController, imageList[i]).Create());

            }
            catch (GameObjectException)
            {

            }

            Form.ActiveForm?.Focus();

            spawnTimer.Start();
            spawnTimer.Tick += SpawnTimer_Tick;
        }

        /// <summary>
        /// Добавление в коллекцию космических обьектов партии астероидов
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void SpawnTimer_Tick(object sender, EventArgs e)
        {
            ScreenSpaceController asteroidSpawnController = new ScreenSpaceController(SpawnType.OutOfScreen);
            for (int i = 0; i < spawnCount; i++)
            {
                spaceObjects.Add(new AsteroidFactory(asteroidSpawnController).Create());
            }
        }

        #endregion

        #region Update every 100ms

        /// <summary>
        /// Отображает обьекты на игровом поле, в обновленном состоянии
        /// </summary>
        public static void Draw()
        {
            Buffer.Graphics.Clear(Color.Black);

            if (bullets != null)
            {
                foreach (Bullet obj in bullets)
                    obj?.Draw();
            }

            if (spaceObjects != null)
            {
                foreach (SpaceObject obj in spaceObjects)
                    obj?.Draw();
            }

            player?.Draw();

            try
            {
                Buffer.Render();
            }
            catch (ArgumentException)
            {
            }
        }

        /// <summary>
        /// Обновляет состояния SpaceObjects
        /// </summary>
        public static void Update()
        {
            player?.Update();
            overlay?.UpdateHpBar(player?.HpBarPoint,player?.Hitpoints);

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
                    if (spaceObjects[i] != null && spaceObjects[i].HasCollider && bullets != null && player!=null)
                    {
                        if (CheckCollision(spaceObjects[i]))
                        {
                            spaceObjects[i].Dispose();
                            spaceObjects.RemoveAt(i);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Проверка попадания снаряда по астероиду и их последующее взаимоуничтожение
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        private static bool CheckCollision(SpaceObject obj)
        {
            if (player.Collide(obj))
            {
                asteroidHitPlayer.Play();
                player.GetDamage((Asteroid)obj);
                return true;
            }

            for (int i = 0; i < bullets.Count; i++)
            {
                if (bullets[i] != null && bullets[i].Collide(obj))
                {
                    bulletHitPlayer.Play();

                    bullets[i].Dispose();
                    bullets.RemoveAt(i);
                    i--;
                    return true;
                }
            }

            return false;
        }

        #endregion

        public static void GameOver()
        {
            updateTimer.Stop();
            spawnTimer.Stop();

            overlay.UpdateHpBar(player?.HpBarPoint, player?.Hitpoints);
            Buffer.Graphics.DrawString("Game Over", new Font("Franklin Gothic Medium", 80F, FontStyle.Bold,
                GraphicsUnit.Point, ((byte)(204))), Brushes.Red, 100, 200);
            overlay.DisplayMainMenuButton();

            try
            {
                Buffer.Render();
            }
            catch (ArgumentException)
            {
            }
        }
    }
}

