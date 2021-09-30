// using UglyToad.PdfPig;
// using UglyToad.PdfPig.Content;

using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Canvas.Parser;
using iText.Kernel.Pdf.Canvas.Parser.Listener;

using System.Text;


// Get pdf file path from argument
string pdfFilePath = args[0];
Console.WriteLine(pdfFilePath);

/*
// Open pdf file
using (PdfDocument doc = PdfDocument.Open(pdfFilePath)) {
    Console.WriteLine($"Page Count: {doc.NumberOfPages}");

    Page p = doc.GetPage(1);

    string text = p.Text;

    Console.WriteLine(text);
}

Console.WriteLine(string.Empty);
Console.WriteLine(string.Empty);
*/

StringBuilder sb = new ();
using (PdfDocument doc = new PdfDocument(new PdfReader(pdfFilePath))) {
    var pageNumberCount = doc.GetNumberOfPages();
    Console.WriteLine($"Page Count: {pageNumberCount}");

    /*
    SimpleTextExtractionStrategy strat = new();
    PdfCanvasProcessor parser = new(strat);
    parser.ProcessPageContent(doc.GetFirstPage());
    sb.Append(strat.GetResultantText());
    */

    int objCount = doc.GetNumberOfPdfObjects();
    Console.WriteLine($"Object Count: {objCount}");
}

/*
string pageText = sb.ToString();
Console.WriteLine(pageText);

string[] lines = pageText.Split('\n');
Console.WriteLine($"Line Count: {lines.Count()}");

string line = lines[4];
Console.WriteLine(line);
string[] items = line.Split('\t');
Console.WriteLine($"Item Count: {items.Count()}");
*/

// Read pdf file text


