using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;
using Zadanie3;

namespace TestProject
{
    [TestClass]
    public class Zadanie3UnitTests
    {
        //Testy sprawdzające działanie "Copier"
        [TestMethod]
        public void Copier_GetState_StateOff()
        {
            var copier = new Copier();
            copier.PowerOff();

            Assert.AreEqual(Devices.IDevice.State.off, copier.GetState()); 
        }

        [TestMethod]
        public void Copier_GetState_StateOn()
        {
            var copier = new Copier();
            copier.PowerOn();

            Assert.AreEqual(Devices.IDevice.State.on, copier.GetState());
        }


        // weryfikacja, czy po wywołaniu metody `Print` i włączonej kopiarce w napisie pojawia się słowo `Print`
        // wymagane przekierowanie konsoli do strumienia StringWriter
        [TestMethod]
        public void Copier_Print_DeviceOn()
        {
            var copier = new Copier();
            copier.PowerOn();

            var currentConsoleOut = Console.Out;
            currentConsoleOut.Flush();
            using( var consoleOutput = new ConsoleRedirectionToStringWriter() )
            {
                Documents.IDocument doc1 = new Documents.PDFDocument("aaa.pdf");
                copier.Print(in doc1);
                Assert.IsTrue(consoleOutput.GetOutput().Contains("Print"));
            }
            Assert.AreEqual(currentConsoleOut, Console.Out);   
        }

        // weryfikacja, czy po wywołaniu metody `Print` i wyłączonej kopiarce w napisie NIE pojawia się słowo `Print`
        // wymagane przekierowanie konsoli do strumienia StringWriter
        [TestMethod]
        public void Copier_Print_DeviceOff()
        {
            var copier = new Copier();
            copier.PowerOff();

            var currentConsoleOut = Console.Out;
            currentConsoleOut.Flush();
            using (var consoleOutput = new ConsoleRedirectionToStringWriter())
            {
                Documents.IDocument doc1 = new Documents.PDFDocument("aaa.pdf");
                copier.Print(in doc1);
                Assert.IsFalse(consoleOutput.GetOutput().Contains("Print"));
            }
            Assert.AreEqual(currentConsoleOut, Console.Out);
        }

        // weryfikacja, czy po wywołaniu metody `Scan` i wyłączonej kopiarce w napisie NIE pojawia się słowo `Scan`
        // wymagane przekierowanie konsoli do strumienia StringWriter
        [TestMethod]
        public void Copier_Scan_DeviceOff()
        {
            var copier = new Copier();
            copier.PowerOff();

            var currentConsoleOut = Console.Out;
            currentConsoleOut.Flush();
            using (var consoleOutput = new ConsoleRedirectionToStringWriter())
            {
                Documents.IDocument doc1;
                copier.Scan(out doc1);
                Assert.IsFalse(consoleOutput.GetOutput().Contains("Scan"));
            }
            Assert.AreEqual(currentConsoleOut, Console.Out);
        }

        // weryfikacja, czy po wywołaniu metody `Scan` i wyłączonej kopiarce w napisie pojawia się słowo `Scan`
        // wymagane przekierowanie konsoli do strumienia StringWriter
        [TestMethod]
        public void Copier_Scan_DeviceOn()
        {
            var copier = new Copier();
            copier.PowerOn();

            var currentConsoleOut = Console.Out;
            currentConsoleOut.Flush();
            using (var consoleOutput = new ConsoleRedirectionToStringWriter())
            {
                Documents.IDocument doc1;
                copier.Scan(out doc1);
                Assert.IsTrue(consoleOutput.GetOutput().Contains("Scan"));
            }
            Assert.AreEqual(currentConsoleOut, Console.Out);
        }

