
namespace SurveyApp.Models;

public class Groups
{
   public long? GroupId {get;set;}
   public string? GroupName {get;set;}
   public string? GroupDescription {get;set;}
   public int? NumberOfMembers {get;set;}
   public DateTime? CreatedAt {get;set;}
   public int? NoOfSurveysShared {get;set;}
   public DateTime? DeletedAt {get;set;}
   public DateTime? UpdatedAt {get;set;}
   public long? CreatedBy {get;set;}

   public virtual Users  Users {get;set;}
}

public class GroupsListResponse 
{
   public List<Groups> Groups {get;set;}
   public long count {get;set;}
}
