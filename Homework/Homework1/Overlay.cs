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
    class Overlay
    {
        private Form form;
        private Panel mainMenu;
        private Panel optionsMenu;

        public Overlay(Form form)
        {
            this.form = form;
            CreateMainMenu();
            CreateOptionsMenu();
        }

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

            Button newGame_button = new Button
            {
                Font = new Font("Franklin Gothic Medium", 12F, FontStyle.Regular,
                    GraphicsUnit.Point, ((byte)(204))),
                Location = new Point(80, 130),
                Size = new Size(140, 40),
                Text = "New Game",
                UseVisualStyleBackColor = true
            };
            newGame_button.Click += newGame_button_Click;
            mainMenu.Controls.Add(newGame_button);

            Button options_button = new Button
            {
                Font = new Font("Franklin Gothic Medium", 12F, FontStyle.Regular,
                    GraphicsUnit.Point, ((byte)(204))),
                Location = new Point(80, 210),
                Size = new Size(140, 40),
                Text = "Options",
                UseVisualStyleBackColor = true
            };
            options_button.Click += options_button_Click;
            mainMenu.Controls.Add(options_button);

            Button exit_button = new Button
            {
                Font = new Font("Franklin Gothic Medium", 12F, FontStyle.Regular,
                    GraphicsUnit.Point, ((byte)(204))),
                Location = new Point(80, 290),
                Size = new Size(140, 40),
                Text = "Exit",
                UseVisualStyleBackColor = true
            };
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
            Game.Start();
        }

        #endregion

        #region OptionsMenu

        private void CreateOptionsMenu()
        {
            optionsMenu = CreatePanelTemplate();
            optionsMenu.Visible = false;
            Label optionsLabel = CreateLabelTemplate();
            optionsLabel.Text = "Options:";
            optionsMenu.Controls.Add(optionsLabel);

            CreateSoundOptions();

            Button back_button = new Button
            {
                Font = new Font("Franklin Gothic Medium", 12F, FontStyle.Regular,
                    GraphicsUnit.Point, ((byte)(204))),
                Location = new Point(80, 290),
                Size = new Size(140, 40),
                Text = "Back",
                UseVisualStyleBackColor = true
            };
            back_button.Click += back_button_Click;
            optionsMenu.Controls.Add(back_button);

            optionsMenu.Location = new Point(250, 100);
            form.Controls.Add(optionsMenu);
        }

        public void CreateSoundOptions()
        {
            Label volumeLabel = new Label
            {
                AutoSize = true,
                Font = new Font("Franklin Gothic Medium", 12F, FontStyle.Regular,
                    GraphicsUnit.Point, ((byte) (204))),
                Location = new Point(105, 150),
                Size = new Size(140, 40),
                Text="Volume"
            };
            optionsMenu.Controls.Add(volumeLabel);

            TrackBar volumeTrackBar = new TrackBar
            {
                Value = 10,
                Size = new Size(140, 40),
                Location = new Point(80, 210)
            };
            volumeTrackBar.Scroll += volumeTrackBar_Scroll;
            optionsMenu.Controls.Add(volumeTrackBar);
        }

        private void volumeTrackBar_Scroll(object sender, EventArgs e)
        {
            //
        }

        private void back_button_Click(object sender, EventArgs e)
        {
            ParentPanelHide(sender);
            mainMenu.Visible = true;
        }

        #endregion

        private void ParentPanelHide(object sender)
        {
            Button button = sender as Button;
            button.Parent.Visible = false;
        }
    }
}
