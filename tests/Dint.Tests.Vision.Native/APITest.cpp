#include "pch.h"
#include "api.h"

namespace Dint::Tests::Vision::Native
{
TEST(APITest, CanCastPointer) {
	auto m = new cv::Mat(100, 100, 1);
	auto v = ToPtr(m);
	auto y = FromPtr(v);
	EXPECT_EQ(m->rows, y->rows);
	EXPECT_TRUE(true);
}
}