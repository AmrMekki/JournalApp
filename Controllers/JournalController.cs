using Microsoft.AspNetCore.Mvc;
using JournalApp.Models;
using System.Threading.Tasks; // at the top

using JournalApp.Services; // Add this at the top

public class JournalController : Controller
{

    public async Task<IActionResult> Index(string search)
{
    var entries = JournalStorage.LoadEntries();

    if (!string.IsNullOrWhiteSpace(search))
    {
        search = search.ToLower();
        entries = entries
            .Where(e =>
                (!string.IsNullOrEmpty(e.Title) && e.Title.ToLower().Contains(search)) ||
                (!string.IsNullOrEmpty(e.Content) && e.Content.ToLower().Contains(search))
            )
            .ToList();
    }

    ViewBag.Quote = await QuoteService.GetQuoteAsync();

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

    [HttpGet]
    public IActionResult Edit(int id)
    {
        var entries = JournalStorage.LoadEntries();
        var entry = entries.FirstOrDefault(e => e.Id == id);
        if (entry == null) return NotFound();

        return View(entry);
    }
[HttpPost]
public IActionResult Edit(JournalEntry updatedEntry)
{
    Console.WriteLine("✏️ Edit POST action hit");

    var entries = JournalStorage.LoadEntries();
    var index = entries.FindIndex(e => e.Id == updatedEntry.Id);
    if (index == -1) return NotFound();

    updatedEntry.Date = DateTime.Now;
    entries[index] = updatedEntry;
    JournalStorage.SaveEntries(entries);

        TempData["Message"] = "Entry updated successfully!";

    return RedirectToAction("Index");
}


    [HttpGet]
    public IActionResult Delete(int id)
    {
        var entries = JournalStorage.LoadEntries();
        var entry = entries.FirstOrDefault(e => e.Id == id);
        if (entry == null) return NotFound();

        return View(entry);
    }

    [HttpPost, ActionName("Delete")]
// 🆕 Called when the Delete form is submitted
public IActionResult ConfirmDelete(int id)
{
    var entries = JournalStorage.LoadEntries();
    var entry = entries.FirstOrDefault(e => e.Id == id);
    if (entry != null)
    {
        entries.Remove(entry);
        JournalStorage.SaveEntries(entries);
    }
    TempData["Message"] = "Entry deleted.";

    // ✅ Redirect to Index after deletion
        return RedirectToAction("Index");
}


}
