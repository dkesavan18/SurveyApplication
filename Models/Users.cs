
namespace SurveyApp.Models;

public class Users
{
   public long? UserId {get;set;}
   public string? UserRole {get;set;}
   public string? UserName {get;set;}
   public string? UserEmail {get;set;}
   public long? UserMobileNo {get;set;}
   public string? UserPassword {get;set;}
   public string? UserGender {get;set;}
   public DateTime? UserDateOfBirth {get;set;}
   public string? PasswordSalt {get;set;}
   public Boolean? MaritalStatus {get;set;}
   public int? UserExperience {get;set;}
   public string? UserCity {get;set;}
   public string? UserDepartment {get;set;}
   public long? BuilderUserId {get;set;}

}

public class UsersListResponse 
{
   public List<Users> Users {get;set;}
   public long count {get;set;}
}
