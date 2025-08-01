using Microsoft.AspNetCore.Mvc;
using JournalApp.Models;

using JournalApp.Services; // Add this at the top

public class JournalController : Controller
{
    public IActionResult Index()
    {
        var entries = JournalStorage.LoadEntries();
        return View(entries.OrderByDescending(e => e.Date).ToList());
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Create(JournalEntry entry)
    {
        if (ModelState.IsValid)
        {
            var entries = JournalStorage.LoadEntries();

            entry.Id = entries.Count + 1;
            entry.Date = DateTime.Now;

            entries.Add(entry);
            JournalStorage.SaveEntries(entries);

            return RedirectToAction("Index");
        }

        return View(entry);
    }
}
