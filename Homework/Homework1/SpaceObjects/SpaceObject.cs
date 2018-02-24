using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Win32.SafeHandles;

namespace Homework
{
    /// <summary>
    /// Базовый абстрактный класс для всех космических обьектов
    /// </summary>
    abstract class SpaceObject: ICollidable, IDisposable
    {
        #region Vars and Props

        /// <summary>
        /// Эквивалентно нанесенному урону или восстановленному здоровью при столкновении
        /// </summary>
        public readonly int Power;

        public bool HasCollider;

        protected Point position;
        protected Point direction;
        protected Size size;
        protected Image image;

        readonly SafeHandle handle = new SafeFileHandle(IntPtr.Zero, true);

        public bool Disposed { get; private set; } = false;

        protected Point Direction
        {
            get => direction;
            set
            {
                if (value.X<0 || value.X>80 || value.Y < 0 || value.Y > 80)
                {
                    throw new GameObjectException($"Неправильные параметры {nameof(Direction)} обьекта {this.GetType()}");
                }

                direction=value;
            }
        }

        #endregion

        #region Ctors

        public SpaceObject(Point position, Point direction, Size size)
        {
            this.position = position;
            Direction = direction;
            this.size = size;
            Power = size.Height * size.Width / 100;
        }
        public SpaceObject(Point position, Point direction, Size size, Image image)
        {
            this.position = position;
            Direction = direction;
            this.size = size;
            this.image = image;
            Power = size.Height * size.Width / 100;
        }

        #endregion

        #region Methods

        public abstract void Update();

        public virtual void Draw()
        {
            Game.Buffer.Graphics.DrawImage(image, position.X, position.Y, size.Width, size.Height);
        }

        public virtual void Relocate()
        {
        }

        public virtual void Relocate(int positionHeight)
        {
        }
        
        #endregion

        #region ICollidable implementation

        public bool Collide(ICollidable obj)
        {
             return obj.Rect.IntersectsWith(this.Rect);
        }

        public Rectangle Rect => new Rectangle(position, size);

        #endregion

        #region IDisposable Implementation

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (Disposed)
                return;

            if (disposing)
            {
                handle.Dispose();
            }

            Disposed = true;
        }

        #endregion

    }
}
