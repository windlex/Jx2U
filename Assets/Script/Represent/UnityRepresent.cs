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
    public short nNumFrames;               //## ͼ�ε��ܵ�֡��Ŀ
    public short nInterval;                //## ͼ�ε�֡�����
    public short nWidth;                   //## ͼ�κ����λ�����ص㣩��
    public short nHeight;                  //## ͼ���ݿ���λ�����ص㣩
    public short nReferenceSpotX;          //## ͼ�βο��㣨���ģ��ĺ�����ֵ��
    public short nReferenceSpotY;          //## ͼ�βο��㣨���ģ���������ֵ��
    public short nNumFramesGroup;          //## ͼ�ε�֡������Ŀ��ͨ��Ӧ��Ϊͼ�η�����Ŀ����
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
    //## 16λ��֡λͼ��
    //## ISI_T_BITMAP16�ĸ�ʽΪ D3DFMT_R5G6B5 ���� D3DFMT_X1R5G5B5
    //## ������Ϊ���ָ�ʽ��iImageStore�ڲ�ȷ��������ͨ������iImageStore::IsBitmapFormat565��֪���������ָ�ʽ��
    ISI_T_BITMAP16,
    //##Documentation
    //##  spr��ʽ�Ĵ�alphaѹ��ͼ�Σ�����֡
    ISI_T_SPR,
    //##Documentation
    //## ISI_T_BITMAP16_ALPHA�ĸ�ʽΪD3DFMT_A1R5G5B5
    //## ����Ϊ���ָ�ʽ���Ǻ������ʵģ�����D3DFMT_A1R5G5B5 !!!
    ISI_T_BITMAP16_ALPHA,
    //������ΪRenderTarget��ͼ��
    ISI_T_BITMAP16_RT,
    //���Դ��д�����
    ISI_T_BITMAP16_VIDEO,
    //���ڴ��д�����
    ISI_T_BITMAP16_SYS,
    //�����ж�ȡ��BMPͼƬ
    ISI_T_STREAM_BITMAP,
}
public enum CHARACTER_CODE_SET
{
    CHARACTER_CODE_SET_START = 0,

    CHARACTER_CODE_SET_GBK = 1,	//GBK����
    CHARACTER_CODE_SET_GB2132,		//GB2132����
    CHARACTER_CODE_SET_BIG5,		//BIG5����
    CHARACTER_CODE_SET_ENGLISH,		//Ӣ�ı���
    CHARACTER_CODE_SET_VIETNAM,		//Խ���ı���

    CHARACTER_CODE_SET_END,
};
public enum REPRESENT_UNIT_TYPE
{
    //##Documentation
    //## ���	KRUPoint
    RU_T_POINT = 0,
    //##Documentation
    //## �߶�	KRULine
    RU_T_LINE,
    //## ���α߿�	KRURect
    RU_T_RECT,
    //##Documentation
    //## ͼ��(����ͼ�Σ�ֻ��������ͼԪ�����)	KRUImage
    RU_T_IMAGE,
    //##Documentation	
    //## ͼ�ξֲ�	KRUImagePart
    RU_T_IMAGE_PART,
    //##Documentation	KRUImage4
    //## ͼ��(�ı���ͼ�Σ������ĸ�ͼԪ�����)
    RU_T_IMAGE_4,
    //���ŵػ���ͼ��	KRUImageStretch
    //ֻ�ڵ�ƽ���ϻ��ƣ���ͼ��ΪISI_T_BITMAP16 ��Ч
    RU_T_IMAGE_STRETCH,
    //##Documentation
    //## �����ֵĹ���ͼ�Ρ�
    RU_T_DIVIDED_LIT_IMAGE,
    //##Documentation
    //## ��Ӱ
    RU_T_SHADOW,
    //ʱ��Ч������Ӱ
    RU_T_TIME_SHADOW,
    //ָ���ض�����ľ���
    RU_T_DIRECT_RECT,
    //##Documentation
    //## ���ݻ���������ͼ��
    RU_T_BUFFER,
    //3Dģ��
    RU_T_MODEL,
    //3D��Ч
    RU_T_3DEFFECT,
    //��Ч ����ϵͳ
    RU_T_SFX_PARTICALSYSTEM,
    //��Ч ����
    RU_T_SFX_BLADE,
    //��Ч �����
    RU_T_SFX_BILLBOARD,
    //��Ч ����
    RU_T_SFX_BELT,
    //��Ч �򵥵Ĺ����
    RU_T_SFX_SIMPLEBOARD,
    //��Ч ���ӵĹ����
    RU_T_SFX_COMPLEXBOARD,
    //��Ч ���ؼ�֡��ģ��
    RU_T_SFX_COMPLEXMODEL,
    //��Чͳһ������
    RU_T_SFX,
    // ģ�͵�����
    RU_T_MESH,

