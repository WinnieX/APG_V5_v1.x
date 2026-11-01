using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Threading;
using System.Diagnostics;
using AEC.Core;
using AEC.Effectors;
using AEC.Comm;
using AEC.Common;
using AEC.Effectors.X5;
using AEC.Effectors.VKxS;
using AEC.Effectors.VKxDM;
using AEC.Effectors.KX;
using AEC.Effectors.VKxSCM;
using AEC.Effectors.VKxDCM;
using AEC.Effectors.VKxPII;
using AEC.Effectors.VKxPIIDCM;
using AEC.Effectors.VX;
using AEC.Effectors.MX;
using AEC.UI.X5;
using AEC.UI.VKxS;
using AEC.UI.VKxDM;
using AEC.UI.KX;
using AEC.UI.VKxSCM;
using AEC.UI.VKxDCM;
using AEC.UI.VKxPII;
using AEC.UI.VKxPIIDCM;
using AEC.UI.VX;
using AEC.UI.MX;
using AEC.UI.RTA;
using AEC.RTA;
using AEC.Tools;
using AEC.Misc;
using AEC.UI.Skin;
using AEC.UI.Utility;

namespace AEC.UI
{
    public partial class FormProMode : Form
    {
        private delegate void KeyCallback(Keys keyData);
        private SynchronizationContext syncContext;

        private readonly MController mController;

        private MEffectorFamily currEffectorFamily;

        private Dictionary<PageItem, PanelBase> x5Item2PanelMap;
        private Dictionary<PageItem, ModuleBase> x5Item2ModuleMap;
        private Dictionary<ModuleBase, PageItem> x5Module2ItemMap;

        private Dictionary<PageItem, PanelBase> vkxsItem2PanelMap;
        private Dictionary<PageItem, ModuleBase> vkxsItem2ModuleMap;
        private Dictionary<ModuleBase, PageItem> vkxsModule2ItemMap;

        private Dictionary<PageItem, PanelBase> vkxdmItem2PanelMap;
        private Dictionary<PageItem, ModuleBase> vkxdmItem2ModuleMap;
        private Dictionary<ModuleBase, PageItem> vkxdmModule2ItemMap;

        private Dictionary<PageItem, PanelBase> kxItem2PanelMap;
        private Dictionary<PageItem, ModuleBase> kxItem2ModuleMap;
        private Dictionary<ModuleBase, PageItem> kxModule2ItemMap;

        private Dictionary<PageItem, PanelBase> vkxscmItem2PanelMap;
        private Dictionary<PageItem, ModuleBase> vkxscmItem2ModuleMap;
        private Dictionary<ModuleBase, PageItem> vkxscmModule2ItemMap;

        private Dictionary<PageItem, PanelBase> vkxdcmItem2PanelMap;
        private Dictionary<PageItem, ModuleBase> vkxdcmItem2ModuleMap;
        private Dictionary<ModuleBase, PageItem> vkxdcmModule2ItemMap;

        private Dictionary<PageItem, PanelBase> vkxPIIItem2PanelMap;
        private Dictionary<PageItem, ModuleBase> vkxPIIItem2ModuleMap;
        private Dictionary<ModuleBase, PageItem> vkxPIIModule2ItemMap;

        private Dictionary<PageItem, PanelBase> vkxPIIdcmItem2PanelMap;
        private Dictionary<PageItem, ModuleBase> vkxPIIdcmItem2ModuleMap;
        private Dictionary<ModuleBase, PageItem> vkxPIIdcmModule2ItemMap;

        private Dictionary<PageItem, PanelBase> vxItem2PanelMap;
        private Dictionary<PageItem, ModuleBase> vxItem2ModuleMap;
        private Dictionary<ModuleBase, PageItem> vxModule2ItemMap;

        private Dictionary<PageItem, PanelBase> mxItem2PanelMap;
        private Dictionary<PageItem, ModuleBase> mxItem2ModuleMap;

        private Dictionary<PageItem, ModuleBase> currItem2ModuleMap;
        private Dictionary<PageItem, PanelBase> currItem2PanelMap;

        private EventBus eventBus = new EventBus();
        private EventBus rtaEventBus = new EventBus();

        private SystemPanel mSystemPanel;     //系统面板
        private NavigationBarBase navigationBar;
        private X5NavigationBar x5NavigationBar;
        private VKxSNavigationBar vkxsNavigationBar;
        private VKxDMNavigationBar vkxsdmNavigationBar;
        private KXNavigationBar kxNavigationBar;
        private VKxSCMNavigationBar vkxscmNavigationBar;
        private VKxDCMNavigationBar vkxdcmNavigationBar;
        private VKxPIINavigationBar vkxPIINavigationBar;
        private VKxPIIDCMNavigationBar vkxPIIdcmNavigationBar;
        private VXNavigationBar vxNavigationBar;
        private MXNavigationBar mxNavigationBar;

        private NumberKeysHelper keysHelper = new NumberKeysHelper();
        private Dictionary<Keys, KeyCallback> key2EventMap;
        private Keys lastKey = Keys.None;
        private DateTime lastKeydownTimestamp = DateTime.Now;

        private static readonly Point controlPanelLeftTop = new Point(2, 67);

        private PageItem lastPageItem = PageItem.Music;

        private FormComm formComm = null;
        private FormRTAOptions formRTAOption = null;
        
        private bool isFirstStart = true;
        private bool isActuallyClose = !AppConfig.isShowMainForm;

        //private ProtectedMask protectedMask = null;

        //private System.ComponentModel.BackgroundWorker bgwInitAudioAnalysis;
        private AudioAnalysis audioAnalysis = AudioAnalysis.GetInstance();
        private Localization localization = Config.GetInstance().localization;

        private EffectorModelBase currEffectorModel
        {
            get
            {
                return this.mController.currEffectorModel;
            }
        }

        public new string Text
        {
            get
            {
                return base.Text;
            }
            set
            {
                base.Text = value;
                this.title.Text = value;
            }
        }

        private PanelBase systemPanel
        {
            get
            {
                return this.currItem2PanelMap[PageItem.System];
            }
        }

        public X5EffectorModel x5EffectorModel
        {
            get
            {
                return this.mController.x5EffectorModel;
            }
        }

        public VKxSEffectorModel vkxsEffectorModel
        {
            get
            {
                return this.mController.vkxsEffectorModel;
            }
        }

        public VKxDMEffectorModel vkxsdmEffectorModel
        {
            get
            {
                return this.mController.vkxsdmEffectorModel;
            }
        }

        public KXEffectorModel kxEffectorModel
        {
            get
            {
                return this.mController.kxEffectorModel;
            }
        }

        public VKxSCMEffectorModel vkxscmEffectorModel
        {
            get
            {
                return this.mController.vkxscmEffectorModel;
            }
        }

        public VKxDCMEffectorModel vkxdcmEffectorModel
        {
            get
            {
                return this.mController.vkxdcmEffectorModel;
            }
        }

        public VKxPIIEffectorModel vkxPIIEffectorModel
        {
            get
            {
                return this.mController.vkxPIIEffectorModel;
            }
        }

        public VKxPIIDCMEffectorModel vkxPIIdcmEffectorModel
        {
            get
            {
                return this.mController.vkxPIIdcmEffectorModel;
            }
        }

        public VXEffectorModel vxEffectorModel
        {
            get
            {
                return this.mController.vxEffectorModel;
            }
        }

        public MXEffectorModel mxEffectorModel
        {
            get
            {
                return this.mController.mxEffectorModel;
            }
        }

        public FormProMode(MController mController_)
        {
            InitializeComponent();
            
            InitFont();

            SetStyle(ControlStyles.OptimizedDoubleBuffer, true); //默认启动双缓冲
            SetStyle(ControlStyles.DoubleBuffer, true);
            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.ResizeRedraw, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true); // 禁止擦除背景.

            this.DoubleBuffered = true;
            this.UpdateStyles();
            this.syncContext = WindowsFormsSynchronizationContext.Current;

            this.mController = mController_;
            this.currEffectorFamily = this.mController.currEffectorFamily;
            
            this.mSystemPanel = new SystemPanel();
            this.mSystemPanel.BindingController(this.mController, this.eventBus);

            this.InitKeyEvent();
        }

        private void InitFont()
        {
            this.title.Font = FontHelper.CreateFont10();
            this.btnConnect.Font = FontHelper.CreateFont10();
        }

        private void InitKeyEvent()
        {
            this.key2EventMap = new Dictionary<Keys, KeyCallback>()
            {
                {Keys.PageUp, PageUpOperation},
                {Keys.PageDown, PageDownOperation},
                {Keys.F1, ShowHelpHandler},
                {Keys.F12, ShowFormCommSettings},
                {Keys.F11, ShowRTAOptionHandler},
                //{Keys.F10, SwitchLockStatusHandler},
                {Keys.Q, AdjuestVolume},
                {Keys.W, AdjuestVolume},
                {Keys.E, AdjuestVolume},
                {Keys.A, AdjuestVolume},
                {Keys.S, AdjuestVolume},
                {Keys.D, AdjuestVolume},
                {Keys.Z, SwitchPage},
                {Keys.X, SwitchPage},
                {Keys.C, SwitchPage},
                {Keys.V, SwitchPage},
                {Keys.B, SwitchPage},
                {Keys.N, SwitchPage},
                {Keys.M, SwitchPage},
                //{Keys.Tab, QuickOperation},
            };
        }

        //初始化eqGraphicPanle
        private void InitGraphicPanel()
        {
            this.eqGraphicPanel.InitPanelSize();
            this.eqGraphicPanel.RegisterSizeChangedHandler();
            this.eqGraphicPanel.SizeChanged += eqGraphicPanel_SizeChanged;
            this.eqGraphicPanel.MouseMove += eqGraphicPanel_MouseMove;
            this.eqGraphicPanel.MouseLeave += eqGraphicPanel_MouseLeave;
        }

