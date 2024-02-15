
using System.Threading.Tasks;
using SurveyApp.Models;
using SurveyApp.Services;
using SurveyApp.Schemas;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace SurveyApp.Controllers;

[ApiController]
[Route("groupmembers")]
public class GroupMemberssController : Controller
{
    private readonly IGroupMembersService _groupmembersService;
    
    public GroupMemberssController(IGroupMembersService groupmembersService)
    {
        _groupmembersService = groupmembersService;
    }

    [HttpPost]
    [Route("list")]
    [Authorize]
    public async Task<GroupMembersListResponse> Get([FromBody] RequestFilter request)
    {
        var groupmembersList =  await _groupmembersService.GetGroupMembersList(request.Index,request.Limit,request);
        var count = await _groupmembersService.GetGroupMembersCount(request);

        GroupMembersListResponse respObj = new GroupMembersListResponse();   
        
        respObj.count = count;            
            
        
        List<GroupMembers> listData = new List<GroupMembers>();
        foreach(var groupmembers in groupmembersList)
        {
            GroupMembers groupmembersObj = new GroupMembers();
            Groups groupsObj = new Groups();
            Users usersObj = new Users();
     
                groupmembersObj.GroupMemberId = groupmembers.groupmemberid ?? null; 
                groupmembersObj.GroupId = groupmembers.groupid ?? null; 
                groupmembersObj.GroupMember = groupmembers.groupmember ?? null;
              
                groupsObj.GroupId = groupmembers.groupid ?? null; 
                groupsObj.GroupName = groupmembers.groupname; 
                groupsObj.GroupDescription = groupmembers.groupdescription; 
                groupsObj.NumberOfMembers = groupmembers.numberofmembers ?? null; 
                groupsObj.CreatedAt = groupmembers.createdat; 
                groupsObj.NoOfSurveysShared = groupmembers.noofsurveysshared ?? null; 
                groupsObj.DeletedAt = groupmembers.deletedat; 
                groupsObj.UpdatedAt = groupmembers.updatedat; 
                groupsObj.CreatedBy = groupmembers.createdby ?? null;
              
                usersObj.UserId = groupmembers.userid ?? null; 
                usersObj.UserRole = groupmembers.userrole; 
                usersObj.UserName = groupmembers.username; 
                usersObj.UserEmail = groupmembers.useremail; 
                usersObj.UserMobileNo = groupmembers.usermobileno ?? null; 
                usersObj.UserPassword = groupmembers.userpassword; 
                usersObj.UserGender = groupmembers.usergender; 
                usersObj.UserDateOfBirth = groupmembers.userdateofbirth; 
                usersObj.PasswordSalt = groupmembers.passwordsalt; 
                usersObj.MaritalStatus = groupmembers.maritalstatus; 
                usersObj.UserExperience = groupmembers.userexperience ?? null; 
                usersObj.UserCity = groupmembers.usercity; 
                usersObj.UserDepartment = groupmembers.userdepartment; 
                usersObj.BuilderUserId = groupmembers.builderuserid ?? null;
    
            groupmembersObj.Groups = new Groups();
            groupmembersObj.Groups = groupsObj;
            groupmembersObj.Users = new Users();
            groupmembersObj.Users = usersObj;
            listData.Add(groupmembersObj);
        }
        respObj.GroupMembers = new List<GroupMembers>();
        respObj.GroupMembers = listData;
        return respObj;
    }
    
    [HttpGet("{id:long}")]
    [Authorize]
    public async Task<GroupMembers> GetGroupMembers(long id)
    {
        var groupmembers =  await _groupmembersService.GetGroupMembers(id);
        GroupMembers groupmembersObj = new GroupMembers();
            Groups groupsObj = new Groups();
            Users usersObj = new Users();
     
                groupmembersObj.GroupMemberId = groupmembers.groupmemberid ?? null; 
                groupmembersObj.GroupId = groupmembers.groupid ?? null; 
                groupmembersObj.GroupMember = groupmembers.groupmember ?? null;
              
                groupsObj.GroupId = groupmembers.groupid ?? null; 
                groupsObj.GroupName = groupmembers.groupname; 
                groupsObj.GroupDescription = groupmembers.groupdescription; 
                groupsObj.NumberOfMembers = groupmembers.numberofmembers ?? null; 
                groupsObj.CreatedAt = groupmembers.createdat; 
                groupsObj.NoOfSurveysShared = groupmembers.noofsurveysshared ?? null; 
                groupsObj.DeletedAt = groupmembers.deletedat; 
                groupsObj.UpdatedAt = groupmembers.updatedat; 
                groupsObj.CreatedBy = groupmembers.createdby ?? null;
              
                usersObj.UserId = groupmembers.userid ?? null; 
                usersObj.UserRole = groupmembers.userrole; 
                usersObj.UserName = groupmembers.username; 
                usersObj.UserEmail = groupmembers.useremail; 
                usersObj.UserMobileNo = groupmembers.usermobileno ?? null; 
                usersObj.UserPassword = groupmembers.userpassword; 
                usersObj.UserGender = groupmembers.usergender; 
                usersObj.UserDateOfBirth = groupmembers.userdateofbirth; 
                usersObj.PasswordSalt = groupmembers.passwordsalt; 
                usersObj.MaritalStatus = groupmembers.maritalstatus; 
                usersObj.UserExperience = groupmembers.userexperience ?? null; 
                usersObj.UserCity = groupmembers.usercity; 
                usersObj.UserDepartment = groupmembers.userdepartment; 
                usersObj.BuilderUserId = groupmembers.builderuserid ?? null;
    
            groupmembersObj.Groups = new Groups();
            groupmembersObj.Groups = groupsObj;
            groupmembersObj.Users = new Users();
            groupmembersObj.Users = usersObj;
        return groupmembersObj;
    }
    
    [HttpPost]
    [Authorize]
    public async Task<IActionResult> AddGroupMembers([FromBody]GroupMembers groupmembers)
    {
        var result =  await _groupmembersService.CreateGroupMembers(groupmembers);

        return Ok(result);
    }
    
    [HttpPut]
    [Authorize]
    public async Task<IActionResult> UpdateGroupMembers([FromBody]GroupMembers groupmembers)
    {
        var result =  await _groupmembersService.UpdateGroupMembers(groupmembers);

        return Ok(result);
    }
    
    [HttpDelete("{id:long}")]
    [Authorize]
    public async Task<IActionResult> DeleteGroupMembers(long id)
    {
        var result =  await _groupmembersService.DeleteGroupMembers(id);

        return Ok(result);
    }
}
