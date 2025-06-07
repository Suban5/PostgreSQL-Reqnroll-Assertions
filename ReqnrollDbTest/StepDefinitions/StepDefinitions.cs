using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using NUnit.Framework;
using Reqnroll;
using ReqnrollDbTest.Models;
using ReqnrollDbTest.Support.Database;
using ReqnrollDbTest.Support.Extensions;

namespace ReqnrollDbTest.StepDefinitions;

[Binding]
public sealed class StepDefinitions
{
    private readonly PostgreSqlDataHelper _dbHelper = new();
    

    [When(@"I added the following data in ""(.*)"" table:")]
    public async Task WhenIAddedTheFollowingDataInTable(String tablename, DataTable dataTable)
    {
        var filteredDataTable = DataTableHelper.ApplyDataFilters(dataTable);
        var customers = filteredDataTable.CreateSet<Customer>();
        await _dbHelper.BulkInsertAsync(tablename, customers);
    }


    [Given(@"I have following data in ""(.*)"" table:")]
    [Then(@"There should exists following data in ""(.*)"" table:")]
    public async Task ThenThereShouldExistFollowingDataInCustomerTable(string tableName, DataTable dataTable)
    {
        var filteredDataTable = DataTableHelper.ApplyDataFilters(dataTable);

        foreach (var row in filteredDataTable.Rows)
        {
            var whereParts = new List<string>();
            var parameters = new DynamicParameters();

            foreach (var col in row.Keys)
            {
                var value = row[col];
                if (value == null)
                {
                    whereParts.Add($"{col} IS NULL");
                }
                else if (value == "")
                {
                    whereParts.Add($"{col} = @{col}");
                    parameters.Add($"@{col}", "");
                }
                else
                {
                    whereParts.Add($"{col} = @{col}");
                    parameters.Add($"@{col}", value);
                }
            }

            var whereClause = string.Join(" AND ", whereParts);
            var sql = $"SELECT 1 FROM {tableName} WHERE {whereClause}";

            bool exists = await _dbHelper.RowExistsAsync(sql, parameters);

            Assert.That(exists, Is.True,
                $"Expected row was not found in '{tableName}': {string.Join(", ", row.Select(kv => $"{kv.Key}='{kv.Value}'"))}");
        }
    }


    [Given(@"I cleaned added data in ""(.*)"" table")]
    [When(@"I cleaned added data in ""(.*)"" table")]
    [Then(@"I cleaned added data in ""(.*)"" table")]
    public async Task ThenICleanedAddedDataInTable(String tableName)
    {
        await _dbHelper.ExecuteAsync($"DELETE FROM {tableName} where ZipCode = '99999'");
    }
    
    
[Then(@"There should not exists following data in ""(.*)"" table:")]
public async Task ThenThereShouldNotExistFollowingDataInTable(string tableName, DataTable dataTable)
{
    var filteredDataTable = DataTableHelper.ApplyDataFilters(dataTable);

    foreach (var row in filteredDataTable.Rows)
    {
        var whereParts = new List<string>();
        var parameters = new DynamicParameters();

        foreach (var col in row.Keys)
        {
            var value = row[col];
            if (value == null)
            {
                whereParts.Add($"{col} IS NULL");
            }
            else if (value == "")
            {
                whereParts.Add($"{col} = @{col}");
                parameters.Add($"@{col}", "");
            }
            else
            {
                whereParts.Add($"{col} = @{col}");
                parameters.Add($"@{col}", value);
            }
        }

        var whereClause = string.Join(" AND ", whereParts);
        var sql = $"SELECT 1 FROM {tableName} WHERE {whereClause}";

        bool exists = await _dbHelper.RowExistsAsync(sql, parameters);

        Assert.That(exists, Is.False,
            $"Row should NOT exist in '{tableName}' but was found: {string.Join(", ", row.Select(kv => $"{kv.Key}='{kv.Value}'"))}");
    }
}



}
