
using System.Collections.Generic;
using System.Threading.Tasks;
using SurveyApp.Models;
using SurveyApp.Helpers;
using SurveyApp.Schemas;

namespace SurveyApp.Services;

public class SurveyResponsesService : ISurveyResponsesService
{
    private readonly IDbService _dbService;

    public SurveyResponsesService(IDbService dbService)
    {
        _dbService = dbService;
    }

    public async Task<ResponseObject> CreateSurveyResponses(SurveyResponses surveyresponses)
    {
        ResponseObject respObj = new ResponseObject();
       
        try
        {
            var result =
            await _dbService.EditData(
                "INSERT INTO survey_responses (survey_response_id,response_id,question_id,option) VALUES (@SurveyResponseId, @ResponseId, @QuestionId, @Option)",
                surveyresponses);
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

    public async Task<long> GetSurveyResponsesCount(RequestFilter filter)
    {
         Util utilities = new Util();
        if(filter == null || String.IsNullOrEmpty(filter.OrderByTable))
        {
            filter.OrderByTable = "SurveyResponses";
            filter.OrderByField = "SurveyResponseId";
            filter.Sort = "ASC";
        }
        string whereQuery = await utilities.FormWhereQuery(filter);
        string orderQuery = await utilities.FormOrderQuery(filter);
        string countQuery = "select count(*) as count from  survey_responses sr LEFT JOIN questions questions on questions.question_id = sr.question_id";
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

    public async Task<dynamic> GetSurveyResponsesList(int index,int limit,RequestFilter filter)
    {
        Util utilities = new Util();
        if(filter == null || String.IsNullOrEmpty(filter.OrderByTable))
        {
            filter.OrderByTable = "SurveyResponses";
            filter.OrderByField = "SurveyResponseId";
            filter.Sort = "ASC";
        }
        string whereQuery = await utilities.FormWhereQuery(filter);
        string orderQuery = await utilities.FormOrderQuery(filter);
        string selectQuery = "select sr.survey_response_id as SurveyResponseId, sr.response_id as ResponseId, sr.question_id as QuestionId, sr.option as Option, questions.survey_id as SurveyId, questions.question as Question, questions.question_category as QuestionCategory, questions.option_category_id as OptionCategoryId, questions.option_text as OptionText, questions.question_text_formatting as QuestionTextFormatting from survey_responses sr LEFT JOIN questions questions on questions.question_id = sr.question_id";
        if(!String.IsNullOrEmpty(whereQuery))
        {
            selectQuery = selectQuery+" where "+whereQuery;
        }
        if(!String.IsNullOrEmpty(orderQuery))
        {
            selectQuery = selectQuery+" "+orderQuery;
        }
        var surveyresponsesList = await _dbService.GetAll<dynamic>(selectQuery+" LIMIT "+limit+ "OFFSET "+index, new { });
      
        return surveyresponsesList;
    }


    public async Task<dynamic> GetSurveyResponses(long SurveyResponseId)
    {
        var surveyresponses = await _dbService.GetAsync<dynamic>("select sr.survey_response_id as SurveyResponseId, sr.response_id as ResponseId, sr.question_id as QuestionId, sr.option as Option, questions.survey_id as SurveyId, questions.question as Question, questions.question_category as QuestionCategory, questions.option_category_id as OptionCategoryId, questions.option_text as OptionText, questions.question_text_formatting as QuestionTextFormatting from survey_responses sr LEFT JOIN questions questions on questions.question_id = sr.question_id where sr.survey_response_id=@SurveyResponseId", new {SurveyResponseId});
        
        return surveyresponses;
    }

    public async Task<bool> UpdateSurveyResponses(SurveyResponses surveyresponses)
    {
        var updateSurveyResponses =
            await _dbService.EditData(
                "Update surveyresponses SET response_id = @ResponseId, question_id = @QuestionId, option = @Option WHERE survey_response_id=@SurveyResponseId",
                surveyresponses);
        return true;
    }

    public async Task<bool> DeleteSurveyResponses(long SurveyResponseId)
    {
        var deleteSurveyResponses = await _dbService.EditData("DELETE FROM surveyresponses WHERE survey_response_id=@SurveyResponseId", new {SurveyResponseId});
        return true;
    }
}
