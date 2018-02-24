using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace Homework
{
    class Homework1: Homework
    {
        /// <summary>
        /// Игра Asteroids
        /// </summary>
        public void Asteroids()
        {
            Form asteroidsForm = new Form ();
            asteroidsForm.FormBorderStyle = FormBorderStyle.FixedDialog;
            asteroidsForm.Width = 800;
            asteroidsForm.Height = 600;
            if (Game.Awake(asteroidsForm))
            {
                asteroidsForm.ShowDialog();
            }
        }
    }
}
