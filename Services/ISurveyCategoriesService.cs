
using System.Collections.Generic;
using System.Threading.Tasks;
using SurveyApp.Models;
using SurveyApp.Schemas;

namespace SurveyApp.Services;

public interface ISurveyCategoriesService
{
    Task<ResponseObject> CreateSurveyCategories(SurveyCategories surveycategories);
    Task<dynamic> GetSurveyCategories(long id);
    Task<long> GetSurveyCategoriesCount(RequestFilter filter);
    Task<dynamic> GetSurveyCategoriesList(int index,int limit,RequestFilter filter);
    Task<bool> UpdateSurveyCategories(SurveyCategories surveycategories);
    Task<bool> DeleteSurveyCategories(long key);
}
