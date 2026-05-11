using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrjRiistvara
{
    public interface IRiistvara
    {
        double OSVersioon { get;}
        public double RAM { get; }
        public double VabaKettamaht { get; }

    }
}
