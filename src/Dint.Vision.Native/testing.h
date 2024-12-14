#pragma once
#include "pch.h"
#include "api.h"

API void* __Buffer_Test(void* im)
{
	auto m = FromPtr(im);
	Mat* o = new Mat();
	cv::resize(*m, *o, cv::Size(317, 455));
	return ToPtr(o);
}