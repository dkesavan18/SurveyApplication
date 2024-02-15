
using System.Threading.Tasks;
using SurveyApp.Models;
using SurveyApp.Services;
using SurveyApp.Schemas;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace SurveyApp.Controllers;

[ApiController]
[Route("questionsuggestions")]
public class QuestionSuggestionssController : Controller
{
    private readonly IQuestionSuggestionsService _questionsuggestionsService;
    
    public QuestionSuggestionssController(IQuestionSuggestionsService questionsuggestionsService)
    {
        _questionsuggestionsService = questionsuggestionsService;
    }

    [HttpPost]
    [Route("list")]
    [Authorize]
    public async Task<QuestionSuggestionsListResponse> Get([FromBody] RequestFilter request)
    {
        var questionsuggestionsList =  await _questionsuggestionsService.GetQuestionSuggestionsList(request.Index,request.Limit,request);
        var count = await _questionsuggestionsService.GetQuestionSuggestionsCount(request);

        QuestionSuggestionsListResponse respObj = new QuestionSuggestionsListResponse();   
        
        respObj.count = count;            
            
        
        List<QuestionSuggestions> listData = new List<QuestionSuggestions>();
        foreach(var questionsuggestions in questionsuggestionsList)
        {
            QuestionSuggestions questionsuggestionsObj = new QuestionSuggestions();
            SurveyCategories surveycategoriesObj = new SurveyCategories();
     
                questionsuggestionsObj.SuggestedQuestionId = questionsuggestions.suggestedquestionid ?? null; 
                questionsuggestionsObj.SuggestedQuestion = questionsuggestions.suggestedquestion; 
                questionsuggestionsObj.SurveyCategoryId = questionsuggestions.surveycategoryid ?? null;
              
                surveycategoriesObj.SurveyCategoryId = questionsuggestions.surveycategoryid ?? null; 
                surveycategoriesObj.SurveyCategory = questionsuggestions.surveycategory;
    
            questionsuggestionsObj.SurveyCategories = new SurveyCategories();
            questionsuggestionsObj.SurveyCategories = surveycategoriesObj;
            listData.Add(questionsuggestionsObj);
        }
        respObj.QuestionSuggestions = new List<QuestionSuggestions>();
        respObj.QuestionSuggestions = listData;
        return respObj;
    }
    
    [HttpGet("{id:long}")]
    [Authorize]
    public async Task<QuestionSuggestions> GetQuestionSuggestions(long id)
    {
        var questionsuggestions =  await _questionsuggestionsService.GetQuestionSuggestions(id);
        QuestionSuggestions questionsuggestionsObj = new QuestionSuggestions();
            SurveyCategories surveycategoriesObj = new SurveyCategories();
     
                questionsuggestionsObj.SuggestedQuestionId = questionsuggestions.suggestedquestionid ?? null; 
                questionsuggestionsObj.SuggestedQuestion = questionsuggestions.suggestedquestion; 
                questionsuggestionsObj.SurveyCategoryId = questionsuggestions.surveycategoryid ?? null;
              
                surveycategoriesObj.SurveyCategoryId = questionsuggestions.surveycategoryid ?? null; 
                surveycategoriesObj.SurveyCategory = questionsuggestions.surveycategory;
    
            questionsuggestionsObj.SurveyCategories = new SurveyCategories();
            questionsuggestionsObj.SurveyCategories = surveycategoriesObj;
        return questionsuggestionsObj;
    }
    
    [HttpPost]
    [Authorize]
    public async Task<IActionResult> AddQuestionSuggestions([FromBody]QuestionSuggestions questionsuggestions)
    {
        var result =  await _questionsuggestionsService.CreateQuestionSuggestions(questionsuggestions);

        return Ok(result);
    }
    
    [HttpPut]
    [Authorize]
    public async Task<IActionResult> UpdateQuestionSuggestions([FromBody]QuestionSuggestions questionsuggestions)
    {
        var result =  await _questionsuggestionsService.UpdateQuestionSuggestions(questionsuggestions);

        return Ok(result);
    }
    
    [HttpDelete("{id:long}")]
    [Authorize]
    public async Task<IActionResult> DeleteQuestionSuggestions(long id)
    {
        var result =  await _questionsuggestionsService.DeleteQuestionSuggestions(id);

        return Ok(result);
    }
}
