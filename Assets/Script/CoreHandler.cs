// CREATE BY auto_interface.py!
// 2017-12-19 16:17:10

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//using CoreWrapper;
using System.Runtime;
using System.Runtime.InteropServices;

public partial class CoreHandler
{
#if UNITY_IPHONE && !UNITY_EDITOR
    const string CoreDLL = "__Internal";
#else
    const string CoreDLL = "CoreWrapper";
#endif
    
    public static void Init()
    {
        RegisterRelease(UnityRepresent.Release);
        RegisterCreate(UnityRepresent.Create);
        RegisterReset(UnityRepresent.Reset);
        RegisterGet3DDevice(UnityRepresent.Get3DDevice);
        RegisterCreateAFont(UnityRepresent.CreateAFont);
        RegisterOutputText(UnityRepresent.OutputText);
        //RegisterOutputRichText(UnityRepresent.OutputRichText);
        //RegisterLocateRichText(UnityRepresent.LocateRichText);
        RegisterReleaseAFont(UnityRepresent.ReleaseAFont);
        RegisterCreateImage(UnityRepresent.CreateImage);
        RegisterFreeImage(UnityRepresent.FreeImage);
        RegisterFreeAllImage(UnityRepresent.FreeAllImage);
        //RegisterGetBitmapDataBuffer(UnityRepresent.GetBitmapDataBuffer);
        //RegisterReleaseBitmapDataBuffer(UnityRepresent.ReleaseBitmapDataBuffer);
        RegisterGetImageParam(UnityRepresent.GetImageParam);
        //RegisterGetImageFrameParam(UnityRepresent.GetImageFrameParam);
        //RegisterGetImagePixelAlpha(UnityRepresent.GetImagePixelAlpha);
        //RegisterConverSpr(UnityRepresent.ConverSpr);
        //RegisterSetImageStoreBalanceParam(UnityRepresent.SetImageStoreBalanceParam);
        //RegisterSetClipRect(UnityRepresent.SetClipRect);
        RegisterDrawPrimitives(UnityRepresent.DrawPrimitives);
        //RegisterDrawPrimitivesOnImage(UnityRepresent.DrawPrimitivesOnImage);
        RegisterClearImageData(UnityRepresent.ClearImageData);
        //RegisterImageNeedReDraw(UnityRepresent.ImageNeedReDraw);
        RegisterLookAt(UnityRepresent.LookAt);
        //RegisterLookAtEx(UnityRepresent.LookAtEx);
        //RegisterCopyDeviceImageToImage(UnityRepresent.CopyDeviceImageToImage);
        RegisterRepresentBegin(UnityRepresent.RepresentBegin);
        RegisterRepresentEnd(UnityRepresent.RepresentEnd);
        //RegisterAddLight(UnityRepresent.AddLight);
        //RegisterViewPortCoordToSpaceCoord(UnityRepresent.ViewPortCoordToSpaceCoord);
        //RegisterAdviseRepresent(UnityRepresent.AdviseRepresent);
        //RegisterUnAdviseRepresent(UnityRepresent.UnAdviseRepresent);
        //RegisterSaveScreenToFile(UnityRepresent.SaveScreenToFile);
        RegisterCreateRepresentObject(UnityRepresent.CreateRepresentObject);
        RegisterGetRepresentParam(UnityRepresent.GetRepresentParam);
        RegisterSetRepresentParam(UnityRepresent.SetRepresentParam);
        //RegisterCreate3DEffectObject(UnityRepresent.Create3DEffectObject);
        //RegisterCreate3DEffectObjectEx(UnityRepresent.Create3DEffectObjectEx);
        //RegisterGetJpgImage(UnityRepresent.GetJpgImage);
        //RegisterReleaseImage(UnityRepresent.ReleaseImage);
        RegisterReleaseImageA(UnityRepresent.ReleaseImageA);
        RegisterPreLoad(UnityRepresent.PreLoad);
        RegisterCreateImageEffect(UnityRepresent.CreateImageEffect);
        //RegisterBeginImageEffect(UnityRepresent.BeginImageEffect);
        //RegisterEndImageEffect(UnityRepresent.EndImageEffect);
        //RegisterReleaseImageEffect(UnityRepresent.ReleaseImageEffect);
        //RegisterSetImageEffectParam(UnityRepresent.SetImageEffectParam);
        //RegisterUnsetImageEffectParam(UnityRepresent.UnsetImageEffectParam);
        //RegisterReloadResources(UnityRepresent.ReloadResources);
        //RegisterClearDepthBuffer(UnityRepresent.ClearDepthBuffer);
        //RegisterCreateQuake(UnityRepresent.CreateQuake);
        //RegisterStartQuake(UnityRepresent.StartQuake);
        //RegisterStopQuake(UnityRepresent.StopQuake);
        //RegisterGetCurrentQuake(UnityRepresent.GetCurrentQuake);
        //RegisterExchangeMedia(UnityRepresent.ExchangeMedia);
	}

