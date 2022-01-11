using IQT.Authorization.API.Data;
using IQT.Authorization.API.Entities;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace IQT.Authorization.API.Services;

public class UserInitializerService
{
    private readonly AuthDbContext _context;
    private readonly UserManager<UserModel> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;

    public UserInitializerService(AuthDbContext context, UserManager<UserModel> userManager, RoleManager<IdentityRole> roleManager)
    {
        _context = context;
        _userManager = userManager;
        _roleManager = roleManager;
    }

    public void Initialize()
    {
        if (_context.Database.EnsureCreated())
        {
            CreateUser(
                new UserModel()
                {
                    UserName = "joao@gmail.com",
                    Email = "joao@gmail.com",
                    EmailConfirmed = true
                }, "123456");
        }
    }

    private void CreateUser(UserModel user, string password)
    {
        if (_userManager.FindByNameAsync(user.UserName).Result == null)
        {
            var resultado = _userManager.CreateAsync(user, password).Result;

            if (resultado.Succeeded)
            {
                var resultado1 = _userManager.AddClaimAsync(user, new Claim("Gender", "M")).Result;
                var resultado2 = _userManager.AddClaimAsync(user, new Claim("Function", "Manager")).Result;
                var resultado3 = _userManager.AddClaimAsync(user, new Claim("Department", "Sales")).Result;

                var resultado4 = _roleManager.CreateAsync(new IdentityRole("Admin")).Result;
                var resultado5 = _userManager.AddToRoleAsync(user, "Admin").Result;
            }
        }
    }

}
