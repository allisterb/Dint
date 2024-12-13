#pragma once

#include "pch.h"
#include "scanners/MryndzDocumentScanner.h"

#define	API extern "C" __declspec(dllexport) 

API void* Get(void* buf, int size)
{
	return new MryndzDocumentScanner(buf, size);
}