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
        private ProgressBar hpBar;
        private Button mainMenuButton;
        private Action MusicStop;
        
        private const string matchboxPath = @"Homework1\Ships\Matchboxes\Matchbox.jpg";
        private const string starshipPath = @"Homework1\Ships\Starships\Starship.jpg";
        
        public Overlay(Form form, Action musicStop)
        {
            this.form = form;
            CreateMainMenu();
            CreateGameModeMenu();
            CreateOptionsMenu();
            CreateMainMenuButton();
            MusicStop = musicStop;
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
                BorderStyle = BorderStyle.Fixed3D
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
                AutoSize=false,
                Value = 10,
                Size = new Size(140, 25),
            };
        }

        #endregion

        #region MainMenu

        /// <summary>
        /// Создыние главного меню с кнопками "New Game", "Options", "Exit".
        /// </summary>
        /// <param name="form"></param>
        private void CreateMainMenu()
        {
            mainMenu = CreatePanelTemplate();

            Label mainMenuLabel = CreateLabelTemplate();
            mainMenuLabel.Text = "Main Menu";

            mainMenu.Controls.Add(mainMenuLabel);

            Button newGame_button = CreateButtonTemplate();
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
            starshipRadioButton.Image = new Bitmap(Image.FromFile(starshipPath),starshipRadioButton.Font.Height, starshipRadioButton.Font.Height);
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
        /// Меню опций
        /// </summary>
        #region OptionsMenu

        private void CreateOptionsMenu()
        {
            optionsMenu = CreatePanelTemplate();
            optionsMenu.Visible = false;
            Label optionsLabel = CreateLabelTemplate();
            optionsLabel.Text = "Options:";
            optionsMenu.Controls.Add(optionsLabel);

            CreateSoundOptions();

            Button back_button = CreateButtonTemplate();
            back_button.Location = new Point(80, 290);
            back_button.Text = "Back";

            back_button.Click += back_button_Click;
            optionsMenu.Controls.Add(back_button);

            optionsMenu.Location = new Point(250, 100);
            form.Controls.Add(optionsMenu);
        }

        private void CreateSoundOptions()
        {
            Label soundVolumeLabel = new Label
            {
                AutoSize = true,
                Font = new Font("Franklin Gothic Medium", 9.75F, FontStyle.Regular,
                    GraphicsUnit.Point, ((byte) (204))),
                Location = new Point(105, 114),
                Size = new Size(88, 17),
                Text="Sound Volume"
            };
            optionsMenu.Controls.Add(soundVolumeLabel);

            TrackBar soundTrackBar = CreateTrackBarTemplate();
            soundTrackBar.Location = new Point(80, 140);

            soundTrackBar.Scroll += soundTrackBar_Scroll;
            optionsMenu.Controls.Add(soundTrackBar);

            Label musicVolumeLabel = new Label
            {
                AutoSize = true,
                Font = new Font("Franklin Gothic Medium", 9.75F, FontStyle.Regular,
                    GraphicsUnit.Point, ((byte)(204))),
                Location = new Point(105, 202),
                Size = new Size(88, 17),
                Text = "Music Volume"
            };
            optionsMenu.Controls.Add(musicVolumeLabel);

            TrackBar musicTrackBar = CreateTrackBarTemplate();
            musicTrackBar.Location = new Point(80, 229);

            musicTrackBar.Scroll += musicTrackBar_Scroll;
            optionsMenu.Controls.Add(musicTrackBar);
        }

        private void soundTrackBar_Scroll(object sender, EventArgs e)
        {
            Game.SetSoundVolumeLevel(0.5d*((TrackBar)sender).Value/ ((TrackBar)sender).Maximum);
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

        /// <summary>
        /// Обновление значения и позиции полоски очков здоровья на экране
        /// </summary>
        /// <param name="location"></param>
        /// <param name="value"></param>
        public void UpdateHpBar(Point? location, int? value)
        {
            if (hpBar!=null)
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
