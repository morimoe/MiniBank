    using Microsoft.EntityFrameworkCore;
    using MiniBank.BusinessLogic.Ports;
    using MiniBank.BusinessLogic.Services;
    using MiniBank.BusinessLogic.Settings;
    using MiniBank.DataAccess.Adapters;
    using MiniBank.DataAccess.Context;

    var builder = WebApplication.CreateBuilder(args);

    builder.Services.AddControllers();
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();
    builder.Services.AddDbContext<MiniBankDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));
    builder.Services.AddScoped<IAccountRepository, AccountRepository>();
    builder.Services.AddScoped<IAccountService, AccountService>();
    builder.Services.AddScoped<ITransactionRepository, TransactionRepository>();
    builder.Services.AddScoped<IAuthService, AuthService>();
    builder.Services.AddScoped<IUserRepository, UserRepository>();
    var jwtSettings = builder.Configuration.GetSection("Jwt").Get<JwtSettings>();
    builder.Services.AddSingleton(jwtSettings);

    var app = builder.Build();

    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseHttpsRedirection();

    app.UseAuthorization();

    app.MapControllers();

    app.Run();
