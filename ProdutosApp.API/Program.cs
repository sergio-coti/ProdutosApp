using ProdutosApp.Domain.Interfaces.Services;
using ProdutosApp.Domain.Services;
using ProdutosApp.Infra.SqlServer.Extensions;
using ProdutosApp.Infra.RabbitMQ.Extensions;
using ProdutosApp.Infra.MongoDB.Extensions;
using ProdutosApp.Domain.Profiles;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddRouting(map => map.LowercaseUrls = true);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAutoMapper(cfg => cfg.AddProfile(new AutoMapperProfile()));
builder.Services.AddTransient<IProdutoDomainService, ProdutoDomainService>();
builder.Services.AddTransient<ICategoriaDomainService, CategoriaDomainService>();

builder.Services.AddEntityFramework(builder.Configuration);
builder.Services.AddRabbitMQ(builder.Configuration);
builder.Services.AddMongoDBConfig(builder.Configuration);

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();
app.UseAuthorization();
app.MapControllers();
app.Run();
