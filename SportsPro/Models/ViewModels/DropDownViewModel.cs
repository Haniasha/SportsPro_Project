// Authors: Cecilia Santiago, David McDonald, Ehsan Jalali, Hanieh Shahrokhi
// Workshop 5
// Date May 28, 2021

using System.Collections.Generic;

namespace SportsPro.Models
{
    public class DropDownViewModel
    {
        public Dictionary<string, string> Items { get; set; }
        public string SelectedValue { get; set; }
        public string DefaultValue { get; set; }
        public string DefaultText { get; set; }
        public bool HasDefault => !string.IsNullOrEmpty(DefaultText);
    }
}
