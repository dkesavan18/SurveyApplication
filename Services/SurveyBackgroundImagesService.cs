
using System.Collections.Generic;
using System.Threading.Tasks;
using SurveyApp.Models;
using SurveyApp.Helpers;
using SurveyApp.Schemas;

namespace SurveyApp.Services;

public class SurveyBackgroundImagesService : ISurveyBackgroundImagesService
{
    private readonly IDbService _dbService;

    public SurveyBackgroundImagesService(IDbService dbService)
    {
        _dbService = dbService;
    }

    public async Task<ResponseObject> CreateSurveyBackgroundImages(SurveyBackgroundImages surveybackgroundimages)
    {
        ResponseObject respObj = new ResponseObject();
       
        try
        {
            var result =
            await _dbService.EditData(
                "INSERT INTO survey_background_images (survey_background_id,background_image) VALUES (@SurveyBackgroundId, @BackgroundImage)",
                surveybackgroundimages);
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

    public async Task<long> GetSurveyBackgroundImagesCount(RequestFilter filter)
    {
         Util utilities = new Util();
        if(filter == null || String.IsNullOrEmpty(filter.OrderByTable))
        {
            filter.OrderByTable = "SurveyBackgroundImages";
            filter.OrderByField = "SurveyBackgroundId";
            filter.Sort = "ASC";
        }
        string whereQuery = await utilities.FormWhereQuery(filter);
        string orderQuery = await utilities.FormOrderQuery(filter);
        string countQuery = "select count(*) as count from  survey_background_images sbi";
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

    public async Task<dynamic> GetSurveyBackgroundImagesList(int index,int limit,RequestFilter filter)
    {
        Util utilities = new Util();
        if(filter == null || String.IsNullOrEmpty(filter.OrderByTable))
        {
            filter.OrderByTable = "SurveyBackgroundImages";
            filter.OrderByField = "SurveyBackgroundId";
            filter.Sort = "ASC";
        }
        string whereQuery = await utilities.FormWhereQuery(filter);
        string orderQuery = await utilities.FormOrderQuery(filter);
        string selectQuery = "select sbi.survey_background_id as SurveyBackgroundId, sbi.background_image as BackgroundImage from survey_background_images sbi";
        if(!String.IsNullOrEmpty(whereQuery))
        {
            selectQuery = selectQuery+" where "+whereQuery;
        }
        if(!String.IsNullOrEmpty(orderQuery))
        {
            selectQuery = selectQuery+" "+orderQuery;
        }
        var surveybackgroundimagesList = await _dbService.GetAll<dynamic>(selectQuery+" LIMIT "+limit+ "OFFSET "+index, new { });
      
        return surveybackgroundimagesList;
    }


    public async Task<dynamic> GetSurveyBackgroundImages(long SurveyBackgroundId)
    {
        var surveybackgroundimages = await _dbService.GetAsync<dynamic>("select sbi.survey_background_id as SurveyBackgroundId, sbi.background_image as BackgroundImage from survey_background_images sbi where sbi.survey_background_id=@SurveyBackgroundId", new {SurveyBackgroundId});
        
        return surveybackgroundimages;
    }

    public async Task<bool> UpdateSurveyBackgroundImages(SurveyBackgroundImages surveybackgroundimages)
    {
        var updateSurveyBackgroundImages =
            await _dbService.EditData(
                "Update surveybackgroundimages SET background_image = @BackgroundImage WHERE survey_background_id=@SurveyBackgroundId",
                surveybackgroundimages);
        return true;
    }

    public async Task<bool> DeleteSurveyBackgroundImages(long SurveyBackgroundId)
    {
        var deleteSurveyBackgroundImages = await _dbService.EditData("DELETE FROM surveybackgroundimages WHERE survey_background_id=@SurveyBackgroundId", new {SurveyBackgroundId});
        return true;
    }
}
