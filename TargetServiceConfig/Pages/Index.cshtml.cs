using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TargetServiceConfig.Data;
using TargetServiceConfig.Services;

namespace TargetServiceConfig.Pages
{
    public class IndexModel : PageModel
    {
        private readonly Logger _logger;
        public IndexModel(Logger logger)
        {
            _logger = logger;
        }

        public string Log { get; set; }
        public bool Enabled { get; private set; }
        public int Timeout { get; private set; }
        public void OnGet()
        {
            Log = _logger.ToString();
            Enabled = Device.Instance.Enabled;
            Timeout = (Device.Instance.Timeout / 1000);
        }
        public IActionResult OnGetOn(int timeout)
        {
            Device.Instance.On(timeout);

            return Page();
        }
        public IActionResult OnGetOff(int timeout)
        {
            Device.Instance.Off(timeout);
            return Page();
        }

        public IActionResult OnGetClear()
        {
            _logger.Clear();
            return Page();
        }
    }
}
