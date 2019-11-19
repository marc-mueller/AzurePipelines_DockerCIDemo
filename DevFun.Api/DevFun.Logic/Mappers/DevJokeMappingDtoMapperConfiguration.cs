using _4tecture.DataAccess.Common.DtoMapping;
using AutoMapper;
using DevFun.Common.Dtos;
using DevFun.Common.Entities;

namespace DevFun.Logic.Mappers
{
    public class DevJokeMappingDtoMapperConfiguration : DtoMapperConfiguration<DevJoke, DevJokeDto>
    {
        protected override IMappingExpression<DevJoke, DevJokeDto> MapEntityToDto(IMappingExpression<DevJoke, DevJokeDto> mappingExpression)
        {
            if (mappingExpression is null)
            {
                throw new System.ArgumentNullException(nameof(mappingExpression));
            }

            return mappingExpression.ForMember(m => m.CategoryName, i => i.MapFrom((source, dest, value) => source.Category?.Name));
        }

        protected override IMappingExpression<DevJokeDto, DevJoke> MapDtoToEntity(IMappingExpression<DevJokeDto, DevJoke> mappingExpression)
        {
            if (mappingExpression is null)
            {
                throw new System.ArgumentNullException(nameof(mappingExpression));
            }

            return mappingExpression
                .ForMember(m => m.Category, i => i.Ignore());
        }
    }
}