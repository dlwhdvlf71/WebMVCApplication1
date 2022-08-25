using Microsoft.Extensions.WebEncoders;
using Microsoft.Net.Http.Headers;
using System.Text.Unicode;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddHttpClient("FakeAPI", httpClient =>
{
    httpClient.BaseAddress = new Uri("https://jsonplaceholder.typicode.com/");

    //httpClient.DefaultRequestHeaders.Add(
    //    HeaderNames.Accept, "application/vnd.github.v3+json");
    //httpClient.DefaultRequestHeaders.Add(
    //    HeaderNames.UserAgent, "HttpRequestsSample");
});

// 한글 문자 출력시 인코딩되는 문제 해결
builder.Services.Configure<WebEncoderOptions>(options =>
{
    options.TextEncoderSettings = new System.Text.Encodings.Web.TextEncoderSettings(UnicodeRanges.All);
});

builder.Services.AddSession();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();



app.UseRouting();

app.UseSession();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
