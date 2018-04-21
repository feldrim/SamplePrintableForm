using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SamplePrintableForm.Data;
using SamplePrintableForm.Models;

namespace SamplePrintableForm.Controllers
{
   public class OffersController : Controller
   {
      private readonly AppDbContext _context;

      public OffersController(AppDbContext context)
      {
         _context = context;
      }

      // GET: Offers
      public async Task<IActionResult> Index()
      {
         var appDbContext = _context.Offer.Include(o => o.Customer);
         return View(await appDbContext.ToListAsync());
      }

      // GET: Offers/Details/5
      public async Task<IActionResult> Details(int? id)
      {
         if (id == null) return NotFound();

         var offer = await _context.Offer
            .Include(o => o.Customer)
            .SingleOrDefaultAsync(m => m.Id == id);
         if (offer == null) return NotFound();

         return View(offer);
      }

      // GET: Offers/Create
      public IActionResult Create()
      {
         ViewData["CustomerId"] = new SelectList(_context.Customer, "Id", "Email");
         return View();
      }

      // POST: Offers/Create
      // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
      // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
      [HttpPost]
      [ValidateAntiForgeryToken]
      public async Task<IActionResult> Create([Bind("Id,CustomerId,Date,Price,Currency")]
         Offer offer)
      {
         if (ModelState.IsValid)
         {
            _context.Add(offer);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
         }

         ViewData["CustomerId"] = new SelectList(_context.Customer, "Id", "Email", offer.CustomerId);
         return View(offer);
      }

      // GET: Offers/Edit/5
      public async Task<IActionResult> Edit(int? id)
      {
         if (id == null) return NotFound();

         var offer = await _context.Offer.SingleOrDefaultAsync(m => m.Id == id);
         if (offer == null) return NotFound();
         ViewData["CustomerId"] = new SelectList(_context.Customer, "Id", "Email", offer.CustomerId);
         return View(offer);
      }

      // POST: Offers/Edit/5
      // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
      // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
      [HttpPost]
      [ValidateAntiForgeryToken]
      public async Task<IActionResult> Edit(int id, [Bind("Id,CustomerId,Date,Price,Currency")]
         Offer offer)
      {
         if (id != offer.Id) return NotFound();

         if (ModelState.IsValid)
         {
            try
            {
               _context.Update(offer);
               await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
               if (!OfferExists(offer.Id))
                  return NotFound();
               throw;
            }

            return RedirectToAction(nameof(Index));
         }

         ViewData["CustomerId"] = new SelectList(_context.Customer, "Id", "Email", offer.CustomerId);
         return View(offer);
      }

      // GET: Offers/Delete/5
      public async Task<IActionResult> Delete(int? id)
      {
         if (id == null) return NotFound();

         var offer = await _context.Offer
            .Include(o => o.Customer)
            .SingleOrDefaultAsync(m => m.Id == id);
         if (offer == null) return NotFound();

         return View(offer);
      }

      // POST: Offers/Delete/5
      [HttpPost]
      [ActionName("Delete")]
      [ValidateAntiForgeryToken]
      public async Task<IActionResult> DeleteConfirmed(int id)
      {
         var offer = await _context.Offer.SingleOrDefaultAsync(m => m.Id == id);
         _context.Offer.Remove(offer);
         await _context.SaveChangesAsync();
         return RedirectToAction(nameof(Index));
      }

      private bool OfferExists(int id)
      {
         return _context.Offer.Any(e => e.Id == id);
      }
   }
}