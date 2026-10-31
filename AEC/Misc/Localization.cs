using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Xml;
using AEC.Core;

namespace AEC.Misc
{
    public enum LanguageCode
    {
        zh_CN = 0,  //简体中文
        en_US,      //
    }

    public class Localization
    {
        private const string RootNode = "Localization";

        public static readonly Dictionary<string, LanguageCode> lang2CodeMap = new Dictionary<string, LanguageCode>()
        {
            {"zh-CN", LanguageCode.zh_CN},
            {"en-US", LanguageCode.en_US},
        };

        public static readonly Dictionary<LanguageCode, string> code2LangMap = new Dictionary<LanguageCode, string>()
        {
            {LanguageCode.zh_CN, "zh-CN"},
            {LanguageCode.en_US, "en-US"},
        };

        public static readonly Dictionary<LanguageCode, byte[]> code2PackageMap = new Dictionary<LanguageCode, byte[]>()
        {
            {LanguageCode.zh_CN, AEC.Properties.Resources.lang_zh_CN},
            {LanguageCode.en_US, AEC.Properties.Resources.lang_en_US},
        };

        //public static Localization GetInstance()
        //{
        //    return _instance;
        //}

        //private static Localization _instance = new Localization();

        private LanguageCode _lang = LanguageCode.zh_CN;
        private Dictionary<string, string> stringMap = new Dictionary<string, string>();
        public readonly EventBus langEventBus = new EventBus();

        public const string formEasyMode = "formEasyMode";
        public const string formProMode = "formProMode";
        public const string formMain_RemoteControl = "formMain.remoteControl";
        public const string formMain_ControlTheOtherParty = "formMain.controlTheOtherParty";
        public const string formMain_ControlledRemotelyByTheOtherParty = "formMain.controlledRemotelyByTheOtherParty";
        public const string formMain_VideoTutorial = "formMain.videoTutorial";
        public const string formMain_OpenVideoTutorial = "formMain.openVideoTutorial";
        public const string formMain_ShowTutorialQRCode = "formMain.showTutorialQRCode";
        public const string formMain_ScanQRCode = "formMain.scanQRCode";

        public const string ok = "ok";
        public const string cancel = "cancel";
        public const string buttonConnection_Connect = "buttonConnection.connect";
        public const string buttonConnection_Disconnect = "buttonConnection.disconnect";
        public const string systemIsBusyAndTryAgain = "systemIsBusyAndTryAgain";
        public const string commInterrupted = "commInterrupted";
        public const string musicVol = "musicVol";
        public const string micVol = "micVol";
        public const string effectVol = "effectVol";
        public const string timbre = "timbre";

        public const string formProMode_Music = "formProMode.music";
        public const string formProMode_MusicDeq = "formProMode.musicDeq";
        public const string formProMode_Mic = "formProMode.mic";
        public const string formProMode_MicA = "formProMode.micA";
        public const string formProMode_MicB = "formProMode.micB";
        public const string formProMode_MicAPlusB = "formProMode.micAPlusB";
        public const string formProMode_MicDeq = "formProMode.micDeq";
        public const string formProMode_MicExciter = "formProMode.micExciter";
        public const string formProMode_Effect = "formProMode.effect";
        public const string formProMode_Echo = "formProMode.echo";
        public const string formProMode_Reverb = "formProMode.reverb";
        public const string formProMode_MainOutput = "formProMode.mainOutput";
        public const string formProMode_Aux1Output = "formProMode.aux1Output";
        public const string formProMode_Aux2Output = "formProMode.aux2Output";
        public const string formProMode_CenterOutput = "formProMode.centerOutput";
        public const string formProMode_SubOutput = "formProMode.subOutput";
        public const string formProMode_SurOutput = "formProMode.surOutput";
        public const string formProMode_System = "formProMode.system";
        public const string formProMode_Monitor = "formProMode.monitor";

        public const string formProMode_DeviceModels = "formProMode.deviceModels";
        public const string formProMode_RTAOptions = "formProMode.rtaOptions";
        public const string formProMode_CommSettings = "formProMode.commSettings";
        public const string formProMode_Preferences = "formProMode.preferences";
        public const string formProMode_Language = "formProMode.language";
        public const string formProMode_Help = "formProMode.help";
        public const string formProMode_ViewHelp = "formProMode.viewHelp";
        public const string formProMode_OnlineHelp = "formProMode.onlineHelp";
        public const string formProMode_BluetoothRename = "formProMode.bluetoothRename";
        public const string formProMode_FirmwareUpdate = "formProMode.firmwareUpdate";
        public const string formProMode_ActivateDevice = "formProMode.activateDevice";
        public const string formProMode_OpenLogFileLocation = "formProMode.openLogFileLocation";
        public const string formProMode_About = "formProMode.about";

        public const string eq_Type = "eq.type";
        public const string eq_Gain = "eq.gain";
        public const string eq_Freq = "eq.freq";
        public const string eq_Q = "eq.q";
        public const string deq_CM = "deq.cm";
        public const string deq_AmpSen = "deq.ampSen";
        public const string deq_Threshold = "deq.threshold";
        public const string deq_Attack = "deq.attack";
        public const string deq_Release = "deq.release";
        public const string deq_Ratio = "deq.ratio";
        public const string deq_DM = "deq.dm";

        public const string eqGroupPanel_XBandEq = "eqGroupPanel.xBandEq";
        public const string eqGroupPanel_XBandEchoEq = "eqGroupPanel.xBandEchoEq";
        
        public const string eqReset = "eqReset";
        public const string autoMode = "autoMode";
        public const string manualMode = "manualMode";
        public const string singMode = "singMode";
        public const string discoMode = "discoMode";
        public const string eqReset_Tips = "eqReset.tips";
        public const string deqReset = "deqReset";
        public const string deqReset_Tips = "deqReset.tips";

        public const string compressorPanel_Threshold = "compressorPanel.threshold";
        public const string compressorPanel_Attack = "compressorPanel.attack";
        public const string compressorPanel_Release = "compressorPanel.release";
        public const string compressorPanel_Ratio = "compressorPanel.ratio";

        public const string x5_compAndLimiterPanel_Threshold = "x5.compAndLimiterPanel.threshold";
        public const string x5_compAndLimiterPanel_Attack = "x5.compAndLimiterPanel.attack";
        public const string x5_compAndLimiterPanel_Release = "x5.compAndLimiterPanel.release";
        public const string x5_compAndLimiterPanel_Ratio = "x5.compAndLimiterPanel.ratio";
        
        public const string fastMode = "fastMode";
        public const string mediumMode = "mediumMode";
        public const string slowMode = "slowMode";

        public const string limiterPanel = "limiterPanel";
        public const string limiterPanel_Threshold = "limiterPanel.threshold";
        public const string limiterPanel_Attack = "limiterPanel.attack";
        public const string limiterPanel_Release = "limiterPanel.release";
        public const string limiterPanel_ClipThreshold = "limiterPanel.clipThreshold";
        public const string limiterPanel_ClipRelease = "limiterPanel.clipRelease";
        public const string limiterPanel_Ratio = "limiterPanel.ratio";
        public const string limiterPanel_Mode = "limiterPanel.mode";

        public const string mixerPanel_MusicLevel = "mixerPanel.musicLevel";
        public const string mixerPanel_MicLevel = "mixerPanel.micLevel";
        public const string mixerPanel_EchoLevel = "mixerPanel.echoLevel";
        public const string mixerPanel_ReverbLevel = "mixerPanel.reverbLevel";

        public const string exciterPanel_Freq = "exciterPanel.freq";
        public const string exciterPanel_XoverFreq = "exciterPanel.xoverfreq";
        public const string exciterPanel_BandWidth = "exciterPanel.bandWidth";
        public const string exciterPanel_FBGain = "exciterPanel.fbGain";
        public const string exciterPanel_BoostLevel = "exciterPanel.boostLevel";
        public const string exciterPanel_BassExciter = "exciterPanel.bassExciter";
        public const string exciterPanel_Exciter = "exciterPanel.exciter";
        public const string exciterPanel_MicExciter1 = "exciterPanel.micExciter1";
        public const string exciterPanel_MicExciter2 = "exciterPanel.micExciter2";

        public const string panelMusic_MusicInput = "panelMusic.musicInput";
        public const string panelMusic_MusicInputPort = "panelMusic.musicInputPort";
        public const string panelMusic_MusicInputGain = "panelMusic.musicInputGain";
        public const string panelMusic_MusicInputPort3Gain = "panelMusic.musicInputPort3Gain";
        public const string panelMusic_Port = "panelMusic.port";
        public const string panelMusic_BT = "panelMusic.bt";
        public const string panelMusic_SPDIF = "panelMusic.spdif";
        public const string panelMusic_FiberPort = "panelMusic.fiberPort";
        public const string panelMusic_MusicHpf = "panelMusic.musicHpf";
        public const string panelMusic_MusicHpfType = "panelMusic.musicHpfType";
        public const string panelMusic_MusicToMainOutHpf = "panelMusic.musicToMainOutHpf";
        public const string panelMusic_MusicToMainOutHpfSlope = "panelMusic.musicToMainOutHpfSlope";
        public const string panelMusic_SuperBass = "panelMusic.superBass";

        public const string panelMic_MicInput = "panelMic.micInput";
        public const string panelMic_MicInput_AB_MIXED = "panelMic.micInput.ab.mixed";
        public const string panelMic_MicInput_SINGLE_A = "panelMic.micInput.single.a";
        public const string panelMic_MicInput_SINGLE_B = "panelMic.micInput.single.b";
        public const string panelMic_MicFBE = "panelMic.micFBE";
        public const string panelMic_MicNoiseGate = "panelMic.micNoiseGate";
        public const string panelMic_MicHpf = "panelMic.micHpf";
        public const string panelMic_MicLpf = "panelMic.micLpf";
        public const string panelMic_MicLpfType = "panelMic.micLpfType";
        public const string panelMic_MicLevel = "panelMic.micLevel";
        public const string panelMic_EffectLevel = "panelMic.effectLevel";
        public const string panelMic_MicABLink = "panelMic.micABLink";
        public const string panelMic_MicABLink_Tips = "panelMic.micABLink.tips";

        public const string panelEffect_Echo_EchoLevel = "panelEffect.echo.echoLevel";
        public const string panelEffect_Echo_EchoDirectLevel = "panelEffect.echo.echoDirectLevel";
        public const string panelEffect_Echo_Hpf = "panelEffect.echo.hpf";
        public const string panelEffect_Echo_HpfType = "panelEffect.echo.hpfType";
        public const string panelEffect_Echo_Lpf = "panelEffect.echo.lpf";
        public const string panelEffect_Echo_LpfQ = "panelEffect.echo.lpfQ";
        public const string panelEffect_Echo_LpfType = "panelEffect.echo.lpfType";
        public const string panelEffect_Echo_PreDelay = "panelEffect.echo.preDelay";
        public const string panelEffect_Echo_Delay = "panelEffect.echo.delay";
        public const string panelEffect_Echo_Repeat = "panelEffect.echo.repeat";
        public const string panelEffect_Echo_RChPreDelay = "panelEffect.echo.rChPreDelay";
        public const string panelEffect_Echo_RChDelay = "panelEffect.echo.rChDelay";

        public const string panelEffect_Reverb_ReverbLevel = "panelEffect.reverb.reverbLevel";
        public const string panelEffect_Reverb_ReverbDirectLevel = "panelEffect.reverb.reverbDirectLevel";
        public const string panelEffect_Reverb_Hpf = "panelEffect.reverb.hpf";
        public const string panelEffect_Reverb_HpfType = "panelEffect.reverb.hpfType";
        public const string panelEffect_Reverb_Lpf = "panelEffect.reverb.lpf";
        public const string panelEffect_Reverb_LpfType = "panelEffect.reverb.lpfType";
        public const string panelEffect_Reverb_MicHpf = "panelEffect.reverb.micHpf";
        public const string panelEffect_Reverb_MicLpf = "panelEffect.reverb.micLpf";
        public const string panelEffect_Reverb_MusicHpf = "panelEffect.reverb.musicHpf";
        public const string panelEffect_Reverb_MusicLpf = "panelEffect.reverb.musicLpf";
        public const string panelEffect_Reverb_PreDelay = "panelEffect.reverb.preDelay";
        public const string panelEffect_Reverb_Delay = "panelEffect.reverb.delay";
        public const string panelEffect_Reverb_BassDamp = "panelEffect.reverb.bassDamp";
        public const string panelEffect_Reverb_TrebleDamp = "panelEffect.reverb.trebleDamp";
        public const string panelEffect_Reverb_MusicReverbLevel = "panelEffect.reverb.musicReverbLevel";

        public const string panelEffect_DEffect_Threshold = "panelEffect.dEffect.threshold";
        public const string panelEffect_DEffect_Gain = "panelEffect.dEffect.gain";

        public const string panelOutput_HpfFreq = "panelOutput.hpfFreq";
        public const string panelOutput_HpfQ = "panelOutput.hpfQ";
        public const string panelOutput_HpfMode = "panelOutput.hpfMode";
        public const string panelOutput_LpfFreq = "panelOutput.lpfFreq";
        public const string panelOutput_LpfQ = "panelOutput.lpfQ";
        public const string panelOutput_LpfMode = "panelOutput.lpfMode";
        public const string panelOutput_LeftChDelay = "panelOutput.leftChDelay";
        public const string panelOutput_RightChDelay = "panelOutput.rightChDelay";
        public const string panelOutput_LeftChMute = "panelOutput.leftChMute";
        public const string panelOutput_RightChMute = "panelOutput.rightChMute";
        public const string panelOutput_LeftChPhase = "panelOutput.leftChPhase";
        public const string panelOutput_RightChPhase = "panelOutput.rightChPhase";
        public const string panelOutput_Delay = "panelOutput.delay";
        public const string panelOutput_Mute = "panelOutput.mute";
        public const string panelOutput_Phase = "panelOutput.phase";
        public const string panelOutput_Linkage = "panelOutput.linkage";
        public const string panelOutput_LeftCh = "panelOutput.leftCh";
        public const string panelOutput_RightCh = "panelOutput.rightCh";

