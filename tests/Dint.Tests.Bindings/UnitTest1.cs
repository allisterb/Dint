namespace Dint.Tests.Bindings;

using OpenCvSharp;
using static OpenCvSharp.Cv2;

public class UnitTest1
{
    [Fact]
    public void Test1()
    {
        Mat mat = new Mat(10, 12, MatType.CV_32FC1);
        nint ptr = dint.Buffer_Test(mat.CvPtr);
        Mat r = Mat.FromNativePointer(ptr);
        Assert.Equal(100, r.Size().Width);

    }
}
