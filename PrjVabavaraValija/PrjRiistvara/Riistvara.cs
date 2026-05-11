using System.ComponentModel.Design;
using System.Runtime.InteropServices;
using System.Management;

namespace PrjRiistvara

{
    public class Riistvara : IRiistvara
    {
        // Klassisisesed muutujad propertyle andmiseks
        private double _osVersioon; // OS versioon 7 - 11

        private double _ram; // Ram GB ühe komakohaga

        private double _vabaKettamaht; // Vaba ketta ruum 

        // Propertyd andmete välja saatmiseks
        double IRiistvara.OSVersioon { get => _osVersioon; }
        double IRiistvara.RAM { get => _ram; }
        double IRiistvara.VabaKettamaht { get => _vabaKettamaht; }

        // Konstruktor
        public Riistvara()
        {
            OSInformatsioon();
            RAMInformatsioon();
            KettaInformatsioon();
        }

        // Meetod Windowsi 7-11 leidmiseks
        private void OSInformatsioon()
        {
            Version osVer = Environment.OSVersion.Version;

            if (osVer.Major == 10)
            {
                _osVersioon = osVer.Build >= 22000 ? 11 : 10;
            }
            else if (osVer.Major == 6)
            {
                _osVersioon = osVer.Minor switch
                {
                    1 => 7,
                    2 => 8,
                    3 => 8.1,
                    _ => 0
                };
            }
            else
            {
                _osVersioon = 0;
            }
        }

        // Meetod Windowsi RAMi leidmine läbi API
        private void RAMInformatsioon()
        {
            // Loome query RAMi küsimiseks
            var searcher = new ManagementObjectSearcher("SELECT TotalVisibleMemorySize FROM Win32_OperatingSystem");

            // Käivitame query ja saame vastused
            foreach (var result in searcher.Get())
            {
                // Leiab RAM-i kilobaitides
                ulong memoryKB = (ulong)result["TotalVisibleMemorySize"];
                // Arvutame kilobaidid gigabaitideks ning tagastame väärtuse meetodist välja
                _ram = memoryKB / 1024.000 / 1024.000;
            }
        }

        private void KettaInformatsioon()
        {
            foreach (DriveInfo ketas in DriveInfo.GetDrives())
            {
                if(ketas.IsReady && ketas.DriveType == DriveType.Fixed)
                {
                    double freeGB = ketas.AvailableFreeSpace / 1024.00 / 1024.00 / 1024.00;

                    if(freeGB > _vabaKettamaht)
                    {
                        _vabaKettamaht = freeGB;
                    }
                }
            }
        }
    }
}
