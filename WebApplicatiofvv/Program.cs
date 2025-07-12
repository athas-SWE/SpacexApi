using Microsoft.AspNetCore.Authentication.Negotiate;
using Microsoft.AspNetCore.Diagnostics;
using WebApplicatiofvv.Data;
using WebApplicatiofvv.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddSingleton<DatabaseHelper>();
builder.Services.AddHttpClient<LaunchService>();



// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

/*builder.Services.AddAuthentication(NegotiateDefaults.AuthenticationScheme)
   .AddNegotiate();*/

/*builder.Services.AddAuthorization(options =>
{
    // By default, all incoming requests will be authorized according to the default policy.
    options.FallbackPolicy = options.DefaultPolicy;
});*/

var app = builder.Build();

app.UseExceptionHandler(errApp =>
{
    errApp.Run(async context =>
    {
        context.Response.StatusCode = 500;
        context.Response.ContentType = "application/json";
        var error = context.Features.Get<IExceptionHandlerFeature>();
        if (error != null)
        {
            await context.Response.WriteAsync($"{{\"error\": \"{error.Error.Message}\"}}");
        }
    });
});

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
