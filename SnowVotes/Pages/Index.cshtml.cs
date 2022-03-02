using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace SnowVotes.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly VoteContext voteContext;
        public long TacoCount { get; set; } = 0;
        public long HamburgerCount { get; set; } = 0;
        public long PizzaCount { get; set; } = 0;
        const string tacos = "Tacos";
        const string hamburgerKing = "Hamburger";
        const string pizzaQueen = "Pizza";

        public IndexModel(ILogger<IndexModel> logger, VoteContext voteContext)
        {
            _logger = logger;
            this.voteContext = voteContext;
        }

        public async Task OnGet()
        {
            await refresh();
        }

        private async Task refresh()
        {
            var taco = await voteContext.Votes.FirstOrDefaultAsync(t => t.Name == tacos);
            var hamburger = await voteContext.Votes.FirstOrDefaultAsync(t => t.Name == hamburgerKing);
            var pizza = await voteContext.Votes.FirstOrDefaultAsync(t => t.Name == pizzaQueen);

            TacoCount = taco?.Total ?? 0;
            HamburgerCount = hamburger?.Total ?? 0;
            PizzaCount = pizza?.Total ?? 0;
        }

        public async Task<IActionResult> OnPostTacoVote()
        {
            await RecordVote(tacos);
            return RedirectToPage();
        }
        public async Task<IActionResult> OnPostHamburgerVote()
        {
            await RecordVote(hamburgerKing);
            return RedirectToPage();
        }
        public async Task<IActionResult> OnPostPizzaVote()
        {
            await RecordVote(pizzaQueen);
            return RedirectToPage();
        }

        private async Task RecordVote(string foodName)
        {
            var record = await voteContext.Votes.FirstOrDefaultAsync(t => t.Name == foodName);
            if (record == null)
            {
                record = new Vote { Name = foodName };
                voteContext.Votes.Add(record);
            }

            record.Total++;
            await voteContext.SaveChangesAsync();

            await refresh();
        }

    }
}