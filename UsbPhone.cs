using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO;
using System.Drawing;

namespace WinUsbPhoneDemo
{
   /// <summary>
    ///   1 APP事件码定义表
   /// </summary>
    public enum te_EVT_TABLE
    {
         /// <summary>
        /// 0
        /// </summary>
       EVT_RESERVE = 0,
        /// <summary>
        /// USB设备插入状态1
        /// </summary>
       EVT_USB_ATTECH = 1,//(WPARAM)1;	//USB设备插入状态
        /// <summary>
        /// usb设备移出状态2
        /// </summary>
       EVT_USB_DETECH = 2,//(WPARAM)2;	//usb设备移出状态

        /// <summary>
        /// PC摘机3
        /// </summary>
       EVT_PC_HOOK_ON = 3, //PC摘机
        /// <summary>
        /// PC摘机4
        /// </summary>
       EVT_PC_HOOK_OFF = 4, //PC摘机
        /// <summary>
        /// 手柄摘机5
        /// </summary>
       EVT_HAND_HOOK_ON = 5, //手柄摘机
        /// <summary>
        /// 手柄挂机6
        /// </summary>
       EVT_HAND_HOOK_OFF = 6,//手柄挂机
        /// <summary>
        /// 免提摘机7
        /// </summary>
       EVT_HFREE_HOOK_ON = 7,//免提摘机
        /// <summary>
        /// 免提挂机8
        /// </summary>
       EVT_HFREE_HOOK_OFF = 8,//免提挂机
        /// <summary>
        /// 并机摘机9
        /// </summary>
       EVT_PARALL_HOOK_ON = 9,//并机摘机
        /// <summary>
        /// 并机挂机10
        /// </summary>
       EVT_PARALL_HOOK_OFF = 10,//并机挂机

        /// <summary>
        /// 振铃11
        /// </summary>
       EVT_RING = 11,//振铃
        /// <summary>
        /// 电话按键12
        /// </summary>
       EVT_KEY_PRESS = 12,//电话按键
        /// <summary>
        /// 电话状态查询13
        /// </summary>
       EVT_PHONE_STATUS = 13,//电话状态查询

        /// <summary>
        /// 来电14
        /// </summary>
       EVT_CALL_ID = 14,//来电
        /// <summary>
        /// 拨号完成15
        /// </summary>
       EVT_DIAL_END = 15,//拨号完成

        /// <summary>
        /// /DTMF二次拨号16
        /// </summary>
       EVT_DTMF_TWICE = 16,//DTMF二次拨号

        /// <summary>
        /// 开始放音17
        /// </summary>
       EVT_BEGIN_PLAY = 17,//开始放音
        /// <summary>
        /// 放音完成18
        /// </summary>
       EVT_PLAY_FINISH = 18,//放音完成
        /// <summary>
        /// 终止放音19
        /// </summary>
       EVT_PLAY_STOP = 19,//终止放音

        /// <summary>
        /// 开始录音20
        /// </summary>
       EVT_BEGIN_RECORD = 20,//开始录音
        /// <summary>
        /// 停止录音21
        /// </summary>
       EVT_RECORD_STOP = 21,//停止录音
        /// <summary>
        /// 对方忙音22
        /// </summary>
       EVT_BUSY_ON = 22,//对方忙音
        /// <summary>
        /// 音频通道准备就绪23
        /// </summary>
       EVT_CODEC_READY = 23,//音频通道准备就绪

        /// <summary>
        /// 未接来电上传24
        /// </summary>
       EVT_NEW_CID_TX = 24,//未接来电上传        
        /// <summary>
        /// 已接来电上传25
        /// </summary>
       EVT_OLD_CID_TX = 25,//已接来电上传        
        /// <summary>
        /// 拨出号码上传26
        /// </summary>
       EVT_DIALED_TX = 26,//拨出号码上传        

