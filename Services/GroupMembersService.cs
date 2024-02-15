
using System.Collections.Generic;
using System.Threading.Tasks;
using SurveyApp.Models;
using SurveyApp.Helpers;
using SurveyApp.Schemas;

namespace SurveyApp.Services;

public class GroupMembersService : IGroupMembersService
{
    private readonly IDbService _dbService;

    public GroupMembersService(IDbService dbService)
    {
        _dbService = dbService;
    }

    public async Task<ResponseObject> CreateGroupMembers(GroupMembers groupmembers)
    {
        ResponseObject respObj = new ResponseObject();
       
        try
        {
            var result =
            await _dbService.EditData(
                "INSERT INTO group_members (group_member_id,group_id,group_member) VALUES (@GroupMemberId, @GroupId, @GroupMember)",
                groupmembers);
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

    public async Task<long> GetGroupMembersCount(RequestFilter filter)
    {
         Util utilities = new Util();
        if(filter == null || String.IsNullOrEmpty(filter.OrderByTable))
        {
            filter.OrderByTable = "GroupMembers";
            filter.OrderByField = "GroupMemberId";
            filter.Sort = "ASC";
        }
        string whereQuery = await utilities.FormWhereQuery(filter);
        string orderQuery = await utilities.FormOrderQuery(filter);
        string countQuery = "select count(*) as count from  group_members gm LEFT JOIN groups groups on groups.group_id = gm.group_id LEFT JOIN users users on users.user_id = gm.group_member";
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

    public async Task<dynamic> GetGroupMembersList(int index,int limit,RequestFilter filter)
    {
        Util utilities = new Util();
        if(filter == null || String.IsNullOrEmpty(filter.OrderByTable))
        {
            filter.OrderByTable = "GroupMembers";
            filter.OrderByField = "GroupMemberId";
            filter.Sort = "ASC";
        }
        string whereQuery = await utilities.FormWhereQuery(filter);
        string orderQuery = await utilities.FormOrderQuery(filter);
        string selectQuery = "select gm.group_member_id as GroupMemberId, gm.group_id as GroupId, gm.group_member as GroupMember, groups.group_name as GroupName, groups.group_description as GroupDescription, groups.number_of_members as NumberOfMembers, groups.created_at as CreatedAt, groups.no_of_surveys_shared as NoOfSurveysShared, groups.deleted_at as DeletedAt, groups.updated_at as UpdatedAt, groups.created_by as CreatedBy, users.user_id as UserId, users.user_role as UserRole, users.user_name as UserName, users.user_email as UserEmail, users.user_mobile_no as UserMobileNo, users.user_password as UserPassword, users.user_gender as UserGender, users.user_date_of_birth as UserDateOfBirth, users.password_salt as PasswordSalt, users.marital_status as MaritalStatus, users.user_experience as UserExperience, users.user_city as UserCity, users.user_department as UserDepartment, users.builder_user_id as BuilderUserId from group_members gm LEFT JOIN groups groups on groups.group_id = gm.group_id LEFT JOIN users users on users.user_id = gm.group_member";
        if(!String.IsNullOrEmpty(whereQuery))
        {
            selectQuery = selectQuery+" where "+whereQuery;
        }
        if(!String.IsNullOrEmpty(orderQuery))
        {
            selectQuery = selectQuery+" "+orderQuery;
        }
        var groupmembersList = await _dbService.GetAll<dynamic>(selectQuery+" LIMIT "+limit+ "OFFSET "+index, new { });
      
        return groupmembersList;
    }


    public async Task<dynamic> GetGroupMembers(long GroupMemberId)
    {
        var groupmembers = await _dbService.GetAsync<dynamic>("select gm.group_member_id as GroupMemberId, gm.group_id as GroupId, gm.group_member as GroupMember, groups.group_name as GroupName, groups.group_description as GroupDescription, groups.number_of_members as NumberOfMembers, groups.created_at as CreatedAt, groups.no_of_surveys_shared as NoOfSurveysShared, groups.deleted_at as DeletedAt, groups.updated_at as UpdatedAt, groups.created_by as CreatedBy, users.user_id as UserId, users.user_role as UserRole, users.user_name as UserName, users.user_email as UserEmail, users.user_mobile_no as UserMobileNo, users.user_password as UserPassword, users.user_gender as UserGender, users.user_date_of_birth as UserDateOfBirth, users.password_salt as PasswordSalt, users.marital_status as MaritalStatus, users.user_experience as UserExperience, users.user_city as UserCity, users.user_department as UserDepartment, users.builder_user_id as BuilderUserId from group_members gm LEFT JOIN groups groups on groups.group_id = gm.group_id LEFT JOIN users users on users.user_id = gm.group_member where gm.group_member_id=@GroupMemberId", new {GroupMemberId});
        
        return groupmembers;
    }

    public async Task<bool> UpdateGroupMembers(GroupMembers groupmembers)
    {
        var updateGroupMembers =
            await _dbService.EditData(
                "Update groupmembers SET group_id = @GroupId, group_member = @GroupMember WHERE group_member_id=@GroupMemberId",
                groupmembers);
        return true;
    }

    public async Task<bool> DeleteGroupMembers(long GroupMemberId)
    {
        var deleteGroupMembers = await _dbService.EditData("DELETE FROM groupmembers WHERE group_member_id=@GroupMemberId", new {GroupMemberId});
        return true;
    }
}
