using Opt.ClosenessModel;
using Opt.Geometrics.Geometrics2d;

namespace Opt.Algorithms
{
    public interface IWithClosenessModel
    {
        Vertex<Geometric2d> Vertex
        {
            get;
        }
    }
}
