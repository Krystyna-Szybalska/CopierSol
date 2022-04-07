using static Zadanie3.Documents;
using static Zadanie3.Devices;

namespace Zadanie3 {
    class Program {
        static void Main(string[] args) {
            var xerox = new Copier();
            xerox.PowerOn();
            IDocument doc1 = new PDFDocument("aaa.pdf");
            xerox.Print(in doc1);

            IDocument doc2;
            xerox.Scan(out doc2);

            xerox.ScanAndPrint();
            System.Console.WriteLine(xerox.Counter);
            System.Console.WriteLine(xerox.Printer.PrintCounter);
            System.Console.WriteLine(xerox.Scanner.ScanCounter);
            System.Console.WriteLine(xerox.Printer.Counter);
            System.Console.WriteLine(xerox.Scanner.Counter);
        }
    }
}