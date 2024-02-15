
using System.Threading.Tasks;
using SurveyApp.Models;
using SurveyApp.Services;
using SurveyApp.Schemas;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace SurveyApp.Controllers;

[ApiController]
[Route("groups")]
public class GroupssController : Controller
{
    private readonly IGroupsService _groupsService;
    
    public GroupssController(IGroupsService groupsService)
    {
        _groupsService = groupsService;
    }

    [HttpPost]
    [Route("list")]
    [Authorize]
    public async Task<GroupsListResponse> Get([FromBody] RequestFilter request)
    {
        var groupsList =  await _groupsService.GetGroupsList(request.Index,request.Limit,request);
        var count = await _groupsService.GetGroupsCount(request);

        GroupsListResponse respObj = new GroupsListResponse();   
        
        respObj.count = count;            
            
        
        List<Groups> listData = new List<Groups>();
        foreach(var groups in groupsList)
        {
            Groups groupsObj = new Groups();
            Users usersObj = new Users();
     
                groupsObj.GroupId = groups.groupid ?? null; 
                groupsObj.GroupName = groups.groupname; 
                groupsObj.GroupDescription = groups.groupdescription; 
                groupsObj.NumberOfMembers = groups.numberofmembers ?? null; 
                groupsObj.CreatedAt = groups.createdat; 
                groupsObj.NoOfSurveysShared = groups.noofsurveysshared ?? null; 
                groupsObj.DeletedAt = groups.deletedat; 
                groupsObj.UpdatedAt = groups.updatedat; 
                groupsObj.CreatedBy = groups.createdby ?? null;
              
                usersObj.UserId = groups.userid ?? null; 
                usersObj.UserRole = groups.userrole; 
                usersObj.UserName = groups.username; 
                usersObj.UserEmail = groups.useremail; 
                usersObj.UserMobileNo = groups.usermobileno ?? null; 
                usersObj.UserPassword = groups.userpassword; 
                usersObj.UserGender = groups.usergender; 
                usersObj.UserDateOfBirth = groups.userdateofbirth; 
                usersObj.PasswordSalt = groups.passwordsalt; 
                usersObj.MaritalStatus = groups.maritalstatus; 
                usersObj.UserExperience = groups.userexperience ?? null; 
                usersObj.UserCity = groups.usercity; 
                usersObj.UserDepartment = groups.userdepartment; 
                usersObj.BuilderUserId = groups.builderuserid ?? null;
    
            groupsObj.Users = new Users();
            groupsObj.Users = usersObj;
            listData.Add(groupsObj);
        }
        respObj.Groups = new List<Groups>();
        respObj.Groups = listData;
        return respObj;
    }
    
    [HttpGet("{id:long}")]
    [Authorize]
    public async Task<Groups> GetGroups(long id)
    {
        var groups =  await _groupsService.GetGroups(id);
        Groups groupsObj = new Groups();
            Users usersObj = new Users();
     
                groupsObj.GroupId = groups.groupid ?? null; 
                groupsObj.GroupName = groups.groupname; 
                groupsObj.GroupDescription = groups.groupdescription; 
                groupsObj.NumberOfMembers = groups.numberofmembers ?? null; 
                groupsObj.CreatedAt = groups.createdat; 
                groupsObj.NoOfSurveysShared = groups.noofsurveysshared ?? null; 
                groupsObj.DeletedAt = groups.deletedat; 
                groupsObj.UpdatedAt = groups.updatedat; 
                groupsObj.CreatedBy = groups.createdby ?? null;
              
                usersObj.UserId = groups.userid ?? null; 
                usersObj.UserRole = groups.userrole; 
                usersObj.UserName = groups.username; 
                usersObj.UserEmail = groups.useremail; 
                usersObj.UserMobileNo = groups.usermobileno ?? null; 
                usersObj.UserPassword = groups.userpassword; 
                usersObj.UserGender = groups.usergender; 
                usersObj.UserDateOfBirth = groups.userdateofbirth; 
                usersObj.PasswordSalt = groups.passwordsalt; 
                usersObj.MaritalStatus = groups.maritalstatus; 
                usersObj.UserExperience = groups.userexperience ?? null; 
                usersObj.UserCity = groups.usercity; 
                usersObj.UserDepartment = groups.userdepartment; 
                usersObj.BuilderUserId = groups.builderuserid ?? null;
    
            groupsObj.Users = new Users();
            groupsObj.Users = usersObj;
        return groupsObj;
    }
    
    [HttpPost]
    [Authorize]
    public async Task<IActionResult> AddGroups([FromBody]Groups groups)
    {
        var result =  await _groupsService.CreateGroups(groups);

        return Ok(result);
    }
    
    [HttpPut]
    [Authorize]
    public async Task<IActionResult> UpdateGroups([FromBody]Groups groups)
    {
        var result =  await _groupsService.UpdateGroups(groups);

        return Ok(result);
    }
    
    [HttpDelete("{id:long}")]
    [Authorize]
    public async Task<IActionResult> DeleteGroups(long id)
    {
        var result =  await _groupsService.DeleteGroups(id);

        return Ok(result);
    }
}
