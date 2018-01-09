// CREATE BY auto_interface.py!
// 2017-12-19 16:17:10

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.InteropServices;
using System.Text;

using byte4 = System.Int32;
using float3 = Utils.float3;
using float2 = Utils.float2;
using System.Runtime.ConstrainedExecution;

//[StructLayoutAttribute(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 4)]
[StructLayoutAttribute(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 4)]
public struct KImageParam
{
    public short nNumFrames;               //## 图形的总的帧数目
    public short nInterval;                //## 图形的帧间隔。
    public short nWidth;                   //## 图形横宽（单位：像素点）。
    public short nHeight;                  //## 图形纵宽（单位：像素点）
    public short nReferenceSpotX;          //## 图形参考点（重心）的横坐标值。
    public short nReferenceSpotY;          //## 图形参考点（重心）的纵坐标值。
    public short nNumFramesGroup;          //## 图形的帧分组数目（通常应用为图形方向数目）。
};
public struct GLB{
    public const int KRF_ZERO_END	= -1;
    public const int KRF_FOLLOW		= (-2147483647 - 1);
    public const int TEXT_IN_SINGLE_PLANE_COORD = -32767;
    public static Encoding GBK;
};

public enum KIS_IMAGE_TYPE
{
    //##Documentation
    //## 16位单帧位图。
    //## ISI_T_BITMAP16的格式为 D3DFMT_R5G6B5 或者 D3DFMT_X1R5G5B5
    //## ，具体为哪种格式由iImageStore内部确定。可以通过方法iImageStore::IsBitmapFormat565来知道是用哪种格式。
    ISI_T_BITMAP16,
    //##Documentation
    //##  spr格式的带alpha压缩图形，若干帧
    ISI_T_SPR,
    //##Documentation
    //## ISI_T_BITMAP16_ALPHA的格式为D3DFMT_A1R5G5B5
    //## 具体为哪种格式这是毫无疑问的，就是D3DFMT_A1R5G5B5 !!!
    ISI_T_BITMAP16_ALPHA,
    //可以作为RenderTarget的图形
    ISI_T_BITMAP16_RT,
    //在显存中创建的
    ISI_T_BITMAP16_VIDEO,
    //在内存中创建的
    ISI_T_BITMAP16_SYS,
    //从流中读取的BMP图片
    ISI_T_STREAM_BITMAP,
}
public enum CHARACTER_CODE_SET
{
    CHARACTER_CODE_SET_START = 0,

    CHARACTER_CODE_SET_GBK = 1,	//GBK编码
    CHARACTER_CODE_SET_GB2132,		//GB2132编码
    CHARACTER_CODE_SET_BIG5,		//BIG5编码
    CHARACTER_CODE_SET_ENGLISH,		//英文编码
    CHARACTER_CODE_SET_VIETNAM,		//越南文编码

    CHARACTER_CODE_SET_END,
};
public enum REPRESENT_UNIT_TYPE
{
    //##Documentation
    //## 像点	KRUPoint
    RU_T_POINT = 0,
    //##Documentation
    //## 线段	KRULine
    RU_T_LINE,
    //## 距形边框	KRURect
    RU_T_RECT,
    //##Documentation
    //## 图形(矩形图形，只给出两个图元坐标点)	KRUImage
    RU_T_IMAGE,
    //##Documentation	
    //## 图形局部	KRUImagePart
    RU_T_IMAGE_PART,
    //##Documentation	KRUImage4
    //## 图形(四边形图形，给出四个图元坐标点)
    RU_T_IMAGE_4,
    //缩放地绘制图形	KRUImageStretch
    //只在单平面上绘制，且图形为ISI_T_BITMAP16 有效
    RU_T_IMAGE_STRETCH,
    //##Documentation
    //## 被划分的光照图形。
    RU_T_DIVIDED_LIT_IMAGE,
    //##Documentation
    //## 阴影
    RU_T_SHADOW,
    //时钟效果的阴影
    RU_T_TIME_SHADOW,
    //指向特定方向的矩形
    RU_T_DIRECT_RECT,
    //##Documentation
    //## 根据缓冲区更新图形
    RU_T_BUFFER,
    //3D模型
    RU_T_MODEL,
    //3D特效
    RU_T_3DEFFECT,
    //特效 粒子系统
    RU_T_SFX_PARTICALSYSTEM,
    //特效 刀光
    RU_T_SFX_BLADE,
    //特效 公告板
    RU_T_SFX_BILLBOARD,
    //特效 条带
    RU_T_SFX_BELT,
    //特效 简单的公告板
    RU_T_SFX_SIMPLEBOARD,
    //特效 复杂的公告板
    RU_T_SFX_COMPLEXBOARD,
    //特效 带关键帧的模型
    RU_T_SFX_COMPLEXMODEL,
    //特效统一管理器
    RU_T_SFX,
    // 模型的网格
    RU_T_MESH,

