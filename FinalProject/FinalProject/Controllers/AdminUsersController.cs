using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using FinalProject.Models;

namespace FinalProject.Controllers
{

    public class AdminUsersController : ApiController
    {
        [HttpGet]
        [Route("api/GenerateDashboardData")]
        public bool GenerateDashboardData()
        {
            DataGenerator dataGenerator = new DataGenerator();
            return dataGenerator.Generate();
        }
        [HttpGet]
        [Route("api/GetDashboardData")]
        public DashBoardVM GetDashboardData()
        {
            AdminUsers au = new AdminUsers();
            return au.GetDashBoardData();
        }

        [HttpGet]
        [Route("api/GetDashboardDataByPerson")]
        public Patient GetDashBoardDataByPerson(int NumPatient, float Height, float Weight)
        {
            AdminUsers au = new AdminUsers();
            return au.GetDashBoardDataByPerson(NumPatient, Height, Weight);
        }

        [HttpGet]
        [Route("api/adminUsers")]
        public AdminUsers Get(string EmailUser, string PasswordUser)
        {
            AdminUsers au = new AdminUsers();
            return au.Exist(EmailUser, PasswordUser);
        }

        [HttpGet]
        [Route("api/adminUsersUserN")]
        public AdminUsers Getuser(string UserName, string PasswordUser)
        {
            AdminUsers au = new AdminUsers();
            return au.AppUser(UserName, PasswordUser);
        }

    }
}
