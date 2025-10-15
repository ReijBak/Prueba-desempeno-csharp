using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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

    public IActionResult Index(string speciality)
    {
        // Consulta base
        var medics = _context.Medics.AsQueryable();

        // Si el usuario escribió algo en el filtro, aplicamos el Where
        if (!string.IsNullOrEmpty(speciality))
        {
            medics = medics.Where(m => m.Speciality.Contains(speciality));
        }

        // Orden descendente por ID
        var medicList = medics
            .OrderByDescending(m => m.Id)
            .ToList();

        // Guardamos el texto actual del filtro
        ViewBag.CurrentFilter = speciality;

        return View(medicList);
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
    public IActionResult Edit(int id, [Bind("Id,Name,Email,Speciality,Phone")] Medic medic)
    {
        if (ModelState.IsValid)
        {
            try
            {
                if (_context.Medics.Any(m => m.Email == medic.Email))
                {
                    ModelState.AddModelError("Email", "The Email is already registered.");
                    TempData["message"] = "The Email is already registered";
                    return RedirectToAction(nameof(Index));
                }
                
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
    public IActionResult Create([Bind("Id,Name,Email,Speciality,Phone")] Medic medic)
    {
        if (ModelState.IsValid)
        {
            try
            {
                if (_context.Medics.Any(m => m.Email == medic.Email))
                {
                    ModelState.AddModelError("Email", "The Email is already registered.");
                    TempData["message"] = "The Email is already registered";
                    return RedirectToAction(nameof(Index));
                }
                if (_context.Medics.Any(m => m.Id == medic.Id))
                {
                    ModelState.AddModelError("Id", "The Id is already registered.");
                    TempData["message"] = "The Id is already registered";
                    return RedirectToAction(nameof(Index));
                }
                
                _context.Add(medic);
                _context.SaveChanges();
                TempData["message"] = "The medic were created sucessfully";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception)
            {
                return StatusCode(500, "Error creating");
            }
            
        }
        return BadRequest();
    }
}