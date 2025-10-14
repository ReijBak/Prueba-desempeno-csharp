using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Prueba_desempeno_csharp.Data;
using Prueba_desempeno_csharp.Models;
using Microsoft.EntityFrameworkCore;

namespace Prueba_desempeno_csharp.Controllers;

public class AppointmentsController : Controller
{
    private readonly AppDbContext _context;
    
    public AppointmentsController(AppDbContext context)
        {
        _context = context;
        }
    
    public IActionResult Index()
    {
        var appointments = _context.Appointments
            .Include(a => a.Patient)
            .Include(a => a.Medic)
            .OrderByDescending(a => a.Id) 
            .ToList();

        ViewBag.Patients = new SelectList(_context.Patients, "Id", "Names");
        ViewBag.Medics = new SelectList(_context.Medics, "Id", "Names");

        return View(appointments);
    }

    [HttpGet]
    public IActionResult Edit(int id)
    {
        var appointment = _context.Appointments.Find(id);
        if (appointment == null)
        {
            return NotFound();
        }
        return View(appointment);
    }
    
    [HttpPost]
    public IActionResult Edit(int id, [Bind("Id,DateTime,Observations,PatientId,MedicId")] Appointment appointment)
    {
        if (id != appointment.Id)
        {
            return BadRequest();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(appointment);
                _context.SaveChanges();
                TempData["Message"] = "Appointment updated successfully!";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception)
            {
                return StatusCode(500, "Error updating");
            }
        }
        return View(appointment);
    }

    [HttpPost]
    public IActionResult Delete(int id)
    {
        var appointment = _context.Appointments.Find(id);
        if (appointment == null)
        {
            return NotFound();
        }
        _context.Appointments.Remove(appointment);
        _context.SaveChanges();
        TempData["Message"] = "The Appointment were deleted successfully";
        return RedirectToAction(nameof(Index));
    }
    
    public IActionResult Details(int id)
    {
        var appointment = _context.Appointments.Find(id);
        if (appointment == null)
        {
            return NotFound();
        }
        return View(appointment);
    }
    
    [HttpPost]
    public IActionResult Create([Bind("Id,DateTime,Observations,PatientId,MedicId")]Appointment appointment)
    {
        if (ModelState.IsValid)
        {
            _context.Appointments.Add(appointment);
            _context.SaveChanges();
            TempData["Message"] = "Appointment created successfully.";
            return RedirectToAction(nameof(Index));
        }

        ViewBag.Patients = new SelectList(_context.Patients, "Id", "Names");
        ViewBag.Medics = new SelectList(_context.Medics, "Id", "Names");

        return BadRequest();
    }
}