        // weryfikacja, czy wywołanie metody `Scan` z parametrem określającym format dokumentu
        // zawiera odpowiednie rozszerzenie (`.jpg`, `.txt`, `.pdf`)
        [TestMethod]
        public void Copier_Scan_FormatTypeDocument()
        {
            var copier = new Copier();
            copier.PowerOn();

            var currentConsoleOut = Console.Out;
            currentConsoleOut.Flush();
            using (var consoleOutput = new ConsoleRedirectionToStringWriter())
            {
                Documents.IDocument doc1;
                copier.Scan(out doc1, formatType: Documents.IDocument.FormatType.JPG);
                Assert.IsTrue(consoleOutput.GetOutput().Contains("Scan"));
                Assert.IsTrue(consoleOutput.GetOutput().Contains(".jpg"));

                copier.Scan(out doc1, formatType: Documents.IDocument.FormatType.TXT);
                Assert.IsTrue(consoleOutput.GetOutput().Contains("Scan"));
                Assert.IsTrue(consoleOutput.GetOutput().Contains(".txt"));

                copier.Scan(out doc1, formatType: Documents.IDocument.FormatType.PDF);
                Assert.IsTrue(consoleOutput.GetOutput().Contains("Scan"));
                Assert.IsTrue(consoleOutput.GetOutput().Contains(".pdf"));
            }
            Assert.AreEqual(currentConsoleOut, Console.Out);
        }


        // weryfikacja, czy po wywołaniu metody `ScanAndPrint` i wyłączonej kopiarce w napisie pojawiają się słowa `Print`
        // oraz `Scan`
        // wymagane przekierowanie konsoli do strumienia StringWriter
        [TestMethod]
        public void Copier_ScanAndPrint_DeviceOn()
        {
            var copier = new Copier();
            copier.PowerOn();

            var currentConsoleOut = Console.Out;
            currentConsoleOut.Flush();
            using (var consoleOutput = new ConsoleRedirectionToStringWriter())
            {
                copier.ScanAndPrint();
                Assert.IsTrue(consoleOutput.GetOutput().Contains("Scan"));
                Assert.IsTrue(consoleOutput.GetOutput().Contains("Print"));
            }
            Assert.AreEqual(currentConsoleOut, Console.Out);
        }

        // weryfikacja, czy po wywołaniu metody `ScanAndPrint` i wyłączonej kopiarce w napisie NIE pojawia się słowo `Print`
        // ani słowo `Scan`
        // wymagane przekierowanie konsoli do strumienia StringWriter
        [TestMethod]
        public void Copier_ScanAndPrint_DeviceOff()
        {
            var copier = new Copier();
            copier.PowerOff();

            var currentConsoleOut = Console.Out;
            currentConsoleOut.Flush();
            using (var consoleOutput = new ConsoleRedirectionToStringWriter())
            {
                copier.ScanAndPrint();
                Assert.IsFalse(consoleOutput.GetOutput().Contains("Scan"));
                Assert.IsFalse(consoleOutput.GetOutput().Contains("Print"));
            }
            Assert.AreEqual(currentConsoleOut, Console.Out);
        }

        [TestMethod]
        public void Copier_PrintCounter()
        {
            var copier = new Copier();
            copier.PowerOn();

            Documents.IDocument doc1 = new Documents.PDFDocument("aaa.pdf");
            copier.Print(in doc1);
            Documents.IDocument doc2 = new Documents.TextDocument("aaa.txt");
            copier.Print(in doc2);
            Documents.IDocument doc3 = new Documents.ImageDocument("aaa.jpg");
            copier.Print(in doc3);

            copier.PowerOff();
            copier.Print(in doc3);
            copier.Scan(out doc1);
            copier.PowerOn();

            copier.ScanAndPrint();
            copier.ScanAndPrint();

            // 5 wydruków, gdy urządzenie włączone
            Assert.AreEqual(5, copier.Printer.PrintCounter);
        }

