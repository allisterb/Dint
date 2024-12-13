#pragma once
#include "../DocumentScanner.h"
class MryndzDocumentScanner : DocumentScanner
{
public:
    MryndzDocumentScanner(const void* buf, int size);
    cv::Mat PreProcess(const Mat& src) override;
};

