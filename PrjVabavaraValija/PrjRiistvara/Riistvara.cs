using System.ComponentModel.Design;
using System.Runtime.InteropServices;

namespace PrjRiistvara

{
    public class Riistvara : IRiistvara
    {
        // Klassisisesed muutujad propertyle andmiseks
        private int _osVersion;

        // Propertyd andmete välja saatmiseks
        public int OSVersion { get => _osVersion; }

        // Konstruktor
        public Riistvara()
        {
            OSInformatsioon();
        }

        // Windowsi 7-11 leidmine
        private void OSInformatsioon()
        {
            Version osVer = Environment.OSVersion.Version;

            if (osVer.Major == 10)
            {
                _osVersion = osVer.Build >= 22000 ? 11 : 10;
            }
            else if (osVer.Major == 6)
            {
                _osVersion = osVer.Minor switch
                {
                    1 => 7,
                    2 => 8,
                    3 => 81,
                    _ => 0
                };
            }
            else
            {
                _osVersion = 0;
            }
        }
    }
}
