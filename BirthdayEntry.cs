using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pozdravlyator
{
    public class BirthdayEntry
    {
        public string Name { get; set; }
        public DateTime DateOfBirth { get; set; }

        public override string ToString()
        {
            return $"{Name} - {DateOfBirth:dd.MM.yyyy}";
        }
    }
}