    RU_T_IMAGE_STRETCH_PART,
    // add by Freeway
    RU_T_MASK = 0xffff,

    RU_T_IMAGE_PARTSAME_TEXTURE = 0x2000000,    //此次绘制请求的图元全部使用同一个贴图，即为第一个KRUImagePart中指定的贴图

    RU_T_IMAGE_MUST_LOAD = 0x4000000,
    RU_T_IMAGE_CLEAR_FLAG = 0x8000000
};
[StructLayoutAttribute(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 1)]
public struct KRPosition3f
{
    public float fX;
    public float fY;
    public float fZ;
};
public struct KRepresentUnit
{
    public KRPosition3f oPosition;
};


[StructLayoutAttribute(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 4)]
public struct pic_cmd_t
{
    public Utils.float3 pos1;
    public Utils.float3 pos2;
	public  byte4			color;
	public  byte			options;		//modulate_by_color? wrap_x or wrap_y or no_wrap?
	public  byte			ref_method;		//center or top_left or frame top_left?
	public  byte4			is_spr;			// = 1 for .spr media, else for pic-spr media or rt-spr or lockable-spr
	[MarshalAs(UnmanagedType.ByValArray, SizeConst = 128)]
    public  byte[]			filename;
	public  byte4			handle1;
	public  byte4			handle2;
	public  short			frame_index;
};
[StructLayoutAttribute(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 4)]
public struct space_obj_primitive_cmd_t
{
    public float3 pos;
    public byte4 camera_options;
    public byte4 user_camera;
    public byte4 unused;
    public float3 lookat;
    public float3 eye;
    public float2 viewport_size;
    public IntPtr obj;
};

public class UnityRepresent
{

    public static void Release()
    {

    }

    public static bool Create(
		int  hWnd, 		//## 窗口句柄
		int  nWidth, 		//## 设备（窗口）横宽（单位：像素点）
		int  nHeight, 		//## 设备（窗口）纵宽（单位：像素点）
		bool  bFullScreen 		//## 是否独占全屏幕
	)
    {
        return true;
    }

    public static bool Reset(
		int  nWidth, 		//## 设备（窗口）横宽（单位：像素点）
		int  nHeight, 		//## 设备（窗口）纵宽（单位：像素点）
		bool  bFullScreen, 		//## 是否独占全屏幕
		bool  bNotAdjustStyle = true 		//## 是否不需要调整窗口样式
	)
    {
        return true;
    }

    public static IntPtr Get3DDevice()
    {
        return IntPtr.Zero;
    }

    public static bool CreateAFont(
        IntPtr pszFontFile, 		//## 字库文件名。
        CHARACTER_CODE_SET CharaSet, 		//## 字库使用的字符编码集。
        int nId 		//## 字体对象id.
    )
    {
        string szFontFile = Marshal.PtrToStringAnsi(pszFontFile);
        Debug.Log("CreateAFont: " + szFontFile);
        return true;
    }

    public static void OutputText(
        int nFontId, 		//## 使用的字体对象id。
        IntPtr psText, 		//## 要输出的字符串。
        int nCount = GLB.KRF_ZERO_END, 		//## 要输出的字符串的长度(BYTE)。\当nCount大于等于0时，字符串可以不包括特殊的结束符号来表示输出字符的结束。\当nCount小于0时，表示此字符串是以'\0'结尾，将根据结束字符来确定输出字符串的长度。\默认值为-1。
        int nX = GLB.KRF_FOLLOW, 		//## 字符串显示起点坐标X，如果传入值为KF_FOLLOW，\则此字符串紧接在上次字符串的输出位置之后。\默认值为KRF_FOLLOW。
        int nY = GLB.KRF_FOLLOW, 		//## 字符串显示起点坐标Y, 如果传入值为KF_FOLLOW，\此字符串与前一次输出字符串在同一行的位置。\默认值为KRF_FOLLOW。
        uint Color = 0xFF000000, 		//## 字符串显示颜色，默认为黑色，用32bit数以ARGB的格\式表示颜色，每个分量8bit。
        int nLineWidth = 0, 		//## 自动换行的行宽限制，如果其值小于一个全角字符宽度则不做自动换行处理。默认值为0，既不做自动换行处理。
        int nZ = GLB.TEXT_IN_SINGLE_PLANE_COORD, 		//
        uint BorderColor = 0 		//字的边缘颜色
    )
    {
        long uuid = (long)psText;
        string sText = Marshal.PtrToStringAnsi(psText);
        //Debug.Log(string.Format("OutputText: {0} : {1} @ [{2}, {3}]", sText, nCount, nX, nY));
        float x = (float)nX / 100;
        float y = 8.0f - (float)nY / 100;
        float z = zbuff;
        zbuff -= 0.1f;
        SprMgr.DrawText(uuid, sText, x, y, z);
        //g_pRepresent->OutputText(12, szDebugRef, KRF_ZERO_END, nX, nY, 0xffffffff, 0, 0);

    }