        private void UpdateEffectorFamily(MEffectorFamily currEffectorFamily_, bool isNeedToSwitchToSystemPanel = false)
        {
            if (currEffectorFamily_ == MEffectorFamily.X5)
            {
                Debug.Assert(this.x5EffectorModel != null);

                if (this.x5Item2PanelMap == null)
                {
                    this.x5Item2PanelMap = new Dictionary<PageItem, PanelBase>()
                    {
                        {PageItem.Music, new X5MusicPanel()},
                        {PageItem.Mic, new X5MicPanel()},
                        {PageItem.Effect, new X5EffectPanel()},
                        {PageItem.MainOutput, new X5MainOutputPanel()},
                        {PageItem.Aux1Output, new X5CenterOutputPanel()},
                        {PageItem.Aux2Output, new X5SubOutputPanel()},
                        {PageItem.SurOutput, new X5SurOutputPanel()},
                        {PageItem.System, this.mSystemPanel},
                    };

                    foreach (PanelBase panel in this.x5Item2PanelMap.Values)
                    {
                        panel.Location = controlPanelLeftTop;

                        panel.BindingController(this.mController, this.eventBus);
                    }
                }
                else
                {
                    if (!this.x5EffectorModel.dataIsPrivate)
                    {
                        foreach (PageItem page in this.x5Item2PanelMap.Keys)
                        {
                            if (page != PageItem.System)
                            {
                                this.x5Item2PanelMap[page].UpdateUIFromData();
                            }
                        }
                    }
                }             

                this.mSystemPanel.BindingSystemModel(this.x5EffectorModel.system);

                if (this.x5Item2ModuleMap == null)
                {
                    this.x5Item2ModuleMap = new Dictionary<PageItem, ModuleBase>
                    {
                        {PageItem.Music, this.x5EffectorModel.music},
                        {PageItem.Mic, this.x5EffectorModel.mic},
                        {PageItem.Effect, this.x5EffectorModel.echo},
                        {PageItem.MainOutput, this.x5EffectorModel.mainOutput},
                        {PageItem.Aux1Output, this.x5EffectorModel.centerOutput},
                        {PageItem.Aux2Output, this.x5EffectorModel.subOutput},
                        {PageItem.SurOutput, this.x5EffectorModel.surOutput},
                    };
                }

                if (this.x5Module2ItemMap == null)
                {
                    this.x5Module2ItemMap = new Dictionary<ModuleBase, PageItem>
                    {
                        {this.x5EffectorModel.music, PageItem.Music},
                        {this.x5EffectorModel.mic, PageItem.Mic},
                        {this.x5EffectorModel.mainOutput, PageItem.MainOutput},
                        {this.x5EffectorModel.centerOutput, PageItem.Aux1Output},
                        {this.x5EffectorModel.subOutput, PageItem.Aux2Output},
                        {this.x5EffectorModel.surOutput, PageItem.SurOutput},
                    };
                }

                this.currItem2ModuleMap = this.x5Item2ModuleMap;
                this.currItem2PanelMap = this.x5Item2PanelMap;

                if (this.x5NavigationBar == null)
                {
                    this.x5NavigationBar = new X5NavigationBar();
                    this.x5NavigationBar.Init();
                    this.x5NavigationBar.CheckedChanged += new System.EventHandler<System.EventArgs>(this.navigationBar_CheckedChanged);
                    this.x5NavigationBar.Binding(this.mController, this.eventBus);
                }
                else
                {
                    this.x5NavigationBar.UpdateUIFromData();
                }

                if (this.navigationBar != null && this.navigationBar != this.x5NavigationBar)
                {
                    if (this.panelToolBar.Contains(this.navigationBar))
                    {
                        this.panelToolBar.Controls.Remove(this.navigationBar);
                    }
                }

                this.navigationBar = this.x5NavigationBar;
                if (!this.panelToolBar.Contains(this.navigationBar))
                {
                    this.panelToolBar.Controls.Add(this.navigationBar, 0, 0);
                }
            }
            else if (currEffectorFamily_ == MEffectorFamily.VKxS)
            {
                Debug.Assert(this.vkxsEffectorModel != null);

                if (this.vkxsItem2PanelMap == null)
                {
                    this.vkxsItem2PanelMap = new Dictionary<PageItem, PanelBase>()
                    {
                        {PageItem.Music, new VKxSMusicPanel()},
                        {PageItem.Mic, new VKxSMicPanel()},
                        {PageItem.Effect, new VKxSEffectPanel()},
                        {PageItem.MainOutput, new VKxSMainOutputPanel()},
                        {PageItem.Aux1Output, new VKxSCenterOutputPanel()},
                        {PageItem.Aux2Output, new VKxSSubOutputPanel()},
                        {PageItem.SurOutput, new VKxSSurOutputPanel()},
                        {PageItem.System, this.mSystemPanel},
                    };

                    foreach (PanelBase panel in this.vkxsItem2PanelMap.Values)
                    {
                        panel.Location = controlPanelLeftTop;

                        panel.BindingController(this.mController, this.eventBus);
                    }
                }
                else
                {
                    if (!this.vkxsEffectorModel.dataIsPrivate)
                    {
                        foreach (PageItem page in this.vkxsItem2PanelMap.Keys)
                        {
                            if (page != PageItem.System)
                            {
                                this.vkxsItem2PanelMap[page].UpdateUIFromData();
                            }
                        }
                    }
                }

                this.mSystemPanel.BindingSystemModel(this.vkxsEffectorModel.system);

                if (this.vkxsItem2ModuleMap == null)
                {
                    this.vkxsItem2ModuleMap = new Dictionary<PageItem, ModuleBase>
                    {
                        {PageItem.Music, this.vkxsEffectorModel.music},
                        {PageItem.Mic, this.vkxsEffectorModel.mic},
                        {PageItem.Effect, this.vkxsEffectorModel.echo},
                        {PageItem.MainOutput, this.vkxsEffectorModel.mainOutput},
                        {PageItem.Aux1Output, this.vkxsEffectorModel.aux1Output},
                        {PageItem.Aux2Output, this.vkxsEffectorModel.aux2Output},
                        {PageItem.SurOutput, this.vkxsEffectorModel.surOutput},
                    };
                }

                if (this.vkxsModule2ItemMap == null)
                {
                    this.vkxsModule2ItemMap = new Dictionary<ModuleBase, PageItem>
                    {
                        {this.vkxsEffectorModel.music, PageItem.Music},
                        {this.vkxsEffectorModel.mic, PageItem.Mic},
                        {this.vkxsEffectorModel.mainOutput, PageItem.MainOutput},
                        {this.vkxsEffectorModel.aux1Output, PageItem.Aux1Output},
                        {this.vkxsEffectorModel.aux2Output, PageItem.Aux2Output},
                        {this.vkxsEffectorModel.surOutput, PageItem.SurOutput},
                    };
                }

                this.currItem2ModuleMap = this.vkxsItem2ModuleMap;
                this.currItem2PanelMap = this.vkxsItem2PanelMap;

                if (this.vkxsNavigationBar == null)
                {
                    this.vkxsNavigationBar = new VKxSNavigationBar();
                    this.vkxsNavigationBar.Init();
                    this.vkxsNavigationBar.CheckedChanged += new System.EventHandler<System.EventArgs>(this.navigationBar_CheckedChanged);
                    this.vkxsNavigationBar.Binding(this.mController, this.eventBus);
                }
                else
                {
                    this.vkxsNavigationBar.UpdateUIFromData();
                }

                if (this.navigationBar != null && this.navigationBar != this.vkxsNavigationBar)
                {
                    if (this.panelToolBar.Contains(this.navigationBar))
                    {
                        this.panelToolBar.Controls.Remove(this.navigationBar);
                    }
                }

                this.navigationBar = this.vkxsNavigationBar;
                if (!this.panelToolBar.Contains(this.navigationBar))
                {
                    this.panelToolBar.Controls.Add(this.navigationBar, 0, 0);
                }
            }
            else if (currEffectorFamily_ == MEffectorFamily.VKxDM)
            {
                Debug.Assert(this.vkxsdmEffectorModel != null);

                if (this.vkxdmItem2PanelMap == null)
                {
                    this.vkxdmItem2PanelMap = new Dictionary<PageItem, PanelBase>()
                    {
                        {PageItem.Music, new VKxDMMusicPanel()},
                        {PageItem.MicA, new VKSxDMMicAPanel()},
                        {PageItem.MicB, new VKSxDMMicBPanel()},
                        {PageItem.Effect, new VKxDMEffectPanel()},
                        {PageItem.MainOutput, new VKxDMMainOutputPanel()},
                        {PageItem.Aux1Output, new VKxDMCenterOutputPanel()},
                        {PageItem.Aux2Output, new VKxDMSubOutputPanel()},
                        {PageItem.SurOutput, new VKxDMSurOutputPanel()},
                        {PageItem.System, this.mSystemPanel},
                    };

                    foreach (PanelBase panel in this.vkxdmItem2PanelMap.Values)
                    {
                        panel.Location = controlPanelLeftTop;

                        panel.BindingController(this.mController, this.eventBus);
                    }
                }
                else
                {
                    if (!this.vkxsdmEffectorModel.dataIsPrivate)
                    {
                        foreach (PageItem page in this.vkxdmItem2PanelMap.Keys)
                        {
                            if (page != PageItem.System)
                            {
                                this.vkxdmItem2PanelMap[page].UpdateUIFromData();
                            }
                        }
                    }
                }

                this.mSystemPanel.BindingSystemModel(this.vkxsdmEffectorModel.system);

                if (this.vkxdmItem2ModuleMap == null)
                {
                    this.vkxdmItem2ModuleMap = new Dictionary<PageItem, ModuleBase>
                    {
                        {PageItem.Music, this.vkxsdmEffectorModel.music},
                        {PageItem.MicA, this.vkxsdmEffectorModel.micA},
                        {PageItem.MicB, this.vkxsdmEffectorModel.micB},
                        {PageItem.Effect, this.vkxsdmEffectorModel.echo},
                        {PageItem.MainOutput, this.vkxsdmEffectorModel.mainOutput},
                        {PageItem.Aux1Output, this.vkxsdmEffectorModel.aux1Output},
                        {PageItem.Aux2Output, this.vkxsdmEffectorModel.aux2Output},
                        {PageItem.SurOutput, this.vkxsdmEffectorModel.surOutput},
                    };
                }

                if (this.vkxdmModule2ItemMap == null)
                {
                    this.vkxdmModule2ItemMap = new Dictionary<ModuleBase, PageItem>
                    {
                        {this.vkxsdmEffectorModel.music, PageItem.Music},
                        {this.vkxsdmEffectorModel.micA, PageItem.MicA},
                        {this.vkxsdmEffectorModel.micB, PageItem.MicB},
                        {this.vkxsdmEffectorModel.mainOutput, PageItem.MainOutput},
                        {this.vkxsdmEffectorModel.aux1Output, PageItem.Aux1Output},
                        {this.vkxsdmEffectorModel.aux2Output, PageItem.Aux2Output},
                        {this.vkxsdmEffectorModel.surOutput, PageItem.SurOutput},
                    };
                }

                this.currItem2ModuleMap = this.vkxdmItem2ModuleMap;
                this.currItem2PanelMap = this.vkxdmItem2PanelMap;

                if (this.vkxsdmNavigationBar == null)
                {
                    this.vkxsdmNavigationBar = new VKxDMNavigationBar();
                    this.vkxsdmNavigationBar.Init();
                    this.vkxsdmNavigationBar.CheckedChanged += new System.EventHandler<System.EventArgs>(this.navigationBar_CheckedChanged);
                    this.vkxsdmNavigationBar.Binding(this.mController, this.eventBus);
                }
                else
                {
                    this.vkxsdmNavigationBar.UpdateUIFromData();
                }

                if (this.navigationBar != null && this.navigationBar != this.vkxsdmNavigationBar)
                {
                    if (this.panelToolBar.Contains(this.navigationBar))
                    {
                        this.panelToolBar.Controls.Remove(this.navigationBar);
                    }
                }

                this.navigationBar = this.vkxsdmNavigationBar;
                if (!this.panelToolBar.Contains(this.navigationBar))
                {
                    this.panelToolBar.Controls.Add(this.navigationBar, 0, 0);
                }
            }
            else if (currEffectorFamily_ == MEffectorFamily.KX)
            {
                Debug.Assert(this.kxEffectorModel != null);

                if (this.kxItem2PanelMap == null)
                {
                    this.kxItem2PanelMap = new Dictionary<PageItem, PanelBase>()
                    {
                        {PageItem.Music, new KXMusicPanel()},
                        {PageItem.Mic, new KXMicPanel()},
                        {PageItem.Effect, new KXEffectPanel()},
                        {PageItem.MainOutput, new KXMainOutputPanel()},
                        {PageItem.Aux1Output, new KXAux1OutputPanel()},
                        {PageItem.Aux2Output, new KXAux2OutputPanel()},
                        {PageItem.SurOutput, new KXSurOutputPanel()},
                        {PageItem.System, this.mSystemPanel},
                        {PageItem.Monitor, new KXMonitorPanel()},
                    };

                    foreach (PanelBase panel in this.kxItem2PanelMap.Values)
                    {
                        panel.Location = controlPanelLeftTop;

                        panel.BindingController(this.mController, this.eventBus);
                    }
                }
                else
                {
                    if (!this.kxEffectorModel.dataIsPrivate)
                    {
                        foreach (PageItem page in this.kxItem2PanelMap.Keys)
                        {
                            if (page != PageItem.System)
                            {
                                this.kxItem2PanelMap[page].UpdateUIFromData();
                            }
                        }
                    }
                }

                this.mSystemPanel.BindingSystemModel(this.kxEffectorModel.system);

                if (this.kxItem2ModuleMap == null)
                {
                    this.kxItem2ModuleMap = new Dictionary<PageItem, ModuleBase>
                    {
                        {PageItem.Music, this.kxEffectorModel.music},
                        {PageItem.Mic, this.kxEffectorModel.mic},
                        {PageItem.Effect, this.kxEffectorModel.echo},
                        {PageItem.MainOutput, this.kxEffectorModel.mainOutput},
                        {PageItem.Aux1Output, this.kxEffectorModel.aux1Output},
                        {PageItem.Aux2Output, this.kxEffectorModel.aux2Output},
                        {PageItem.SurOutput, this.kxEffectorModel.surOutput},
                    };
                }

                if (this.kxModule2ItemMap == null)
                {
                    this.kxModule2ItemMap = new Dictionary<ModuleBase, PageItem>
                    {
                        {this.kxEffectorModel.music, PageItem.Music},
                        {this.kxEffectorModel.mic, PageItem.Mic},
                        {this.kxEffectorModel.mainOutput, PageItem.MainOutput},
                        {this.kxEffectorModel.aux1Output, PageItem.Aux1Output},
                        {this.kxEffectorModel.aux2Output, PageItem.Aux2Output},
                        {this.kxEffectorModel.surOutput, PageItem.SurOutput},
                    };
                }

                this.currItem2ModuleMap = this.kxItem2ModuleMap;
                this.currItem2PanelMap = this.kxItem2PanelMap;

                if (this.kxNavigationBar == null)
                {
                    this.kxNavigationBar = new KXNavigationBar();
                    this.kxNavigationBar.Init();
                    this.kxNavigationBar.CheckedChanged += new System.EventHandler<System.EventArgs>(this.navigationBar_CheckedChanged);
                    this.kxNavigationBar.Binding(this.mController, this.eventBus);
                }
                else
                {
                    this.kxNavigationBar.UpdateUIFromData();
                }

                if (this.navigationBar != null && this.navigationBar != this.kxNavigationBar)
                {
                    if (this.panelToolBar.Contains(this.navigationBar))
                    {
                        this.panelToolBar.Controls.Remove(this.navigationBar);
                    }
                }

                this.navigationBar = this.kxNavigationBar;
                if (!this.panelToolBar.Contains(this.navigationBar))
                {
                    this.panelToolBar.Controls.Add(this.navigationBar, 0, 0);
                }
            }
            else if (currEffectorFamily_ == MEffectorFamily.VKxSCM)
            {
                Debug.Assert(this.vkxscmEffectorModel != null);

                if (this.vkxscmItem2PanelMap == null)
                {
                    this.vkxscmItem2PanelMap = new Dictionary<PageItem, PanelBase>()
                    {
                        {PageItem.Music, new VKxSCMMusicPanel()},
                        {PageItem.Mic, new VKxSCMMicPanel()},
                        {PageItem.MicExciter, new VKxSCMMicExciterPanel()},
                        {PageItem.Echo, new VKxSCMEchoPanel()},
                        {PageItem.Reverb, new VKxSCMReverbPanel()},
                        {PageItem.MainOutput, new VKxSCMMainOutputPanel()},
                        {PageItem.Aux1Output, new VKxSCMAux1OutputPanel()},
                        {PageItem.Aux2Output, new VKxSCMAux2OutputPanel()},
                        {PageItem.SurOutput, new VKxSCMSurOutputPanel()},
                        {PageItem.System, this.mSystemPanel},
                    };

                    foreach (PanelBase panel in this.vkxscmItem2PanelMap.Values)
                    {
                        panel.Location = controlPanelLeftTop;

                        panel.BindingController(this.mController, this.eventBus);
                    }
                }
                else
                {
                    if (!this.vkxscmEffectorModel.dataIsPrivate)
                    {
                        foreach (PageItem page in this.vkxscmItem2PanelMap.Keys)
                        {
                            if (page != PageItem.System)
                            {
                                this.vkxscmItem2PanelMap[page].UpdateUIFromData();
                            }
                        }
                    }
                }

                this.mSystemPanel.BindingSystemModel(this.vkxscmEffectorModel.system);

                if (this.vkxscmItem2ModuleMap == null)
                {
                    this.vkxscmItem2ModuleMap = new Dictionary<PageItem, ModuleBase>
                    {
                        {PageItem.Music, this.vkxscmEffectorModel.music},
                        {PageItem.Mic, this.vkxscmEffectorModel.mic},
                        {PageItem.MicExciter, this.vkxscmEffectorModel.micExciter},
                        {PageItem.Echo, this.vkxscmEffectorModel.echo},
                        {PageItem.Reverb, this.vkxscmEffectorModel.reverb},
                        {PageItem.MainOutput, this.vkxscmEffectorModel.mainOutput},
                        {PageItem.Aux1Output, this.vkxscmEffectorModel.aux1Output},
                        {PageItem.Aux2Output, this.vkxscmEffectorModel.aux2Output},
                        {PageItem.SurOutput, this.vkxscmEffectorModel.surOutput},
                    };
                }

                if (this.vkxscmModule2ItemMap == null)
                {
                    this.vkxscmModule2ItemMap = new Dictionary<ModuleBase, PageItem>
                    {
                        {this.vkxscmEffectorModel.music, PageItem.Music},
                        {this.vkxscmEffectorModel.mic, PageItem.Mic},                        
                        {this.vkxscmEffectorModel.mainOutput, PageItem.MainOutput},
                        {this.vkxscmEffectorModel.aux1Output, PageItem.Aux1Output},
                        {this.vkxscmEffectorModel.aux2Output, PageItem.Aux2Output},
                        {this.vkxscmEffectorModel.surOutput, PageItem.SurOutput},
                    };
                }

                this.currItem2ModuleMap = this.vkxscmItem2ModuleMap;
                this.currItem2PanelMap = this.vkxscmItem2PanelMap;

                if (this.vkxscmNavigationBar == null)
                {
                    this.vkxscmNavigationBar = new VKxSCMNavigationBar();
                    this.vkxscmNavigationBar.Init();
                    this.vkxscmNavigationBar.CheckedChanged += new System.EventHandler<System.EventArgs>(this.navigationBar_CheckedChanged);
                    this.vkxscmNavigationBar.Binding(this.mController, this.eventBus);
                }
                else
                {
                    this.vkxscmNavigationBar.UpdateUIFromData();
                }

                if (this.navigationBar != null && this.navigationBar != this.vkxscmNavigationBar)
                {
                    if (this.panelToolBar.Contains(this.navigationBar))
                    {
                        this.panelToolBar.Controls.Remove(this.navigationBar);
                    }
                }

                this.navigationBar = this.vkxscmNavigationBar;
                if (!this.panelToolBar.Contains(this.navigationBar))
                {
                    this.panelToolBar.Controls.Add(this.navigationBar, 0, 0);
                }
            }
            else if (currEffectorFamily_ == MEffectorFamily.VKxDCM)
            {
                Debug.Assert(this.vkxdcmEffectorModel != null);

                if (this.vkxdcmItem2PanelMap == null)
                {
                    this.vkxdcmItem2PanelMap = new Dictionary<PageItem, PanelBase>()
                    {
                        {PageItem.Music, new VKxDCMMusicPanel()},
                        {PageItem.MicA, new VKxDCMMicAPanel()},
                        {PageItem.MicB, new VKxDCMMicBPanel()},
                        {PageItem.MicExciter, new VKxDCMMicExciterPanel()},
                        {PageItem.Echo, new VKxDCMEchoPanel()},
                        {PageItem.Reverb, new VKxDCMReverbPanel()},
                        {PageItem.MainOutput, new VKxDCMMainOutputPanel()},
                        {PageItem.Aux1Output, new VKxDCMAux1OutputPanel()},
                        {PageItem.Aux2Output, new VKxDCMAux2OutputPanel()},
                        {PageItem.SurOutput, new VKxDCMSurOutputPanel()},
                        {PageItem.System, this.mSystemPanel},
                    };

                    foreach (PanelBase panel in this.vkxdcmItem2PanelMap.Values)
                    {
                        panel.Location = controlPanelLeftTop;

                        panel.BindingController(this.mController, this.eventBus);
                    }
                }
                else
                {
                    if (!this.vkxdcmEffectorModel.dataIsPrivate)
                    {
                        foreach (PageItem page in this.vkxdcmItem2PanelMap.Keys)
                        {
                            if (page != PageItem.System)
                            {
                                this.vkxdcmItem2PanelMap[page].UpdateUIFromData();
                            }
                        }
                    }
                }

                this.mSystemPanel.BindingSystemModel(this.vkxdcmEffectorModel.system);

                if (this.vkxdcmItem2ModuleMap == null)
                {
                    this.vkxdcmItem2ModuleMap = new Dictionary<PageItem, ModuleBase>
                    {
                        {PageItem.Music, this.vkxdcmEffectorModel.music},
                        {PageItem.MicA, this.vkxdcmEffectorModel.micA},
                        {PageItem.MicB, this.vkxdcmEffectorModel.micB},
                        {PageItem.MicExciter, this.vkxdcmEffectorModel.micExciter},
                        {PageItem.Echo, this.vkxdcmEffectorModel.echo},
                        {PageItem.Reverb, this.vkxdcmEffectorModel.reverb},
                        {PageItem.MainOutput, this.vkxdcmEffectorModel.mainOutput},
                        {PageItem.Aux1Output, this.vkxdcmEffectorModel.aux1Output},
                        {PageItem.Aux2Output, this.vkxdcmEffectorModel.aux2Output},
                        {PageItem.SurOutput, this.vkxdcmEffectorModel.surOutput},
                    };
                }

                if (this.vkxdcmModule2ItemMap == null)
                {
                    this.vkxdcmModule2ItemMap = new Dictionary<ModuleBase, PageItem>
                    {
                        {this.vkxdcmEffectorModel.music, PageItem.Music},
                        {this.vkxdcmEffectorModel.micA, PageItem.MicA},                        
                        {this.vkxdcmEffectorModel.micB, PageItem.MicB},                        
                        {this.vkxdcmEffectorModel.mainOutput, PageItem.MainOutput},
                        {this.vkxdcmEffectorModel.aux1Output, PageItem.Aux1Output},
                        {this.vkxdcmEffectorModel.aux2Output, PageItem.Aux2Output},
                        {this.vkxdcmEffectorModel.surOutput, PageItem.SurOutput},
                    };
                }

                this.currItem2ModuleMap = this.vkxdcmItem2ModuleMap;
                this.currItem2PanelMap = this.vkxdcmItem2PanelMap;

                if (this.vkxdcmNavigationBar == null)
                {
                    this.vkxdcmNavigationBar = new VKxDCMNavigationBar();
                    this.vkxdcmNavigationBar.Init();
                    this.vkxdcmNavigationBar.CheckedChanged += new System.EventHandler<System.EventArgs>(this.navigationBar_CheckedChanged);
                    this.vkxdcmNavigationBar.Binding(this.mController, this.eventBus);
                }
                else
                {
                    this.vkxdcmNavigationBar.UpdateUIFromData();
                }

                if (this.navigationBar != null && this.navigationBar != this.vkxdcmNavigationBar)
                {
                    if (this.panelToolBar.Contains(this.navigationBar))
                    {
                        this.panelToolBar.Controls.Remove(this.navigationBar);
                    }
                }

                this.navigationBar = this.vkxdcmNavigationBar;
                if (!this.panelToolBar.Contains(this.navigationBar))
                {
                    this.panelToolBar.Controls.Add(this.navigationBar, 0, 0);
                }
            }
            else if (currEffectorFamily_ == MEffectorFamily.VKxPII)
            {
                Debug.Assert(this.vkxPIIEffectorModel != null);

                if (this.vkxPIIItem2PanelMap == null)
                {
                    this.vkxPIIItem2PanelMap = new Dictionary<PageItem, PanelBase>()
                    {
                        {PageItem.Music, new VKxPIIMusicPanel()},
                        {PageItem.Mic, new VKxPIIMicPanel()},
                        {PageItem.MicExciter, new VKxPIIMicExciterPanel()},
                        {PageItem.Echo, new VKxPIIEchoPanel()},
                        {PageItem.Reverb, new VKxPIIReverbPanel()},
                        {PageItem.MainOutput, new VKxPIIMainOutputPanel()},
                        {PageItem.Aux1Output, new VKxPIIAux1OutputPanel()},
                        {PageItem.Aux2Output, new VKxPIIAux2OutputPanel()},
                        {PageItem.SurOutput, new VKxPIISurOutputPanel()},
                        {PageItem.System, this.mSystemPanel},
                    };

                    foreach (PanelBase panel in this.vkxPIIItem2PanelMap.Values)
                    {
                        panel.Location = controlPanelLeftTop;

                        panel.BindingController(this.mController, this.eventBus);
                    }
                }
                else
                {
                    if (!this.vkxPIIEffectorModel.dataIsPrivate)
                    {
                        foreach (PageItem page in this.vkxPIIItem2PanelMap.Keys)
                        {
                            if (page != PageItem.System)
                            {
                                this.vkxPIIItem2PanelMap[page].UpdateUIFromData();
                            }
                        }
                    }
                }

                this.mSystemPanel.BindingSystemModel(this.vkxPIIEffectorModel.system);

                if (this.vkxPIIItem2ModuleMap == null)
                {
                    this.vkxPIIItem2ModuleMap = new Dictionary<PageItem, ModuleBase>
                    {
                        {PageItem.Music, this.vkxPIIEffectorModel.music},
                        {PageItem.Mic, this.vkxPIIEffectorModel.mic},
                        {PageItem.MicExciter, this.vkxPIIEffectorModel.micExciter},
                        {PageItem.Echo, this.vkxPIIEffectorModel.echo},
                        {PageItem.Reverb, this.vkxPIIEffectorModel.reverb},
                        {PageItem.MainOutput, this.vkxPIIEffectorModel.mainOutput},
                        {PageItem.Aux1Output, this.vkxPIIEffectorModel.aux1Output},
                        {PageItem.Aux2Output, this.vkxPIIEffectorModel.aux2Output},
                        {PageItem.SurOutput, this.vkxPIIEffectorModel.surOutput},
                    };
                }

                if (this.vkxPIIModule2ItemMap == null)
                {
                    this.vkxPIIModule2ItemMap = new Dictionary<ModuleBase, PageItem>
                    {
                        {this.vkxPIIEffectorModel.music, PageItem.Music},
                        {this.vkxPIIEffectorModel.mic, PageItem.Mic},                        
                        {this.vkxPIIEffectorModel.mainOutput, PageItem.MainOutput},
                        {this.vkxPIIEffectorModel.aux1Output, PageItem.Aux1Output},
                        {this.vkxPIIEffectorModel.aux2Output, PageItem.Aux2Output},
                        {this.vkxPIIEffectorModel.surOutput, PageItem.SurOutput},
                    };
                }

                this.currItem2ModuleMap = this.vkxPIIItem2ModuleMap;
                this.currItem2PanelMap = this.vkxPIIItem2PanelMap;

                if (this.vkxPIINavigationBar == null)
                {
                    this.vkxPIINavigationBar = new VKxPIINavigationBar();
                    this.vkxPIINavigationBar.Init();
                    this.vkxPIINavigationBar.CheckedChanged += new System.EventHandler<System.EventArgs>(this.navigationBar_CheckedChanged);
                    this.vkxPIINavigationBar.Binding(this.mController, this.eventBus);
                }
                else
                {
                    this.vkxPIINavigationBar.UpdateUIFromData();
                }

                if (this.navigationBar != null && this.navigationBar != this.vkxPIINavigationBar)
                {
                    if (this.panelToolBar.Contains(this.navigationBar))
                    {
                        this.panelToolBar.Controls.Remove(this.navigationBar);
                    }
                }

                this.navigationBar = this.vkxPIINavigationBar;
                if (!this.panelToolBar.Contains(this.navigationBar))
                {
                    this.panelToolBar.Controls.Add(this.navigationBar, 0, 0);
                }
            }
            else if (currEffectorFamily_ == MEffectorFamily.VKxPIIDCM)
            {
                Debug.Assert(this.vkxPIIdcmEffectorModel != null);

                if (this.vkxPIIdcmItem2PanelMap == null)
                {
                    this.vkxPIIdcmItem2PanelMap = new Dictionary<PageItem, PanelBase>()
                    {
                        {PageItem.Music, new VKxPIIDCMMusicPanel()},
                        {PageItem.MicA, new VKxPIIDCMMicAPanel()},
                        {PageItem.MicB, new VKxPIIDCMMicBPanel()},
                        {PageItem.MicExciter, new VKxPIIDCMMicExciterPanel()},
                        {PageItem.Echo, new VKxPIIDCMEchoPanel()},
                        {PageItem.Reverb, new VKxPIIDCMReverbPanel()},
                        {PageItem.MainOutput, new VKxPIIDCMMainOutputPanel()},
                        {PageItem.Aux1Output, new VKxPIIDCMAux1OutputPanel()},
                        {PageItem.Aux2Output, new VKxPIIDCMAux2OutputPanel()},
                        {PageItem.SurOutput, new VKxPIIDCMSurOutputPanel()},
                        {PageItem.System, this.mSystemPanel},
                    };

                    foreach (PanelBase panel in this.vkxPIIdcmItem2PanelMap.Values)
                    {
                        panel.Location = controlPanelLeftTop;

                        panel.BindingController(this.mController, this.eventBus);
                    }
                }
                else
                {
                    if (!this.vkxPIIdcmEffectorModel.dataIsPrivate)
                    {
                        foreach (PageItem page in this.vkxPIIdcmItem2PanelMap.Keys)
                        {
                            if (page != PageItem.System)
                            {
                                this.vkxPIIdcmItem2PanelMap[page].UpdateUIFromData();
                            }
                        }
                    }
                }

                this.mSystemPanel.BindingSystemModel(this.vkxPIIdcmEffectorModel.system);

                if (this.vkxPIIdcmItem2ModuleMap == null)
                {
                    this.vkxPIIdcmItem2ModuleMap = new Dictionary<PageItem, ModuleBase>
                    {
                        {PageItem.Music, this.vkxPIIdcmEffectorModel.music},
                        {PageItem.MicA, this.vkxPIIdcmEffectorModel.micA},
                        {PageItem.MicB, this.vkxPIIdcmEffectorModel.micB},
                        {PageItem.MicExciter, this.vkxPIIdcmEffectorModel.micExciter},
                        {PageItem.Echo, this.vkxPIIdcmEffectorModel.echo},
                        {PageItem.Reverb, this.vkxPIIdcmEffectorModel.reverb},
                        {PageItem.MainOutput, this.vkxPIIdcmEffectorModel.mainOutput},
                        {PageItem.Aux1Output, this.vkxPIIdcmEffectorModel.aux1Output},
                        {PageItem.Aux2Output, this.vkxPIIdcmEffectorModel.aux2Output},
                        {PageItem.SurOutput, this.vkxPIIdcmEffectorModel.surOutput},
                    };
                }

                if (this.vkxPIIdcmModule2ItemMap == null)
                {
                    this.vkxPIIdcmModule2ItemMap = new Dictionary<ModuleBase, PageItem>
                    {
                        {this.vkxPIIdcmEffectorModel.music, PageItem.Music},
                        {this.vkxPIIdcmEffectorModel.micA, PageItem.MicA},                        
                        {this.vkxPIIdcmEffectorModel.micB, PageItem.MicB},                        
                        {this.vkxPIIdcmEffectorModel.mainOutput, PageItem.MainOutput},
                        {this.vkxPIIdcmEffectorModel.aux1Output, PageItem.Aux1Output},
                        {this.vkxPIIdcmEffectorModel.aux2Output, PageItem.Aux2Output},
                        {this.vkxPIIdcmEffectorModel.surOutput, PageItem.SurOutput},
                    };
                }

                this.currItem2ModuleMap = this.vkxPIIdcmItem2ModuleMap;
                this.currItem2PanelMap = this.vkxPIIdcmItem2PanelMap;

                if (this.vkxPIIdcmNavigationBar == null)
                {
                    this.vkxPIIdcmNavigationBar = new VKxPIIDCMNavigationBar();
                    this.vkxPIIdcmNavigationBar.Init();
                    this.vkxPIIdcmNavigationBar.CheckedChanged += new System.EventHandler<System.EventArgs>(this.navigationBar_CheckedChanged);
                    this.vkxPIIdcmNavigationBar.Binding(this.mController, this.eventBus);
                }
                else
                {
                    this.vkxPIIdcmNavigationBar.UpdateUIFromData();
                }

                if (this.navigationBar != null && this.navigationBar != this.vkxPIIdcmNavigationBar)
                {
                    if (this.panelToolBar.Contains(this.navigationBar))
                    {
                        this.panelToolBar.Controls.Remove(this.navigationBar);
                    }
                }

                this.navigationBar = this.vkxPIIdcmNavigationBar;
                if (!this.panelToolBar.Contains(this.navigationBar))
                {
                    this.panelToolBar.Controls.Add(this.navigationBar, 0, 0);
                }
            }
            else if (currEffectorFamily_ == MEffectorFamily.VX)
            {
                Debug.Assert(this.vxEffectorModel != null);

                if (this.vxItem2PanelMap == null)
                {
                    this.vxItem2PanelMap = new Dictionary<PageItem, PanelBase>()
                    {
                        {PageItem.Music, new VXMusicPanel()},
                        {PageItem.Mic, new VXMicPanel()},
                        {PageItem.Effect, new VXEffectPanel()},
                        {PageItem.MainOutput, new VXMainOutputPanel()},
                        {PageItem.Aux1Output, new VXAux1OutputPanel()},
                        {PageItem.Aux2Output, new VXAux2OutputPanel()},
                        {PageItem.SurOutput, new VXSurOutputPanel()},
                        {PageItem.System, this.mSystemPanel},
                        {PageItem.Monitor, new VXMonitorPanel()},
                    };

                    foreach (PanelBase panel in this.vxItem2PanelMap.Values)
                    {
                        panel.Location = controlPanelLeftTop;

                        panel.BindingController(this.mController, this.eventBus);
                    }
                }
                else
                {
                    if(!this.vxEffectorModel.dataIsPrivate)
                    {
                        foreach (PageItem page in this.vxItem2PanelMap.Keys)
                        {
                            if (page != PageItem.System)
                            {
                                this.vxItem2PanelMap[page].UpdateUIFromData();
                            }
                        }
                    }
                }

                this.mSystemPanel.BindingSystemModel(this.vxEffectorModel.system);

                if(this.vxItem2ModuleMap == null)
                {
                    this.vxItem2ModuleMap = new Dictionary<PageItem, ModuleBase>
                    {
                        {PageItem.Music, this.vxEffectorModel.music},
                        {PageItem.Mic, this.vxEffectorModel.mic},
                        {PageItem.Effect, this.vxEffectorModel.echo},
                        {PageItem.MainOutput, this.vxEffectorModel.mainOutput},
                        {PageItem.Aux1Output, this.vxEffectorModel.aux1Output},
                        {PageItem.Aux2Output, this.vxEffectorModel.aux2Output},
                        {PageItem.SurOutput, this.vxEffectorModel.surOutput},
                    };
                }

                if(this.vxModule2ItemMap == null)
                {
                    this.vxModule2ItemMap = new Dictionary<ModuleBase, PageItem>
                    {
                        {this.vxEffectorModel.mainOutput, PageItem.MainOutput},
                        {this.vxEffectorModel.aux1Output, PageItem.Aux1Output},
                        {this.vxEffectorModel.aux2Output, PageItem.Aux2Output},
                        {this.vxEffectorModel.surOutput, PageItem.SurOutput},
                    };
                }

                this.currItem2ModuleMap = this.vxItem2ModuleMap;
                this.currItem2PanelMap = this.vxItem2PanelMap;

                if(this.vxNavigationBar == null)
                {
                    this.vxNavigationBar = new VXNavigationBar();
                    this.vxNavigationBar.Init();
                    this.vxNavigationBar.CheckedChanged += new System.EventHandler<System.EventArgs>(this.navigationBar_CheckedChanged);
                    this.vxNavigationBar.Binding(this.mController, this.eventBus);
                }
                else
                {
                    this.vxNavigationBar.UpdateUIFromData();
                }

                if(this.navigationBar != null && this.navigationBar != this.vxNavigationBar)
                {
                    if(this.panelToolBar.Contains(this.navigationBar))
                    {
                        this.panelToolBar.Controls.Remove(this.navigationBar);
                    }
                }

                this.navigationBar = this.vxNavigationBar;
                if(!this.panelToolBar.Contains(this.navigationBar))
                {
                    this.panelToolBar.Controls.Add(this.navigationBar, 0, 0);
                }
            }
            else if(currEffectorFamily_ == MEffectorFamily.MX)
            {
                Debug.Assert(this.mxEffectorModel != null);

                if (this.mxItem2PanelMap == null)
                {
                    this.mxItem2PanelMap = new Dictionary<PageItem, PanelBase>()
                    {
                        {PageItem.Music, new MXMusicPanel()},
                        {PageItem.MusicDeq, new MXMusicDeqPanel()},
                        {PageItem.Mic, new MXMicPanel()},
                        {PageItem.MicDeq, new MXMicDeqPanel()},
                        {PageItem.Effect, new MXEffectPanel()},
                        {PageItem.MainOutput, new MXMainOutputPanel()},
                        {PageItem.Aux1Output, new MXAux1OutputPanel()},
                        {PageItem.Aux2Output, new MXAux2OutputPanel()},
                        {PageItem.SurOutput, new MXSurOutputPanel()},
                        {PageItem.System, this.mSystemPanel},
                    };

                    foreach (PanelBase panel in this.mxItem2PanelMap.Values)
                    {
                        panel.Location = controlPanelLeftTop;

                        panel.BindingController(this.mController, this.eventBus);
                    }
                }
                else
                {
                    if (!this.mxEffectorModel.dataIsPrivate)
                    {
                        foreach (PageItem page in this.mxItem2PanelMap.Keys)
                        {
                            if (page != PageItem.System)
                            {
                                this.mxItem2PanelMap[page].UpdateUIFromData();
                            }
                        }
                    }
                }

                this.mSystemPanel.BindingSystemModel(this.mxEffectorModel.system);

                if (this.mxItem2ModuleMap == null)
                {
                    this.mxItem2ModuleMap = new Dictionary<PageItem, ModuleBase>
                    {
                        {PageItem.Music, this.mxEffectorModel.music},
                        {PageItem.MusicDeq, this.mxEffectorModel.musicDeq},
                        {PageItem.Mic, this.mxEffectorModel.mic},
                        {PageItem.MicDeq, this.mxEffectorModel.micDeq},
                        {PageItem.Effect, this.mxEffectorModel.echo},
                        {PageItem.MainOutput, this.mxEffectorModel.mainOutput},
                        {PageItem.Aux1Output, this.mxEffectorModel.aux1Output},
                        {PageItem.Aux2Output, this.mxEffectorModel.aux2Output},
                        {PageItem.SurOutput, this.mxEffectorModel.surOutput},
                    };
                }

                this.currItem2ModuleMap = this.mxItem2ModuleMap;
                this.currItem2PanelMap = this.mxItem2PanelMap;

                if (this.mxNavigationBar == null)
                {
                    this.mxNavigationBar = new MXNavigationBar();
                    this.mxNavigationBar.Init();
                    this.mxNavigationBar.CheckedChanged += new System.EventHandler<System.EventArgs>(this.navigationBar_CheckedChanged);
                    this.mxNavigationBar.Binding(this.mController, this.eventBus);
                }
                else
                {
                    this.mxNavigationBar.UpdateUIFromData();
                }

                if (this.navigationBar != null && this.navigationBar != this.mxNavigationBar)
                {
                    if (this.panelToolBar.Contains(this.navigationBar))
                    {
                        this.panelToolBar.Controls.Remove(this.navigationBar);
                    }
                }

                this.navigationBar = this.mxNavigationBar;
                if (!this.panelToolBar.Contains(this.navigationBar))
                {
                    this.panelToolBar.Controls.Add(this.navigationBar, 0, 0);
                }
            }

            int navigationBarContainerWidth = 551;
            //if (currEffectorFamily_ == MEffectorFamily.VKxSDM)
            //{
            //    navigationBarContainerWidth = 551;
            //}
            //else
            if (currEffectorFamily_ == MEffectorFamily.KX)
            {
                navigationBarContainerWidth = 547;
            }
            else if(currEffectorFamily_ == MEffectorFamily.MX)
            {
                navigationBarContainerWidth = 580;
            }
            else if (currEffectorFamily_ == MEffectorFamily.VKxSCM)
            {
                navigationBarContainerWidth = 594 + 4;
            }
            else if (currEffectorFamily_ == MEffectorFamily.VKxDCM)
            {
                navigationBarContainerWidth = 660 + 4;
            }
            else if (currEffectorFamily_ == MEffectorFamily.VKxPII)
            {
                navigationBarContainerWidth = 606 + 4;
            }
            else if(currEffectorFamily_ == MEffectorFamily.VKxPIIDCM)
            {
                navigationBarContainerWidth = 660 + 4;
            }
            this.panelToolBar.ColumnStyles[0] = new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, navigationBarContainerWidth);

