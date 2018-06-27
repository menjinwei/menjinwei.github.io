using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VR;
using System;
using System.Diagnostics;
using Valve.VR;
using System.Runtime.InteropServices;

public class TestOpenGame : MonoBehaviour {


    [HideInInspector]
    public Rect screenPosition;
    [DllImport("user32.dll")]
    static extern IntPtr SetWindowLong(IntPtr hwnd, int _nIndex, int dwNewLong);
    [DllImport("user32.dll")]
    static extern bool SetWindowPos(IntPtr hWnd, int hWndInsertAfter, int X, int Y, int cx, int cy, uint uFlags);
    [DllImport("user32.dll")]
    static extern IntPtr GetActiveWindow();
    IntPtr OpenWin;
    const uint SWP_SHOWWINDOW = 0x0040;
    const int GWL_STYLE = -16;
    const int WS_BORDER = 1;
    private int i = 0;

    // Use this for initialization
    void Awake ()
    {
        //获取设置当前屏幕分辩率  
        Resolution[] resolutions = Screen.resolutions;
        //设置当前分辨率  
        Screen.SetResolution(resolutions[resolutions.Length - 1].width, resolutions[resolutions.Length - 1].height, true);

        Screen.fullScreen = true;  //设置成全屏,  

    }

    // Update is called once per frame
    void Update () {
        //i++;
        //if (i < 5)
        //{
        //    SetWindowLong(GetActiveWindow(), GWL_STYLE, WS_BORDER);
        //    SetWindowPos(GetActiveWindow(), -1, (int)screenPosition.x, (int)screenPosition.y, (int)screenPosition.width, (int)screenPosition.height, SWP_SHOWWINDOW);
        //}
    }




    //public Screen getMainScreen()
    //{
    //    for (int i = 0; i < Screen.AllScreens.Length; i++)
    //    {
    //        Screen scr = Screen.AllScreens[i];
    //        if (scr.Bounds.X == 0 && scr.Bounds.Y == 0)
    //        {
    //            return scr;
    //        }
    //    }
    //    return Screen.AllScreens[0];
    //}

    //public Screen getSecondScreen()
    //{
    //    for (int i = 0; i < Screen.AllScreens.Length; i++)
    //    {
    //        Screen scr = Screen.AllScreens[i];
    //        if ((scr.Bounds.X != 0 || scr.Bounds.Y != 0) && !(scr.Bounds.X == 0 && scr.Bounds.Y == 0))
    //        {
    //            return scr;
    //        }
    //    }
    //    return Screen.AllScreens[0];
    //}



    void OnGUI()
    {
        if (GUI.Button(new Rect(200, 200, 200, 200), "OpenGame"))
        {
            VRSettings.enabled = false;
            OpenVR.ShutdownInternal();

            //Application.OpenURL("D:/testgame/Game/备选/Knockout/Knockout.exe");
            StartProcess("D:/testgame/Game/备选/Knockout/Knockout.exe");
            //StartProcess(@"D:/testgame/Game/备选/HoloBall光之球/HoloBall/HoloBall.exe");
            //SetWindowLong(GetActiveWindow(), GWL_STYLE, WS_BORDER);
            SetWindowPos(OpenWin, -1, (int)screenPosition.x, (int)screenPosition.y, (int)screenPosition.width, (int)screenPosition.height, SWP_SHOWWINDOW);
        }

        if (GUI.Button(new Rect(500, 200, 200, 200), "CloseGame"))
        {
            //KillProcess("Knockout");
            if (proo != null && !proo.HasExited)
            {
                proo.Kill();
                proo = null;
            }
            EVRInitError a = EVRInitError.None;
             OpenVR.InitInternal(ref a, EVRApplicationType.VRApplication_Scene);
            VRSettings.enabled = true;
        }
    }

    Process proo = null;

    /// <summary>
    /// 打开本地应用
    /// </summary>
    /// <param name="applicationPath">本地应用路径</param>
    void StartProcess(string applicationPath)
    {
        UnityEngine.Debug.Log("打开游戏");
        proo = new Process();
        proo.StartInfo.FileName = applicationPath;
        proo.Start();
        UnityEngine.Debug.Log(proo.ProcessName);
        UnityEngine.Debug.Log(proo.MainWindowHandle);
        OpenWin = proo.MainWindowHandle;
    }

    /// <summary>
    /// 检测应用是否正在运行
    /// </summary>
    /// <param name="processName"></param>
    /// <returns></returns>
    bool CheckProcess(string processName)
    {
        bool isRunning = false;
        Process[] processes = Process.GetProcesses();
        int i = 0;
        foreach (var pro in processes)
        {
            try
            {
                i++;
                if (!pro.HasExited)
                {
                    if (pro.ProcessName.Contains(processName))
                    {
                        UnityEngine.Debug.Log(processName + "正在运行");
                        isRunning = true;
                        continue;
                    }
                    else if (!pro.ProcessName.Contains(processName) && i > processes.Length)
                    {
                        UnityEngine.Debug.Log(processName + "没有运行");
                        isRunning = false;
                    }
                }
            }
            catch { }
        }
        return isRunning;
    }


    /// <summary>
    /// 关闭应用程序
    /// </summary>
    /// <param name="processName"></param>
    void KillProcess(string processName)
    {
        Process[] processes = Process.GetProcesses();
        foreach (var pro in processes)
        {
            try
            {
                if (!pro.HasExited)
                {
                    if (pro.ProcessName == processName)
                    {
                        pro.Kill();
                        UnityEngine.Debug.Log("杀死进程,关闭游戏");
                    }
                }
            }
            catch { }
        }
    }
}
