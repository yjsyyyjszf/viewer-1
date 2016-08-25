

#ifdef BILINEARINTERPOLATION_EXPORTS
#define BILINEARINTERPOLATION_API __declspec(dllexport)
#else
#define BILINEARINTERPOLATION_API __declspec(dllimport)
#endif

struct LUTDATA
{
	int *LutData;
	int FirstMappedPixelValue;
	int Length;
};

extern "C"
{
	BILINEARINTERPOLATION_API BOOL InterpolateBilinear
	(
            BYTE* pSrcPixelData,

			unsigned int srcWidth,
            unsigned int srcHeight,
            unsigned int srcBytesPerPixel,
			unsigned int srcBitsStored,

			BOOL isSigned,
			BOOL isRGB,
			BOOL isPlanar,

			float srcRegionRectLeft,
            float srcRegionRectTop,
            float srcRegionRectRight,
            float srcRegionRectBottom,
			
            BYTE* pDstPixelData,
            unsigned int dstWidth,
            unsigned int dstBytesPerPixel,

			int dstRegionRectLeft,
            int dstRegionRectTop,
            int dstRegionRectRight,
            int dstRegionRectBottom,

			BOOL swapXY,
			LUTDATA* pLutData
	);
}
