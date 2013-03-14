using System;

using Opt.ClosenessModel;
using Opt.Geometrics;

namespace Opt.Algorithms
{
    public interface IWithClosenessModel
    {
        Vertex<Geometric> Vertex
        {
            get;
        }
    }
}
