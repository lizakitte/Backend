using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BackendLab01.Pages;

public class Summary : PageModel
{
    public int Correct { get; set; }
    public int All { get; set; }
    public void OnGet(int correct, int all)
    {
        Correct = correct;
        All = all;
    }
}