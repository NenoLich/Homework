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
    class Bullet : SpaceObject, IDisposable
    {
        private static int speed = 3;

        bool disposed = false;
        SafeHandle handle = new SafeFileHandle(IntPtr.Zero, true);

        public Bullet(Point position, Point direction, Size size) : base(position, direction, size)
        {
            HasCollider = true;
        }

        public Bullet(Point position, Point direction, Size size, Image image) : base(position, direction, size, image)
        {
            HasCollider = true;
        }

        public override void Update()
        {
            if (position.X > Game.Width)
            {
                this.Dispose();
            }

            position.X = position.X + speed;
        }

        #region IDisposable Implementation

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposed)
                return;

            if (disposing)
            {
                handle.Dispose();
            }

            disposed = true;
        }

        #endregion

    }
}
