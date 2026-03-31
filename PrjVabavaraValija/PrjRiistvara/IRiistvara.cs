using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrjRiistvara
{
    public interface IRiistvara
    {
        public int OSVersion { get;}
        public double RAM { get; }
        public string CPU { get; }
        public double FreeSpace { get; }
        public string Drive { get; }
        public string GPU { get; } 
    }
}
