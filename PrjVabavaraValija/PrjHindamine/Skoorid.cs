using PrjAndmebaas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrjHindamine
{
    public class Skoorid
    {
        public int TarkvaraId { get; set; }
        public string Nimi {  get; set; } = string.Empty;
        public int Skoor { get; set; }
        public int MaxSkoor { get; set; }
        public List<Kriteeriumid> SobivadKriteeriumid { get; set; } = new List<Kriteeriumid>();
        public List<Kriteeriumid> PuuduvadKriteeriumid { get; set; } = new List<Kriteeriumid>();
        public override string ToString()
        {
            return $"{Nimi} - {Skoor}/{MaxSkoor}";
        }

    }
}
