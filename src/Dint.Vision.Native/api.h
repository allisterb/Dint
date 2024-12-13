#pragma once

#include "pch.h"
#include "scanners/MryndzDocumentScanner.h"

#define	API extern "C" __declspec(dllexport) 

static void* ToPtr(cv::Mat* mat)
{
	mat->addref();
	return mat;
}

static Mat* FromPtr(void* ptr)
{
	return reinterpret_cast<Mat*>(ptr);
}

API void* Get(void* buf, int size)
{
	//auto ds =  new MryndzDocumentScanner(buf, size);
	//return static_cast<void*>(ds);
	return nullptr;
}