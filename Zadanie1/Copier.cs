using System;
using static Zadanie1.Devices;
using static Zadanie1.Documents;

namespace Zadanie1 {
    public class Copier : BaseDevice, IPrinter, IScanner {
        public new int Counter { get; private set; } = 0;
        public int PrintCounter { get; private set; } = 0;
        public int ScanCounter { get; private set; } = 0;

        public void Print(in IDocument document) {
            if (GetState() == IDevice.State.off) return;
            Console.WriteLine($"{DateTime.Now} Print: {document.GetFileName()}");
            PrintCounter++;
        }
       
        public void Scan(out IDocument documentScan,IDocument.FormatType formatType = IDocument.FormatType.PDF)
        {
            if (GetState() == IDevice.State.off)
            {
                documentScan = null;
                return;
            }
            
            ScanCounter++;
            switch (formatType)
            {
                case IDocument.FormatType.PDF:
                    documentScan = new PDFDocument($"PDFScan{ScanCounter}.pdf");
                    break;
                case IDocument.FormatType.JPG:
                    documentScan = new ImageDocument($"ImageScan{ScanCounter}.jpg");
                    break;
                case IDocument.FormatType.TXT:
                    documentScan = new TextDocument($"TextScan{ScanCounter}.txt");
                    break;
                default:
                    documentScan = null;
                    break;
            }
            
            Console.WriteLine($"{DateTime.Now} Scan: {documentScan.GetFileName()}");

        }

        public void ScanAndPrint()
        {
            if (GetState() == IDevice.State.off) return;
            IDocument document;
            Scan(out document);
            Print(document);
        }
        
        public new void PowerOn() {
            if (state == IDevice.State.off) {
                Counter++;
                base.PowerOn();
            }
        }

        public new void PowerOff()
        {
            if (state == IDevice.State.on)
            {
                base.PowerOff();
            }
        }
    }
}