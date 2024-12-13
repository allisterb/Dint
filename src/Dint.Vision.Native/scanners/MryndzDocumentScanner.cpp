#include "pch.h"
#include "MryndzDocumentScanner.h"

using namespace cv;
using namespace std;

using namespace cv;
using namespace std;


static bool compareContourAreas(std::vector<cv::Point> contour1, std::vector<cv::Point> contour2)
{
	double i = fabs(contourArea(cv::Mat(contour1)));
	double j = fabs(contourArea(cv::Mat(contour2)));
	return (i > j);
}

static bool compareXCords(Point p1, Point p2)
{
	return (p1.x < p2.x);
}

static bool compareYCords(Point p1, Point p2)
{
	return (p1.y < p2.y);
}

static bool compareDistance(pair<Point, Point> p1, pair<Point, Point> p2)
{
	return (norm(p1.first - p1.second) < norm(p2.first - p2.second));
}

static double _distance(Point p1, Point p2)
{
	return sqrt(((p1.x - p2.x) * (p1.x - p2.x)) +
		((p1.y - p2.y) * (p1.y - p2.y)));
}

void resizeToHeight(Mat src, Mat& dst, int height)
{
	Size s = Size(src.cols * (height / double(src.rows)), height);
	resize(src, dst, s, 0.0, 0.0, INTER_AREA);
}

void orderPoints(vector<Point> inpts, vector<Point>& ordered)
{
	sort(inpts.begin(), inpts.end(), compareXCords);
	vector<Point> lm(inpts.begin(), inpts.begin() + 2);
	vector<Point> rm(inpts.end() - 2, inpts.end());

	sort(lm.begin(), lm.end(), compareYCords);
	Point tl(lm[0]);
	Point bl(lm[1]);
	vector<pair<Point, Point>> tmp;
	for (size_t i = 0; i < rm.size(); i++)
	{
		tmp.push_back(make_pair(tl, rm[i]));
	}

	sort(tmp.begin(), tmp.end(), compareDistance);
	Point tr(tmp[0].second);
	Point br(tmp[1].second);

	ordered.push_back(tl);
	ordered.push_back(tr);
	ordered.push_back(br);
	ordered.push_back(bl);
}

void fourPointTransform(Mat src, Mat& dst, vector<Point> pts)
{
	vector<Point> ordered_pts;
	orderPoints(pts, ordered_pts);

	double wa = _distance(ordered_pts[2], ordered_pts[3]);
	double wb = _distance(ordered_pts[1], ordered_pts[0]);
	double mw = max(wa, wb);

	double ha = _distance(ordered_pts[1], ordered_pts[2]);
	double hb = _distance(ordered_pts[0], ordered_pts[3]);
	double mh = max(ha, hb);

	Point2f src_[] =
	{
		Point2f(ordered_pts[0].x, ordered_pts[0].y),
		Point2f(ordered_pts[1].x, ordered_pts[1].y),
		Point2f(ordered_pts[2].x, ordered_pts[2].y),
		Point2f(ordered_pts[3].x, ordered_pts[3].y),
	};
	Point2f dst_[] =
	{
		Point2f(0, 0),
		Point2f(mw - 1, 0),
		Point2f(mw - 1, mh - 1),
		Point2f(0, mh - 1) };
	Mat m = getPerspectiveTransform(src_, dst_);
	warpPerspective(src, dst, m, Size(mw, mh));
}

void preProcess(Mat src, Mat& dst)
{
	cv::Mat imageGrayed;
	cv::Mat imageOpen, imageClosed, imageBlurred;

	cvtColor(src, imageGrayed, COLOR_BGR2GRAY);

	cv::Mat structuringElmt = cv::getStructuringElement(cv::MORPH_ELLIPSE, cv::Size(4, 4));
	morphologyEx(imageGrayed, imageOpen, cv::MORPH_OPEN, structuringElmt);
	morphologyEx(imageOpen, imageClosed, cv::MORPH_CLOSE, structuringElmt);

	GaussianBlur(imageClosed, imageBlurred, Size(7, 7), 0);
	Canny(imageBlurred, dst, 75, 100);
}

MryndzDocumentScanner::MryndzDocumentScanner(const void* buf, int size) : DocumentScanner(buf, size) {}

cv::Mat MryndzDocumentScanner::PreProcess(const Mat& src)
{
	cv::Mat dst;
	cv::Mat imageGrayed;
	cv::Mat imageOpen, imageClosed, imageBlurred;

	cvtColor(src, imageGrayed, COLOR_BGR2GRAY);

	cv::Mat structuringElmt = cv::getStructuringElement(cv::MORPH_ELLIPSE, cv::Size(4, 4));
	morphologyEx(imageGrayed, imageOpen, cv::MORPH_OPEN, structuringElmt);
	morphologyEx(imageOpen, imageClosed, cv::MORPH_CLOSE, structuringElmt);

	GaussianBlur(imageClosed, imageBlurred, Size(7, 7), 0);
	Canny(imageBlurred, dst, 75, 100);
	return dst;
}