    [DllImport("CoreWrapper.dll", EntryPoint = "RegisterRelease", ExactSpelling = false, CallingConvention = CallingConvention.StdCall)]
	private static extern void RegisterRelease(FnRelease callback); 
	delegate void FnRelease();

    [DllImport("CoreWrapper.dll", EntryPoint = "RegisterCreate", ExactSpelling = false, CallingConvention = CallingConvention.StdCall)]
	private static extern void RegisterCreate(FnCreate callback); 
	delegate bool FnCreate(
		int  hWnd, 
		int  nWidth, 
		int  nHeight, 
		bool  bFullScreen 
	);


    [DllImport("CoreWrapper.dll", EntryPoint = "RegisterReset", ExactSpelling = false, CallingConvention = CallingConvention.StdCall)]
	private static extern void RegisterReset(FnReset callback); 
	delegate bool FnReset(
		int  nWidth, 
		int  nHeight, 
		bool  bFullScreen, 
		bool  bNotAdjustStyle  
	);


    [DllImport("CoreWrapper.dll", CharSet = CharSet.Ansi, EntryPoint = "RegisterGet3DDevice", ExactSpelling = false, CallingConvention = CallingConvention.StdCall)]
    private static extern void RegisterGet3DDevice(FnGet3DDevice callback);
    delegate IntPtr FnGet3DDevice();


    [DllImport("CoreWrapper.dll", CharSet = CharSet.Ansi, EntryPoint = "RegisterCreateAFont", ExactSpelling = false, CallingConvention = CallingConvention.StdCall)]
    private static extern void RegisterCreateAFont(FnCreateAFont callback);
    delegate bool FnCreateAFont(
        IntPtr pszFontFile,
        CHARACTER_CODE_SET CharaSet,
        int nId
    );


    [DllImport("CoreWrapper.dll", CharSet = CharSet.Ansi, EntryPoint = "RegisterOutputText", ExactSpelling = false, CallingConvention = CallingConvention.StdCall)]
    private static extern void RegisterOutputText(FnOutputText callback);
    delegate void FnOutputText(
        int nFontId,
        IntPtr psText,
        int nCount,
        int nX,
        int nY,
        uint Color,
        int nLineWidth,
        int nZ,
        uint BorderColor
    );


    //[DllImport("CoreWrapper.dll", CharSet = CharSet.Ansi, EntryPoint = "RegisterOutputRichText", ExactSpelling = false, CallingConvention = CallingConvention.StdCall)]
    //private static extern void RegisterOutputRichText(FnOutputRichText callback); 
    //delegate int FnOutputRichText(
    //    int  nFontId, 
    //    KOutputTextParam*  pParam, 
    //    IntPtr  psText, 
    //    int  nCount , 
    //    int  nLineWidth  
    //);


    //[DllImport("CoreWrapper.dll", CharSet = CharSet.Ansi, EntryPoint = "RegisterLocateRichText", ExactSpelling = false, CallingConvention = CallingConvention.StdCall)]
    //private static extern void RegisterLocateRichText(FnLocateRichText callback); 
    //delegate int FnLocateRichText(
    //    int  nX , 
    //    int  nY, 
    //    int  nFontId, 
    //    KOutputTextParam*  pParam, 
    //    IntPtr  psText, 
    //    int  nCount , 
    //    int  nLineWidth  
    //);


