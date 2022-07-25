using SPCrudUsingEntityMVC.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SPCrudUsingEntityMVC.Controllers
{
    public class ProductController : Controller
    {
        public ActionResult Index()
        {
            var db = new ArchiesDbEntities();
            var data = db.SP_SHOWPRODUCT();
            return View(data.ToList());
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(tb_ProductMaster collection)
        {
            try
            {
                SqlParameter params1 = new SqlParameter("@ProductName", collection.ProductName);
                SqlParameter params2 = new SqlParameter("@ManufaturingDate", collection.ManufaturingDate);
                SqlParameter params3 = new SqlParameter("@Discription", collection.Discription);
                SqlParameter params4 = new SqlParameter("@Heading", collection.Heading);
                SqlParameter params5 = new SqlParameter("@Isdeleted", 1);
                SqlParameter params6 = new SqlParameter("@Created", 1);
                SqlParameter params7 = new SqlParameter("@CreatedOn", DateTime.Now);

                var db = new ArchiesDbEntities();
                var data = db.Database.ExecuteSqlCommand("SP_ADDPRODUCT @ProductName, @ManufaturingDate, @Discription, @Heading, @Isdeleted, @Created, @CreatedOn", params1, params2, params3, params4, params5, params6, params7);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        [HttpGet]
        public ActionResult Edit(int Id)
        {
            var db = new ArchiesDbEntities();
            SqlParameter param = new SqlParameter("@Id", Id);
            var data = db.Database.SqlQuery<tb_ProductMaster>(@"exec GetEditId @Id", param).FirstOrDefault();
            return View(data);
        }


        [HttpPost]
        public ActionResult Edit(int Id, tb_ProductMaster collection)
        {
            try
            {
                var db = new ArchiesDbEntities();
                SqlParameter params0 = new SqlParameter("@Id", collection.Id);
                SqlParameter params1 = new SqlParameter("@ProductName", collection.ProductName);
                SqlParameter params2 = new SqlParameter("@ManufaturingDate", collection.ManufaturingDate);
                SqlParameter params3 = new SqlParameter("@Discription", collection.Discription);
                SqlParameter params4 = new SqlParameter("@Heading", collection.Heading);
                SqlParameter params6 = new SqlParameter("@Modified", 1);
                SqlParameter params7 = new SqlParameter("@ModifiedOn", DateTime.Now);


                var data = db.Database.ExecuteSqlCommand("SP_EditPostId @Id, @ProductName, @ManufaturingDate, @Discription, @Heading, @Modified, @ModifiedOn", params0, params1, params2, params3, params4, params6, params7);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        [HttpGet]
        public ActionResult Delete(int Id)
        {
            ViewBag.id = Id;
            return View();
        }

        [HttpGet] 
        public ActionResult DeletePost(int Id)
        {
            var db = new ArchiesDbEntities();
            SqlParameter param = new SqlParameter("@Id", Id);
            var data = db.Database.ExecuteSqlCommand("exec SP_REMOVEPRODUCT @Id", param);
            return RedirectToAction("Index");
        }
    }
}
