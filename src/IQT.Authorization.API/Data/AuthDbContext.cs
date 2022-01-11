using IQT.Authorization.API.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace IQT.Authorization.API.Data;

public class AuthDbContext : IdentityDbContext<UserModel>
{

    public AuthDbContext()
    {

    }

    public AuthDbContext(DbContextOptions<AuthDbContext> options) : base(options)
    {

    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        base.OnModelCreating(modelBuilder);
    }

}

