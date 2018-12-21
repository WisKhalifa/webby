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
                var adminEmail = "Member1@email.com";
                var adminName = "Admin";
                var adminPassword = "Password123!";
                string adminRole = "Administrator";
                var user1Email = "Customer1@email.com";
                var user1Name = "User1";
                var user2Email = "Customer2@email.com";
                var user2Name = "User2";
                var user3Email = "Customer3@email.com";
                var user3Name = "User3";
                var user4Email = "Customer4@email.com";
                var user4Name = "User4";
                var user5Email = "Customer5@email.com";
                var user5Name = "User5";
                var userPass = "Password123!";



                //Create Admin Role
                var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
                roleManager.Create(new IdentityRole(adminRole));



                CreateUser(context, user1Email, user1Name, userPass);
                CreateUser(context, user2Email, user2Name, userPass);
                CreateUser(context, user3Email, user3Name, userPass);
                CreateUser(context, user4Email, user4Name, userPass);
                CreateUser(context, user5Email, user5Name, userPass);
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
                    userManager.AddToRole(adminUser.Id, adminRole);


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
                                new CommentModels(){Name = "test", Text = "Anonymous Comment Seed Test 2 1"},
                                new CommentModels(){Text = "Anonymous Comment Seed Test 2 2"},
                                new CommentModels(){Text = "Anonymous Comment Seed Test 2 3"}
                            }
                    });
                }
            }
        }

    }
}

