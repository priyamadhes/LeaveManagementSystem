using Microsoft.AspNetCore.Identity;
using System;

namespace LmsWebApi.Data.Entities
{
   public class Appuser:IdentityUser
   {
       public DateTime DateCreated { get; set; }
       public DateTime DateModified { get; set; }
       public string FullName { get; set; }
   } 
}