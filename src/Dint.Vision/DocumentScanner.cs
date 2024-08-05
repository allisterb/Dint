namespace Dint.Vision;

using System;
using System.Collections.Generic;

using OpenCvSharp;
using static OpenCvSharp.Cv2;

public class DocumentScanner
{
    public static void PreProcess(Mat src, ref Mat dst)
    {
        using Mat imageGrayed = new Mat();
        using Mat imageOpen = new Mat();
        using Mat imageClosed = new Mat();
        using Mat imageBlurred = new Mat();

        CvtColor(src, imageGrayed, ColorConversionCodes.BGR2GRAY);

        Mat structuringElmt = GetStructuringElement(MorphShapes.Ellipse, new Size(4, 4));
        MorphologyEx(imageGrayed, imageOpen, MorphTypes.Open, structuringElmt);
        MorphologyEx(imageOpen, imageClosed, MorphTypes.Close, structuringElmt);

        GaussianBlur(imageClosed, imageBlurred, new Size(7, 7), 0);
        Canny(imageBlurred, dst, 75, 100);
    }
}

