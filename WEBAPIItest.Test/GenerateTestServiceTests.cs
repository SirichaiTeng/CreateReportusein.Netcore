using Microsoft.AspNetCore.Hosting;
using Microsoft.Reporting.NETCore;
using Moq;
using WEBAPItest.Models;
using WEBAPItest.Services;

namespace WEBAPIItest.Test;

public class GenerateTestServiceTests
{

    [Fact]
    public void GeneratePDF_ReturnsByteArray()
    {
        // Create a real LocalReport for testing
        var localReport = new LocalReport();
        localReport.ReportPath = Path.Combine(
            Directory.GetCurrentDirectory(),
            "Reports",
            "Report1.rdlc"
        );

        // Mock WebHostEnvironment
        var mockEnvironment = new Mock<IWebHostEnvironment>();
        mockEnvironment
            .Setup(e => e.ContentRootPath)
            .Returns(Directory.GetCurrentDirectory());

        // Create service with real LocalReport
        var service = new GenerateTestService(
            mockEnvironment.Object,
            localReport
        );

        // Prepare test data
        var testExamples = new List<Example>
    {
        new Example { Name = "John", LastName = "Doe" },
        new Example { Name = "Jane", LastName = "Smith" }
    };

        // Act
        byte[] result = service.GeneratePDF(testExamples);

        // Assert
        Assert.NotNull(result);
        Assert.True(result.Length > 0);
    }

}