        [TestMethod]
        public void Copier_ScanCounter()
        {
            var copier = new Copier();
            copier.PowerOn();

            Documents.IDocument doc1;
            copier.Scan(out doc1);
            Documents.IDocument doc2;
            copier.Scan(out doc2);

            Documents.IDocument doc3 = new Documents.ImageDocument("aaa.jpg");
            copier.Print(in doc3);

            copier.PowerOff();
            copier.Print(in doc3);
            copier.Scan(out doc1);
            copier.PowerOn();

            copier.ScanAndPrint();
            copier.ScanAndPrint();

            // 4 skany, gdy urządzenie włączone
            Assert.AreEqual(4, copier.Scanner.ScanCounter);
        }

        [TestMethod]
        public void Copier_PowerOnCounter()
        {
            var copier = new Copier();
            copier.PowerOn();
            copier.PowerOn();
            copier.PowerOn();

            Documents.IDocument doc1;
            copier.Scan(out doc1);
            Documents.IDocument doc2;
            copier.Scan(out doc2);

            copier.PowerOff();
            copier.PowerOff();
            copier.PowerOff();
            copier.PowerOn();

            Documents.IDocument doc3 = new Documents.ImageDocument("aaa.jpg");
            copier.Print(in doc3);

            copier.PowerOff();
            copier.Print(in doc3);
            copier.Scan(out doc1);
            copier.PowerOn();

            copier.ScanAndPrint();
            copier.ScanAndPrint();

            // 3 włączenia
            Assert.AreEqual(3, copier.Counter);
        }
        //Testy sprawdzające działanie "MultifunctionalDevice"
        [TestMethod]
        public void MultifunctionalDevice_GetState_StateOff()
        {
            var multifunctionalDevice = new MultifunctionalDevice();
            multifunctionalDevice.PowerOff();

            Assert.AreEqual(Devices.IDevice.State.off, multifunctionalDevice.GetState());
        }

        [TestMethod]
        public void MultifunctionalDevice_GetState_StateOn()
        {
            var multifunctionalDevice = new MultifunctionalDevice();
            multifunctionalDevice.PowerOn();

            Assert.AreEqual(Devices.IDevice.State.on, multifunctionalDevice.GetState());
        }


        // weryfikacja, czy po wywołaniu metody `Print` i włączonej kopiarce w napisie pojawia się słowo `Print`
        // wymagane przekierowanie konsoli do strumienia StringWriter
        [TestMethod]
        public void MultifunctionalDevice_Print_DeviceOn()
        {
            var multifunctionalDevice = new MultifunctionalDevice();
            multifunctionalDevice.PowerOn();

            var currentConsoleOut = Console.Out;
            currentConsoleOut.Flush();
            using (var consoleOutput = new ConsoleRedirectionToStringWriter())
            {
                Documents.IDocument doc1 = new Documents.PDFDocument("aaa.pdf");
                multifunctionalDevice.Print(in doc1);
                Assert.IsTrue(consoleOutput.GetOutput().Contains("Print"));
            }

            Assert.AreEqual(currentConsoleOut, Console.Out);
        }

        // weryfikacja, czy po wywołaniu metody `Print` i wyłączonej kopiarce w napisie NIE pojawia się słowo `Print`
        // wymagane przekierowanie konsoli do strumienia StringWriter
        [TestMethod]
        public void MultifunctionalDevice_Print_DeviceOff()
        {
            var multifunctionalDevice = new MultifunctionalDevice();
            multifunctionalDevice.PowerOff();

            var currentConsoleOut = Console.Out;
            currentConsoleOut.Flush();
            using (var consoleOutput = new ConsoleRedirectionToStringWriter())
            {
                Documents.IDocument doc1 = new Documents.PDFDocument("aaa.pdf");
                multifunctionalDevice.Print(in doc1);
                Assert.IsFalse(consoleOutput.GetOutput().Contains("Print"));
            }

            Assert.AreEqual(currentConsoleOut, Console.Out);
        }

