using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;

namespace Homework
{
    /// <summary>
    /// Космический корабль, управляется игроком
    /// </summary>
    class Starship: SpaceObject
    {
        /// <summary>
        /// Очки здоровья, игра заканчивается при их полном истощении
        /// </summary>
        public int Hitpoints { get; private set; } = 150;

        public Point GunPoint { get; private set; }

        public Starship(Point position, Point direction, Size size) : base(position, direction, size)
        {
            HasCollider = true;
            GunPoint = new Point(position.X + size.Width, position.Y + size.Height / 2);
        }

        public Starship(Point position, Point direction, Size size, Image image) : base(position, direction, size, image)
        {
            HasCollider = true;
            GunPoint = new Point(position.X + size.Width, position.Y + size.Height / 2);
        }

        public override void Update()
        {
            GunPoint = new Point(position.X + size.Width, position.Y + size.Height / 2);
        }

        #region Motion

        public void MoveUp()
        {
            int predictedPosition = position.Y - Direction.Y;
            if (predictedPosition > 0) position.Y = predictedPosition;
        }
        public void MoveDown()
        {
            int predictedPosition = position.Y + Direction.Y;
            if (predictedPosition < Game.Height) position.Y = predictedPosition;
        }

        public void MoveLeft()
        {
            int predictedPosition = position.X - Direction.X;
            if (predictedPosition > 0) position.X = predictedPosition;
        }
        public void MoveRight()
        {
            int predictedPosition = position.X + Direction.X;
            if (predictedPosition <Game.Width-200) position.X = predictedPosition;
        }

        #endregion

        public void GetDamage(Asteroid asteroid)
        {
            Hitpoints -= asteroid.Power;

            if (Hitpoints<=0)
            {
                Hitpoints = 0;
                Die();
            }
        }

        private void Die()
        {
            Game.GameOver();
        }
    }
}
