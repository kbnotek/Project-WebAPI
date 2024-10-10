using Microsoft.EntityFrameworkCore;
using ProjetoFinal.ORM;
using ProjetoFinal.Repositorio;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
// Adicione o contexto do banco de dados
builder.Services.AddDbContext<BdQuantumContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
// Adicione o reposit�rio FuncionarioR
builder.Services.AddScoped<ClienteR>();
// Adicione o reposit�rio EnderecoR
builder.Services.AddScoped<EnderecoR>();
// Adicione o reposit�rio ProdutoR
builder.Services.AddScoped<ProdutoR>();


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
