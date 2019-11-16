using Alg1.Practica.Utils.Logging;
using System;
using System.Linq;

namespace Alg1.Practica.Utils.Models
{
    public class NAW : IComparable<NAW>
    {

        public NAW()
        {
            _naam = "";
            _adres = "";
            _woonplaats = "";
        }

        public NAW(string initialNaam, string initialAdres, string initialWoonplaats)
        {
            _naam = initialNaam;
            _adres = initialAdres;
            _woonplaats = initialWoonplaats;
        }

        private String _naam;
        public String Naam
        {
            get
            {
                return _naam;
            }
            set
            {
                if ((value != null) && (value.Length > 0))
                {
                    _naam = value;
                }
            }
        }

        private string _adres;
        public string Adres
        {
            get
            {
                return _adres;
            }
            set
            {
                _adres = value;
            }
        }

        private string _woonplaats;
        public string Woonplaats
        {
            get
            {
                return _woonplaats;
            }
            set
            {
                _woonplaats = value;
            }
        }

        public bool HeeftNaam(string gezochteNaam)
        {
            return this._naam.Equals(gezochteNaam, StringComparison.Ordinal);
        }

        public bool HeeftAdres(string gezochteAdres)
        {
            return this._adres.Equals(gezochteAdres, StringComparison.Ordinal);
        }

        public bool HeeftWoonplaats(string gezochteWoonplaats)
        {
            return this._woonplaats.Equals(gezochteWoonplaats, StringComparison.Ordinal);
        }

        public void Show()
        {
            System.Console.WriteLine(this.ToString());
        }

        public int CompareTo(NAW andereNaw)
        {
            // signal compare to Logger
            Logger.Instance.LogCompare();

            if (andereNaw.Naam == Naam && andereNaw.Adres == Adres && andereNaw.Woonplaats == Woonplaats)
            {
                return 0;
            }
            else
            {
                if (andereNaw.Woonplaats != Woonplaats)
                { // woonplaatsen zijn verschillend
                    return Woonplaats.CompareTo(andereNaw.Woonplaats);
                }
                else if (andereNaw.Naam != Naam)
                { // woonplaatsen zijn verschillend en namen zijn verschillend
                    return Naam.CompareTo(andereNaw.Naam);
                }
                else
                { // woonplaatsen en namen zijn gelijk
                    return Adres.CompareTo(andereNaw.Adres);
                }
            }

        }

        public override bool Equals(Object obj)
        {
            // Check for null values and compare run-time types.
            if (obj == null || GetType() != obj.GetType())
                return false;

            NAW naw = (NAW)obj;
            return naw.Naam == Naam && naw.Adres == Adres && naw.Woonplaats == Woonplaats;
        }

        public override string ToString()
        {
            return String.Format("\t{{ naam=\"{0}\", adres=\"{1}\", woonplaats=\"{2}\" }}", Naam, Adres, Woonplaats);
        }

        public override int GetHashCode()
        {
            int hash = 17;
            hash = hash * 23 + Naam.GetHashCode();
            hash = hash * 23 + Adres.GetHashCode();
            hash = hash * 23 + Woonplaats.GetHashCode();
            return hash;
        }
    }
}
