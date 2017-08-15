using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Drawing;
using System.IO;
using System;

namespace WinUsbPhoneDemo
{
    /// <summary>
    /// 电话相关平台消息定义类
    /// </summary>
    public class CyMsg
    {
        public static VoiceStates recordStates = VoiceStates.Stop;
        /// <summary>
        /// 录音文件保存父目录
        /// </summary>
        public static string RecordFileDir = "";
        /// <summary>
        /// 呼叫状态---区分呼入，呼出
        /// </summary>
        public static CallStates CallState = CallStates.CallOut;
        /// <summary>
        /// 设备是否连接
        /// </summary>
        public static bool IsDeviceConnected = false;
        /// <summary>
        /// 来电号码
        /// </summary>
        public static string InCallingNum = "";
        /// <summary>
        /// 电话状态
        /// </summary>
        public static CyPhoneStates phoneStates = CyPhoneStates.挂机;
        /// <summary>
        /// 来电未处理个数---该参数为了控制来电提示窗体的个数是（为了控制显示位置）
        /// </summary>
        public static int inComingCallNoAnswerNum = 0;

        /// <summary>
        /// 电话是否接通---主要是为了区分来电提示窗口中接听电话与电话呼叫界面切换间的不同
        /// </summary>
        public static bool IsConnected = false;
       
        /// <summary>
        /// 判断是否录音
        /// </summary>
        public static bool IsRecord = false;
        ///// <summary>
        ///// 播完号码自动接通的等待时间
        ///// </summary>
        //public static int aotoOffHork = 15;
        /// <summary>
        /// 电话设备是否初始化
        /// </summary>
        public static bool IsInitDevice = false;
       
        /// <summary>
        /// 是否摘机标志
        /// </summary>
        public static bool IsOffHork = false;
        /// <summary>
        /// 创建文件夹
        /// </summary>
        /// <param name="contactName"></param>
        /// <param name="parentPath"></param>
        /// <returns></returns>
        public static string GetRecordFilePath(string contactName, string telNo, string parentPath)
        {
            return AppDomain.CurrentDomain.BaseDirectory;
            /*
            if (String.IsNullOrEmpty(parentPath))
                parentPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "RecordFile");
            if (!Directory.Exists(parentPath))
                Directory.CreateDirectory(parentPath);
            string path = Path.Combine(parentPath, String.Format(DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Day.ToString(), "yyyyMMdd"));

            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);
            path = Path.Combine(path, telNo);
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);

            return path;
            */
        }

