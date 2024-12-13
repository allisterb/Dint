#pragma once

#include "pch.h"

using namespace cv;
using namespace std;

class DocumentScanner
{
private:
	cv::Mat m_input;
public:
	DocumentScanner(cv::Mat input);
	DocumentScanner(const void* input, int size);
	cv::Mat GetImage(const void* buffer, int size);
	virtual cv::Mat PreProcess(const cv::Mat& src) = 0;
};

