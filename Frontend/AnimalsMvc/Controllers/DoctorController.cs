using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using AnimalsMvc.Models;

public class DoctorsController : Controller
{
    private readonly HttpClient _httpClient;
    private readonly string _apiUrl = "http://localhost:8080/api/medecins"; // Remplacez par l'URL de votre API

    public DoctorsController(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    // GET: /Doctors/Index
    public async Task<IActionResult> Index()
    {
        var response = await _httpClient.GetAsync(_apiUrl);
        if (response.IsSuccessStatusCode)
        {
            var doctors = JsonConvert.DeserializeObject<IEnumerable<Doctor>>(await response.Content.ReadAsStringAsync());
            return View(doctors);
        }
        else
        {
            // Gérer l'erreur ici
            return StatusCode((int)response.StatusCode, "Erreur lors de la récupération des données.");
        }
    }

    // GET: /Doctors/Details/5
    public async Task<IActionResult> Details(int id)
    {
        var response = await _httpClient.GetAsync($"{_apiUrl}/{id}");
        if (response.IsSuccessStatusCode)
        {
            var doctor = JsonConvert.DeserializeObject<Doctor>(await response.Content.ReadAsStringAsync());
            if (doctor == null)
            {
                return NotFound();
            }
            return View(doctor);
        }
        else
        {
            // Gérer l'erreur ici
            return StatusCode((int)response.StatusCode, "Erreur lors de la récupération des données.");
        }
    }

    // GET: /Doctors/Edit/5
    public async Task<IActionResult> Edit(int id)
    {
        var response = await _httpClient.GetAsync($"{_apiUrl}/{id}");
        if (response.IsSuccessStatusCode)
        {
            var doctor = JsonConvert.DeserializeObject<Doctor>(await response.Content.ReadAsStringAsync());
            if (doctor == null)
            {
                return NotFound();
            }
            return View(doctor);
        }
        else
        {
            // Gérer l'erreur ici
            return StatusCode((int)response.StatusCode, "Erreur lors de la récupération des données.");
        }
    }

    // POST: /Doctors/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, Doctor doctor)
    {
        if (id != doctor.Id)
        {
            return BadRequest();
        }

        if (ModelState.IsValid)
        {
            var content = new StringContent(
                JsonConvert.SerializeObject(doctor),
                System.Text.Encoding.UTF8,
                "application/json");

            var response = await _httpClient.PutAsync($"{_apiUrl}/{id}", content);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction(nameof(Index));
            }

            ModelState.AddModelError("", "Erreur lors de la mise à jour du doctor.");
        }

        return View(doctor);
    }
}
