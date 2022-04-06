using System;
using static Zadanie2.Devices;
using static Zadanie2.Documents;

namespace Zadanie2
{
    public class MultifunctionalDevice : BaseDevice, IPrinter, IScanner, IFax
    {
        public new int Counter { get; private set; } = 0;
        public int PrintCounter { get; private set; } = 0;
        public int ScanCounter { get; private set; } = 0;
        public int SendCounter { get; set; }
        public int ReceiveCounter { get; set; }
        
        public void Print(in Documents.IDocument document)
        {
            if (GetState() == IDevice.State.off) return;
            Console.WriteLine($"{DateTime.Now} Print: {document.GetFileName()}");
            PrintCounter++;
        }

        public void Scan(out Documents.IDocument document, Documents.IDocument.FormatType formatType= IDocument.FormatType.PDF)
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
        public void ScanAndPrint()
        {
            if (GetState() == IDevice.State.off) return;
            IDocument document;
            Scan(out document);
            Print(document);
        }
        public void Receive(in Documents.IDocument document)
        {
            if (GetState() == IDevice.State.off) return;
            
            ReceiveCounter++;
            Console.WriteLine($"Receive: {document.GetFileName()}");
            Print(document);
        }

        public void Send()
        {
            if (GetState() == IDevice.State.off) return;
            
            SendCounter++;
            Scan(out var document);
            Console.WriteLine($"Send: {document.GetFileName()}");
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