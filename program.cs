
using SurveyApp.Services;
using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using dotenv.net;
using System.Text;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);
DotEnv.Load();
builder.Services.AddAuthentication(options =>
    {
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(o =>
{
    o.TokenValidationParameters = new TokenValidationParameters
    {
        ValidIssuer = Environment.GetEnvironmentVariable("JWTISSUER"),
        ValidAudience = Environment.GetEnvironmentVariable("JWTAUDIENCE"),
        IssuerSigningKey = new SymmetricSecurityKey
        (Encoding.UTF8.GetBytes(Environment.GetEnvironmentVariable("JWTKEY"))),
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = false,
        ValidateIssuerSigningKey = true
    };
});
builder.Services.AddAuthorization();

// Add services to the container.

builder.Services.AddControllers().AddJsonOptions(options => options.JsonSerializerOptions.PropertyNamingPolicy = null);;

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IDbService, DbService>();

builder.Services.AddScoped<IGroupMembersService, GroupMembersService>();
builder.Services.AddScoped<IGroupsService, GroupsService>();
builder.Services.AddScoped<IOptionCategoriesService, OptionCategoriesService>();
builder.Services.AddScoped<IQuestionSuggestionsService, QuestionSuggestionsService>();
builder.Services.AddScoped<IQuestionsService, QuestionsService>();
builder.Services.AddScoped<IResponsesService, ResponsesService>();
builder.Services.AddScoped<ISurveyActivityLogsService, SurveyActivityLogsService>();
builder.Services.AddScoped<ISurveyBackgroundImagesService, SurveyBackgroundImagesService>();
builder.Services.AddScoped<ISurveyCategoriesService, SurveyCategoriesService>();
builder.Services.AddScoped<ISurveyResponsesService, SurveyResponsesService>();
builder.Services.AddScoped<ISurveysService, SurveysService>();
builder.Services.AddScoped<IUsersService, UsersService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.MapGet("/ping", () =>
{
    
   return "pong";
})
.WithName("GetPingApi")
.WithOpenApi();

app.MapGet("/gettoken",() => {
    var issuer = Environment.GetEnvironmentVariable("JWTISSUER");
    var audience = Environment.GetEnvironmentVariable("JWTAUDIENCE");
    var key = Encoding.ASCII.GetBytes
    (Environment.GetEnvironmentVariable("JWTKEY"));
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[]
            {
                new Claim("Id", Guid.NewGuid().ToString())
            }),
            Expires = DateTime.UtcNow.AddMinutes(5),
            Issuer = issuer,
            Audience = audience,
            SigningCredentials = new SigningCredentials
            (new SymmetricSecurityKey(key),
            SecurityAlgorithms.HmacSha512Signature)
        };
        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescriptor);
        var jwtToken = tokenHandler.WriteToken(token);
        var stringToken = tokenHandler.WriteToken(token);
        return Results.Ok(stringToken);
})
.WithName("GetTokenApi")
.WithOpenApi();

app.Run();
