using AutoMapper;

namespace MG.BuildingBlock.Infra.Mappers.AutoMapper.Contracts;

public interface IMapFrom<T>
{
    void MapFrom(Profile profile) => profile.CreateMap(typeof(T), GetType());
}
