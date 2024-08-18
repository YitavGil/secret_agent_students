using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using jwtApp.Models;

namespace jwtApp.Controllers
{
    [Authorize]
    public class MissionController : Controller
    {
        public IActionResult Index()
        {
            var missions = new List<Mission>
            {
                new Mission { Id = 1, Title = "Operation Midnight Shadow", Description = "Infiltrate the enemy base under the cover of darkness.", Difficulty = "Hard" },
                new Mission { Id = 2, Title = "Project Cyber Sentinel", Description = "Protect our systems from a sophisticated hacking attempt.", Difficulty = "Medium" },
                new Mission { Id = 3, Title = "Operation Golden Mirage", Description = "Retrieve the stolen artifact from the desert fortress.", Difficulty = "Expert" }
            };

            return View(missions);
        }

        [HttpPost]
        public IActionResult AcceptMission(int missionId)
        {
            // Here you would typically update a database
            // For now, we'll just redirect with a message
            TempData["Message"] = $"You've accepted mission {missionId}!";
            return RedirectToAction("Index");
        }
    }
}

