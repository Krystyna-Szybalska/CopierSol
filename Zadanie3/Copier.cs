using static Zadanie3.Documents;
using static Zadanie3.Devices;

namespace Zadanie3
{
    public class Copier : BaseDevice
    {
        public new int Counter { get; private set; } = 0;
        public Printer Printer { get; set; }
        public Scanner Scanner { get; set; }
        
        public Copier() {
            Printer = new Printer();
            Scanner = new Scanner();
        }
        public void PowerOn() {
            if (state == IDevice.State.off)
            {
                Counter++;
                base.PowerOn();
            }
        }

        public void PowerOff() {
            if (state == IDevice.State.on) {
                base.PowerOff();
            }
        }
        public void Scan(out IDocument document, IDocument.FormatType formatType = IDocument.FormatType.JPG) {
            if (state == IDevice.State.off)
            {
                document = null;
                return;
            }

            Scanner.PowerOn();
            Scanner.Scan(out document, formatType);
            Scanner.PowerOff();
        }

        public void Print(in IDocument document) {
            if (state == IDevice.State.off) return;
            
            Printer.PowerOn();
            Printer.Print(document);
            Printer.PowerOff();
        }

        public void ScanAndPrint()
        {
            if (state == IDevice.State.off) return;
            IDocument doc;
            Scan(out doc, IDocument.FormatType.JPG);
            Print(in doc);
            
        }
    }
}