
using System.Collections.Generic;
using System.Threading.Tasks;
using SurveyApp.Models;
using SurveyApp.Schemas;

namespace SurveyApp.Services;

public interface IResponsesService
{
    Task<ResponseObject> CreateResponses(Responses responses);
    Task<dynamic> GetResponses(long id);
    Task<long> GetResponsesCount(RequestFilter filter);
    Task<dynamic> GetResponsesList(int index,int limit,RequestFilter filter);
    Task<bool> UpdateResponses(Responses responses);
    Task<bool> DeleteResponses(long key);
}
