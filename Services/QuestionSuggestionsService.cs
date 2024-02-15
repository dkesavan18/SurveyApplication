
using System.Collections.Generic;
using System.Threading.Tasks;
using SurveyApp.Models;
using SurveyApp.Helpers;
using SurveyApp.Schemas;

namespace SurveyApp.Services;

public class QuestionSuggestionsService : IQuestionSuggestionsService
{
    private readonly IDbService _dbService;

    public QuestionSuggestionsService(IDbService dbService)
    {
        _dbService = dbService;
    }

    public async Task<ResponseObject> CreateQuestionSuggestions(QuestionSuggestions questionsuggestions)
    {
        ResponseObject respObj = new ResponseObject();
       
        try
        {
            var result =
            await _dbService.EditData(
                "INSERT INTO question_suggestions (suggested_question_id,suggested_question,survey_category_id) VALUES (@SuggestedQuestionId, @SuggestedQuestion, @SurveyCategoryId)",
                questionsuggestions);
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

    public async Task<long> GetQuestionSuggestionsCount(RequestFilter filter)
    {
         Util utilities = new Util();
        if(filter == null || String.IsNullOrEmpty(filter.OrderByTable))
        {
            filter.OrderByTable = "QuestionSuggestions";
            filter.OrderByField = "SuggestedQuestionId";
            filter.Sort = "ASC";
        }
        string whereQuery = await utilities.FormWhereQuery(filter);
        string orderQuery = await utilities.FormOrderQuery(filter);
        string countQuery = "select count(*) as count from  question_suggestions qs LEFT JOIN survey_categories sc on sc.survey_category_id = qs.survey_category_id";
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

    public async Task<dynamic> GetQuestionSuggestionsList(int index,int limit,RequestFilter filter)
    {
        Util utilities = new Util();
        if(filter == null || String.IsNullOrEmpty(filter.OrderByTable))
        {
            filter.OrderByTable = "QuestionSuggestions";
            filter.OrderByField = "SuggestedQuestionId";
            filter.Sort = "ASC";
        }
        string whereQuery = await utilities.FormWhereQuery(filter);
        string orderQuery = await utilities.FormOrderQuery(filter);
        string selectQuery = "select qs.suggested_question_id as SuggestedQuestionId, qs.suggested_question as SuggestedQuestion, qs.survey_category_id as SurveyCategoryId, sc.survey_category as SurveyCategory from question_suggestions qs LEFT JOIN survey_categories sc on sc.survey_category_id = qs.survey_category_id";
        if(!String.IsNullOrEmpty(whereQuery))
        {
            selectQuery = selectQuery+" where "+whereQuery;
        }
        if(!String.IsNullOrEmpty(orderQuery))
        {
            selectQuery = selectQuery+" "+orderQuery;
        }
        var questionsuggestionsList = await _dbService.GetAll<dynamic>(selectQuery+" LIMIT "+limit+ "OFFSET "+index, new { });
      
        return questionsuggestionsList;
    }


    public async Task<dynamic> GetQuestionSuggestions(long SuggestedQuestionId)
    {
        var questionsuggestions = await _dbService.GetAsync<dynamic>("select qs.suggested_question_id as SuggestedQuestionId, qs.suggested_question as SuggestedQuestion, qs.survey_category_id as SurveyCategoryId, sc.survey_category as SurveyCategory from question_suggestions qs LEFT JOIN survey_categories sc on sc.survey_category_id = qs.survey_category_id where qs.suggested_question_id=@SuggestedQuestionId", new {SuggestedQuestionId});
        
        return questionsuggestions;
    }

    public async Task<bool> UpdateQuestionSuggestions(QuestionSuggestions questionsuggestions)
    {
        var updateQuestionSuggestions =
            await _dbService.EditData(
                "Update questionsuggestions SET suggested_question = @SuggestedQuestion, survey_category_id = @SurveyCategoryId WHERE suggested_question_id=@SuggestedQuestionId",
                questionsuggestions);
        return true;
    }

    public async Task<bool> DeleteQuestionSuggestions(long SuggestedQuestionId)
    {
        var deleteQuestionSuggestions = await _dbService.EditData("DELETE FROM questionsuggestions WHERE suggested_question_id=@SuggestedQuestionId", new {SuggestedQuestionId});
        return true;
    }
}
