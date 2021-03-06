﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Media;
using Brushes = System.Drawing.Brushes;
using Color = System.Drawing.Color;

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

        private static bool isActive;
        private static Starship player;
        private static BufferedGraphicsContext context;
        private static Timer updateTimer;
        private static Timer spawnTimer;
        private static int width;
        private static int height;
        private static int score;
        private static int gameSpeed = 100;
        private static List<SpaceObject> spaceObjects;
        private static List<Bullet> bullets;
        private static ScreenSpaceController screenSpaceController;
        private static Overlay overlay;
        private static MediaPlayer asteroidHitPlayer;
        private static MediaPlayer medicKitPlayer;
        private static MediaPlayer bulletHitPlayer;
        private static MediaPlayer musicPlayer;
        public static readonly string resourcesPath = Directory.GetParent(Directory.GetCurrentDirectory()).Parent?.Parent?.FullName;

        private const int updateRate=100;         //Интервал срабатывания обновления состояния игры
        private const int spawnInterval=1000;     //Интервал создания новой партии астероидов
        private const int spawnCount = 5;         //Количество астероидов в партии

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

            form.BackColor = Color.Black;
            form.KeyDown += Form_KeyDown;

            asteroidHitPlayer=new MediaPlayer();
            asteroidHitPlayer.Open(new Uri(Path.Combine(resourcesPath,@"Resources\Sound\ShipExplosion.wav"), UriKind.Relative));
            bulletHitPlayer = new MediaPlayer();
            bulletHitPlayer.Open(new Uri(Path.Combine(resourcesPath, @"Resources\Sound\AsretoidExplosion.wav"), UriKind.Relative));
            musicPlayer = new MediaPlayer();
            musicPlayer.Open(new Uri(Path.Combine(resourcesPath, @"Resources\Sound\Music.wav"), UriKind.Relative));
            medicKitPlayer = new MediaPlayer();
            medicKitPlayer.Open(new Uri(Path.Combine(resourcesPath, @"Resources\Sound\Heal.wav"), UriKind.Relative));
            musicPlayer.MediaEnded += Media_Ended;

            overlay = new Overlay(form, ChangeSpeed, () => musicPlayer?.Stop());

            return true;
        }

        /// <summary>
        /// Управление с помощью клавиатуры
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
                case Keys.Escape:
                    Pause();
                    break;
            }
        }

        /// <summary>
        /// Изменение скорости игры
        /// </summary>
        /// <param name="change"></param>
        private static void ChangeSpeed(int change)
        {
            gameSpeed += change;

            if (updateTimer!=null)
            {
                updateTimer.Interval = Convert.ToInt32(updateRate / ((double)gameSpeed / 100));
            }

            if (spawnTimer != null)
            {
                spawnTimer.Interval = Convert.ToInt32(spawnInterval / ((double)gameSpeed / 100));
            }
        }

        /// <summary>
        /// Регуляция уровня звуковых эффектов
        /// </summary>
        /// <param name="volume"></param>
        public static void SetSoundVolumeLevel(double volume)
        {
            asteroidHitPlayer.Volume = volume;
            bulletHitPlayer.Volume = volume;
        }

        /// <summary>
        /// Регуляция уровня музыки
        /// </summary>
        /// <param name="volume"></param>
        public static void SetMusicVolumeLevel(double volume)
        {
            musicPlayer.Volume = volume;
        }

        /// <summary>
        /// Повторное воспроизведение композиции
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void Media_Ended(object sender, EventArgs e)
        {
            musicPlayer.Position = TimeSpan.Zero;
        }

        #endregion

        #region Start

        /// <summary>
        /// Инициализация Фабрик и звуковой составляющей
        /// </summary>
        public static void Init(GameMode gameMode)
        {
            score = 0;

            screenSpaceController = new ScreenSpaceController(SpawnType.OnScreen);

            AsteroidFactory.Init(Path.Combine(resourcesPath, @"Resources\Asteroids"));
            MedicKitFactory.Init(Path.Combine(resourcesPath, @"Resources\MedicKits"));

            string shipsPath=string.Empty;
            string bulletsPath= string.Empty;

            switch (gameMode)
            {
                case GameMode.Matches:
                    shipsPath = Path.Combine(resourcesPath, @"Resources\Ships\Matchboxes");
                    bulletsPath = Path.Combine(resourcesPath, @"Resources\Bullets\Matches");
                    break;
                case GameMode.Starships:
                    shipsPath = Path.Combine(resourcesPath, @"Resources\Ships\Starships");
                    bulletsPath = Path.Combine(resourcesPath, @"Resources\Bullets\Shells");
                    break;
            }
            StarshipFactory.Init(shipsPath);
            BulletFactory.Init(bulletsPath);

            Start();
        }

        /// <summary>
        /// Создание обьектов иерархии SpaceObject
        /// </summary>
        public static void Start()
        {
            player = (Starship) new StarshipFactory().Create();
            overlay.CreateHpBar();

            List<Image> imageList = SpaceObjectFactory.ImagesLoad(Path.Combine(resourcesPath, @"Resources\Stars"));

            int iMax = imageList.Count;
            imageList.AddRange(SpaceObjectFactory.ImagesLoad(Path.Combine(resourcesPath, @"Resources\StaticObjects")));
            if (imageList.Count == 0)
            {
                throw new NullReferenceException("Файлы повреждены или отсутсвуют");
            }

            bullets = new List<Bullet>();
            spaceObjects = new List<SpaceObject>();
            try
            {
                for (int i = 0; i < iMax; i++)
                {
                    spaceObjects.Add(new StarFactory(screenSpaceController, imageList[i]).Create());
                }

                for (int i = iMax; i < imageList.Count; i++)
                    spaceObjects.Add(new StaticObjectFactory(screenSpaceController, imageList[i]).Create());

                RemoveNullsOrDisposed(spaceObjects);
            }
            catch (GameObjectException)
            {

            }

            Form.ActiveForm?.Focus();

            updateTimer = new Timer { Interval = Convert.ToInt32(updateRate / ((double)gameSpeed / 100)) };
            updateTimer.Start();
            updateTimer.Tick += Timer_Tick;

            spawnTimer = new Timer {Interval = Convert.ToInt32(spawnInterval / ((double) gameSpeed / 100))};
            spawnTimer.Start();
            spawnTimer.Tick += SpawnTimer_Tick;

            musicPlayer.Play();
            isActive = true;
        }

        /// <summary>
        /// Отображение игровых обьектов и обновление их состояний
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void Timer_Tick(object sender, EventArgs e)
        {
            Draw();
            Update();
        }

        /// <summary>
        /// Добавление в коллекцию космических обьектов аптечек и астероидов
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void SpawnTimer_Tick(object sender, EventArgs e)
        {
            MedicKitHandle();

            ScreenSpaceController asteroidSpawnController = new ScreenSpaceController(SpawnType.OutOfScreen);
            for (int i = 0; i < spawnCount; i++)
            {
                spaceObjects.Add(new AsteroidFactory(asteroidSpawnController).Create());
            }

            RemoveNullsOrDisposed(spaceObjects);
        }

        /// <summary>
        /// Создает аптечку на игровом поле при отсутствии таковых 
        /// </summary>
        private static void MedicKitHandle()
        {
            int indexOfLastColliderless = spaceObjects.FindLastIndex(x=>x!=null && !x.HasCollider);
            if (indexOfLastColliderless == spaceObjects.Count-1)
            {
                spaceObjects.Add(new MedicKitFactory(screenSpaceController).Create());
            }

            if (spaceObjects[indexOfLastColliderless+1] is Asteroid)
            {
                spaceObjects.Insert(indexOfLastColliderless+1, new MedicKitFactory(screenSpaceController).Create());
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

            if (isActive)
            {
                Buffer.Graphics.DrawString($"Score {score}", new Font("Franklin Gothic Medium", 30F, FontStyle.Bold,
                    GraphicsUnit.Point, ((byte)(204))), Brushes.White, new RectangleF(500, 0, 300, 50));
            }

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
                    if (spaceObjects[i] != null && spaceObjects[i].HasCollider && bullets != null && player != null)
                    {
                        if (CheckCollision(spaceObjects[i]))
                        {
                            if (spaceObjects[i].GetType()!=typeof(MedicKit))
                            {
                                score += spaceObjects[i].Power;
                            }
                            
                            spaceObjects[i].Dispose();
                        }
                    }
                }

                RemoveNullsOrDisposed(spaceObjects);
                RemoveNullsOrDisposed(bullets);
            }
        }

        /// <summary>
        /// Проверка столкновений SpaceObjects
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        private static bool CheckCollision(SpaceObject obj)
        {
            if (player.Collide(obj))
            {
                if (obj is MedicKit)
                {
                    medicKitPlayer.Stop();
                    medicKitPlayer.Play();
                }

                if (obj is Asteroid)
                {
                    asteroidHitPlayer.Stop();
                    asteroidHitPlayer.Play();
                }
                
                player.GetDamageOrHeal(obj);
                return true;
            }

            for (int i = 0; i < bullets.Count; i++)
            {
                if (bullets[i] != null && bullets[i].Collide(obj))
                {
                    bulletHitPlayer.Stop();
                    bulletHitPlayer.Play();

                    bullets[i].Dispose();
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Удаляет лишние элементы из коллекции spaceObjects
        /// </summary>
        private static void RemoveNullsOrDisposed<T>(List<T> objects) where T: SpaceObject
        {
            for (int i = 0; i < objects.Count; i++)
            {
                if (objects[i] is null || objects[i].Disposed)
                {
                    objects.RemoveAt(i--);
                }
            }
        }

        /// <summary>
        /// Пауза игры
        /// </summary>
        private static void Pause()
        {
            updateTimer.Stop();
            spawnTimer.Stop();
            overlay.DisplayPauseMenu();
        }

        /// <summary>
        /// Возобновление игры
        /// </summary>
        public static void Resume()
        {
            updateTimer.Start();
            spawnTimer.Start();
            Form.ActiveForm?.Focus();
        }

        #endregion

        #region EndOfTheGame

        /// <summary>
        /// Окончание игры
        /// </summary>
        public static void GameOver()
        {
            updateTimer.Stop();
            spawnTimer.Stop();
            overlay.UpdateHpBar(player?.HpBarPoint, player?.Hitpoints);
            isActive = false;

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

        #endregion
    }
}

