using Microsoft.AspNetCore.Mvc;
using WEBAPItest.Models;
using WEBAPItest.Services;


namespace WEBAPItest.Controllers;

[Route("api/[controller]")]
[ApiController]
public class HomeController : ControllerBase
{
    private readonly IGenerateTestService _generate;

    public HomeController(IGenerateTestService generate)
    {
        _generate = generate;
    }


    [HttpPost]
    [Route("rdlc/v1")]
    public IActionResult GenaratePDF(string name, string lastname)
    {
        var result = new List<Example>
        {
            new Example {Name = name, LastName = lastname },
            new Example {Name = name, LastName = lastname }

        }; 
        var mergeContent = _generate.GeneratePDF(result);

        return File(mergeContent, "application/pdf", "TESTFILE.pdf");

    }




































    //private byte[] generatepdf(Example examples)
    //{
    //    _localreport.AddDataSource("DataSet1", new List<Example> { examples });
    //    var pdfContent = _localreport.Execute(RenderType.Pdf);
    //    return pdfContent.MainStream;
    //}

    //private static byte[] merge(List<byte[]> bytes)
    //{
    //    MemoryStream outputStream = new MemoryStream();
    //    PdfDocument mergepdf = new PdfDocument(new PdfWriter(outputStream));
    //    foreach (var item in bytes)
    //    {
    //        PdfDocument merge = new PdfDocument(new PdfReader(new MemoryStream(item)));
    //        merge.CopyPagesTo(1, merge.GetNumberOfPages(), mergepdf);
    //    }
    //    mergepdf.Close();
    //    return outputStream.ToArray();
    //}
}
