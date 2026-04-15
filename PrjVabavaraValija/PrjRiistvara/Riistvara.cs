using System.ComponentModel.Design;
using System.Runtime.InteropServices;
using System.Management;

namespace PrjRiistvara

{
    public class Riistvara : IRiistvara
    {
        // Klassisisesed muutujad propertyle andmiseks
        private double _osVersion; // OS versioon 7 - 11

        private double _ram; // Ram GB ühe komakohaga

        private double _freeSpace; // Vaba ketta ruum 

        private string _bestDrive = string.Empty; // Ketas millel on kõige rohkem ruumi

        // Propertyd andmete välja saatmiseks
        double IRiistvara.OSVersion { get => _osVersion; }
        double IRiistvara.RAM { get => _ram; }
        double IRiistvara.FreeSpace { get => _freeSpace; }
        string IRiistvara.Drive { get => _bestDrive; }

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
                _osVersion = osVer.Build >= 22000 ? 11 : 10;
            }
            else if (osVer.Major == 6)
            {
                _osVersion = osVer.Minor switch
                {
                    1 => 7,
                    2 => 8,
                    3 => 8.1,
                    _ => 0
                };
            }
            else
            {
                _osVersion = 0;
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
            foreach (DriveInfo drive in DriveInfo.GetDrives())
            {
                if(drive.IsReady && drive.DriveType == DriveType.Fixed)
                {
                    double freeGB = drive.AvailableFreeSpace / 1024.00 / 1024.00 / 1024.00;

                    if(freeGB > _freeSpace)
                    {
                        _freeSpace = freeGB;
                        _bestDrive = drive.Name;
                    }
                }
            }
        }
    }
}