    [DllImport("CoreWrapper.dll", CharSet = CharSet.Ansi, EntryPoint = "RegisterReleaseAFont", ExactSpelling = false, CallingConvention = CallingConvention.StdCall)]
    private static extern void RegisterReleaseAFont(FnReleaseAFont callback);
    delegate void FnReleaseAFont(
        int nId
    );


    [DllImport("CoreWrapper.dll", CharSet = CharSet.Ansi, EntryPoint = "RegisterCreateImage", ExactSpelling = false, CallingConvention = CallingConvention.StdCall)]
    private static extern void RegisterCreateImage(FnCreateImage callback);
    delegate uint FnCreateImage(
        IntPtr pszName,
        int nWidth,
        int nHeight,
        KIS_IMAGE_TYPE nType
    );


    [DllImport("CoreWrapper.dll", CharSet = CharSet.Ansi, EntryPoint = "RegisterFreeImage", ExactSpelling = false, CallingConvention = CallingConvention.StdCall)]
    private static extern void RegisterFreeImage(FnFreeImage callback);
    delegate void FnFreeImage(
        IntPtr pszImage
    );


    [DllImport("CoreWrapper.dll", CharSet = CharSet.Ansi, EntryPoint = "RegisterFreeAllImage", ExactSpelling = false, CallingConvention = CallingConvention.StdCall)]
    private static extern void RegisterFreeAllImage(FnFreeAllImage callback);
    delegate void FnFreeAllImage();


    //[DllImport("CoreWrapper.dll", CharSet = CharSet.Ansi, EntryPoint = "RegisterGetBitmapDataBuffer", ExactSpelling = false, CallingConvention = CallingConvention.StdCall)]
    //private static extern void RegisterGetBitmapDataBuffer(FnGetBitmapDataBuffer callback); 
    //delegate void* FnGetBitmapDataBuffer(
    //    IntPtr  pszImage, 
    //    KBitmapDataBuffInfo*  pInfo 
    //);


    //[DllImport("CoreWrapper.dll", CharSet = CharSet.Ansi, EntryPoint = "RegisterReleaseBitmapDataBuffer", ExactSpelling = false, CallingConvention = CallingConvention.StdCall)]
    //private static extern void RegisterReleaseBitmapDataBuffer(FnReleaseBitmapDataBuffer callback); 
    //delegate void FnReleaseBitmapDataBuffer(
    //    IntPtr  pszImage, 
    //    void*  pBuffer 
    //);


    [DllImport("CoreWrapper.dll", CharSet = CharSet.Ansi, EntryPoint = "RegisterGetImageParam", ExactSpelling = false, CallingConvention = CallingConvention.StdCall)]
    private static extern void RegisterGetImageParam(FnGetImageParam callback);
    delegate bool FnGetImageParam(
        IntPtr pszImage,
        ref KImageParam pImageData,
        int nType
    );


    //[DllImport("CoreWrapper.dll", CharSet = CharSet.Ansi, EntryPoint = "RegisterGetImageFrameParam", ExactSpelling = false, CallingConvention = CallingConvention.StdCall)]
    //private static extern void RegisterGetImageFrameParam(FnGetImageFrameParam callback); 
    //delegate bool FnGetImageFrameParam(
    //    IntPtr  pszImage, 
    //    int nFrame, 
    //    KRPosition2*  pOffset, 
    //    KRPosition2*  pSize, 
    //    KIS_IMAGE_TYPE  nType 
    //);


    //[DllImport("CoreWrapper.dll", CharSet = CharSet.Ansi, EntryPoint = "RegisterGetImagePixelAlpha", ExactSpelling = false, CallingConvention = CallingConvention.StdCall)]
    //private static extern void RegisterGetImagePixelAlpha(FnGetImagePixelAlpha callback); 
    //delegate int FnGetImagePixelAlpha(
    //    IntPtr  pszImage, 
    //    int  nFrame, 
    //    int  nX, 
    //    int  nY, 
    //    int  nType 
    //);


    //[DllImport("CoreWrapper.dll", CharSet = CharSet.Ansi, EntryPoint = "RegisterConverSpr", ExactSpelling = false, CallingConvention = CallingConvention.StdCall)]
    //private static extern void RegisterConverSpr(FnConverSpr callback); 
    //delegate HRESULT FnConverSpr(
    //   IntPtr  pFileName , 
    //   IntPtr  pFileNameTo , 
    //    int  nType 
    //);


