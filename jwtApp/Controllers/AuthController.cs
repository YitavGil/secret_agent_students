using jwtApp.Models;
using jwtApp.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace SecretAgentDashboard.Controllers
{
    public class AuthController : Controller
    {
        private readonly JwtService _jwtService;
        private static List<Agent> _agents = new List<Agent>(); // In-memory storage for demonstration

        public AuthController(JwtService jwtService)
        {
            _jwtService = jwtService;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(Agent agent)
        {
            if (ModelState.IsValid)
            {
                // In a real application, you would hash the password before storing
                _agents.Add(agent);
                return RedirectToAction("Login");
            }
            return View(agent);
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(Agent agent)
        {
            // In a real application, you would validate against a database
            var registeredAgent = _agents.FirstOrDefault(a => a.CodeName == agent.CodeName && a.Password == agent.Password);

            if (registeredAgent != null)
            {
                var token = _jwtService.GenerateToken(agent.CodeName);

                // Store the token in a cookie
                Response.Cookies.Append("JwtToken", token, new CookieOptions
                {
                    HttpOnly = true,
                    Secure = true,
                    SameSite = SameSiteMode.Strict
                });

                return RedirectToAction("Index", "Mission");
            }

            ModelState.AddModelError(string.Empty, "Invalid login attempt.");
            return View(agent);
        }

        [HttpPost]
        public IActionResult Logout()
        {
            // Clear the JWT token cookie
            Response.Cookies.Delete("JwtToken");
            return RedirectToAction("Index", "Home");
        }
    }
}