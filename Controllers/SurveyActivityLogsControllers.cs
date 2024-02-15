
using System.Threading.Tasks;
using SurveyApp.Models;
using SurveyApp.Services;
using SurveyApp.Schemas;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace SurveyApp.Controllers;

[ApiController]
[Route("surveyactivitylogs")]
public class SurveyActivityLogssController : Controller
{
    private readonly ISurveyActivityLogsService _surveyactivitylogsService;
    
    public SurveyActivityLogssController(ISurveyActivityLogsService surveyactivitylogsService)
    {
        _surveyactivitylogsService = surveyactivitylogsService;
    }

    [HttpPost]
    [Route("list")]
    [Authorize]
    public async Task<SurveyActivityLogsListResponse> Get([FromBody] RequestFilter request)
    {
        var surveyactivitylogsList =  await _surveyactivitylogsService.GetSurveyActivityLogsList(request.Index,request.Limit,request);
        var count = await _surveyactivitylogsService.GetSurveyActivityLogsCount(request);

        SurveyActivityLogsListResponse respObj = new SurveyActivityLogsListResponse();   
        
        respObj.count = count;            
            
        
        List<SurveyActivityLogs> listData = new List<SurveyActivityLogs>();
        foreach(var surveyactivitylogs in surveyactivitylogsList)
        {
            SurveyActivityLogs surveyactivitylogsObj = new SurveyActivityLogs();
            Surveys surveysObj = new Surveys();
     
                surveyactivitylogsObj.LogId = surveyactivitylogs.logid ?? null; 
                surveyactivitylogsObj.StartTime = surveyactivitylogs.starttime; 
                surveyactivitylogsObj.EndTime = surveyactivitylogs.endtime; 
                surveyactivitylogsObj.SurveyId = surveyactivitylogs.surveyid ?? null; 
                surveyactivitylogsObj.UserAction = surveyactivitylogs.useraction; 
                surveyactivitylogsObj.LastModified = surveyactivitylogs.lastmodified;
              
                surveysObj.SurveyId = surveyactivitylogs.surveyid ?? null; 
                surveysObj.SurveyName = surveyactivitylogs.surveyname; 
                surveysObj.SurveyCategoryId = surveyactivitylogs.surveycategoryid ?? null; 
                surveysObj.BackroundImageId = surveyactivitylogs.backroundimageid ?? null; 
                surveysObj.UserId = surveyactivitylogs.userid ?? null; 
                surveysObj.CreatedAt = surveyactivitylogs.createdat; 
                surveysObj.SurveyStatus = surveyactivitylogs.surveystatus;
    
            surveyactivitylogsObj.Surveys = new Surveys();
            surveyactivitylogsObj.Surveys = surveysObj;
            listData.Add(surveyactivitylogsObj);
        }
        respObj.SurveyActivityLogs = new List<SurveyActivityLogs>();
        respObj.SurveyActivityLogs = listData;
        return respObj;
    }
    
    [HttpGet("{id:long}")]
    [Authorize]
    public async Task<SurveyActivityLogs> GetSurveyActivityLogs(long id)
    {
        var surveyactivitylogs =  await _surveyactivitylogsService.GetSurveyActivityLogs(id);
        SurveyActivityLogs surveyactivitylogsObj = new SurveyActivityLogs();
            Surveys surveysObj = new Surveys();
     
                surveyactivitylogsObj.LogId = surveyactivitylogs.logid ?? null; 
                surveyactivitylogsObj.StartTime = surveyactivitylogs.starttime; 
                surveyactivitylogsObj.EndTime = surveyactivitylogs.endtime; 
                surveyactivitylogsObj.SurveyId = surveyactivitylogs.surveyid ?? null; 
                surveyactivitylogsObj.UserAction = surveyactivitylogs.useraction; 
                surveyactivitylogsObj.LastModified = surveyactivitylogs.lastmodified;
              
                surveysObj.SurveyId = surveyactivitylogs.surveyid ?? null; 
                surveysObj.SurveyName = surveyactivitylogs.surveyname; 
                surveysObj.SurveyCategoryId = surveyactivitylogs.surveycategoryid ?? null; 
                surveysObj.BackroundImageId = surveyactivitylogs.backroundimageid ?? null; 
                surveysObj.UserId = surveyactivitylogs.userid ?? null; 
                surveysObj.CreatedAt = surveyactivitylogs.createdat; 
                surveysObj.SurveyStatus = surveyactivitylogs.surveystatus;
    
            surveyactivitylogsObj.Surveys = new Surveys();
            surveyactivitylogsObj.Surveys = surveysObj;
        return surveyactivitylogsObj;
    }
    
    [HttpPost]
    [Authorize]
    public async Task<IActionResult> AddSurveyActivityLogs([FromBody]SurveyActivityLogs surveyactivitylogs)
    {
        var result =  await _surveyactivitylogsService.CreateSurveyActivityLogs(surveyactivitylogs);

        return Ok(result);
    }
    
    [HttpPut]
    [Authorize]
    public async Task<IActionResult> UpdateSurveyActivityLogs([FromBody]SurveyActivityLogs surveyactivitylogs)
    {
        var result =  await _surveyactivitylogsService.UpdateSurveyActivityLogs(surveyactivitylogs);

        return Ok(result);
    }
    
    [HttpDelete("{id:long}")]
    [Authorize]
    public async Task<IActionResult> DeleteSurveyActivityLogs(long id)
    {
        var result =  await _surveyactivitylogsService.DeleteSurveyActivityLogs(id);

        return Ok(result);
    }
}
