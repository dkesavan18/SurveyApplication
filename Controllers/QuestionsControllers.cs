
using System.Threading.Tasks;
using SurveyApp.Models;
using SurveyApp.Services;
using SurveyApp.Schemas;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace SurveyApp.Controllers;

[ApiController]
[Route("questions")]
public class QuestionssController : Controller
{
    private readonly IQuestionsService _questionsService;
    
    public QuestionssController(IQuestionsService questionsService)
    {
        _questionsService = questionsService;
    }

    [HttpPost]
    [Route("list")]
    [Authorize]
    public async Task<QuestionsListResponse> Get([FromBody] RequestFilter request)
    {
        var questionsList =  await _questionsService.GetQuestionsList(request.Index,request.Limit,request);
        var count = await _questionsService.GetQuestionsCount(request);

        QuestionsListResponse respObj = new QuestionsListResponse();   
        
        respObj.count = count;            
            
        
        List<Questions> listData = new List<Questions>();
        foreach(var questions in questionsList)
        {
            Questions questionsObj = new Questions();
            Surveys surveysObj = new Surveys();
            OptionCategories optioncategoriesObj = new OptionCategories();
     
                questionsObj.QuestionId = questions.questionid ?? null; 
                questionsObj.SurveyId = questions.surveyid ?? null; 
                questionsObj.Question = questions.question; 
                questionsObj.QuestionCategory = questions.questioncategory; 
                questionsObj.OptionCategoryId = questions.optioncategoryid ?? null; 
                questionsObj.OptionText = questions.optiontext; 
                questionsObj.QuestionTextFormatting = questions.questiontextformatting;
              
                surveysObj.SurveyId = questions.surveyid ?? null; 
                surveysObj.SurveyName = questions.surveyname; 
                surveysObj.SurveyCategoryId = questions.surveycategoryid ?? null; 
                surveysObj.BackroundImageId = questions.backroundimageid ?? null; 
                surveysObj.UserId = questions.userid ?? null; 
                surveysObj.CreatedAt = questions.createdat; 
                surveysObj.SurveyStatus = questions.surveystatus;
              
                optioncategoriesObj.OptionCategoryId = questions.optioncategoryid ?? null; 
                optioncategoriesObj.OptionCategory = questions.optioncategory; 
                optioncategoriesObj.DefaultChoices = questions.defaultchoices;
    
            questionsObj.Surveys = new Surveys();
            questionsObj.Surveys = surveysObj;
            questionsObj.OptionCategories = new OptionCategories();
            questionsObj.OptionCategories = optioncategoriesObj;
            listData.Add(questionsObj);
        }
        respObj.Questions = new List<Questions>();
        respObj.Questions = listData;
        return respObj;
    }
    
    [HttpGet("{id:long}")]
    [Authorize]
    public async Task<Questions> GetQuestions(long id)
    {
        var questions =  await _questionsService.GetQuestions(id);
        Questions questionsObj = new Questions();
            Surveys surveysObj = new Surveys();
            OptionCategories optioncategoriesObj = new OptionCategories();
     
                questionsObj.QuestionId = questions.questionid ?? null; 
                questionsObj.SurveyId = questions.surveyid ?? null; 
                questionsObj.Question = questions.question; 
                questionsObj.QuestionCategory = questions.questioncategory; 
                questionsObj.OptionCategoryId = questions.optioncategoryid ?? null; 
                questionsObj.OptionText = questions.optiontext; 
                questionsObj.QuestionTextFormatting = questions.questiontextformatting;
              
                surveysObj.SurveyId = questions.surveyid ?? null; 
                surveysObj.SurveyName = questions.surveyname; 
                surveysObj.SurveyCategoryId = questions.surveycategoryid ?? null; 
                surveysObj.BackroundImageId = questions.backroundimageid ?? null; 
                surveysObj.UserId = questions.userid ?? null; 
                surveysObj.CreatedAt = questions.createdat; 
                surveysObj.SurveyStatus = questions.surveystatus;
              
                optioncategoriesObj.OptionCategoryId = questions.optioncategoryid ?? null; 
                optioncategoriesObj.OptionCategory = questions.optioncategory; 
                optioncategoriesObj.DefaultChoices = questions.defaultchoices;
    
            questionsObj.Surveys = new Surveys();
            questionsObj.Surveys = surveysObj;
            questionsObj.OptionCategories = new OptionCategories();
            questionsObj.OptionCategories = optioncategoriesObj;
        return questionsObj;
    }
    
    [HttpPost]
    [Authorize]
    public async Task<IActionResult> AddQuestions([FromBody]Questions questions)
    {
        var result =  await _questionsService.CreateQuestions(questions);

        return Ok(result);
    }
    
    [HttpPut]
    [Authorize]
    public async Task<IActionResult> UpdateQuestions([FromBody]Questions questions)
    {
        var result =  await _questionsService.UpdateQuestions(questions);

        return Ok(result);
    }
    
    [HttpDelete("{id:long}")]
    [Authorize]
    public async Task<IActionResult> DeleteQuestions(long id)
    {
        var result =  await _questionsService.DeleteQuestions(id);

        return Ok(result);
    }
}
