using System;
using Microsoft.EntityFrameworkCore;
using StudentPortal.web.Models.Entities;

namespace StudentPortal.web.Data;

public class ApplicationDbContext: DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options):base(options)
    {   
    }
    public DbSet<Student> Students {get;set;} 
    // representing a collection of a particular type ,Student 
    // creates a table students in the db
}
