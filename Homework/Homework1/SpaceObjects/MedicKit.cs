using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homework
{
    /// <summary>
    /// Аптечки, восстанавливающие запас здоровья
    /// </summary>
    class MedicKit: SpaceObject
    {
        public MedicKit(Point pos, Point dir, Size size) : base(pos, dir, size)
        {
            HasCollider = true;
        }
        public MedicKit(Point pos, Point dir, Size size, Image image) : base(pos, dir, size, image)
        {
            HasCollider = true;
        }

        public override void Update()
        {
            
        }
    }
}