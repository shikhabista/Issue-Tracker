using System.Data;
using Base.Services.Interfaces;
using Dapper;
using Microsoft.Extensions.Configuration;
using Npgsql;

namespace Base.Services;

public class DbService : IDbService
{
    private readonly IDbConnection _db;

    public DbService(IConfiguration configuration)
    {
        _db = new NpgsqlConnection(configuration.GetConnectionString("DefaultConnection"));
    }

    public async Task<T> GetAsync<T>(string command, object @params)
    {
        T result;

        result = (await _db.QueryAsync<T>(command, @params).ConfigureAwait(false)).FirstOrDefault();

        return result;
    }

    public async Task<List<T>> GetAll<T>(string command, object @params)
    {
        List<T> result = new List<T>();

        result = (await _db.QueryAsync<T>(command, @params)).ToList();

        return result;
    }

    public async Task<int> ExecuteQuery(string command, object @params)
    {
        int result;

        result = await _db.ExecuteAsync(command, @params);

        return result;
    }

    public async Task<T> QuerySingleOrDefaultAsync<T>(string command, object @params)
    {
        T result;
        result = await _db.QuerySingleOrDefaultAsync<T>(command, @params);
        return result;
    }
}