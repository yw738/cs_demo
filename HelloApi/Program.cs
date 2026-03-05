
using Microsoft.EntityFrameworkCore;
using HelloApi.Data;

var builder = WebApplication.CreateBuilder(args);

// 监听所有网卡，便于本机与局域网访问（避免只监听 localhost 导致"访问不了端口"）
builder.WebHost.UseUrls("http://0.0.0.0:5119");

// 添加数据库连接
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new() { Title = "HelloApi", Version = "v1" });
    options.EnableAnnotations();  // 关键：启用特性注解
});

var app = builder.Build();

// 确保数据库已创建（开发环境）
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.EnsureCreated();
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "HelloApi V1");
    });
}

app.MapControllers();

app.Run();



