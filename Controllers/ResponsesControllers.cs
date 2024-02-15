
using System.Threading.Tasks;
using SurveyApp.Models;
using SurveyApp.Services;
using SurveyApp.Schemas;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace SurveyApp.Controllers;

[ApiController]
[Route("responses")]
public class ResponsessController : Controller
{
    private readonly IResponsesService _responsesService;
    
    public ResponsessController(IResponsesService responsesService)
    {
        _responsesService = responsesService;
    }

    [HttpPost]
    [Route("list")]
    [Authorize]
    public async Task<ResponsesListResponse> Get([FromBody] RequestFilter request)
    {
        var responsesList =  await _responsesService.GetResponsesList(request.Index,request.Limit,request);
        var count = await _responsesService.GetResponsesCount(request);

        ResponsesListResponse respObj = new ResponsesListResponse();   
        
        respObj.count = count;            
            
        
        List<Responses> listData = new List<Responses>();
        foreach(var responses in responsesList)
        {
            Responses responsesObj = new Responses();
            Users usersObj = new Users();
            Surveys surveysObj = new Surveys();
     
                responsesObj.ResponseId = responses.responseid ?? null; 
                responsesObj.RespondentId = responses.respondentid ?? null; 
                responsesObj.SurveyId = responses.surveyid ?? null; 
                responsesObj.TotalScore = responses.totalscore ?? null;
              
                usersObj.UserId = responses.userid ?? null; 
                usersObj.UserRole = responses.userrole; 
                usersObj.UserName = responses.username; 
                usersObj.UserEmail = responses.useremail; 
                usersObj.UserMobileNo = responses.usermobileno ?? null; 
                usersObj.UserPassword = responses.userpassword; 
                usersObj.UserGender = responses.usergender; 
                usersObj.UserDateOfBirth = responses.userdateofbirth; 
                usersObj.PasswordSalt = responses.passwordsalt; 
                usersObj.MaritalStatus = responses.maritalstatus; 
                usersObj.UserExperience = responses.userexperience ?? null; 
                usersObj.UserCity = responses.usercity; 
                usersObj.UserDepartment = responses.userdepartment; 
                usersObj.BuilderUserId = responses.builderuserid ?? null;
              
                surveysObj.SurveyId = responses.surveyid ?? null; 
                surveysObj.SurveyName = responses.surveyname; 
                surveysObj.SurveyCategoryId = responses.surveycategoryid ?? null; 
                surveysObj.BackroundImageId = responses.backroundimageid ?? null; 
                surveysObj.UserId = responses.userid ?? null; 
                surveysObj.CreatedAt = responses.createdat; 
                surveysObj.SurveyStatus = responses.surveystatus;
    
            responsesObj.Users = new Users();
            responsesObj.Users = usersObj;
            responsesObj.Surveys = new Surveys();
            responsesObj.Surveys = surveysObj;
            listData.Add(responsesObj);
        }
        respObj.Responses = new List<Responses>();
        respObj.Responses = listData;
        return respObj;
    }
    
    [HttpGet("{id:long}")]
    [Authorize]
    public async Task<Responses> GetResponses(long id)
    {
        var responses =  await _responsesService.GetResponses(id);
        Responses responsesObj = new Responses();
            Users usersObj = new Users();
            Surveys surveysObj = new Surveys();
     
                responsesObj.ResponseId = responses.responseid ?? null; 
                responsesObj.RespondentId = responses.respondentid ?? null; 
                responsesObj.SurveyId = responses.surveyid ?? null; 
                responsesObj.TotalScore = responses.totalscore ?? null;
              
                usersObj.UserId = responses.userid ?? null; 
                usersObj.UserRole = responses.userrole; 
                usersObj.UserName = responses.username; 
                usersObj.UserEmail = responses.useremail; 
                usersObj.UserMobileNo = responses.usermobileno ?? null; 
                usersObj.UserPassword = responses.userpassword; 
                usersObj.UserGender = responses.usergender; 
                usersObj.UserDateOfBirth = responses.userdateofbirth; 
                usersObj.PasswordSalt = responses.passwordsalt; 
                usersObj.MaritalStatus = responses.maritalstatus; 
                usersObj.UserExperience = responses.userexperience ?? null; 
                usersObj.UserCity = responses.usercity; 
                usersObj.UserDepartment = responses.userdepartment; 
                usersObj.BuilderUserId = responses.builderuserid ?? null;
              
                surveysObj.SurveyId = responses.surveyid ?? null; 
                surveysObj.SurveyName = responses.surveyname; 
                surveysObj.SurveyCategoryId = responses.surveycategoryid ?? null; 
                surveysObj.BackroundImageId = responses.backroundimageid ?? null; 
                surveysObj.UserId = responses.userid ?? null; 
                surveysObj.CreatedAt = responses.createdat; 
                surveysObj.SurveyStatus = responses.surveystatus;
    
            responsesObj.Users = new Users();
            responsesObj.Users = usersObj;
            responsesObj.Surveys = new Surveys();
            responsesObj.Surveys = surveysObj;
        return responsesObj;
    }
    
    [HttpPost]
    [Authorize]
    public async Task<IActionResult> AddResponses([FromBody]Responses responses)
    {
        var result =  await _responsesService.CreateResponses(responses);

        return Ok(result);
    }
    
    [HttpPut]
    [Authorize]
    public async Task<IActionResult> UpdateResponses([FromBody]Responses responses)
    {
        var result =  await _responsesService.UpdateResponses(responses);

        return Ok(result);
    }
    
    [HttpDelete("{id:long}")]
    [Authorize]
    public async Task<IActionResult> DeleteResponses(long id)
    {
        var result =  await _responsesService.DeleteResponses(id);

        return Ok(result);
    }
}
