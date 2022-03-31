using System;
using System.Globalization;
using static Zadanie1.Devices;
using static Zadanie1.Documents;

namespace Zadanie1 {
    public class Copier : BaseDevice, IPrinter, IScanner {

        public int PrintCounter {
            get { return _printCounter; }
            private set { }
        } 
        
        public int ScanCounter {
            get { return _scanCounter; }
            private set { }
        }
        private int _scanCounter = 0;
        private int _printCounter = 0;
        public Copier() {
            state = this.GetState();
            if (state==IDevice.State.off) {
                return;
            }
        }
        public void Print(in IDocument document) {
            if (GetState() == IDevice.State.off) return;
            Console.WriteLine($"{DateTime.Now} Print: {document.GetFileName()}");
            _printCounter++;
        }
       
        public void Scan(out IDocument documentScan,IDocument.FormatType formatType = IDocument.FormatType.PDF)
        {
            if (GetState() == IDevice.State.off)
            {
                documentScan = null;
                return;
            }
            
            _scanCounter++;
            switch (formatType)
            {
                case IDocument.FormatType.PDF:
                    documentScan = new PDFDocument($"PDFScan{_scanCounter}.pdf");
                    break;
                case IDocument.FormatType.JPG:
                    documentScan = new PDFDocument($"ImageScan{_scanCounter}.jpg");
                    break;
                case IDocument.FormatType.TXT:
                    documentScan = new PDFDocument($"TextScan{_scanCounter}.txt");
                    break;
                default:
                    documentScan = null;
                    break;
            }
            
            Console.WriteLine($"{DateTime.Now} Scan: {documentScan.GetFileName()}");

        }

        public void ScanAndPrint()
        {
            IDocument document;
            Scan(out document);
            Print(document);
        }
    }
}