using DataAccess.BcMoore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Models.Data.BcMoore;
using Services;
using System.Linq;

namespace Website.Pages.Teams
{
    public class IndexModel : PageModel
    {
        private readonly BcMooreService _service;
        private readonly BcMooreDataAccess _data;

        public IndexModel()
        {
            _data = new();
            _service = new(_data);
        }

        public IList<Team> Teams { get; set; } = default!;

        public async Task OnGetAsync()
        {
            IEnumerable<Team> thisThing = await _service.GetTeams();
            Teams = thisThing.ToList();
        }
    }
}
