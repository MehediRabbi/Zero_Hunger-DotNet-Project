using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Zero_Hunger.DataBase;
using Zero_Hunger.Models;

namespace Zero_Hunger.Controllers
{
    public class ZeroHungerHomeController : Controller
    {
        
        [HttpGet]
        public ActionResult LogIn()
        {
            return View();
        }

        [HttpPost]
        public ActionResult LogIn(Models.LoginDTO logProfile)
        {
           
                var DBcontext = new Zero_HungerEntities1 { };
                var data = DBcontext.LogIninfoes.FirstOrDefault
                (LogIninfo => LogIninfo.Email == logProfile.Email && LogIninfo.Password == logProfile.pass);
                if (data != null)
                {
                    if (data.AcType == 100)
                    {
                        Session["userid"] = data.ID;
                        return RedirectToAction("AdminDashBoard");
                    }
                    else if (data.AcType == 101)
                    {
                        Session["userid"] = data.ID;
                        return RedirectToAction("RestuarentDashBoard");
                    }
                    else
                    {
                        Session["userid"] = data.ID;
                        return RedirectToAction("StaffDashboard");
                    }
                }
               
            
            return View();
        }

        [HttpGet]
        public ActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SignUp(Models.SignUpDTO newUser)
        {
            if(ModelState.IsValid)
            {
                var loginData = new LogIninfo
                {
                    Email = newUser.Email,
                    Phone = newUser.Phone,
                    Password = newUser.pass,
                    AcType = newUser.AcType
                };

                var DBcontext = new Zero_HungerEntities1 { };
                DBcontext.LogIninfoes.Add(loginData);
                DBcontext.SaveChanges();

                var restaurantData = new RestuarentInfo
                {
                    ID = loginData.ID,
                    RestuarantName = newUser.RestaurantName,
                    Address = newUser.Address,
                    OwnerName = newUser.OwnerName,
                };

                DBcontext.RestuarentInfoes.Add(restaurantData);
                DBcontext.SaveChanges();
                
                return RedirectToAction("LogIn");
            }
            return View();
        }
 
        public ActionResult AdminDashBoard()
        {
            return View();
        }

        public ActionResult RestuarentDashBoard ()
        {
            return View();
        }

        [HttpGet]
        public ActionResult RegistarStaff()
        {
            return View();
        }                                                                           

        [HttpPost]
        public ActionResult RegistarStaff(Models.StaffDTO newStaff)
        {
            if(ModelState.IsValid)
            {
                var loginData = new LogIninfo
                {
                    Email = newStaff.Email,
                    Phone = newStaff.Phone,
                    Password = newStaff.Password,
                    AcType = 102
                };



                var DBcontext = new Zero_HungerEntities1 { };
                DBcontext.LogIninfoes.Add(loginData);
                DBcontext.SaveChanges();

                var staffData = new AdminStaffInfo
                {
                    Name = newStaff.Name,
                    ID = loginData.ID,
                    Gender = newStaff.Gender
                };

                DBcontext.AdminStaffInfoes.Add(staffData);
                DBcontext.SaveChanges();

                return RedirectToAction("AdminDashBoard");
            }
            else { return View(); }
           
        }

        [HttpGet]
        public ActionResult RequestGeneration()//R
        {
            return View();
        }
        
        [HttpPost]
        public ActionResult RequestGeneration(Models.RequestGenerationDTO newRequest)
        {
            if(ModelState.IsValid)
            {
                int userId = Convert.ToInt32(Session["UserId"]);
                var requestInfo = new Request
                {
                    Foodtype = newRequest.Foodtype,
                    PreservationHour = newRequest.Preservationtime,
                    PacketCount = newRequest.Packetcount,
                    RID = userId
                };

                var DBcontext = new Zero_HungerEntities1 { };
                DBcontext.Requests.Add(requestInfo);
                DBcontext.SaveChanges();

                return RedirectToAction("RestuarentDashBoard");
            }
            return View();
        }
        [HttpGet]
        public ActionResult OrderDetails()
        {
            var DBcontext = new Zero_HungerEntities1 { };
            var allRequest = DBcontext.Requests.ToList();
            return View(allRequest);
        }

        [HttpPost]
        public ActionResult OrderDetails(Models.StafAssignDTO assign)//A
        {
            var DBcontext = new Zero_HungerEntities1 { };
            var orderID = DBcontext.Requests.Find(assign.ordernumber);
            if (orderID != null)
            {
                orderID.Assign = assign.idnumber;
                DBcontext.SaveChanges();
                return RedirectToAction("AdminDashBoard");
            }
            return RedirectToAction("AdminDashBoard");
        }

        [HttpGet]
        

        public ActionResult staffList()
        {
            var DBcontext = new Zero_HungerEntities1();

            var selectedData = (from loginInfo in DBcontext.LogIninfoes
                                join staffInfo in DBcontext.AdminStaffInfoes
                                on loginInfo.ID equals staffInfo.ID
                                where loginInfo.AcType == 102
                                select new stafDTO
                                {
                                    ID = loginInfo.ID,
                                    Phone = loginInfo.Phone,
                                    Name = staffInfo.Name,
                                    Gender = staffInfo.Gender
                                }).ToList();

            return View(selectedData);
        }

        public ActionResult LogOut()
        {
            Session["userid"] = null;
            Session.Clear();
            Session.Abandon();
            return RedirectToAction("LogIn");
        }

        public ActionResult RequestStatus()//R
        {
            int userId = Convert.ToInt32(Session["UserId"]);
            var DBcontext = new Zero_HungerEntities1 { };
            var orders = DBcontext.Requests.Where(o => o.RID == userId).ToList();
            if (orders != null)
            {
                return View(orders);
            }
            return View();
        }

        public ActionResult StaffDashboard()
        {
            return View();
        }

        [HttpGet]
        public ActionResult AssignedOrderList()//S
        {
            int userId = Convert.ToInt32(Session["UserId"]);
            var DBcontext = new Zero_HungerEntities1 { };
            var orders = DBcontext.Requests.Where(o => o.Assign == userId).ToList();
            if (orders != null)
            {
                return View(orders);
            }
            return View();
        }

        [HttpPost]
        public ActionResult AssignedOrderList(Models.AssignedOrderListDTO values)
        {
            var DBcontext = new Zero_HungerEntities1 { };
            var orderID = DBcontext.Requests.Find(values.ordernumber);
            if (orderID != null)
            {
                orderID.Status = 1;
                DBcontext.SaveChanges();
                return RedirectToAction("StaffDashboard");
            }
            return RedirectToAction("StaffDashboard");
        }
    }
}