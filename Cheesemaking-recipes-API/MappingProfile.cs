using AutoMapper;
using Cheesemaking_recipes_API.Entities;
using Cheesemaking_recipes_API.Models;

namespace Cheesemaking_recipes_API
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Note, NoteDto>();
            CreateMap<Template, TemplateDto>();
            CreateMap<Category, CategoryDto>();
            CreateMap<Label, LabelDto>();
            CreateMap<Input, InputDto>();
        }
    }
}
