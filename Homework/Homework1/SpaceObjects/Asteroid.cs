using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace Homework
{
    /// <summary>
    /// Враждебные обьекты, наносят урон при столкновении
    /// </summary>
    class Asteroid: SpaceObject
    {
        public Asteroid(Point position, Point direction, Size size) : base(position, direction, size)
        {
            HasCollider = true;
        }

        public Asteroid(Point position, Point direction, Size size, Image image) : base(position, direction, size, image)
        {
            HasCollider = true;
        }

        /// <summary>
        /// Вращается на 90° по часовой стрелке, при выходе за экран - переносится в видимую область
        /// </summary>
        public override void Update()
        {
            position.X = position.X - Direction.X;
            image.RotateFlip(RotateFlipType.Rotate90FlipNone);
            if (position.X < 0) Relocate();
        }

        public override void Relocate()
        {
            position.X = Game.Width;
            position.Y = Game.randomizer.Next(0,Game.Height-size.Height);
        }

        public override void Relocate(int positionHeight)
        {
            position.X = Game.Width;
            position.Y = positionHeight;
        }
    }
}
