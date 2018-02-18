using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homework
{
    abstract class SpaceObject:ICollidable
    {
        public bool HasCollider;

        protected Point position;
        protected Point direction;
        protected Size size;
        protected Image image;

        protected Point Direction
        {
            get => direction;
            set
            {
                if (value.X<0 || value.X>50 || value.Y < 0 || value.Y > 50)
                {
                    throw new GameObjectException($"Неправильные параметры {nameof(Direction)} обьекта {this.GetType()}");
                }

                direction=value;
            }
        }

        public SpaceObject(Point position, Point direction, Size size)
        {
            this.position = position;
            Direction = direction;
            this.size = size;
        }
        public SpaceObject(Point position, Point direction, Size size, Image image)
        {
            this.position = position;
            Direction = direction;
            this.size = size;
            this.image = image;

        }
        public virtual void Draw()
        {
            Game.Buffer.Graphics.DrawImage(image, position.X, position.Y, size.Width, size.Height);
        }

        public abstract void Update();

        public virtual void Relocate()
        {
        }

        public virtual void Relocate(int positionHeight)
        {
        }

        #region ICollidable implementation

        public bool Collide(ICollidable obj)
        {
             return obj.Rect.IntersectsWith(this.Rect);
        }

        public Rectangle Rect => new Rectangle(position, size);

        #endregion

    }
}
