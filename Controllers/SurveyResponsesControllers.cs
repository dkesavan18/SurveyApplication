
using System.Threading.Tasks;
using SurveyApp.Models;
using SurveyApp.Services;
using SurveyApp.Schemas;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace SurveyApp.Controllers;

[ApiController]
[Route("surveyresponses")]
public class SurveyResponsessController : Controller
{
    private readonly ISurveyResponsesService _surveyresponsesService;
    
    public SurveyResponsessController(ISurveyResponsesService surveyresponsesService)
    {
        _surveyresponsesService = surveyresponsesService;
    }

    [HttpPost]
    [Route("list")]
    [Authorize]
    public async Task<SurveyResponsesListResponse> Get([FromBody] RequestFilter request)
    {
        var surveyresponsesList =  await _surveyresponsesService.GetSurveyResponsesList(request.Index,request.Limit,request);
        var count = await _surveyresponsesService.GetSurveyResponsesCount(request);

        SurveyResponsesListResponse respObj = new SurveyResponsesListResponse();   
        
        respObj.count = count;            
            
        
        List<SurveyResponses> listData = new List<SurveyResponses>();
        foreach(var surveyresponses in surveyresponsesList)
        {
            SurveyResponses surveyresponsesObj = new SurveyResponses();
            Questions questionsObj = new Questions();
     
                surveyresponsesObj.SurveyResponseId = surveyresponses.surveyresponseid ?? null; 
                surveyresponsesObj.ResponseId = surveyresponses.responseid ?? null; 
                surveyresponsesObj.QuestionId = surveyresponses.questionid ?? null; 
                surveyresponsesObj.Option = surveyresponses.option;
              
                questionsObj.QuestionId = surveyresponses.questionid ?? null; 
                questionsObj.SurveyId = surveyresponses.surveyid ?? null; 
                questionsObj.Question = surveyresponses.question; 
                questionsObj.QuestionCategory = surveyresponses.questioncategory; 
                questionsObj.OptionCategoryId = surveyresponses.optioncategoryid ?? null; 
                questionsObj.OptionText = surveyresponses.optiontext; 
                questionsObj.QuestionTextFormatting = surveyresponses.questiontextformatting;
    
            surveyresponsesObj.Questions = new Questions();
            surveyresponsesObj.Questions = questionsObj;
            listData.Add(surveyresponsesObj);
        }
        respObj.SurveyResponses = new List<SurveyResponses>();
        respObj.SurveyResponses = listData;
        return respObj;
    }
    
    [HttpGet("{id:long}")]
    [Authorize]
    public async Task<SurveyResponses> GetSurveyResponses(long id)
    {
        var surveyresponses =  await _surveyresponsesService.GetSurveyResponses(id);
        SurveyResponses surveyresponsesObj = new SurveyResponses();
            Questions questionsObj = new Questions();
     
                surveyresponsesObj.SurveyResponseId = surveyresponses.surveyresponseid ?? null; 
                surveyresponsesObj.ResponseId = surveyresponses.responseid ?? null; 
                surveyresponsesObj.QuestionId = surveyresponses.questionid ?? null; 
                surveyresponsesObj.Option = surveyresponses.option;
              
                questionsObj.QuestionId = surveyresponses.questionid ?? null; 
                questionsObj.SurveyId = surveyresponses.surveyid ?? null; 
                questionsObj.Question = surveyresponses.question; 
                questionsObj.QuestionCategory = surveyresponses.questioncategory; 
                questionsObj.OptionCategoryId = surveyresponses.optioncategoryid ?? null; 
                questionsObj.OptionText = surveyresponses.optiontext; 
                questionsObj.QuestionTextFormatting = surveyresponses.questiontextformatting;
    
            surveyresponsesObj.Questions = new Questions();
            surveyresponsesObj.Questions = questionsObj;
        return surveyresponsesObj;
    }
    
    [HttpPost]
    [Authorize]
    public async Task<IActionResult> AddSurveyResponses([FromBody]SurveyResponses surveyresponses)
    {
        var result =  await _surveyresponsesService.CreateSurveyResponses(surveyresponses);

        return Ok(result);
    }
    
    [HttpPut]
    [Authorize]
    public async Task<IActionResult> UpdateSurveyResponses([FromBody]SurveyResponses surveyresponses)
    {
        var result =  await _surveyresponsesService.UpdateSurveyResponses(surveyresponses);

        return Ok(result);
    }
    
    [HttpDelete("{id:long}")]
    [Authorize]
    public async Task<IActionResult> DeleteSurveyResponses(long id)
    {
        var result =  await _surveyresponsesService.DeleteSurveyResponses(id);

        return Ok(result);
    }
}
