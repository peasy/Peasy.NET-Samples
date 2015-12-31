using Mapster;
using Peasy.DataProxy.EF6;

namespace Orders.com.DAL.EF
{
    public class MapsterHelper : IMapper
    {
        public TDestination Map<TSource, TDestination>(TSource source)
        {
            return TypeAdapter.Adapt<TDestination>(source);
        }

        public TDestination Map<TSource, TDestination>(TSource source, TDestination destination)
        {
            return TypeAdapter.Adapt<TSource, TDestination>(source, destination);
        }
    }
}
