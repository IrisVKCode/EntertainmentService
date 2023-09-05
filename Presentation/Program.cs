using Infrastructure.Configuration;

var builder = WebApplication.CreateBuilder(args);

builder.Services.ConfigureInfraServices();

//var mappingConfig = new MapperConfiguration(mc =>
//{
//    mc.AddMaps(AppDomain.CurrentDomain.GetAssemblies());
//    mc.AddProfile(new AutoProfiler());
//    mc.ShouldMapMethod = (x) => { return false; };

//});

//IMapper mapper = mappingConfig.CreateMapper();
//builder.Services.AddSingleton(mapper);


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.ConfigureDatabase(builder.Configuration.GetConnectionString("DefaultConnection"));

var httpClientConfigSection = builder.Configuration.GetSection("HttpClients");
builder.Services.Configure<HttpClientConfig>(httpClientConfigSection);
var httpClientConfig = httpClientConfigSection.Get<HttpClientConfig>();

builder.Services.ConfigureHttpClients(httpClientConfig);

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

app.Run();
