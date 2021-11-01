using System;
using System.Security.Claims;
using System.Threading.Tasks;
using ApplicationCore.ServiceInterfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MovieShopMVC.Services;

namespace MovieShopMVC.Controllers
{
    // all the action methods in User Controller should work only when user is Authenticated (login success)
    public class UserController : Controller
    {
        private readonly ICurrentUserService _currentUserService;
        public UserController(ICurrentUserService currentUserService)
        {
            _currentUserService = currentUserService;
        }

        [HttpPost]
        public async Task<IActionResult> Purchase()
        {
            // purchase a movie when user clicks on Buy button on Movie Details Page
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Favorite()
        {
            // favorite a movie when user clicks on Favorite Button on Movie Details Page
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Review()
        {
            // add a new review done by the user for that movie
            return View();
        }

        [HttpGet]
        // Filters in ASP.NET 
        [Authorize]
        public async Task<IActionResult> Purchases()
        {
            // get the id from HttpCOntext.User.Claims
            /*var userIdentity = this.User.Identity;
            if (userIdentity != null && userIdentity.IsAuthenticated)
            {
                // call the databsae to get the data
                return View();
            }
            

            RedirectToAction("Login", "Account");*/
            // get all the movies purchased by user => List<MovieCard> 

            //int userId = Convert.ToInt32((HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value));
            // call userservie that will give list od moviesCard Models that this user purchased
            // Purchase, dbContext.Purchase.where(u=> u.UserId == id);
            var userId = _currentUserService.UserId;
            //ViewBag.UserId = userId;
            // call the USer
            return View();

        }

        [HttpGet]
        public async Task<IActionResult> Favorites(int id)
        {
            // get all movies favorited by that user
            //var favorite = _currentUserService.UserId == id;
            return View();
        }

        public async Task<IActionResult> Reviews(int id)
        {
            // get all the reviews done by this user
            return View();
        }
    }
}