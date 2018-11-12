namespace webby.Migrations
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using webby.Models;

    internal sealed class Configuration : DbMigrationsConfiguration<webby.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(webby.Models.ApplicationDbContext context)
        {


            if (!context.Users.Any())
            {

                var userStore = new UserStore<ApplicationUser>(context);
                var userManager = new UserManager<ApplicationUser>(userStore);
                var adminEmail = "admin@admin.com";
                var adminName = "Admin";
                var adminPassword = "Admin123!";
                string adminRole = "Administrator";
                var user1Email = "user1@user.com";
                var user1Name = "User1";
                var user2Email = "user2@user.com";
                var user2Name = "User2";
                var userPass = "Test123!";
                


                //Create Admin Role
                var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
                var roleCreateResult = roleManager.Create(new IdentityRole(adminRole));
                if (!roleCreateResult.Succeeded)
                {
                    throw new Exception(string.Join(";", roleCreateResult.Errors));
                }


                CreateUser(context, user1Email, user1Name, userPass);
                CreateUser(context, user2Email, user2Name, userPass);
                CreateAdminUser(context, adminEmail, adminName, adminPassword, adminRole);
                CreateSeveralPosts(context);


                void CreateAdminUser(ApplicationDbContext contxt, string admnEmail, string admnName, string admnPass, string admnRole)
                {
                    //Create Admin User
                    var adminUser = new ApplicationUser
                    {
                        Name = adminName,
                        Email = adminEmail
                    };
                    adminUser.UserName = adminEmail;
                    adminUser.EmailConfirmed = true;
                    userManager.Create(adminUser, adminPassword);
                    context.SaveChanges();

                    //Add Admin Role to Admin User
                    var addAdminRoleResult = userManager.AddToRole(adminUser.Id, adminRole);
                    if (!addAdminRoleResult.Succeeded)
                    {
                        throw new Exception(string.Join(";", addAdminRoleResult.Errors));
                    }

                }
                
                //Add Users
                void CreateUser(ApplicationDbContext contxt, string usrEmail, string usrName, string usrPass)
                {
                    var genUser = new ApplicationUser
                    {
                        Name = usrName,
                        Email = usrEmail
                        
                    };
                    genUser.UserName = usrEmail;
                    genUser.EmailConfirmed = true;
                    userManager.Create(genUser, usrPass);
                    context.SaveChanges();
                }


                //Method to Seed Database with Posts
                void CreateSeveralPosts(ApplicationDbContext Context)
                {
                    context.PostModels.Add(new PostModels()
                    {
                        Title = "Seed Test 1",
                        PostContent = "Seed Test 1 Post Content",
                        Comments = new HashSet<CommentModels>()
                            {
                                new CommentModels(){Text = "Anonymous Comment Seed Test 1"}
                            }
                    });

                    context.PostModels.Add(new PostModels()
                    {
                        Title = "Seed Test 2",
                        PostContent = "Seed Test 2 Post Content",
                        Comments = new HashSet<CommentModels>()
                            {
                                new CommentModels(){Text = "Anonymous Comment Seed Test 2 1"},
                                new CommentModels(){Text = "Anonymous Comment Seed Test 2 2"},
                                new CommentModels(){Text = "Anonymous Comment Seed Test 2 3"}
                            }
                    });
                }
            }
        }

    }
}