    [StructLayoutAttribute(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 4)]
    struct KOutputTextParam
    {
	    public int	nX;
	    public int	nY;
	    public int	nZ;
	    public short 	nSkipLine;
	    public short 	nNumLine;
	    public uint Color;
	    public uint BorderColor;
        public ushort nVertAlign;	//0:居中/1:上对齐/2:下对齐
        public ushort nHoriAlign;	//0:居中/1:左对齐/2:右对齐
	    public int nHoriLen;				//一行的宽度（文字水平对齐的时候的参考量）
	    public int bPicPackInSingleLine;
	    public int nPicStretchPercent;
	    public int	nRowSpacing;			//行间距
    }
    public static int OutputRichText(
        int nFontId, 		//## 使用的字体对象id。
        IntPtr pParam, 		//
        IntPtr psText, 		//## 要输出的字符串。
        int nCount = GLB.KRF_ZERO_END, 		//## 要输出的字符串的长度(BYTE)。\当nCount大于等于0时，字符串可以不包括特殊的结束符号来表示输出字符的结束。\当nCount小于0时，表示此字符串是以'\0'结尾，且中间不存在'\0'的字符。\默认值为-1。
        int nLineWidth = 0 		//##Documentation\自动换行的行宽限制，如果其值小于一个全角字符宽度则不做自动换行处理。默认值为0，既不做自动换行处理。
    )
    {
        // todo: txt address + xy to uuid
        long uuid = (long)psText;
        string sText2 = Marshal.PtrToStringAnsi(psText);
        byte[] txtbuff = new byte[nCount+1];
        Marshal.Copy(psText, txtbuff, 0, nCount);
        string sText = GLB.GBK.GetString(txtbuff);
        //Debug.Log(sText);
        KOutputTextParam param = (KOutputTextParam)Marshal.PtrToStructure(pParam, typeof(KOutputTextParam));
        int nX = param.nX;
        int nY = param.nY;
        float x = (float)nX / 100;
        float y = 8.0f - (float)nY / 100;
        float z = zbuff;
        zbuff -= 0.1f;
        //Debug.Log(string.Format("OutputRichText: {0} : {1} @ [{2}, {3}]", sText, nCount, nX, nY));
        SprMgr.DrawText(uuid, sText, x, y, z);
        return 1;
    }

    //public static int LocateRichText(
    //    int  nX , 		//## 指定的坐标
    //    int  nY, 		//## 指定的坐标
    //    int  nFontId, 		//## 使用的字体对象id。
    //    KOutputTextParam*  pParam, 		//
    //    IntPtr  psText, 		//## 要输出的字符串。
    //    int  nCount = KRF_ZERO_END, 		//## 要输出的字符串的长度(BYTE)。\当nCount大于等于0时，字符串可以不包括特殊的结束符号来表示输出字符的结束。\当nCount小于0时，表示此字符串是以'\0'结尾，且中间不存在'\0'的字符。\默认值为-1。
    //    int  nLineWidth = 0 		//##Documentation\自动换行的行宽限制，如果其值小于一个全角字符宽度则不做自动换行处理。默认值为0，既不做自动换行处理。
    //)
    //{

    //}

    public static void ReleaseAFont(
        int nId 		//## 字体对象的id
    )
    {
        Debug.Log("ReleaseAFont: ");
    }

    public static uint CreateImage(
        IntPtr pszName, 		//## 图形的名字。
        int nWidth, 		//## 图形横宽
        int nHeight, 		//## 图形纵宽
        KIS_IMAGE_TYPE nType 		//## 图形类型
    )
    {
        string szName = Marshal.PtrToStringAnsi(pszName);
        Debug.Log("CreateImage: " + szName);
        uint imgId = SprMgr.CreateImage(szName, nWidth, nHeight);
        return imgId;
    }

