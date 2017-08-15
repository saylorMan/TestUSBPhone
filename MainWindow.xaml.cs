using AxUSBPHONECTRLLib;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WinUsbPhoneDemo;

namespace TestUSBPhone
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {

        public AxUSBPHONECTRLLib.AxUsbPhoneCtrl usbPhone = null;
        System.Windows.Forms.Integration.WindowsFormsHost host = new System.Windows.Forms.Integration.WindowsFormsHost();
        public MainWindow()
        {
          
            InitializeComponent();
            usbPhone = new AxUSBPHONECTRLLib.AxUsbPhoneCtrl();
            ((System.ComponentModel.ISupportInitialize)usbPhone).BeginInit();
            host.Child = usbPhone;
            ((System.ComponentModel.ISupportInitialize)usbPhone).EndInit();
           // this.grid.Children.Add(host);

            this.usbPhone.UsbPhoneEvent += new AxUSBPHONECTRLLib._DUsbPhoneCtrlEvents_UsbPhoneEventEventHandler(this.usbPhone_UsbPhoneEvent);
            this.usbPhone.UsbPlug += new AxUSBPHONECTRLLib._DUsbPhoneCtrlEvents_UsbPlugEventHandler(this.usbPhone_UsbPlug);
            this.usbPhone.UsbPhoneKeyPress += new AxUSBPHONECTRLLib._DUsbPhoneCtrlEvents_UsbPhoneKeyPressEventHandler(this.usbPhone_UsbPhoneKeyPress);
            this.usbPhone.OnRing += new System.EventHandler(this.usbPhone_OnRing);
            this.usbPhone.OnComingCall += new AxUSBPHONECTRLLib._DUsbPhoneCtrlEvents_OnComingCallEventHandler(this.usbPhone_OnComingCall);


            this.Loaded += MainWindow_Loaded;
        }

        /// <summary>
        /// 振铃事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// 

        int sum = 0;
        private void usbPhone_OnRing(object sender, EventArgs e)
        {
            Debug.WriteLine("usbPhone_OnRing----振铃事件");
        }


        /// <summary>
        /// 来电事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void usbPhone_OnComingCall(object sender, AxUSBPHONECTRLLib._DUsbPhoneCtrlEvents_OnComingCallEvent e)
        {
            Debug.WriteLine("usbPhone_OnComingCall----来电事件");
        }

        /// <summary>
        ///按键操作
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void usbPhone_UsbPhoneKeyPress(object sender, AxUSBPHONECTRLLib._DUsbPhoneCtrlEvents_UsbPhoneKeyPressEvent e)
        {
            WriteLog("usbPhone_UsbPlug:---" + e.key);
            try
            {
                string keyName = TXJ_EventCode.KEYCODE_STRING_TABLE[e.key];
                TestData data = new TestData(DateTime.Now, keyName, "", e.key.ToString());
                if (e.key <= (int)te_KEYCODE_TABLE.KEY_HASH)//0--9*#
                { }
                else if (e.key <= (int)te_KEYCODE_TABLE.KEY_BRIGHT)//功能菜单
                { }
                else if (e.key <= (int)te_KEYCODE_TABLE.KEY_M10)//Memory1-10
                { }
                else if (e.key <= (int)te_KEYCODE_TABLE.KEY_RING)//设置
                { }
                else if (e.key <= (int)te_KEYCODE_TABLE.DTMF_P)//DTMF
                { }
                else if (e.key > (int)te_KEYCODE_TABLE.TOTAL_KEY)//无效键
                { }
               
            }
            catch
            { }
        }


        private void usbPhone_UsbPlug(object sender, AxUSBPHONECTRLLib._DUsbPhoneCtrlEvents_UsbPlugEvent e)
        {
            WriteLog("usbPhone_UsbPlug:---" + e.isPlugIn);
            CyMsg.IsDeviceConnected = e.isPlugIn;
            SetPhoneLinkStatus(e.isPlugIn);
            if (CyMsg.IsInitDevice)
            {
                // phone.CheckDevice();
            }
            else
            {
                InitPhoneDevice();
            }
        }

        private void SetPhoneLinkStatus(bool link)
        {
            if (link)
            {
                TB.Text = String.Format("话机状态--<{0}>", "已连接");
            }
            else
            {
                TB.Text = String.Format("话机状态--<{0}>", "已断开");
            }
        }

        /// <summary>
        /// 话机事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void usbPhone_UsbPhoneEvent(object sender, AxUSBPHONECTRLLib._DUsbPhoneCtrlEvents_UsbPhoneEventEvent e)
        {
            WriteLog("phoneEvent:---" + e.@event);
            int param = e.param;//事件参数
            int pevent = e.@event;//事件类型
            string eventName = TXJ_EventCode.EVENT_STRING_TABLE[e.@event];
            TestData data = data = new TestData(DateTime.Now, eventName, e.param.ToString(), e.@event.ToString());

            switch ((te_EVT_TABLE)e.@event)
            {
                #region 0
                case te_EVT_TABLE.EVT_RESERVE://0
                    { }
                    break;
                #endregion

                #region 设备插入，拔出
                case te_EVT_TABLE.EVT_USB_ATTECH://1
                    { }
                    break;
                case te_EVT_TABLE.EVT_USB_DETECH://2
                    { }
                    break;
                #endregion

                #region 话机状态
                case te_EVT_TABLE.EVT_PARALL_HOOK_OFF://10并机挂机
                    {
                        //usbPhone.HandFreeOff();
                    }
                    break;
                case te_EVT_TABLE.EVT_PARALL_HOOK_ON://9并机摘机
                    {
                        //usbPhone.HandFreeOn();
                    }
                    break;
                case te_EVT_TABLE.EVT_HFREE_HOOK_OFF://8免提挂机
                    {
                    
                    }
                    break;
                case te_EVT_TABLE.EVT_HFREE_HOOK_ON://7免提摘机
                    {
                    
                    }
                    break;
                case te_EVT_TABLE.EVT_HAND_HOOK_OFF://6手柄挂机
                    { CyMsg.IsOffHork = false;  CyMsg.phoneStates = CyPhoneStates.挂机; sum = 0; }
                    break;
                case te_EVT_TABLE.EVT_HAND_HOOK_ON://5手柄摘机
                    { CyMsg.IsOffHork = true; CyMsg.phoneStates = CyPhoneStates.PSTN手柄摘机; }
                    break;
                case te_EVT_TABLE.EVT_PC_HOOK_OFF://4PC挂机
                    { CyMsg.IsOffHork = false; CyMsg.phoneStates = CyPhoneStates.挂机; sum = 0; }
                    break;
                case te_EVT_TABLE.EVT_PC_HOOK_ON://3PC摘机
                    { CyMsg.IsOffHork = true;  CyMsg.phoneStates = CyPhoneStates.PSTN手柄摘机; }
                    break;
                #endregion

                #region 振铃11,电话按键12,电话状态查询13,来电14,拨号完成15,DTMF二次拨号16，并机拨号（34）
                case te_EVT_TABLE.EVT_RING://11，
                    { }
                    break;
                case te_EVT_TABLE.EVT_KEY_PRESS://12
                    {

                    }
                    break;
                case te_EVT_TABLE.EVT_PHONE_STATUS://13
                    { }
                    break;
                case te_EVT_TABLE.EVT_CALL_ID://14
                    {
                    }
                    break;

                case te_EVT_TABLE.EVT_DIAL_END://15
                    {
                        CompletedDial();
                    
                    }
                    break;

                case te_EVT_TABLE.EVT_DTMF_TWICE://16
                    {
                        //if (e.param == 1 && CyMsg.iVRState == IVRStates.Start)//开启ivr语音播放
                        //    InCommingIVR_II(CyMsg.PlayIVR_I_File);
                        //else if (e.param == 2 && CyMsg.iVRState == IVRStates.Start)//开启ivr语音播放
                        //    InCommingIVR_II(CyMsg.PlayIVR_II_File);
                        //else if (e.param == 40 && CyMsg.iVRState == IVRStates.Playing)//开启ivr语音播放
                        //    InCommingIVR_III_A();
                    }
                    break;
                case te_EVT_TABLE.EVT_PARAL_DTMF://34并机拔号 DTMF码
                    {
                        //if (e.param == 1 && CyMsg.iVRState == IVRStates.Start)//开启ivr语音播放
                        //    InCommingIVR_II(CyMsg.PlayIVR_I_File);
                        //else if (e.param == 2 && CyMsg.iVRState == IVRStates.Start)//开启ivr语音播放
                        //    InCommingIVR_II(CyMsg.PlayIVR_II_File);
                        //else if (e.param == 40 && CyMsg.iVRState == IVRStates.Playing)//开启ivr语音播放
                        //    InCommingIVR_III_A();
                    }
                    break;
                #endregion

                #region 录音，放音17--22，音道（23），音量（38）
                case te_EVT_TABLE.EVT_BEGIN_PLAY:// 17
                    { }
                    break;
                case te_EVT_TABLE.EVT_PLAY_FINISH://18
                    { }
                    break;
                case te_EVT_TABLE.EVT_PLAY_STOP://19
                    { }
                    break;
                case te_EVT_TABLE.EVT_BEGIN_RECORD://20
                    { }
                    break;
                case te_EVT_TABLE.EVT_RECORD_STOP://21
                    { }
                    break;

                case te_EVT_TABLE.EVT_BUSY_ON://22
                    {
                        //if (CyMsg.iVRState == IVRStates.Playing)
                        //    InCommingIVR_III_A();
                        //if (CyMsg.IsRecordLineOn)
                        //    RecordOnLine_III_A();
                        //else if (CyMsg.IsRecord)
                        //    RecordOff();
                    }
                    break;

                case te_EVT_TABLE.EVT_CODEC_READY://23
                    { }
                    break;
                case te_EVT_TABLE.EVT_VOLUM_CHANGE://38音量消息
                    { }
                    break;
                #endregion

                #region 上传24--30
                case te_EVT_TABLE.EVT_NEW_CID_TX://24
                    { }
                    break;
                case te_EVT_TABLE.EVT_OLD_CID_TX://25
                    { }
                    break;
                case te_EVT_TABLE.EVT_DIALED_TX://26
                    { }
                    break;
                case te_EVT_TABLE.EVT_HISTORY_FINISHED://27
                    { }
                    break;
                case te_EVT_TABLE.EVT_GET_VERSION://28
                    { }
                    break;
                case te_EVT_TABLE.EVT_RESTART://29
                    { }
                    break;
                case te_EVT_TABLE.EVT_REDIAL_TEL://30
                    { }
                    break;
                #endregion

                #region 低电31
                case te_EVT_TABLE.EVT_BAT_CRITICAL://31 电池低电提示//上位软件在线检测请求 (当单片机发出请求上位软件响应USB在线[6A])，
                    { }
                    break;
                #endregion

                #region 网络摘机，挂机32--33
                case te_EVT_TABLE.EVT_VOIP_HOOK_ON://33网络电话挂机
                    { }
                    break;
                case te_EVT_TABLE.EVT_VOIP_HOOK_OFF://32网络电话摘机
                    { }
                    break;
                #endregion

                #region 厂家，硬件，序列号，总数

                case te_EVT_TABLE.TOTAL_EVENT://39基本事件
                    { }
                    break;
                case te_EVT_TABLE.MSG_HW_TYPE://37硬件类型返回
                    {
                        data.Params = String.Format("{0}[{1}]", e.param, TXJ_EventCode.DEVICE_TYPE_NAM[e.param]);
                    }
                    break;
                case te_EVT_TABLE.MSG_SERIAL://36序列号返回类型
                    {
                    }
                    break;
                case te_EVT_TABLE.MSG_VENDOR://35厂家代码信息
                    {

                    }
                    break;
                #endregion

                #region 未知

                case te_EVT_TABLE.EVT_UNKOWN:// 0xFF未识别事件
                    { }
                    break;
                default://未识别事件
                    { data = new TestData(DateTime.Now, "话机事件", e.param.ToString(), e.@event.ToString()); }
                    break;
                #endregion
            }

           
        }

        private void CompletedDial()
        {
            //bool isHandUp = true;
            //while (isHandUp)
            //{
            //    int status = usbPhone.GetPhoneStatus();



            //    Debug.WriteLine("status:-->" + status);
            //}
        }

        private void PutDown()
        {
            //usbPhone.HandFreeOff();
            //Debug.WriteLine("Action:---HandFreeOff");
        }

        private void PickUp()
        {
            //usbPhone.HandFreeOn();
            //Debug.WriteLine("Action:---HandFreeOn");
        }

        void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            usbPhone.InitPhoneDevice();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {

            usbPhone.DialNumber(number.Text, number.Text.Length);
            //usbPhone.HandFreeOff(); 
            //if (!CyMsg.IsOffHork)
            //{
            //    usbPhone.HandFreeOn();
            //    usbPhone.DialNumber(number.Text, number.Text.Length);
            //    CyMsg.IsOffHork = true;
            //    Debug.WriteLine("Action:---HandFreeOn");
            //}
            //else
            //{
            //    usbPhone.HandFreeOff();
            //    CyMsg.IsOffHork = false;
            //    Debug.WriteLine("Action:---HandFreeOff");
            //}
            
            
        }

        private void InitPhoneDevice()
        {
            int num = usbPhone.InitPhoneDevice();
            CyMsg.IsInitDevice = num == 1 ? true : false;
        }

        private void Button_Click_on(object sender, RoutedEventArgs e)
        {
            usbPhone.HandFreeOn();
        }

        private void Button_Click_off(object sender, RoutedEventArgs e)
        {
            usbPhone.HandFreeOff();
        }

        private void Button_Click_DirectCall(object sender, RoutedEventArgs e)
        {
            usbPhone.DirectDialout(number.Text);
        }

        private void WriteLog(string logstr)
        {
            Debug.WriteLine(logstr);
        }
    }
}
