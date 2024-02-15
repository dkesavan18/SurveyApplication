
using System.Collections.Generic;
using System.Threading.Tasks;
using SurveyApp.Models;
using SurveyApp.Helpers;
using SurveyApp.Schemas;

namespace SurveyApp.Services;

public class SurveyActivityLogsService : ISurveyActivityLogsService
{
    private readonly IDbService _dbService;

    public SurveyActivityLogsService(IDbService dbService)
    {
        _dbService = dbService;
    }

    public async Task<ResponseObject> CreateSurveyActivityLogs(SurveyActivityLogs surveyactivitylogs)
    {
        ResponseObject respObj = new ResponseObject();
       
        try
        {
            var result =
            await _dbService.EditData(
                "INSERT INTO survey_activity_logs (log_id,start_time,end_time,survey_id,user_action,last_modified) VALUES (@LogId, @StartTime, @EndTime, @SurveyId, @UserAction, @LastModified)",
                surveyactivitylogs);
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

    public async Task<long> GetSurveyActivityLogsCount(RequestFilter filter)
    {
         Util utilities = new Util();
        if(filter == null || String.IsNullOrEmpty(filter.OrderByTable))
        {
            filter.OrderByTable = "SurveyActivityLogs";
            filter.OrderByField = "LogId";
            filter.Sort = "ASC";
        }
        string whereQuery = await utilities.FormWhereQuery(filter);
        string orderQuery = await utilities.FormOrderQuery(filter);
        string countQuery = "select count(*) as count from  survey_activity_logs sal LEFT JOIN surveys surveys on surveys.survey_id = sal.survey_id";
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

    public async Task<dynamic> GetSurveyActivityLogsList(int index,int limit,RequestFilter filter)
    {
        Util utilities = new Util();
        if(filter == null || String.IsNullOrEmpty(filter.OrderByTable))
        {
            filter.OrderByTable = "SurveyActivityLogs";
            filter.OrderByField = "LogId";
            filter.Sort = "ASC";
        }
        string whereQuery = await utilities.FormWhereQuery(filter);
        string orderQuery = await utilities.FormOrderQuery(filter);
        string selectQuery = "select sal.log_id as LogId, sal.start_time as StartTime, sal.end_time as EndTime, sal.survey_id as SurveyId, sal.user_action as UserAction, sal.last_modified as LastModified, surveys.survey_name as SurveyName, surveys.survey_category_id as SurveyCategoryId, surveys.backround_image_id as BackroundImageId, surveys.user_id as UserId, surveys.created_at as CreatedAt, surveys.survey_status as SurveyStatus from survey_activity_logs sal LEFT JOIN surveys surveys on surveys.survey_id = sal.survey_id";
        if(!String.IsNullOrEmpty(whereQuery))
        {
            selectQuery = selectQuery+" where "+whereQuery;
        }
        if(!String.IsNullOrEmpty(orderQuery))
        {
            selectQuery = selectQuery+" "+orderQuery;
        }
        var surveyactivitylogsList = await _dbService.GetAll<dynamic>(selectQuery+" LIMIT "+limit+ "OFFSET "+index, new { });
      
        return surveyactivitylogsList;
    }


    public async Task<dynamic> GetSurveyActivityLogs(long LogId)
    {
        var surveyactivitylogs = await _dbService.GetAsync<dynamic>("select sal.log_id as LogId, sal.start_time as StartTime, sal.end_time as EndTime, sal.survey_id as SurveyId, sal.user_action as UserAction, sal.last_modified as LastModified, surveys.survey_name as SurveyName, surveys.survey_category_id as SurveyCategoryId, surveys.backround_image_id as BackroundImageId, surveys.user_id as UserId, surveys.created_at as CreatedAt, surveys.survey_status as SurveyStatus from survey_activity_logs sal LEFT JOIN surveys surveys on surveys.survey_id = sal.survey_id where sal.log_id=@LogId", new {LogId});
        
        return surveyactivitylogs;
    }

    public async Task<bool> UpdateSurveyActivityLogs(SurveyActivityLogs surveyactivitylogs)
    {
        var updateSurveyActivityLogs =
            await _dbService.EditData(
                "Update surveyactivitylogs SET start_time = @StartTime, end_time = @EndTime, survey_id = @SurveyId, user_action = @UserAction, last_modified = @LastModified WHERE log_id=@LogId",
                surveyactivitylogs);
        return true;
    }

    public async Task<bool> DeleteSurveyActivityLogs(long LogId)
    {
        var deleteSurveyActivityLogs = await _dbService.EditData("DELETE FROM surveyactivitylogs WHERE log_id=@LogId", new {LogId});
        return true;
    }
}