    RU_T_IMAGE_STRETCH_PART,
    // add by Freeway
    RU_T_MASK = 0xffff,

    RU_T_IMAGE_PARTSAME_TEXTURE = 0x2000000,    //�˴λ��������ͼԪȫ��ʹ��ͬһ����ͼ����Ϊ��һ��KRUImagePart��ָ������ͼ

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
		int  hWnd, 		//## ���ھ��
		int  nWidth, 		//## �豸�����ڣ������λ�����ص㣩
		int  nHeight, 		//## �豸�����ڣ��ݿ���λ�����ص㣩
		bool  bFullScreen 		//## �Ƿ��ռȫ��Ļ
	)
    {
        return true;
    }

    public static bool Reset(
		int  nWidth, 		//## �豸�����ڣ������λ�����ص㣩
		int  nHeight, 		//## �豸�����ڣ��ݿ���λ�����ص㣩
		bool  bFullScreen, 		//## �Ƿ��ռȫ��Ļ
		bool  bNotAdjustStyle = true 		//## �Ƿ���Ҫ����������ʽ
	)
    {
        return true;
    }

    public static IntPtr Get3DDevice()
    {
        return IntPtr.Zero;
    }

    public static bool CreateAFont(
        IntPtr pszFontFile, 		//## �ֿ��ļ�����
        CHARACTER_CODE_SET CharaSet, 		//## �ֿ�ʹ�õ��ַ����뼯��
        int nId 		//## �������id.
    )
    {
        string szFontFile = Marshal.PtrToStringAnsi(pszFontFile);
        Debug.Log("CreateAFont: " + szFontFile);
        return true;
    }

    public static void OutputText(
        int nFontId, 		//## ʹ�õ��������id��
        IntPtr psText, 		//## Ҫ������ַ�����
        int nCount = GLB.KRF_ZERO_END, 		//## Ҫ������ַ����ĳ���(BYTE)��\��nCount���ڵ���0ʱ���ַ������Բ���������Ľ�����������ʾ����ַ��Ľ�����\��nCountС��0ʱ����ʾ���ַ�������'\0'��β�������ݽ����ַ���ȷ������ַ����ĳ��ȡ�\Ĭ��ֵΪ-1��
        int nX = GLB.KRF_FOLLOW, 		//## �ַ�����ʾ�������X���������ֵΪKF_FOLLOW��\����ַ����������ϴ��ַ��������λ��֮��\Ĭ��ֵΪKRF_FOLLOW��
        int nY = GLB.KRF_FOLLOW, 		//## �ַ�����ʾ�������Y, �������ֵΪKF_FOLLOW��\���ַ�����ǰһ������ַ�����ͬһ�е�λ�á�\Ĭ��ֵΪKRF_FOLLOW��
        uint Color = 0xFF000000, 		//## �ַ�����ʾ��ɫ��Ĭ��Ϊ��ɫ����32bit����ARGB�ĸ�\ʽ��ʾ��ɫ��ÿ������8bit��
        int nLineWidth = 0, 		//## �Զ����е��п����ƣ������ֵС��һ��ȫ���ַ���������Զ����д���Ĭ��ֵΪ0���Ȳ����Զ����д���
        int nZ = GLB.TEXT_IN_SINGLE_PLANE_COORD, 		//
        uint BorderColor = 0 		//�ֵı�Ե��ɫ
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
        public ushort nVertAlign;	//0:����/1:�϶���/2:�¶���
        public ushort nHoriAlign;	//0:����/1:�����/2:�Ҷ���
	    public int nHoriLen;				//һ�еĿ�ȣ�����ˮƽ�����ʱ��Ĳο�����
	    public int bPicPackInSingleLine;
	    public int nPicStretchPercent;
	    public int	nRowSpacing;			//�м��
    }
    public static int OutputRichText(
        int nFontId, 		//## ʹ�õ��������id��
        IntPtr pParam, 		//
        IntPtr psText, 		//## Ҫ������ַ�����
        int nCount = GLB.KRF_ZERO_END, 		//## Ҫ������ַ����ĳ���(BYTE)��\��nCount���ڵ���0ʱ���ַ������Բ���������Ľ�����������ʾ����ַ��Ľ�����\��nCountС��0ʱ����ʾ���ַ�������'\0'��β�����м䲻����'\0'���ַ���\Ĭ��ֵΪ-1��
        int nLineWidth = 0 		//##Documentation\�Զ����е��п����ƣ������ֵС��һ��ȫ���ַ���������Զ����д���Ĭ��ֵΪ0���Ȳ����Զ����д���
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
    //    int  nX , 		//## ָ��������
    //    int  nY, 		//## ָ��������
    //    int  nFontId, 		//## ʹ�õ��������id��
    //    KOutputTextParam*  pParam, 		//
    //    IntPtr  psText, 		//## Ҫ������ַ�����
    //    int  nCount = KRF_ZERO_END, 		//## Ҫ������ַ����ĳ���(BYTE)��\��nCount���ڵ���0ʱ���ַ������Բ���������Ľ�����������ʾ����ַ��Ľ�����\��nCountС��0ʱ����ʾ���ַ�������'\0'��β�����м䲻����'\0'���ַ���\Ĭ��ֵΪ-1��
    //    int  nLineWidth = 0 		//##Documentation\�Զ����е��п����ƣ������ֵС��һ��ȫ���ַ���������Զ����д���Ĭ��ֵΪ0���Ȳ����Զ����д���
    //)
    //{

    //}

    public static void ReleaseAFont(
        int nId 		//## ��������id
    )
    {
        Debug.Log("ReleaseAFont: ");
    }

    public static uint CreateImage(
        IntPtr pszName, 		//## ͼ�ε����֡�
        int nWidth, 		//## ͼ�κ��
        int nHeight, 		//## ͼ���ݿ�
        KIS_IMAGE_TYPE nType 		//## ͼ������
    )
    {
        string szName = Marshal.PtrToStringAnsi(pszName);
        Debug.Log("CreateImage: " + szName);
        uint imgId = SprMgr.CreateImage(szName, nWidth, nHeight);
        return imgId;
    }

    public static void FreeImage(
        IntPtr pszImage 		//## ͼ���ļ���/ͼ������
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
    //    IntPtr pszImage, 		//## ͼ����
    //    KBitmapDataBuffInfo* pInfo 		//���ڻ�ȡͼ�����ݻ������������Ϣ���ݣ���������ָ�룬�������Щ��Ϣ��
    //)
    //{
    //    string szImage = Marshal.PtrToStringAnsi(pszImage);
    //    Debug.Log(": ");

    //}

    //public static void ReleaseBitmapDataBuffer(
    //    IntPtr  pszImage, 		//## ͼ����
    //    void*  pBuffer 		//ͨ��GetBitmapDataBuffer���û�ȡ�õ�ͼ��������ݻ�����ָ��
    //)
    //{
    //  string szImage = Marshal.PtrToStringAnsi(pszImage);

    //}

    public static bool GetImageParam(
        IntPtr pszImage, 		//## ͼ�ε���Դ�ļ���/ͼ����
        ref KImageParam pImageData, 		//## ͼ����Ϣ�洢�ṹ��ָ��
        int nType 		//## ͼ������
    )
    {
        string szImage = Marshal.PtrToStringAnsi(pszImage);
        //Debug.Log("GetImageParam:" + szImage);
        pImageData.nNumFrames = 1;
        return false;
    }

    //public static bool GetImageFrameParam(
    //    IntPtr  pszImage, 		//## ָ�򱣴�ͼ����Դ�ļ���/ͼ�����Ļ�����
    //    int nFrame, 		//ͼ��֡����
    //    KRPosition2*  pOffset, 		//## ֡ͼ�����������ͼ�ε�ƫ��
    //    KRPosition2*  pSize, 		//## ֡ͼ�δ�С
    //    KIS_IMAGE_TYPE  nType 		//## ͼ��Դ����
    //)
    //{
    //  string szImage = Marshal.PtrToStringAnsi(pszImage);

    //}

    //public static int GetImagePixelAlpha(
    //    IntPtr  pszImage, 		//## ͼ����Դ�ļ���/ͼ����
    //    int  nFrame, 		//## ͼ�ε�֡������
    //    int  nX, 		//## �����ͼ�к�����
    //    int  nY, 		//## �����ͼ��������
    //    int  nType 		//## ͼ������
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
    //    int  nNumImage, 		//## ����ͼ�ε���Ŀ��ƽ��ֵ��
    //    uint  uCheckPoint = 1000 		//## ÿ���ٴ�����ͼ�ζ������һ��ƽ���顣
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
        int nPrimitiveCount, 		//## ���Ƶ�ͼԪ����Ŀ
        IntPtr pPrimitives, 		//## ����ͼԪ�Ľṹ������
        uint uGenre, 		//## Primitive���ͣ�ȡֵ����ö��ֵREPRESENT_UNIT_TYPE
        int bSinglePlaneCoord 		//## ͼԪ���Ʋ����ṩ�������Ƿ�Ϊ��ֱ�����ߵĵ�ƽ�����ꡣ������ǣ���ͼԪ���Ʋ����ṩ����������ά�ռ����ꡣ
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
    //    int  nPrimitiveCount, 		//## ���Ƶ�ͼԪ����Ŀ
    //    IntPtr  pPrimitives, 		//## ����ͼԪ�Ľṹ������
    //    uint  uGenre, 		//## Primitive���ͣ�ȡֵ����ö��ֵREPRESENT_UNIT_TYPE
    //    IntPtr  pszImage, 		//## ͼ����
    //    uint  uImage, 		//## Ŀ��ͼ�ε�id
    //    int& nImagePosition, 		// �����־Ϊtrue��ʾǿ�л���
    //    BOOL  bForceDrawFlag = FALSE 		//
    //)
    //{
    //  string szImage = Marshal.PtrToStringAnsi(pszImage);

    //}

    public static void ClearImageData(
        IntPtr pszImage, 		//## ͼ����
        uint uImage, 		//## Ŀ��ͼ�ε�id
        int nImagePosition 		//
    )
    {
        string szImage = Marshal.PtrToStringAnsi(pszImage);
        Debug.Log("ClearImageData: ");

    }

    //public static bool ImageNeedReDraw(
    //   IntPtr  szFileName, 		//�ļ���
    //    uint&  uImage, 		//ͼƬID
    //    int&  nPos        , 		//ͼƬλ��
    //    BOOL&  bImageExist     		// ����0��ʾImage�����ڣ�������ʾ����
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
    //    IntPtr  pszName, 		//## ͼ�ε����֡�
    //    int  nDeviceX, 		//## ��ͼ�豸ͼ�θ��Ʒ�Χ�����Ͻǵ������
    //    int  nDeviceY, 		//## ��ͼ�豸ͼ�θ��Ʒ�Χ�����Ͻǵ�������
    //    int  nImageX, 		//## Ŀ��ͼ�θ��Ƶ���Χ�����Ͻǵ������
    //    int  nImageY, 		//## Ŀ��ͼ�θ��Ƶ���Χ�����Ͻǵ�������
    //    int  nWidth, 		//## ���Ʒ�Χ�ĺ��
    //    int  nHeight 		//## ���Ʒ�Χ�ĺ��
    //)
    //{

    //}
    static float zbuff = 0f;
    public static bool RepresentBegin(
        int bClear, 		//## �Ƿ�����豸�ϵ�ǰ��ͼ�Ρ�
        uint Color 		//## ���bClearΪ��0ֵ����Colorָ����ʲô��ɫֵ������豸ԭ����ͼ�Ρ�
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
    //    int&  nX, 		//���룺��ͼ/��ͼ�豸�����x�����������ռ������x��
    //    int&  nY, 		//���룺��ͼ/��ͼ�豸�����y�����������ռ������y��
    //    int nZ 		//���������õ��Ŀռ������z��
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
        uint uGenre, 		// ��ͼ���������
        IntPtr pObjectName, 		// ��ͼ������ļ���
        int nParam1, 		// ���Ʋ���1
        int nParam2 		// ���Ʋ���2
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