        public const string panelSystem_VolMgt = "panelSystem.volMgt";
        public const string panelSystem_VolMgt_MusicStartVolume = "panelSystem.volMgt.musicStartVolume";
        public const string panelSystem_VolMgt_MusicMaxVolume = "panelSystem.volMgt.musicMaxVolume";
        public const string panelSystem_VolMgt_MicStartVolume = "panelSystem.volMgt.micStartVolume";
        public const string panelSystem_VolMgt_MicMaxVolume = "panelSystem.volMgt.micMaxVolume";
        public const string panelSystem_VolMgt_EffectStartVolume = "panelSystem.volMgt.effectStartVolume";
        public const string panelSystem_VolMgt_EffectMaxVolume = "panelSystem.volMgt.effectMaxVolume";
        public const string panelSystem_VolMgt_QuickSettings = "panelSystem.volMgt.quickSettings";

        public const string panelSystem_AutoLockKeyboard = "panelSystem.autoLockKeyboard";
        public const string panelSystem_Enabled = "panelSystem.enabled";
        public const string panelSystem_Disabled = "panelSystem.disabled";
        public const string panelSystem_SystemUser = "panelSystem.systemUser";
        public const string panelSystem_SystemPassword = "panelSystem.systemPassword";
        public const string panelSystem_OK = "panelSystem.ok";
        public const string panelSystem_NormalUser = "panelSystem.normalUser";
        public const string panelSystem_Admin = "panelSystem.admin";

        public const string panelSystem_Passowrd_ChangeKeyboardLockPassowrd = "panelSystem.passowrd.changeKeyboardLockPassowrd";
        public const string panelSystem_Passowrd_ChangeSystemUserPassowrd = "panelSystem.passowrd.changeSystemUserPassowrd";
        public const string panelSystem_Passowrd_OldPassowrd = "panelSystem.passowrd.oldPassowrd";
        public const string panelSystem_Passowrd_NewPassowrd = "panelSystem.passowrd.newPassowrd";
        public const string panelSystem_Passowrd_ConfirmPassowrd = "panelSystem.passowrd.confirmPassowrd";
        public const string panelSystem_Passowrd_InputError = "panelSystem.passowrd.inputError";
        public const string panelSystem_Passowrd_InvalidInput = "panelSystem.passowrd.invalidInput";
        public const string panelSystem_Passowrd_AskForChangeKeyboardPassword = "panelSystem.passowrd.askForChangeKeyboardPassword";
        public const string panelSystem_Passowrd_AskForChangeSystemPassword = "panelSystem.passowrd.askForChangeSystemPassword";

        public const string panelSystem_DataBackupMgt = "panelSystem.dataBackupMgt";
        public const string panelSystem_DataBackupMgt_Number = "panelSystem.dataBackupMgt.number";
        public const string panelSystem_DataBackupMgt_DataName = "panelSystem.dataBackupMgt.dataName";
        public const string panelSystem_DataBackupMgt_BackupFileName = "panelSystem.dataBackupMgt.backupFileName";
        public const string panelSystem_DataBackupMgt_Save = "panelSystem.dataBackupMgt.save";
        public const string panelSystem_DataBackupMgt_Load = "panelSystem.dataBackupMgt.load";
        public const string panelSystem_DataBackupMgt_LoadData = "panelSystem.dataBackupMgt.loadData";
        public const string panelSystem_DataBackupMgt_OpenFileLocation = "panelSystem.dataBackupMgt.openFileLocation";
        public const string panelSystem_DataBackupMgt_Delete = "panelSystem.dataBackupMgt.delete";
        public const string panelSystem_DataBackupMgt_FactoryMode = "panelSystem.dataBackupMgt.factoryMode";
        public const string panelSystem_DataBackupMgt_UserData = "panelSystem.dataBackupMgt.userData";
        public const string panelSystem_DataBackupMgt_UserDataInfo = "panelSystem.dataBackupMgt.userDataInfo";
        public const string panelSystem_DataBackupMgt_AskForSaveAndOverwriteFile = "panelSystem.dataBackupMgt.askForSaveAndOverwriteFile";
        public const string panelSystem_DataBackupMgt_FailedToLoadDataAndFileNotExist = "panelSystem.dataBackupMgt.failedToLoadDataAndFileNotExist";
        public const string panelSystem_DataBackupMgt_AskForLoadAndOverwriteData = "panelSystem.dataBackupMgt.askForLoadAndOverwriteData";
        public const string panelSystem_DataBackupMgt_AskForContinueLoadingData = "panelSystem.dataBackupMgt.askForContinueLoadingData";
        public const string panelSystem_DataBackupMgt_FailedToLoadDataAndFileDataError = "panelSystem.dataBackupMgt.failedToLoadDataAndFileDataError";
        public const string panelSystem_DataBackupMgt_FileNotExist = "panelSystem.dataBackupMgt.fileNotExist";
        public const string panelSystem_DataBackupMgt_DeleteFile = "panelSystem.dataBackupMgt.deleteFile";
        public const string panelSystem_DataBackupMgt_AskForDeleteDataFile = "panelSystem.dataBackupMgt.askForDeleteDataFile";
        public const string panelSystem_DataBackupMgt_FileTypeError = "panelSystem.dataBackupMgt.fileTypeError";
        public const string panelSystem_DataBackupMgt_FileDataError = "panelSystem.dataBackupMgt.fileDataError";
        public const string panelSystem_DataBackupMgt_FailedToCopyFile = "panelSystem.dataBackupMgt.failedToCopyFile";
        public const string panelSystem_DataBackupMgt_DataIsEncryptedAndCannotBeExported = "panelSystem.dataBackupMgt.dataIsEncryptedAndCannotBeExported";
        public const string panelSystem_DataBackupMgt_FailedToSaveFile = "panelSystem.dataBackupMgt.failedToSaveFile";
               
        public const string panelSystem_DeviceModeMgt = "panelSystem.deviceModeMgt";
        public const string panelSystem_DeviceModeMgt_DeviceMode = "panelSystem.deviceModeMgt.deviceMode";
        public const string panelSystem_DeviceModeMgt_CurrDeviceMode = "panelSystem.deviceModeMgt.currDeviceMode";
        public const string panelSystem_DeviceModeMgt_ModeName = "panelSystem.deviceModeMgt.modeName";
        public const string panelSystem_DeviceModeMgt_SaveMode = "panelSystem.deviceModeMgt.saveMode";
        public const string panelSystem_DeviceModeMgt_CallMode = "panelSystem.deviceModeMgt.callMode";
        public const string panelSystem_DeviceModeMgt_AskForCallModeData = "panelSystem.deviceModeMgt.askForCallModeData";
        public const string panelSystem_DeviceModeMgt_CannotSave = "panelSystem.deviceModeMgt.cannotSave";
        public const string panelSystem_DeviceModeMgt_AskForSaveModeData = "panelSystem.deviceModeMgt.askForSaveModeData";
        public const string panelSystem_DeviceModeMgt_AskForEncrpyData = "panelSystem.deviceModeMgt.askForEncrpyData";
        public const string panelSystem_DeviceModeMgt_EncrpyCurrModeData = "panelSystem.deviceModeMgt.encrpyCurrModeData";
        public const string panelSystem_DeviceModeMgt_FactoryPresetCannotBeSaved = "panelSystem.deviceModeMgt.factoryPresetCannotBeSaved";

        public const string panelSystem_InfraredRemoteControl = "panelSystem.infraredRemoteControl";
        public const string panelSystem_TimbreSelect = "panelSystem.timbreSelect";
        public const string panelSystem_TimbrePro = "panelSystem.timbrePro";
        public const string panelSystem_TimbreClassics = "panelSystem.timbreClassics";
        public const string panelSystem_DeviceWorkMode = "panelSystem.deviceWorkMode";
        public const string panelSystem_DeviceWorkMode_Standard = "panelSystem.deviceWorkMode.standard";
        public const string panelSystem_DeviceWorkMode_X5 = "panelSystem.deviceWorkMode.x5";
        public const string panelSystem_Clarity = "panelSystem.clarity";
        public const string panelSystem_Clarity_SDA = "panelSystem.clarity.SDA";
        public const string panelSystem_Clarity_SDB = "panelSystem.clarity.SDB";
        public const string panelSystem_Clarity_HDA = "panelSystem.clarity.HDA";
        public const string panelSystem_Clarity_HDB = "panelSystem.clarity.HDB";
        public const string panelSystem_Clarity_UHDA = "panelSystem.clarity.UHDA";
        public const string panelSystem_Clarity_UHDB = "panelSystem.clarity.UHDB";
        public const string panelSystem_Clarity_Note = "panelSystem.clarity.note";
        public const string panelSystem_DeviceInfo = "panelSystem.deviceInfo";
        public const string panelSystem_DeviceID = "panelSystem.deviceID";
        public const string panelSystem_DevicePrototype = "panelSystem.devicePrototype";
        public const string panelSystem_DeviceFirmwareVersion = "panelSystem.deviceFirmwareVersion";
        public const string panelSystem_DeviceFirmwareBuildNumber = "panelSystem.deviceFirmwareBuildNumber";

        public const string popMenu_ThresholdUnit = "popMenu.thresholdUnit";
        public const string popMenu_MixerLevelUnit = "popMenu.mixerLevelUnit";
        public const string popMenu_ChannelDelayUnit = "popMenu.channelDelayUnit";

        public const string formModeQuickOperation = "formModeQuickOperation";

        public const string panelSystemSelection = "panelSystemSelection";
        public const string panelSystemSelection_Loudspeaker = "panelSystemSelection.loudspeaker";
        public const string panelSystemSelection_PowerAmplifier = "panelSystemSelection.powerAmplifier";
        public const string panelSystemSelection_Microphone = "panelSystemSelection.microphone";
        public const string panelSystemSelection_Brand = "panelSystemSelection.brand";
        public const string panelSystemSelection_Model = "panelSystemSelection.model";
        public const string panelSystemSelection_Power = "panelSystemSelection.power";
        public const string panelSystemSelection_Size = "panelSystemSelection.size";
        public const string panelSystemSelection_Impedance = "panelSystemSelection.impedance";
        public const string panelSystemSelection_Sensitivity = "panelSystemSelection.sensitivity";
        public const string panelSystemSelection_Wired = "panelSystemSelection.wired";
        public const string panelSystemSelection_Wireless = "panelSystemSelection.wireless";
        public const string panelSystemSelection_RecommendedVolume = "panelSystemSelection.recommendedVolume";
        public const string panelSystemSelection_Clear = "panelSystemSelection.clear";
        public const string panelSystemSelection_All = "panelSystemSelection.all";
        public const string panelSystemSelection_BuildIn = "panelSystemSelection.buildIn";
        public const string panelSystemSelection_Customer = "panelSystemSelection.customer";
        public const string panelSystemSelection_SelectedItem = "panelSystemSelection.selectedItem";
        public const string panelSystemSelection_TheExactMatchCouldNotBeFound = "panelSystemSelection.theExactMatchCouldNotBeFound";

        public const string formExportPresetModeData = "formExportPresetModeData";
        public const string formExportPresetModeData_ModeFileName = "formExportPresetModeData.modeFileName";
        public const string formExportPresetModeData_ModeName = "formExportPresetModeData.modeName";
        public const string formExportPresetModeData_Export = "formExportPresetModeData.export";
        public const string formExportPresetModeData_OpenFileLocation = "formExportPresetModeData.openFileLocation"; 
        public const string formExportPresetModeData_CannotBeEmpty = "formExportPresetModeData.cannotBeEmpty";
        public const string formExportPresetModeData_LoudspeakerBrand = "formExportPresetModeData.loudspeakerBrand";
        public const string formExportPresetModeData_LoudspeakerModel = "formExportPresetModeData.loudspeakerModel";
        public const string formExportPresetModeData_PowerAmplifierBrand = "formExportPresetModeData.powerAmplifierBrand";
        public const string formExportPresetModeData_PowerAmplifierModel = "formExportPresetModeData.powerAmplifierModel";
        public const string formExportPresetModeData_PowerAmplifierSensitivity = "formExportPresetModeData.powerAmplifierSensitivity";
        public const string formExportPresetModeData_PowerAmplifierSensitivityInputError = "formExportPresetModeData.powerAmplifierSensitivityInputError";
        public const string formExportPresetModeData_MicrophoneBrand = "formExportPresetModeData.microphoneBrand";
        public const string formExportPresetModeData_MicrophoneModel = "formExportPresetModeData.microphoneModel";
        public const string formExportPresetModeData_WirelessMicrophoneRecommendedVolumeCannotBeZero = "formExportPresetModeData.wirelessMicrophoneRecommendedVolumeCannotBeZero";
        public const string formExportPresetModeData_SelectAtLeastOneModeFile = "formExportPresetModeData.selectAtLeastOneModeFile";
        public const string formExportPresetModeData_ModeFileNotExist = "formExportPresetModeData.modeFileNotExist";
        public const string formExportPresetModeData_AskForOverwriteFile = "formExportPresetModeData.askForOverwriteFile";
        public const string formExportPresetModeData_ExportPresetModeDataSuccessfully = "formExportPresetModeData.exportPresetModeDataSuccessfully";
        public const string formExportPresetModeData_FailedToExportPresetModeData = "formExportPresetModeData.failedToExportPresetModeData";

        public const string formCommSettings = "formCommSettings";
        public const string formCommSettings_AutoConnection = "formCommSettings.autoConnection";
        public const string formCommSettings_CommMethod = "formCommSettings.commMethod";
        public const string formCommSettings_SelectOneMethodAtLeast = "formCommSettings.selectOneMethodAtLeast";
        public const string formCommSettings_DefaultSerialPort = "formCommSettings.defaultSerialPort";
        public const string formCommSettings_CommRetransmission = "formCommSettings.commRetransmission";
        public const string formCommSettings_SerialPortManualMode = "formCommSettings.serialPortManualMode";
        public const string formCommSettings_On = "formCommSettings.on";
        public const string formCommSettings_Off = "formCommSettings.off";        
        public const string formCommSettings_AutoMode = "formCommSettings.autoMode";
        public const string formCommSettings_ManualMode = "formCommSettings.manualMode";
        public const string formCommSettings_IPAddress = "formCommSettings.ipAddress";
        public const string formCommSettings_Port = "formCommSettings.port";
        public const string formCommSettings_Help = "formCommSettings.help";
        public const string formCommSettings_RestoreToDefault = "formCommSettings.restoreToDefault";
        public const string formCommSettings_Close = "formCommSettings.close";

