using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Baze_de_Numerație
{
    class Cifra
    {
        char c='0';
        public char Value { get => c; set => c = value; }
        public static implicit operator char(Cifra c) => (char)((c.Value >= 10) ? ('A' + c.Value - 10) : ('0' + c.Value));//c
        public static implicit operator int(Cifra c) => (c.Value >= 'A') ? (c.Value - 55) : (c.Value - 48);
    }
}
