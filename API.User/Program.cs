using BLL.Bussiness;
using BLL.Inerfaces;
using DAL.Helper;
using DAL.Interfaces;
using DAL.Repository;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddTransient<ExcuteProcedure, ConnectToDatabase>();
builder.Services.AddTransient<IcustomerRepository, customerRepository>();
builder.Services.AddTransient<IcustomerBusiness, customerBusiness>();
builder.Services.AddTransient<IproductRepository, productRepository>();
builder.Services.AddTransient<IproductBusiness, productBusiness>();
builder.Services.AddTransient<IratingRepository, ratingRepository>();
builder.Services.AddTransient<IratingBusiness, ratingBussiness>();
builder.Services.AddTransient<IcartRepository, cartRepository>();
builder.Services.AddTransient<IcartBusiness, cartBusiness>();
builder.Services.AddTransient<IbillRepository, billRepository>();
builder.Services.AddTransient<IbillBusiness, billBusiness>();



// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


// Cấu hình CORS
app.UseCors(builder =>
{
    builder.WithOrigins("http://127.0.0.1:5501") // Thay đổi domain của frontend 
        .AllowAnyHeader()
        .AllowAnyMethod();
});

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
