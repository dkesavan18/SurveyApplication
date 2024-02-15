
using System.Collections.Generic;
using System.Threading.Tasks;
using SurveyApp.Models;
using SurveyApp.Helpers;
using SurveyApp.Schemas;

namespace SurveyApp.Services;

public class GroupsService : IGroupsService
{
    private readonly IDbService _dbService;

    public GroupsService(IDbService dbService)
    {
        _dbService = dbService;
    }

    public async Task<ResponseObject> CreateGroups(Groups groups)
    {
        ResponseObject respObj = new ResponseObject();
       
        try
        {
            var result =
            await _dbService.EditData(
                "INSERT INTO groups (group_id,group_name,group_description,number_of_members,created_at,no_of_surveys_shared,deleted_at,updated_at,created_by) VALUES (@GroupId, @GroupName, @GroupDescription, @NumberOfMembers, @CreatedAt, @NoOfSurveysShared, @DeletedAt, @UpdatedAt, @CreatedBy)",
                groups);
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

    public async Task<long> GetGroupsCount(RequestFilter filter)
    {
         Util utilities = new Util();
        if(filter == null || String.IsNullOrEmpty(filter.OrderByTable))
        {
            filter.OrderByTable = "Groups";
            filter.OrderByField = "GroupId";
            filter.Sort = "ASC";
        }
        string whereQuery = await utilities.FormWhereQuery(filter);
        string orderQuery = await utilities.FormOrderQuery(filter);
        string countQuery = "select count(*) as count from  groups groups LEFT JOIN users users on users.user_id = groups.created_by";
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

    public async Task<dynamic> GetGroupsList(int index,int limit,RequestFilter filter)
    {
        Util utilities = new Util();
        if(filter == null || String.IsNullOrEmpty(filter.OrderByTable))
        {
            filter.OrderByTable = "Groups";
            filter.OrderByField = "GroupId";
            filter.Sort = "ASC";
        }
        string whereQuery = await utilities.FormWhereQuery(filter);
        string orderQuery = await utilities.FormOrderQuery(filter);
        string selectQuery = "select groups.group_id as GroupId, groups.group_name as GroupName, groups.group_description as GroupDescription, groups.number_of_members as NumberOfMembers, groups.created_at as CreatedAt, groups.no_of_surveys_shared as NoOfSurveysShared, groups.deleted_at as DeletedAt, groups.updated_at as UpdatedAt, groups.created_by as CreatedBy, users.user_id as UserId, users.user_role as UserRole, users.user_name as UserName, users.user_email as UserEmail, users.user_mobile_no as UserMobileNo, users.user_password as UserPassword, users.user_gender as UserGender, users.user_date_of_birth as UserDateOfBirth, users.password_salt as PasswordSalt, users.marital_status as MaritalStatus, users.user_experience as UserExperience, users.user_city as UserCity, users.user_department as UserDepartment, users.builder_user_id as BuilderUserId from groups groups LEFT JOIN users users on users.user_id = groups.created_by";
        if(!String.IsNullOrEmpty(whereQuery))
        {
            selectQuery = selectQuery+" where "+whereQuery;
        }
        if(!String.IsNullOrEmpty(orderQuery))
        {
            selectQuery = selectQuery+" "+orderQuery;
        }
        var groupsList = await _dbService.GetAll<dynamic>(selectQuery+" LIMIT "+limit+ "OFFSET "+index, new { });
      
        return groupsList;
    }


    public async Task<dynamic> GetGroups(long GroupId)
    {
        var groups = await _dbService.GetAsync<dynamic>("select groups.group_id as GroupId, groups.group_name as GroupName, groups.group_description as GroupDescription, groups.number_of_members as NumberOfMembers, groups.created_at as CreatedAt, groups.no_of_surveys_shared as NoOfSurveysShared, groups.deleted_at as DeletedAt, groups.updated_at as UpdatedAt, groups.created_by as CreatedBy, users.user_id as UserId, users.user_role as UserRole, users.user_name as UserName, users.user_email as UserEmail, users.user_mobile_no as UserMobileNo, users.user_password as UserPassword, users.user_gender as UserGender, users.user_date_of_birth as UserDateOfBirth, users.password_salt as PasswordSalt, users.marital_status as MaritalStatus, users.user_experience as UserExperience, users.user_city as UserCity, users.user_department as UserDepartment, users.builder_user_id as BuilderUserId from groups groups LEFT JOIN users users on users.user_id = groups.created_by where groups.group_id=@GroupId", new {GroupId});
        
        return groups;
    }

    public async Task<bool> UpdateGroups(Groups groups)
    {
        var updateGroups =
            await _dbService.EditData(
                "Update groups SET group_name = @GroupName, group_description = @GroupDescription, number_of_members = @NumberOfMembers, created_at = @CreatedAt, no_of_surveys_shared = @NoOfSurveysShared, deleted_at = @DeletedAt, updated_at = @UpdatedAt, created_by = @CreatedBy WHERE group_id=@GroupId",
                groups);
        return true;
    }

    public async Task<bool> DeleteGroups(long GroupId)
    {
        var deleteGroups = await _dbService.EditData("DELETE FROM groups WHERE group_id=@GroupId", new {GroupId});
        return true;
    }
}
