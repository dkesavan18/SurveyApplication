
using System.Collections.Generic;
using System.Threading.Tasks;
using SurveyApp.Models;
using SurveyApp.Schemas;

namespace SurveyApp.Services;

public interface ISurveyResponsesService
{
    Task<ResponseObject> CreateSurveyResponses(SurveyResponses surveyresponses);
    Task<dynamic> GetSurveyResponses(long id);
    Task<long> GetSurveyResponsesCount(RequestFilter filter);
    Task<dynamic> GetSurveyResponsesList(int index,int limit,RequestFilter filter);
    Task<bool> UpdateSurveyResponses(SurveyResponses surveyresponses);
    Task<bool> DeleteSurveyResponses(long key);
}