        /// <summary>
        /// 记录上传完成27
        /// </summary>
       EVT_HISTORY_FINISHED = 27,//记录上传完成
        /// <summary>
        /// 取版本号28
        /// </summary>
       EVT_GET_VERSION = 28,//取版本号
        /// <summary>
        /// 来电显示单片机重启29
        /// </summary>
       EVT_RESTART = 29,//来电显示单片机重启
        /// <summary>
        /// 重拨号码上传30
        /// </summary>
       EVT_REDIAL_TEL = 30,
        /// <summary>
        ///31 电池低电提示//上位软件在线检测请求 (当单片机发出请求上位软件响应USB在线[6A])，
        /// </summary>
        EVT_BAT_CRITICAL,          
        /// <summary>
        ///  网络电话摘机32
        /// </summary>
        EVT_VOIP_HOOK_OFF,         
        /// <summary>
        ///  网络电话挂机33
        /// </summary>
        EVT_VOIP_HOOK_ON,          
        /// <summary>
        /// 并机拔号 DTMF码34
        /// </summary>
        EVT_PARAL_DTMF,              
        /// <summary>
        /// 厂家代码信息35
        /// </summary>
        MSG_VENDOR,                
        /// <summary>
        /// 序列号返回类型36
        /// </summary>
        MSG_SERIAL,                 
        /// <summary>
        /// 硬件类型返回37
        /// </summary>
        MSG_HW_TYPE,               
        /// <summary>
        ///  音量消息38
        /// </summary>
        EVT_VOLUM_CHANGE,           
        /// <summary>
        /// 总事件39
        /// </summary>
        TOTAL_EVENT,
       /// <summary>
        /// 未识别事件
        /// </summary>
         EVT_UNKOWN = 0xFF
    }

   /// <summary>
    /// 2 APP命令宏定义
   /// </summary>
    public enum te_CMD_TABLE
    {
        //-------------------------------------------------------
        // 宏						  序号  编码    含义
        //-------------------------------------------------------
        CTL_HANDFREE_ON = 0,//0   免提摘机        
        CTL_HANDFREE_OFF,//1   免提挂机        
        CTL_RECORD_ON,//2   本地录音开始        
        CTL_RECORD_OFF,//3   本地录音结束        
        CTL_R_FUNC,//4   转接        
        CTL_RECORD_PLAY_ON,//5   本地播放开始        
        CTL_RECORD_PLAY_OFF,//6   本地播放结束        
        CTL_RECORD_LINE_PLAY_ON,//7   留言电话线上播放开始        
        CTL_RECORD_LINE_PLAY_OFF,//8   留言电话线上播放结束        
        CTL_USB_ON,//9   USB 启动        
        CTL_RINGER_OFF,//10  开始特色振铃        
        CTL_USB_VOIP_ON,//11  VOIP 开始
        CTL_USB_VOIP_OFF,//12  VOIP 结束
        ASK_PHONE_STATE,//13  电话状态检测                       
        ASK_DTMF_TX,//14  DTMF发送        
        ASK_RECORD_UP,//15  所有记录上传        
        ASK_GET_VERSION,//16  读取版本信息        
        ASK_AUTHORIZATION,//17  身份验证        
        ASK_READ_MEMORY,//18  读取内存   
        CTL_RINGER_ON,//19  关闭特色振铃
        CTL_SET_TIME,//20  设置话机的时间
        CTL_SPEECH_RECORDING_ON,//21  通话录音开始
        CTL_SPEECH_RECORDING_OFF,//22  通话录音结束
        ASK_BUSY_TONE,//23  启动忙音检测	            
        CTL_SET_FLASH_TIME,//24  写入FLASH时间  	//0906		        
        CTL_SET_MEMORY_NUMBER,//25  写入MERMORY号码	//0906	
        ASK_READ_MANUFACTURER_INF,//26  读取厂家代码   
        RESERVED_CMD1,//27  暂时保留   
        ASK_READ_SN,//28  读取序列号   
        CTL_WRITE_SN,//29  写入序列号 
        ASK_HARDWARE_INF,//30  读取硬件类型 
        CTL_VOIP_RINGER,//31  网络电话振铃  
        CTL_KEYBOARD_MAP,//32  定义电话机按键
        CTL_DISP_ICON,//33  点亮和关闭图标
        CTL_PHONE_VOLUM,//34  加减话机音量
        CTL_PHONE_MUTE,//35  话机静音 
        CTL_EEPROM_INIT,//36  初始化EEPROM，不带参数
        CTL_RECORD_LIGHT_ON,//37  开启留言灯，不带参数
        CTL_RECORD_LIGHT_OFF,//38  关闭留言灯，不带参数
        CTL_INI_SET,//39  初始化设置
        CTL_BBK_VOIP_ON,//40  BBKVOIP模式开
        CTL_BBK_VOIP_OFF,//41  BBKVOIP模式关

