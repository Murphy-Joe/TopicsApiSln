using AutoMapper;
using TopicsApi.AutomapperProfiles;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// configuring backend services ...
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(config =>
{
    config.AddDefaultPolicy(pol =>
    {
        pol.AllowAnyOrigin();
        pol.AllowAnyMethod();
        pol.AllowAnyHeader(); // you can't allow this AND allow credentials
    });
});

builder.Services.AddScoped<ILookupOnCallDevs, FakeDevLookup>();

var mapperConfig = new MapperConfiguration(opts =>
{
    opts.AddProfile<TopicsProfile>();
});

builder.Services.AddSingleton<MapperConfiguration>(mapperConfig);
var mapper = mapperConfig.CreateMapper();
builder.Services.AddSingleton<IMapper>(mapper);
builder.Services.AddScoped<IProvideTopicsData, EfSqlTopicsData>();

// The TopicsDataContext is set up as a Scoped service. You can inject it into your controllers, services, and stuff.
builder.Services.AddDbContext<TopicsDataContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("topics"));
});

// building the actual application ...
var app = builder.Build();

app.UseCors(); //OPTIONS request from browsers, by using the services we set up

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

// culminates in this ...
app.Run(); // basically in a forever loop, listening for http requests
