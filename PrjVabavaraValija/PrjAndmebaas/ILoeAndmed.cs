
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrjAndmebaas
{
    public interface ILoeAndmed
    {
        List<Tarkvaranõuded> LeiaSobivadTarkvarad(double osVersion, double ram, double freeSpace, int kategooriaId);

        List<Kriteeriumid> LoeKriteeriumidKategooriaJargi(int kategooriaId);

        List<int> LoeTarkvaraKriteeriumid(int tarkvaraId);
    }
}