        TOTAL_COMMAND
    }
   /// <summary>
    /// 3 APP层电话按键码
   /// </summary>
    public enum te_KEYCODE_TABLE
    {
        /// <summary>
        /// 0
        /// </summary>
        KEY_0 = 0,
        /// <summary>
        /// 1
        /// </summary>
        KEY_1 = 1,
        /// <summary>
        /// 2
        /// </summary>
        KEY_2 = 2,
        /// <summary>
        /// 3
        /// </summary>
        KEY_3 = 3,
        /// <summary>
        /// 4
        /// </summary>
        KEY_4 = 4,
        /// <summary>
        /// 5
        /// </summary>
        KEY_5 = 5,
        /// <summary>
        /// 6
        /// </summary>
        KEY_6 = 6,
        /// <summary>
        /// 7
        /// </summary>
        KEY_7 = 7,
        /// <summary>
        /// 8
        /// </summary>
        KEY_8 = 8,
        /// <summary>
        /// 9
        /// </summary>
        KEY_9 = 9,

        /// <summary>
        /// *号10
        /// </summary>
        KEY_STAR = 10,//*号
        /// <summary>
        /// #号11
        /// </summary>
        KEY_HASH = 11,//#号
        /// <summary>
        /// p12
        /// </summary>
        KEY_P = 12,//p

        /// <summary>
        /// 上翻13
        /// </summary>
        KEY_UP = 13,//上翻
        /// <summary>
        /// 下翻14
        /// </summary>
        KEY_DOWN = 14,//下翻
        /// <summary>
        /// 重拨15
        /// </summary>
        KEY_REDIAL = 15,//重拨
        /// <summary>
        /// 免提16
        /// </summary>
        KEY_HANDFREE = 16,//免提
        /// <summary>
        /// 删除17
        /// </summary>
        KEY_DEL = 17,//删除
        /// <summary>
        /// 拨出记录18
        /// </summary>
        KEY_DIAL_REC = 18,//拨出记录
        /// <summary>
        /// 转接(闪断)19
        /// </summary>
        KEY_FLASH = 19,//转接
        /// <summary>
        /// 暂停20
        /// </summary>
        KEY_PAUSE = 20,//暂停
        /// <summary>
        /// VOIP21
        /// </summary>
        KEY_VOIP = 21,//VOIP
        /// <summary>
        /// 录音22
        /// </summary>
        KEY_RECORD = 22,//录音
        /// <summary>
        /// 未接来电23
        /// </summary>
        KEY_MISSED = 23,//未接来电
        /// <summary>
        /// 回拨24
        /// </summary>
        KEY_DIALAGAIN = 24,//回拨
        /// <summary>
        /// 亮度25
        /// </summary>
        KEY_BRIGHT = 25,//亮度
       
         /// <summary>
        /// M1-26
        /// </summary>
        KEY_M1=26,  	
        /// <summary>
        /// M2-27
        /// </summary>
        KEY_M2=27,   	
        /// <summary>
        /// M3	-
        /// </summary>
        KEY_M3=28, 
        /// <summary>
        ///  M4	-
        /// </summary>
        KEY_M4=29,  
        /// <summary>
        ///  M5-
        /// </summary>
        KEY_M5=30,  	
        /// <summary>
        /// M6-
        /// </summary>
        KEY_M6=31,   	
        /// <summary>
        /// M7-
        /// </summary>
        KEY_M7=32,   	
        /// <summary>
        /// M8-
        /// </summary>
        KEY_M8=33,   	
        /// <summary>
        ///  M9-
        /// </summary>
        KEY_M9=34,  	
        /// <summary>
        /// M10	
        /// </summary>
        KEY_M10=35,   
        /// <summary>
        /// MUTE
        /// </summary>
        KEY_MUTE=36,   
        /// <summary>
        /// VOL
        /// </summary>
        KEY_VOL=37,   

