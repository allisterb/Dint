#include "pch.h"
#include "DocumentScanner.h"

DocumentScanner::DocumentScanner(cv::Mat input) : m_input(input) {}
DocumentScanner::DocumentScanner(const void* input, int size) : DocumentScanner(GetImage(input, size)) {}

cv::Mat DocumentScanner::GetImage(const void* buf, int size)
{
	auto im = cv::InputArray(std::vector<uchar>(static_cast<const uchar*>(buf), static_cast<const uchar*>(buf) + size));
	return cv::imdecode(im, cv::ImreadModes::IMREAD_UNCHANGED);
}
