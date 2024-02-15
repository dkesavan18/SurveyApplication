
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SurveyApp.Services;

public interface IDbService
{
    Task<T> GetAsync<T>(string command, object parms); 
    Task<List<dynamic>> GetAll<dynamic>(string command, object parms );
    Task<dynamic> GetCount(string command, object parms);
    Task<int> EditData(string command, object parms);
}
