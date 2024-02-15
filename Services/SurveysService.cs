
using System.Collections.Generic;
using System.Threading.Tasks;
using SurveyApp.Models;
using SurveyApp.Helpers;
using SurveyApp.Schemas;

namespace SurveyApp.Services;

public class SurveysService : ISurveysService
{
    private readonly IDbService _dbService;

    public SurveysService(IDbService dbService)
    {
        _dbService = dbService;
    }

    public async Task<ResponseObject> CreateSurveys(Surveys surveys)
    {
        ResponseObject respObj = new ResponseObject();
       
        try
        {
            var result =
            await _dbService.EditData(
                "INSERT INTO surveys (survey_id,survey_name,survey_category_id,backround_image_id,user_id,created_at,survey_status) VALUES (@SurveyId, @SurveyName, @SurveyCategoryId, @BackroundImageId, @UserId, @CreatedAt, @SurveyStatus)",
                surveys);
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

    public async Task<long> GetSurveysCount(RequestFilter filter)
    {
         Util utilities = new Util();
        if(filter == null || String.IsNullOrEmpty(filter.OrderByTable))
        {
            filter.OrderByTable = "Surveys";
            filter.OrderByField = "SurveyId";
            filter.Sort = "ASC";
        }
        string whereQuery = await utilities.FormWhereQuery(filter);
        string orderQuery = await utilities.FormOrderQuery(filter);
        string countQuery = "select count(*) as count from  surveys surveys LEFT JOIN survey_categories sc on sc.survey_category_id = surveys.survey_category_id LEFT JOIN survey_background_images sbi on sbi.survey_background_id = surveys.backround_image_id LEFT JOIN users users on users.user_id = surveys.user_id";
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

    public async Task<dynamic> GetSurveysList(int index,int limit,RequestFilter filter)
    {
        Util utilities = new Util();
        if(filter == null || String.IsNullOrEmpty(filter.OrderByTable))
        {
            filter.OrderByTable = "Surveys";
            filter.OrderByField = "SurveyId";
            filter.Sort = "ASC";
        }
        string whereQuery = await utilities.FormWhereQuery(filter);
        string orderQuery = await utilities.FormOrderQuery(filter);
        string selectQuery = "select surveys.survey_id as SurveyId, surveys.survey_name as SurveyName, surveys.survey_category_id as SurveyCategoryId, surveys.backround_image_id as BackroundImageId, surveys.user_id as UserId, surveys.created_at as CreatedAt, surveys.survey_status as SurveyStatus, sc.survey_category as SurveyCategory, sbi.survey_background_id as SurveyBackgroundId, sbi.background_image as BackgroundImage, users.user_role as UserRole, users.user_name as UserName, users.user_email as UserEmail, users.user_mobile_no as UserMobileNo, users.user_password as UserPassword, users.user_gender as UserGender, users.user_date_of_birth as UserDateOfBirth, users.password_salt as PasswordSalt, users.marital_status as MaritalStatus, users.user_experience as UserExperience, users.user_city as UserCity, users.user_department as UserDepartment, users.builder_user_id as BuilderUserId from surveys surveys LEFT JOIN survey_categories sc on sc.survey_category_id = surveys.survey_category_id LEFT JOIN survey_background_images sbi on sbi.survey_background_id = surveys.backround_image_id LEFT JOIN users users on users.user_id = surveys.user_id";
        if(!String.IsNullOrEmpty(whereQuery))
        {
            selectQuery = selectQuery+" where "+whereQuery;
        }
        if(!String.IsNullOrEmpty(orderQuery))
        {
            selectQuery = selectQuery+" "+orderQuery;
        }
        var surveysList = await _dbService.GetAll<dynamic>(selectQuery+" LIMIT "+limit+ "OFFSET "+index, new { });
      
        return surveysList;
    }


    public async Task<dynamic> GetSurveys(long SurveyId)
    {
        var surveys = await _dbService.GetAsync<dynamic>("select surveys.survey_id as SurveyId, surveys.survey_name as SurveyName, surveys.survey_category_id as SurveyCategoryId, surveys.backround_image_id as BackroundImageId, surveys.user_id as UserId, surveys.created_at as CreatedAt, surveys.survey_status as SurveyStatus, sc.survey_category as SurveyCategory, sbi.survey_background_id as SurveyBackgroundId, sbi.background_image as BackgroundImage, users.user_role as UserRole, users.user_name as UserName, users.user_email as UserEmail, users.user_mobile_no as UserMobileNo, users.user_password as UserPassword, users.user_gender as UserGender, users.user_date_of_birth as UserDateOfBirth, users.password_salt as PasswordSalt, users.marital_status as MaritalStatus, users.user_experience as UserExperience, users.user_city as UserCity, users.user_department as UserDepartment, users.builder_user_id as BuilderUserId from surveys surveys LEFT JOIN survey_categories sc on sc.survey_category_id = surveys.survey_category_id LEFT JOIN survey_background_images sbi on sbi.survey_background_id = surveys.backround_image_id LEFT JOIN users users on users.user_id = surveys.user_id where surveys.survey_id=@SurveyId", new {SurveyId});
        
        return surveys;
    }

    public async Task<bool> UpdateSurveys(Surveys surveys)
    {
        var updateSurveys =
            await _dbService.EditData(
                "Update surveys SET survey_name = @SurveyName, survey_category_id = @SurveyCategoryId, backround_image_id = @BackroundImageId, user_id = @UserId, created_at = @CreatedAt, survey_status = @SurveyStatus WHERE survey_id=@SurveyId",
                surveys);
        return true;
    }

    public async Task<bool> DeleteSurveys(long SurveyId)
    {
        var deleteSurveys = await _dbService.EditData("DELETE FROM surveys WHERE survey_id=@SurveyId", new {SurveyId});
        return true;
    }
}