    //[DllImport("CoreWrapper.dll", CharSet = CharSet.Ansi, EntryPoint = "RegisterSetImageStoreBalanceParam", ExactSpelling = false, CallingConvention = CallingConvention.StdCall)]
    //private static extern void RegisterSetImageStoreBalanceParam(FnSetImageStoreBalanceParam callback); 
    //delegate void FnSetImageStoreBalanceParam(
    //    int  nNumImage, 
    //    uint  uCheckPoint  
    //);


    //[DllImport("CoreWrapper.dll", CharSet = CharSet.Ansi, EntryPoint = "RegisterSetClipRect", ExactSpelling = false, CallingConvention = CallingConvention.StdCall)]
    //private static extern void RegisterSetClipRect(FnSetClipRect callback); 
    //delegate void FnSetClipRect(
    //     RECT*  pClipRect 
    //);


    [DllImport("CoreWrapper.dll", CharSet = CharSet.Ansi, EntryPoint = "RegisterDrawPrimitives", ExactSpelling = false, CallingConvention = CallingConvention.StdCall)]
    private static extern void RegisterDrawPrimitives(FnDrawPrimitives callback);
    delegate void FnDrawPrimitives(
        int nPrimitiveCount,
        IntPtr pPrimitives,
        uint uGenre,
        int bSinglePlaneCoord
    );


    //[DllImport("CoreWrapper.dll", CharSet = CharSet.Ansi, EntryPoint = "RegisterDrawPrimitivesOnImage", ExactSpelling = false, CallingConvention = CallingConvention.StdCall)]
    //private static extern void RegisterDrawPrimitivesOnImage(FnDrawPrimitivesOnImage callback); 
    //delegate void FnDrawPrimitivesOnImage(
    //    int  nPrimitiveCount, 
    //    IntPtr  pPrimitives, 
    //    uint  uGenre, 
    //    IntPtr  pszImage, 
    //    uint  uImage, 
    //    int& nImagePosition, 
    //    BOOL  bForceDrawFlag  
    //);


    [DllImport("CoreWrapper.dll", CharSet = CharSet.Ansi, EntryPoint = "RegisterClearImageData", ExactSpelling = false, CallingConvention = CallingConvention.StdCall)]
    private static extern void RegisterClearImageData(FnClearImageData callback);
    delegate void FnClearImageData(
        IntPtr pszImage,
        uint uImage,
        int nImagePosition
    );


    //[DllImport("CoreWrapper.dll", CharSet = CharSet.Ansi, EntryPoint = "RegisterImageNeedReDraw", ExactSpelling = false, CallingConvention = CallingConvention.StdCall)]
    //private static extern void RegisterImageNeedReDraw(FnImageNeedReDraw callback); 
    //delegate bool FnImageNeedReDraw(
    //   IntPtr  szFileName, 
    //    uint&  uImage, 
    //    int&  nPos        , 
    //    BOOL&  bImageExist     
    //);


    [DllImport("CoreWrapper.dll", CharSet = CharSet.Ansi, EntryPoint = "RegisterLookAt", ExactSpelling = false, CallingConvention = CallingConvention.StdCall)]
    private static extern void RegisterLookAt(FnLookAt callback); 
    delegate void FnLookAt(
        int  nX, 
        int  nY, 
        int  nZ, 
        int  nAdj 
    );


    //[DllImport("CoreWrapper.dll", CharSet = CharSet.Ansi, EntryPoint = "RegisterLookAtEx", ExactSpelling = false, CallingConvention = CallingConvention.StdCall)]
    //private static extern void RegisterLookAtEx(FnLookAtEx callback); 
    //delegate void FnLookAtEx(
    //    KRPosition3f&  vecCamera, 
    //    KRPosition3f&  vecLookAt 
    //);


