#pragma once
#include "pch.h"
#include "api.h"

API void* __Test_Buffer(void* im, int size)
{
	auto m = ReadImage(im, size);
	Mat* o = new Mat();
	cv::resize(m, *o, cv::Size(317, 455));
	return ToPtr(o);
}