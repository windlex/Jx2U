using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//using CoreWrapper;
using System.Runtime;
using System.Runtime.InteropServices;

public class URepresentObject
{
    public static void AdjustColor(IntPtr objId, uint uColor)
    {

    }
    public static void Bind(IntPtr objId, IntPtr pRepresentObject, IntPtr pSocketName, IntPtr pTransform)
    {
        //const KROBindTransform* pTransform
    }
    public static float GetAnimationPos(IntPtr objId, IntPtr pAnimationName)
    {
    }
    public static bool LoadMaterialFromFile(IntPtr objId, IntPtr pszMaterialName)
    {
    }
    public static bool LoadMeshFromFile(IntPtr objId, IntPtr pszMeshName)
    {
    }
    public static bool LoadAnimationFromFile(IntPtr objId, IntPtr pszAniName)
    {
    }
    public static void PauseAnimation(IntPtr objId)
    {
    }
    public static void PlayAnimation(IntPtr objId, IntPtr pAnimationName, float StartPos, bool bLoop)
    {
    }
    public static void UORelease(IntPtr objId)
    {
    
    }
    public static void SetAlpha(IntPtr objId, float fAlpha)
    {
    }
    public static void SetAnimationPos(IntPtr objId, float AnimationPos)
    {
    }
    public static void SetRotation(IntPtr objId, float x, float y, float z)
    {
    }
    public static void SetScaling(IntPtr objId, float x, float y, float z)
    {
    }
    public static void SetPosition(IntPtr objId, float x, float y, float z)
    {
    }
    public static void UnBind(IntPtr objId)
    {
    }
    public static void SetAnimationSpeed(IntPtr objId, float fSpeed)
    {
    }
    public static void SetArgument(IntPtr objId, int eArgu, int Argument1, int Argument2)
    {
    }
    public static void GetArgument(IntPtr objId, int eArgu, IntPtr Argument1, IntPtr Argument2)
    {
    }
    public static void Render(IntPtr objId)
    {
    }
}
public partial class UR_Register
{
    public static void Init()
    {
        UO_RegisterAdjustColor(URepresentObject.AdjustColor);
        UO_RegisterBind(URepresentObject.Bind);
        UO_RegisterGetAnimationPos(URepresentObject.GetAnimationPos);
        UO_RegisterLoadMaterialFromFile(URepresentObject.LoadMaterialFromFile);
        UO_RegisterLoadMeshFromFile(URepresentObject.LoadMeshFromFile);
        UO_RegisterLoadAnimationFromFile(URepresentObject.LoadAnimationFromFile);
        UO_RegisterPauseAnimation(URepresentObject.PauseAnimation);
        UO_RegisterPlayAnimation(URepresentObject.PlayAnimation);
        UO_RegisterRelease(URepresentObject.UORelease);
        UO_RegisterSetAlpha(URepresentObject.SetAlpha);
        UO_RegisterSetAnimationPos(URepresentObject.SetAnimationPos);
        UO_RegisterSetRotation(URepresentObject.SetRotation);
        UO_RegisterSetScaling(URepresentObject.SetScaling);
        UO_RegisterSetPosition(URepresentObject.SetPosition);
        UO_RegisterUnBind(URepresentObject.UnBind);
        UO_RegisterSetAnimationSpeed(URepresentObject.SetAnimationSpeed);
        UO_RegisterSetArgument(URepresentObject.SetArgument);
        UO_RegisterGetArgument(URepresentObject.GetArgument);
        UO_RegisterRender(URepresentObject.Render);
    }

    delegate void FnAdjustColor(IntPtr objId, uint uColor);
    delegate void FnBind(IntPtr objId, IntPtr pRepresentObject, IntPtr pSocketName, IntPtr pTransform /*= NULL*/);
    delegate float FnGetAnimationPos(IntPtr objId, IntPtr pAnimationName);
    delegate bool FnLoadMaterialFromFile(IntPtr objId, IntPtr pszMaterialName);
    delegate bool FnLoadMeshFromFile(IntPtr objId, IntPtr pszMeshName);
    delegate bool FnLoadAnimationFromFile(IntPtr objId, IntPtr pszAniName);
    delegate void FnPauseAnimation(IntPtr objId);
    delegate void FnPlayAnimation(IntPtr objId, IntPtr pAnimationName, float StartPos, bool bLoop);
    delegate void FnUORelease(IntPtr objId);
    delegate void FnSetAlpha(IntPtr objId, float fAlpha);
    delegate void FnSetAnimationPos(IntPtr objId, float AnimationPos);
    delegate void FnSetRotation(IntPtr objId, float x, float y, float z);
    delegate void FnSetScaling(IntPtr objId, float x, float y, float z);
    delegate void FnSetPosition(IntPtr objId, float x, float y, float z);
    delegate void FnUnBind(IntPtr objId);
    delegate void FnSetAnimationSpeed(IntPtr objId, float fSpeed);
    delegate void FnSetArgument(IntPtr objId, int eArgu, int Argument1, int Argument2);
    delegate void FnGetArgument(IntPtr objId, int eArgu, IntPtr Argument1, IntPtr Argument2);
    delegate void FnRender(IntPtr objId);