        /// <summary>
        /// 设置
        /// </summary>
        KEY_SET=38,	//	
        /// <summary>
        /// 铃声
        /// </summary>
        KEY_RING=39,	//	
        /// <summary>
        /// *
        /// </summary>
        DTMF_STAR=39,
        /// <summary>
        /// #
        /// </summary>
        DTMF_HASH=40,
        /// <summary>
        /// A
        /// </summary>
        DTMF_A=41,
        /// <summary>
        /// B
        /// </summary>
        DTMF_B=42,
        /// <summary>
        /// C
        /// </summary>
        DTMF_C=43,
        /// <summary>
        /// D
        /// </summary>
        DTMF_D=44,
        /// <summary>
        /// P
        /// </summary>
        DTMF_P=45,
        /// <summary>
        /// 总数46
        /// </summary>
        TOTAL_KEY=46,
        /// <summary>
        ///  无效按键
        /// </summary>
        KEY_INVALID = 255          

       
       
    }//KEYCODE_TABLE;
    /// <summary>
    /// 电话状态查询结果
    /// </summary>
    public enum te_PHONE_STATUS
    {
        PHONE_STATUS_IDLE,//	0   //IDLE状态
        PHONE_STATUS_PARALL_HOOK,//	1   //并机摘机
        PHONE_STATUS_HANDFREE_HOOK,//	2   //免提摘机
        PHONE_STATUS_HAND_HOOK,//	3   //手柄摘机
        TOTAL_PHONE_STATUS,
        PHONE_STATUS_INVALID = 255    //无效状态
    }//PHONE_STATUS;

    public enum te_DEVICE_CHIP_ID
    {
        CHIP_UNKOWN = 0,
        CHIP_HT82M99E,
        CHIP_HT82A832R
    }//DEVICE_CHIP_ID;


    public class TXJ_EventCode
    {
        #region 电话状态查询结果
        //=========================================================
        //		电话状态查询结果
        //=========================================================
        /// <summary>
        /// IDLE状态0
        /// </summary>
        public const int PHONE_STATUS_IDLE = 0;//IDLE状态
        /// <summary>
        /// 并机摘机1
        /// </summary>
        public const int PHONE_STATUS_PARALL_HOOK = 1;//并机摘机
        /// <summary>
        /// 免提摘机2
        /// </summary>
        public const int PHONE_STATUS_HANDFREE_HOOK = 2;//免提摘机
        /// <summary>
        /// 手柄摘机3
        /// </summary>
        public const int PHONE_STATUS_HAND_HOOK = 3;//手柄摘机
        /// <summary>
        /// 无效状态255
        /// </summary>
        public const int PHONE_STATUS_INVALID = 255;//无效状态
        #endregion

        #region 其它参数
        //====================================================
        //			4 其它参数
        //====================================================
        //----------------------------------------------------
        //			4.1 话机USB主控芯片类型
        //----------------------------------------------------
        public const int PDIUSBD12 = 0;
        public const int CM109 = 1;
        public const string USB_CONTROL_IC_KIND = "PDIUSBD12";
        public const int POS_PDIUSBD12 = 1;
        public const int POS_CM109 = 0;
        //----------------------------------------------------
        //			4.1 初始化等待时长:秒
        //----------------------------------------------------
        /// <summary>
        /// 初始化等待时长:秒
        /// </summary>
        public const int ATTACH_TIMEOUT = 2;
        //----------------------------------------------------
        //			4.2 连接状态
        //----------------------------------------------------
        public const bool USB_STU_DISCONNECT = false;
        public const bool USB_STU_CONNECT = true;
        //----------------------------------------------------
        //			4.3 来电显示缓冲定义
        //----------------------------------------------------
        public const int CID_BUFFER_MAX_LEN = 64;
        /// <summary>
        /// 
        /// </summary>
        public const int CID_NAME_BUF_MAX_LEN = 32;
        /// <summary>
        /// 缓存大小
        /// </summary>
        public const int EP1_PACKAGE_SIZE = 16;
        #endregion

      

        //====================================================
        //			4 其它参数


        public const int WM_USER = 0x0400;
        public const int WM_USB_PNP = (WM_USER + 779); //defined in dll

