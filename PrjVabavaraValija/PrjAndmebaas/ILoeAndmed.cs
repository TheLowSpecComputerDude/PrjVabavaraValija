using PrjRiistvara;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrjAndmebaas
{
    public interface ILoeAndmed
    {
        List<Tarkvaranõuded> LeiaSobivadTarkvarad(IRiistvara riistvara);
    }
}
