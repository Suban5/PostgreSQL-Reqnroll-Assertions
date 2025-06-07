using System;
using System.Linq;
using Reqnroll;

namespace ReqnrollDbTest.Support.Extensions;

public static class DataTableHelper
{
    public static DataTable ApplyDataFilters(DataTable dataTable)
    {
        // Create a new Reqnroll.DataTable with the same headers
        var filteredTable = new DataTable(dataTable.Header.ToArray());

        foreach (var row in dataTable.Rows)
        {
            var newRow = row.Values
                .Select(value =>
                {
                    if (value == null) return null;
                    var replaced = value.Replace("~", " ");
                    return replaced.ToLower() == "null" ? null : replaced;
                })
                .ToArray();

            filteredTable.AddRow(newRow);
        }

        return filteredTable;
    }
}

