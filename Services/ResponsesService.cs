
using System.Collections.Generic;
using System.Threading.Tasks;
using SurveyApp.Models;
using SurveyApp.Helpers;
using SurveyApp.Schemas;

namespace SurveyApp.Services;

public class ResponsesService : IResponsesService
{
    private readonly IDbService _dbService;

    public ResponsesService(IDbService dbService)
    {
        _dbService = dbService;
    }

    public async Task<ResponseObject> CreateResponses(Responses responses)
    {
        ResponseObject respObj = new ResponseObject();
       
        try
        {
            var result =
            await _dbService.EditData(
                "INSERT INTO responses (response_id,respondent_id,survey_id,total_score) VALUES (@ResponseId, @RespondentId, @SurveyId, @TotalScore)",
                responses);
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

    public async Task<long> GetResponsesCount(RequestFilter filter)
    {
         Util utilities = new Util();
        if(filter == null || String.IsNullOrEmpty(filter.OrderByTable))
        {
            filter.OrderByTable = "Responses";
            filter.OrderByField = "ResponseId";
            filter.Sort = "ASC";
        }
        string whereQuery = await utilities.FormWhereQuery(filter);
        string orderQuery = await utilities.FormOrderQuery(filter);
        string countQuery = "select count(*) as count from  responses responses LEFT JOIN users users on users.user_id = responses.respondent_id LEFT JOIN surveys surveys on surveys.survey_id = responses.survey_id";
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

    public async Task<dynamic> GetResponsesList(int index,int limit,RequestFilter filter)
    {
        Util utilities = new Util();
        if(filter == null || String.IsNullOrEmpty(filter.OrderByTable))
        {
            filter.OrderByTable = "Responses";
            filter.OrderByField = "ResponseId";
            filter.Sort = "ASC";
        }
        string whereQuery = await utilities.FormWhereQuery(filter);
        string orderQuery = await utilities.FormOrderQuery(filter);
        string selectQuery = "select responses.response_id as ResponseId, responses.respondent_id as RespondentId, responses.survey_id as SurveyId, responses.total_score as TotalScore, users.user_id as UserId, users.user_role as UserRole, users.user_name as UserName, users.user_email as UserEmail, users.user_mobile_no as UserMobileNo, users.user_password as UserPassword, users.user_gender as UserGender, users.user_date_of_birth as UserDateOfBirth, users.password_salt as PasswordSalt, users.marital_status as MaritalStatus, users.user_experience as UserExperience, users.user_city as UserCity, users.user_department as UserDepartment, users.builder_user_id as BuilderUserId, surveys.survey_name as SurveyName, surveys.survey_category_id as SurveyCategoryId, surveys.backround_image_id as BackroundImageId, surveys.created_at as CreatedAt, surveys.survey_status as SurveyStatus from responses responses LEFT JOIN users users on users.user_id = responses.respondent_id LEFT JOIN surveys surveys on surveys.survey_id = responses.survey_id";
        if(!String.IsNullOrEmpty(whereQuery))
        {
            selectQuery = selectQuery+" where "+whereQuery;
        }
        if(!String.IsNullOrEmpty(orderQuery))
        {
            selectQuery = selectQuery+" "+orderQuery;
        }
        var responsesList = await _dbService.GetAll<dynamic>(selectQuery+" LIMIT "+limit+ "OFFSET "+index, new { });
      
        return responsesList;
    }


    public async Task<dynamic> GetResponses(long ResponseId)
    {
        var responses = await _dbService.GetAsync<dynamic>("select responses.response_id as ResponseId, responses.respondent_id as RespondentId, responses.survey_id as SurveyId, responses.total_score as TotalScore, users.user_id as UserId, users.user_role as UserRole, users.user_name as UserName, users.user_email as UserEmail, users.user_mobile_no as UserMobileNo, users.user_password as UserPassword, users.user_gender as UserGender, users.user_date_of_birth as UserDateOfBirth, users.password_salt as PasswordSalt, users.marital_status as MaritalStatus, users.user_experience as UserExperience, users.user_city as UserCity, users.user_department as UserDepartment, users.builder_user_id as BuilderUserId, surveys.survey_name as SurveyName, surveys.survey_category_id as SurveyCategoryId, surveys.backround_image_id as BackroundImageId, surveys.created_at as CreatedAt, surveys.survey_status as SurveyStatus from responses responses LEFT JOIN users users on users.user_id = responses.respondent_id LEFT JOIN surveys surveys on surveys.survey_id = responses.survey_id where responses.response_id=@ResponseId", new {ResponseId});
        
        return responses;
    }

    public async Task<bool> UpdateResponses(Responses responses)
    {
        var updateResponses =
            await _dbService.EditData(
                "Update responses SET respondent_id = @RespondentId, survey_id = @SurveyId, total_score = @TotalScore WHERE response_id=@ResponseId",
                responses);
        return true;
    }

    public async Task<bool> DeleteResponses(long ResponseId)
    {
        var deleteResponses = await _dbService.EditData("DELETE FROM responses WHERE response_id=@ResponseId", new {ResponseId});
        return true;
    }
}