    //[DllImport("CoreWrapper.dll", CharSet = CharSet.Ansi, EntryPoint = "RegisterCopyDeviceImageToImage", ExactSpelling = false, CallingConvention = CallingConvention.StdCall)]
    //private static extern void RegisterCopyDeviceImageToImage(FnCopyDeviceImageToImage callback); 
    //delegate bool FnCopyDeviceImageToImage(
    //    IntPtr  pszName, 
    //    int  nDeviceX, 
    //    int  nDeviceY, 
    //    int  nImageX, 
    //    int  nImageY, 
    //    int  nWidth, 
    //    int  nHeight 
    //);


    [DllImport("CoreWrapper.dll", CharSet = CharSet.Ansi, EntryPoint = "RegisterRepresentBegin", ExactSpelling = false, CallingConvention = CallingConvention.StdCall)]
    private static extern void RegisterRepresentBegin(FnRepresentBegin callback);
    delegate bool FnRepresentBegin(
        int bClear,
        uint Color
    );


    [DllImport("CoreWrapper.dll", CharSet = CharSet.Ansi, EntryPoint = "RegisterRepresentEnd", ExactSpelling = false, CallingConvention = CallingConvention.StdCall)]
    private static extern void RegisterRepresentEnd(FnRepresentEnd callback);
    delegate void FnRepresentEnd();


    //[DllImport("CoreWrapper.dll", CharSet = CharSet.Ansi, EntryPoint = "RegisterAddLight", ExactSpelling = false, CallingConvention = CallingConvention.StdCall)]
    //private static extern void RegisterAddLight(FnAddLight callback); 
    //delegate void FnAddLight(
    //    KLight Light 
    //);


    //[DllImport("CoreWrapper.dll", CharSet = CharSet.Ansi, EntryPoint = "RegisterViewPortCoordToSpaceCoord", ExactSpelling = false, CallingConvention = CallingConvention.StdCall)]
    //private static extern void RegisterViewPortCoordToSpaceCoord(FnViewPortCoordToSpaceCoord callback); 
    //delegate void FnViewPortCoordToSpaceCoord(
    //    int&  nX, 
    //    int&  nY, 
    //    int nZ 
    //);


    //[DllImport("CoreWrapper.dll", CharSet = CharSet.Ansi, EntryPoint = "RegisterAdviseRepresent", ExactSpelling = false, CallingConvention = CallingConvention.StdCall)]
    //private static extern void RegisterAdviseRepresent(FnAdviseRepresent callback); 
    //delegate long FnAdviseRepresent(
    //    IInlinePicEngineSink* IS 
    //);


    //[DllImport("CoreWrapper.dll", CharSet = CharSet.Ansi, EntryPoint = "RegisterUnAdviseRepresent", ExactSpelling = false, CallingConvention = CallingConvention.StdCall)]
    //private static extern void RegisterUnAdviseRepresent(FnUnAdviseRepresent callback); 
    //delegate long FnUnAdviseRepresent(
    //    IInlinePicEngineSink* IS 
    //);


    //[DllImport("CoreWrapper.dll", CharSet = CharSet.Ansi, EntryPoint = "RegisterSaveScreenToFile", ExactSpelling = false, CallingConvention = CallingConvention.StdCall)]
    //private static extern void RegisterSaveScreenToFile(FnSaveScreenToFile callback); 
    //delegate bool FnSaveScreenToFile(
    //    IntPtr  pszName , 
    //    ScreenFileType  eType , 
    //    uint  nQuality 
    //);


    [DllImport("CoreWrapper.dll", CharSet = CharSet.Ansi, EntryPoint = "RegisterCreateRepresentObject", ExactSpelling = false, CallingConvention = CallingConvention.StdCall)]
    private static extern void RegisterCreateRepresentObject(FnCreateRepresentObject callback);
    delegate IntPtr FnCreateRepresentObject(
        uint uGenre,
        IntPtr pObjectName,
        int nParam1,
        int nParam2
    );


    [DllImport("CoreWrapper.dll", EntryPoint = "RegisterGetRepresentParam", ExactSpelling = false, CallingConvention = CallingConvention.StdCall)]
    private static extern void RegisterGetRepresentParam(FnGetRepresentParam callback);
    delegate int FnGetRepresentParam(
        int  lCommand, 
        IntPtr  lParam, 
        IntPtr  uParam 
    );


