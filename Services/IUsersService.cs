
using System.Collections.Generic;
using System.Threading.Tasks;
using SurveyApp.Models;
using SurveyApp.Schemas;

namespace SurveyApp.Services;

public interface IUsersService
{
    Task<ResponseObject> CreateUsers(Users users);
    Task<dynamic> GetUsers(long id);
    Task<long> GetUsersCount(RequestFilter filter);
    Task<dynamic> GetUsersList(int index,int limit,RequestFilter filter);
    Task<bool> UpdateUsers(Users users);
    Task<bool> DeleteUsers(long key);
}
