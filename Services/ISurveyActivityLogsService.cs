
using System.Collections.Generic;
using System.Threading.Tasks;
using SurveyApp.Models;
using SurveyApp.Schemas;

namespace SurveyApp.Services;

public interface ISurveyActivityLogsService
{
    Task<ResponseObject> CreateSurveyActivityLogs(SurveyActivityLogs surveyactivitylogs);
    Task<dynamic> GetSurveyActivityLogs(long id);
    Task<long> GetSurveyActivityLogsCount(RequestFilter filter);
    Task<dynamic> GetSurveyActivityLogsList(int index,int limit,RequestFilter filter);
    Task<bool> UpdateSurveyActivityLogs(SurveyActivityLogs surveyactivitylogs);
    Task<bool> DeleteSurveyActivityLogs(long key);
}
