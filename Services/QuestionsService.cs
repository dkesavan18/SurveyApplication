
using System.Collections.Generic;
using System.Threading.Tasks;
using SurveyApp.Models;
using SurveyApp.Helpers;
using SurveyApp.Schemas;

namespace SurveyApp.Services;

public class QuestionsService : IQuestionsService
{
    private readonly IDbService _dbService;

    public QuestionsService(IDbService dbService)
    {
        _dbService = dbService;
    }

    public async Task<ResponseObject> CreateQuestions(Questions questions)
    {
        ResponseObject respObj = new ResponseObject();
       
        try
        {
            var result =
            await _dbService.EditData(
                "INSERT INTO questions (question_id,survey_id,question,question_category,option_category_id,option_text,question_text_formatting) VALUES (@QuestionId, @SurveyId, @Question, @QuestionCategory, @OptionCategoryId, @OptionText, @QuestionTextFormatting)",
                questions);
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

    public async Task<long> GetQuestionsCount(RequestFilter filter)
    {
         Util utilities = new Util();
        if(filter == null || String.IsNullOrEmpty(filter.OrderByTable))
        {
            filter.OrderByTable = "Questions";
            filter.OrderByField = "QuestionId";
            filter.Sort = "ASC";
        }
        string whereQuery = await utilities.FormWhereQuery(filter);
        string orderQuery = await utilities.FormOrderQuery(filter);
        string countQuery = "select count(*) as count from  questions questions LEFT JOIN surveys surveys on surveys.survey_id = questions.survey_id LEFT JOIN option_categories oc on oc.option_category_id = questions.option_category_id";
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

    public async Task<dynamic> GetQuestionsList(int index,int limit,RequestFilter filter)
    {
        Util utilities = new Util();
        if(filter == null || String.IsNullOrEmpty(filter.OrderByTable))
        {
            filter.OrderByTable = "Questions";
            filter.OrderByField = "QuestionId";
            filter.Sort = "ASC";
        }
        string whereQuery = await utilities.FormWhereQuery(filter);
        string orderQuery = await utilities.FormOrderQuery(filter);
        string selectQuery = "select questions.question_id as QuestionId, questions.survey_id as SurveyId, questions.question as Question, questions.question_category as QuestionCategory, questions.option_category_id as OptionCategoryId, questions.option_text as OptionText, questions.question_text_formatting as QuestionTextFormatting, surveys.survey_name as SurveyName, surveys.survey_category_id as SurveyCategoryId, surveys.backround_image_id as BackroundImageId, surveys.user_id as UserId, surveys.created_at as CreatedAt, surveys.survey_status as SurveyStatus, oc.option_category as OptionCategory, oc.default_choices as DefaultChoices from questions questions LEFT JOIN surveys surveys on surveys.survey_id = questions.survey_id LEFT JOIN option_categories oc on oc.option_category_id = questions.option_category_id";
        if(!String.IsNullOrEmpty(whereQuery))
        {
            selectQuery = selectQuery+" where "+whereQuery;
        }
        if(!String.IsNullOrEmpty(orderQuery))
        {
            selectQuery = selectQuery+" "+orderQuery;
        }
        var questionsList = await _dbService.GetAll<dynamic>(selectQuery+" LIMIT "+limit+ "OFFSET "+index, new { });
      
        return questionsList;
    }


    public async Task<dynamic> GetQuestions(long QuestionId)
    {
        var questions = await _dbService.GetAsync<dynamic>("select questions.question_id as QuestionId, questions.survey_id as SurveyId, questions.question as Question, questions.question_category as QuestionCategory, questions.option_category_id as OptionCategoryId, questions.option_text as OptionText, questions.question_text_formatting as QuestionTextFormatting, surveys.survey_name as SurveyName, surveys.survey_category_id as SurveyCategoryId, surveys.backround_image_id as BackroundImageId, surveys.user_id as UserId, surveys.created_at as CreatedAt, surveys.survey_status as SurveyStatus, oc.option_category as OptionCategory, oc.default_choices as DefaultChoices from questions questions LEFT JOIN surveys surveys on surveys.survey_id = questions.survey_id LEFT JOIN option_categories oc on oc.option_category_id = questions.option_category_id where questions.question_id=@QuestionId", new {QuestionId});
        
        return questions;
    }

    public async Task<bool> UpdateQuestions(Questions questions)
    {
        var updateQuestions =
            await _dbService.EditData(
                "Update questions SET survey_id = @SurveyId, question = @Question, question_category = @QuestionCategory, option_category_id = @OptionCategoryId, option_text = @OptionText, question_text_formatting = @QuestionTextFormatting WHERE question_id=@QuestionId",
                questions);
        return true;
    }

    public async Task<bool> DeleteQuestions(long QuestionId)
    {
        var deleteQuestions = await _dbService.EditData("DELETE FROM questions WHERE question_id=@QuestionId", new {QuestionId});
        return true;
    }
}
