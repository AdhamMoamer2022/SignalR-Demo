using SignalRDemo.HubConfig;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddCors(
    options =>
            {
                options.AddPolicy("AllowAllHeaders",
                    builder =>
                    {
                        builder.AllowAnyOrigin()
                                .AllowAnyHeader()
                                .AllowAnyMethod();
                    });
            }
);

builder.Services.AddSignalR(
    options =>
    {
        options.EnableDetailedErrors = true;
    }
);

builder.Services.AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseCors("AllowAllHeaders");

app.UseEndpoints(endpoints =>
         { 
             endpoints.MapControllers();
             endpoints.MapHub<MyHub>("/toastr");
         }
);

app.UseAuthorization();

app.MapRazorPages();

app.Run();
