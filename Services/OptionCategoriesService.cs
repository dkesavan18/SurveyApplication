
using System.Collections.Generic;
using System.Threading.Tasks;
using SurveyApp.Models;
using SurveyApp.Helpers;
using SurveyApp.Schemas;

namespace SurveyApp.Services;

public class OptionCategoriesService : IOptionCategoriesService
{
    private readonly IDbService _dbService;

    public OptionCategoriesService(IDbService dbService)
    {
        _dbService = dbService;
    }

    public async Task<ResponseObject> CreateOptionCategories(OptionCategories optioncategories)
    {
        ResponseObject respObj = new ResponseObject();
       
        try
        {
            var result =
            await _dbService.EditData(
                "INSERT INTO option_categories (option_category_id,option_category,default_choices) VALUES (@OptionCategoryId, @OptionCategory, @DefaultChoices)",
                optioncategories);
            respObj.Error = false;

        }
        catch(Exception e)
        {
            string[] errorMessage = e.Message.Split(":");
            respObj.Message = errorMessage[1];
            respObj.Error = true;
        }
        
        return respObj;
    }

    public async Task<long> GetOptionCategoriesCount(RequestFilter filter)
    {
         Util utilities = new Util();
        if(filter == null || String.IsNullOrEmpty(filter.OrderByTable))
        {
            filter.OrderByTable = "OptionCategories";
            filter.OrderByField = "OptionCategoryId";
            filter.Sort = "ASC";
        }
        string whereQuery = await utilities.FormWhereQuery(filter);
        string orderQuery = await utilities.FormOrderQuery(filter);
        string countQuery = "select count(*) as count from  option_categories oc";
        if(!String.IsNullOrEmpty(whereQuery))
        {
            countQuery = countQuery+" where "+whereQuery;
        }
       
        var count = await _dbService.GetCount(countQuery,new {});
        long countData = 0;
        foreach(var c in count)
        {
            countData = c.count;            
            
        }
        return countData;
    }

    public async Task<dynamic> GetOptionCategoriesList(int index,int limit,RequestFilter filter)
    {
        Util utilities = new Util();
        if(filter == null || String.IsNullOrEmpty(filter.OrderByTable))
        {
            filter.OrderByTable = "OptionCategories";
            filter.OrderByField = "OptionCategoryId";
            filter.Sort = "ASC";
        }
        string whereQuery = await utilities.FormWhereQuery(filter);
        string orderQuery = await utilities.FormOrderQuery(filter);
        string selectQuery = "select oc.option_category_id as OptionCategoryId, oc.option_category as OptionCategory, oc.default_choices as DefaultChoices from option_categories oc";
        if(!String.IsNullOrEmpty(whereQuery))
        {
            selectQuery = selectQuery+" where "+whereQuery;
        }
        if(!String.IsNullOrEmpty(orderQuery))
        {
            selectQuery = selectQuery+" "+orderQuery;
        }
        var optioncategoriesList = await _dbService.GetAll<dynamic>(selectQuery+" LIMIT "+limit+ "OFFSET "+index, new { });
      
        return optioncategoriesList;
    }


    public async Task<dynamic> GetOptionCategories(long OptionCategoryId)
    {
        var optioncategories = await _dbService.GetAsync<dynamic>("select oc.option_category_id as OptionCategoryId, oc.option_category as OptionCategory, oc.default_choices as DefaultChoices from option_categories oc where oc.option_category_id=@OptionCategoryId", new {OptionCategoryId});
        
        return optioncategories;
    }

    public async Task<bool> UpdateOptionCategories(OptionCategories optioncategories)
    {
        var updateOptionCategories =
            await _dbService.EditData(
                "Update optioncategories SET option_category = @OptionCategory, default_choices = @DefaultChoices WHERE option_category_id=@OptionCategoryId",
                optioncategories);
        return true;
    }

    public async Task<bool> DeleteOptionCategories(long OptionCategoryId)
    {
        var deleteOptionCategories = await _dbService.EditData("DELETE FROM optioncategories WHERE option_category_id=@OptionCategoryId", new {OptionCategoryId});
        return true;
    }
}
