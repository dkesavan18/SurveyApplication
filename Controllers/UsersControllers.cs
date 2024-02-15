
using System.Threading.Tasks;
using SurveyApp.Models;
using SurveyApp.Services;
using SurveyApp.Schemas;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace SurveyApp.Controllers;

[ApiController]
[Route("users")]
public class UserssController : Controller
{
    private readonly IUsersService _usersService;

    public UserssController(IUsersService usersService)
    {
        _usersService = usersService;
    }

    [HttpPost]
    [Route("list")]
    [Authorize]
    public async Task<UsersListResponse> Get([FromBody] RequestFilter request)
    {
        var usersList = await _usersService.GetUsersList(request.Index, request.Limit, request);
        var count = await _usersService.GetUsersCount(request);

        UsersListResponse respObj = new UsersListResponse();

        respObj.count = count;


        List<Users> listData = new List<Users>();
        foreach (var users in usersList)
        {
            Users usersObj = new Users();

            usersObj.UserId = users.userid ?? null;
            usersObj.UserRole = users.userrole;
            usersObj.UserName = users.username;
            usersObj.UserEmail = users.useremail;
            usersObj.UserMobileNo = users.usermobileno ?? null;
            usersObj.UserPassword = users.userpassword;
            usersObj.UserGender = users.usergender;
            usersObj.UserDateOfBirth = users.userdateofbirth;
            usersObj.PasswordSalt = users.passwordsalt;
            usersObj.MaritalStatus = users.maritalstatus;
            usersObj.UserExperience = users.userexperience ?? null;
            usersObj.UserCity = users.usercity;
            usersObj.UserDepartment = users.userdepartment;
            usersObj.BuilderUserId = users.builderuserid ?? null;

            listData.Add(usersObj);
        }
        respObj.Users = new List<Users>();
        respObj.Users = listData;
        return respObj;
    }

    [HttpGet("{id:long}")]
    [Authorize]
    public async Task<Users> GetUsers(long id)
    {
        var users = await _usersService.GetUsers(id);
        Users usersObj = new Users();

        usersObj.UserId = users.userid ?? null;
        usersObj.UserRole = users.userrole;
        usersObj.UserName = users.username;
        usersObj.UserEmail = users.useremail;
        usersObj.UserMobileNo = users.usermobileno ?? null;
        usersObj.UserPassword = users.userpassword;
        usersObj.UserGender = users.usergender;
        usersObj.UserDateOfBirth = users.userdateofbirth;
        usersObj.PasswordSalt = users.passwordsalt;
        usersObj.MaritalStatus = users.maritalstatus;
        usersObj.UserExperience = users.userexperience ?? null;
        usersObj.UserCity = users.usercity;
        usersObj.UserDepartment = users.userdepartment;
        usersObj.BuilderUserId = users.builderuserid ?? null;

        return usersObj;
    }

    [HttpPost("signup")]
    // [Authorize]
    public async Task<IActionResult> AddUsers([FromBody] Users users)
    {
        var result = await _usersService.CreateUsers(users);

        return Ok(result);
    }

    [HttpPut]
    [Authorize]
    public async Task<IActionResult> UpdateUsers([FromBody] Users users)
    {
        var result = await _usersService.UpdateUsers(users);

        return Ok(result);
    }

    [HttpDelete("{id:long}")]
    [Authorize]
    public async Task<IActionResult> DeleteUsers(long id)
    {
        var result = await _usersService.DeleteUsers(id);

        return Ok(result);
    }
}