        // weryfikacja, czy po wywołaniu metody `Scan` i wyłączonej kopiarce w napisie NIE pojawia się słowo `Scan`
        // wymagane przekierowanie konsoli do strumienia StringWriter
        [TestMethod]
        public void MultifunctionalDevice_Scan_DeviceOff()
        {
            var multifunctionalDevice = new MultifunctionalDevice();
            multifunctionalDevice.PowerOff();

            var currentConsoleOut = Console.Out;
            currentConsoleOut.Flush();
            using (var consoleOutput = new ConsoleRedirectionToStringWriter())
            {
                Documents.IDocument doc1;
                multifunctionalDevice.Scan(out doc1);
                Assert.IsFalse(consoleOutput.GetOutput().Contains("Scan"));
            }

            Assert.AreEqual(currentConsoleOut, Console.Out);
        }

        // weryfikacja, czy po wywołaniu metody `Scan` i włączonej kopiarce w napisie pojawia się słowo `Scan`
        // wymagane przekierowanie konsoli do strumienia StringWriter
        [TestMethod]
        public void MultifunctionalDevice_Scan_DeviceOn()
        {
            var multifunctionalDevice = new MultifunctionalDevice();
            multifunctionalDevice.PowerOn();

            var currentConsoleOut = Console.Out;
            currentConsoleOut.Flush();
            using (var consoleOutput = new ConsoleRedirectionToStringWriter())
            {
                Documents.IDocument doc1;
                multifunctionalDevice.Scan(out doc1);
                Assert.IsTrue(consoleOutput.GetOutput().Contains("Scan"));
            }

            Assert.AreEqual(currentConsoleOut, Console.Out);
        }

        // weryfikacja, czy wywołanie metody `Scan` z parametrem określającym format dokumentu
        // zawiera odpowiednie rozszerzenie (`.jpg`, `.txt`, `.pdf`)
        [TestMethod]
        public void MultifunctionalDevice_Scan_FormatTypeDocument()
        {
            var multifunctionalDevice = new MultifunctionalDevice();
            multifunctionalDevice.PowerOn();

            var currentConsoleOut = Console.Out;
            currentConsoleOut.Flush();
            using (var consoleOutput = new ConsoleRedirectionToStringWriter())
            {
                Documents.IDocument doc1;
                multifunctionalDevice.Scan(out doc1, formatType: Documents.IDocument.FormatType.JPG);
                Assert.IsTrue(consoleOutput.GetOutput().Contains("Scan"));
                Assert.IsTrue(consoleOutput.GetOutput().Contains(".jpg"));

                multifunctionalDevice.Scan(out doc1, formatType: Documents.IDocument.FormatType.TXT);
                Assert.IsTrue(consoleOutput.GetOutput().Contains("Scan"));
                Assert.IsTrue(consoleOutput.GetOutput().Contains(".txt"));

                multifunctionalDevice.Scan(out doc1, formatType: Documents.IDocument.FormatType.PDF);
                Assert.IsTrue(consoleOutput.GetOutput().Contains("Scan"));
                Assert.IsTrue(consoleOutput.GetOutput().Contains(".pdf"));
            }

            Assert.AreEqual(currentConsoleOut, Console.Out);
        }


        // weryfikacja, czy po wywołaniu metody `ScanAndPrint` i wyłączonej kopiarce w napisie pojawiają się słowa `Print`
        // oraz `Scan`
        // wymagane przekierowanie konsoli do strumienia StringWriter
        [TestMethod]
        public void MultifunctionalDevice_ScanAndPrint_DeviceOn()
        {
            var multifunctionalDevice = new MultifunctionalDevice();
            multifunctionalDevice.PowerOn();

            var currentConsoleOut = Console.Out;
            currentConsoleOut.Flush();
            using (var consoleOutput = new ConsoleRedirectionToStringWriter())
            {
                multifunctionalDevice.ScanAndPrint();
                Assert.IsTrue(consoleOutput.GetOutput().Contains("Scan"));
                Assert.IsTrue(consoleOutput.GetOutput().Contains("Print"));
            }

            Assert.AreEqual(currentConsoleOut, Console.Out);
        }

