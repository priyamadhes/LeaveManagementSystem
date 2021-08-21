using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LmsWebApi.Models.BindingModel
{
    public class LeaveModel
    {
        [Required]
        public string Email { get; set; }
       
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public string LeaveType { get; set; }
        public string LeaveDetail { get; set; }
    }
}
