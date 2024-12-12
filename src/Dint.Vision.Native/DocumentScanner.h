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
	DocumentScanner(void* input, int size);
	cv::Mat GetImage(void* buffer, int size);
	virtual cv::Mat PreProcess(cv::Mat src) = 0;
};

