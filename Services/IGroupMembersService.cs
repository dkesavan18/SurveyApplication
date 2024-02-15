
using System.Collections.Generic;
using System.Threading.Tasks;
using SurveyApp.Models;
using SurveyApp.Schemas;

namespace SurveyApp.Services;

public interface IGroupMembersService
{
    Task<ResponseObject> CreateGroupMembers(GroupMembers groupmembers);
    Task<dynamic> GetGroupMembers(long id);
    Task<long> GetGroupMembersCount(RequestFilter filter);
    Task<dynamic> GetGroupMembersList(int index,int limit,RequestFilter filter);
    Task<bool> UpdateGroupMembers(GroupMembers groupmembers);
    Task<bool> DeleteGroupMembers(long key);
}