    public static void FreeImage(
        IntPtr pszImage 		//## 图形文件名/图形名。
    )
    {
        string szImage = Marshal.PtrToStringAnsi(pszImage);
        Debug.Log("FreeImage: ");

    }

    public static void FreeAllImage()
    {
        Debug.Log("FreeAllImage: ");

    }

    //public static void* GetBitmapDataBuffer(
    //    IntPtr pszImage, 		//## 图形名
    //    KBitmapDataBuffInfo* pInfo 		//用于获取图形数据缓冲区的相关信息数据，如果传入空指针，则忽略这些信息。
    //)
    //{
    //    string szImage = Marshal.PtrToStringAnsi(pszImage);
    //    Debug.Log(": ");

    //}

    //public static void ReleaseBitmapDataBuffer(
    //    IntPtr  pszImage, 		//## 图形名
    //    void*  pBuffer 		//通过GetBitmapDataBuffer调用获取得的图形像点数据缓冲区指针
    //)
    //{
    //  string szImage = Marshal.PtrToStringAnsi(pszImage);

    //}

    public static bool GetImageParam(
        IntPtr pszImage, 		//## 图形的资源文件名/图形名
        ref KImageParam pImageData, 		//## 图形信息存储结构的指针
        int nType 		//## 图形类型
    )
    {
        string szImage = Marshal.PtrToStringAnsi(pszImage);
        //Debug.Log("GetImageParam:" + szImage);
        pImageData.nNumFrames = 1;
        return false;
    }

    //public static bool GetImageFrameParam(
    //    IntPtr  pszImage, 		//## 指向保存图形资源文件名/图形名的缓冲区
    //    int nFrame, 		//图形帧索引
    //    KRPosition2*  pOffset, 		//## 帧图形相对于整个图形的偏移
    //    KRPosition2*  pSize, 		//## 帧图形大小
    //    KIS_IMAGE_TYPE  nType 		//## 图资源类型
    //)
    //{
    //  string szImage = Marshal.PtrToStringAnsi(pszImage);

    //}

    //public static int GetImagePixelAlpha(
    //    IntPtr  pszImage, 		//## 图形资源文件名/图形名
    //    int  nFrame, 		//## 图形的帧索引。
    //    int  nX, 		//## 像点在图中横坐标
    //    int  nY, 		//## 像点在图中纵坐标
    //    int  nType 		//## 图形类型
    //)
    //{
    //  string szImage = Marshal.PtrToStringAnsi(pszImage);

    //}

    //public static HRESULT ConverSpr(
    //   IntPtr  pFileName , 		//
    //   IntPtr  pFileNameTo , 		//
    //    int  nType 		//
    //)
    //{

    //}

    //public static void SetImageStoreBalanceParam(
    //    int  nNumImage, 		//## 加载图形的数目的平衡值。
    //    uint  uCheckPoint = 1000 		//## 每多少次引用图形对象后作一次平衡检查。
    //)
    //{

    //}

    //public static void SetClipRect(
    //     RECT*  pClipRect 		//
    //)
    //{

    //}

    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
    internal static IntPtr IntPtrOffset(IntPtr pbase, int offset)
    {
        if (4 == IntPtr.Size)
        {
            return (IntPtr)(pbase.ToInt32() + offset);
        }
        return (IntPtr)(pbase.ToInt64() + offset);
    }

