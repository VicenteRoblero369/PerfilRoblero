using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PerfilRoblero.Data;
using PerfilRoblero.Modelos;

namespace PerfilRoblero.Utilidades.Inicializador
{
    public class DbInitializer : IDbInitializer
    {
        private readonly ApplicationDbContext _db;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public DbInitializer(ApplicationDbContext db, UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _db = db;
            _userManager = userManager;
            _roleManager = roleManager;
        }
        public void Initialize()
        {
            try
            {
                if (_db.Database.GetPendingMigrations().Count() > 0)
                {
                    _db.Database.Migrate();
                }
            }
            catch (Exception)
            {

                throw;
            }

            if (_db.Roles.Any(r => r.Name == DS.Role_Admin)) return;

            _roleManager.CreateAsync(new IdentityRole(DS.Role_Admin)).GetAwaiter().GetResult();
            _roleManager.CreateAsync(new IdentityRole(DS.Role_Usuario)).GetAwaiter().GetResult();


            _userManager.CreateAsync(new UsuarioAplicacion
            {
                UserName = "vicentrroblero61@gmail.com",
                Email = "vicentrroblero61@gmail.com",
                EmailConfirmed = true,
                Nombres = "Vicente",
                Apellidos = "Roblero"
            }, "Admin123*").GetAwaiter().GetResult();  // Password


            UsuarioAplicacion user = _db.UsuarioAplicacion.Where(u => u.UserName == "vicentrroblero61@gmail.com").FirstOrDefault();

            _userManager.AddToRoleAsync(user, DS.Role_Admin).GetAwaiter().GetResult();


        }
    }
}

