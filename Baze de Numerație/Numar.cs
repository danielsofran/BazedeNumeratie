using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Baze_de_Numerație
{
    public class Numar
    {
        int _baza=10;
        List<char> nr;

        public int Baza
        {
            get => _baza;
            set
            {
                this.Value = Convert(value).Value;
                _baza = value;
            }
        }

        public string Value {
            get => RemoveZero(nr);
            set 
            {
                nr = new List<char>(value);
                nr.Reverse();
            } 
        }
        public int Length {
            get => nr.Count;
            set
            {
                while (nr.Count < value && nr.Count!=value)
                    nr.Add('0');
            }
        }
        public Numar Reverse
        {
            get
            {
                List<char> c = new List<char>(nr);
                while (c.Last() == '0' && c.Count > 1) c.RemoveAt(c.Count - 1);
                return new Numar(new string(c.ToArray()), Baza);
            }
        }

        public char this[int index]
        {// de la dreapta
            get
            {
                while (nr.Count < index+1)
                    nr.Add('0');
                return nr[index];
            }
            set {
                while (nr.Count < index+1)
                    nr.Add('0');
                nr[index] = value; 
            }
        }

        public static Numar operator+(Numar a, Numar b)
        {
            Numar c = new Numar(0, 10);
            int bz = a.Baza;
            if(a.Baza!=10) a = _Base10(a);
            if(b.Baza!=10) b = _Base10(b);
            int t = 0;
            for(int i=0;i< Math.Max(a.Length, b.Length)+System.Convert.ToInt32(t!=0); ++i)
            {
                int cod = fromCifra(a[i]) + fromCifra(b[i]) + t; /// worst method
                c[i] = toCifra(cod % c.Baza);
                t = cod / c.Baza;
            }
            c = _BaseB(c, bz);
            return c;
        }

        public static Numar operator +(Numar a, char b)
        {
            Numar c = new Numar(0, 10);
            int bz = a.Baza;
            if (a.Baza != 10) a = _Base10(a);
            int t = 0;
            for (int i = 0; i < a.Length + System.Convert.ToInt32(t != 0); ++i)
            {
                int cod = fromCifra(a[i]) + fromCifra(b) + t; /// worst method
                c[i] = toCifra(cod % c.Baza);
                t = cod / c.Baza;
            }
            c = _BaseB(c, bz);
            return c;
        }

        public static Numar operator*(Numar a, char c)
        {
            Numar rez = new Numar(0, a.Baza);
            int t = 0;
            for(int i=0;i<a.Length||t!=0;++i)
            {
                int cod = fromCifra(a[i]) * fromCifra(c) + t;
                rez[i] = toCifra(cod%a.Baza);
                t = cod / a.Baza;
            }
            return rez;
        }

        public static Numar operator/(Numar a, char c)
        {
            Numar b = new Numar(0, a.Baza);
            int r = 0;
            for(int i=a.Length-1;i>=0;--i)
            {
                r = r*a.Baza + fromCifra(a[i]);
                b[i] = toCifra(r / fromCifra(c));
                r = r % fromCifra(c);
            }
            return b;
        }

        public static Numar operator %(Numar a, char c)
        {
            Numar b = new Numar(0, a.Baza);
            int r = 0;
            for (int i = a.Length - 1; i >= 0; --i)
            {
                r = r * a.Baza + fromCifra(a[i]);
                b[i] = toCifra(r / fromCifra(c));
                r = r % fromCifra(c);
            }
            Numar rez = new Numar(0, a.Baza);
            rez.Value = r.ToString();
            return rez;
        }

        public static bool operator==(Numar a1, Numar b1)
        {
            if (a1.Value == "0" && b1.Value == "0") return true;
            Numar a = new Numar(a1.Convert(10)), b = new Numar(b1.Convert(10));
            if (a.ToString().Length != b.ToString().Length) return false;
            for (int i = 0; i < a.Length; ++i)
                if (a[i] != b[i])
                    return false;
            return true;
        }
        public static bool operator !=(Numar a, Numar b) => !(a == b);

        public Numar(int n=0, int b=10) { nr = n.ToString().ToList(); nr.Reverse(); _baza = b; }
        public Numar(string n, int b=10) { nr = n.ToList(); nr.Reverse(); _baza = b; }
        public Numar(Numar n) { nr = n.nr;  _baza = n.Baza; }
        public static char toCifra(int n) => (char)((n >= 10) ? ('A'+n-10) : ('0'+n));//c
        public static int fromCifra(char x) => (x >= 'A') ? (x - 55) : (x-48);//c

        static Numar _Base10(Numar n)
        {
            if (n.Baza == 10) return n;
            Numar rez = new Numar(0, 10);
            Numar bazalai = new Numar(1, 10);
            for(int i=0;i<n.Length;++i, bazalai=bazalai*toCifra(n.Baza))
                rez = rez + bazalai * n[i];
            return rez;
        }

        static Numar _BaseB(Numar n, int b)
        {
            if (n.Baza != 10) throw new Exception("Baza nu este 10 la conversia inspre baza " + b);
            if (b == 10) return n;
            Numar cat = new Numar(n), zero = new Numar();
            Numar rez = new Numar(0, b); int index = 0;
            while (cat!=zero)
            {
                rez[index++] = toCifra(int.Parse((cat % toCifra(b)).Value));
                cat = cat / toCifra(b);
            }
            return rez;
        }

        string RemoveZero(List<char> l)
        {
            while (l.Last() == '0' && l.Count>1) l.RemoveAt(l.Count - 1);
            List<char> c = new List<char>(l);
            c.Reverse();
            return new string(c.ToArray());
        }

        public Numar Convert(int b)
        {
            Numar b10 = _Base10(this);
            b10 = _BaseB(b10, b);
            return b10;
        }

        public override string ToString() => RemoveZero(nr);
    }
}
