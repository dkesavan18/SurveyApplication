
using System.Collections.Generic;
using System.Threading.Tasks;
using SurveyApp.Models;
using SurveyApp.Schemas;

namespace SurveyApp.Services;

public interface IOptionCategoriesService
{
    Task<ResponseObject> CreateOptionCategories(OptionCategories optioncategories);
    Task<dynamic> GetOptionCategories(long id);
    Task<long> GetOptionCategoriesCount(RequestFilter filter);
    Task<dynamic> GetOptionCategoriesList(int index,int limit,RequestFilter filter);
    Task<bool> UpdateOptionCategories(OptionCategories optioncategories);
    Task<bool> DeleteOptionCategories(long key);
}
