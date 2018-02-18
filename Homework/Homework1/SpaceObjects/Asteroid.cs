using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace Homework
{
    class Asteroid: SpaceObject
    {
        public readonly int Power;

        public Asteroid(Point position, Point direction, Size size) : base(position, direction, size)
        {
            HasCollider = true;
            Power = size.Height * size.Width / 100;
        }

        public Asteroid(Point position, Point direction, Size size, Image image) : base(position, direction, size, image)
        {
            HasCollider = true;
            Power = size.Height * size.Width / 100;
        }

        public override void Update()
        {
            position.X = position.X - Direction.X;
            image.RotateFlip(RotateFlipType.Rotate90FlipNone);
            if (position.X < 0) Relocate(position.Y);
        }

        public override void Relocate()
        {
            position.X = Game.Width + size.Width;
            position.Y = Game.randomizer.Next(0,Game.Height-size.Height);
        }

        public override void Relocate(int positionHeight)
        {
            position.X = Game.Width + size.Width;
            position.Y = positionHeight;
        }
    }
}