    [DllImport("CoreWrapper.dll", CharSet = CharSet.Ansi, EntryPoint = "RegisterSetRepresentParam", ExactSpelling = false, CallingConvention = CallingConvention.StdCall)]
    private static extern void RegisterSetRepresentParam(FnSetRepresentParam callback);
    delegate int FnSetRepresentParam(
        int  lCommand,
        int lParam,
        int uParam 
    );


    //[DllImport("CoreWrapper.dll", CharSet = CharSet.Ansi, EntryPoint = "RegisterCreate3DEffectObject", ExactSpelling = false, CallingConvention = CallingConvention.StdCall)]
    //private static extern void RegisterCreate3DEffectObject(FnCreate3DEffectObject callback); 
    //delegate IK3DEffect*  FnCreate3DEffectObject(
    //    IntPtr  szFileName 
    //);


    //[DllImport("CoreWrapper.dll", CharSet = CharSet.Ansi, EntryPoint = "RegisterCreate3DEffectObjectEx", ExactSpelling = false, CallingConvention = CallingConvention.StdCall)]
    //private static extern void RegisterCreate3DEffectObjectEx(FnCreate3DEffectObjectEx callback); 
    //delegate IK3DEffectEx*  FnCreate3DEffectObjectEx(
    //    IntPtr  szFileName 
    //);


    //[DllImport("CoreWrapper.dll", CharSet = CharSet.Ansi, EntryPoint = "RegisterGetJpgImage", ExactSpelling = false, CallingConvention = CallingConvention.StdCall)]
    //private static extern void RegisterGetJpgImage(FnGetJpgImage callback); 
    //delegate KSGImageContent*  FnGetJpgImage(
    //    IntPtr  cszName, 
    //    unsigned  uRGBMask16  
    //);


    //[DllImport("CoreWrapper.dll", CharSet = CharSet.Ansi, EntryPoint = "RegisterReleaseImage", ExactSpelling = false, CallingConvention = CallingConvention.StdCall)]
    //private static extern void RegisterReleaseImage(FnReleaseImage callback); 
    //delegate void  FnReleaseImage(
    //    KSGImageContent * pImage 
    //);


    [DllImport("CoreWrapper.dll", CharSet = CharSet.Ansi, EntryPoint = "RegisterReleaseImageA", ExactSpelling = false, CallingConvention = CallingConvention.StdCall)]
    private static extern void RegisterReleaseImageA(FnReleaseImageA callback);
    delegate void FnReleaseImageA(
       IntPtr pszImage
    );


    [DllImport("CoreWrapper.dll", CharSet = CharSet.Ansi, EntryPoint = "RegisterPreLoad", ExactSpelling = false, CallingConvention = CallingConvention.StdCall)]
    private static extern void RegisterPreLoad(FnPreLoad callback);
    delegate int FnPreLoad(
        REPRESENT_UNIT_TYPE uType,
        [In, MarshalAs(UnmanagedType.LPStr)] string cszName,
        int nReserve
    );


    [DllImport("CoreWrapper.dll", CharSet = CharSet.Ansi, EntryPoint = "RegisterCreateImageEffect", ExactSpelling = false, CallingConvention = CallingConvention.StdCall)]
    private static extern void RegisterCreateImageEffect(FnCreateImageEffect callback);
    delegate uint FnCreateImageEffect(
        [In, MarshalAs(UnmanagedType.LPStr)] string filename
    );


    //[DllImport("CoreWrapper.dll", CharSet = CharSet.Ansi, EntryPoint = "RegisterBeginImageEffect", ExactSpelling = false, CallingConvention = CallingConvention.StdCall)]
    //private static extern void RegisterBeginImageEffect(FnBeginImageEffect callback); 
    //delegate bool FnBeginImageEffect(
    //    uint  handle 
    //);


    //[DllImport("CoreWrapper.dll", CharSet = CharSet.Ansi, EntryPoint = "RegisterEndImageEffect", ExactSpelling = false, CallingConvention = CallingConvention.StdCall)]
    //private static extern void RegisterEndImageEffect(FnEndImageEffect callback); 
    //delegate void FnEndImageEffect(
    //    uint  handle 
    //);


