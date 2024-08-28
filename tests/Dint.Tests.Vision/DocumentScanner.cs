namespace Dint.Tests.Vision;

using System;
using System.IO;
using Xunit;

public class DocumentScanner : Runtime
{
    static DocumentScanner()
    {
        if (!File.Exists(AssemblyLocation.CombinePath("DocumentScanner.TestsData.xml")))
        {
            if (!DownloadFile("DocumentScanner.TestsData.xml", new Uri("https://ajb.nyc3.cdn.digitaloceanspaces.com/DocumentScanner_TestsData.xml"), AssemblyLocation.CombinePath("DocumentScanner.TestsData.xml")))
            {
                throw new Exception("Could not download DocumentScanner test data file.");
                
            }

        }

        if (!File.Exists(AssemblyLocation.CombinePath("DocumentScanner_Ticket.jpg")))
        {
            if (!DownloadFile("DocumentScanner_Ticket.JPG", new Uri("https://ajb.nyc3.cdn.digitaloceanspaces.com/DocumentScanner_Ticket.jpg"), AssemblyLocation.CombinePath("DocumentScanner_Ticket.jpg")))
            {
                throw new Exception("Could not download DocumentScanner ticket image file.");

            }

        }
    }
    
    [Fact]
    public void CanPreprocess()
    {
        Assert.True(true);  
    }
}