        public const string formRS232ToWifiConverter = "formRS232ToWifiConverter";
        public const string formRS232ToWifiConverter_DeviceIp = "formRS232ToWifiConverter.deviceIp";
        public const string formRS232ToWifiConverter_RefreshDevice = "formRS232ToWifiConverter.refreshDevice";
        public const string formRS232ToWifiConverter_DeviceInfo = "formRS232ToWifiConverter.deviceInfo";
        public const string formRS232ToWifiConverter_BaudRate = "formRS232ToWifiConverter.baudRate";
        public const string formRS232ToWifiConverter_Update = "formRS232ToWifiConverter.update";
        public const string formRS232ToWifiConverter_DeviceModel = "formRS232ToWifiConverter.deviceModel";
        public const string formRS232ToWifiConverter_DeviceFirmwareVersion = "formRS232ToWifiConverter.deviceFirmwareVersion";
        public const string formRS232ToWifiConverter_DeviceMacAddress = "formRS232ToWifiConverter.deviceMacAddress";
        public const string formRS232ToWifiConverter_DeviceSocketName = "formRS232ToWifiConverter.deviceSocketName";
        public const string formRS232ToWifiConverter_DeviceSocketProtocol = "formRS232ToWifiConverter.deviceSocketProtocol";
        public const string formRS232ToWifiConverter_DeviceSocketProtocolUnknown = "formRS232ToWifiConverter.deviceSocketProtocolUnknown";
        public const string formRS232ToWifiConverter_DeviceSocketPort = "formRS232ToWifiConverter.deviceSocketPort";
        public const string formRS232ToWifiConverter_DeviceUartDataBits = "formRS232ToWifiConverter.deviceUartDataBits";
        public const string formRS232ToWifiConverter_DeviceUartStopBits = "formRS232ToWifiConverter.deviceUartStopBits";
        public const string formRS232ToWifiConverter_DeviceUartParity = "formRS232ToWifiConverter.deviceUartParity";
        public const string formRS232ToWifiConverter_DeviceUartParityNone = "formRS232ToWifiConverter.deviceUartParityNone";
        public const string formRS232ToWifiConverter_DeviceUartParityOdd = "formRS232ToWifiConverter.deviceUartParityOdd";
        public const string formRS232ToWifiConverter_DeviceUartParityEven = "formRS232ToWifiConverter.deviceUartParityEven";
        public const string formRS232ToWifiConverter_DeviceUartParityOther = "formRS232ToWifiConverter.deviceUartParityOther";
        public const string formRS232ToWifiConverter_DeviceUserName = "formRS232ToWifiConverter.deviceUserName";
        public const string formRS232ToWifiConverter_DevicePassword = "formRS232ToWifiConverter.devicePassword";
        public const string formRS232ToWifiConverter_RestoreConfigToDefault = "formRS232ToWifiConverter.restoreConfigToDefault";
        public const string formRS232ToWifiConverter_BaudRateNotMatch = "formRS232ToWifiConverter.baudRateNotMatch";
        public const string formRS232ToWifiConverter_SocketNotMatch = "formRS232ToWifiConverter.socketNotMatch";
         
        public const string formPreferences = "formPreferences";
        public const string formPreferences_DeviceModelWhenStarts = "formPreferences.deviceModelWhenStarts";
        public const string formPreferences_UseLastConnectedDeviceModel = "formPreferences.useLastConnectedDeviceModel";

        public const string formOnlineHelp = "formOnlineHelp";
        public const string formOnlineHelp_Audo = "formOnlineHelp.auto";
        public const string formOnlineHelp_CommInterface = "formOnlineHelp.commInterface";
        public const string formOnlineHelp_Port = "formOnlineHelp.port";
        public const string formOnlineHelp_BaudRate = "formOnlineHelp.baudRate";
        public const string formOnlineHelp_IpAddress = "formOnlineHelp.ipAddress";
        public const string formOnlineHelp_Run = "formOnlineHelp.run";

        public const string formFirmwareUpdate = "formFirmwareUpdate";
        public const string formFirmwareUpdate_Port = "formFirmwareUpdate.port";
        public const string formFirmwareUpdate_Auto = "formFirmwareUpdate.auto";
        public const string formFirmwareUpdate_BaudRate = "formFirmwareUpdate.baudRate";
        public const string formFirmwareUpdate_Refresh = "formFirmwareUpdate.refresh";
        public const string formFirmwareUpdate_Key = "formFirmwareUpdate.key";
        public const string formFirmwareUpdate_File = "formFirmwareUpdate.file";
        public const string formFirmwareUpdate_Update = "formFirmwareUpdate.update";
        public const string formFirmwareUpdate_Info = "formFirmwareUpdate.info";
        public const string formFirmwareUpdate_Progress = "formFirmwareUpdate.progress";
        public const string formFirmwareUpdate_KeyDataError = "formFirmwareUpdate.keyDataError";
        public const string formFirmwareUpdate_GetFileHashError = "formFirmwareUpdate.getFileHashError";
        public const string formFirmwareUpdate_FileHash = "formFirmwareUpdate.fileHash";
        public const string formFirmwareUpdate_FirmwareVersion = "formFirmwareUpdate.firmwareVersion";
        public const string formFirmwareUpdate_BluetoothName = "formFirmwareUpdate.bluetoothName";
        public const string formFirmwareUpdate_Rename = "formFirmwareUpdate.rename";
        public const string formFirmwareUpdate_Output = "formFirmwareUpdate.output";

        public const string formActivateDevice = "formActivateDevice";
        public const string formActivateDevice_CommInterface = "formActivateDevice.commInterface";
        public const string formActivateDevice_Port = "formActivateDevice.port";
        public const string formActivateDevice_BaudRate = "formActivateDevice.baudRate";
        public const string formActivateDevice_IpAddress = "formActivateDevice.ipAddress";
        public const string formActivateDevice_Activate = "formActivateDevice.activate";


        public const string formActivation = "formActivation";
        public const string formActivation_MachineCode = "formActivation.machineCode";
        public const string formActivation_RequestCode = "formActivation.requestCode";
        public const string formActivation_ActivationCode = "formActivation.activationCode";
        public const string formActivation_WhetherToCancelTheLicense = "formActivation.whetherToCancelTheLicense";

        public const string formAbout = "formAbout";
        public const string formAbout_SupportModels = "formAbout.supportModels";

        public const string rta_FailedToStartCapturingAudio = "rta.FailedToStartCapturingAudio";
        public const string rta_FailedToStartAudioOut = "rta.FailedToStartAudioOut";

        public const string formRTAOptions = "formRTAOptions";
        public const string formRTAOptions_Spectrum = "formRTAOptions.spectrum";
        public const string formRTAOptions_TransferFunction = "formRTAOptions.transferFunction";
        public const string formRTAOptions_AudioIO = "formRTAOptions.audioIO";
        public const string formRTAOptions_AudioInDevice = "formRTAOptions.audioInDevice";
        public const string formRTAOptions_SampleRate = "formRTAOptions.sampleRate";
        public const string formRTAOptions_SampleBitDepth = "formRTAOptions.sampleBitDepth";
        public const string formRTAOptions_AudioOutDevice = "formRTAOptions.audioOutDevice";
        public const string formRTAOptions_RTAParams = "formRTAOptions.rtaParams";
        public const string formRTAOptions_FftSize = "formRTAOptions.fftSize";
        public const string formRTAOptions_WindowFunction = "formRTAOptions.windowFunction";
        public const string formRTAOptions_Average = "formRTAOptions.average";
        public const string formRTAOptions_Smooth = "formRTAOptions.smooth";
        public const string formRTAOptions_Scale = "formRTAOptions.scale";
        public const string formRTAOptions_SpectrumGraph = "formRTAOptions.spectrumGraph";
        public const string formRTAOptions_DoubleCh = "formRTAOptions.doubleCh";
        public const string formRTAOptions_LeftCh = "formRTAOptions.leftCh";
        public const string formRTAOptions_RightCh = "formRTAOptions.rightCh";
        public const string formRTAOptions_AmpOffset = "formRTAOptions.ampOffset";
        public const string formRTAOptions_FreqRespGraph = "formRTAOptions.freqRespGraph";
        public const string formRTAOptions_AmpAndPhaseResp = "formRTAOptions.ampAndPhaseResp";
        public const string formRTAOptions_AmpResp = "formRTAOptions.ampResp";
        public const string formRTAOptions_PhaseResp = "formRTAOptions.phaseResp";   
        public const string formRTAOptions_SignalLevel = "formRTAOptions.signalLevel";   
        public const string formRTAOptions_SignalLeftCh = "formRTAOptions.signalLeftCh";   
        public const string formRTAOptions_SignalRightCh = "formRTAOptions.signalRightCh";   
        public const string formRTAOptions_Delay = "formRTAOptions.delay";   
        public const string formRTAOptions_MeasDelay = "formRTAOptions.measDelay";   
        public const string formRTAOptions_MeasDelayResult = "formRTAOptions.measDelayResult";   
        public const string formRTAOptions_ApplyMeasResult = "formRTAOptions.applyMeasResult";   
        public const string formRTAOptions_DeclineMeasResult = "formRTAOptions.declineMeasResult";   
        public const string formRTAOptions_SignalGenerator = "formRTAOptions.signalGenerator";   
        public const string formRTAOptions_Signal = "formRTAOptions.signal";   
        public const string formRTAOptions_Level = "formRTAOptions.level";
        public const string formRTAOptions_SineFreq = "formRTAOptions.sineFreq";
        public const string formRTAOptions_Sine1Level = "formRTAOptions.sine1Level";
        public const string formRTAOptions_Sine1Freq = "formRTAOptions.sine1Freq";
        public const string formRTAOptions_Sine2Level = "formRTAOptions.sine2Level";
        public const string formRTAOptions_Sine2Freq = "formRTAOptions.sine2Freq";
        public const string formRTAOptions_RefTrace = "formRTAOptions.refTrace";
        public const string formRTAOptions_Capture = "formRTAOptions.capture";
        public const string formRTAOptions_Delete = "formRTAOptions.delete";
        public const string formRTAOptions_HannWindow = "formRTAOptions.hannWindow";
        public const string formRTAOptions_HammingWindow = "formRTAOptions.hammingWindow";
        public const string formRTAOptions_RectangleWindow = "formRTAOptions.rectangleWindow";
        public const string formRTAOptions_GaussianWindow = "formRTAOptions.gaussianWindow";
        public const string formRTAOptions_BlackmanWindow = "formRTAOptions.blackmanWindow";
        public const string formRTAOptions_BlackmanHarrisWindow = "formRTAOptions.blackmanHarrisWindow";
        public const string formRTAOptions_FlatTopWindow = "formRTAOptions.flatTopWindow";
        public const string formRTAOptions_AverageInf = "formRTAOptions.averageInf";
        public const string formRTAOptions_AverageFast = "formRTAOptions.averageFast";
        public const string formRTAOptions_AverageSlow = "formRTAOptions.averageSlow";
        public const string formRTAOptions_SmoothNone = "formRTAOptions.smoothNone";
        public const string formRTAOptions_Smooth3Pt = "formRTAOptions.smooth3Pt";
        public const string formRTAOptions_Smooth5Pt = "formRTAOptions.smooth5Pt";
        public const string formRTAOptions_Smooth7Pt = "formRTAOptions.smooth7Pt";
        public const string formRTAOptions_Smooth9Pt = "formRTAOptions.smooth9Pt";
        public const string formRTAOptions_PinkNoise = "formRTAOptions.pinkNoise";
        public const string formRTAOptions_PinkSweep = "formRTAOptions.pinkSweep";
        public const string formRTAOptions_Sine = "formRTAOptions.sine";
        public const string formRTAOptions_DualSine = "formRTAOptions.dualSine";

        public const string formComm = "formComm";
        public const string formComm_ConnectAndLoadData = "formComm.connectAndLoadData";
        public const string formComm_UploadData = "formComm.uploadData";
        public const string formComm_CallModeData = "formComm.callModeData";
        public const string formComm_DeviceIsNotActivated = "formComm.deviceIsNotActivated";
        public const string formComm_PleaseActivateTheDeviceFirst = "formComm.pleaseActivateTheDeviceFirst";
        public const string formComm_DeviceIsLocked = "formComm.deviceIsLocked";
        public const string formComm_PleaseInputPasswordToUnlockSystem = "formComm.pleaseInputPasswordToUnlockSystem";
        public const string formComm_InputAdminPassword = "formComm.inputAdminPassword";
        public const string formComm_InputPasswordError = "formComm.inputPasswordError";
        public const string formComm_Error = "formComm.error";
        public const string formComm_Password = "formComm.password";
        public const string formComm_Ok = "formComm.ok";
        public const string formComm_Close = "formComm.close";
        public const string formComm_Disconnect = "formComm.disconnect";

