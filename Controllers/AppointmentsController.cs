using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Prueba_desempeno_csharp.Data;
using Prueba_desempeno_csharp.Models;
using Prueba_desempeno_csharp.Services;
using Microsoft.EntityFrameworkCore;

namespace Prueba_desempeno_csharp.Controllers;

public class AppointmentsController : Controller
{
    private readonly AppDbContext _context;
    private readonly EmailService _emailService;
    
    public AppointmentsController(AppDbContext context)
        {
        _context = context;
        _emailService = new EmailService();
        }
    
    public IActionResult Index()
    {
        var appointments = _context.Appointments
            .Include(a => a.Patient)
            .Include(a => a.Medic)
            .OrderByDescending(a => a.Id) 
            .ToList();

        ViewBag.Patients = new SelectList(_context.Patients, "Id", "Name");
        ViewBag.Medics = new SelectList(_context.Medics, "Id", "Name");

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
    public IActionResult Edit(int id, [Bind("Id,DateTime,Observations,State,PatientId,MedicId")] Appointment appointment)
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
        var appointment = _context.Appointments
            .Include(a => a.Patient)
            .Include(a => a.Medic)
            .FirstOrDefault(a => a.Id == id);

        if (appointment == null)
        {
            return NotFound();
        }

        return View(appointment);
    }
    
    [HttpPost]
    public async Task<IActionResult> Create([Bind("Id,DateTime,Observations,State,PatientId,MedicId")]Appointment appointment)
    {
        bool AppointmentExist1 = await _context.Appointments.AnyAsync(a =>
            a.PatientId == appointment.MedicId &&
            a.DateTime == appointment.DateTime);

        if (AppointmentExist1)
        {
            ModelState.AddModelError(string.Empty, "The medic has already an appointment at the same time.");
            TempData["message"] = "The medic has already an appointment at the same time";
            return RedirectToAction(nameof(Index));
        }
        
        bool AppointmentExist2 = await _context.Appointments.AnyAsync(a =>
            a.PatientId == appointment.PatientId &&
            a.DateTime == appointment.DateTime);

        if (AppointmentExist2)
        {
            ModelState.AddModelError(string.Empty, "The patient has already an appointment at the same time.");
            TempData["message"] = "The patient has already an appointment at the same time";
            return RedirectToAction(nameof(Index));
        }
        
        if (ModelState.IsValid)
        {
            _context.Appointments.Add(appointment);
            _context.SaveChanges();
            // Load related patient and medic data
            var patient = _context.Patients.Find(appointment.PatientId);
            var medic = _context.Medics.Find(appointment.MedicId);

            // Send confirmation email to the patient
            _emailService.SendAppointmentEmail(
                toEmail: patient.Email,
                patientName: patient.Name,
                appointmentDate: appointment.DateTime,
                medicName: medic.Name
            );
            TempData["Message"] = "Appointment created successfully.";
            return RedirectToAction(nameof(Index));
        }

        ViewBag.Patients = new SelectList(_context.Patients, "Id", "Names");
        ViewBag.Medics = new SelectList(_context.Medics, "Id", "Names");

        return BadRequest();
    }
    
    [HttpPost]
    public IActionResult ChangeState(int id, string newState)
    {
        var appointment = _context.Appointments.FirstOrDefault(a => a.Id == id);
        if (appointment == null)
        {
            return NotFound();
        }

        appointment.State = newState;
        _context.SaveChanges();

        return RedirectToAction("Details", new { id = appointment.Id });
    }

}