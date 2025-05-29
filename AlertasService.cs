using Alertas.Infra;
using Microsoft.EntityFrameworkCore;

public class AlertasService
{
    private readonly SqlContext _context;

    public AlertasService(SqlContext context)
    {
        _context = context;
    }

    // M�todos usando _context
}

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<SqlContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// ...restante da configura��o