using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LmsWebApi.Models.BindingModel;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;
using System.Data;
using Microsoft.Data.SqlClient;

namespace LmsWebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LeaveController : ControllerBase
    {
        private readonly IConfiguration _configuration; //to read config data

        public LeaveController(IConfiguration configuration)
        {
            _configuration = configuration;
        }




        [HttpPost("RequestLeave")]
        //From body -- information will be in request body and  other model request will come from url
        public JsonResult  RequestLeave([FromBody] LeaveModel model)
        {
            try
            {
                var leavemodel = new LeaveModel() { Email = model.Email, FromDate = model.FromDate, ToDate = model.ToDate, LeaveType = model.LeaveType,LeaveDetail = model.LeaveDetail };

                DataTable dt = new DataTable();
                string sqldatasource = _configuration.GetConnectionString("constring");
                using (SqlConnection con = new SqlConnection(sqldatasource))
                {
                    SqlCommand cmd = new SqlCommand("sp_createleave", con);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@Email", leavemodel.Email);
                    cmd.Parameters.AddWithValue("@FromDate", leavemodel.FromDate);
                    cmd.Parameters.AddWithValue("@ToDate", leavemodel.ToDate);
                    cmd.Parameters.AddWithValue("@LeaveType", leavemodel.LeaveType);
                    cmd.Parameters.AddWithValue("@LeaveDetail", leavemodel.LeaveDetail);
                                     

                    con.Open();
                    cmd.ExecuteNonQuery();
                   
                }

                //var result = await _userManager.CreateAsync(user, model.Password);
                //if (result.Succeeded)
                //{
                //    return await Task.FromResult("User has been Registered");
                //}

                // AppContext dbcontext = new AppContext();


                return new JsonResult("Added Leave request");
            }
            catch (Exception ex)
            {
                return new JsonResult(ex.Message);
            }
        }

        [HttpGet("GetAllLeave")]
        //From body -- information will be in request body and  other model request will come from url
        public JsonResult GetAllLeave()
        {
           try
            {
                
                DataTable dt = new DataTable();
                string sqldatasource = _configuration.GetConnectionString("constring");
                using (SqlConnection con = new SqlConnection(sqldatasource))
                {
                    string query = @"SELECT * from EmployeeLeaveDetails";

                    SqlDataReader dr = null;
                    SqlCommand cmd = null;
                    cmd = new SqlCommand(query, con);
                    
                       
                        con.Open();

                        //execute the SQLCommand
                        dr = cmd.ExecuteReader();
                        dt.Load(dr);

                        dr.Close();
                        con.Close();
                    
                }
                return new JsonResult(dt);
            }
            catch (Exception ex)
            {
                return new JsonResult(ex.Message);
            }
        }
    }
}
