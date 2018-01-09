using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Runtime.InteropServices;
using System.Text;

public class Engine
{
    //IFile*	g_CreateFile(const char* FileName)
    [DllImport("Engine", EntryPoint = "g_CreateFile", CallingConvention = CallingConvention.StdCall)]
    public static extern IntPtr CreateFile(byte[] FileName);

	//C_EXPORT_API void GC()
    [DllImport("Engine", EntryPoint = "GC", CallingConvention = CallingConvention.StdCall)]
    public static extern void GC();
}
public class CoreWrapper
{
    [DllImport("CoreWrapper", EntryPoint = "Init", CallingConvention = CallingConvention.StdCall)]
    public static extern bool Init();

    [DllImport("CoreWrapper", EntryPoint = "GameLoop", CallingConvention = CallingConvention.StdCall)]
    public static extern int GameLoop();

    [DllImport("CoreWrapper", EntryPoint = "GameEnd", CallingConvention = CallingConvention.StdCall)]
    public static extern int GameEnd();

    [DllImport("CoreWrapper", EntryPoint = "HandleInput", CallingConvention = CallingConvention.StdCall)]
    public static extern int HandleInput(uint uMsg, int wParam, int lParam);

    //	C_EXPORT_API int LoadFile(char *pFileName)
    //[DllImport("CoreWrapper", EntryPoint = "LoadFile", CallingConvention = CallingConvention.StdCall)]
    //public static extern int LoadFile(byte[] pFileName, out IntPtr pData);

    [DllImport("CoreWrapper", EntryPoint = "LoadFile")]
    public static extern IntPtr LoadFile(byte[] pFileName);

	//C_EXPORT_API void InitFile()
    [DllImport("CoreWrapper", EntryPoint = "InitFile", CallingConvention = CallingConvention.StdCall)]
    public static extern void InitFile();

	//C_EXPORT_API int GetFileSize(char *pFileName)
    [DllImport("CoreWrapper", EntryPoint = "GetFileSize", CallingConvention = CallingConvention.StdCall)]
    public static extern int GetFileSize(byte[] pFileName);

    //	C_EXPORT_API int LoadFile2(char *pFileName, char *pBuffer, int size)
    [DllImport("CoreWrapper", EntryPoint = "LoadFile2", CallingConvention = CallingConvention.StdCall)]
    public static extern int LoadFile2(byte[] pFileName, ref byte[] pBuffer, int size);

    [DllImport("CoreWrapper", EntryPoint = "AOutput", CallingConvention = CallingConvention.StdCall)]
    public static extern void AOutput(FnOutput callback);
    public delegate void FnOutput(IntPtr str);

}

public class UR_Wrapper : MonoBehaviour {
    //lua_State *lua_open (int stacksize)
    [DllImport("LuaLibDll", CallingConvention = CallingConvention.StdCall)]
    private static extern IntPtr lua_open(int stacksize);

    void Output(IntPtr bystr)
    {
        string str = Marshal.PtrToStringAnsi(bystr);
        //Debug.Log("Output: "+str);
    }
    // Use this for initialization
	void Start () {
        CoreWrapper.AOutput(Output);
        //GLB.GBK = Encoding.GetEncoding("GB2312");
        //IntPtr o = g_CreateFile(Encoding.Default.GetBytes("ablblabla.bla"));
        UR_Register.Init();
        CoreWrapper.Init();
    }
	
	// Update is called once per frame
	void FixedUpdate () {
        CoreWrapper.GameLoop();
	}
    void OnDestroy()
    {
        Debug.Log("Destroyed");
        CoreWrapper.GameEnd();
    }

}