    public static void DrawPrimitives(
        int nPrimitiveCount, 		//## 绘制的图元的数目
        IntPtr pPrimitives, 		//## 描述图元的结构的数组
        uint uGenre, 		//## Primitive类型，取值来自枚举值REPRESENT_UNIT_TYPE
        int bSinglePlaneCoord 		//## 图元绘制操作提供的坐标是否为垂直与视线的单平面坐标。如果不是，则图元绘制操作提供的坐标是三维空间坐标。
    )
    {
        //Debug.Log("DrawPrimitives: ");
        switch (uGenre)
        {
        case (int)REPRESENT_UNIT_TYPE.RU_T_IMAGE:
        case (int)REPRESENT_UNIT_TYPE.RU_T_IMAGE_PART:
        case (int)REPRESENT_UNIT_TYPE.RU_T_IMAGE_4:
        case (int)REPRESENT_UNIT_TYPE.RU_T_IMAGE_STRETCH:
        {
            IntPtr pSpr = pPrimitives;
            for (int n = 0; n < nPrimitiveCount; n++, IntPtrOffset(pSpr, IntPtr.Size))
            {
                long uuid = (long)pSpr;
                pic_cmd_t cmds = (pic_cmd_t)Marshal.PtrToStructure(pSpr, typeof(pic_cmd_t));
                //Debug.Log("uuid: " + uuid + ", Type: " + uGenre);
                //Debug.Log(GLB.GBK.GetString(cmds.filename));
                cmds.pos1.x /= 100;
                cmds.pos1.y /= 100;
                cmds.pos1.y = 8.0f - cmds.pos1.y;
                cmds.pos1.z /= 100;
                cmds.pos1.z += zbuff;
                zbuff -= 0.1f;
                //Debug.Log(string.Format("Image: {0},{4} at [{1},{2},{3}]", "", cmds.pos1.x, cmds.pos1.y, cmds.pos1.z, cmds.frame_index));
                if (cmds.filename[0] == 0)
                    break;
                SprMgr.DrawSpr(uuid, cmds);
            }
        }
        break;
        case (int)REPRESENT_UNIT_TYPE.RU_T_MODEL:
        {
            long uuid = (long)pPrimitives;
            space_obj_primitive_cmd_t cmds = (space_obj_primitive_cmd_t)Marshal.PtrToStructure(pPrimitives, typeof(space_obj_primitive_cmd_t));
            cmds.pos.x /= 100;
            cmds.pos.y /= 100;
            cmds.pos.y = 8.0f - cmds.pos.y;
            cmds.pos.z += zbuff;
            zbuff += 0.1f;
            //SprMgr.DrawModel(uuid, cmds);
        }
        break;
        }
    }

    //public static void DrawPrimitivesOnImage(
    //    int  nPrimitiveCount, 		//## 绘制的图元的数目
    //    IntPtr  pPrimitives, 		//## 描述图元的结构的数组
    //    uint  uGenre, 		//## Primitive类型，取值来自枚举值REPRESENT_UNIT_TYPE
    //    IntPtr  pszImage, 		//## 图形名
    //    uint  uImage, 		//## 目标图形的id
    //    int& nImagePosition, 		// 这个标志为true表示强行绘制
    //    BOOL  bForceDrawFlag = FALSE 		//
    //)
    //{
    //  string szImage = Marshal.PtrToStringAnsi(pszImage);

    //}

    public static void ClearImageData(
        IntPtr pszImage, 		//## 图形名
        uint uImage, 		//## 目标图形的id
        int nImagePosition 		//
    )
    {
        string szImage = Marshal.PtrToStringAnsi(pszImage);
        Debug.Log("ClearImageData: ");

    }

    //public static bool ImageNeedReDraw(
    //   IntPtr  szFileName, 		//文件名
    //    uint&  uImage, 		//图片ID
    //    int&  nPos        , 		//图片位置
    //    BOOL&  bImageExist     		// 返回0表示Image不存在，其他表示存在
    //)
    //{

    //}

    public static void LookAt(
        int nX, 		//
        int nY, 		//
        int nZ, 		//
        int nAdj 		//
    )
    {

    }

    //public static void LookAtEx(
    //    KRPosition3f&  vecCamera, 		//
    //    KRPosition3f&  vecLookAt 		//
    //)
    //{

    //}

    //public static bool CopyDeviceImageToImage(
    //    IntPtr  pszName, 		//## 图形的名字。
    //    int  nDeviceX, 		//## 绘图设备图形复制范围的左上角点横坐标
    //    int  nDeviceY, 		//## 绘图设备图形复制范围的左上角点纵坐标
    //    int  nImageX, 		//## 目的图形复制到范围的左上角点横坐标
    //    int  nImageY, 		//## 目的图形复制到范围的左上角点纵坐标
    //    int  nWidth, 		//## 复制范围的横宽
    //    int  nHeight 		//## 复制范围的横宽
    //)
    //{

    //}
    static float zbuff = 0f;
    public static bool RepresentBegin(
        int bClear, 		//## 是否清除设备上当前的图形。
        uint Color 		//## 如果bClear为非0值，则Color指出用什么颜色值来清除设备原来的图形。
    )
    {
        if (SprMgr.GetInstance() != null)
            SprMgr.GetInstance().Swap();
        zbuff = 0f;
        return true;
    }

    public static void RepresentEnd()
    {

    }

    //public static void AddLight(
    //    KLight Light 
    //)
    //{

