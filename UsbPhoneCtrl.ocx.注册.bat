@echo off
echo 如果 Windows 7 下注册失败请右击本批处理文件选择以管理员方式运行
cd /d "%~dp0"
regsvr32 "UsbPhoneCtrl.ocx"
pause