        // weryfikacja, czy po wywołaniu metody `ScanAndPrint` i wyłączonej kopiarce w napisie NIE pojawia się słowo `Print`
        // ani słowo `Scan`
        // wymagane przekierowanie konsoli do strumienia StringWriter
        [TestMethod]
        public void MultifunctionalDevice_ScanAndPrint_DeviceOff()
        {
            var multifunctionalDevice = new MultifunctionalDevice();
            multifunctionalDevice.PowerOff();

            var currentConsoleOut = Console.Out;
            currentConsoleOut.Flush();
            using (var consoleOutput = new ConsoleRedirectionToStringWriter())
            {
                multifunctionalDevice.ScanAndPrint();
                Assert.IsFalse(consoleOutput.GetOutput().Contains("Scan"));
                Assert.IsFalse(consoleOutput.GetOutput().Contains("Print"));
            }

            Assert.AreEqual(currentConsoleOut, Console.Out);
        }

        [TestMethod]
        public void MultifunctionalDevice_PrintCounter()
        {
            var multifunctionalDevice = new MultifunctionalDevice();
            multifunctionalDevice.PowerOn();

            Documents.IDocument doc1 = new Documents.PDFDocument("aaa.pdf");
            multifunctionalDevice.Print(in doc1);
            Documents.IDocument doc2 = new Documents.TextDocument("aaa.txt");
            multifunctionalDevice.Print(in doc2);
            Documents.IDocument doc3 = new Documents.ImageDocument("aaa.jpg");
            multifunctionalDevice.Print(in doc3);

            multifunctionalDevice.PowerOff();
            multifunctionalDevice.Print(in doc3);
            multifunctionalDevice.Scan(out doc1);
            multifunctionalDevice.PowerOn();

            multifunctionalDevice.ScanAndPrint();
            multifunctionalDevice.ScanAndPrint();

            // 5 wydruków, gdy urządzenie włączone
            Assert.AreEqual(5, multifunctionalDevice.Printer.PrintCounter);
        }

        [TestMethod]
        public void MultifunctionalDevice_ScanCounter()
        {
            var multifunctionalDevice = new MultifunctionalDevice();
            multifunctionalDevice.PowerOn();

            Documents.IDocument doc1;
            multifunctionalDevice.Scan(out doc1);
            Documents.IDocument doc2;
            multifunctionalDevice.Scan(out doc2);

            Documents.IDocument doc3 = new Documents.ImageDocument("aaa.jpg");
            multifunctionalDevice.Print(in doc3);

            multifunctionalDevice.PowerOff();
            multifunctionalDevice.Print(in doc3);
            multifunctionalDevice.Scan(out doc1);
            multifunctionalDevice.PowerOn();

            multifunctionalDevice.ScanAndPrint();
            multifunctionalDevice.ScanAndPrint();

            // 4 skany, gdy urządzenie włączone
            Assert.AreEqual(4, multifunctionalDevice.Scanner.ScanCounter);
        }

        [TestMethod]
        public void MultifunctionalDevice_PowerOnCounter()
        {
            var multifunctionalDevice = new MultifunctionalDevice();
            multifunctionalDevice.PowerOn();
            multifunctionalDevice.PowerOn();
            multifunctionalDevice.PowerOn();

            Documents.IDocument doc1;
            multifunctionalDevice.Scan(out doc1);
            Documents.IDocument doc2;
            multifunctionalDevice.Scan(out doc2);

            multifunctionalDevice.PowerOff();
            multifunctionalDevice.PowerOff();
            multifunctionalDevice.PowerOff();
            multifunctionalDevice.PowerOn();

            Documents.IDocument doc3 = new Documents.ImageDocument("aaa.jpg");
            multifunctionalDevice.Print(in doc3);

            multifunctionalDevice.PowerOff();
            multifunctionalDevice.Print(in doc3);
            multifunctionalDevice.Scan(out doc1);
            multifunctionalDevice.PowerOn();

            multifunctionalDevice.ScanAndPrint();
            multifunctionalDevice.ScanAndPrint();

            // 3 włączenia
            Assert.AreEqual(3, multifunctionalDevice.Counter);
        }