        public  IntPtr DEFALT_DEV = (IntPtr)(-3);// ((HANDLE)(-3));
        public  IntPtr BROADCAST_HANDLE = (IntPtr)(-2);// ((HANDLE)(-2))
        public static IList<TXJ_PhoneCmd> GetTestCmdList()
        {
            IList<TXJ_PhoneCmd> cmdlist = new List<TXJ_PhoneCmd>()
            {
             new TXJ_PhoneCmd(0,"PC摘机"),
             new TXJ_PhoneCmd(1,"PC挂机"),
             new TXJ_PhoneCmd(2,"本地录音开始"),
             new TXJ_PhoneCmd(3,"本地录音结束"),
             new TXJ_PhoneCmd(4,"转接"),
             new TXJ_PhoneCmd(5,"本地播放开始"),
             new TXJ_PhoneCmd(6,"本地播放结束"),
             new TXJ_PhoneCmd(7,"留言电话线上播放开始"),
             new TXJ_PhoneCmd(8,"留言电话线上播放结束"),
             new TXJ_PhoneCmd(9,"USB 启动 "),
             new TXJ_PhoneCmd(10,"开始特色振铃"),
             new TXJ_PhoneCmd(11,"VOIP 开始"),
             new TXJ_PhoneCmd(12,"VOIP 结束"),
             new TXJ_PhoneCmd(13,"电话状态检测"),
             new TXJ_PhoneCmd(14,"DTMF发送"),
             new TXJ_PhoneCmd(15,"所有记录上传"),
             new TXJ_PhoneCmd(16,"读取版本信息"),
             new TXJ_PhoneCmd(17,"身份验证"),
             new TXJ_PhoneCmd(18,"读取78806内存"),
             new TXJ_PhoneCmd(19,"关闭特色振铃"),
             new TXJ_PhoneCmd(20,"设置话机的时间"),
             new TXJ_PhoneCmd(21,"通话录音开始"),
             new TXJ_PhoneCmd(22,"通话录音结束"),
             new TXJ_PhoneCmd(23,"启动忙音检测"),
             new TXJ_PhoneCmd(24,"写入转接时间"),
             new TXJ_PhoneCmd(25,"写入Mermory号码"),
             new TXJ_PhoneCmd(26,"读取厂家代码"),
             new TXJ_PhoneCmd(27,"*保留未用！--"),
             new TXJ_PhoneCmd(28,"读取序列号"),
             new TXJ_PhoneCmd(29,"写入序列号 "),
             new TXJ_PhoneCmd(30,"读取硬件类型"),
              new TXJ_PhoneCmd(31,"网络电话振铃"),
             new TXJ_PhoneCmd(32,"自定义键盘"),
             new TXJ_PhoneCmd(33,"点亮和关闭图标"),
             new TXJ_PhoneCmd(34,"加减话机音量"),
             new TXJ_PhoneCmd(35,"取消话机静音"),
             new TXJ_PhoneCmd(36,"初始化EEProm"),
             new TXJ_PhoneCmd(37,"开启留言灯"),
             new TXJ_PhoneCmd(38,"初始化设置"),
              new TXJ_PhoneCmd(39,"BBKVoip模式开"),
             new TXJ_PhoneCmd(40,"BBKVoip模式关")
            };
            return cmdlist;
        }
        /// <summary>
        /// 设备列表
        /// </summary>
        public static List<string> DEVICE_TYPE_NAM
        {
            get
            {
                return new List<string>(){
                "未知设备",
                "来电盒子",
                "电话机",
                "录音电话机",
                "录音盒",
                "网络电话机"};
            }
        }
        /// <summary>
        /// 事件列表
        /// </summary>
        public static List<string> EVENT_STRING_TABLE
        {
            get
            {
                return new List<string>(){
   
    "保留消息字",
    "USB设备插入状态",
    "usb设备移出状态",
    "PC摘机",
    "PC挂机",
    "手柄摘机",
    "手柄挂机",
    "免提摘机",
    "免提挂机",
    "并机摘机",
    "并机挂机",
    "振铃",
    "电话按键",
    "电话状态查询",
    "来电",
    "拨号完成",
    "摘机DTMF",
    "开始放音",
    "放音完成",
    "终止放音",
    "开始录音",
    "停止录音",
    "对方忙音",
    "音频通道准备就绪",
    "未接来电上传",
    "已接来电上传",
    "拨出号码上传",
    "记录上传完成",
    "取版本号",
    "来电显示单片机重启",
    "重拨号码上传",
    "电池低电提示",
    "网络电话摘机",
    "网络电话挂机",
    "并机拔号",
    "厂家代码信息",
    "序列号信息",
    "硬件类型信息",
    "音量变化消息"};
            }
        }
        /// <summary>
        /// 命令列表
        /// </summary>
        public static List<string> COMMAND_STRING_TABLE
        {
            get
            {
                return new List<string>(){
  "免提摘机",
    "免提挂机",
    "本地录音开始",
    "本地录音结束",
    "转接",
    "本地播放开始",
    "本地播放结束",
    "留言电话线上播放开始",
    "留言电话线上播放结束",
    "USB 启动",
    "开始特色振铃",
    "VOIP 开始",
    "VOIP 结束",
    "电话状态检测",
    "DTMF发送",
    "所有记录上传",
    "读取版本信息",
    "身份验证",
    "读取内存",
    "关闭特色振铃",
    "设置话机的时间",
    "通话录音开始",
    "通话录音结束",
    "启动忙音检测",
    "写入转接时间",
    "写入MERMORY号码",
    "读取厂家代码",
    "*保留未用!--",
    "读取序列号",
    "写入序列号",
    "读取硬件类型",
    "网络电话振铃",
    "自定义键盘",
    "点亮和关闭图标",
    "加减话机音量",
    "取消话机静音",
    "初始化EEPROM",
    "开启留言灯",
    "关闭留言灯",
    "初始化设置",
    "BBKVOIP模式开",
    "BBKVOIP模式关"};
            }

        }
        /// <summary>
        /// 按键列表
        /// </summary>
        public static List<string> KEYCODE_STRING_TABLE
        {
            get
            {
                return new List<string>(){
   "0",
    "1",
    "2",
    "3",
    "4",
    "5",
    "6",
    "7",
    "8",
    "9",
    " *号",
    " #号",
    " p",
    "上翻",
    "下翻",
    "重拨",
    "免提",
    "删除/R",
    "拨出记录",
    "闪断",
    "暂停",
    "VOIP",
    "录音",
    "未接来电",
    "回拨",
    "亮度",
    "M1",
    "M2",
    "M3",
    "M4",
    "M5",
    "M6",
    "M7",
    "M8",
    "M9",
    "M10",
    "MUTE",
    "VOL",
    "设置",
    "铃声",
    "*",
    "#",
    "A",
    "B",
    "C",
    "D",
    "P",
    "无效按键"};
            }

        }
        /// <summary>
        /// 按键英文列表
        /// </summary>
        public static List<string> KEYCODE_STRING_TABLE_ENG
        {
            get
            {
                return new List<string>(){
    "0",
    "1",
    "2",
    "3",
    "4",
    "5",
    "6",
    "7",
    "8",
    "9",
    " *",
    " #",
    " p",
    "UP",
    "DOWN",
    "REDIAL",
    "HANDFREE",
    "DEL",
    "HISTORY DIAL",
    "R",
    "PAUSE",
    "VOIP",
    "RECORD",
    "MISS CALL",
    "DIAL BACK",
    "LIGHT",
    "SETTING",
    "RING",
    "*",
    "#",
    "A",
    "B",
    "C",
    "D",
    "P",
    "INVALID KEY"};
            }

        }
        /// <summary>
        /// Memory列表
        /// </summary>
        public static List<string> MEMORY_KEY_LIST
        {
            get
            {
                return new List<string>(){
    "M1",
    "M2",
    "M3",
    "M4",
    "M5",
    "M6",
    "M7",
    "M8",
    "M9",
    "M10"};
            }

        }
        /// <summary>
        /// 话机消息列表
        /// </summary>
        public static List<string> PHONE_STRING_TABLE
        {
            get
            {
                return new List<string>(){
   "IDLE状态",
    "并机摘机",
    "免提摘机",
    "手柄摘机",
    "无效状态"};
            }
        }

        /// <summary>
        /// 命令对象
        /// </summary>
        public class TXJ_PhoneCmd
        {
            public short Cmd { get; set; }
            public string Describe { get; set; }
            public TXJ_PhoneCmd(short cmd, string describe)
            {
                this.Cmd = cmd;
                this.Describe = describe;
            }
        }
    }
   
}

