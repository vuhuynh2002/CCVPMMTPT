using PagedList;
using SourceCode.Models;
using SourceCode.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace SourceCode.Controllers
{
    public class RateController : Controller
    {
        // GET: Rate
      
        private IRatingService _ratingService;
        private IProductService _productService;
        private IUserService _userService;
        ShopPhuKienEntities context = new ShopPhuKienEntities();

        private RateController(IRatingService ratingService, IProductService productService, IUserService userService)
        {
            _ratingService = ratingService;
            _productService = productService;
    
            _userService = userService;
        }
        // GET: Rate
        public ActionResult Rating(int page = 1)
        {
            if (Session["User"] == null)
            {
                return RedirectToAction("Login", "Admin");
            }
            int pageSize = 5;
            //Get qAs list
            IEnumerable<Rating> raTing = _ratingService.GetListAllRating().OrderBy(x => x.UserID);
            PagedList<Rating> listraTing = new PagedList<Rating>(raTing, page, pageSize);
            //Check null
            if (listraTing != null)
            {
                ViewBag.Page = page;
                //Return view
                return View(listraTing);
            }
            else
            {
                //return 404
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
        }
    }
}