
using System.Collections.Generic;
using System.Threading.Tasks;
using SurveyApp.Models;
using SurveyApp.Helpers;
using SurveyApp.Schemas;

namespace SurveyApp.Services;

public class SurveyCategoriesService : ISurveyCategoriesService
{
    private readonly IDbService _dbService;

    public SurveyCategoriesService(IDbService dbService)
    {
        _dbService = dbService;
    }

    public async Task<ResponseObject> CreateSurveyCategories(SurveyCategories surveycategories)
    {
        ResponseObject respObj = new ResponseObject();
       
        try
        {
            var result =
            await _dbService.EditData(
                "INSERT INTO survey_categories (survey_category_id,survey_category) VALUES (@SurveyCategoryId, @SurveyCategory)",
                surveycategories);
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

    public async Task<long> GetSurveyCategoriesCount(RequestFilter filter)
    {
         Util utilities = new Util();
        if(filter == null || String.IsNullOrEmpty(filter.OrderByTable))
        {
            filter.OrderByTable = "SurveyCategories";
            filter.OrderByField = "SurveyCategoryId";
            filter.Sort = "ASC";
        }
        string whereQuery = await utilities.FormWhereQuery(filter);
        string orderQuery = await utilities.FormOrderQuery(filter);
        string countQuery = "select count(*) as count from  survey_categories sc";
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

    public async Task<dynamic> GetSurveyCategoriesList(int index,int limit,RequestFilter filter)
    {
        Util utilities = new Util();
        if(filter == null || String.IsNullOrEmpty(filter.OrderByTable))
        {
            filter.OrderByTable = "SurveyCategories";
            filter.OrderByField = "SurveyCategoryId";
            filter.Sort = "ASC";
        }
        string whereQuery = await utilities.FormWhereQuery(filter);
        string orderQuery = await utilities.FormOrderQuery(filter);
        string selectQuery = "select sc.survey_category_id as SurveyCategoryId, sc.survey_category as SurveyCategory from survey_categories sc";
        if(!String.IsNullOrEmpty(whereQuery))
        {
            selectQuery = selectQuery+" where "+whereQuery;
        }
        if(!String.IsNullOrEmpty(orderQuery))
        {
            selectQuery = selectQuery+" "+orderQuery;
        }
        var surveycategoriesList = await _dbService.GetAll<dynamic>(selectQuery+" LIMIT "+limit+ "OFFSET "+index, new { });
      
        return surveycategoriesList;
    }


    public async Task<dynamic> GetSurveyCategories(long SurveyCategoryId)
    {
        var surveycategories = await _dbService.GetAsync<dynamic>("select sc.survey_category_id as SurveyCategoryId, sc.survey_category as SurveyCategory from survey_categories sc where sc.survey_category_id=@SurveyCategoryId", new {SurveyCategoryId});
        
        return surveycategories;
    }

    public async Task<bool> UpdateSurveyCategories(SurveyCategories surveycategories)
    {
        var updateSurveyCategories =
            await _dbService.EditData(
                "Update surveycategories SET survey_category = @SurveyCategory WHERE survey_category_id=@SurveyCategoryId",
                surveycategories);
        return true;
    }

    public async Task<bool> DeleteSurveyCategories(long SurveyCategoryId)
    {
        var deleteSurveyCategories = await _dbService.EditData("DELETE FROM surveycategories WHERE survey_category_id=@SurveyCategoryId", new {SurveyCategoryId});
        return true;
    }
}