        public const string comm_NoUSBHIDAvailable = "comm.noUSBHIDAvailable";
        public const string comm_NoPortAvailable = "comm.noPortAvailable";
        public const string comm_Successful = "comm.successful";
        public const string comm_Failed = "comm.failed";
        public const string comm_OpenPort = "comm.openPort";
        public const string comm_InitUdpClient = "comm.initUdpClient";
        public const string comm_InitTcpClient = "comm.initTcpClient";
        public const string comm_SendConnectionRequest = "comm.sendConnectionRequest";
        public const string comm_CheckResponseSuccessful = "comm.checkResponseSuccessful";
        public const string comm_CheckResponseFailed = "comm.checkResponseFailed";
        public const string comm_LoadDeviceInfo = "comm.loadDeviceInfo";
        public const string comm_LoadXModuleData = "comm.loadXModuleData";        
        public const string comm_MusicModule = "comm.musicModule";
        public const string comm_MicModule = "comm.micModule";
        public const string comm_MicAModule = "comm.micAModule";
        public const string comm_MicBModule = "comm.micBModule";
        public const string comm_MusicDeqModule = "comm.musicDeqModule";
        public const string comm_MicDeqModule = "comm.micDeqModule";
        public const string comm_DEffectModule = "comm.dEffectModule";
        public const string comm_EchoModule = "comm.echoModule";
        public const string comm_ReverbModule = "comm.reverbModule";
        public const string comm_MainOutputModule = "comm.mainOutputModule";
        public const string comm_Aux1OutputModule = "comm.aux1OutputModule";
        public const string comm_Aux2OutputModule = "comm.aux2OutputModule";
        public const string comm_SurOutputModule = "comm.surOutputModule";
        public const string comm_LoadSystemXData = "comm.loadSystemXData";
        public const string comm_MusicStartVolume = "comm.musicStartVolume";
        public const string comm_MusicMaxVolume = "comm.musicMaxVolume";
        public const string comm_MicStartVolume = "comm.micStartVolume";
        public const string comm_MicMaxVolume = "comm.micMaxVolume";
        public const string comm_EffectStartVolume = "comm.effectStartVolume";
        public const string comm_EffectMaxVolume = "comm.effectMaxVolume";
        public const string comm_MusicVolume = "comm.musicVolume";
        public const string comm_MicVolume = "comm.micVolume";
        public const string comm_EffectVolume = "comm.effectVolume";
        public const string comm_Timbre = "comm.timbre";
        public const string comm_Clarity = "comm.clarity";
        public const string comm_DeviceWorkMode = "comm.deviceWorkMode";
        public const string comm_MusicMute = "comm.musicMute";
        public const string comm_MicMute = "comm.micMute";
        public const string comm_EffectMute = "comm.effectMute";
        public const string comm_MusicInputGain = "comm.musicInputGain";
        public const string comm_MusicInputPort = "comm.musicInputPort";
        public const string comm_SystemPassword = "comm.systemPassword";
        public const string comm_KeyboardLockPassword = "comm.keyboardLockPassword";
        public const string comm_User = "comm.user";
        public const string comm_IsLockKeyboard = "comm.isLockKeyboard";
        public const string comm_IsEnableRomoteControl = "comm.isEnableRomoteControl";
        public const string comm_DeviceModeSave = "comm.deviceModeSave";
        public const string comm_DeviceModeCall = "comm.deviceModeCall";
        public const string comm_DeviceId = "comm.deviceId";
        public const string comm_DeviceModel = "comm.deviceModel";
        public const string comm_PrototypeModel = "comm.prototypeModel";
        public const string comm_FirmwareVersion = "comm.firmwareVersion";
        public const string comm_FirmwareBuildNumbers = "comm.firmwareBuildNumbers";
        public const string comm_IsEnableCustomDeviceInfo = "comm.isEnableCustomDeviceInfo";
        public const string comm_CustomDeviceInfoData = "comm.customDeviceInfoData";
        public const string comm_DeviceHardwareState = "comm.deviceHardwareState";
        public const string comm_DeviceName = "comm.deviceName";
        public const string comm_UsageLimitLockData = "comm.usageLimitLock";
        public const string comm_LoadSystemModeXNameData = "comm.loadSystemModeXNameData";
        public const string comm_LoadSystemData = "comm.loadSystemData";
        public const string comm_UploadXModuleData = "comm.uploadXModuleData";
        public const string comm_UploadSystemXData = "comm.uploadSystemXData";
        public const string comm_FailedToLoadData = "comm.failedToLoadData";
        public const string comm_FailedToUploadData = "comm.failedToUploadData";
        public const string comm_FailedToCallModeData = "comm.failedToCallModeData";
        public const string comm_UpdateDeviceModeIndexData = "comm.updateDeviceModeIndexData";
        public const string comm_UpdateXModeData = "comm.updateXModeData";
        public const string comm_SaveXDataToYMode = "comm.saveXDataToYMode";
        public const string comm_InitializingUI = "comm.initializingUI";
        public const string comm_UnknownDevcieModelIdentity = "comm.unknownDeviceModelIdentity";
        public const string comm_UnsupportedDevcieModelIdentity = "comm.unsupportedDeviceModelIdentity";

        public const string formControlTheOtherParty = "formControlTheOtherParty";
        public const string formControlTheOtherParty_RemoteId = "formControlTheOtherParty.remoteId";
        public const string formControlTheOtherParty_RemoteVerificationCode = "formControlTheOtherParty.remoteVerificationCode";
        public const string formControlTheOtherParty_RemoteControlConnect = "formControlTheOtherParty.remoteControlConnect";
        public const string formControlTheOtherParty_RemoteControlConnecting = "formControlTheOtherParty.remoteControlConnecting";
        public const string formControlTheOtherParty_DisconnectRemoteControl = "formControlTheOtherParty.disconnectRemoteControl";
        public const string formControlTheOtherParty_DisconnectingRemoteControl = "formControlTheOtherParty.disconnectingRemoteControl";
        public const string formControlTheOtherParty_FailedToConnectToRemoteClient = "formControlTheOtherParty.failedToConnectToRemoteClient";
        public const string formControlTheOtherParty_RemoteControl = "formControlTheOtherParty.remoteControl";
        public const string formControlTheOtherParty_IdCodeError = "formControlTheOtherParty.idCodeError";
        public const string formControlTheOtherParty_VerificationCodeError = "formControlTheOtherParty.verificationCodeError";
        public const string formControlTheOtherParty_EnterNonLocalIdCode = "formControlTheOtherParty.enterNonLocalIdCode";

        public const string formControlledRemotely = "formControlledRemotely";
        public const string formControlledRemotely_LocalId = "formControlledRemotely.localId";
        public const string formControlledRemotely_LocalVerificationCode = "formControlledRemotely.localVerificationCode";
        public const string formControlledRemotely_DisconnectRemoteControl = "formControlledRemotely.disconnectRemoteControl";
        public const string formControlledRemotely_DisconnectingRemoteControl = "formControlledRemotely.disconnectingRemoteControl";
        public const string formControlledRemotely_AllowToBeControlled = "formControlledRemotely.allowToBeControlled";
        
        public const string formRCSettings = "formRCSettings";
        public const string formRCSettings_ServerIPAddress = "formRCSettings.serverIPAddress";
        public const string formRCSettings_ServerPort = "formRCSettings.serverPort";
        public const string formRCSettings_EnterCorrentIP = "formRCSettings.enterCorrentIP";
        public const string formRCSettings_EnterCorrentPort = "formRCSettings.enterCorrentPort";
        public const string formRCSettings_RestoreToDefault = "formRCSettings.restoreToDefault";
        public const string formRCSettings_Close = "formRCSettings.close";

        public const string panelNetworkConnectionStatus_NotConnectedToTheServer = "panelNetworkConnectionStatus.notConnectedToTheServer";       
        public const string panelNetworkConnectionStatus_ConnectingToTheServer = "panelNetworkConnectionStatus.connectingToTheServer";
        public const string panelNetworkConnectionStatus_SuccessfullyConnectedToTheServer = "panelNetworkConnectionStatus.successfullyConnectedToTheServer";
        public const string panelNetworkConnectionStatus_FailedToConnectToTheServer = "panelNetworkConnectionStatus.failedToConnectToTheServer";
        public const string panelNetworkConnectionStatus_DisconnectFromTheServer = "panelNetworkConnectionStatus.disconnectFromTheServer";
        public const string panelNetworkConnectionStatus_ConnectionToTheServerIsInterrupted = "panelNetworkConnectionStatus.connectionToTheServerIsInterrupted";
        public const string panelNetworkConnectionStatus_ReconnectToTheServer = "panelNetworkConnectionStatus.reconnectToTheServer";

        public const string remoteControl_ConnectResponse_Unknown = "remoteControl.connectResponse.unknown";
        public const string remoteControl_ConnectResponse_TargetNotOnline = "remoteControl.connectResponse.targetNotOnline";
        public const string remoteControl_ConnectResponse_VerificationCodeError = "remoteControl.connectResponse.verificationCodeError";
        public const string remoteControl_ConnectResponse_NotAllowedToBeControlled = "remoteControl.connectResponse.notAllowedToBeControlled";
        public const string remoteControl_ConnectResponse_RemoteClientBusy = "remoteControl.connectResponse.remoteClientBusy";

        public const string remoteControl_RemoteClientDisconnect = "remoteControl.remoteClientDisconnect";
        public const string remoteControl_ConnectionIsInterrupted = "remoteControl.connectionIsInterrupted";

        public const string formEasyMode_ImportFromFile = "formEasyMode.importFromFile";
        public const string formEasyMode_Skip = "formEasyMode.skip";        
        public const string formEasyMode_ImportData = "formEasyMode.importData";
        public const string formEasyMode_Back = "formEasyMode.back";
        public const string formEasyMode_Save = "formEasyMode.save";
        public const string formEasyMode_FileDataError = "formEasyMode.fileDataError";
        
        public const string formViewImportAudioSystem = "formViewImportAudioSystem";
        public const string formViewImportAudioSystem_DataAdaptedToAudioSystem = "formViewImportAudioSystem.dataAdaptedToAudioSystem";
        public const string formViewImportAudioSystem_AudioSystem = "formViewImportAudioSystem.audioSystem";
        public const string formViewImportAudioSystem_Modes = "formViewImportAudioSystem.modes";
        public const string formViewImportAudioSystem_AskForImportMode = "formViewImportAudioSystem.askForImportMode";

        public const string easyModeControlPanel_SystemStatus = "easyModeControlPanel.systemStatus";
        public const string easyModeControlPanel_CurrentVolume = "easyModeControlPanel.currentVolume";
        public const string easyModeControlPanel_StartVolume = "easyModeControlPanel.startVolume";
        public const string easyModeControlPanel_LimitVolume = "easyModeControlPanel.limitVolume";
        public const string easyModeControlPanel_Mute = "easyModeControlPanel.mute";
        public const string easyModeControlPanel_Preset = "easyModeControlPanel.preset";
        public const string easyModeControlPanel_Other = "easyModeControlPanel.other";
        public const string easyModeControlPanel_ModeX = "easyModeControlPanel.modex";
        public const string easyModeControlPanel_Empty = "easyModeControlPanel.empty";

        public const string licenseType_Unauthorized = "licenseType.unauthorized";
        public const string licenseType_Temporary = "licenseType.temporary";
        public const string licenseType_Limited = "licenseType.limited";
        public const string licenseType_Unlimited = "licenseType.unlimited";

        public const string firmwareUpdate_Yes = "firmwareUpdate.yes";
        public const string firmwareUpdate_No = "firmwareUpdate.no";
        public const string firmwareUpdate_DataIsInvalid = "firmwareUpdate.dataIsInvalid";
        public const string firmwareUpdate_FirmwareType = "firmwareUpdate.firmwareType";
        public const string firmwareUpdate_WhetherToUpdate = "firmwareUpdate.whetherToUpdate";
        public const string firmwareUpdate_DataLength = "firmwareUpdate.dataLength";
        public const string firmwareUpdate_UpdateTime = "firmwareUpdate.updateTime";
        public const string firmwareUpdate_DataHash = "firmwareUpdate.dataHash";
        public const string firmwareUpdate_WhetherToUpdateFlashAllData = "firmwareUpdate.whetherToUpdateFlashAllData";
        public const string firmwareUpdate_FlashAllData = "firmwareUpdate.flashAllData";
        public const string firmwareUpdate_WhetherToUpdateCustomDeviceInfo = "firmwareUpdate.whetherToUpdateCustomDeviceInfo";
        public const string firmwareUpdate_CustomDeviceInfo = "firmwareUpdate.customDeviceInfo";
        public const string firmwareUpdate_WhetherToUpdatePresetData = "firmwareUpdate.whetherToUpdatePresetData";
        public const string firmwareUpdate_PresetData = "firmwareUpdate.presetData";
        public const string firmwareUpdate_UpdatedSuccessfully = "firmwareUpdate.updatedSuccessfully";
        public const string firmwareUpdate_FailedToUpdate = "firmwareUpdate.failedToUpdate";

        public const string usageLimitLock = "usageLimitLock";
        public const string usageLimitLock_Management = "usageLimitLock.management";
        public const string usageLimitLock_Management_NotSupport = "usageLimitLock.management.notSupport";
        public const string usageLimitLock_DeviceID = "usageLimitLock.deviceId";
        public const string usageLimitLock_DeviceName = "usageLimitLock.deviceName";
        public const string usageLimitLock_RandKey = "usageLimitLock.randKey";
        public const string usageLimitLock_PasswordManagement = "usageLimitLock.passwordManagement";
        public const string usageLimitLock_LockTaskManagement = "usageLimitLock.lockTaskManagement";
        public const string usageLimitLock_ForgotPassword = "usageLimitLock.forgotPassword";
        public const string usageLimitLock_TaskFilePath = "usageLimitLock.taskFilePath";
        public const string usageLimitLock_Setting = "usageLimitLock.setting";
        public const string usageLimitLock_Copy = "usageLimitLock.copy";
        public const string usageLimitLock_Clear = "usageLimitLock.clear";
        public const string usageLimitLock_Import = "usageLimitLock.import";
        public const string usageLimitLock_Export = "usageLimitLock.export";
        public const string usageLimitLock_CurrentState = "usageLimitLock.currentState";
        public const string usageLimitLock_RemainderUsage = "usageLimitLock.remainderUsage";
        public const string usageLimitLock_CurrentPassword = "usageLimitLock.currentPassword";
        public const string usageLimitLock_OK = "usageLimitLock.ok";
        public const string usageLimitLock_NewPassword = "usageLimitLock.newPassword";
        public const string usageLimitLock_ConfirmNewPassword = "usageLimitLock.confirmNewPassword";
        public const string usageLimitLock_UseCurrentPassword = "usageLimitLock.useCurrentPassword";
        public const string usageLimitLock_State = "usageLimitLock.state";
        public const string usageLimitLock_UsageCount = "usageLimitLock.usageCount";
        public const string usageLimitLock_NewPasswordLengthLessThan6 = "usageLimitLock.newPasswordLengthLessThan6";
        public const string usageLimitLock_ConfirmNewPasswordLengthLessThan6 = "usageLimitLock.confirmNewPasswordLengthLessThan6";
        public const string usageLimitLock_PasswordEnteredInconsistent = "usageLimitLock.passwordEnteredInconsistent";
        public const string usageLimitLock_UsageCountEnteredIncorrectly = "usageLimitLock.usageCountEnteredIncorrectly";
        public const string usageLimitLock_UsageCountEnteredExceedsMaximum = "usageLimitLock.usageCountEnteredExceedsMaximum";
        public const string usageLimitLock_PasswordsMustBeNumeric = "usageLimitLock.PasswordsMustBeNumeric";
        public const string usageLimitLock_Random = "usageLimitLock.random";
        public const string usageLimitLock_LockXPassword = "usageLimitLock.lockXPassword";
        public const string usageLimitLock_LockX = "usageLimitLock.lockX";
        public const string usageLimitLock_OldPassword = "usageLimitLock.oldPassword";
        public const string usageLimitLock_IsUpdate = "usageLimitLock.isUpdate";
        public const string usageLimitLock_IsEnable = "usageLimitLock.isEnable";
        public const string usageLimitLock_LockXIsUpdated = "usageLimitLock.lockXIsUpdated";
        public const string usageLimitLock_LockToString = "usageLimitLock.lockToString";
        public const string usageLimitLock_TaskFileXNotExist = "usageLimitLock.taskFileXNotExist";
        public const string usageLimitLock_NoLockWillToBeUpdated = "usageLimitLock.noLockWillToBeUpdated";
        public const string usageLimitLock_LockXPasswordNotMatch = "usageLimitLock.lockXPasswordNotMatch";
        public const string usageLimitLock_TheFollowingLocksWillToBeUpdated = "usageLimitLock.theFollowingLocksWillToBeUpdated";
        public const string usageLimitLock_LockTaskToString = "usageLimitLock.lockTaskToString";
        public const string usageLimitLock_Enable = "usageLimitLock.enable";
        public const string usageLimitLock_Disable = "usageLimitLock.disable";

