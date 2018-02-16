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
            // Запоминаем размеры формы
            
            Width = form.Width;
            Height = form.Height;
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

            CreateMenu(form);
        }

        private static void Timer_Tick(object sender, EventArgs e)
        {
            Draw();
            Update();
        }

        #endregion

        /// <summary>
        /// Создание панели Меню с кнопками: "New Game", "Records", "Exit"
        /// </summary>
        /// <param name="form"></param>
        #region CreateMenu

        private static void CreateMenu(Form form)
        {
            Panel menu = new Panel();
            menu.Size = new Size(300, 400);
            menu.BorderStyle = BorderStyle.Fixed3D;

            Label mainMenuLabel = new Label
            {
                AutoSize = true,
                Font = new Font("Franklin Gothic Medium", 12F, System.Drawing.FontStyle.Regular,
                    GraphicsUnit.Point, ((byte) (204))),
                Location = new Point(105, 55),
                Size = new Size(140, 40),
                Text = "Main Menu"
            };
            menu.Controls.Add(mainMenuLabel);

            Button newGame_button = new Button
            {
                Font = new Font("Franklin Gothic Medium", 12F, System.Drawing.FontStyle.Regular,
                    GraphicsUnit.Point, ((byte)(204))),
                Location = new Point(80, 130),
                Size = new Size(140, 40),
                Text = "New Game",
                UseVisualStyleBackColor = true
            };
            newGame_button.Click += newGame_button_Click;
            menu.Controls.Add(newGame_button);

            Button records_button = new Button
            {
                Font = new Font("Franklin Gothic Medium", 12F, System.Drawing.FontStyle.Regular,
                    GraphicsUnit.Point, ((byte)(204))),
                Location = new Point(80, 210),
                Size = new Size(140, 40),
                Text = "Records",
                UseVisualStyleBackColor = true
            };
            records_button.Click += records_button_Click;
            menu.Controls.Add(records_button);

            Button exit_button = new Button
            {
                Font = new Font("Franklin Gothic Medium", 12F, System.Drawing.FontStyle.Regular,
                    GraphicsUnit.Point, ((byte)(204))),
                Location = new Point(80, 290),
                Size = new Size(140, 40),
                Text = "Exit",
                UseVisualStyleBackColor = true
            };
            exit_button.Click += exit_button_Click;
            menu.Controls.Add(exit_button);

            menu.Location=new Point(250,100);
            form.Controls.Add(menu);
            form.AcceptButton = newGame_button;
            form.CancelButton = exit_button;
        }

        private static void exit_button_Click(object sender, EventArgs e)
        {
            Button button = sender as Button;
            button.FindForm().Close();
        }

        private static void records_button_Click(object sender, EventArgs e)
        {
            
        }

        private static void newGame_button_Click(object sender, EventArgs e)
        {
            Button button = sender as Button;
            button.Parent.Visible = false;
            Load();
        }

        #endregion

        #region Start

        /// <summary>
        /// Создание обьектов иерархии SpaceObject
        /// </summary>
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
            if (spaceObjects != null)
            {
                foreach (SpaceObject obj in spaceObjects)
                    obj?.Update();
            }
        }

        #endregion
    }
}

