using AutoMapper;
using ITMat.Core.DTO;
using ITMat.Core.Models;

namespace ITMat.Core.Services
{
    public abstract class AbstractService<TModel, TDTO>
        where TModel : AbstractModel
        where TDTO : AbstractDTO
    {
        protected IMapper Mapper { get; }

        public AbstractService()
            => Mapper = new Mapper(new MapperConfiguration(cfg => cfg.CreateMap<TModel, TDTO>()));

        public AbstractService(IMapper mapper)
            => Mapper = mapper;

        protected class MapperFactory
        {
            public static IMapper Create<TProfile>() where TProfile : Profile, new()
                => new Mapper(new MapperConfiguration(cfg => cfg.AddProfile(new TProfile())));
        }
    }
}