        public const string FormUnlockOutput = "FormUnlockOutput";
        public const string FormUnlockOutput_Password = "FormUnlockOutput.password";
        public const string FormUnlockOutput_Unlock = "FormUnlockOutput.unlock";
        public const string FormUnlockOutput_PasswordIncorrect = "FormUnlockOutput.passwordIncorrect ";

        public Localization()
        {
            InitStringMap();
            InitLangCode();
        }

        private void InitStringMap()
        {
            stringMap[formEasyMode] = "易调模式";
            stringMap[formProMode] = "专业模式";
            stringMap[formMain_RemoteControl] = "远程控制";
            stringMap[formMain_ControlTheOtherParty] = "远程控制对方";
            stringMap[formMain_ControlledRemotelyByTheOtherParty] = "请求对方远程控制本机";
            stringMap[formMain_VideoTutorial] = "视频教学";
            stringMap[formMain_OpenVideoTutorial] = "打开在线视频教程";
            stringMap[formMain_ShowTutorialQRCode] = "显示教程二维码";
            stringMap[formMain_ScanQRCode] = "扫一扫打开在线视频教程";

            stringMap[ok] = "确定";
            stringMap[cancel] = "取消";
            stringMap[buttonConnection_Connect] = "联机";
            stringMap[buttonConnection_Disconnect] = "断开";
            stringMap[systemIsBusyAndTryAgain] = "系统忙碌中,请稍后重试！";
            stringMap[commInterrupted] = "通讯中断！";
            stringMap[musicVol] = "音乐";
            stringMap[micVol] = "话筒";
            stringMap[effectVol] = "效果";
            stringMap[timbre] = "音色";

            stringMap[formProMode] = "专业模式";
            stringMap[formProMode_Music] = "音乐";
            stringMap[formProMode_MusicDeq] = "音乐Deq";
            stringMap[formProMode_Mic] = "麦克风";
            stringMap[formProMode_MicA] = "麦克风A";
            stringMap[formProMode_MicB] = "麦克风B";
            stringMap[formProMode_MicAPlusB] = "麦克风A+B";
            stringMap[formProMode_MicDeq] = "麦克风Deq";
            stringMap[formProMode_MicExciter] = "麦克风增强";
            stringMap[formProMode_Effect] = "效果";
            stringMap[formProMode_Echo] = "回声";
            stringMap[formProMode_Reverb] = "混响";
            stringMap[formProMode_MainOutput] = "主输出";
            stringMap[formProMode_Aux1Output] = "辅助输出1";
            stringMap[formProMode_Aux2Output] = "辅助输出2";
            stringMap[formProMode_CenterOutput] = "中置输出";
            stringMap[formProMode_SubOutput] = "超低输出";
            stringMap[formProMode_SurOutput] = "后置输出";
            stringMap[formProMode_System] = "系统";
            stringMap[formProMode_Monitor] = "监视";

            stringMap[formProMode_DeviceModels] = "设备型号";
            stringMap[formProMode_RTAOptions] = "RTA 选项";
            stringMap[formProMode_CommSettings] = "通讯设置";
            stringMap[formProMode_Preferences] = "首选项";
            stringMap[formProMode_Language] = "语言/Language";
            stringMap[formProMode_Help] = "帮助";
            stringMap[formProMode_ViewHelp] = "查看帮助";
            stringMap[formProMode_OnlineHelp] = "联机帮助";
            stringMap[formProMode_BluetoothRename] = "蓝牙重命名";
            stringMap[formProMode_FirmwareUpdate] = "固件升级";
            stringMap[formProMode_ActivateDevice] = "激活设备";
            stringMap[formProMode_OpenLogFileLocation] = "打开日志文件目录";
            stringMap[formProMode_About] = "关于";

            stringMap[eqGroupPanel_XBandEq] = "{0} 段EQ";
            stringMap[eqGroupPanel_XBandEchoEq] = "{0} 段回声EQ";
            stringMap[eq_Type] = "类型";
            stringMap[eq_Gain] = "增益";
            stringMap[eq_Freq] = "频率";
            stringMap[eq_Q] = "Q值";

            stringMap[deq_CM] = "模式";
            stringMap[deq_AmpSen] = "功放灵敏度";
            stringMap[deq_Threshold] = "启动电平";
            stringMap[deq_Attack] = "启动时间";
            stringMap[deq_Release] = "恢复时间";
            stringMap[deq_Ratio] = "动态比例";
            stringMap[deq_DM] = "动态模式";

            stringMap[fastMode] = "快速";
            stringMap[mediumMode] = "中速";
            stringMap[slowMode] = "慢速";

            stringMap[compressorPanel_Threshold] = "压缩启动电平";
            stringMap[compressorPanel_Attack] = "压缩启动时间";
            stringMap[compressorPanel_Release] = "压缩恢复时间";
            stringMap[compressorPanel_Ratio] = "压缩比例";

            stringMap[x5_compAndLimiterPanel_Threshold] = "启动电平";
            stringMap[x5_compAndLimiterPanel_Attack] = "启动时间";
            stringMap[x5_compAndLimiterPanel_Release] = "释放时间";
            stringMap[x5_compAndLimiterPanel_Ratio] = "压限比例";

            stringMap[limiterPanel] = "限幅器";
            stringMap[limiterPanel_Threshold] = "启动电平";
            stringMap[limiterPanel_Attack] = "启动时间";
            stringMap[limiterPanel_Release] = "恢复时间";
            stringMap[limiterPanel_ClipThreshold] = "削波启动电平";
            stringMap[limiterPanel_ClipRelease] = "削波恢复时间";
            stringMap[limiterPanel_Ratio] = "压限比例";
            stringMap[limiterPanel_Mode] = "压限模式";

            stringMap[eqReset] = "EQ重置";
            stringMap[autoMode] = "自动";
            stringMap[manualMode] = "手动";
            stringMap[singMode] = "唱歌";
            stringMap[discoMode] = "热舞";
            stringMap[eqReset_Tips] = "此操作将全部EQ恢复至默认参数，是否继续？";
            stringMap[deqReset] = "DEQ重置";
            stringMap[deqReset_Tips] = "此操作将重置DEQ参数，是否继续？";

            stringMap[exciterPanel_Freq] = "频率";
            stringMap[exciterPanel_XoverFreq] = "分频点";
            stringMap[exciterPanel_BandWidth] = "带宽";
            stringMap[exciterPanel_FBGain] = "深度";
            stringMap[exciterPanel_BoostLevel] = "电平";
            stringMap[exciterPanel_BassExciter] = "低音";
            stringMap[exciterPanel_Exciter] = "频段";
            stringMap[exciterPanel_MicExciter1] = "麦克风电子管1";
            stringMap[exciterPanel_MicExciter2] = "麦克风电子管2";

            stringMap[panelMusic_MusicInput] = "音乐输入";
            stringMap[panelMusic_MusicInputPort] = "音乐输入端口";
            stringMap[panelMusic_MusicInputGain] = "音乐输入增益";
            stringMap[panelMusic_MusicInputPort3Gain] = "端口 3 输入增益";
            stringMap[panelMusic_Port] = "端口";
            stringMap[panelMusic_BT] = "蓝牙";
            stringMap[panelMusic_SPDIF] = "光纤";
            stringMap[panelMusic_FiberPort] = "光纤端口为自动识别";
            stringMap[panelMusic_MusicHpf] = "音乐高通";
            stringMap[panelMusic_MusicHpfType] = "滤波器类型";
            stringMap[panelMusic_MusicToMainOutHpf] = "音乐至主输出高通";
            stringMap[panelMusic_MusicToMainOutHpfSlope] = "滤波器斜率";
            stringMap[panelMusic_SuperBass] = "电子管低频";

            stringMap[panelMic_MicInput] = "麦克风输入";
            stringMap[panelMic_MicInput_AB_MIXED] = "A+B 混合";
            stringMap[panelMic_MicInput_SINGLE_A] = "单端 A";
            stringMap[panelMic_MicInput_SINGLE_B] = "单端 B";
            stringMap[panelMic_MicFBE] = "麦克风移频";
            stringMap[panelMic_MicNoiseGate] = "噪声门";
            stringMap[panelMic_MicHpf] = "麦克风高通";
            stringMap[panelMic_MicLpf] = "麦克风低通";
            stringMap[panelMic_MicLpfType] = "低通类型";
            stringMap[panelMic_MicLevel] = "麦克风电平";
            stringMap[panelMic_EffectLevel] = "效果电平";
            stringMap[panelMic_MicABLink] = "麦克风 A+B 联调";
            stringMap[panelMic_MicABLink_Tips] = "联动操作会将麦克风 {0} 的数据同步到麦克风 {1}，是否继续？";

            stringMap[panelEffect_Echo_EchoLevel] = "回声效果电平";
            stringMap[panelEffect_Echo_EchoDirectLevel] = "回声直达电平";
            stringMap[panelEffect_Echo_Hpf] = "回声高通";
            stringMap[panelEffect_Echo_HpfType] = "回声高通斜率";
            stringMap[panelEffect_Echo_Lpf] = "回声低通";
            stringMap[panelEffect_Echo_LpfQ] = "回声低通斜率";
            stringMap[panelEffect_Echo_LpfType] = "回声低通斜率";
            stringMap[panelEffect_Echo_PreDelay] = "回声预延时";
            stringMap[panelEffect_Echo_Delay] = "回声延时";
            stringMap[panelEffect_Echo_Repeat] = "回声重复比例";
            stringMap[panelEffect_Echo_RChPreDelay] = "回声右通道预延时";
            stringMap[panelEffect_Echo_RChDelay] = "回声右通道延时";

            stringMap[panelEffect_Reverb_ReverbLevel] = "Mic混响效果电平";
            stringMap[panelEffect_Reverb_ReverbDirectLevel] = "Mic混响直达电平";
            stringMap[panelEffect_Reverb_Hpf] = "混响高通";
            stringMap[panelEffect_Reverb_HpfType] = "混响高通斜率";
            stringMap[panelEffect_Reverb_Lpf] = "混响低通";
            stringMap[panelEffect_Reverb_LpfType] = "混响低通斜率";
            stringMap[panelEffect_Reverb_MicHpf] = "混响麦克风高通";
            stringMap[panelEffect_Reverb_MicLpf] = "混响麦克风低通";
            stringMap[panelEffect_Reverb_MusicHpf] = "混响音乐高通";
            stringMap[panelEffect_Reverb_MusicLpf] = "混响音乐低通";
            stringMap[panelEffect_Reverb_PreDelay] = "混响预延时";
            stringMap[panelEffect_Reverb_Delay] = "混响延时";
            stringMap[panelEffect_Reverb_BassDamp] = "混响低音衰减";
            stringMap[panelEffect_Reverb_TrebleDamp] = "混响高音衰减";
            stringMap[panelEffect_Reverb_MusicReverbLevel] = "音乐至混响";

            stringMap[panelEffect_DEffect_Threshold] = "启动电平";
            stringMap[panelEffect_DEffect_Gain] = "动态增益";

            stringMap[panelOutput_HpfFreq] = "高通";
            stringMap[panelOutput_HpfQ] = "高通增益";
            stringMap[panelOutput_HpfMode] = "高通滤波器模式";
            stringMap[panelOutput_LpfFreq] = "低通";
            stringMap[panelOutput_LpfQ] = "低通Q值";
            stringMap[panelOutput_LpfMode] = "低通滤波器模式";
            stringMap[panelOutput_LeftChDelay] = "左通道延时";
            stringMap[panelOutput_RightChDelay] = "右通道延时";
            stringMap[panelOutput_LeftChMute] = "左通道静音";
            stringMap[panelOutput_RightChMute] = "右通道静音";
            stringMap[panelOutput_LeftChPhase] = "左通道反相";
            stringMap[panelOutput_RightChPhase] = "右通道反相";
            stringMap[panelOutput_Delay] = "延时";
            stringMap[panelOutput_Mute] = "静音";
            stringMap[panelOutput_Phase] = "反相";
            stringMap[panelOutput_Linkage] = "联调";
            stringMap[panelOutput_LeftCh] = "左";
            stringMap[panelOutput_RightCh] = "右";

            stringMap[panelSystem_VolMgt] = "音量管理";
            stringMap[panelSystem_VolMgt_MusicStartVolume] = "音乐起始音量";
            stringMap[panelSystem_VolMgt_MusicMaxVolume] = "音乐最大音量";
            stringMap[panelSystem_VolMgt_MicStartVolume] = "话筒起始音量";
            stringMap[panelSystem_VolMgt_MicMaxVolume] = "话筒最大音量";
            stringMap[panelSystem_VolMgt_EffectStartVolume] = "效果起始音量";
            stringMap[panelSystem_VolMgt_EffectMaxVolume] = "效果最大音量";
            stringMap[panelSystem_VolMgt_QuickSettings] = "快捷设置";

            stringMap[panelSystem_AutoLockKeyboard] = "自动键盘锁";
            stringMap[panelSystem_Enabled] = "开启";
            stringMap[panelSystem_Disabled] = "禁用";

            stringMap[panelSystem_SystemUser] = "系统用户设置";
            stringMap[panelSystem_SystemPassword] = "系统权限密码";
            stringMap[panelSystem_OK] = "确定";
            stringMap[panelSystem_NormalUser] = "用户";
            stringMap[panelSystem_Admin] = "管理员";

            stringMap[panelSystem_Passowrd_ChangeKeyboardLockPassowrd] = "修改键盘锁密码";
            stringMap[panelSystem_Passowrd_ChangeSystemUserPassowrd] = "修改系统密码";
            stringMap[panelSystem_Passowrd_OldPassowrd] = "旧密码";
            stringMap[panelSystem_Passowrd_NewPassowrd] = "新密码";
            stringMap[panelSystem_Passowrd_ConfirmPassowrd] = "确认新密码";
            stringMap[panelSystem_Passowrd_InputError] = "输入有误";
            stringMap[panelSystem_Passowrd_InvalidInput] = "无效输入 ！";
            stringMap[panelSystem_Passowrd_AskForChangeKeyboardPassword] = "是否修改键盘锁密码 ？";
            stringMap[panelSystem_Passowrd_AskForChangeSystemPassword] = "是否修改管理员密码 ？";

            stringMap[panelSystem_DataBackupMgt] = "数据备份管理";
            stringMap[panelSystem_DataBackupMgt_Number] = "序号";
            stringMap[panelSystem_DataBackupMgt_DataName] = "数据名称";
            stringMap[panelSystem_DataBackupMgt_BackupFileName] = "备份文件名";
            stringMap[panelSystem_DataBackupMgt_Save] = "保存数据到本地文件";
            stringMap[panelSystem_DataBackupMgt_Load] = "从本地文件加载数据";
            stringMap[panelSystem_DataBackupMgt_LoadData] = "加载数据";
            stringMap[panelSystem_DataBackupMgt_OpenFileLocation] = "打开所在目录";
            stringMap[panelSystem_DataBackupMgt_Delete] = "删除";
            stringMap[panelSystem_DataBackupMgt_FactoryMode] = "出厂模式";
            stringMap[panelSystem_DataBackupMgt_UserData] = "用户数据";
            stringMap[panelSystem_DataBackupMgt_UserDataInfo] = "{0}型号：{1}路径：{2}修改日期：{3}";
            stringMap[panelSystem_DataBackupMgt_AskForSaveAndOverwriteFile] = "模式文件 {0} 已经存在，是否覆盖现有文件 ？";
            stringMap[panelSystem_DataBackupMgt_FailedToLoadDataAndFileNotExist] = "加载数据失败，文件不存在！";
            stringMap[panelSystem_DataBackupMgt_AskForLoadAndOverwriteData] = "加载 {0} 数据将会覆盖当前数据，是否继续 ？";
            stringMap[panelSystem_DataBackupMgt_AskForContinueLoadingData] = "系统未联机，是否继续加载 {0} 数据 ？";
            stringMap[panelSystem_DataBackupMgt_FailedToLoadDataAndFileDataError] = "加载数据失败，文件数据错误！";
            stringMap[panelSystem_DataBackupMgt_FileNotExist] = "文件 {0} 不存在，可能已经被移除，是否从列表中删除 ？";
            stringMap[panelSystem_DataBackupMgt_DeleteFile] = "删除文件";
            stringMap[panelSystem_DataBackupMgt_AskForDeleteDataFile] = "确实要删除此数据文件吗？{0}";
            stringMap[panelSystem_DataBackupMgt_FileTypeError] = "文件类型错误 ！";
            stringMap[panelSystem_DataBackupMgt_FileDataError] = "文件数据错误 ！";
            stringMap[panelSystem_DataBackupMgt_FailedToCopyFile] = "拷贝文件失败：({0}) ！";
            stringMap[panelSystem_DataBackupMgt_DataIsEncryptedAndCannotBeExported] = "数据已经加密，无法导出！";
            stringMap[panelSystem_DataBackupMgt_FailedToSaveFile] = "保存文件失败：({0}) ！";
            
            stringMap[panelSystem_DeviceModeMgt] = "设备模式管理";
            stringMap[panelSystem_DeviceModeMgt_DeviceMode] = "设备模式";
            stringMap[panelSystem_DeviceModeMgt_CurrDeviceMode] = "当前模式：";
            stringMap[panelSystem_DeviceModeMgt_ModeName] = "模式名称";
            stringMap[panelSystem_DeviceModeMgt_SaveMode] = "保存设备模式";
            stringMap[panelSystem_DeviceModeMgt_CallMode] = "调用设备模式";
            stringMap[panelSystem_DeviceModeMgt_AskForCallModeData] = "是否调用模式{0}: {1} ?";
            stringMap[panelSystem_DeviceModeMgt_CannotSave] = "当前用户无法执行保存操作！";
            stringMap[panelSystem_DeviceModeMgt_AskForSaveModeData] = "是否保存 模式{0}: {1} ?";
            stringMap[panelSystem_DeviceModeMgt_AskForEncrpyData] = "建议先备份数据，此加密操作不可撤销，是否继续 ?";
            stringMap[panelSystem_DeviceModeMgt_EncrpyCurrModeData] = "加密当前模式数据";
            stringMap[panelSystem_DeviceModeMgt_FactoryPresetCannotBeSaved] = "出厂模式不能保存";

            stringMap[panelSystem_InfraredRemoteControl] = "红外遥控管理";
            stringMap[panelSystem_TimbreSelect] = "音色选择";
            stringMap[panelSystem_TimbrePro] = "专业";
            stringMap[panelSystem_TimbreClassics] = "经典";
            stringMap[panelSystem_DeviceWorkMode] = "设备工作模式";
            stringMap[panelSystem_DeviceWorkMode_Standard] = "标准模式";
            stringMap[panelSystem_DeviceWorkMode_X5] = "X5模式";
            stringMap[panelSystem_TimbreSelect] = "音色选择";
            stringMap[panelSystem_Clarity] = "清晰度";
            stringMap[panelSystem_Clarity_SDA] = "标准A";
            stringMap[panelSystem_Clarity_SDB] = "标准B";
            stringMap[panelSystem_Clarity_HDA] = "高清A";
            stringMap[panelSystem_Clarity_HDB] = "高清B";
            stringMap[panelSystem_Clarity_UHDA] = "超高清A";
            stringMap[panelSystem_Clarity_UHDB] = "超高清B";
            stringMap[panelSystem_Clarity_Note] = "选择清晰度档位 A 时，其超高音频段要比选择清晰度档位 B 时更丰富。";
            stringMap[panelSystem_DeviceInfo] = "设备信息";
            stringMap[panelSystem_DeviceID] = "设备ID：";
            stringMap[panelSystem_DevicePrototype] = "原型";
            stringMap[panelSystem_DeviceFirmwareVersion] = "固件版本";
            stringMap[panelSystem_DeviceFirmwareBuildNumber] = "构建号";

            stringMap[popMenu_ThresholdUnit] = "启动电平单位：{0}";
            stringMap[popMenu_MixerLevelUnit] = "混音电平单位：{0}";
            stringMap[popMenu_ChannelDelayUnit] = "输出延时单位：{0}";

            stringMap[formModeQuickOperation] = "模式快捷操作";

            stringMap[panelSystemSelection] = "系统选择";
            stringMap[panelSystemSelection_Loudspeaker] = "音箱";
            stringMap[panelSystemSelection_PowerAmplifier] = "功放";
            stringMap[panelSystemSelection_Microphone] = "麦克风";
            stringMap[panelSystemSelection_Brand] = "品牌";
            stringMap[panelSystemSelection_Model] = "型号";
            stringMap[panelSystemSelection_Power] = "功率";
            stringMap[panelSystemSelection_Size] = "尺寸";
            stringMap[panelSystemSelection_Impedance] = "阻抗";
            stringMap[panelSystemSelection_Sensitivity] = "灵敏度";
            stringMap[panelSystemSelection_Wired] = "有线";
            stringMap[panelSystemSelection_Wireless] = "无线";
            stringMap[panelSystemSelection_RecommendedVolume] = "建议音量";
            stringMap[panelSystemSelection_Clear] = "清除";
            stringMap[panelSystemSelection_All] = "全部";
            stringMap[panelSystemSelection_BuildIn] = "内置";
            stringMap[panelSystemSelection_Customer] = "外部";
            stringMap[panelSystemSelection_SelectedItem] = "选中项";
            stringMap[panelSystemSelection_TheExactMatchCouldNotBeFound] = "找不到完全匹配项";

            stringMap[formExportPresetModeData] = "导出预设模式数据";            
            stringMap[formExportPresetModeData_ModeFileName] = "模式文件名";
            stringMap[formExportPresetModeData_ModeName] = "模式名";
            stringMap[formExportPresetModeData_Export] = "导出";
            stringMap[formExportPresetModeData_OpenFileLocation] = "打开所在目录";
            stringMap[formExportPresetModeData_CannotBeEmpty] = "不能为空！";
            stringMap[formExportPresetModeData_LoudspeakerBrand] = "音箱品牌";
            stringMap[formExportPresetModeData_LoudspeakerModel] = "音箱型号";
            stringMap[formExportPresetModeData_PowerAmplifierBrand] = "功放品牌";
            stringMap[formExportPresetModeData_PowerAmplifierModel] = "功放型号";
            stringMap[formExportPresetModeData_PowerAmplifierSensitivity] = "功放灵敏度";
            stringMap[formExportPresetModeData_PowerAmplifierSensitivityInputError] = "功放灵敏度输入有误！";
            stringMap[formExportPresetModeData_MicrophoneBrand] = "麦克风品牌";
            stringMap[formExportPresetModeData_MicrophoneModel] = "麦克风型号";
            stringMap[formExportPresetModeData_WirelessMicrophoneRecommendedVolumeCannotBeZero] = "无线麦克风建议音量不能为0！";
            stringMap[formExportPresetModeData_SelectAtLeastOneModeFile] = "需要至少选择一个模式文件和模式名！";
            stringMap[formExportPresetModeData_ModeFileNotExist] = "模式文件 {0} 不存在！";
            stringMap[formExportPresetModeData_AskForOverwriteFile] = "此模式文件已经存在，是否覆盖现有文件？";
            stringMap[formExportPresetModeData_ExportPresetModeDataSuccessfully] = "导出预设模式文件 {0} 成功";
            stringMap[formExportPresetModeData_FailedToExportPresetModeData] = "导出预设模式文件 {0} 失败！";

            stringMap[formCommSettings] = "通讯设置";
            stringMap[formCommSettings_AutoConnection] = "启动后自动联机";
            stringMap[formCommSettings_CommMethod] = "通讯方式";
            stringMap[formCommSettings_SelectOneMethodAtLeast] = "请至少选择一种通讯方式！";
            stringMap[formCommSettings_CommRetransmission] = "通讯重传次数";
            stringMap[formCommSettings_DefaultSerialPort] = "RS232通讯默认端口号";
            stringMap[formCommSettings_SerialPortManualMode] = "RS232端口手动模式";
            stringMap[formCommSettings_On] = "打开";
            stringMap[formCommSettings_Off] = "关闭";            
            stringMap[formCommSettings_AutoMode] = "自动";
            stringMap[formCommSettings_ManualMode] = "手动";
            stringMap[formCommSettings_IPAddress] = "IP地址";
            stringMap[formCommSettings_Port] = "端口";
            stringMap[formCommSettings_Help] = "帮助";
            stringMap[formCommSettings_RestoreToDefault] = "恢复默认";
            stringMap[formCommSettings_Close] = "关闭";

            stringMap[formRS232ToWifiConverter] = "RS232转WiFi";
            stringMap[formRS232ToWifiConverter_DeviceIp] = "设备ip";
            stringMap[formRS232ToWifiConverter_RefreshDevice] = "刷新";
            stringMap[formRS232ToWifiConverter_DeviceInfo] = "设备信息";
            stringMap[formRS232ToWifiConverter_BaudRate] = "波特率";
            stringMap[formRS232ToWifiConverter_Update] = "更新";
            stringMap[formRS232ToWifiConverter_DeviceModel] = "型号";
            stringMap[formRS232ToWifiConverter_DeviceFirmwareVersion] = "版本";
            stringMap[formRS232ToWifiConverter_DeviceMacAddress] = "MAC地址";
            stringMap[formRS232ToWifiConverter_DeviceSocketName] = "连接名称";
            stringMap[formRS232ToWifiConverter_DeviceSocketProtocol] = "协议类型";
            stringMap[formRS232ToWifiConverter_DeviceSocketProtocolUnknown] = "未知";
            stringMap[formRS232ToWifiConverter_DeviceSocketPort] = "端口号";
            stringMap[formRS232ToWifiConverter_DeviceUartDataBits] = "数据位";
            stringMap[formRS232ToWifiConverter_DeviceUartStopBits] = "停止位";
            stringMap[formRS232ToWifiConverter_DeviceUartParity] = "校验位";
            stringMap[formRS232ToWifiConverter_DeviceUartParityNone] = "无";
            stringMap[formRS232ToWifiConverter_DeviceUartParityOdd] = "奇检验";
            stringMap[formRS232ToWifiConverter_DeviceUartParityEven] = "偶检验";
            stringMap[formRS232ToWifiConverter_DeviceUartParityOther] = "其他";
            stringMap[formRS232ToWifiConverter_DeviceUserName] = "用户名";
            stringMap[formRS232ToWifiConverter_DevicePassword] = "密码";
            stringMap[formRS232ToWifiConverter_RestoreConfigToDefault] = "点击恢复默认设置";
            stringMap[formRS232ToWifiConverter_BaudRateNotMatch] = "设备波特率与系统不匹配，点击更新波特率";
            stringMap[formRS232ToWifiConverter_SocketNotMatch] = "检测到连接参数与系统不匹配，建议同步当前设备参数到系统设置，是否同步设置？";

            stringMap[formPreferences] = "首选项";
            stringMap[formPreferences_DeviceModelWhenStarts] = "启动时设备型号";
            stringMap[formPreferences_UseLastConnectedDeviceModel] = "最后一次联机设备型号";

            stringMap[formOnlineHelp] = "联机帮助";
            stringMap[formOnlineHelp_Audo] = "自动";
            stringMap[formOnlineHelp_CommInterface] = "通讯接口";
            stringMap[formOnlineHelp_Port] = "端口";
            stringMap[formOnlineHelp_BaudRate] = "波特率";
            stringMap[formOnlineHelp_IpAddress] = "IP地址";
            stringMap[formOnlineHelp_Run] = "运行";

            stringMap[formFirmwareUpdate] = "固件更新";
            stringMap[formFirmwareUpdate_Port] = "端口";
            stringMap[formFirmwareUpdate_Auto] = "自动";
            stringMap[formFirmwareUpdate_BaudRate] = "波特率";
            stringMap[formFirmwareUpdate_Refresh] = "刷新";
            stringMap[formFirmwareUpdate_Key] = "密钥";
            stringMap[formFirmwareUpdate_File] = "文件";
            stringMap[formFirmwareUpdate_Update] = "更新";
            stringMap[formFirmwareUpdate_Info] = "信息";
            stringMap[formFirmwareUpdate_Progress] = "进度";
            stringMap[formFirmwareUpdate_KeyDataError] = "密钥数据有误";
            stringMap[formFirmwareUpdate_GetFileHashError] = "获取文件Hash错误";
            stringMap[formFirmwareUpdate_FileHash] = "文件Hash：{0}";
            stringMap[formFirmwareUpdate_FirmwareVersion] = "固件版本";
            stringMap[formFirmwareUpdate_BluetoothName] = "蓝牙名称";
            stringMap[formFirmwareUpdate_Rename] = "重命名";
            stringMap[formFirmwareUpdate_Output] = "输出";

            stringMap[formActivateDevice] = "联机帮助";
            stringMap[formActivateDevice_CommInterface] = "通讯接口";
            stringMap[formActivateDevice_Port] = "端口";
            stringMap[formActivateDevice_BaudRate] = "波特率";
            stringMap[formActivateDevice_IpAddress] = "IP地址";
            stringMap[formActivateDevice_Activate] = "激活";

            stringMap[formActivation] = "激活";
            stringMap[formActivation_MachineCode] = "机器码";
            stringMap[formActivation_RequestCode] = "请求码";
            stringMap[formActivation_ActivationCode] = "激活码";
            stringMap[formActivation_WhetherToCancelTheLicense] = "是否注销许可？";

            stringMap[formAbout] = "关于";
            stringMap[formAbout_SupportModels] = "支持的设备型号 : {0}";

            stringMap[rta_FailedToStartCapturingAudio] = "启动捕捉音频数据失败:{0}";
            stringMap[formRTAOptions] = "RTA 选项";
            stringMap[formRTAOptions_Spectrum] = "频谱";
            stringMap[formRTAOptions_TransferFunction] = "传递函数";
            stringMap[formRTAOptions_AudioIO] = "音频输入输出";
            stringMap[formRTAOptions_AudioInDevice] = "音频输入设备";
            stringMap[formRTAOptions_SampleRate] = "采样频率";
            stringMap[formRTAOptions_SampleBitDepth] = "采样位深度";
            stringMap[formRTAOptions_AudioOutDevice] = "音频输出设备";
            stringMap[formRTAOptions_RTAParams] = "RTA参数";
            stringMap[formRTAOptions_FftSize] = "FFT 长度";
            stringMap[formRTAOptions_WindowFunction] = "窗函数";
            stringMap[formRTAOptions_Average] = "均值处理";
            stringMap[formRTAOptions_Smooth] = "平滑处理";
            stringMap[formRTAOptions_Scale] = "刻度";
            stringMap[formRTAOptions_SpectrumGraph] = "频谱图";
            stringMap[formRTAOptions_DoubleCh] = "左右声道";
            stringMap[formRTAOptions_LeftCh] = "左声道";
            stringMap[formRTAOptions_RightCh] = "右声道";
            stringMap[formRTAOptions_AmpOffset] = "幅度偏移";
            stringMap[formRTAOptions_FreqRespGraph] = "频率响应曲线";
            stringMap[formRTAOptions_AmpAndPhaseResp] = "幅频和相频响应";
            stringMap[formRTAOptions_AmpResp] = "幅频响应";
            stringMap[formRTAOptions_PhaseResp] = "相频响应";
            stringMap[formRTAOptions_SignalLevel] = "信号电平";
            stringMap[formRTAOptions_SignalLeftCh] = "左声道（测量信号）";
            stringMap[formRTAOptions_SignalRightCh] = "右声道（参考信号）";
            stringMap[formRTAOptions_Delay] = "延时";
            stringMap[formRTAOptions_MeasDelay] = "测量延时";
            stringMap[formRTAOptions_MeasDelayResult] = "测量结果";
            stringMap[formRTAOptions_ApplyMeasResult] = "应用";
            stringMap[formRTAOptions_DeclineMeasResult] = "拒绝";
            stringMap[formRTAOptions_SignalGenerator] = "信号发生器";
            stringMap[formRTAOptions_Signal] = "信号";
            stringMap[formRTAOptions_Level] = "电平";
            stringMap[formRTAOptions_SineFreq] = "频率";
            stringMap[formRTAOptions_Sine1Level] = "电平1";
            stringMap[formRTAOptions_Sine1Freq] = "频率1";
            stringMap[formRTAOptions_Sine2Level] = "电平2";
            stringMap[formRTAOptions_Sine2Freq] = "频率2";
            stringMap[formRTAOptions_RefTrace] = "参考曲线";
            stringMap[formRTAOptions_Capture] = "捕捉";
            stringMap[formRTAOptions_Delete] = "删除";
            stringMap[formRTAOptions_HannWindow] = "汉宁窗";
            stringMap[formRTAOptions_HammingWindow] = "海明窗";
            stringMap[formRTAOptions_RectangleWindow] = "矩形窗";
            stringMap[formRTAOptions_GaussianWindow] = "高斯窗";
            stringMap[formRTAOptions_BlackmanWindow] = "布莱克曼窗";
            stringMap[formRTAOptions_BlackmanHarrisWindow] = "布莱克曼-哈里斯窗";
            stringMap[formRTAOptions_FlatTopWindow] = "平顶窗";
            stringMap[formRTAOptions_AverageInf] = "无限";
            stringMap[formRTAOptions_AverageFast] = "快速";
            stringMap[formRTAOptions_AverageSlow] = "慢速";
            stringMap[formRTAOptions_SmoothNone] = "无";
            stringMap[formRTAOptions_Smooth3Pt] = "3点";
            stringMap[formRTAOptions_Smooth5Pt] = "5点";
            stringMap[formRTAOptions_Smooth7Pt] = "7点";
            stringMap[formRTAOptions_Smooth9Pt] = "9点";
            stringMap[formRTAOptions_PinkNoise] = "粉噪声";
            stringMap[formRTAOptions_PinkSweep] = "粉扫频";
            stringMap[formRTAOptions_Sine] = "正弦";
            stringMap[formRTAOptions_DualSine] = "双正弦";

            stringMap[formComm_ConnectAndLoadData] = "联机加载数据";
            stringMap[formComm_UploadData] = "上传数据";
            stringMap[formComm_CallModeData] = "调用设备模式";
            stringMap[formComm_DeviceIsNotActivated] = "设备未激活";
            stringMap[formComm_PleaseActivateTheDeviceFirst] = "设备未激活，请先激活后再使用！";           
            stringMap[formComm_DeviceIsLocked] = "设备已被锁";
            stringMap[formComm_PleaseInputPasswordToUnlockSystem] = "设备已被锁，请输入{0}位密码解锁";
            stringMap[formComm_InputAdminPassword] = "请输入{0}位管理员密码解锁";
            stringMap[formComm_InputPasswordError] = "输入的密码错误,请重试！";
            stringMap[formComm_Error] = "错误";
            stringMap[formComm_Password] = "密码";
            stringMap[formComm_Ok] = "确定";
            stringMap[formComm_Close] = "关闭";
            stringMap[formComm_Disconnect] = "断开连接";

            stringMap[comm_NoUSBHIDAvailable] = "系统没有检测到已连接的 USB-HID 设备！";
            stringMap[comm_NoPortAvailable] = "系统没有检测到可用 COM 端口！";
            stringMap[comm_Successful] = "成功";
            stringMap[comm_Failed] = "失败！";
            stringMap[comm_OpenPort] = "打开 {0} ...";
            stringMap[comm_InitUdpClient] = "初始化UDP，（目的IP:{0}, Port:{1}）";
            stringMap[comm_InitTcpClient] = "初始化TCP，（目的IP:{0}, Port:{1}）";
            stringMap[comm_SendConnectionRequest] = "发送联机握手请求...";
            stringMap[comm_CheckResponseSuccessful] = "检测响应成功.";
            stringMap[comm_CheckResponseFailed] = "检测响应失败！";
            stringMap[comm_LoadDeviceInfo] = "加载设备信息...";
            stringMap[comm_LoadXModuleData] = "加载 {0} 模块数据...";
            stringMap[comm_MusicModule] = "音乐";
            stringMap[comm_MicModule] = "麦克风";
            stringMap[comm_MicAModule] = "麦克风A";
            stringMap[comm_MicBModule] = "麦克风B";
            stringMap[comm_MusicDeqModule] = "音乐DEQ";
            stringMap[comm_MicDeqModule] = "麦克风DEQ";
            stringMap[comm_DEffectModule] = "动态效果";
            stringMap[comm_EchoModule] = "回声";
            stringMap[comm_ReverbModule] = "混响";
            stringMap[comm_MainOutputModule] = "主输出";
            stringMap[comm_Aux1OutputModule] = "辅助输出1";
            stringMap[comm_Aux2OutputModule] = "辅助输出2";
            stringMap[comm_SurOutputModule] = "后置输出";
            stringMap[comm_LoadSystemXData] = "加载系统 {0} 数据...";
            stringMap[comm_MusicStartVolume] = "音乐起始音量";
            stringMap[comm_MusicMaxVolume] = "音乐最大音量";
            stringMap[comm_MicStartVolume] = "麦克风起始音量";
            stringMap[comm_MicMaxVolume] = "麦克风最大音量";
            stringMap[comm_EffectStartVolume] = "效果起始音量";
            stringMap[comm_EffectMaxVolume] = "效果最大音量";
            stringMap[comm_MusicVolume] = "音乐音量";
            stringMap[comm_MicVolume] = "麦克风音量";
            stringMap[comm_EffectVolume] = "效果音量";
            stringMap[comm_Timbre] = "音色";
            stringMap[comm_Clarity] = "清晰度";
            stringMap[comm_DeviceWorkMode] = "设备工作模式";
            stringMap[comm_MusicMute] = "音乐静音";
            stringMap[comm_MicMute] = "麦克风静音";
            stringMap[comm_EffectMute] = "效果静音";
            stringMap[comm_MusicInputGain] = "音乐输入增益";
            stringMap[comm_MusicInputPort] = "音乐输入端口";
            stringMap[comm_SystemPassword] = "系统密码";
            stringMap[comm_KeyboardLockPassword] = "键盘锁密码";
            stringMap[comm_User] = "系统权限控制位";
            stringMap[comm_IsLockKeyboard] = "自动键盘锁控制位";
            stringMap[comm_IsEnableRomoteControl] = "遥控使能控制位";
            stringMap[comm_DeviceModeSave] = "设备模式（存储）";
            stringMap[comm_DeviceModeCall] = "设备模式（调用）";
            stringMap[comm_DeviceId] = "设备Id";
            stringMap[comm_DeviceModel] = "设备型号标识";
            stringMap[comm_PrototypeModel] = "设备原型";
            stringMap[comm_FirmwareVersion] = "固件版本";
            stringMap[comm_FirmwareBuildNumbers] = "固件构建号";
            stringMap[comm_IsEnableCustomDeviceInfo] = "设备信息使能";
            stringMap[comm_CustomDeviceInfoData] = "设备信息";
            stringMap[comm_DeviceHardwareState] = "设备硬件状态";
            stringMap[comm_DeviceName] = "设备名称";
            stringMap[comm_UsageLimitLockData] = "设备系统信息数据";
            stringMap[comm_LoadSystemModeXNameData] = "加载系统模式 {0} 名称数据...";
            stringMap[comm_LoadSystemData] = "加载系统数据...";
            stringMap[comm_UploadXModuleData] = "上传 {0} 模块数据...";
            stringMap[comm_UploadSystemXData] = "上传系统 {0} 数据...";
            stringMap[comm_FailedToLoadData] = "联机失败！";
            stringMap[comm_FailedToUploadData] = "上传数据失败！";
            stringMap[comm_FailedToCallModeData] = "调用设备模式失败！";
            stringMap[comm_UpdateDeviceModeIndexData] = "更新设备模式（调用）索引数据...";
            stringMap[comm_UpdateXModeData] = "上传 {0} 模式数据...";
            stringMap[comm_SaveXDataToYMode] = "保存 {0} 模式数据至第{1}个模式...";
            stringMap[comm_InitializingUI] = "界面初始化中...";
            stringMap[comm_UnknownDevcieModelIdentity] = "未知的设备型号标识：{0}！";
            stringMap[comm_UnsupportedDevcieModelIdentity] = "不支持的设备型号标识：{0}！";

            stringMap[formControlTheOtherParty] = "远程控制对方";
            stringMap[formControlTheOtherParty_RemoteId] = "对方识别码";
            stringMap[formControlTheOtherParty_RemoteVerificationCode] = "对方验证码";
            stringMap[formControlTheOtherParty_RemoteControlConnect] = "远程连接";
            stringMap[formControlTheOtherParty_RemoteControlConnecting] = "正在连接中...";
            stringMap[formControlTheOtherParty_DisconnectRemoteControl] = "断开远程连接";
            stringMap[formControlTheOtherParty_DisconnectingRemoteControl] = "正在断开连接...";
            stringMap[formControlTheOtherParty_FailedToConnectToRemoteClient] = "连接到远程客户端 {0} 失败:{1}";
            stringMap[formControlTheOtherParty_RemoteControl] = "远程控制";
            stringMap[formControlTheOtherParty_IdCodeError] = "识别码输入有误！";
            stringMap[formControlTheOtherParty_VerificationCodeError] = "验证码码输入有误！";
            stringMap[formControlTheOtherParty_EnterNonLocalIdCode] = "请输入非本机识别码！";

            stringMap[formControlledRemotely] = "请求对方远程控制本机";
            stringMap[formControlledRemotely_LocalId] = "本机识别码";
            stringMap[formControlledRemotely_LocalVerificationCode] = "本机验证码";
            stringMap[formControlledRemotely_DisconnectRemoteControl] = "断开远程连接";
            stringMap[formControlledRemotely_DisconnectingRemoteControl] = "正在断开...";
            stringMap[formControlledRemotely_AllowToBeControlled] = "允许被远程控制";

            stringMap[formRCSettings] = "设置";
            stringMap[formRCSettings_ServerIPAddress] = "服务器IP地址";
            stringMap[formRCSettings_ServerPort] = "端口";
            stringMap[formRCSettings_EnterCorrentIP] = "请输入正确的IP地址！";
            stringMap[formRCSettings_EnterCorrentPort] = "请输入正确的端口号！";
            stringMap[formRCSettings_RestoreToDefault] = "恢复默认";
            stringMap[formRCSettings_Close] = "关闭";

            stringMap[panelNetworkConnectionStatus_NotConnectedToTheServer] = "未连接到服务器";
            stringMap[panelNetworkConnectionStatus_ConnectingToTheServer] = "正在连接服务器...";
            stringMap[panelNetworkConnectionStatus_SuccessfullyConnectedToTheServer] = "连接服务器成功";
            stringMap[panelNetworkConnectionStatus_FailedToConnectToTheServer] = "连接服务器 {0} 失败!";
            stringMap[panelNetworkConnectionStatus_DisconnectFromTheServer] = "断开与服务器连接";
            stringMap[panelNetworkConnectionStatus_ConnectionToTheServerIsInterrupted] = "与服务器连接中断！";
            stringMap[panelNetworkConnectionStatus_ReconnectToTheServer] = "重连服务器";

            stringMap[remoteControl_ConnectResponse_Unknown] = "未知原因";
            stringMap[remoteControl_ConnectResponse_TargetNotOnline] = "远程主机不在线";
            stringMap[remoteControl_ConnectResponse_VerificationCodeError] = "验证码错误";
            stringMap[remoteControl_ConnectResponse_NotAllowedToBeControlled] = "远程主机不允许被远程控制";
            stringMap[remoteControl_ConnectResponse_RemoteClientBusy] = "远程主机忙碌中...";

            stringMap[remoteControl_RemoteClientDisconnect] = "远程客户端断开连接！";
            stringMap[remoteControl_ConnectionIsInterrupted] = "与服务器连接中断！";

            stringMap[formEasyMode_ImportFromFile] = "从本地文件导入数据";
            stringMap[formEasyMode_Skip] = "跳过";
            stringMap[formEasyMode_ImportData] = "生成数据";
            stringMap[formEasyMode_Back] = "返回";
            stringMap[formEasyMode_Save] = "保存";
            stringMap[formEasyMode_FileDataError] = "文件数据错误";

            stringMap[formViewImportAudioSystem] = "导入数据";
            stringMap[formViewImportAudioSystem_DataAdaptedToAudioSystem] = "数据适配以下音响系统:";
            stringMap[formViewImportAudioSystem_AudioSystem] = "音响系统";
            stringMap[formViewImportAudioSystem_Modes] = "模式-";
            stringMap[formViewImportAudioSystem_AskForImportMode] = "此组数据包含以下 {0} 个模式:{1}。点击 \"{2}\" 会将数据分别覆盖到设备:{3}， 是否继续？";

            stringMap[easyModeControlPanel_SystemStatus] = "系统状态";
            stringMap[easyModeControlPanel_CurrentVolume] = "当前音量";
            stringMap[easyModeControlPanel_StartVolume] = "最小音量";
            stringMap[easyModeControlPanel_LimitVolume] = "最大音量";
            stringMap[easyModeControlPanel_Mute] = "静音";
            stringMap[easyModeControlPanel_Preset] = "预设模式";
            stringMap[easyModeControlPanel_Other] = "其他模式";
            stringMap[easyModeControlPanel_ModeX] = "模式{0}";
            stringMap[easyModeControlPanel_Empty] = "（空）";

            stringMap[licenseType_Unauthorized] = "未授权";
            stringMap[licenseType_Temporary] = "临时许可";
            stringMap[licenseType_Limited] = "有限许可";
            stringMap[licenseType_Unlimited] = "无限制许可";

            stringMap[firmwareUpdate_Yes] = "是";
            stringMap[firmwareUpdate_No] = "否";
            stringMap[firmwareUpdate_DataIsInvalid] = "数据无效";
            stringMap[firmwareUpdate_FirmwareType] = "固件类型：{0}";
            stringMap[firmwareUpdate_WhetherToUpdate] = "是否更新：{0}";
            stringMap[firmwareUpdate_DataLength] = "数据长度：{0}";
            stringMap[firmwareUpdate_UpdateTime] = "更新时间：{0}";
            stringMap[firmwareUpdate_DataHash] = "数据Hash：{0}";
            stringMap[firmwareUpdate_WhetherToUpdateFlashAllData] = "是否更新 Flash 全部数据?: {0}";
            stringMap[firmwareUpdate_FlashAllData] = "Flash 全部数据, 地址: {0}, 长度：{1}";
            stringMap[firmwareUpdate_WhetherToUpdateCustomDeviceInfo] = "是否更新自定义设备信息数据?: {0}";
            stringMap[firmwareUpdate_CustomDeviceInfo] = "自定义设备信息数据[{0}], 地址: {1}, 长度：{2}";
            stringMap[firmwareUpdate_WhetherToUpdatePresetData] = "是否更新预设数据?: {0}";
            stringMap[firmwareUpdate_PresetData] = "预设数据, 地址: {0}, 长度：{1}";
            stringMap[firmwareUpdate_UpdatedSuccessfully] = "更新成功.";
            stringMap[firmwareUpdate_FailedToUpdate] = "更新失败！";

            stringMap[usageLimitLock] = "使用限制锁";
            stringMap[usageLimitLock_Management] = "使用限制锁管理";
            stringMap[usageLimitLock_Management_NotSupport] = "使用限制锁管理，当前设备不支持此功能";
            stringMap[usageLimitLock_DeviceID] = "设备ID";
            stringMap[usageLimitLock_DeviceName] = "设备名称";
            stringMap[usageLimitLock_RandKey] = "随机密钥";
            stringMap[usageLimitLock_PasswordManagement] = "密码管理";
            stringMap[usageLimitLock_LockTaskManagement] = "锁任务管理";
            stringMap[usageLimitLock_ForgotPassword] = "忘记密码";
            stringMap[usageLimitLock_TaskFilePath] = "任务文件";
            stringMap[usageLimitLock_Setting] = "设置";
            stringMap[usageLimitLock_Copy] = "复制";
            stringMap[usageLimitLock_Clear] = "清除";
            stringMap[usageLimitLock_Import] = "导入";
            stringMap[usageLimitLock_Export] = "导出";
            stringMap[usageLimitLock_CurrentState] = "当前状态";
            stringMap[usageLimitLock_RemainderUsage] = "剩余使用次数";
            stringMap[usageLimitLock_CurrentPassword] = "当前密码";
            stringMap[usageLimitLock_OK] = "确定";
            stringMap[usageLimitLock_NewPassword] = "新密码";
            stringMap[usageLimitLock_ConfirmNewPassword] = "确认新密码";
            stringMap[usageLimitLock_UseCurrentPassword] = "使用当前密码";
            stringMap[usageLimitLock_State] = "状态";
            stringMap[usageLimitLock_UsageCount] = "使用次数";
            stringMap[usageLimitLock_NewPasswordLengthLessThan6] = "新密码长度不足6位！";
            stringMap[usageLimitLock_ConfirmNewPasswordLengthLessThan6] = "确认新密码长度不足6位数！";
            stringMap[usageLimitLock_PasswordEnteredInconsistent] = "密码输入不一致，请重新输入！";
            stringMap[usageLimitLock_UsageCountEnteredIncorrectly] = "使用次数输入有误，请重新输入！";
            stringMap[usageLimitLock_UsageCountEnteredExceedsMaximum] = "输入的使用次数 {0} 超过上限 {1}，请重新输入！";
            stringMap[usageLimitLock_PasswordsMustBeNumeric] = "密码必须为数字！";
            stringMap[usageLimitLock_Random] = "随机";
            stringMap[usageLimitLock_LockXPassword] = "锁 {0} 密码";
            stringMap[usageLimitLock_LockX] = "锁 {0}";
            stringMap[usageLimitLock_OldPassword] = "旧密码";
            stringMap[usageLimitLock_IsUpdate] = "是否更新";
            stringMap[usageLimitLock_IsEnable] = "是否开启";
            stringMap[usageLimitLock_LockXIsUpdated] = "#{0} 锁 已更新：{1}";
            stringMap[usageLimitLock_LockToString] = "{0}，使用次数：{1}，密码：{2}";
            stringMap[usageLimitLock_TaskFileXNotExist] = "所任务文件 {0} 不存在！";
            stringMap[usageLimitLock_NoLockWillToBeUpdated] = "没有锁会被更新！";
            stringMap[usageLimitLock_LockXPasswordNotMatch] = "# {0} 锁 原密码 不匹配";
            stringMap[usageLimitLock_TheFollowingLocksWillToBeUpdated] = "将会更新以下锁：";
            stringMap[usageLimitLock_LockTaskToString] = "#{0} 锁：旧密码：{1}，{2}，使用次数：{3}，新密码：{4}";
            stringMap[usageLimitLock_Enable] = "开启";
            stringMap[usageLimitLock_Disable] = "关闭";

            stringMap[FormUnlockOutput] = "解锁参数";
            stringMap[FormUnlockOutput_Password] = "密码";
            stringMap[FormUnlockOutput_Unlock] = "解锁";
            stringMap[FormUnlockOutput_PasswordIncorrect] = "密码错误";
        }

