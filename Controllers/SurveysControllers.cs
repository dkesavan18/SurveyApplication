
using System.Threading.Tasks;
using SurveyApp.Models;
using SurveyApp.Services;
using SurveyApp.Schemas;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace SurveyApp.Controllers;

[ApiController]
[Route("surveys")]
public class SurveyssController : Controller
{
    private readonly ISurveysService _surveysService;
    
    public SurveyssController(ISurveysService surveysService)
    {
        _surveysService = surveysService;
    }

    [HttpPost]
    [Route("list")]
    [Authorize]
    public async Task<SurveysListResponse> Get([FromBody] RequestFilter request)
    {
        var surveysList =  await _surveysService.GetSurveysList(request.Index,request.Limit,request);
        var count = await _surveysService.GetSurveysCount(request);

        SurveysListResponse respObj = new SurveysListResponse();   
        
        respObj.count = count;            
            
        
        List<Surveys> listData = new List<Surveys>();
        foreach(var surveys in surveysList)
        {
            Surveys surveysObj = new Surveys();
            SurveyCategories surveycategoriesObj = new SurveyCategories();
            SurveyBackgroundImages surveybackgroundimagesObj = new SurveyBackgroundImages();
            Users usersObj = new Users();
     
                surveysObj.SurveyId = surveys.surveyid ?? null; 
                surveysObj.SurveyName = surveys.surveyname; 
                surveysObj.SurveyCategoryId = surveys.surveycategoryid ?? null; 
                surveysObj.BackroundImageId = surveys.backroundimageid ?? null; 
                surveysObj.UserId = surveys.userid ?? null; 
                surveysObj.CreatedAt = surveys.createdat; 
                surveysObj.SurveyStatus = surveys.surveystatus;
              
                surveycategoriesObj.SurveyCategoryId = surveys.surveycategoryid ?? null; 
                surveycategoriesObj.SurveyCategory = surveys.surveycategory;
              
                surveybackgroundimagesObj.SurveyBackgroundId = surveys.surveybackgroundid ?? null; 
                surveybackgroundimagesObj.BackgroundImage = surveys.backgroundimage;
              
                usersObj.UserId = surveys.userid ?? null; 
                usersObj.UserRole = surveys.userrole; 
                usersObj.UserName = surveys.username; 
                usersObj.UserEmail = surveys.useremail; 
                usersObj.UserMobileNo = surveys.usermobileno ?? null; 
                usersObj.UserPassword = surveys.userpassword; 
                usersObj.UserGender = surveys.usergender; 
                usersObj.UserDateOfBirth = surveys.userdateofbirth; 
                usersObj.PasswordSalt = surveys.passwordsalt; 
                usersObj.MaritalStatus = surveys.maritalstatus; 
                usersObj.UserExperience = surveys.userexperience ?? null; 
                usersObj.UserCity = surveys.usercity; 
                usersObj.UserDepartment = surveys.userdepartment; 
                usersObj.BuilderUserId = surveys.builderuserid ?? null;
    
            surveysObj.SurveyCategories = new SurveyCategories();
            surveysObj.SurveyCategories = surveycategoriesObj;
            surveysObj.SurveyBackgroundImages = new SurveyBackgroundImages();
            surveysObj.SurveyBackgroundImages = surveybackgroundimagesObj;
            surveysObj.Users = new Users();
            surveysObj.Users = usersObj;
            listData.Add(surveysObj);
        }
        respObj.Surveys = new List<Surveys>();
        respObj.Surveys = listData;
        return respObj;
    }
    
    [HttpGet("{id:long}")]
    [Authorize]
    public async Task<Surveys> GetSurveys(long id)
    {
        var surveys =  await _surveysService.GetSurveys(id);
        Surveys surveysObj = new Surveys();
            SurveyCategories surveycategoriesObj = new SurveyCategories();
            SurveyBackgroundImages surveybackgroundimagesObj = new SurveyBackgroundImages();
            Users usersObj = new Users();
     
                surveysObj.SurveyId = surveys.surveyid ?? null; 
                surveysObj.SurveyName = surveys.surveyname; 
                surveysObj.SurveyCategoryId = surveys.surveycategoryid ?? null; 
                surveysObj.BackroundImageId = surveys.backroundimageid ?? null; 
                surveysObj.UserId = surveys.userid ?? null; 
                surveysObj.CreatedAt = surveys.createdat; 
                surveysObj.SurveyStatus = surveys.surveystatus;
              
                surveycategoriesObj.SurveyCategoryId = surveys.surveycategoryid ?? null; 
                surveycategoriesObj.SurveyCategory = surveys.surveycategory;
              
                surveybackgroundimagesObj.SurveyBackgroundId = surveys.surveybackgroundid ?? null; 
                surveybackgroundimagesObj.BackgroundImage = surveys.backgroundimage;
              
                usersObj.UserId = surveys.userid ?? null; 
                usersObj.UserRole = surveys.userrole; 
                usersObj.UserName = surveys.username; 
                usersObj.UserEmail = surveys.useremail; 
                usersObj.UserMobileNo = surveys.usermobileno ?? null; 
                usersObj.UserPassword = surveys.userpassword; 
                usersObj.UserGender = surveys.usergender; 
                usersObj.UserDateOfBirth = surveys.userdateofbirth; 
                usersObj.PasswordSalt = surveys.passwordsalt; 
                usersObj.MaritalStatus = surveys.maritalstatus; 
                usersObj.UserExperience = surveys.userexperience ?? null; 
                usersObj.UserCity = surveys.usercity; 
                usersObj.UserDepartment = surveys.userdepartment; 
                usersObj.BuilderUserId = surveys.builderuserid ?? null;
    
            surveysObj.SurveyCategories = new SurveyCategories();
            surveysObj.SurveyCategories = surveycategoriesObj;
            surveysObj.SurveyBackgroundImages = new SurveyBackgroundImages();
            surveysObj.SurveyBackgroundImages = surveybackgroundimagesObj;
            surveysObj.Users = new Users();
            surveysObj.Users = usersObj;
        return surveysObj;
    }
    
    [HttpPost]
    [Authorize]
    public async Task<IActionResult> AddSurveys([FromBody]Surveys surveys)
    {
        var result =  await _surveysService.CreateSurveys(surveys);

        return Ok(result);
    }
    
    [HttpPut]
    [Authorize]
    public async Task<IActionResult> UpdateSurveys([FromBody]Surveys surveys)
    {
        var result =  await _surveysService.UpdateSurveys(surveys);

        return Ok(result);
    }
    
    [HttpDelete("{id:long}")]
    [Authorize]
    public async Task<IActionResult> DeleteSurveys(long id)
    {
        var result =  await _surveysService.DeleteSurveys(id);

        return Ok(result);
    }
}
