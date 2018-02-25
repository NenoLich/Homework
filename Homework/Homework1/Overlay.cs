using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Homework
{
    /// <summary>
    /// Класс, отвечающий за UI
    /// </summary>
    class Overlay
    {
        #region Vars and Ctors

        private Form form;
        private Panel mainMenu;
        private Panel gameModeMenu;
        private Panel optionsMenu;
        private Panel gameOptionsMenu;
        private Panel soundOptionsMenu;
        private ProgressBar hpBar;
        private Button mainMenuButton;
        private Action MusicStop;
        private Action<int> ChangeSpeed;
        private Button previousButton;
        private Button nextButton;
        private Button newGame_button;
        private Button resumeGame_button;
        
        private int gameSpeed=100;
        private int speedStep = minStep;

        private const int maxSpeed = 1000;
        private const int minStep = 10;
        private const int maxStep = 50;
        private const string matchboxPath = @"Homework1\Ships\Matchboxes\Matchbox.jpg";
        private const string starshipPath = @"Homework1\Ships\Starships\Starship.jpg";
        private const string previousButtonPath = @"Homework1\Buttons\Previous.jpg";
        private const string nextButtonPath = @"Homework1\Buttons\Next.jpg";

        public Overlay(Form form, Action<int> changeSpeed, Action musicStop)
        {
            this.form = form;
            CreateMainMenu();
            CreatPauseMenu();
            CreateGameModeMenu();
            CreateOptionsMenu();
            CreateGameOptionsMenu();
            CreateSoundOptionsMenu();
            CreateMainMenuButton();
            MusicStop = musicStop;
            ChangeSpeed = changeSpeed;
        }

        #endregion

        #region Templates

        /// <summary>
        /// Создание шаблона панели
        /// </summary>
        /// <returns></returns>
        private Panel CreatePanelTemplate()
        {
            return new Panel
            {
                Size = new Size(300, 400),
                BorderStyle = BorderStyle.Fixed3D,
                BackColor=Color.Lavender
            };
        }

        /// <summary>
        /// Создание шаблона заглавной метки
        /// </summary>
        /// <returns></returns>
        private Label CreateLabelTemplate()
        {
            return new Label
            {
                AutoSize = true,
                Font = new Font("Franklin Gothic Medium", 12F, FontStyle.Regular,
                    GraphicsUnit.Point, ((byte)(204))),
                Location = new Point(105, 55),
                Size = new Size(140, 40),
            };
        }

        private Button CreateButtonTemplate()
        {
            return new Button
            {
                Font = new Font("Franklin Gothic Medium", 12F, FontStyle.Regular,
                    GraphicsUnit.Point, ((byte)(204))),
                Size = new Size(140, 40),
                UseVisualStyleBackColor = true
            };
        }

        private RadioButton CreateRadioButtonTemplate()
        {
            return new RadioButton
            {
                AutoSize = true,
                Font = new Font("Franklin Gothic Medium", 12F, FontStyle.Regular,
                    GraphicsUnit.Point, ((byte)(204))),
                Size = new Size(98, 25),
                TextImageRelation = TextImageRelation.ImageBeforeText,
                UseVisualStyleBackColor = true
            };
        }

        private TrackBar CreateTrackBarTemplate()
        {
            return new TrackBar
            {
                AutoSize = false,
                Value = 10,
                Size = new Size(140, 25),
            };
        }

        #endregion

        /// <summary>
        /// Создание главного меню с кнопками "New Game", "Options", "Exit".
        /// </summary>
        #region MainMenu


        private void CreateMainMenu()
        {
            mainMenu = CreatePanelTemplate();

            Label mainMenuLabel = CreateLabelTemplate();
            mainMenuLabel.Text = "Main Menu";

            mainMenu.Controls.Add(mainMenuLabel);

            newGame_button = CreateButtonTemplate();
            newGame_button.Location = new Point(80, 130);
            newGame_button.Text = "New Game";

            newGame_button.Click += newGame_button_Click;
            mainMenu.Controls.Add(newGame_button);

            Button options_button = CreateButtonTemplate();
            options_button.Location = new Point(80, 210);
            options_button.Text = "Options";

            options_button.Click += options_button_Click;
            mainMenu.Controls.Add(options_button);

            Button exit_button = CreateButtonTemplate();
            exit_button.Location = new Point(80, 290);
            exit_button.Text = "Exit";

            exit_button.Click += exit_button_Click;
            mainMenu.Controls.Add(exit_button);

            mainMenu.Location = new Point(250, 100);
            form.Controls.Add(mainMenu);
            form.AcceptButton = newGame_button;
            form.CancelButton = exit_button;
        }

        private void exit_button_Click(object sender, EventArgs e)
        {
            Button button = sender as Button;
            button.FindForm().Close();
        }

        private void options_button_Click(object sender, EventArgs e)
        {
            ParentPanelHide(sender);
            optionsMenu.Visible = true;
        }

        private void newGame_button_Click(object sender, EventArgs e)
        {
            ParentPanelHide(sender);
            gameModeMenu.Visible = true;
        }

        private void ParentPanelHide(object sender)
        {
            Button button = sender as Button;
            button.Parent.Visible = false;
        }

        #endregion

        /// <summary>
        /// Создание меню паузы с кнопками "Resume Game", "Options", "Exit".
        /// </summary>
        #region PauseMenu

        private void CreatPauseMenu()
        {
            resumeGame_button = CreateButtonTemplate();
            resumeGame_button.Location = new Point(80, 130);
            resumeGame_button.Text = "Resume Game";
            resumeGame_button.Visible = false;

            resumeGame_button.Click += resumeGame_button_Click;
            mainMenu.Controls.Add(resumeGame_button);
        }

        private void resumeGame_button_Click(object sender, EventArgs e)
        {
            ParentPanelHide(sender);
            resumeGame_button.Visible = false;
            newGame_button.Visible = true;

            hpBar.Visible = true;
            Game.Resume();
        }

        public void DisplayPauseMenu()
        {
            hpBar.Visible = false;
            resumeGame_button.Visible = true;
            newGame_button.Visible = false;
            mainMenu.Visible = true;
        }

        #endregion

        /// <summary>
        /// Создание меню выбора режима игры
        /// </summary>
        #region GameModeMenu

        private void CreateGameModeMenu()
        {
            gameModeMenu = CreatePanelTemplate();
            gameModeMenu.Visible = false;
            Label gameModeLabel = CreateLabelTemplate();
            gameModeLabel.Text = "Game Mode";
            gameModeMenu.Controls.Add(gameModeLabel);

            RadioButton starshipRadioButton = CreateRadioButtonTemplate();
            starshipRadioButton.Checked = false;
            starshipRadioButton.Image = new Bitmap(Image.FromFile(starshipPath), starshipRadioButton.Font.Height, starshipRadioButton.Font.Height);
            starshipRadioButton.Location = new Point(100, 112);
            starshipRadioButton.Name = "starshipRadioButton";
            starshipRadioButton.Text = "Starships";

            gameModeMenu.Controls.Add(starshipRadioButton);

            RadioButton matchboxRadioButton = CreateRadioButtonTemplate();
            matchboxRadioButton.Checked = true;
            matchboxRadioButton.Image = new Bitmap(Image.FromFile(matchboxPath), matchboxRadioButton.Font.Height, matchboxRadioButton.Font.Height);
            matchboxRadioButton.Location = new Point(100, 155);
            matchboxRadioButton.Name = "matchboxRadioButton";
            matchboxRadioButton.Text = "Matches";

            gameModeMenu.Controls.Add(matchboxRadioButton);

            Button start_button = CreateButtonTemplate();
            start_button.Location = new Point(80, 216);
            start_button.Text = "Start";

            start_button.Click += start_button_Click;
            gameModeMenu.Controls.Add(start_button);

            Button back_button = CreateButtonTemplate();
            back_button.Location = new Point(80, 290);
            back_button.Text = "Back";

            back_button.Click += back_button_Click;
            gameModeMenu.Controls.Add(back_button);

            gameModeMenu.Location = new Point(250, 100);
            form.Controls.Add(gameModeMenu);
        }

        private void start_button_Click(object sender, EventArgs e)
        {
            ParentPanelHide(sender);

            Game.Init(((RadioButton)gameModeMenu.Controls.Find("matchboxRadioButton", false).First()).Checked ?
                 GameMode.Matches : GameMode.Starships);
        }

        #endregion

        /// <summary>
        /// Создание меню опций с кнопками "Game Options", "Sound Options", "Back".
        /// </summary>
        #region OptionsMenu

        private void CreateOptionsMenu()
        {
            optionsMenu = CreatePanelTemplate();
            optionsMenu.Visible = false;

            Label optionsMenuLabel = CreateLabelTemplate();
            optionsMenuLabel.Text = "Options:";

            optionsMenu.Controls.Add(optionsMenuLabel);

            Button gameOptions_button = CreateButtonTemplate();
            gameOptions_button.Location = new Point(80, 130);
            gameOptions_button.Text = "Game Options";

            gameOptions_button.Click += gameOptions_button_Click;
            optionsMenu.Controls.Add(gameOptions_button);

            Button soundOptions_button = CreateButtonTemplate();
            soundOptions_button.Location = new Point(80, 210);
            soundOptions_button.Text = "Sound Options";

            soundOptions_button.Click += soundOptions_button_Click;
            optionsMenu.Controls.Add(soundOptions_button);

            Button back_button = CreateButtonTemplate();
            back_button.Location = new Point(80, 290);
            back_button.Text = "Back";

            back_button.Click += back_button_Click;
            optionsMenu.Controls.Add(back_button);

            optionsMenu.Location = new Point(250, 100);
            form.Controls.Add(optionsMenu);
        }

        private void soundOptions_button_Click(object sender, EventArgs e)
        {
            ParentPanelHide(sender);
            soundOptionsMenu.Visible = true;
        }

        private void gameOptions_button_Click(object sender, EventArgs e)
        {
            ParentPanelHide(sender);
            gameOptionsMenu.Visible = true;
        }

        #endregion

        /// <summary>
        /// Меню игровых опций
        /// </summary>
        #region GameOptionsMenu

        private void CreateGameOptionsMenu()
        {
            gameOptionsMenu = CreatePanelTemplate();
            gameOptionsMenu.Visible = false;
            Label gameOptionsLabel = CreateLabelTemplate();
            gameOptionsLabel.Text = "Game Options:";
            gameOptionsLabel.Location = new Point(90, 55);
            gameOptionsMenu.Controls.Add(gameOptionsLabel);

            CreateSpeedOptions();

            Button back_button = CreateButtonTemplate();
            back_button.Location = new Point(80, 290);
            back_button.Text = "Back";

            back_button.Click += back_button_Click;
            gameOptionsMenu.Controls.Add(back_button);

            gameOptionsMenu.Location = new Point(250, 100);
            form.Controls.Add(gameOptionsMenu);
        }

        private void CreateSpeedOptions()
        {
            Label speedLabel = new Label
            {
                AutoSize = true,
                Font = new Font("Franklin Gothic Medium", 9.75F, FontStyle.Regular,
                    GraphicsUnit.Point, ((byte)(204))),
                Location = new Point(80, 175),
                Size = new Size(46, 17),
                Text = "Speed:"
            };
            gameOptionsMenu.Controls.Add(speedLabel);

            previousButton = new Button
            {
                Image = new Bitmap(Image.FromFile(previousButtonPath), 20, 20),
                Location = new Point(140, 173),
                Size = new Size(20, 20),
                UseVisualStyleBackColor = true
            };

            previousButton.Click += previousButton_Click;
            gameOptionsMenu.Controls.Add(previousButton);

            TextBox speedTextBox = new TextBox
            {
                Font = new Font("Franklin Gothic Medium", 9.75F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(204))),
                Location = new Point(160, 172),
                ReadOnly = true,
                Name= "speedTextBox",
                Size = new Size(40, 22),
                Text = $"{gameSpeed}%",
                TextAlign = HorizontalAlignment.Center
            };

            gameOptionsMenu.Controls.Add(speedTextBox);

            nextButton = new Button
            {
                Image = new Bitmap(Image.FromFile(nextButtonPath), 20, 20),
                Location = new Point(200, 173),
                Size = new Size(20, 20),
                UseVisualStyleBackColor = true
            };

            nextButton.Click += nextButton_Click;
            gameOptionsMenu.Controls.Add(nextButton);
        }

        private void previousButton_Click(object sender, EventArgs e)
        {
            ChangeSpeed(-speedStep);
            gameSpeed -= speedStep;
            UpdateSpeedTextBox();
            nextButton.Enabled = true;
            speedStep = gameSpeed < 200 ? minStep : maxStep;

            if (gameSpeed<=speedStep)
            {
                previousButton.Enabled=false;
            }
        }

        private void nextButton_Click(object sender, EventArgs e)
        {
            ChangeSpeed(speedStep);
            gameSpeed += speedStep;
            UpdateSpeedTextBox();
            previousButton.Enabled = true;
            speedStep = gameSpeed < 200 ? minStep : maxStep;
            
            if (gameSpeed > maxSpeed-speedStep)
            {
                nextButton.Enabled = false;
            }
        }

        private void UpdateSpeedTextBox()
        {
            ((TextBox) gameOptionsMenu.Controls.Find("speedTextBox", false).First()).Text = $"{gameSpeed}%";
        }

        #endregion

        /// <summary>
        /// Меню звуковых опций
        /// </summary>
        #region SoundOptionsMenu

        private void CreateSoundOptionsMenu()
        {
            soundOptionsMenu = CreatePanelTemplate();
            soundOptionsMenu.Visible = false;
            Label optionsLabel = CreateLabelTemplate();
            optionsLabel.Text = "Sound Options:";
            optionsLabel.Location = new Point(90, 55);
            soundOptionsMenu.Controls.Add(optionsLabel);

            CreateSoundOptions();

            Button back_button = CreateButtonTemplate();
            back_button.Location = new Point(80, 290);
            back_button.Text = "Back";

            back_button.Click += back_button_Click;
            soundOptionsMenu.Controls.Add(back_button);

            soundOptionsMenu.Location = new Point(250, 100);
            form.Controls.Add(soundOptionsMenu);
        }

        private void CreateSoundOptions()
        {
            Label soundVolumeLabel = new Label
            {
                AutoSize = true,
                Font = new Font("Franklin Gothic Medium", 9.75F, FontStyle.Regular,
                    GraphicsUnit.Point, ((byte)(204))),
                Location = new Point(105, 114),
                Size = new Size(88, 17),
                Text = "Sound Volume"
            };
            soundOptionsMenu.Controls.Add(soundVolumeLabel);

            TrackBar soundTrackBar = CreateTrackBarTemplate();
            soundTrackBar.Location = new Point(80, 140);

            soundTrackBar.Scroll += soundTrackBar_Scroll;
            soundOptionsMenu.Controls.Add(soundTrackBar);

            Label musicVolumeLabel = new Label
            {
                AutoSize = true,
                Font = new Font("Franklin Gothic Medium", 9.75F, FontStyle.Regular,
                    GraphicsUnit.Point, ((byte)(204))),
                Location = new Point(105, 202),
                Size = new Size(88, 17),
                Text = "Music Volume"
            };
            soundOptionsMenu.Controls.Add(musicVolumeLabel);

            TrackBar musicTrackBar = CreateTrackBarTemplate();
            musicTrackBar.Location = new Point(80, 229);

            musicTrackBar.Scroll += musicTrackBar_Scroll;
            soundOptionsMenu.Controls.Add(musicTrackBar);
        }

        private void soundTrackBar_Scroll(object sender, EventArgs e)
        {
            Game.SetSoundVolumeLevel(0.5d * ((TrackBar)sender).Value / ((TrackBar)sender).Maximum);
            //Utility.waveOutSetVolume(0, (uint)(Convert.ToDouble(0xFFFF0000) * ((TrackBar)sender).Value / ((TrackBar)sender).Maximum));
        }

        private void musicTrackBar_Scroll(object sender, EventArgs e)
        {
            Game.SetMusicVolumeLevel(0.5d * ((TrackBar)sender).Value / ((TrackBar)sender).Maximum);
            //Utility.waveOutSetVolume(0, (uint)(Convert.ToDouble(0xFFFF0000) * ((TrackBar)sender).Value / ((TrackBar)sender).Maximum));
        }

        private void back_button_Click(object sender, EventArgs e)
        {
            ParentPanelHide(sender);
            mainMenu.Visible = true;
        }

        #endregion

        #region HPBar

        /// <summary>
        /// Полоска отображения очков здоровья игрока
        /// </summary>
        public void CreateHpBar()
        {
            if (hpBar is null)
            {
                hpBar = new ProgressBar
                {
                    Maximum = 150,
                    Size = new Size(80, 8),
                    Step = 1,
                    Style = ProgressBarStyle.Continuous,
                };

                //hpBar.DataBindings.Add("Value",Game.player, "Hitpoints");
                //hpBar.DataBindings.Add("Location", Game.player, "HpBarPoint");

                form.Controls.Add(hpBar);
            }
        }

        /// <summary>
        /// Обновление значения и позиции полоски очков здоровья на экране
        /// </summary>
        /// <param name="location"></param>
        /// <param name="value"></param>
        public void UpdateHpBar(Point? location, int? value)
        {
            if (hpBar != null)
            {
                hpBar.Location = location ?? new Point(-100, -100);
                hpBar.Value = value ?? 0;
            }
        }

        #endregion

        /// <summary>
        /// Создание кнопки ссылающейся на главное меню
        /// </summary>
        #region MainMenuButton

        private void CreateMainMenuButton()
        {
            mainMenuButton = CreateButtonTemplate();
            mainMenuButton.Location = new Point(330, 360);
            mainMenuButton.Text = "Main Menu";
            mainMenuButton.Visible = false;

            mainMenuButton.Click += mainMenuButton_Click;
            form.Controls.Add(mainMenuButton);
        }

        private void mainMenuButton_Click(object sender, EventArgs e)
        {
            MusicStop();
            ((Button)sender).Visible = false;
            mainMenu.Visible = true;
        }

        public void DisplayMainMenuButton()
        {
            mainMenuButton.Visible = true;
        }

        #endregion

    }
}
