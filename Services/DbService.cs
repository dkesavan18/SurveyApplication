using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Extensions.Configuration;
using Npgsql;

namespace SurveyApp.Services;

public class DbService : IDbService
{
    private readonly IDbConnection _db;

    public DbService(IConfiguration configuration)
    {
        _db = new NpgsqlConnection(Environment.GetEnvironmentVariable("DBSTRING"));
    }

    public async Task<T> GetAsync<T>(string command, object parms)
    {
        T result;

        result = (await _db.QueryAsync<T>(command, parms).ConfigureAwait(false)).FirstOrDefault();

        return result;

    }

    public async Task<List<dynamic>> GetAll<dynamic>(string command, object parms)
    {

        List<dynamic> result = new List<dynamic>();

        result = (await _db.QueryAsync<dynamic>(command, parms)).ToList();

        return result;
    }

    public async Task<dynamic> GetCount(string command, object parms)
    {
        dynamic result;

        result = await _db.QueryAsync<dynamic>(command, parms);

        return result;
    }

    public async Task<int> EditData(string command, object parms)
    {
        int result;

        result = await _db.ExecuteAsync(command, parms);

        return result;
    }
}