        public static IVRStates iVRState = IVRStates.Start;
        /// <summary>
        /// 录音文件保存地址
        /// </summary>
        public static string SaveRecordFilePath = AppDomain.CurrentDomain.BaseDirectory;
        /// <summary>
        /// 线上留言文件保存路径
        /// </summary>
        public static string SaveRecordOnLineFilePath = AppDomain.CurrentDomain.BaseDirectory;
       
       
        /// <summary>
        /// 留言提示音时长-----未用
        /// </summary>
        public static int PlayRecordOnLineWavFileTimes = 60;
        /// <summary>
        /// 录音流程最大时长
        /// </summary>
        public static int RecordFlowOnLineMaxTimes =400;
        /// <summary>
        /// 录音最大时长
        /// </summary>
        public static int RecordMaxTimes = 30;
       /// <summary>
       /// 是否为开始留言
       /// </summary>
        public static bool IsRecordLineOn = false;
        /// <summary>
        /// 是否特色响铃
        /// </summary>
        public static bool IsRingToneOn = false;
        /// <summary>
        /// 音量
        /// </summary>
        public static int DefaultVolum = 50;
        /// <summary>
        /// IVR1播放语音提示文件
        /// </summary>
        public static string PlayIVR_I_File = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "IVR1.mp3");
        /// <summary>
        /// IVR2播放语音提示文件
        /// </summary>
        public static string PlayIVR_II_File = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "IVR2.mp3");
        /// <summary>
        /// 留言提示音文件
        /// </summary>
        public static string PlayRecordOnLineWavFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "ON_LINE.mp3");
        /// <summary>
        /// 本地播放语音文件
        /// </summary>
        public static string PlayWavFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "REC.wav");
        /// <summary>
        /// 特色铃声提示音文件
        /// </summary>
        public static string PlayRingToneWavFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "IVR1.mp3");
    }
    /// <summary>
    /// 电话操作状态
    /// </summary>
    [Flags()]
    public enum CyPhoneStates
    {
        Sip来电 = 1,
        SIP外呼 = Sip来电 * 2,
        SIP手柄摘机 = SIP外呼 * 2,
        SIP免提摘机 = SIP手柄摘机 * 2,
        PSTN来电 = SIP免提摘机 * 2,
        PSTN外呼 = PSTN来电 * 2,
        PSTN对方拒接 = PSTN外呼 * 2,
        PSTN拒接来电 = PSTN对方拒接 * 2,
        PSTN手柄摘机 = PSTN拒接来电 * 2,
        PSTN免提摘机 = PSTN手柄摘机 * 2,
        SIP来电通话中 = PSTN免提摘机 * 2,
        SIP外呼通话中 = SIP来电通话中 * 2,
        SIP对方拒接 = SIP外呼通话中 * 2,
        SIP拒接对方 = SIP对方拒接 * 2,
        PSTN来电通话中 = SIP拒接对方 * 2,
        PSTN外呼通话中 = PSTN来电通话中 * 2,
        SIP呼叫失败 = PSTN外呼通话中 * 2,
        PSTN呼叫失败 = SIP呼叫失败 * 2,
        PSTN话机接听 = PSTN呼叫失败 * 2,
        SIP话机接听 = PSTN话机接听 * 2,
        SIP键盘输入 = SIP话机接听 * 2,
        PSTN键盘输入 = SIP键盘输入 * 2,
        会议通话 = PSTN键盘输入 * 2,
        会议外呼 = 会议通话 * 2,
        会议输入 = 会议外呼 * 2,
        SIP结束通话 = 会议输入 * 2,
        PSTN结束通话 = SIP结束通话 * 2,
        挂机 = PSTN结束通话 * 2,
        PSTN拨号中_NV100 = 挂机 * 2,
    }
    /// <summary>
    /// 呼叫状态
    /// </summary>
    public enum CallStates
    {
        /// <summary>
        /// 呼入
        /// </summary>
        CallIn,
        /// <summary>
        /// 呼出
        /// </summary>
        CallOut,
        /// <summary>
        /// 未接（未处理的号码）
        /// </summary>
        UnOffHook,
        /// <summary>
        /// 闲置状态
        /// </summary>
        None
    }

    /// <summary>
    /// 语音操作状态
    /// </summary>
    public enum VoiceStates
    {

        /// <summary>
        /// 开始录音
        /// </summary>
        Start,
        /// <summary>
        /// 暂停
        /// </summary>
        Pause,
        /// <summary>
        /// 继续
        /// </summary>
        Restart,
        /// <summary>
        /// 停止
        /// </summary>
        Stop
    }
    public enum IVRStates
    {
        /// <summary>
        /// 开始
        /// </summary>
        Start,
        /// <summary>
        /// 暂停
        /// </summary>
        Playing,
        /// <summary>
        /// 停止
        /// </summary>
        Stop
    }
    /// <summary>
    /// 测试数据实体
    /// </summary>
    public class TestData
    {
        public DateTime SendTime { get; set; }
        public DateTime ReciveTime { get; set; }
        public int DialTime { get; set; }
        public string SendData { get; set; }
        public string ReciveData { get; set; }
        public string Cmd { get; set; }
        public string Params { get; set; }
        // public int CmdCode { get; set; }
        public string Msg { get; set; }
        public TestData()
        {
            SendTime = DateTime.Now;
            ReciveTime = DateTime.Now;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="cmd"></param>
        /// <param name="dialtime"></param>
        /// <param name="cmdcode"></param>
        public TestData(string cmd, int dialtime, int cmdcode)
            : this()
        { this.Cmd = cmd; this.DialTime = dialtime; }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="reciveTime"></param>
        /// <param name="msg"></param>
        /// <param name="parmars"></param>
        /// <param name="recivedata"></param>
        public TestData(DateTime reciveTime, string msg, string parmars, string recivedata)
            : this()
        { this.ReciveTime = reciveTime; this.Msg = msg; this.Params = parmars; this.ReciveData = recivedata; }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sendTime"></param>
        /// <param name="senddata"></param>
        public TestData(DateTime sendTime, string senddata)
            : this()
        { this.SendTime = sendTime; this.SendData = senddata; }
    }
    /// <summary>
    /// 测试数据委托
    /// </summary>
    /// <param name="td"></param>
    public delegate void UsbPhoneTestHandle(TestData td);
}
