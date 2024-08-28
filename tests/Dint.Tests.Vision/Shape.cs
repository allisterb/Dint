namespace Dint.Tests.Vision;

using OpenCvSharp;

public class Shape
{
    [Fact]
    public void CanSlice()
    {
        var a = new Point[4] { new Point(0, 0), new Point(1, 0), new Point(2, 0), new Point(3, 0) };
        var lm = a[0..2];
        //var rm = a[^0..^1];
        Assert.Equal(2, lm.Length);
    }
}
