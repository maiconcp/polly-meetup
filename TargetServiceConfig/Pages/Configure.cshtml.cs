using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TargetServiceConfig.Data;

namespace TargetServiceConfig.Pages
{
    public class ConfigureModel : PageModel
    {
        [BindProperty]
        public bool Enabled { get; set; }
        [BindProperty]
        public int Timeout { get; set; }

        public void OnGet()
        {

        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (Enabled)
                Device.Instance.On(Timeout);
            else
                Device.Instance.Off(Timeout);

            return RedirectToPage("Index");
        }
    }
}