        private void InitLangCode()
        {
            string currCode = System.Threading.Thread.CurrentThread.CurrentCulture.Name;
            if (lang2CodeMap.ContainsKey(currCode))
            {
                _lang = lang2CodeMap[currCode];
            }
            else
            {
                _lang = LanguageCode.en_US;
            }

            UpdateStringMap();
        }

        public string this[string key]
        {
            get
            {
                if(stringMap.ContainsKey(key))
                {
                    return stringMap[key];
                }

                return string.Empty;
            }
        }

        private void UpdateStringMap()
        {
            byte[] package = code2PackageMap[this._lang];
            if(package == null)
            {
                return;
            }

            MemoryStream stream = new MemoryStream(package);

            try
            {
                //XmlDocument xmldoc = new XmlDocument();
                XmlReader reader = new XmlTextReader(stream);
                XmlDocument xmldoc = new XmlDocument();
                xmldoc.Load(reader);
                XmlNode root = xmldoc.DocumentElement;
                //获取XML文件中功能模块
                XmlNodeList nodeList = root.SelectNodes("Content/Item");
                foreach (XmlNode node in nodeList)
                {
                    try
                    {
                        XmlNode keyNode = node.SelectSingleNode("@key");
                        XmlNode valueNode = node.SelectSingleNode("@value");
                        if (keyNode != null && valueNode != null)
                        {
                            string key = keyNode.InnerText;
                            if(!string.IsNullOrEmpty(key))
                            {
                                this.stringMap[key] = valueNode.InnerText;
                                //Console.WriteLine("{0} : {1}"，key，valueNode.InnerText);
                            }
                        }
                    }
                    catch
                    {
                    }
                }
                reader.Close();
            }
            catch
            {
                //MessageBox.Show("加载文件发生异常!"，"AEC"，MessageBoxButtons.OK，MessageBoxIcon.Error);
                //return null;
            }
        }

