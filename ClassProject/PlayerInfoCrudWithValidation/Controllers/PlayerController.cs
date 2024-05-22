using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PlayerInfoCrudWithValidation.Data;
using PlayerInfoCrudWithValidation.Models;

namespace PlayerInfoCrudWithValidation.Controllers
{
    public class PlayerController : Controller
    {
        private readonly PlayerDbContext playerDbContext;

        public PlayerController(PlayerDbContext playerDbContext)
        {

            this.playerDbContext = playerDbContext;
        }

        // Listing all Players
        public async Task<IActionResult> Index()
        {
            var players = await playerDbContext.Players.ToListAsync();
            return View(players);
        }

        // Adding new PlayerPlayer
        //1) Take to the view page using GET method
        //2) send data from view page to server in POST method
        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(Player playerDataModel)
        {
            if (ModelState.IsValid)
            {
                var newPlayer = new Player
                {
                    Name = playerDataModel.Name,
                    DateOfBirth = playerDataModel.DateOfBirth,
                    Sex = playerDataModel.Sex,
                    Nationality = playerDataModel.Nationality,
                    Address = playerDataModel.Address,
                    PhoneNumber = playerDataModel.PhoneNumber,
                    PlayerPosition = playerDataModel.PlayerPosition
                };

                await playerDbContext.AddAsync(newPlayer);
                await playerDbContext.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View();
        }

    }
}
