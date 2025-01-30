 
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using AnimalsMvc.Models;

public class PatientsController : Controller
{
    private readonly HttpClient _httpClient;
    private readonly string _apiUrl = "http://localhost:8080/api/patients"; // Remplacez par l'URL de votre API

    public PatientsController(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    // GET: /Patients/Index
    public async Task<IActionResult> Index()
    {
        var response = await _httpClient.GetStringAsync(_apiUrl);
        var patients = JsonConvert.DeserializeObject<IEnumerable<Patient>>(response);
        return View(patients);
    }

    // GET: /Patients/Details/5
    public async Task<IActionResult> Details(int id)
    {
        var response = await _httpClient.GetStringAsync($"{_apiUrl}/{id}");
        var patient = JsonConvert.DeserializeObject<Patient>(response);

        if (patient == null)
        {
            return NotFound();
        }

        return View(patient);
    }

    // GET: /Patients/Edit/5
    public async Task<IActionResult> Edit(int id)
    {
        var response = await _httpClient.GetStringAsync($"{_apiUrl}/{id}");
        var patient = JsonConvert.DeserializeObject<Patient>(response);

        if (patient == null)
        {
            return NotFound();
        }

        return View(patient);
    }

    // POST: /Patients/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, Patient patient)
    {
        if (id != patient.Id)
        {
            return BadRequest();
        }

        if (ModelState.IsValid)
        {
            var content = new StringContent(
                JsonConvert.SerializeObject(patient),
                System.Text.Encoding.UTF8,
                "application/json");

            var response = await _httpClient.PutAsync($"{_apiUrl}/{id}", content);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction(nameof(Index));
            }

            ModelState.AddModelError("", "Erreur lors de la mise à jour du patient.");
        }

        return View(patient);
    }
}