    //}

    //public static void ViewPortCoordToSpaceCoord(
    //    int&  nX, 		//传入：视图/绘图设备坐标的x量，传出：空间坐标的x量
    //    int&  nY, 		//传入：视图/绘图设备坐标的y量，传出：空间坐标的y量
    //    int nZ 		//（期望）得到的空间坐标的z量
    //)
    //{

    //}

    //public static long AdviseRepresent(
    //    IInlinePicEngineSink* IS 		//
    //)
    //{

    //}

    //public static long UnAdviseRepresent(
    //    IInlinePicEngineSink* IS 		//
    //)
    //{

    //}

    //public static bool SaveScreenToFile(
    //    IntPtr  pszName , 		//
    //    ScreenFileType  eType , 		//
    //    uint  nQuality 		//
    //)
    //{

    //}

    public static IntPtr CreateRepresentObject(
        uint uGenre, 		// 绘图对象的类型
        IntPtr pObjectName, 		// 绘图对象的文件名
        int nParam1, 		// 控制参数1
        int nParam2 		// 控制参数2
    )
    {
        //RepresentObject Obj;
        string objName = Marshal.PtrToStringAnsi(pObjectName);
        Debug.Log("CreateRepresentObject: " + objName);
        return IntPtr.Zero;
    }

    public static int GetRepresentParam(
        int lCommand,
        IntPtr lParam,
        IntPtr uParam
    )
    {
        return 0;
    }

    public static int SetRepresentParam(
        int  lCommand, 
        int  lParam, 
        int  uParam 
    )
    {
        return 1;
    }

    //public static IK3DEffect*  Create3DEffectObject(
    //    IntPtr  szFileName 
    //)
    //{

    //}

    //public static IK3DEffectEx*  Create3DEffectObjectEx(
    //    IntPtr  szFileName 
    //)
    //{

    //}

    //public static KSGImageContent*  GetJpgImage(
    //    IntPtr  cszName, 
    //    unsigned  uRGBMask16 = (unsigned)-1 
    //)
    //{

    //}

    //public static void  ReleaseImage(
    //    KSGImageContent * pImage 
    //)
    //{

    //}

    public static void ReleaseImageA(
       IntPtr pszImage
    )
    {
        string szImage = Marshal.PtrToStringAnsi(pszImage);
        Debug.Log("ReleaseImageA: ");

    }

    public static int PreLoad(
        REPRESENT_UNIT_TYPE uType,
        [In, MarshalAs(UnmanagedType.LPStr)] string cszName,
        int nReserve
    )
    {
        Debug.Log("PreLoad: ");
        return 1;
    }

    public static uint CreateImageEffect(
        [In, MarshalAs(UnmanagedType.LPStr)] string filename
    )
    {
        Debug.Log("CreateImageEffect: ");
        return 0;
    }

    //public static bool BeginImageEffect(
    //    uint  handle 
    //)
    //{

    //}

    //public static void EndImageEffect(
    //    uint  handle 
    //)
    //{

    //}

    //public static void ReleaseImageEffect(
    //    uint  handle 
    //)
    //{

    //}

    //public static void SetImageEffectParam(
    //    uint  handle, 
    //    int  fx_index, 		// fx_index=-1 for all fx
    //    IntPtr  param_name, 
    //    float  x, 
    //    float  y = 0.0f, 
    //    float  z = 0.0f, 
    //    float  w = 1.0f 
    //)
    //{

    //}

    //public static unsafe void UnsetImageEffectParam(
    //    uint  handle, 
    //    int  fx_index, 		// fx_index=-1 for all fx
    //    IntPtr  param_name 
    //)
    //{

    //}

    //public static void ReloadResources(
    //    RESOURCE_TYPE_FLAG  flag 
    //)
    //{

    //}

    //public static void ClearDepthBuffer()
    //{

    //}

    //public static DWORD CreateQuake(
    //    IntPtr  filename 
    //)
    //{

    //}

    //public static void StartQuake(
    //    DWORD  handle, 
    //    float  scale_x = 1, 
    //    float  scale_y = 1, 
    //    float  scale_s = 1, 
    //    float  speed = 1, 
    //    bool  loop = 1, 
    //    float  max_time = 9999 
    //)
    //{

    //}

    //public static void StopQuake()
    //{

    //}

    //public static DWORD GetCurrentQuake()
    //{

    //}

    //public static bool ExchangeMedia(
    //    uint _type_, 
    //    IntPtr filename 
    //)
    //{

    //}

}
