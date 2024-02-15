
using System.Collections.Generic;
using System.Threading.Tasks;
using SurveyApp.Models;
using SurveyApp.Schemas;

namespace SurveyApp.Services;

public interface IGroupsService
{
    Task<ResponseObject> CreateGroups(Groups groups);
    Task<dynamic> GetGroups(long id);
    Task<long> GetGroupsCount(RequestFilter filter);
    Task<dynamic> GetGroupsList(int index,int limit,RequestFilter filter);
    Task<bool> UpdateGroups(Groups groups);
    Task<bool> DeleteGroups(long key);
}