        //private byte[] GetLanguagePackage()
        //{
        //    switch(this._lang)
        //    {
        //        case LanguageCode.zh_CN:
        //            return AEC.Properties.Resources.lang_zh_CN;

        //        case LanguageCode.en_US:
        //            return AEC.Properties.Resources.lang_en_US;
        //    }

        //    return null;
        //}

        public LanguageCode lang
        {
            get
            {
                return this._lang;
            }
            set
            {
                if(_lang != value)
                {
                    _lang = value;
                    UpdateStringMap();
                    langEventBus.NotifyObeserver(new LanguageChangedArgs());
                }
            }
        }

        public string langCode
        {
            get
            {
                if (code2LangMap.ContainsKey(this._lang))
                {
                    return code2LangMap[this._lang];
                }
                else
                {
                    return string.Empty;
                }
            }
            set
            {
                LanguageCode langCode = LanguageCode.zh_CN;

                if (lang2CodeMap.ContainsKey(value))
                {
                    langCode = lang2CodeMap[value];
                }
                else
                {
                    string currCode = System.Threading.Thread.CurrentThread.CurrentCulture.Name;
                    if (lang2CodeMap.ContainsKey(currCode))
                    {
                        langCode = lang2CodeMap[currCode];
                    }
                    else
                    {
                        langCode = LanguageCode.en_US;
                    }
                }

                if (_lang != langCode)
                {
                    _lang = langCode;
                    UpdateStringMap();
                }
            }
        }

        public string helpFileName
        {
            get
            {
                if(this._lang == LanguageCode.zh_CN)
                {
                    return "AEC_zh-CN.chm";
                }
                else
                {
                    return "AEC_en-US.chm";
                }
            }
        }
    }
}
