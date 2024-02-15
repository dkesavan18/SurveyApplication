
using System.Collections.Generic;
using System.Threading.Tasks;
using SurveyApp.Models;
using SurveyApp.Schemas;

namespace SurveyApp.Services;

public interface ISurveysService
{
    Task<ResponseObject> CreateSurveys(Surveys surveys);
    Task<dynamic> GetSurveys(long id);
    Task<long> GetSurveysCount(RequestFilter filter);
    Task<dynamic> GetSurveysList(int index,int limit,RequestFilter filter);
    Task<bool> UpdateSurveys(Surveys surveys);
    Task<bool> DeleteSurveys(long key);
}
