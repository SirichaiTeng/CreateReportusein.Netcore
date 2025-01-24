using iText.Kernel.Pdf;
using Microsoft.Reporting.NETCore;
using WEBAPItest.Models;

namespace WEBAPItest.Services;

public interface IGenerateTestService
{
    byte[] GeneratePDF(List<Example> examples);
}

public class GenerateTestService : IGenerateTestService
{
    private readonly IWebHostEnvironment _environment;
    private readonly LocalReport _localReport;
    public GenerateTestService(IWebHostEnvironment environment, LocalReport localReport)
    {
        _environment = environment;
        _localReport = localReport;
    }
    public byte[] GeneratePDF(List<Example> examples)
    {
        List<byte[]> bytes = new List<byte[]>();

        foreach (var item in examples)
        {
            bytes.Add(generatepdf(item));
        }
        return merge(bytes);


    }
    private byte[] generatepdf(Example examples)
    {
        _localReport.ReportPath = Path.Combine(_environment.ContentRootPath, "Reports", "Report1.rdlc"); _localReport.DataSources.Add(new ReportDataSource("DataSet1", new List<Example> { examples }));
        _localReport.DataSources.Add(new ReportDataSource("DataSet1", new List<Example> { examples }));
         return _localReport.Render("PDF");
    }

    private byte[] merge(List<byte[]> bytes)
    {
        MemoryStream outputStream = new MemoryStream();
        PdfDocument mergepdf = new PdfDocument(new PdfWriter(outputStream));
        foreach (var item in bytes)
        {
            PdfDocument merge = new PdfDocument(new PdfReader(new MemoryStream(item)));
            merge.CopyPagesTo(1, merge.GetNumberOfPages(), mergepdf);
        }
        mergepdf.Close();
        return outputStream.ToArray();
    }
}



