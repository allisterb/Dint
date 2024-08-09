namespace Dint.Vision;

using System;
using System.Collections.Generic;
using OpenCvSharp;
using static OpenCvSharp.Cv2;

public class DocumentScanner
{
    int compareXCords(Point p1, Point p2) => p1.X.CompareTo(p2.X);
    

    int compareYCords(Point p1, Point p2) => p1.Y.CompareTo(p2.Y);

    int compareContourAreas(Point[] contour1, Point[] contour2) => Math.Abs(ContourArea(contour1)).CompareTo(Math.Abs(ContourArea(contour2))); 
        
    public void OrderPoints(Point[] inpts, ref Point[] ordered)
    {
        Array.Sort(inpts, compareXCords);
        Point[] lm = inpts[0..2];
        Point[] rm = inpts[^2..^0];
        Array.Sort(lm, compareYCords);
    }
    

    public void FourPointTransform(Mat src, ref Mat dst, Point[] pts)
    {
        //Orde
    }


    public void PreProcess(Mat src, ref Mat dst)
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

