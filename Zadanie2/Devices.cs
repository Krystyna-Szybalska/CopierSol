using System;
using static Zadanie2.Documents;

namespace Zadanie2 {
    public class Devices {
        public interface IDevice {
            enum State { on, off };

            void PowerOn(); // uruchamia urządzenie, zmienia stan na `on`
            void PowerOff(); // wyłącza urządzenie, zmienia stan na `off
            State GetState(); // zwraca aktualny stan urządzenia

            int Counter { get; }  // zwraca liczbę charakteryzującą eksploatację urządzenia,
            // np. liczbę uruchomień, liczbę wydrukow, liczbę skanów, ...
        }

        public abstract class BaseDevice : IDevice {
            protected IDevice.State state = IDevice.State.off;
            public IDevice.State GetState() => state;
            private int counter = 0;
            public void PowerOff()
            {
                state = IDevice.State.off;
                Console.WriteLine("... Device is off !");
            }

            public void PowerOn()
            {
                if (state == IDevice.State.off) counter++;
                state = IDevice.State.on;
                Console.WriteLine("Device is on ...");
            }

            public int Counter {
                get { return counter; }
                private set { }
            }
        }

        public interface IPrinter : IDevice {
            /// <summary>
            /// Dokument jest drukowany, jeśli urządzenie włączone. W przeciwnym przypadku nic się nie wykonuje
            /// </summary>
            /// <param name="document">obiekt typu IDocument, różny od `null`</param>
            void Print(in IDocument document);
        }

        public interface IScanner : IDevice {
            // dokument jest skanowany, jeśli urządzenie włączone
            // w przeciwnym przypadku nic się dzieje
            void Scan(out IDocument document, IDocument.FormatType formatType);
        }
    }
}