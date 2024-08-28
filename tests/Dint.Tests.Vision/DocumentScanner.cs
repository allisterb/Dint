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
    }
    
    [Fact]
    public void CanPreprocess()
    {
        Assert.True(true);
        using var fs = new FileStorage(testsdatafile, FileStorage.Modes.Read);
        using var src = ImRead(ticketimagefile);
        var dst = new Mat();
        var ds = new DocumentScanner();
        ds.PreProcess(src, ref dst);
        var x = (Mat) fs["preProcess"]!;
        Assert.True(x.IsEqualTo(dst));

    }
}

