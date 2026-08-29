using System.Text.Json;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using FP.Web.Models;

namespace FP.Web.Extensions;

public static class TempDataExtensions
{
    private const string CrudResultKey = "CrudResult";

    public static void SetCrudResult(
        this ITempDataDictionary tempData,
        CrudResultViewModel result)
    {
        tempData[CrudResultKey] = JsonSerializer.Serialize(result);
    }

    public static CrudResultViewModel? GetCrudResult(
        this ITempDataDictionary tempData)
    {
        if (!tempData.TryGetValue(CrudResultKey, out var value))
        {
            return null;
        }

        if (value is not string json)
        {
            return null;
        }

        return JsonSerializer.Deserialize<CrudResultViewModel>(json);
    }
}