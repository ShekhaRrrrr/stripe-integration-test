using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Stripe.Checkout;
using stripewebapp.Data;
using stripewebapp.Models;

namespace stripewebapp.Controllers
{
    public class PaymentController : Controller
    {
        private readonly ApplicationDbContext _context;
        public PaymentController(ApplicationDbContext context)
        {
            _context = context;
        }
        public async  Task<IActionResult> Index()
        {
            return View(await _context.Items.ToListAsync());
        }
        [HttpPost]
        public IActionResult Checkout([Bind("Id,Name,ImageUrl,PriceId")] Item item)
        {
            var domain = "https://localhost:7171/";
            var options = new SessionCreateOptions
            {
                LineItems = new List<SessionLineItemOptions>
                {
                  new SessionLineItemOptions
                  {
                    // Provide the exact Price ID (for example, pr_1234) of the product you want to sell
                    Price = $"{item.PriceId}",
                    Quantity = 1,
                  },
                },
                Mode = "payment",
                SuccessUrl = domain + "/Payment/Success.cshtml",
                CancelUrl = domain + "/Payment/Cancel.cshtml",

                //the mode is set to one time payment mode only if we want to add both subscription and payment modes we need to add both payment and subsctiption details
                //in if and else loops
            };
            var service = new SessionService();
            Session session = service.Create(options);

            Response.Headers.Append("Location", session.Url);
            return new StatusCodeResult(303);
        }

        public IActionResult Cancel()
        {
            return View();
        }

        public IActionResult Sucess()
        {
            return View();
        }
    }
}
