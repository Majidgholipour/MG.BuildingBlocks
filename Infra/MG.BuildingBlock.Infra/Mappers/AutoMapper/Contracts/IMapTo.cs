using AutoMapper;

namespace MG.BuildingBlock.Infra.Mappers.AutoMapper.Contracts;

public interface IMapTo<T>
{
    void MapTo(Profile profile) => profile.CreateMap(GetType(), typeof(T));
}