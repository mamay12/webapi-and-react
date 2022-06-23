using Microsoft.Data.SqlClient;
using Microsoft.OpenApi.Models;
using webapi.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy("CORS Policy",
        builder =>
        {
            builder.AllowAnyMethod()
                   .AllowAnyHeader()
                   .WithOrigins("http://localhost:3000", "https://appname.azurestaticapps.net");
        });
});

// Add services to the container. Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(swaggerGenOptions =>
{
    swaggerGenOptions.SwaggerDoc("v1", new OpenApiInfo { Title = "ASP.NET React", Version = "v1" });
});

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI(swaggerOptions =>
{
    swaggerOptions.DocumentTitle = "ASP.NET React";
    swaggerOptions.SwaggerEndpoint("swagger/v1/swagger.json", "Web API serving a veri simple Post model");
    swaggerOptions.RoutePrefix = String.Empty;
}
);
app.UseHttpsRedirection();
app.UseCors("CORS Policy");

app.MapGet("/get-all-posts", async () => await PostsRepository.GetPostsAsync())
    .WithTags("Posts Endpoints");

app.MapGet("/get-post-by-id/{postId}", async (int postId) =>
{
    Post postToReturn = await PostsRepository.GetPostByIdAsync(postId);

    if (postToReturn != null)
    {
        return Results.Ok(postToReturn);
    }
    else
    {
        return Results.BadRequest();
    }
}).WithTags("Post endpoints");

app.MapPost("/create-post", async (Post postToCreate) =>
{
    bool createSuccesfull = await PostsRepository.CreatePostAsync(postToCreate);

    if (createSuccesfull)
    {
        return Results.Ok("Create succesfull");
    }
    else
    {
        return Results.BadRequest();
    }
}).WithTags("Post endpoints");

app.MapPut("/update-post", async (Post postToUpdate) =>
{
    bool updateSuccesfull = await PostsRepository.UpdatePostAsync(postToUpdate);

    if (updateSuccesfull)
    {
        return Results.Ok("Update succesfull");
    }
    else
    {
        return Results.BadRequest();
    }
}).WithTags("Post endpoints");

app.MapDelete("/delete-post/{postId}", async (int postId) =>
{
    bool deleteSuccesfull = await PostsRepository.DeletePostAsync(postId);

    if (deleteSuccesfull)
    {
        return Results.Ok("Delete succesfull");
    }
    else
    {
        return Results.BadRequest();
    }
}).WithTags("Post endpoints");

app.Run();