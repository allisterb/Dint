namespace Dint.Vision;

using OpenCvSharp;
using static OpenCvSharp.Cv2;
public static class CvExtensions
{
    public static bool IsEqualTo(this Mat m, Mat n)
    {
        if (m.Dims != n.Dims || m.Size() != n.Size() || m.ElemSize() != n.ElemSize())
        {
            return false;
        }
        else
        {
            using Mat diff = new();
            Compare(m, n, diff, CmpType.NE);
            return CountNonZero(diff.Reshape(1)) == 0;
        }
    }
}