            this.systemControlPanel.showTimbre = currEffectorFamily_ == MEffectorFamily.KX;

            if (isNeedToSwitchToSystemPanel)
            {
                this.navigationBar.initPageItem = PageItem.System;
                this.lastPageItem = PageItem.System;
                this.InitPage(lastPageItem);
            }
            else
            {
                this.lastPageItem = this.navigationBar.pageItem;

                if (this.lastPageItem == PageItem.Monitor && this.mController.currEffectorFamily == MEffectorFamily.VKxS)
                {
                    this.lastPageItem = PageItem.Music;
                }
                
                this.InitPage(lastPageItem);
            }
        }

        private void InitTheme()
        {
            //this.panelMusicInput.FillColor = SkinManager.CurrentSkin.PanelBackColor;
            this.btnConnect.BackColor = this.title.BackColor;
            this.panelToolBar.BackColor = SkinManager.CurrentSkin.ToolbarBackColor;
            this.systemControlPanel.FillColor = SkinManager.CurrentSkin.ToolbarBackColor;
            //this.systemControlPanel.FillColor = SkinManager.CurrentSkin.ToolbarBackColor;
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            //AEC.Log.OutputLine("FormMain_Load");            
            //绑定Title
            this.title.BindingForm(this);
            this.title.btnMenu.MouseClick += btnMenu_MouseClick;

            this.InitTheme();

            this.InitSwitchDevcieModelMenuItems();

            //初始化eqGraphicPanle
            this.InitGraphicPanel();

            //根据效果器家族初始化界面
            this.UpdateEffectorFamily(this.currEffectorFamily);

            this.systemControlPanel.Binding(this.mController, this.eventBus);
            
            this.RegisterEventBus();
            
            this.UpdateConnectBtnStatus();

            this.UpdateTsmi();
            this.UpdateDisplayLanguage();
            //this.UpdateEffectorModel();
            //syncContext.Post(AsyncUpdate, null);

            //isLockParams = false;
            //LockParams(isLockParams);
#if COMM_TEST
            this.InitCommTestUI();
#endif
        }

        private void btnMenu_MouseClick(object sender, MouseEventArgs e)
        {
            this.tsmiDeviceModels.Visible = AppConfig.IsShowSwitchDeviceModelMenuItem();
            this.tsmiDeviceModels.Enabled = mController.commState == CommState.Disconnected;

            this.tsmiOnlineHelp.Visible = AppConfig.isSupportOnlineHelp;
            this.tsmiOnlineHelp.Enabled = mController.commState == CommState.Disconnected;

            this.tsmiBluetoothRename.Visible = AppConfig.isSupportBluetoothRename && e.Button == System.Windows.Forms.MouseButtons.Right && ((Control.ModifierKeys & Keys.Control) != 0);
            this.tsmiBluetoothRename.Enabled = mController.commState == CommState.Disconnected;

            this.tsmiFirmwareUpdate.Visible = AppConfig.isSupportFirmwareUpdate;
            this.tsmiFirmwareUpdate.Enabled = mController.commState == CommState.Disconnected;

            this.tsmiActivateDevice.Visible = AppConfig.isSupportActivateDevice && e.Button == System.Windows.Forms.MouseButtons.Right && ((Control.ModifierKeys & Keys.Shift) != 0);
            this.tsmiActivateDevice.Enabled = mController.commState == CommState.Disconnected;

            this.UpdateDevcieModelMenuItems();

            contextMenuStrip.Show(this.title.btnMenu, new Point(0, this.title.btnMenu.Height + 2));
        }

        private void FormProMode_FormClosing(object sender, FormClosingEventArgs e)
        {
            //this.eventBus.NotifyObeserver(new StopReadLevelArgs());
            Tools.LogHelper.FlushBuffers();

            if (this.mController.commState == CommState.Connecting)
            {
                gtMessageBox.ShowMessage(this, localization[Localization.systemIsBusyAndTryAgain]);
                e.Cancel = true;
                return;
            }

            this.DisconnectDevice();

            if (this.audioAnalysis.rtaIsON)
            {
                this.audioAnalysis.TurnOffRTA();
                this.rtaSwitch.isOn = false;
            }

            if (this.formRTAOption != null)
            {
                this.formRTAOption.Hide();
            }

            this.HideCommForm();
            this.Hide();
            e.Cancel = !isActuallyClose;
            //AEC.Log.OutputLine("FormProMode_FormClosing");
        }

        private void FormProMode_FormClosed(object sender, FormClosedEventArgs e)
        {
            //this.v5Controller.commSvc.StopService();
            if (this.formRTAOption != null)
            {
                this.formRTAOption.Close();
            }
            
            this.RemoveEventBus();
            this.CloseCommForm();
            this.audioAnalysis.StopWrok();

            AEC.Misc.Config.GetInstance().SaveToConfigFile();

            this.mController.StopService();
            
            //AEC.Log.OutputLine("FormProMode_FormClosed");
        }

        private void FormProMode_Shown(object sender, EventArgs e)
        {
            AutoConnectDevice();

            var bgwInitAudioAnalysis = new System.ComponentModel.BackgroundWorker();
            bgwInitAudioAnalysis.DoWork += delegate(object s, DoWorkEventArgs dwe)
            {
                audioAnalysis.Init(rtaEventBus);
                //Console.WriteLine("InitAudioAnalysis");
            };
            bgwInitAudioAnalysis.RunWorkerCompleted += delegate(object s, RunWorkerCompletedEventArgs rwce)
            {
                //Console.WriteLine("RunWorkerCompleted");
                syncContext.Post(delegate(object state)
                {
                    this.rtaSwitch.Enabled = true;
                }, null);
            };
            bgwInitAudioAnalysis.RunWorkerAsync();

            rtaEventBus.RegisterHandler(RTAEventBusHandler);
        }

        private void ControllerEventHandler(object sender, EventBusArgs e)
        {
            syncContext.Post(CommEventHandlerCallback, e);
        }

        public void RTAEventBusHandler(object sender, EventBusArgs e)
        {
            syncContext.Post(RTAEventBusHandlerCallback, e);
        }

        public new void Show()
        {
            base.Show();
            if (this.isFirstStart)
            {
                this.isFirstStart = false;
            }
            else
            {
                AutoConnectDevice();
            }
        }

        public new void Close()
        {
            this.isActuallyClose = true;
            base.Close();
        }

        private void AutoConnectDevice()
        {
            if (AEC.Misc.Config.GetInstance().isAutoConnection)
            {
                syncContext.Post(AsyncConnectDevice, null);
            }
        }

        private void RTAEventBusHandlerCallback(object state)
        {
            EventBusArgs e = state as EventBusArgs;
            switch (e.type)
            {
                case EventType.RTATryOperate:
                    //Console.WriteLine("FormMain Try Operate");
                    //syncContext.Post(delegate(object state1)
                    //{
                    //    RTATryOperateCallback((e as RTATryOperateArgs).rtaOperate);
                    //}, null);
                    //Console.WriteLine("EventType.RTATryOperate...");
                    RTATryOperateCallback((e as RTATryOperateArgs).rtaOperate);
                    break;

                case EventType.RTAOperateCompleted:
                    //Console.WriteLine("FormMain Operate Completed");
                    RTAOperateCompletedCallback(e as RTAOperateCompletedArgs);
                    break;

                case EventType.UpdateRTADisplay:
                    if(this.WindowState != FormWindowState.Minimized && lastPageItem != PageItem.System)
                    {
                        this.eqGraphicPanel.UpdateGraphic();
                    }
                    break;
            }
        }

        private void RTATryOperateCallback(RTAOperate rtaOperate)
        {
            switch (rtaOperate)
            {
                case RTAOperate.TurnOnRTA:
                    this.rtaSwitch.Enabled = false;
                    this.audioAnalysis.TurnOnRTA();
                    break;

                case RTAOperate.TurnOffRTA:
                    this.rtaSwitch.Enabled = false;
                    this.audioAnalysis.TurnOffRTA();
                    break;
            }
        }

        private void RTAOperateCompletedCallback(RTAOperateCompletedArgs e)
        {
            switch(e.operate)
            {
                case RTAOperate.TurnOnRTA:
                    this.rtaSwitch.Enabled = true;
                    this.rtaSwitch.isOn = e.result.success;
                    if(!e.result.success)
                    {
                        gtMessageBox.ShowMessage(this, string.Format(localization[Localization.rta_FailedToStartCapturingAudio], e.result.error), "RTA", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    break;

                case RTAOperate.TurnOffRTA:
                    this.rtaSwitch.Enabled = true;
                    this.rtaSwitch.isOn = !e.result.success;
                    break;

                case RTAOperate.TurnOnOutput:
                    break;

                case RTAOperate.TurnOffOutput:
                    break;
            }
        }

        private void CommEventHandlerCallback(object state)
        {
            EventBusArgs e = state as EventBusArgs;
            //AEC.Log.OutputLine("CommEventHandlerCallback event:{0}", e.type);
            switch (e.type)
            {
                case EventType.CommStateChanged:
                    this.CommStateChangedHandler();
                    break;

                case EventType.CommInterrupted:
                    this.CommInterrupted();
                    break;

                case EventType.CommCompleted:
                    CommCompletedHandler((e as CommCompletedArgs).commResult);
                    break;

                case EventType.StartSaveMode:
                    StartSaveModeHandler();
                    break;

                case EventType.SaveModeCompleted:
                    SaveModeCompletedHandler();
                    break;
            }
        }

        private void CommCompletedHandler(CommResult commResult)
        {
            if (!commResult.success)
            {
                return;
            }
            if (commResult.process == CommProcess.ProModeConnectLoadData ||
                   commResult.process == CommProcess.ProModeCallDeviceMode ||
                   commResult.process == CommProcess.ProModeUploadData||
                commResult.process == CommProcess.ProModeSwitchDeviceWorkMode)
            {
                //this.UpdateUIFromData();
                this.UpdateUI((commResult.process == CommProcess.ProModeConnectLoadData || commResult.process == CommProcess.ProModeSwitchDeviceWorkMode) && this.currEffectorModel.deviceKeyIsLocked);
                this.SystemProtected(commResult.process);
            }

#if COMM_TEST
            if (commResult.process == CommProcess.ProModeConnectLoadData)
            {
                //联机成功后上传模式
                //if (this.chkTestComm.Checked)
                //{
                //    syncContext.Post(AsyncRandLoadDataToDevice, null);
                //}

                AEC.Tools.LogHelper.WriteDebug("联机加载数据完成");

                mController.SeralizeEffectorModelToFile(this.mController.currEffectorModel, this.mController.customModeDataFileManager.GetPresetFileFullPath(this.mController.mEffectorModel.currModelType, "test_comm_init_" + AEC.Tools.StringHelper.DateTime2String(DateTime.Now)));

                if (this.switchCommTest.IsChecked)
                {
                    syncContext.Post(AsyncRandUpdateAndSaveMode, null);
                }
                //else
                //{
                //    syncContext.Post(AsyncRandUpdateAndSaveMode, null);
                //}
            }
            else if (commResult.process == CommProcess.ProModeUploadData)
            {
                //if (this.chkTestComm.Checked)
                //{
                //    syncContext.Post(AsyncRandUpdateAndSaveMode, null);
                //}
            }
            else if (commResult.process == CommProcess.ProModeCallDeviceMode)
            {
                //随机调用模式完成后随机调用模式
                //if (this.chkTestComm.Checked)
                //{
                //    syncContext.Post(AsyncRandLoadDataToDevice, null);
                //}
                AEC.Tools.LogHelper.WriteDebug("调用模式数据完成");

                mController.SeralizeEffectorModelToFile(this.mController.currEffectorModel, this.mController.customModeDataFileManager.GetPresetFileFullPath(this.mController.mEffectorModel.currModelType, "test_comm_random_call_" + AEC.Tools.StringHelper.DateTime2String(DateTime.Now) + " " + commTestCount.ToString()));
                ++commTestCount;

                if (string.IsNullOrEmpty(modeDataHashCode))
                {
                    return;
                }

                string callModeDataHashCode = AEC.Core.Utils.BytesToHexStr(HashHelper.MD5(this.mController.currEffectorModel.Serialize(this.mController.mEffectorModel.currModelType, false)));

                if (vkxdcmEffectorModel != null)
                {
                    Log.OutputLine("Load bassExciter : {0}, exciter1 switch : {1}", vkxdcmEffectorModel.micA.bassExciter.on.v, vkxdcmEffectorModel.micA.exciter1.on.v);
                }

                if (modeDataHashCode == callModeDataHashCode)
                {
                    AEC.Log.OutputLine("加载模式数据检测OK，继续...");
                    if (this.switchCommTest.IsChecked)
                    {
                        AEC.Log.OutputLine("**********************************   {0}", commTestCount);
                        syncContext.Post(AsyncRandUpdateAndSaveMode, null);
                    }
                }
                else
                {
                    AEC.Log.OutputLine("加载模式数据出现了不一致！");
                }
            }
#endif
        }

        private void UpdateConnectBtnStatus()
        {
            //Log.OutputLine("UpdateConnectBtnStatus()..");
            if (this.mController.isConnected)
            {
                this.btnConnect.Text = localization[Localization.buttonConnection_Disconnect];
            }
            else
            {
                this.btnConnect.Text = localization[Localization.buttonConnection_Connect];
            }
            this.btnConnect.isConnected = this.mController.isConnected;
            this.btnConnect.UpdateEnabled(this.mController.commState != CommState.Connecting);
        }

        private void UpdateUI(bool isNeedToSwitchToSystemPanel)
        {
            //bool isNeedToSwitchToSystemPanel = commProcess == CommProcess.ProModeConnectLoadData && this.currEffectorModel.deviceKeyIsLocked;

            //Log.OutputLine("是否切换到系统页面:{0}", isNeedToSwitchToSystemPanel);

            this.UpdateEffectorModel();

            //型号家族发生变化
            if (this.currEffectorFamily != this.mController.currEffectorFamily)
            {
                this.currItem2PanelMap[lastPageItem].HidePanel();

                this.currEffectorFamily = this.mController.currEffectorFamily;

                this.UpdateEffectorFamily(this.currEffectorFamily, isNeedToSwitchToSystemPanel);

                this.UpdateDisplayLanguage();
            }
            else
            {
                if (!this.currEffectorModel.dataIsPrivate)
                {
                    if (isNeedToSwitchToSystemPanel && this.lastPageItem != PageItem.System)
                    {
                        this.eqGraphicPanel.Visible = false;

                        if (currItem2PanelMap.ContainsKey(lastPageItem))
                        {
                            currItem2PanelMap[lastPageItem].HidePanel();
                        }

                        lastPageItem = PageItem.System;
                        this.navigationBar.initPageItem = PageItem.System;
                        this.InitPage(lastPageItem);
                    }

                    foreach (PanelBase panel in this.currItem2PanelMap.Values)
                    {
                        if (panel != null)
                        {
                            panel.UpdateUIFromData();
                        }
                    }

                    if (this.eqGraphicPanel.Visible)
                    {
                        this.eqGraphicPanel.UpdateUIFromData();
                    }
                }

                if (this.lastPageItem == PageItem.Monitor && this.mController.currEffectorFamily == MEffectorFamily.VKxS)
                {
                    this.navigationBar.initPageItem = PageItem.Music;
                    this.InitPage(PageItem.Music);
                }

                this.mSystemPanel.OnEffectorModelChanged();
            }

            if (this.navigationBar != null)
            {
                this.navigationBar.UpdateUIFromData();
            }

            this.systemControlPanel.ReBindingSystem();

            this.CheckUnlinkMicAB();
        }

        private void UpdateUIFromData()
        {
            this.navigationBar.UpdateUIFromData();
            this.systemControlPanel.UpdateUIFromData();
            if(!this.currEffectorModel.dataIsPrivate)
            {
                foreach (PanelBase panel in this.currItem2PanelMap.Values)
                {
                    if (panel != null)
                    {
                        panel.UpdateUIFromData();
                    }
                }

                this.eqGraphicPanel.UpdateUIFromData();
            }
            else
            {
                systemPanel.UpdateUIFromData();
            }

            this.CheckUnlinkMicAB();
        }

        private void CheckUnlinkMicAB()
        {
            if (mController.currEffectorFamily == MEffectorFamily.VKxDCM && vkxdcmEffectorModel != null)
            {
                if (vkxdcmEffectorModel.isLinkMicAB && !vkxdcmEffectorModel.micA.IsEqual(vkxdcmEffectorModel.micB))
                {
                    (vkxdcmItem2PanelMap[PageItem.MicA] as VKxDCMMicPanel).UnlinkMicAB();
                    (vkxdcmItem2PanelMap[PageItem.MicB] as VKxDCMMicPanel).UnlinkMicAB();
                }
            }
        }

        private void SystemProtected(CommProcess commProcess)
        {
            EffectorModelBase effectorModel = this.mController.currEffector.effectorModel;
            
            if(commProcess == CommProcess.ProModeConnectLoadData || commProcess == CommProcess.ProModeSwitchDeviceWorkMode)
            {
                if (!effectorModel.deviceIsActivated)
                {
                    LockSystem(lockSystemType.deviceIsNotActivated);
                }
                else if (effectorModel.deviceKeyIsLocked)
                {
                    LockSystem(lockSystemType.keyboardIsLocked);
                }
                else if (effectorModel.dataIsPrivate)
                {
                    LockSystem(lockSystemType.dataIsPrivate);
                }
                else if(effectorModel.isUsageLimit)
                {
                    LockSystem(lockSystemType.usageLimitLock);
                }
            }
            else
            {
                if (effectorModel.dataIsPrivate)
                {
                    LockSystem(lockSystemType.dataIsPrivate);
                }
                else
                {
                    //Log.OutputLine("FormProMode()...SystemProtected()...{0}", commProcess);
                    UnlockSystem(false);
                }
            }
        }

        //检测设备状态,例如设备未激活
        public void CheckDeviceStateHandler(object state)
        {
            if (this.mController.isConnected)
            {
                if (!this.mController.currEffectorModel.deviceIsActivated)
                {
                    LockSystem(lockSystemType.deviceIsNotActivated);
                    ShowCommFormDeviceNotActivated();
                }
                else if (this.mController.currEffectorModel.deviceKeyIsLocked)
                {
                    //是否显示
                    LockSystem(lockSystemType.keyboardIsLocked);
                    ShowCommFormInputKeyPassword();
                }
            }
        }

        private bool isLockeded = false;
        private void LockSystem(lockSystemType lockType)
        {
            if (!isLockeded)
            {
                isLockeded = true;

                this.navigationBar.Enabled = false;

                if(lockType == lockSystemType.deviceIsNotActivated)
                {
                    if(this.navigationBar.pageItem != PageItem.System)
                    {
                        currItem2PanelMap[this.navigationBar.pageItem].Enabled = false;
                        this.eqGraphicPanel.Enabled = false;
                    }
                    else
                    {
                        this.systemPanel.Enabled = false;
                    }

                    this.systemControlPanel.Enabled = false;
                }
                else if (lockType == lockSystemType.keyboardIsLocked)
                {
                    if (this.navigationBar.pageItem != PageItem.System)
                    {
                        this.navigationBar.pageItem = PageItem.System;
                    }
                    
                    this.systemPanel.Enabled = false;
                    this.systemControlPanel.Enabled = false;
                }
                else if(lockType == lockSystemType.dataIsPrivate)
                {
                    if (this.navigationBar.pageItem != PageItem.System)
                    {
                        this.navigationBar.pageItem = PageItem.System;
                    }
                    this.systemControlPanel.Enabled = true;
                    this.systemPanel.Enabled = true;
                }
                else if(lockType == lockSystemType.usageLimitLock)
                {
                    if (this.navigationBar.pageItem != PageItem.System)
                    {
                        this.navigationBar.pageItem = PageItem.System;
                    }

                    this.systemPanel.Enabled = false;
                    this.systemControlPanel.Enabled = false;

                    (this.systemPanel as SystemPanel).ShowFromUsageLimitLock();
                }
            }
        }
        
        private void UnlockSystem(bool isResetData)
        {
            if (isLockeded)
            {
                isLockeded = false;

                this.navigationBar.Enabled = true;
                this.systemControlPanel.Enabled = true;

                if (this.navigationBar.pageItem != PageItem.System)
                {
                    currItem2PanelMap[this.navigationBar.pageItem].Enabled = true;
                    this.eqGraphicPanel.Enabled = true;
                }
                else
                {
                    this.systemPanel.Enabled = true;
                }

                if(isResetData)
                {
                    this.currEffectorModel.ResetNonSystemModuleData();  //重置数据
                    this.currEffectorModel.InitFreqRespAndEQGraphic();  //初始化频率响应

                    foreach (PanelBase panel in this.currItem2PanelMap.Values)
                    {
                        if (panel != null && panel != systemPanel)
                        {
                            //Log.OutputLine("panel name:{0}", panel.Name);
                            panel.UpdateUIFromData();
                        }
                    }
                }
            }

            this.Activate();
        }

        private void CommStateChangedHandler()
        {
            this.UpdateConnectBtnStatus();
            if(mController.commState == CommState.Disconnected)
            {
                this.CommIdleHandler();
            }
        }

        private void CommInterrupted()
        {
            this.UnlockSystem(true);
            this.UpdateText();
            if(this.Visible)
            {
                gtMessageBox.ShowMessage(this, localization[Localization.commInterrupted], "AEC", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void StartSaveModeHandler()
        {
            this.systemControlPanel.Enabled = false;
            this.btnConnect.UpdateEnabled(false);
            //AEC.Log.OutputLine("StartSaveModeHandler()");
        }

        private void SaveModeCompletedHandler()
        {
            this.CommIdleHandler();
            //AEC.Log.OutputLine("SaveModeCompleteHandler()");
        }

        private void CommIdleHandler()
        {
            this.systemControlPanel.Enabled = true;
            this.btnConnect.UpdateEnabled(true);
#if COMM_TEST
            Log.OutputLine("保存随机模式完成!");
            //AEC.Tools.LogHelper.WriteDebug("保存模式完成!");
            //然后随机调用模式
            if(this.switchCommTest.IsChecked)
            {
                syncContext.Post(AsyncRandCallMode, null);
            }
#endif
        }

        protected override void OnKeyUp(KeyEventArgs e)
        {
            base.OnKeyUp(e);
            if (e.KeyCode == lastKey)
            {
                lastKey = Keys.None;
            }
        }

        private const int WM_KEYDOWN = 0x0100;
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            //AEC.Log.OutputLine("ProModeForm ProcessCmdKey:{0}", keyData);

            if (msg.Msg == WM_KEYDOWN)
            {
                if(keyData == Keys.Tab)
                {
                    //AEC.Log.OutputLine("ProcessCmdKey Msg:{0:X} ketData:{1:X}", msg.Msg, keyData);
                    QuickOperation(keyData);

                    return false;
                }
                else if(keyData == Keys.Escape)
                {
                    ShowMask();

                    return false;
                }
            }

            //Log.OutputLine("ProcessCmdKey key data:{0}", keyData);
            
            return base.ProcessCmdKey(ref msg, keyData);
        }

        //protected override bool ProcessKeyPreview(ref Message m)
        //{
        //    AEC.Log.OutputLine("ProcessKeyPreview Msg:{0:X}", m.Msg);
        //    return base.ProcessKeyPreview(ref m);
        //}

        protected override bool ProcessDialogKey(Keys keyData)
        {
            //AEC.Log.OutputLine("ProModeForm ProcessDialogKey:{0}", keyData);
            if (GetEQGraphicPanelIsVisible(lastPageItem) && NumberKeysHelper.IsNumber(keyData))
            {
                //AEC.Log.OutputLine("lastPageItem:{0}", lastPageItem);
                List<short> indexList = this.keysHelper.GetIndex(keyData);
                if (indexList != null)
                {
                    PanelBase panel = currItem2PanelMap[lastPageItem];
                    if (panel != null)
                    {
                        panel.ActiveEQ(indexList);
                    }
                }

                return false;
            }
            else if (this.key2EventMap.ContainsKey(keyData))
            {
                if (keyData == Keys.F11 || keyData == Keys.F12)
                {
                    //Log.OutputLine("ProMode 快捷键:{0}", keyData);
                    this.key2EventMap[keyData](keyData);
                }
                else
                {
                    if (this.lastKey != keyData || (DateTime.Now - lastKeydownTimestamp).Milliseconds >= 250)
                    {
                        this.lastKey = keyData;
                        lastKeydownTimestamp = DateTime.Now;
                        this.key2EventMap[keyData](keyData);
                    }
                }

                return false;
            }

            //AEC.Log.OutputLine("ProcessDialogKey");
            return base.ProcessDialogKey(keyData);
        }

        private void PageUpOperation(Keys keyData)
        {
            if (!GetEQGraphicPanelIsVisible(lastPageItem))
            {
                return;
            }

            PanelBase panel = currItem2PanelMap[lastPageItem];
            if (panel != null)
            {
                panel.ActivePrevEQ();
            }
        }

        private void PageDownOperation(Keys keyData)
        {
            if (!GetEQGraphicPanelIsVisible(lastPageItem))
            {
                return;
            }

            PanelBase panel = currItem2PanelMap[lastPageItem];
            if (panel != null)
            {
                panel.ActiveNextEQ();
            }
        }

        private void AdjuestVolume(Keys keyData)
        {
            //AEC.Log.OutputLine("keydata:{0}", keyData);
            switch(keyData)
            {
                case Keys.Q:
                    if (this.currEffectorModel.system.musicVolume.v < this.currEffectorModel.system.musicMaxVolume.v)
                    {
                        this.currEffectorModel.system.musicVolume.v += 1;
                        this.eventBus.NotifyObeserver(new MusicVolumeChangedArgs());
                    }
                    break;

                case Keys.W:
                    if (this.currEffectorModel.system.micVolume.v < this.currEffectorModel.system.micMaxVolume.v)
                    {
                        this.currEffectorModel.system.micVolume.v += 1;
                        this.eventBus.NotifyObeserver(new MicVolumeChangedArgs());
                    }
                    break;

                case Keys.E:
                    if (this.currEffectorModel.system.effectVolume.v < this.currEffectorModel.system.effectMaxVolume.v)
                    {
                        this.currEffectorModel.system.effectVolume.v += 1;
                        this.eventBus.NotifyObeserver(new EffectVolumeChangedArgs());
                    }
                    break;

                case Keys.A:
                    if (this.currEffectorModel.system.musicVolume.v > 0)
                    {
                        this.currEffectorModel.system.musicVolume.v -= 1;
                        //AEC.Log.OutputLine("music:{0}", this.v5.system.musicVolume.v);
                        this.eventBus.NotifyObeserver(new MusicVolumeChangedArgs());
                    }
                    break;

                case Keys.S:
                    if (this.currEffectorModel.system.micVolume.v > 0)
                    {
                        this.currEffectorModel.system.micVolume.v -= 1;
                        this.eventBus.NotifyObeserver(new MicVolumeChangedArgs());
                    }
                    break;

                case Keys.D:
                    if (this.currEffectorModel.system.effectVolume.v > 0)
                    {
                        this.currEffectorModel.system.effectVolume.v -= 1;
                        this.eventBus.NotifyObeserver(new EffectVolumeChangedArgs());
                    }
                    break;
            }
        }

        private void SwitchPage(Keys keyData)
        {
            if(this.currEffectorFamily != MEffectorFamily.VX || !this.navigationBar.Enabled)
            {
                return;
            }

            Dictionary<Keys, PageItem> key2PageMap = new Dictionary<Keys, PageItem>()
            {
                {Keys.Z, PageItem.Music},
                {Keys.X, PageItem.Mic},
                {Keys.C, PageItem.Effect},
                {Keys.V, PageItem.MainOutput},
                {Keys.B, PageItem.Aux1Output},
                {Keys.N, PageItem.Aux2Output},
                {Keys.M, PageItem.SurOutput},
            };

            if(key2PageMap.ContainsKey(keyData))
            {
                this.navigationBar.pageItem = key2PageMap[keyData];
            }
        }

        private void QuickOperation(Keys keyData)
        {
            //AEC.Log.OutputLine("QuickOperation()...");
            if(keyData == Keys.Tab)
            {
                //if(true || this.navigationBar.pageItem != PageItem.System)
                {
                    FormModeOperation form = new FormModeOperation();
                    form.Binding(this.mController, this.currEffectorModel.system, this.eventBus);
                    form.ShowDialog();
                }
            }
        }

        private void ShowMask()
        {
            if (!AppConfig.isOEMVersion && Win32.GetForegroundWindow() == this.Handle)
            {
                Form mask = new FormMask();
                mask.Owner = this;
                mask.Location = this.Location;
                mask.Size = this.Size;
                mask.ShowDialog();
            }
        }

        //private void ShowMask()
        //{
        //    if (protectedMask != null && protectedMask.Visible)
        //    {
        //        //protectedMask.Visible = false;
        //        if (this.Controls.Contains(protectedMask))
        //        {
        //            this.Controls.Remove(protectedMask);
        //            //protectedMask.BringToFront();
        //        }
        //        protectedMask.Visible = false;
        //        return;
        //    }

        //    //Log.OutputLine("ShowMask");
        //    if (protectedMask == null)
        //    {
        //        protectedMask = new ProtectedMask();
        //        protectedMask.MouseDoubleClick += protectedMask_MouseDoubleClick;
        //    }
        //    protectedMask.Size = new Size(this.Width - 4, this.Height - this.title.Height - 3);
        //    protectedMask.Location = new Point(2, this.title.Height + 1);
        //    //protectedMask.Size = new Size(this.Width - 4, this.Height - this.title.Height - 3 - 300);
        //    //protectedMask.Location = new Point(2, this.title.Height + 301);
        //    protectedMask.Visible = true;
        //    if(!this.Controls.Contains(protectedMask))
        //    {
        //        this.Controls.Add(protectedMask);
        //        protectedMask.BringToFront();
        //    }
        //}

        //private void protectedMask_MouseDoubleClick(object sender, MouseEventArgs e)
        //{
        //    if (protectedMask != null && protectedMask.Visible)
        //    {
        //        //protectedMask.Visible = false;
        //        if (this.Controls.Contains(protectedMask))
        //        {
        //            this.Controls.Remove(protectedMask);
        //            //protectedMask.BringToFront();
        //        }
        //        return;
        //    }
        //    protectedMask.Visible = false;
        //    //protectedMask.Visible = false;
        //    //throw new NotImplementedException();
        //}

        private void ShowHelpHandler(Keys keyData)
        {
            ShowHelp();
        }

        private void ShowHelp()
        {
            //FormHelp form = new FormHelp();
            //form.ShowDialog(this);
            string helpFile = System.AppDomain.CurrentDomain.BaseDirectory + localization.helpFileName;
            if(System.IO.File.Exists(helpFile))
            {
                try
                {
                    Help.ShowHelp(this, helpFile);
                }
                catch(Exception ex)
                {
                    AEC.Tools.LogHelper.WriteInfo("FromProMode.ShowHelp().... Exception: ", ex);
                }
            }
            //Help.ShowHelp(this, "")
        }

        private void ShowOnlineHelp()
        {
            Form form = new UI.Forms.FormOnlineHelp();
            form.ShowDialog();
        }

        private void ShowBluetoothRename()
        {
            BluetoothName.FormBluetoothRename form = new BluetoothName.FormBluetoothRename();
            form.ShowDialog();
        }

        private void ShowFirmwareUpdate()
        {
            Form form = new UI.Forms.FormFirmwareUpdate();
            form.ShowDialog();
        }

        private void ShowActivateDevice()
        {
            ActivationCode.MLicenseType currLicenseType = ActivationCode.CheckLicense();
            if(currLicenseType == ActivationCode.MLicenseType.Unauthorized)
            {
                UI.Forms.FormActivation form = new UI.Forms.FormActivation();
                string activationCode;
                ActivationCode.MLicenseType licenseType = form.ShowDialog(out activationCode);
                if (licenseType == ActivationCode.MLicenseType.Unauthorized)
                {
                    return;
                }
            }


            Form formActivateDevice = new UI.Forms.FormActivateDevice();
            formActivateDevice.ShowDialog();
        }

        private void OpenLogFileLocation()
        {
            string logDir = System.AppDomain.CurrentDomain.BaseDirectory + @"Logs\";
            string filePath = logDir + "AEC.log";
            if (System.IO.File.Exists(filePath))
            {
                Tools.FileHelper.OpenFileLocation(filePath);
            }            
            else if (System.IO.Directory.Exists(logDir))
            {
                Tools.FileHelper.OpenDirectory(logDir);
            }
            else
            {
                Tools.LogHelper.WriteDebug("FormProMode.OpenLogFileLocation()...Log file Or directory does not Exist!");
            }
        }

        private void ShowFormCommSettings(Keys keyData)
        {
            this.ShowCommSettings();
            //Form form = new FormExportPresetModeData(this.mController);
            //form.ShowDialog(this);
        }

        private void ShowCommSettings()
        {
            FormCommSettings setting = new FormCommSettings();
            setting.ShowDialog();
        }

        private void ShowPreferences()
        {
            FormPreferences form = new FormPreferences();
            form.ShowDialog();
        }

        //private void SuperModeDecryptData(Keys keyData)
        //{
        //    if (this.isLockeded && systemPanel.Enabled && this.v5Controller.currEffector.dataIsPrivate)
        //    {
        //        if(DialogResult.Yes == MessageBox.Show(this, "是否强制解除加密?", "AEC", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
        //        {
        //            this.v5Controller.currEffector.dataIsPrivate = false;
        //            //this.v5Controller.currEffector.InitFreqRespAndEQGraphic();
        //            //this.UpdateUIFromData();
        //            //this.navigationBar.Enabled = true;
        //            //this.UnlockSystem();
        //            this.UploadDataToDevice(this.v5Controller.currEffector);
        //            //AEC.Log.OutputLine("强制解除加密!!!");
        //        }
        //    }
        //}

        private void EncryptModeData()
        {
            if (!this.mController.currEffector.effectorModel.dataIsPrivate)
            {
                return;
            }

            this.mController.currEffector.effectorModel.dataIsPrivate = true;
            this.UploadDataToDevice(this.mController.currEffector.effectorModel);
        }

        //private void buttonTest_Click(object sender, EventArgs e)
        //{
        //    if(sender.Equals(btnReadLevel))
        //    {
        //        StartReadLevel();
        //    }
        //    else if(sender.Equals(this.btnMusicMute))
        //    {
        //        MusicMute();
        //    }
        //    else if (sender.Equals(this.btnMicMute))
        //    {
        //        MicMute();
        //    }
        //    else if (sender.Equals(this.btnEffectMute))
        //    {
        //        EffectMute();
        //    }
        //    else if(sender.Equals(this.btnEncrypt))
        //    {
        //        Encrypt();
        //    }
        //    else if(sender.Equals(this.btnUploadData))
        //    {
        //        UploadData();
        //    }
        //}
        
        //private void StartReadLevel()
        //{
        //    if(timer == null)
        //    {
        //        timer = new System.Windows.Forms.Timer();
        //        timer.Interval = 1000;
        //        timer.Tick += timer_Tick;
        //    }

        //    if(timer.Enabled)
        //    {
        //        timer.Stop();
        //    }
        //    else
        //    {
        //        timer.Start();
        //    }
        //}

        //private void timer_Tick(object sender, EventArgs e)
        //{
        //    this.v5Controller.commSvc.ReadSystemLevel();
        //}

        //private void MusicMute()
        //{
        //    v5Controller.currEffector.system.musicMute.value = !v5Controller.currEffector.system.musicMute.value;
        //}

        //private void MicMute()
        //{
        //    v5Controller.currEffector.system.micMute.value = !v5Controller.currEffector.system.micMute.value;
        //}

        //private void EffectMute()
        //{
        //    v5Controller.currEffector.system.effectMute.value = !v5Controller.currEffector.system.effectMute.value;
        //}

        //private void Encrypt()
        //{
        //    v5Controller.currEffector.dataIsPrivate = !v5Controller.currEffector.dataIsPrivate;
        //    AEC.Log.OutputLine("当前数据加密状态:{0}，之前状态:{1}", v5Controller.currEffector.dataIsPrivate, !v5Controller.currEffector.dataIsPrivate);
        //}

        //private void UploadData()
        //{
        //    this.v5Controller.commSvc.AsyncUploadData2Device();
        //}

        private void RegisterEventBus()
        {
            //注册控制器事件处理器
            this.mController.controllerEvent.RegisterHandler(ControllerEventHandler);
            this.eventBus.RegisterHandler(MainEventBusHandler);
            this.localization.langEventBus.RegisterHandler(LangEventBusHandler);
            this.mController.rcLocalEvent.RegisterHandler(RCStatusChangedHandler);
        }

        private void RemoveEventBus()
        {
            this.mController.rcLocalEvent.RegisterHandler(RCStatusChangedHandler);
            this.localization.langEventBus.RemoveHandler(LangEventBusHandler);
            this.eventBus.RemoveHandler(MainEventBusHandler);
            this.mController.controllerEvent.RemoveHandler(ControllerEventHandler);
        }

        public void MainEventBusHandler(object sender, EventBusArgs e)
        {
            switch (e.type)
            {
                case EventType.UploadDataToDevice:
                    {
                        EffectorModelBase tempEffectorModel = (e as UploadDataToDeviceArgs).effectorModel;
                        this.UploadDataToDevice(tempEffectorModel);
                    }
                    break;

                case EventType.CallDeviceMode:
                    CallDeviceMode((e as CallDeviceModeArgs).modeIndex);
                    break;

                case EventType.SwitchDeviceWorkMode:
                    SwitchDeviceWorkMode((e as SwitchDeviceWorkModeArgs).switchWorkMode);
                    break;

                case EventType.UnlockKeyLock:
                    UnlockSystem(false);
                    break;

                case EventType.UnlockUsageLimit:
                    UnlockSystem(false);
                    break;

                case EventType.UsageLimitLockFormShow:
                    UsageLimitLockFormShowHandler((e as UsageLimitLockFormShowArgs).isShow);
                    break;

                case EventType.EncryptModeData:
                    EncryptModeData();
                    break;

                case EventType.UpdateUIFromData:
                    this.UpdateUIFromData();
                    break;

                case EventType.Disconnect:
                    this.DisconnectDevice();
                    break;

                case EventType.EQGraphicPanelJump:
                    JumpToGraphicPanle((e as EQGraphicPanelJumpArgs).module);
                    break;

                case EventType.MicABLinked:
                    MicABLinkedHandler((e as MicABLinkedArgs).isLink, (e as MicABLinkedArgs).channel);
                    break;
            }
        }

        private void UsageLimitLockFormShowHandler(bool isShow)
        {
            this.tsmiLanguage.Enabled = !isShow;
        }

        private void LangEventBusHandler(object sender, EventBusArgs e)
        {
            UpdateDisplayLanguage();
        }

        private void RCStatusChangedHandler(object sender, EventBusArgs e)
        {
            AEC.Tools.LogHelper.WriteInfo("FormProMode.RCStatusChangedHandler()...event type:{0}, {1}", e.type, mController.isConnected);
            if(e.type == EventType.RCOperationStatusChanged ||
                e.type == EventType.RCConnectionStatusChanged ||
                e.type == EventType.RCDisconnectedFromServer ||
                e.type == EventType.RCDisconnectFromRc ||
                e.type == EventType.RCDisconnectedFromRc)
            {
                syncContext.Post(RCStatusChangedHandlerCallback, null);
            }
        }

        private void RCStatusChangedHandlerCallback(object state)
        {
            this.UpdateText();
            if(!this.mController.isConnected)
            {
                this.btnConnect.isConnected = false;
            }
        }

        private void JumpToGraphicPanle(ModuleBase module)
        {
            if(this.mController.currEffectorFamily == MEffectorFamily.VX)
            {
                if (vxModule2ItemMap != null &&
                    module != null &&
                    vxModule2ItemMap.ContainsKey(module))
                {
                    this.navigationBar.pageItem = vxModule2ItemMap[module];
                    this.eqGraphicPanel.Focus();
                }
                
            }
            else if (this.mController.currEffectorFamily == MEffectorFamily.KX)
            {
                if(kxModule2ItemMap != null &&
                    module != null &&
                    kxModule2ItemMap.ContainsKey(module))
                {
                    this.navigationBar.pageItem = kxModule2ItemMap[module];
                    this.eqGraphicPanel.Focus();
                }
            }
            //else if (this.mController.currEffectorFamily == MEffectorFamily.VKxPIIDCM)
            //{
            //    if (vkxPIIdcmModule2ItemMap != null &&
            //        module != null &&
            //        vkxPIIdcmModule2ItemMap.ContainsKey(module))
            //    {
            //        this.navigationBar.pageItem = vkxPIIdcmModule2ItemMap[module];
            //        this.eqGraphicPanel.Focus();
            //    }
            //}
        }

        private void MicABLinkedHandler(bool isLinked, MicChannel channel)
        {
            //Log.OutputLine("{0}_{1}", isLinked, channel);
            if(mController.currEffectorFamily == MEffectorFamily.VKxDCM)
            {
                if(this.vkxdcmNavigationBar != null)
                {
                    if (isLinked)
                    {
                        this.vkxdcmNavigationBar.LInkMicAB(channel);
                    }
                    else
                    {
                        this.vkxdcmNavigationBar.UnlinkMicAB();

                        PageItem anotherMicPage = channel != MicChannel.MicA ? PageItem.MicA : PageItem.MicB;
                        
                        ModuleBase anotherMicModule = vkxdcmItem2ModuleMap[anotherMicPage];
                        PanelBase anotherMicPanel = vkxdcmItem2PanelMap[anotherMicPage];

                        anotherMicModule.InitFreqResp();
                        anotherMicModule.InitEQGraphic();

                        anotherMicPanel.UpdateUIFromData();
                    }
                }                
            }
        }

        public void CallDeviceMode(short modeIndex)
        {
            if (!this.mController.isConnected)
            {
                return;
            }

            ShowCommForm(CommProcess.ProModeCallDeviceMode);
            this.mController.AsyncProModeCallDeviceMode(modeIndex);
        }

        public void SwitchDeviceWorkMode(SwitchWorkMode switchWorkMode)
        {
            if (this.mController.isConnected)
            {
                ShowCommForm(CommProcess.ProModeSwitchDeviceWorkMode);
                this.mController.AsyncProModeSwitchDeviceWorkMode(switchWorkMode);
            }
            else
            {
                if(this.mController.currEffectorFamily != MEffectorFamily.VKxS && 
                    this.mController.currEffectorFamily != MEffectorFamily.VKxPIIDCM &&
                    this.mController.currEffectorFamily != MEffectorFamily.X5)
                {
                    return;
                }

                bool isModelWithW = this.mController.mEffectorModel.currModel == DeviceModel.VK3W ||
                                    this.mController.mEffectorModel.currModel == DeviceModel.VK3W_WithoutBT || 
                                    this.mController.mEffectorModel.currModel == DeviceModel.X5W ;

                DeviceModel targetModel = switchWorkMode.workModeIndex == 1 ? DeviceModel.X5 : DeviceModel.VK6SPro;//this.mController.mEffectorModel.currModel;
                if (isModelWithW)
                {
                    targetModel = switchWorkMode.workModeIndex == 1 ? DeviceModel.X5W : DeviceModel.VK3W;
                }
                else
                {
                    targetModel = switchWorkMode.workModeIndex == 1 ? DeviceModel.X5 : DeviceModel.VK6SPro;
                }

                if (this.mController.SwitchCurrDeviceModel(targetModel))
                {
                    this.UpdateUI(false);
                }
            }
        }

        public void UploadDataToDevice(EffectorModelBase tempEffectorModel)
        {
            if (!this.mController.isConnected)
            {
                return;
            }

            this.mController.UpdateToBeUploadedData(tempEffectorModel);

            ShowCommForm(CommProcess.ProModeUploadData);
            this.mController.AsyncProModeUploadData2Device();
        }

        public void ShowCommForm(CommProcess process)
        {
            if (this.formComm == null || this.formComm.IsDisposed)
            {
                this.formComm = new FormComm();
                this.formComm.Binding(this.mController, eventBus);
                //this.formComm.VisibleChanged += formComm_VisibleChanged;
                //this.formComm.Owner = this;
            }
            this.formComm.ShowCommInfo(this, process, OperationMode.Professional);
            //this.formComm.Show(this)
        }

        public void ShowCommFormDeviceNotActivated()
        {
            if (this.formComm == null || this.formComm.IsDisposed)
            {
                this.formComm = new FormComm();
                this.formComm.Binding(this.mController, eventBus);
                //this.formComm.VisibleChanged += formComm_VisibleChanged;
                //this.formComm.Owner = this;
            }
            this.formComm.ShowDeviceNotActivated();
        }

        public void ShowCommFormInputKeyPassword()
        {
            if (this.formComm == null || this.formComm.IsDisposed)
            {
                this.formComm = new FormComm();
                this.formComm.Binding(this.mController, eventBus);
                //this.formComm.VisibleChanged += formComm_VisibleChanged;
                //this.formComm.Owner = this;
            }
            this.formComm.ShowDeviceKeyboradIsLocked(OperationMode.Professional);
        }

        //private void formComm_VisibleChanged(object sender, EventArgs e)
        //{
        //    AEC.Log.OutputLine("formComm_VisibleChanged:{0}", this.formComm.Visible);
        //    this.maskLayer.Visible = this.formComm.Visible;
        //    //throw new NotImplementedException();
        //}

        public void CloseCommForm()
        {
            if (this.formComm != null)
            {
                this.formComm.Close();
            }
        }

        private void HideCommForm()
        {
            if (this.formComm != null && !this.formComm.IsDisposed)
            {
                this.formComm.HideForm();
            }
        }

        private void navigationBar_CheckedChanged(object sender, EventArgs e)
        {
            //AEC.Log.OutputLine("{0}", this.navigationBar.pageItem);
            InitPage(this.navigationBar.pageItem);
        }

        private bool GetEQGraphicPanelIsVisible(PageItem item)
        {
            return this.currItem2ModuleMap.ContainsKey(item);
        }
        
        private void InitPage(PageItem item)
        {
            if (!currItem2PanelMap.ContainsKey(item))
            {
                return;
            }
            
            this.rtaSwitch.Visible = GetEQGraphicPanelIsVisible(item);
            
            //隐藏上次的页面
            if(lastPageItem != item)
            {
                if (currItem2PanelMap.ContainsKey(lastPageItem))
                {
                    //item2PanelMap[lastPageItem].Visible = false;
                    currItem2PanelMap[lastPageItem].HidePanel();
                    //if (this.Controls.Contains(item2PanelMap[lastPageItem]))
                    //{
                    //    this.Controls.Remove(item2PanelMap[lastPageItem]);
                    //}
                }

                lastPageItem = item;
            }

            if(currItem2ModuleMap.ContainsKey(item))
            {
                if (!this.Controls.Contains(currItem2PanelMap[item]))
                {
                    this.Controls.Add(currItem2PanelMap[item]);
                }
                
                currItem2PanelMap[item].ShowPanel();
                currItem2PanelMap[item].Invalidate();
                currItem2PanelMap[item].Update();

                this.eqGraphicPanel.BindingModule(currItem2ModuleMap[item], mController.currEffectorModel.GetOtherModulesInTheGroup(currItem2ModuleMap[item]));

                if (!this.eqGraphicPanel.Visible)
                {
                    this.eqGraphicPanel.Visible = true;
                }
            }
            else
            {
                this.eqGraphicPanel.Visible = false;
                this.currItem2PanelMap[item].Visible = true;

                if (!this.Controls.Contains(currItem2PanelMap[item]))
                {
                    this.Controls.Add(currItem2PanelMap[item]);
                }
            }
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            //this.navigationBar.Enabled = !this.navigationBar.Enabled;
            if (!this.btnConnect.Enabled)
            {
                return;
            }

            this.SwitchConnectionState();
        }

        //private int count = 0;
        private void SwitchConnectionState()
        {
            if (this.mController.isConnected)
            {
                //this.eventBus.NotifyObeserver(new StopReadLevelArgs());

                //if (!this.v5Controller.commSvc.CheckCanBeDisconnected())
                //{
                //    MessageBox.Show("系统忙碌中,请稍后重试!");
                //    return;
                //}

                //this.v5Controller.commSvc.Disconnect();
                //this.btnConnect.Text = "联机";
                //btnConnect.isConnected = false;
                //this.UnlockSystem();
                //AEC.Log.OutputLine("断开连接");
                this.DisconnectDevice();
            }
            else
            {
                //AEC.Log.OutputLine("联机 count:{0}", ++count);
                ConnectDevice();
            }
        }

        private void AsyncUpdate(object state)
        {
            this.Update();
        }

        private void AsyncConnectDevice(object state)
        {
            ConnectDevice();
        }

        private void ConnectDevice()
        {
            if (!this.mController.isConnected)
            {
                if (!this.mController.CanConnectToDevice())
                {
                    gtMessageBox.ShowMessage(this, "当前处于远程控制模式，但是未连接到远程主机，请先连接到远程主机!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    return;
                }

                ShowCommForm(CommProcess.ProModeConnectLoadData);
                this.mController.AsyncProModeConnectAndLoadData();
                //btnConnect.UpdateEnabled(false);
                //if (this.v5Controller.commSvc.AsyncConnectAndLoadAllData())
                //{
                //    //this.button1.Enabled = false;
                //    //btnConnect.Enabled = false;
                //    btnConnect.UpdateEnabled(false);
                //}
            }
        }

        private void DisconnectDevice()
        {
            if (this.mController.isConnected)
            {
                this.mController.ForcedDisconnect();
                this.btnConnect.Text = localization[Localization.buttonConnection_Connect];
                btnConnect.isConnected = false;
                this.UnlockSystem(true);
                this.UpdateText();
                Tools.LogHelper.WriteInfo("Disconnect from the device.");
            }
        }

        //private void ResetAllEQGraphicCachedBack()
        //{
        //    if(this.v5Controller == null || this.v5 == null)
        //    {
        //        return;
        //    }

        //    foreach(ModuleBase module in this.v5.nonSystemModules)
        //    {
        //        if(module.eqGraphic != null)
        //        {
        //            module.eqGraphic.ResetCachedBackground();
        //        }
        //    }
        //}

        //private void FormProMode_SizeChanged(object sender, EventArgs e)
        //{
        //    ResetAllEQGraphicCachedBack();
        //    //if (protectedMask != null && protectedMask.Visible)
        //    //{
        //    //    protectedMask.Size = new Size(this.Width - 4, this.Height - this.title.Height - 3);
        //    //    protectedMask.Location = new Point(2, this.title.Height + 1);
        //    //}
        //}

        //private void FormProMode_Paint(object sender, PaintEventArgs e)
        //{
            
        //}

        private void panelToolBar_Paint(object sender, PaintEventArgs e)
        {
            //GDIHelper.FillRectangle(e.Graphics, this.ClientRectangle, Color.LightGray);
            //GDIHelper.QuickDrawImage(e.Graphics, Point.Empty, AEC.Properties.Resources.tool_bar_background);
        }

        //private int count = 0;
        protected override void OnPaint(PaintEventArgs e)
        {
            //base.OnPaint(e);
            GDIHelper.FillRectangle(e.Graphics, this.ClientRectangle, SkinManager.CurrentSkin.proModeBackgroundColor);
            //Log.OutputLine("FromProMode OnPaint:{0}", ++count);
        }

        private void eqGraphicPanel_SizeChanged(object sender, EventArgs e)
        {
            if (audioAnalysis.displayIsOn)
            {
                audioAnalysis.UpdateDisplaySize();
            }
        }

        private void eqGraphicPanel_MouseMove(object sender, MouseEventArgs e)
        {
            if(audioAnalysis.rtaIsON)
            {
                audioAnalysis.MouseMove(e.Location);
            }
        }

        private void eqGraphicPanel_MouseLeave(object sender, EventArgs e)
        {
            if (audioAnalysis.rtaIsON)
            {
                audioAnalysis.MouseLeave();
            }
        }

        private void chkRTA_MouseClick(object sender, MouseEventArgs e)
        {
            if(e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                //如果RTA未打开，则
                if (!this.audioAnalysis.rtaIsON)
                {
                    this.rtaSwitch.Enabled = false;
                    //Console.WriteLine("FormMain NotifyObeserver RTATryOperateArgs RTAOperate.StartRTA");
                    this.rtaEventBus.NotifyObeserver(new RTATryOperateArgs(RTAOperate.TurnOnRTA));
                }
                else
                {
                    this.rtaSwitch.Enabled = false;
                    this.rtaEventBus.NotifyObeserver(new RTATryOperateArgs(RTAOperate.TurnOffRTA));
                }
            }
            else if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                ShowRTAOption();
            }
        }

        private void ShowRTAOptionHandler(Keys keyData)
        {
            ShowRTAOption();
        }

        private bool isLockParams = false;
        private void SwitchLockStatusHandler(Keys keyData)
        {
            isLockParams = !isLockParams;
            LockParams(isLockParams);
        }

        private void ShowRTAOption()
        {
            if (this.formRTAOption == null || this.formRTAOption.IsDisposed)
            {
                this.formRTAOption = new FormRTAOptions();
            }

            if (!this.formRTAOption.Visible)
            {
                this.formRTAOption.Show();
            }
            else
            {
                this.formRTAOption.Activate();
            }
        }

        private void tsmi_Click(object sender, EventArgs e)
        {
            if(sender.Equals(this.tsmiCommSettings))
            {
                this.ShowCommSettings();
            }
            else if(sender.Equals(this.tsmiPreferences))
            {
                this.ShowPreferences();
            }
            else if(sender.Equals(this.tsmiRTAOptions))
            {
                this.ShowRTAOption();
            }
            else if (sender.Equals(this.tsmizh_CN))
            {
               //Config
                Config.GetInstance().localization.lang = LanguageCode.zh_CN;                
                UpdateTsmi();
            }
            else if (sender.Equals(this.tsmien_US))
            {
                localization.lang = LanguageCode.en_US;
                UpdateTsmi();
            }
            else if (sender.Equals(this.tsmiOpenLogFileLocation))
            {
                this.OpenLogFileLocation();
            }
            else if(sender.Equals(this.tsmiOnlineHelp))
            {
                this.ShowOnlineHelp();
            }
            else if (sender.Equals(this.tsmiBluetoothRename))
            {
                this.ShowBluetoothRename();
            }
            else if (sender.Equals(this.tsmiFirmwareUpdate))
            {
                this.ShowFirmwareUpdate();
            }
            else if(sender.Equals(this.tsmiActivateDevice))
            {
                this.ShowActivateDevice();
            }
            else if(sender.Equals(this.tsmiAbout))
            {
                FormAbout about = new FormAbout();
                about.ShowDialog();
            }
        }

        private void LockParams(bool isLock)
        {
            //if (this.mController.mEffectorModel.currModel != DeviceModel.K220_OEM_LINGJIE_AEC || this.kxNavigationBar == null || this.kxItem2PanelMap == null || !this.kxItem2PanelMap.ContainsKey(PageItem.Monitor))
            if (this.mController.mEffectorModel.currModel != DeviceModel.VK3D || this.vkxscmNavigationBar == null || this.vkxscmItem2PanelMap == null)
            {
                return;
            }

            AEC.UI.VKxSCM.VKxSCMMusicPanel musicPanel = this.vkxscmItem2PanelMap[PageItem.Music] as AEC.UI.VKxSCM.VKxSCMMusicPanel;
            AEC.UI.VKxSCM.VKxSCMMicPanel micPanel = this.vkxscmItem2PanelMap[PageItem.Mic] as AEC.UI.VKxSCM.VKxSCMMicPanel;
            if (musicPanel == null || micPanel == null)
            {
                return;
            }

            if (isLock)
            {
                this.vkxscmEffectorModel.music.isShowExciter = false;
                this.vkxscmNavigationBar.isLockMicVT = true;
                musicPanel.isLockParams = true;
                (this.systemPanel as SystemPanel).isLockParams = true;
            }
            else
            {
                FormUnlockOutput form = new FormUnlockOutput();
                if (form.ShowDialog())
                {
                    this.vkxscmEffectorModel.music.isShowExciter = true;
                    this.vkxscmNavigationBar.isLockMicVT = false;
                    musicPanel.isLockParams = false;
                    (this.systemPanel as SystemPanel).isLockParams = false;
                }
            }

            this.eqGraphicPanel.UpdateGraphic();

            this.UpdateText();
        }

        private void UpdateTsmi()
        {
            this.tsmizh_CN.Checked = localization.lang == LanguageCode.zh_CN;
            this.tsmien_US.Checked = localization.lang == LanguageCode.en_US;
        }

        private void UpdateText()
        {
            //this.Text = this.mController.mEffectorModel.modelLabel + " AEC " + localization[Localization.formProMode] + this.mController.RmoteControlMessage + VersionHelper.GetVersion();
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            if(AppConfig.isShowManufacturer)
            {
                sb.AppendFormat("{0} ", AppConfig.GetManufacturer());
            }

            sb.AppendFormat("{0} ", ProductsConfig.ProductName);
            //if(!isLockParams)
            //{
            //    sb.AppendFormat("- Unlocked");
            //}
            
            if(this.mController.isConnected)
            {
                sb.AppendFormat("   |   {0}", this.mController.connectionInfo);
            }

            this.Text = sb.ToString();
        }

        private void UpdateDisplayLanguage()
        {
            this.UpdateText();
            this.navigationBar.UpdateDisplayLanguage();
            this.systemControlPanel.UpdateDisplayLanguage();
            this.btnConnect.UpdateDisplayLanguage();
            this.tsmiDeviceModels.Text = localization[Localization.formProMode_DeviceModels];
            this.tsmiRTAOptions.Text = localization[Localization.formProMode_RTAOptions];
            this.tsmiCommSettings.Text = localization[Localization.formProMode_CommSettings];
            this.tsmiPreferences.Text = localization[Localization.formProMode_Preferences];
            this.tsmiLanguage.Text = localization[Localization.formProMode_Language];
            this.tsmiHelp.Text = localization[Localization.formProMode_Help];
            this.tsmiOnlineHelp.Text = localization[Localization.formProMode_OnlineHelp];
            this.tsmiBluetoothRename.Text = localization[Localization.formProMode_BluetoothRename];
            this.tsmiFirmwareUpdate.Text = localization[Localization.formProMode_FirmwareUpdate];
            this.tsmiActivateDevice.Text = localization[Localization.formProMode_ActivateDevice];
            this.tsmiOpenLogFileLocation.Text = localization[Localization.formProMode_OpenLogFileLocation];
            this.tsmiAbout.Text = localization[Localization.formProMode_About];

            foreach(PanelBase panel in currItem2PanelMap.Values)
            {
                if(panel != null)
                {
                    panel.UpdateDisplayLanguage();
                }
            }
        }

        private const int vhiNavigationBarWidth = 50;
        private const int vhiVolumeControlPanelWidth = 50;
        private const int vxNavigationBarWidth = 41;
        private const int vxVolumeControlPanelWidth = 59;

        public void UpdateEffectorModel()
        {
            this.UpdateText();

            if (this.mController.currEffectorFamily == MEffectorFamily.VKxS && this.vkxsItem2PanelMap != null)
            {
                foreach (PageItem item in this.vkxsItem2PanelMap.Keys)
                {
                    this.vkxsItem2PanelMap[item].UpdateEffectorModel();
                }
            }
            else if (this.mController.currEffectorFamily == MEffectorFamily.VX && this.vxItem2PanelMap != null)
            {
                foreach (PageItem item in this.vxItem2PanelMap.Keys)
                {
                    this.vxItem2PanelMap[item].UpdateEffectorModel();
                }
            }

            //this.systemControlPanel.showTimbre = this.mController.currEffectorFamily == MEffectorFamily.VKxS;
            //else if (this.mxItem2PanelMap != null)
            //{
            //    foreach (PageItem item in this.mxItem2PanelMap.Keys)
            //    {
            //        this.mxItem2PanelMap[item].UpdateEffectorModel();
            //    }
            //}
        }

        private const short CriticalMusicVolumeValue = 76;
        private const short CriticalMicVolumeValue = 77;
        private const short CriticalEffectVolumeValue = 7;
        private static readonly Color SuperUserColor = Color.FromArgb(0x76, 0xff, 0x03);

        private void FormProMode_KeyDown(object sender, KeyEventArgs e)
        {
            if(lastPageItem == PageItem.System && e.Shift && e.Control && e.Alt && e.KeyCode == Keys.F7)
            {
                SystemModuleBase system = currEffectorModel.system;
                if(system.musicVolume.v == CriticalMusicVolumeValue &&
                   system.micVolume.v == CriticalMicVolumeValue &&
                   system.effectVolume.v == CriticalEffectVolumeValue &&
                   system.musicStartVolume.v == (system.musicVolume.v - 1) &&
                   system.micStartVolume.v == (system.micVolume.v - 1) &&
                   system.effectStartVolume.v == (system.effectVolume.v - 1) &&
                   system.musicMaxVolume.v == (system.musicVolume.v + 1) &&
                   system.micMaxVolume.v == (system.micVolume.v + 1) &&
                   system.effectMaxVolume.v == (system.effectVolume.v + 1))
                {
                    if ((this.systemPanel as SystemPanel).suPasswordIsMatch)
                    {
                        (this.systemPanel as SystemPanel).ClearSuPassword();

                        FormSuperUserPermission form = new FormSuperUserPermission(this.mController);
                        form.ShowDialog();
                        this.title.ForeColor = this.mController.isSuperUserMode ? SuperUserColor : Color.White;

                        return;
                    }
                }

                //AEC.Log.OutputLine("MessageBeep()...");
                Win32API.MessageBeep(0x00000010);
            }
        }

        private void InitSwitchDevcieModelMenuItems()
        {
            if (AppConfig.IsShowSwitchDeviceModelMenuItem())
            {
                this.tsmiDeviceModels.DropDownItems.AddRange(CreateSupportedDeviceModelMenuItems());
            }
        }

        private void UpdateDevcieModelMenuItems()
        {
            foreach(ToolStripItem tsi in this.tsmiDeviceModels.DropDownItems)
            {
                ToolStripMenuItem tsmi = tsi as ToolStripMenuItem;
                if(tsmi == null)
                {
                    continue;
                }

                DeviceModel deviceModel = ProductsConfig.MAudioCorrespondingModel;
                if (ProductsConfig.GetDeviceModelFromText(tsmi.Text, ref deviceModel))
                {
                    tsmi.Checked = this.mController.mEffectorModel.currModel == deviceModel;
                }
                else
                {
                    tsmi.Checked = false;
                }
            }
        }

        private System.Windows.Forms.ToolStripItem[] CreateSupportedDeviceModelMenuItems()
        {
            List<System.Windows.Forms.ToolStripItem> items = new List<ToolStripItem>();
            List<string> modelList = new List<string>();
            foreach(DeviceModel deviceModel in AppConfig.supportedDeviceModelList)
            {
                string modelText = ProductsConfig.GetDeviceModelText(deviceModel);
                if(string.IsNullOrEmpty(modelText) || modelList.Contains(modelText))
                {
                    continue;
                }

                modelList.Add(modelText);

                ToolStripMenuItem tsmi = new ToolStripMenuItem();
                tsmi.Checked = this.mController.mEffectorModel.currModel == deviceModel;
                tsmi.CheckState = System.Windows.Forms.CheckState.Checked;
                tsmi.Name = string.Format("tsmi_Model_{0}", modelText);
                tsmi.Size = new Size(168, 22);
                tsmi.Text = modelText;
                tsmi.Click += new System.EventHandler(this.tsmi_DeviceModel_Click);

                items.Add(tsmi);
            }

            return items.ToArray();
        }

        private void tsmi_DeviceModel_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem tsmi = sender as ToolStripMenuItem;
            if(tsmi == null || string.IsNullOrEmpty(tsmi.Text))
            {
                return;
            }

            DeviceModel deviceModel = ProductsConfig.MAudioCorrespondingModel;
            if (ProductsConfig.GetDeviceModelFromText(tsmi.Text, ref deviceModel) && deviceModel != this.mController.mEffectorModel.currModel)
            {
                if(this.mController.SwitchCurrDeviceModel(deviceModel))
                {
                    this.UpdateUI(false);
                }
            }
        }

#if COMM_TEST
        private string modeDataHashCode = string.Empty;
        private int commTestCount = 0;
        //private byte[] srcData;
        private Controls.MSwitch switchCommTest = new AEC.UI.Controls.MSwitch();

        private void InitCommTestUI()
        {
            this.switchCommTest.Anchor = AnchorStyles.Right | AnchorStyles.Top;
            this.switchCommTest.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(168)))), ((int)(((byte)(168)))), ((int)(((byte)(168)))));
            this.switchCommTest.BorderWidth = 1F;
            this.switchCommTest.CheckedDisbaledBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.switchCommTest.CheckedDisbaledFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(6)))), ((int)(((byte)(162)))), ((int)(((byte)(78)))));
            this.switchCommTest.CheckedDisbaledTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.switchCommTest.CheckedHoverBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.switchCommTest.CheckedHoverFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(107)))), ((int)(((byte)(213)))), ((int)(((byte)(106)))));
            this.switchCommTest.CheckedHoverTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.switchCommTest.CheckedNormalBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(168)))), ((int)(((byte)(168)))), ((int)(((byte)(168)))));
            this.switchCommTest.CheckedNormalFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(7)))), ((int)(((byte)(193)))), ((int)(((byte)(96)))));
            this.switchCommTest.CheckedNormalTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.switchCommTest.CheckedPressedBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(96)))), ((int)(((byte)(96)))));
            this.switchCommTest.CheckedPressedFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(6)))), ((int)(((byte)(175)))), ((int)(((byte)(87)))));
            this.switchCommTest.CheckedPressedTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(168)))), ((int)(((byte)(168)))), ((int)(((byte)(168)))));
            this.switchCommTest.fillColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(16)))), ((int)(((byte)(16)))));
            this.switchCommTest.isAutoCheck = true;
            this.switchCommTest.IsChecked = false;
            this.switchCommTest.IsCheckedInit = false;
            this.switchCommTest.Location = new System.Drawing.Point(844, 2);
            this.switchCommTest.Name = "switchCommTest";
            this.switchCommTest.Radius = 29;
            this.switchCommTest.ShowBorder = false;
            this.switchCommTest.ShowText = true;
            this.switchCommTest.Size = new System.Drawing.Size(128, 29);
            this.switchCommTest.TabIndex = 33;
            this.switchCommTest.Text = "通讯测试";
            this.switchCommTest.UncheckedDisbaledBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.switchCommTest.UncheckedDisbaledFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.switchCommTest.UncheckedDisbaledTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.switchCommTest.UncheckedHoverBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.switchCommTest.UncheckedHoverFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.switchCommTest.UncheckedHoverTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.switchCommTest.UncheckedNormalBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(168)))), ((int)(((byte)(168)))), ((int)(((byte)(168)))));
            this.switchCommTest.UncheckedNormalFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(96)))), ((int)(((byte)(96)))));
            this.switchCommTest.UncheckedNormalTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.switchCommTest.UncheckedPressedBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(96)))), ((int)(((byte)(96)))));
            this.switchCommTest.UncheckedPressedFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.switchCommTest.UncheckedPressedTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(168)))), ((int)(((byte)(168)))), ((int)(((byte)(168)))));

            this.Controls.Add(this.switchCommTest);
            this.switchCommTest.BringToFront();
        }

        private void AsyncRandLoadDataToDevice(object state)
        {
            (this.systemPanel as SystemPanel).RandLoadDataToDevice();
        }

        private void AsyncRandUpdateAndSaveMode(object state)
        {
            Log.OutputLine("第 {0} 次测试...", this.commTestCount + 1);
            //上传数据成功后随机更新数据
            this.mController.currEffectorModel.RandomTest();

            //srcData = this.mController.currEffectorModel.Serialize(this.mController.mEffectorModel.currModelType, true);

            modeDataHashCode = AEC.Core.Utils.BytesToHexStr(HashHelper.MD5(this.mController.currEffectorModel.Serialize(this.mController.mEffectorModel.currModelType, false)));

            mController.SeralizeEffectorModelToFile(this.mController.currEffectorModel, this.mController.customModeDataFileManager.GetPresetFileFullPath(this.mController.mEffectorModel.currModelType, "test_comm_random_" + AEC.Tools.StringHelper.DateTime2String(DateTime.Now) + " " + commTestCount.ToString()));

            //然后随机保存模式
            (this.systemPanel as SystemPanel).RandSaveMode();
            this.mController.currEffectorModel.InitFreqRespAndEQGraphic();
            this.UpdateUIFromData();
        }

        private void AsyncRandCallMode(object state)
        {
            (this.systemPanel as SystemPanel).RandCallMode();
        }
#endif
    }
}
