﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SystemLosowaniaOsobyDoOdpowiedzi.Models
{
    public class Student
    {
        public string Name { get; set; }
        public int Number { get; set; }
        public bool IsPresent { get; set; } = true;
        public bool WasPicked { get; set; } = false;
    }
}
