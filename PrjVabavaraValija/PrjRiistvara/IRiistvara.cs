using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrjRiistvara
{
    public interface IRiistvara
    {
        double OSVersion { get;}
        public double RAM { get; }
        public double FreeSpace { get; }
        public string Drive { get; }

    }
}