    //[DllImport("CoreWrapper.dll", CharSet = CharSet.Ansi, EntryPoint = "RegisterReleaseImageEffect", ExactSpelling = false, CallingConvention = CallingConvention.StdCall)]
    //private static extern void RegisterReleaseImageEffect(FnReleaseImageEffect callback); 
    //delegate void FnReleaseImageEffect(
    //    uint  handle 
    //);


    //[DllImport("CoreWrapper.dll", CharSet = CharSet.Ansi, EntryPoint = "RegisterSetImageEffectParam", ExactSpelling = false, CallingConvention = CallingConvention.StdCall)]
    //private static extern void RegisterSetImageEffectParam(FnSetImageEffectParam callback); 
    //delegate void FnSetImageEffectParam(
    //    uint  handle, 
    //    int  fx_index, 
    //    IntPtr  param_name, 
    //    float  x, 
    //    float  y , 
    //    float  z , 
    //    float  w  
    //);


    //[DllImport("CoreWrapper.dll", CharSet = CharSet.Ansi, EntryPoint = "RegisterUnsetImageEffectParam", ExactSpelling = false, CallingConvention = CallingConvention.StdCall)]
    //private static extern void RegisterUnsetImageEffectParam(FnUnsetImageEffectParam callback); 
    //delegate void FnUnsetImageEffectParam(
    //    uint  handle, 
    //    int  fx_index, 
    //    IntPtr  param_name 
    //);


    //[DllImport("CoreWrapper.dll", CharSet = CharSet.Ansi, EntryPoint = "RegisterReloadResources", ExactSpelling = false, CallingConvention = CallingConvention.StdCall)]
    //private static extern void RegisterReloadResources(FnReloadResources callback); 
    //delegate void FnReloadResources(
    //    RESOURCE_TYPE_FLAG  flag 
    //);


    //[DllImport("CoreWrapper.dll", CharSet = CharSet.Ansi, EntryPoint = "RegisterClearDepthBuffer", ExactSpelling = false, CallingConvention = CallingConvention.StdCall)]
    //private static extern void RegisterClearDepthBuffer(FnClearDepthBuffer callback); 
    //delegate void FnClearDepthBuffer();


    //[DllImport("CoreWrapper.dll", CharSet = CharSet.Ansi, EntryPoint = "RegisterCreateQuake", ExactSpelling = false, CallingConvention = CallingConvention.StdCall)]
    //private static extern void RegisterCreateQuake(FnCreateQuake callback); 
    //delegate DWORD FnCreateQuake(
    //    IntPtr  filename 
    //);


    //[DllImport("CoreWrapper.dll", CharSet = CharSet.Ansi, EntryPoint = "RegisterStartQuake", ExactSpelling = false, CallingConvention = CallingConvention.StdCall)]
    //private static extern void RegisterStartQuake(FnStartQuake callback); 
    //delegate void FnStartQuake(
    //    DWORD  handle, 
    //    float  scale_x , 
    //    float  scale_y , 
    //    float  scale_s , 
    //    float  speed , 
    //    bool  loop , 
    //    float  max_time  
    //);


    //[DllImport("CoreWrapper.dll", CharSet = CharSet.Ansi, EntryPoint = "RegisterStopQuake", ExactSpelling = false, CallingConvention = CallingConvention.StdCall)]
    //private static extern void RegisterStopQuake(FnStopQuake callback); 
    //delegate void FnStopQuake();


    //[DllImport("CoreWrapper.dll", CharSet = CharSet.Ansi, EntryPoint = "RegisterGetCurrentQuake", ExactSpelling = false, CallingConvention = CallingConvention.StdCall)]
    //private static extern void RegisterGetCurrentQuake(FnGetCurrentQuake callback); 
    //delegate DWORD FnGetCurrentQuake();


    //[DllImport("CoreWrapper.dll", CharSet = CharSet.Ansi, EntryPoint = "RegisterExchangeMedia", ExactSpelling = false, CallingConvention = CallingConvention.StdCall)]
    //private static extern void RegisterExchangeMedia(FnExchangeMedia callback); 
    //delegate bool FnExchangeMedia(
    //    uint _type_, 
    //    IntPtr filename 
    //);


}
