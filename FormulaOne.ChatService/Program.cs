using FormulaOne.ChatService.DataService;
using FormulaOne.ChatService.Hubs;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddSignalR();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(opt =>
{
    opt.AddPolicy( "reactApp", builder => 
    {
        builder.WithOrigins("http://localhost:3000", "https://poncinijchatapp.vercel.app")
                .AllowAnyHeader()
                .AllowAnyMethod()
                .AllowCredentials();
    });
});

builder.Services.AddSingleton<SharedDb>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

//ChatGptAdded
app.UseRouting();

app.UseCors("reactApp");

app.UseAuthorization();

//ChatGpt Changed
// app.MapControllers();

// app.MapHub<ChatHub>("/Chat");

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
    endpoints.MapHub<ChatHub>("/Chat");
});


app.Run();