        [TestMethod]
        public void MultifunctionalDevice_ReceiveCounter()
        {
            var multifunctionalDevice = new MultifunctionalDevice();
            multifunctionalDevice.PowerOn();
        }

        [TestMethod]
        public void MultifunctionalDevice_SendCounter()
        {
            var multifunctionalDevice = new MultifunctionalDevice();
            multifunctionalDevice.PowerOn();
        }
        
        // weryfikacja, czy po wywołaniu metody `Send` i włączonej kopiarce w napisie pojawia się słowo `Send`
        // wymagane przekierowanie konsoli do strumienia StringWriter
        [TestMethod]
        public void MultifunctionalDevice_Send_DeviceOn()
        {
            var multifunctionalDevice = new MultifunctionalDevice();
            multifunctionalDevice.PowerOn();

            var currentConsoleOut = Console.Out;
            currentConsoleOut.Flush();
            using (var consoleOutput = new ConsoleRedirectionToStringWriter())
            {
                Documents.IDocument doc1 = new Documents.TextDocument("TestDocument");
                multifunctionalDevice.Send(doc1);
            }

            Assert.AreEqual(currentConsoleOut, Console.Out);
        }

        // weryfikacja, czy po wywołaniu metody `Send` i włączonej kopiarce w napisie pojawia się słowo `Send`
        // wymagane przekierowanie konsoli do strumienia StringWriter
        [TestMethod]
        public void MultifunctionalDevice_Send_DeviceOff()
        {
            var multifunctionalDevice = new MultifunctionalDevice();
            multifunctionalDevice.PowerOff();

            var currentConsoleOut = Console.Out;
            currentConsoleOut.Flush();
            using (var consoleOutput = new ConsoleRedirectionToStringWriter())
            {
                Documents.IDocument doc1 = new Documents.TextDocument("TestDocument");
                multifunctionalDevice.Send(doc1);
                Assert.IsFalse(consoleOutput.GetOutput().Contains("Send"));
            }

            Assert.AreEqual(currentConsoleOut, Console.Out);
        }

        // weryfikacja, czy po wywołaniu metody `Receive` i włączonej kopiarce w napisie pojawia się słowo `Receive`
        // wymagane przekierowanie konsoli do strumienia StringWriter
        [TestMethod]
        public void MultifunctionalDevice_Receive_DeviceOn()
        {
            var multifunctionalDevice = new MultifunctionalDevice();
            multifunctionalDevice.PowerOn();

            var currentConsoleOut = Console.Out;
            currentConsoleOut.Flush();
            using (var consoleOutput = new ConsoleRedirectionToStringWriter())
            {
                Documents.IDocument doc1 = new Documents.TextDocument("TestDocument");
                multifunctionalDevice.Receive(doc1);
                Assert.IsTrue(consoleOutput.GetOutput().Contains("Receive"));
            }

            Assert.AreEqual(currentConsoleOut, Console.Out);
        }
        
        // weryfikacja, czy po wywołaniu metody `Receive` i wyłączonej kopiarce w napisie pojawia się słowo `Receive`
        // wymagane przekierowanie konsoli do strumienia StringWriter
        [TestMethod]
        public void MultifunctionalDevice_Receive_DeviceOff()
        {
            var multifunctionalDevice = new MultifunctionalDevice();
            multifunctionalDevice.PowerOff();

            var currentConsoleOut = Console.Out;
            currentConsoleOut.Flush();
            using (var consoleOutput = new ConsoleRedirectionToStringWriter())
            {
                Documents.IDocument doc1 = new Documents.TextDocument("TestDocument");
                multifunctionalDevice.Receive(doc1);
                Assert.IsFalse(consoleOutput.GetOutput().Contains("Receive"));
            }

            Assert.AreEqual(currentConsoleOut, Console.Out);
        }
    }
}