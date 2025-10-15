using Microsoft.AspNetCore.Mvc;
using Prueba_desempeno_csharp.Data;
using Prueba_desempeno_csharp.Models;
using Microsoft.EntityFrameworkCore;

namespace Prueba_desempeno_csharp.Controllers;

public class PatientsController : Controller
{
    private readonly AppDbContext _context;
    
    public PatientsController(AppDbContext context)
        {
        _context = context;
        }

    public IActionResult Index()
    {
        var patients = _context.Patients
            .OrderByDescending(p => p.Id) 
            .ToList();
        return View(patients);
    }

    [HttpGet]
    public IActionResult Edit(int id)
    {
        var pacient = _context.Patients.Find(id);
        if (pacient == null)
        {
            return NotFound();
        }
        return View(pacient);
    }
    
    [HttpPost]
    public IActionResult Edit(int id, [Bind("Id,Name,Age,Email,Phone")] Patient patient)
    {
        if (ModelState.IsValid)
        {
            try
            {
                if (_context.Patients.Any(p => p.Email == patient.Email))
                {
                    ModelState.AddModelError("Email", "The Email is already registered.");
                    TempData["message"] = "The Email is already registered";
                    return RedirectToAction(nameof(Index));
                }
                
                _context.Update(patient);
                _context.SaveChanges();
                TempData["Message"] = "Patient updated successfully!";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception)
            {
                return StatusCode(500, "Error updating");
            }
        }
        return View(patient);
    }

    [HttpPost]
    public IActionResult Delete(int id)
    {
        var pacient = _context.Patients.Find(id);
        if (pacient == null)
        {
            return NotFound();
        }
        _context.Patients.Remove(pacient);
        _context.SaveChanges();
        TempData["Message"] = "The patient were deleted successfully";
        return RedirectToAction(nameof(Index));
    }
    
    public IActionResult Details(int id)
    {
        var client = _context.Patients.Find(id);
        if (client == null)
        {
            return NotFound();
        }
        return View(client);
    }

    [HttpPost]
    public IActionResult Create([Bind("Id,Name,Age,Email,Phone")] Patient patient)
    {
        if (ModelState.IsValid)
            {
                try
                {
                    if (_context.Patients.Any(p => p.Email == patient.Email))
                    {
                        ModelState.AddModelError("Email", "The Email is already registered.");
                        TempData["message"] = "The Email is already registered";
                        return RedirectToAction(nameof(Index));
                    }
                    if (_context.Patients.Any(p => p.Id == patient.Id))
                    {
                        ModelState.AddModelError("Id", "The Id is already registered.");
                        TempData["message"] = "The Id is already registered";
                        return RedirectToAction(nameof(Index));
                    }
                    _context.Add(patient);
                    _context.SaveChanges();
                    TempData["message"] = "The patient were created sucessfully";
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