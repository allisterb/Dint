namespace Dint.Vision;

using System;
using static System.Math;

using OpenCvSharp;
using static OpenCvSharp.Cv2;

public class DocumentScanner
{
    
    int CompareXCords(Point p1, Point p2) => p1.X.CompareTo(p2.X);
    
    int CompareYCords(Point p1, Point p2) => p1.Y.CompareTo(p2.Y);

    int CompareContourAreas(Point[] contour1, Point[] contour2) => Abs(ContourArea(contour1)).CompareTo(Abs(ContourArea(contour2)));

    int CompareDistance((Point, Point) p1, (Point, Point) p2) => p1.Item1.DistanceTo(p1.Item2).CompareTo((p2.Item1.DistanceTo(p2.Item2)));
    
    internal void ResizeToHeight(Mat src, Mat dst, int height)
    {
        //var m = Mat.FromNativePointer
        Size s = new Size(src.Cols * (height / (double) src.Rows), height);
        Resize(src, dst, s, interpolation: InterpolationFlags.Area);
    }

    public void OrderPoints(Point[] inpts, out Point[] ordered)
    {
        if (inpts.Length != 4) throw new ArgumentException("The number of input points must be 4.");

        Array.Sort(inpts, CompareXCords);
        Point[] lm = [inpts[0], inpts[1]];
        Point[] rm = [inpts[3], inpts[2]];
        Array.Sort(lm, CompareYCords);
        Point tl = lm[0];
        Point bl = lm[1];

        (Point, Point)[] tmp = [(tl, rm[0]), (tl, rm[1])];

        Array.Sort(tmp, CompareDistance);
        Point tr = tmp[0].Item2;
        Point br = tmp[1].Item2;
        ordered = [tl, tr, br, bl];
    }

    public void FourPointTransform(Mat src, Mat dst, Point[] pts)
    {
        OrderPoints(pts, out var ordered_pts);
    }


    public void PreProcess(Mat src, Mat dst)
    {
        using Mat imageGrayed = new();
        using Mat imageOpen = new();
        using Mat imageClosed = new();
        using Mat imageBlurred = new();

        CvtColor(src, imageGrayed, ColorConversionCodes.BGR2GRAY);

        Mat structuringElmt = GetStructuringElement(MorphShapes.Ellipse, new Size(4, 4));
        MorphologyEx(imageGrayed, imageOpen, MorphTypes.Open, structuringElmt);
        MorphologyEx(imageOpen, imageClosed, MorphTypes.Close, structuringElmt);

        GaussianBlur(imageClosed, imageBlurred, new Size(7, 7), 0);
        Canny(imageBlurred, dst, 75, 100);
    }
}

