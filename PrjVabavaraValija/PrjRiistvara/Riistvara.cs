using System.ComponentModel.Design;
using System.Runtime.InteropServices;
using System.Management;

namespace PrjRiistvara

{
    public class Riistvara : IRiistvara
    {
        // Klassisisesed muutujad propertyle andmiseks
        private int _osVersion; // OS versioon 7 - 11

        private double _ram; // Ram GB ühe komakohaga

        private string _cpuName = string.Empty; // CPU nimi, kuvamiseks

        private string _gpuName = string.Empty;

        private double _freeSpace;
        private string _bestDrive = string.Empty;

        // Propertyd andmete välja saatmiseks
        public int OSVersion { get => _osVersion; }
        public double RAM { get => _ram; }
        public string CPU { get => _cpuName; }
        public double FreeSpace { get => _freeSpace; }
        public string Drive { get => _bestDrive; }
        public string GPU { get => _gpuName; }


        // Konstruktor
        public Riistvara()
        {
            OSInformatsioon();
            RAMInformatsioon();
            CPUInformatsioon();
            KettaInformatsioon();
            GPUInformatsioon();
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
                    3 => 81,
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
                _ram = memoryKB / 1024.0 / 1024.0;
            }
        }

        // Meetod Windowsi CPU nime leidmine läbi API
        private void CPUInformatsioon()
        {
            // Loome query CPU nime küsimiseks
            var searcher = new ManagementObjectSearcher("SELECT Name FROM Win32_Processor");

            // Käivitame query ja saame vastused
            foreach (var result in searcher.Get())
            {
                // Tagastame CPU nime meetodist välja
                _cpuName = result["Name"].ToString();
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

        private void GPUInformatsioon()
        {
            ulong maxVRAM = 0;
            // Loome query GPU nime küsimiseks
            var searcher = new ManagementObjectSearcher("SELECT Name, AdapterRAM FROM Win32_VideoController");

            int i = 0;
            // Käivitame query ja saame vastused
            foreach (var result in searcher.Get())
            {
                ulong vram = Convert.ToUInt64(result["AdapterRAM"]);
                if (vram > maxVRAM)
                {
                    maxVRAM = vram;
                    _gpuName = result["Name"].ToString();
                }
            }
        }
    }
}
