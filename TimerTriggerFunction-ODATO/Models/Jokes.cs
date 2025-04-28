using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace TimerTriggerFunction_ODATO.Models
{
    public partial class Jokes
    {
        public int Id { get; set; }
        public string Setup { get; set; }
        public string Punchline { get; set; }
    }
}
