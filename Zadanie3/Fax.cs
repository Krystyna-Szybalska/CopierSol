using System;

namespace Zadanie3
{
    public class Fax : Devices.BaseDevice, Devices.IFax, Devices.IPrinter{
        public int SendCounter { get; private set; } = 0;
        public int ReceiveCounter { get; private set; } = 0;
        public void Receive(in Documents.IDocument document)
        {
            if (GetState() == Devices.IDevice.State.off) return;
            
            ReceiveCounter++;
            Console.WriteLine($"Receive: {document.GetFileName()}");
            Print(document);
        }

        public void Send(in Documents.IDocument document)
        {
            if (state == Devices.IDevice.State.off) return;
            SendCounter++;
            Console.WriteLine($"Send: {document.GetFileName()}");
        }

        public void Print(in Documents.IDocument document)
        {
            if (state == Devices.IDevice.State.off) return;
            Console.WriteLine($"{DateTime.Now} Print: {document.GetFileName()}");
        }
    }
}