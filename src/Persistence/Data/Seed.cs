using Domain.Constants;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace Persistence.Data;

public class Seed
{
    public static async Task SeedDataAsync(AppDbContext context,
        UserManager<User> userManager, 
        RoleManager<IdentityRole> roleManager)
    {
        await CreateRolesAsync(roleManager);

        await CreateUsersAsync(userManager);

        await AddCoursesAsync(context);
    }

    private static async Task AddCoursesAsync(AppDbContext context)
    {
        if (!context.Courses.Any())
        {
            List<Course> courses =
            [
                new ()
                {
                    Id = "C#-id",
                    Title = "C# Basics",
                    Description = "Learn the basics of C# programming language.",
                    InstructorName = "James Anderson",
                    Lessons =
                    [
                        new Lesson
                        {
                            Id = "lesson-1-id",
                            Title = "Introduction to C#",
                            ContentUrl = "https://example.com/video1",
                            CourseId = "course-1-id"
                        },

                        new Lesson
                        {
                            Id = "lesson-2-id",
                            Title = "Variables and Data Types",
                            ContentUrl = "https://example.com/video2",
                            CourseId = "course-1-id"
                        },

                        new Lesson
                        {
                            Id = "lesson-3-id",
                            Title = "Control Flow Statements",
                            ContentUrl = "https://example.com/video3",
                            CourseId = "course-1-id"
                        }
                    ]
                },

                new ()
                {
                    Id = "JavaScript-id",
                    Title = "JavaScript for Beginners",
                    Description = "Introduction to JavaScript programming.",
                    InstructorName = "Olivia Bennett",
                    Lessons =
                    [
                        new Lesson
                        {
                            Id = "lesson-4-id",
                            Title = "Getting Started with JavaScript",
                            ContentUrl = "https://example.com/video4",
                            CourseId = "course-2-id"
                        },

                        new Lesson
                        {
                            Id = "lesson-5-id",
                            Title = "Functions and Loops",
                            ContentUrl = "https://example.com/video5",
                            CourseId = "course-2-id"
                        }
                    ]
                },

                new ()
                {
                    Id = "Python-id",
                    Title = "Introduction to Python",
                    Description = "Learn Python basics and how to write simple Python scripts.",
                    InstructorName = "Ethan Carter",
                    Lessons =
                    [
                        new Lesson
                        {
                            Id = "lesson-6-id",
                            Title = "Python Syntax and Setup",
                            ContentUrl = "https://example.com/video6",
                            CourseId = "course-3-id"
                        },

                        new Lesson
                        {
                            Id = "lesson-7-id",
                            Title = "Data Structures in Python",
                            ContentUrl = "https://example.com/video7",
                            CourseId = "course-3-id"
                        },

                        new Lesson
                        {
                            Id = "lesson-8-id",
                            Title = "Object-Oriented Programming in Python",
                            ContentUrl = "https://example.com/video8",
                            CourseId = "course-3-id" }
                    ]
                },

                new ()
                {
                    Id = "HTML&CSS-id",
                    Title = "Web Development with HTML & CSS",
                    Description = "A beginner's guide to building websites using HTML and CSS.",
                    InstructorName = "Benjamin Harris",
                    Lessons =
                    [
                        new Lesson
                        {
                            Id = "lesson-9-id",
                            Title = "HTML Basics",
                            ContentUrl = "https://example.com/video9",
                            CourseId = "course-4-id"
                        },

                        new Lesson
                        {
                            Id = "lesson-10-id",
                            Title = "CSS Styling",
                            ContentUrl = "https://example.com/video10",
                            CourseId = "course-4-id"
                        },

                        new Lesson
                        {
                            Id = "lesson-11-id",
                            Title = "Building a Simple Website",
                            ContentUrl = "https://example.com/video11",
                            CourseId = "course-4-id"
                        }
                    ]
                },

                new ()
                {
                    Id = "SQL-id",
                    Title = "Introduction to SQL",
                    Description = "Learn the basics of SQL queries and database management.",
                    InstructorName = "Sarah Lee",
                    Lessons =
                    [
                        new Lesson
                        {
                            Id = "lesson-12-id",
                            Title = "SQL Basics",
                            ContentUrl = "https://example.com/video12",
                            CourseId = "course-5-id"
                        },

                        new Lesson
                        {
                            Id = "lesson-13-id",
                            Title = "SELECT and WHERE Clauses",
                            ContentUrl = "https://example.com/video13",
                            CourseId = "course-5-id" },

                        new Lesson
                        {
                            Id = "lesson-14-id",
                            Title = "JOIN Operations in SQL",
                            ContentUrl = "https://example.com/video14",
                            CourseId = "course-5-id"
                        }
                    ]
                }
            ];

            context.Courses.AddRange(courses);

            await context.SaveChangesAsync();
        }
    }

    private static async Task CreateUsersAsync(UserManager<User> userManager)
    {
        if (!userManager.Users.Any())
        {
            var admin = new User
            {
                Id = "admin-id",
                Email = "admin@test.com",
                UserName = "admin",
                FirstName = "Admin",
                LastName = "Admin"
            };

            await userManager.CreateAsync(admin, "Pa$$w0rd");

            await userManager.AddToRoleAsync(admin, UserRoles.Admin);

            List<User> users =
            [
                new ()
                {
                    Id = "john-id",
                    Email = "john@test.com",
                    UserName = "john",
                    FirstName = "John",
                    LastName = "Doe"
                },

                new ()
                {
                    Id = "jane-id",
                    Email = "jane@test.com",
                    UserName = "jane",
                    FirstName = "Jane",
                    LastName = "Smith"
                },

                new ()
                {
                    Id = "michael-id",
                    Email = "michael@test.com",
                    UserName = "michael",
                    FirstName = "Michael",
                    LastName = "Johnson"
                },

                new ()
                {
                    Id = "emily-id",
                    Email = "emily@test.com",
                    UserName = "emily",
                    FirstName = "Emily",
                    LastName = "Davis"
                },

                new ()
                {
                    Id = "david-id",
                    Email = "david@test.com",
                    UserName = "david",
                    FirstName = "David",
                    LastName = "Wilson"
                }
            ];

            foreach (var user in users)
            {
                await userManager.CreateAsync(user, "Pa$$w0rd");

                await userManager.AddToRoleAsync(user, UserRoles.Student);
            }
        }
    }

    private static async Task CreateRolesAsync(RoleManager<IdentityRole> roleManager)
    {
        if (!roleManager.Roles.Any())
        {
            List<IdentityRole> roles =
            [
                new() { Name = UserRoles.Student, NormalizedName = UserRoles.Student.ToUpper() },

                new() { Name = UserRoles.Admin, NormalizedName = UserRoles.Admin.ToUpper() }
            ];

            foreach (var role in roles)
            {
                await roleManager.CreateAsync(role);
            }
        }
    }
}