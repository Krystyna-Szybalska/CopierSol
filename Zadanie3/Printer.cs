using System;
using static Zadanie3.Devices;

namespace Zadanie3
{
    public class Printer : BaseDevice,IPrinter
    
    {
        public new int Counter { get; private set; } = 0;
        public int PrintCounter { get; private set; } = 0;
            
        public void PowerOn()
        {
            if (state == IDevice.State.off) {
                Counter++;
                base.PowerOn();
            }
        }
        public void PowerOff()
        {
            if (state == IDevice.State.on) {
                base.PowerOff();
            }
        }
        public void Print(in Documents.IDocument document)
        {
            if (state == IDevice.State.off) return;
            Console.WriteLine($"{DateTime.Now} Print: {document.GetFileName()}");
            PrintCounter++;
        }
    }
}