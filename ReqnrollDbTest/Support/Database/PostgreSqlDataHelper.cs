using System;
using System.Data;
using Npgsql;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using Dapper;
using NpgsqlTypes;

namespace ReqnrollDbTest.Support.Database;

public class PostgreSqlDataHelper : IDisposable
{
    private readonly NpgsqlConnection _connection;

    public PostgreSqlDataHelper()
    {
        _connection = new NpgsqlConnection(ConfigHelper.PostgreSQLConnectionString);
        _connection.Open();
    }

    #region Basic Operations with Dapper

    public async Task<T> ExecuteScalarAsync<T>(string sql, object parameters = null)
    {
        return await _connection.ExecuteScalarAsync<T>(sql, parameters);
    }

    public async Task<IEnumerable<dynamic>> ExecuteQueryAsync(string sql, object parameters = null)
    {
        return await _connection.QueryAsync(sql, parameters);
    }

    public async Task<IEnumerable<T>> ExecuteQueryAsync<T>(string sql, object parameters = null)
    {
        return await _connection.QueryAsync<T>(sql, parameters);
    }

    public async Task<int> ExecuteAsync(string sql, object parameters = null)
    {
        return await _connection.ExecuteAsync(sql, parameters);
    }

    #endregion

    #region Bulk Operations (Optimized)

    public async Task BulkInsertAsync<T>(string tableName, IEnumerable<T> entities)
    {
        if (!entities.Any()) return;

        using (var writer = _connection.BeginBinaryImport(GetCopyCommand<T>(tableName)))
        {
            foreach (var entity in entities)
            {
                writer.StartRow();
                foreach (var prop in typeof(T).GetProperties())
                {
                    var value = prop.GetValue(entity);
                    await writer.WriteAsync(value ?? DBNull.Value, GetNpgsqlDbType(prop.PropertyType));
                }
            }
            await writer.CompleteAsync();
        }
    }

    private string GetCopyCommand<T>(string tableName)
    {
        var columns = typeof(T).GetProperties().Select(p => p.Name);
        return $"COPY {tableName} ({string.Join(", ", columns)}) FROM STDIN (FORMAT BINARY)";
    }

    private NpgsqlDbType GetNpgsqlDbType(Type type)
    {
        if (type == typeof(int)) return NpgsqlDbType.Integer;
        if (type == typeof(string)) return NpgsqlDbType.Text;
        if (type == typeof(DateTime)) return NpgsqlDbType.Timestamp;
        if (type == typeof(bool)) return NpgsqlDbType.Boolean;
        if (type == typeof(decimal)) return NpgsqlDbType.Numeric;
        if (type == typeof(double)) return NpgsqlDbType.Double;
        if (type == typeof(Guid)) return NpgsqlDbType.Uuid;
        return NpgsqlDbType.Unknown;
    }

    #endregion

    #region Utility Methods

    public async Task<int> GetRecordCountAsync(string tableName, string whereClause = "", object parameters = null)
    {
        var sql = $"SELECT COUNT(*) FROM {tableName}";
        if (!string.IsNullOrWhiteSpace(whereClause))
            sql += $" WHERE {whereClause}";

        return await ExecuteScalarAsync<int>(sql, parameters);
    }

    public async Task<bool> RowExistsAsync(string sql, object parameters = null)
    {
        var result = await _connection.ExecuteScalarAsync<object>(sql, parameters);
        return result != null && result != DBNull.Value;
    }

    public async Task<bool> TableExistsAsync(string tableName)
    {
        const string sql = @"
                SELECT EXISTS (
                    SELECT 1 
                    FROM information_schema.tables 
                    WHERE table_schema = 'public' 
                    AND table_name = @tableName)";

        return await ExecuteScalarAsync<bool>(sql, new { tableName });
    }

    public async Task TruncateTableAsync(string tableName, bool cascade = false)
    {
        var sql = $"TRUNCATE TABLE {tableName}";
        if (cascade) sql += " CASCADE";
        await ExecuteAsync(sql);
    }

    #endregion

    #region Disposable Pattern

    public void Dispose()
    {
        _connection?.Close();
        _connection?.Dispose();
        GC.SuppressFinalize(this);
    }

    #endregion
}
