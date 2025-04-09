using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SystemLosowaniaOsobyDoOdpowiedzi.Models
{
    public class Class
    {
        public string ClassName { get; set; }
        public List<Student> Students { get; set; } = new List<Student>();
        public int LuckyNumber { get; set; } = -1;
    }
}
