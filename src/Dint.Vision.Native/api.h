#pragma once

#include "pch.h"

#define	API extern "C" __declspec(dllexport) 

using namespace cv;
using namespace std;

static void* ToPtr(cv::Mat* mat) {mat->addref(); return mat; }

static Mat* FromPtr(void* ptr) { return reinterpret_cast<Mat*>(ptr); }

static Mat ReadImage(void* buf, int size)
{
	auto im = cv::InputArray(std::vector<uchar>(static_cast<const uchar*>(buf), static_cast<const uchar*>(buf) + size));
	return cv::imdecode(im, cv::ImreadModes::IMREAD_UNCHANGED);
}

