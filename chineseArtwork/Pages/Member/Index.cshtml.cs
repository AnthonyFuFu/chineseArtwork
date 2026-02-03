using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using chineseArtwork.Models;

namespace chineseArtwork.Pages.Member
{
    public class IndexModel : PageModel
    {
        private readonly chineseArtwork.Models.ChineseArtworkContext _context;

        public IndexModel(chineseArtwork.Models.ChineseArtworkContext context)
        {
            _context = context;
        }

        public IList<chineseArtwork.Models.Member> Member { get;set; } = default!;

        public async Task OnGetAsync()
        {
            Member = await _context.Members.ToListAsync();
        }
    }
}
