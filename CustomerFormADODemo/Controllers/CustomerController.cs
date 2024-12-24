using CustomerFormADODemo.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Reflection;
using System.Runtime.Remoting.Messaging;
using System.Web;
using System.Web.Mvc;

namespace CustomerFormADODemo.Controllers
{
    public class CustomerController : Controller
    {
        // GET: Customer
        string cs = ConfigurationManager.ConnectionStrings["dbcs"].ConnectionString;
        public ActionResult CustomerList()
        {
            List<CustomerModelView> list = new List<CustomerModelView>();
            using (SqlConnection con = new SqlConnection(cs))
            {
                string query = "select * from CustomerAdo";
                SqlCommand cmd = new SqlCommand(query, con);
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    list.Add(new CustomerModelView
                    {
                        Id = (int)reader["Id"],
                        Cust_Name = reader["Cust_Name"].ToString(),
                        Address = reader["Address"].ToString(),
                        Mobile_No = reader["Mobile_No"].ToString()
                    });
                }
                con.Close();
            }
            return View(list);
        }
        public ActionResult IndexCustomer()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult IndexCustomer(CustomerModelView model)
        {
            if (ModelState.IsValid)
            {
                using (SqlConnection con = new SqlConnection(cs))
                {
                    string query = "Insert Into CustomerAdo(Cust_Name,Address,Mobile_No) values(@Cust_Name,@Address,@Mobile_No)";
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@Id", model.Id);
                    cmd.Parameters.AddWithValue("@Cust_Name", model.Cust_Name);
                    cmd.Parameters.AddWithValue("@Address", model.Address);
                    cmd.Parameters.AddWithValue("@Mobile_No", model.Mobile_No);
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
                return RedirectToAction("CustomerList");
            }
            return View(model);
        }
        public ActionResult EditCustomer(int Id)
        {
            CustomerModelView model = null;
            using (SqlConnection con = new SqlConnection(cs))
            {
                string query = " select * from CustomerAdo where Id=@Id";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@Id", Id);
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {

                    model = new CustomerModelView
                    {
                        Id = (int)reader["Id"],
                        Cust_Name = reader["Cust_Name"].ToString(),
                        Address = reader["Address"].ToString(),
                        Mobile_No = reader["Mobile_No"].ToString()
                    };
                }
                con.Close();
            }
            if (model == null)
            {
                return HttpNotFound();
            }
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditCustomer(CustomerModelView model)
        {
            if (ModelState.IsValid)
            {
                using (SqlConnection con = new SqlConnection(cs))
                {
                    string query = "Update CustomerAdo set Cust_Name =@Cust_Name, Address=@Address,Mobile_No=@Mobile_No where Id=@Id";
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@Id", model.Id);
                    cmd.Parameters.AddWithValue("@Cust_Name", model.Cust_Name);
                    cmd.Parameters.AddWithValue("@Address", model.Address);
                    cmd.Parameters.AddWithValue("@Mobile_No", model.Mobile_No);
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
                return RedirectToAction("CustomerList");
            }
            return View(model);
        }
        public ActionResult DeleteCustomer(int Id)
        {
            CustomerModelView model = null;
            using (SqlConnection con = new SqlConnection(cs))
            {
                string query = " select * from CustomerAdo where Id=@Id";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@Id", Id);
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {

                    model = new CustomerModelView
                    {
                        Id = (int)reader["Id"],
                        Cust_Name = reader["Cust_Name"].ToString(),
                        Address = reader["Address"].ToString(),
                        Mobile_No = reader["Mobile_No"].ToString()
                    };
                }
                con.Close();
            }
            if (model == null)
            {
                return HttpNotFound();
            }
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteCustomer(CustomerModelView model)
        {
            using (SqlConnection conn = new SqlConnection(cs))
            {
                string query = "DELETE FROM CustomerAdo WHERE Id = @Id";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Id", model.Id);

                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
            }

            return RedirectToAction("CustomerList");
        }
    }

}