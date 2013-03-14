using System;

namespace Opt.Geometrics3d
{
    /// <summary>
    /// Геометрический объект в трёхмерном пространстве.
    /// </summary>
    public abstract class Geometric3d
    {
        public int Dim
        {
            get
            {
                return 3;
            }
        }
    }
}
