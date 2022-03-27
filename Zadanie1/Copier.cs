using System;
using static Zadanie1.Devices;
using static Zadanie1.Documents;

namespace Zadanie1 {
    public class Copier : BaseDevice {

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
            Console.WriteLine($"{DateTime.Now} Print: {document.GetFileName()}");
            _printCounter++;
        }
       
        public void Scan(out IDocument documentScan,IDocument.FormatType formatType = IDocument.FormatType.PDF)
        {
            _scanCounter++;
            Console.WriteLine($"{DateTime.Now} Scan:");
            if (formatType == IDocument.FormatType.PDF)
            {
                Console.WriteLine($"PDFScan{_scanCounter}.pdf");
                documentScan = new PDFDocument("PDFScan{_scanCounter}.pdf");
            } else if (formatType == IDocument.FormatType.JPG) {
                Console.WriteLine($"ImageScan{_scanCounter}.jpg");
                documentScan = new PDFDocument("ImageScan{_scanCounter}.jpg");
            } else if(formatType == IDocument.FormatType.TXT){
                Console.WriteLine($"TextScan{_scanCounter}.txt");
                documentScan = new PDFDocument("TextScan{_scanCounter}.txt");
            }
            else
            {
                documentScan = null;
                //exception
            }
        }

        public void ScanAndPrint() {
        }
    }
}