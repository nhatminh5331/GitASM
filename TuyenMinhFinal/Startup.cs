using Microsoft.Owin;
using Owin;
using TuyenMinhFinal.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace TuyenMinhFinal
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            createRolesandUsers();

        }
        private void createRolesandUsers()
        {
            ApplicationDbContext context = new ApplicationDbContext();

            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
            // In Startup iam creating first Admin Role and creating a default Admin User     
            if (!roleManager.RoleExists("Admin"))
            {

                // first we create Admin rool    
                var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
                role.Name = "Admin";
                roleManager.Create(role);
            }
            var user = new ApplicationUser();
            user.UserName = "admin";
            user.Email = "admin@gmail.com";
            string userPassword = "Tuyenminh123@";
            var checkUser = UserManager.Create(user, userPassword);
            if (checkUser.Succeeded)
            {
                var result1 = UserManager.AddToRole(user.Id, "Admin");
            }
            // creating Creating TrainingStaff role     
            if (!roleManager.RoleExists("TrainingStaff"))
            {
                var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
                role.Name = "TrainingStaff";
                roleManager.Create(role);

            }
            // creating Creating Trainer role     
            if (!roleManager.RoleExists("Trainer"))
            {
                var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
                role.Name = "Trainer";
                roleManager.Create(role);
            }
            // creating Creating Trainee role     
            if (!roleManager.RoleExists("Trainee"))
            {
                var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
                role.Name = "Trainee";
                roleManager.Create(role);
            }
        }
    }
}