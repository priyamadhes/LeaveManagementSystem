
using LmsWebApi.Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace LmsWebApi.Data
{
    public class AppDBContext:IdentityDbContext<Appuser,IdentityRole,string>
    {
        public AppDBContext(DbContextOptions options) : base(options) { }

    }
}