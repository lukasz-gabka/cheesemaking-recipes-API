using Cheesemaking_recipes_API.Models;
using System.Collections.Generic;

namespace Cheesemaking_recipes_API.Services
{
    public interface ITemplateService
    {
        int Create(CreateTemplateDto dto);
        void CreateDefaultTemplateForNewUser(int userId);
        bool Delete(int templateId);
        List<TemplateDto> GetAll();
        TemplateDto GetById(int id);
    }
}