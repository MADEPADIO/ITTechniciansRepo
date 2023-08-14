using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ITTechniciansRepo.Data;
using ITTechniciansRepo.Models;
using System.Linq.Expressions;
using Microsoft.AspNetCore.Authorization;

namespace ITTechniciansRepo.Controllers
{
    public class KnowledgeBaseController : Controller
    {
        private readonly ITTechniciansRepoContext _context;

        public KnowledgeBaseController(ITTechniciansRepoContext context)
        {
            _context = context;
        }

        // GET: KnowledgeBase
        public async Task<IActionResult> Index()
        {
              return _context.KnowledgeBase != null ? 
                          View(await _context.KnowledgeBase.ToListAsync()) :
                          Problem("Entity set 'ITTechniciansRepoContext.KnowledgeBase'  is null.");
        }

        // GET: KnowledgeBase/ShowSearch
        public async Task<IActionResult> ShowSearch()
        {
            return View();
        }

        // POST: KnowledgeBase/SearchResults
        public async Task<IActionResult> SearchResults(string Search) => View("Index", await _context.KnowledgeBase.Where(j => j.Description.Contains(Search) || j.Diagnosis.Contains(Search) || j.OS.Contains(Search) || j.Model.Contains(Search)).ToListAsync());

        // GET: KnowledgeBase/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.KnowledgeBase == null)
            {
                return NotFound();
            }

            var knowledgeBase = await _context.KnowledgeBase
                .FirstOrDefaultAsync(m => m.Id == id);
            if (knowledgeBase == null)
            {
                return NotFound();
            }

            return View(knowledgeBase);
        }

        // GET: KnowledgeBase/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: KnowledgeBase/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        // The values of the form filled by the user are collected here.
        public async Task<IActionResult> Create([Bind("Id,Description,Diagnosis,Fix,Component,Model,OS")] KnowledgeBase knowledgeBase)
        {
            if (ModelState.IsValid)
            {
                _context.Add(knowledgeBase);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(knowledgeBase);
        }

        // GET: KnowledgeBase/Edit/5
        // Authorization for Vetted Technicians only
        [Authorize]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.KnowledgeBase == null)
            {
                return NotFound();
            }

            var knowledgeBase = await _context.KnowledgeBase.FindAsync(id);
            if (knowledgeBase == null)
            {
                return NotFound();
            }
            return View(knowledgeBase);
        }

        // POST: KnowledgeBase/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Description,Diagnosis,Fix,Component,Model,OS")] KnowledgeBase knowledgeBase)
        {
            if (id != knowledgeBase.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(knowledgeBase);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!KnowledgeBaseExists(knowledgeBase.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(knowledgeBase);
        }

        // GET: KnowledgeBase/Delete/5
        [Authorize]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.KnowledgeBase == null)
            {
                return NotFound();
            }

            var knowledgeBase = await _context.KnowledgeBase
                .FirstOrDefaultAsync(m => m.Id == id);
            if (knowledgeBase == null)
            {
                return NotFound();
            }

            return View(knowledgeBase);
        }

        // POST: KnowledgeBase/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.KnowledgeBase == null)
            {
                return Problem("Entity set 'ITTechniciansRepoContext.KnowledgeBase'  is null.");
            }
            var knowledgeBase = await _context.KnowledgeBase.FindAsync(id);
            if (knowledgeBase != null)
            {
                _context.KnowledgeBase.Remove(knowledgeBase);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool KnowledgeBaseExists(int id)
        {
          return (_context.KnowledgeBase?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
