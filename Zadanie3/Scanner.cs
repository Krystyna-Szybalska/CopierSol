using System;
using static Zadanie3.Documents;
using static Zadanie3.Devices;

namespace Zadanie3
{
    public class Scanner : Devices.BaseDevice, Devices.IScanner
    {
        public new int Counter { get; private set; } = 0;
        public int ScanCounter { get; private set; } = 0;

        public void Scan(out IDocument document, IDocument.FormatType formatType)
        {
            if (GetState() == IDevice.State.off)
            {
                document = null;
                return;
            }
            
            ScanCounter++;
            switch (formatType)
            {
                case IDocument.FormatType.PDF:
                    document = new PDFDocument($"PDFScan{ScanCounter}.pdf");
                    break;
                case IDocument.FormatType.JPG:
                    document = new PDFDocument($"ImageScan{ScanCounter}.jpg");
                    break;
                case IDocument.FormatType.TXT:
                    document = new PDFDocument($"TextScan{ScanCounter}.txt");
                    break;
                default:
                    document = null;
                    break;
            }
            
            Console.WriteLine($"{DateTime.Now} Scan: {document.GetFileName()}");

        }

            
        public void PowerOn()
        {
            if (state == Devices.IDevice.State.off) {
                Counter++;
                base.PowerOn();
            }
        }
        public void PowerOff()
        {
            if (state == Devices.IDevice.State.on) {
                base.PowerOff();
            }
        }

    }
}