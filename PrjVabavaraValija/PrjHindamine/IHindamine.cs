using PrjAndmebaas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrjHindamine
{
    public interface IHindamine
    {
        List<Skoorid> HindaTarkvarad(List<Tarkvaranõuded> sobivadTarkvarad, List<int> valitudKriteeriumid, ILoeAndmed andmed);
    }
}
