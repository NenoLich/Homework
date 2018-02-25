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
        public int Hitpoints { get; private set; } = maxHitpoints;

        /// <summary>
        /// Точка для отрисовки полоски очков здоровья
        /// </summary>
        public Point HpBarPoint { get; private  set; }

        /// <summary>
        /// Точка для инстанцирования выпущенных снарядов
        /// </summary>
        public Point GunPoint { get; private set; }

        /// <summary>
        /// Расстояние между Spaceship и его полоской очков здоровья
        /// </summary>
        private const int hpBarOffset = 20;

        private const int maxHitpoints = 150;

        public Starship(Point position, Point direction, Size size) : base(position, direction, size)
        {
            HasCollider = true;
            UpdateProperties();
        }

        public Starship(Point position, Point direction, Size size, Image image) : base(position, direction, size, image)
        {
            HasCollider = true;
            UpdateProperties();
        }

        public override void Update()
        {
            UpdateProperties();
        }

        private void UpdateProperties()
        {
            GunPoint = new Point(position.X + size.Width, position.Y + size.Height / 2);
            HpBarPoint = new Point(position.X, position.Y + size.Height + hpBarOffset);
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

        #region Interaction

        /// <summary>
        /// Срабатывает при столкновении с астероидом или аптечкой
        /// </summary>
        /// <param name="spaceObject"></param>
        public void GetDamageOrHeal(SpaceObject spaceObject)
        {
            if (spaceObject is Asteroid)
            {
                Hitpoints -= spaceObject.Power;

                if (Hitpoints <= 0)
                {
                    Hitpoints = 0;
                    Die();
                }
            }

            if (spaceObject is MedicKit)
            {
                Hitpoints += spaceObject.Power*4;
                if (Hitpoints > maxHitpoints)
                {
                    Hitpoints = maxHitpoints;
                }
            }
        }

        /// <summary>
        /// Крушение корабля
        /// </summary>
        private void Die()
        {
            Game.GameOver();
        }

        #endregion

    }
}
