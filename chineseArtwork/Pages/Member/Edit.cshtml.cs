using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using chineseArtwork.Models;

namespace chineseArtwork.Pages.Member
{
    public class EditModel : PageModel
    {
        private readonly chineseArtwork.Models.ChineseArtworkContext _context;

        public EditModel(chineseArtwork.Models.ChineseArtworkContext context)
        {
            _context = context;
        }

        [BindProperty]
        public chineseArtwork.Models.Member Member { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var member =  await _context.Members.FirstOrDefaultAsync(m => m.MemId == id);
            if (member == null)
            {
                return NotFound();
            }
            Member = member;

            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Member).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MemberExists(Member.MemId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool MemberExists(int id)
        {
            return _context.Members.Any(e => e.MemId == id);
        }
    }
}
