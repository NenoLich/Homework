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
        public void Asteroids()
        {
            Form asteroidsForm = new Form ();
            asteroidsForm.Width = 800;
            asteroidsForm.Height = 600;
            Game.Init(asteroidsForm);
            asteroidsForm.ShowDialog();
            Game.Draw();
        }
    }
}
