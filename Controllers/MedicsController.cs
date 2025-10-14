using Microsoft.AspNetCore.Mvc;
using Prueba_desempeno_csharp.Data;
using Prueba_desempeno_csharp.Models;
using Microsoft.EntityFrameworkCore;

namespace Prueba_desempeno_csharp.Controllers;

public class MedicsController : Controller
{
    private readonly AppDbContext _context;
    
    public MedicsController(AppDbContext context)
        {
        _context = context;
        }

    public IActionResult Index()
    {
        var medics = _context.Medics
            .OrderByDescending(m => m.Id) 
            .ToList();
        return View(medics);
    }

    [HttpGet]
    public IActionResult Edit(int id)
    {
        var medic = _context.Medics.Find(id);
        if (medic == null)
        {
            return NotFound();
        }
        return View(medic);
    }
    
    [HttpPost]
    public IActionResult Edit(int id, [Bind("Id,Names,LastNames,Email,Speciality,Phone")] Medic medic)
    {
        if (id != medic.Id)
        {
            return BadRequest();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(medic);
                _context.SaveChanges();
                TempData["Message"] = "Medic updated successfully!";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception)
            {
                return StatusCode(500, "Error updating");
            }
        }
        return View(medic);
    }

    [HttpPost]
    public IActionResult Delete(int id)
    {
        var medic = _context.Medics.Find(id);
        if (medic == null)
        {
            return NotFound();
        }
        _context.Medics.Remove(medic);
        _context.SaveChanges();
        TempData["Message"] = "The patient were deleted successfully";
        return RedirectToAction(nameof(Index));
    }
    
    public IActionResult Details(int id)
    {
        var medic = _context.Medics.Find(id);
        if (medic == null)
        {
            return NotFound();
        }
        return View(medic);
    }

    [HttpPost]
    public IActionResult Create([Bind("Names,LastNames,Email,Speciality,Phone")] Medic medic)
    {
        if (ModelState.IsValid)
        {
            _context.Add(medic);
            _context.SaveChanges();
            TempData["message"] = "The medic were created sucessfully";
            return RedirectToAction(nameof(Index));
        }
        return BadRequest();
    }
}