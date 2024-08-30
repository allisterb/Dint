namespace Dint.Tests.Vision;

using System;
using System.IO;
using Xunit;

using OpenCvSharp;
using static OpenCvSharp.Cv2;

using Dint.Vision;

public class DocumentScannerTests : Runtime
{
    static readonly string ticketimagefile = AssemblyLocation.CombinePath("DocumentScanner_Ticket.jpg");
    static readonly string testsdatafile = AssemblyLocation.CombinePath("DocumentScanner.TestsData.xml");
    static readonly Mat ticket;
    static readonly DocumentScanner scanner = new DocumentScanner();

    static DocumentScannerTests()
    {
        if (!File.Exists(testsdatafile))
        {
            if (!DownloadFile("DocumentScanner.TestsData.xml", new Uri("https://ajb.nyc3.cdn.digitaloceanspaces.com/DocumentScanner_TestsData.xml"), testsdatafile))
            {
                throw new Exception("Could not download DocumentScanner test data file.");
            }

        }

        if (!File.Exists(ticketimagefile))
        {
            if (!DownloadFile("DocumentScanner_Ticket.JPG", new Uri("https://ajb.nyc3.cdn.digitaloceanspaces.com/DocumentScanner_Ticket.jpg"), ticketimagefile))
            {
                throw new Exception("Could not download DocumentScanner ticket image file.");
            }
        }

        ticket = ImRead(ticketimagefile);
    }

    [Fact]
    public void CanPreProcess()
    {
        using var fs = new FileStorage(testsdatafile, FileStorage.Modes.Read);
        using Mat dst = new();
        scanner.PreProcess(ticket, dst);
        var x = (Mat)fs["preProcess"]!;
        Assert.True(x.IsEqualTo(dst));
    }

    [Fact]
    public void CanResize()
    {
        using var fs = new FileStorage(testsdatafile, FileStorage.Modes.Read);
        using Mat dst = new();
        scanner.ResizeToHeight(ticket, dst, 500);
        var x = (Mat)fs["resizeToHeight"]!;
        Assert.True(x.IsEqualTo(dst));
    }
}

