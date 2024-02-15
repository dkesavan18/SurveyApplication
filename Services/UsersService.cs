
using System.Collections.Generic;
using System.Threading.Tasks;
using SurveyApp.Models;
using SurveyApp.Helpers;
using SurveyApp.Schemas;

namespace SurveyApp.Services;

public class UsersService : IUsersService
{
    private readonly IDbService _dbService;

    public UsersService(IDbService dbService)
    {
        _dbService = dbService;
    }

    public async Task<ResponseObject> CreateUsers(Users users)
    {
        ResponseObject respObj = new ResponseObject();
       
        try
        {
            var result =
            await _dbService.EditData(
                "INSERT INTO users (user_id,user_role,user_name,user_email,user_mobile_no,user_password,user_gender,user_date_of_birth,password_salt,marital_status,user_experience,user_city,user_department,builder_user_id) VALUES (@UserId, @UserRole, @UserName, @UserEmail, @UserMobileNo, @UserPassword, @UserGender, @UserDateOfBirth, @PasswordSalt, @MaritalStatus, @UserExperience, @UserCity, @UserDepartment, @BuilderUserId)",
                users);
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

    public async Task<long> GetUsersCount(RequestFilter filter)
    {
         Util utilities = new Util();
        if(filter == null || String.IsNullOrEmpty(filter.OrderByTable))
        {
            filter.OrderByTable = "Users";
            filter.OrderByField = "UserId";
            filter.Sort = "ASC";
        }
        string whereQuery = await utilities.FormWhereQuery(filter);
        string orderQuery = await utilities.FormOrderQuery(filter);
        string countQuery = "select count(*) as count from  users users";
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

    public async Task<dynamic> GetUsersList(int index,int limit,RequestFilter filter)
    {
        Util utilities = new Util();
        if(filter == null || String.IsNullOrEmpty(filter.OrderByTable))
        {
            filter.OrderByTable = "Users";
            filter.OrderByField = "UserId";
            filter.Sort = "ASC";
        }
        string whereQuery = await utilities.FormWhereQuery(filter);
        string orderQuery = await utilities.FormOrderQuery(filter);
        string selectQuery = "select users.user_id as UserId, users.user_role as UserRole, users.user_name as UserName, users.user_email as UserEmail, users.user_mobile_no as UserMobileNo, users.user_password as UserPassword, users.user_gender as UserGender, users.user_date_of_birth as UserDateOfBirth, users.password_salt as PasswordSalt, users.marital_status as MaritalStatus, users.user_experience as UserExperience, users.user_city as UserCity, users.user_department as UserDepartment, users.builder_user_id as BuilderUserId from users users";
        if(!String.IsNullOrEmpty(whereQuery))
        {
            selectQuery = selectQuery+" where "+whereQuery;
        }
        if(!String.IsNullOrEmpty(orderQuery))
        {
            selectQuery = selectQuery+" "+orderQuery;
        }
        var usersList = await _dbService.GetAll<dynamic>(selectQuery+" LIMIT "+limit+ "OFFSET "+index, new { });
      
        return usersList;
    }


    public async Task<dynamic> GetUsers(long UserId)
    {
        var users = await _dbService.GetAsync<dynamic>("select users.user_id as UserId, users.user_role as UserRole, users.user_name as UserName, users.user_email as UserEmail, users.user_mobile_no as UserMobileNo, users.user_password as UserPassword, users.user_gender as UserGender, users.user_date_of_birth as UserDateOfBirth, users.password_salt as PasswordSalt, users.marital_status as MaritalStatus, users.user_experience as UserExperience, users.user_city as UserCity, users.user_department as UserDepartment, users.builder_user_id as BuilderUserId from users users where users.user_id=@UserId", new {UserId});
        
        return users;
    }

    public async Task<bool> UpdateUsers(Users users)
    {
        var updateUsers =
            await _dbService.EditData(
                "Update users SET user_role = @UserRole, user_name = @UserName, user_email = @UserEmail, user_mobile_no = @UserMobileNo, user_password = @UserPassword, user_gender = @UserGender, user_date_of_birth = @UserDateOfBirth, password_salt = @PasswordSalt, marital_status = @MaritalStatus, user_experience = @UserExperience, user_city = @UserCity, user_department = @UserDepartment, builder_user_id = @BuilderUserId WHERE user_id=@UserId",
                users);
        return true;
    }

    public async Task<bool> DeleteUsers(long UserId)
    {
        var deleteUsers = await _dbService.EditData("DELETE FROM users WHERE user_id=@UserId", new {UserId});
        return true;
    }
}
