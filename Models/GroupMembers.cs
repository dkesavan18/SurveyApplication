
namespace SurveyApp.Models;

public class GroupMembers
{
   public long? GroupMemberId {get;set;}
   public long? GroupId {get;set;}
   public long? GroupMember {get;set;}

   public virtual Groups  Groups {get;set;}
   public virtual Users  Users {get;set;}
}

public class GroupMembersListResponse 
{
   public List<GroupMembers> GroupMembers {get;set;}
   public long count {get;set;}
}