    [DllImport("CoreWrapper.dll", ExactSpelling = false, CallingConvention = CallingConvention.StdCall)]
    private static void UO_RegisterAdjustColor(FnAdjustColor _fnAdjustColor);

    [DllImport("CoreWrapper.dll", ExactSpelling = false, CallingConvention = CallingConvention.StdCall)]
    private static void UO_RegisterBind(FnBind _fnBind);

    [DllImport("CoreWrapper.dll", ExactSpelling = false, CallingConvention = CallingConvention.StdCall)]
    private static void UO_RegisterGetAnimationPos(FnGetAnimationPos _fnGetAnimationPos);

    [DllImport("CoreWrapper.dll", ExactSpelling = false, CallingConvention = CallingConvention.StdCall)]
    private static void UO_RegisterLoadMaterialFromFile(FnLoadMaterialFromFile _fnLoadMaterialFromFile);

    [DllImport("CoreWrapper.dll", ExactSpelling = false, CallingConvention = CallingConvention.StdCall)]
    private static void UO_RegisterLoadMeshFromFile(FnLoadMeshFromFile _fnLoadMeshFromFile);

    [DllImport("CoreWrapper.dll", ExactSpelling = false, CallingConvention = CallingConvention.StdCall)]
    private static void UO_RegisterLoadAnimationFromFile(FnLoadAnimationFromFile _fnLoadAnimationFromFile);

    [DllImport("CoreWrapper.dll", ExactSpelling = false, CallingConvention = CallingConvention.StdCall)]
    private static void UO_RegisterPauseAnimation(FnPauseAnimation _fnPauseAnimation);

    [DllImport("CoreWrapper.dll", ExactSpelling = false, CallingConvention = CallingConvention.StdCall)]
    private static void UO_RegisterPlayAnimation(FnPlayAnimation _fnPlayAnimation);

    [DllImport("CoreWrapper.dll", ExactSpelling = false, CallingConvention = CallingConvention.StdCall)]
    private static void UO_RegisterRelease(FnUORelease _fnRelease);

    [DllImport("CoreWrapper.dll", ExactSpelling = false, CallingConvention = CallingConvention.StdCall)]
    private static void UO_RegisterSetAlpha(FnSetAlpha _fnSetAlpha);

    [DllImport("CoreWrapper.dll", ExactSpelling = false, CallingConvention = CallingConvention.StdCall)]
    private static void UO_RegisterSetAnimationPos(FnSetAnimationPos _fnSetAnimationPos);

    [DllImport("CoreWrapper.dll", ExactSpelling = false, CallingConvention = CallingConvention.StdCall)]
    private static void UO_RegisterSetRotation(FnSetRotation _fnSetRotation);

    [DllImport("CoreWrapper.dll", ExactSpelling = false, CallingConvention = CallingConvention.StdCall)]
    private static void UO_RegisterSetScaling(FnSetScaling _fnSetScaling);

    [DllImport("CoreWrapper.dll", ExactSpelling = false, CallingConvention = CallingConvention.StdCall)]
    private static void UO_RegisterSetPosition(FnSetPosition _fnSetPosition);

    [DllImport("CoreWrapper.dll", ExactSpelling = false, CallingConvention = CallingConvention.StdCall)]
    private static void UO_RegisterUnBind(FnUnBind _fnUnBind);

    [DllImport("CoreWrapper.dll", ExactSpelling = false, CallingConvention = CallingConvention.StdCall)]
    private static void UO_RegisterSetAnimationSpeed(FnSetAnimationSpeed _fnSetAnimationSpeed);

    [DllImport("CoreWrapper.dll", ExactSpelling = false, CallingConvention = CallingConvention.StdCall)]
    private static void UO_RegisterSetArgument(FnSetArgument _fnSetArgument);

    [DllImport("CoreWrapper.dll", ExactSpelling = false, CallingConvention = CallingConvention.StdCall)]
    private static void UO_RegisterGetArgument(FnGetArgument _fnGetArgument);

    [DllImport("CoreWrapper.dll", ExactSpelling = false, CallingConvention = CallingConvention.StdCall)]
    private static void UO_RegisterRender(FnRender _fnRender);

}
