using Jacobi.Vst.Core;
using Jacobi.Vst.Core.Host;
using Jacobi.Vst.Interop.Host;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;

namespace VSTManager
{
    class HostCommandStub : IVstHostCommandStub
    {
        /// <summary>
        /// Raised when one of the methods is called.
        /// </summary>
        public event EventHandler<PluginCalledEventArgs> PluginCalled;

        private void RaisePluginCalled(string message)
        {
            EventHandler<PluginCalledEventArgs> handler = PluginCalled;

            if (handler != null)
            {
                handler(this, new PluginCalledEventArgs(message));
            }
        }

        #region IVstHostCommandsStub Members

        /// <inheritdoc />
        public IVstPluginContext PluginContext { get; set; }

        #endregion

        #region IVstHostCommands20 Members

        /// <inheritdoc />
        public bool BeginEdit(int index)
        {
            RaisePluginCalled("BeginEdit(" + index + ")");

            return false;
        }

        /// <inheritdoc />
        public Jacobi.Vst.Core.VstCanDoResult CanDo(string cando)
        {
            RaisePluginCalled("CanDo(" + cando + ")");
            return Jacobi.Vst.Core.VstCanDoResult.Unknown;
        }

        /// <inheritdoc />
        public bool CloseFileSelector(Jacobi.Vst.Core.VstFileSelect fileSelect)
        {
            RaisePluginCalled("CloseFileSelector(" + fileSelect.Command + ")");
            return false;
        }

        /// <inheritdoc />
        public bool EndEdit(int index)
        {
            RaisePluginCalled("EndEdit(" + index + ")");
            return false;
        }

        /// <inheritdoc />
        public Jacobi.Vst.Core.VstAutomationStates GetAutomationState()
        {
            RaisePluginCalled("GetAutomationState()");
            return Jacobi.Vst.Core.VstAutomationStates.Off;
        }

        /// <inheritdoc />
        public int GetBlockSize()
        {
            RaisePluginCalled("GetBlockSize()");
            return 1024;
        }

        /// <inheritdoc />
        public string GetDirectory()
        {
            RaisePluginCalled("GetDirectory()");
            return null;
        }

        /// <inheritdoc />
        public int GetInputLatency()
        {
            RaisePluginCalled("GetInputLatency()");
            return 0;
        }

        /// <inheritdoc />
        public Jacobi.Vst.Core.VstHostLanguage GetLanguage()
        {
            RaisePluginCalled("GetLanguage()");
            return Jacobi.Vst.Core.VstHostLanguage.NotSupported;
        }

        /// <inheritdoc />
        public int GetOutputLatency()
        {
            RaisePluginCalled("GetOutputLatency()");
            return 0;
        }

        /// <inheritdoc />
        public Jacobi.Vst.Core.VstProcessLevels GetProcessLevel()
        {
            RaisePluginCalled("GetProcessLevel()");
            return Jacobi.Vst.Core.VstProcessLevels.Unknown;
        }

        /// <inheritdoc />
        public string GetProductString()
        {
            RaisePluginCalled("GetProductString()");
            return "VST.NET";
        }

        /// <inheritdoc />
        public float GetSampleRate()
        {
            RaisePluginCalled("GetSampleRate()");
            return 44.8f;
        }

        /// <inheritdoc />
        public Jacobi.Vst.Core.VstTimeInfo GetTimeInfo(Jacobi.Vst.Core.VstTimeInfoFlags filterFlags)
        {
            RaisePluginCalled("GetTimeInfo(" + filterFlags + ")");
            return null;
        }

        /// <inheritdoc />
        public string GetVendorString()
        {
            RaisePluginCalled("GetVendorString()");
            return "Jacobi Software";
        }

        /// <inheritdoc />
        public int GetVendorVersion()
        {
            RaisePluginCalled("GetVendorVersion()");
            return 1000;
        }

        /// <inheritdoc />
        public bool IoChanged()
        {
            RaisePluginCalled("IoChanged()");
            return false;
        }

        /// <inheritdoc />
        public bool OpenFileSelector(Jacobi.Vst.Core.VstFileSelect fileSelect)
        {
            RaisePluginCalled("OpenFileSelector(" + fileSelect.Command + ")");
            return false;
        }

        /// <inheritdoc />
        public bool ProcessEvents(Jacobi.Vst.Core.VstEvent[] events)
        {
            RaisePluginCalled("ProcessEvents(" + events.Length + ")");
            return false;
        }

        /// <inheritdoc />
        public bool SizeWindow(int width, int height)
        {
            RaisePluginCalled("SizeWindow(" + width + ", " + height + ")");
            return false;
        }

        /// <inheritdoc />
        public bool UpdateDisplay()
        {
            RaisePluginCalled("UpdateDisplay()");
            return false;
        }

        #endregion

        #region IVstHostCommands10 Members

        /// <inheritdoc />
        public int GetCurrentPluginID()
        {
            RaisePluginCalled("GetCurrentPluginID()");
            if (PluginContext.PluginInfo == null)
                throw new Exception("Plugin info is null");
            return PluginContext.PluginInfo.PluginID;
        }

        /// <inheritdoc />
        public int GetVersion()
        {
            RaisePluginCalled("GetVersion()");
            return 1000;
        }

        /// <inheritdoc />
        public void ProcessIdle()
        {
            RaisePluginCalled("ProcessIdle()");
        }

        /// <inheritdoc />
        public void SetParameterAutomated(int index, float value)
        {
            RaisePluginCalled("SetParameterAutomated(" + index + ", " + value + ")");
        }

        #endregion
    }

    /// <summary>
    /// Event arguments used when one of the mehtods is called.
    /// </summary>
    class PluginCalledEventArgs : EventArgs
    {
        /// <summary>
        /// Constructs a new instance with a <paramref name="message"/>.
        /// </summary>
        /// <param name="message"></param>
        public PluginCalledEventArgs(string message)
        {
            Message = message;
        }

        /// <summary>
        /// Gets the message.
        /// </summary>
        public string Message { get; private set; }
    }

    public class VstPluginProperties: IDisposable
    {
        public VstPluginProperties(string path)
        {
            PluginContext = OpenPlugin(path);
        }
        public void Dispose()
        {
            if (PluginContext != null) PluginContext.Dispose();
        }
        private void HostCmdStub_PluginCalled(object sender, PluginCalledEventArgs e)
        {
            HostCommandStub hostCmdStub = (HostCommandStub)sender;

            // can be null when called from inside the plugin main entry point.
            if (hostCmdStub.PluginContext.PluginInfo != null)
            {
                Console.WriteLine("Plugin " + hostCmdStub.PluginContext.PluginInfo.PluginID + " called:" + e.Message);
            }
            else
            {
                Console.WriteLine("The loading Plugin called:" + e.Message);
            }
        }

        private VstPluginContext OpenPlugin(string pluginPath)
        {
            try
            {
                HostCommandStub hostCmdStub = new HostCommandStub();
                hostCmdStub.PluginCalled += new EventHandler<PluginCalledEventArgs>(HostCmdStub_PluginCalled);

                VstPluginContext ctx = VstPluginContext.Create(pluginPath, hostCmdStub);

                // add custom data to the context
                ctx.Set("PluginPath", pluginPath);
                ctx.Set("HostCmdStub", hostCmdStub);

                // actually open the plugin itself
                ctx.PluginCommandStub.Open();

                return ctx;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }

            return null;
        }

        public VstPluginContext PluginContext = null;

        public List<string> getPluginInfo()
        {
            if (PluginContext != null)
            {
                var pluginInfo = new List<string>(); // Create new list of strings

                // plugin product
                pluginInfo.Add("Plugin Name " + PluginContext.PluginCommandStub.GetEffectName());
                pluginInfo.Add("Product " + PluginContext.PluginCommandStub.GetProductString());
                pluginInfo.Add("Vendor " + PluginContext.PluginCommandStub.GetVendorString());
                pluginInfo.Add("Vendor Version " + PluginContext.PluginCommandStub.GetVendorVersion().ToString());
                pluginInfo.Add("Vst Support " + PluginContext.PluginCommandStub.GetVstVersion().ToString());
                pluginInfo.Add("Plugin Category " + PluginContext.PluginCommandStub.GetCategory().ToString());

                // plugin info
                pluginInfo.Add("Flags " + PluginContext.PluginInfo.Flags.ToString());
                pluginInfo.Add("Plugin ID " + PluginContext.PluginInfo.PluginID.ToString());
                pluginInfo.Add("Plugin Version " + PluginContext.PluginInfo.PluginVersion.ToString());
                pluginInfo.Add("Audio Input Count " + PluginContext.PluginInfo.AudioInputCount.ToString());
                pluginInfo.Add("Audio Output Count " + PluginContext.PluginInfo.AudioOutputCount.ToString());
                pluginInfo.Add("Initial Delay " + PluginContext.PluginInfo.InitialDelay.ToString());
                pluginInfo.Add("Program Count " + PluginContext.PluginInfo.ProgramCount.ToString());
                pluginInfo.Add("Parameter Count " + PluginContext.PluginInfo.ParameterCount.ToString());
                pluginInfo.Add("Tail Size " + PluginContext.PluginCommandStub.GetTailSize().ToString());

                // can do
                
                pluginInfo.Add("CanDo: " + VstPluginCanDo.Bypass + PluginContext.PluginCommandStub.CanDo(VstCanDoHelper.ToString(VstPluginCanDo.Bypass)).ToString());
                pluginInfo.Add("CanDo: " + VstPluginCanDo.MidiProgramNames + PluginContext.PluginCommandStub.CanDo(VstCanDoHelper.ToString(VstPluginCanDo.MidiProgramNames)).ToString());
                pluginInfo.Add("CanDo: " + VstPluginCanDo.Offline + PluginContext.PluginCommandStub.CanDo(VstCanDoHelper.ToString(VstPluginCanDo.Offline)).ToString());
                pluginInfo.Add("CanDo: " + VstPluginCanDo.ReceiveVstEvents + PluginContext.PluginCommandStub.CanDo(VstCanDoHelper.ToString(VstPluginCanDo.ReceiveVstEvents)).ToString());
                pluginInfo.Add("CanDo: " + VstPluginCanDo.ReceiveVstMidiEvent + PluginContext.PluginCommandStub.CanDo(VstCanDoHelper.ToString(VstPluginCanDo.ReceiveVstMidiEvent)).ToString());
                pluginInfo.Add("CanDo: " + VstPluginCanDo.ReceiveVstTimeInfo + PluginContext.PluginCommandStub.CanDo(VstCanDoHelper.ToString(VstPluginCanDo.ReceiveVstTimeInfo)).ToString());
                pluginInfo.Add("CanDo: " + VstPluginCanDo.SendVstEvents + PluginContext.PluginCommandStub.CanDo(VstCanDoHelper.ToString(VstPluginCanDo.SendVstEvents)).ToString());
                pluginInfo.Add("CanDo: " + VstPluginCanDo.SendVstMidiEvent + PluginContext.PluginCommandStub.CanDo(VstCanDoHelper.ToString(VstPluginCanDo.SendVstMidiEvent)).ToString());

                pluginInfo.Add("CanDo: " + VstPluginCanDo.ConformsToWindowRules + PluginContext.PluginCommandStub.CanDo(VstCanDoHelper.ToString(VstPluginCanDo.ConformsToWindowRules)).ToString());
                pluginInfo.Add("CanDo: " + VstPluginCanDo.Metapass + PluginContext.PluginCommandStub.CanDo(VstCanDoHelper.ToString(VstPluginCanDo.Metapass)).ToString());
                pluginInfo.Add("CanDo: " + VstPluginCanDo.MixDryWet + PluginContext.PluginCommandStub.CanDo(VstCanDoHelper.ToString(VstPluginCanDo.MixDryWet)).ToString());
                pluginInfo.Add("CanDo: " + VstPluginCanDo.Multipass + PluginContext.PluginCommandStub.CanDo(VstCanDoHelper.ToString(VstPluginCanDo.Multipass)).ToString());
                pluginInfo.Add("CanDo: " + VstPluginCanDo.NoRealTime + PluginContext.PluginCommandStub.CanDo(VstCanDoHelper.ToString(VstPluginCanDo.NoRealTime)).ToString());
                pluginInfo.Add("CanDo: " + VstPluginCanDo.PlugAsChannelInsert + PluginContext.PluginCommandStub.CanDo(VstCanDoHelper.ToString(VstPluginCanDo.PlugAsChannelInsert)).ToString());
                pluginInfo.Add("CanDo: " + VstPluginCanDo.PlugAsSend + PluginContext.PluginCommandStub.CanDo(VstCanDoHelper.ToString(VstPluginCanDo.PlugAsSend)).ToString());
                pluginInfo.Add("CanDo: " + VstPluginCanDo.SendVstTimeInfo + PluginContext.PluginCommandStub.CanDo(VstCanDoHelper.ToString(VstPluginCanDo.SendVstTimeInfo)).ToString());
                pluginInfo.Add("CanDo: " + VstPluginCanDo.x1in1out + PluginContext.PluginCommandStub.CanDo(VstCanDoHelper.ToString(VstPluginCanDo.x1in1out)).ToString());
                pluginInfo.Add("CanDo: " + VstPluginCanDo.x1in2out + PluginContext.PluginCommandStub.CanDo(VstCanDoHelper.ToString(VstPluginCanDo.x1in2out)).ToString());
                pluginInfo.Add("CanDo: " + VstPluginCanDo.x2in1out + PluginContext.PluginCommandStub.CanDo(VstCanDoHelper.ToString(VstPluginCanDo.x2in1out)).ToString());
                pluginInfo.Add("CanDo: " + VstPluginCanDo.x2in2out + PluginContext.PluginCommandStub.CanDo(VstCanDoHelper.ToString(VstPluginCanDo.x2in2out)).ToString());
                pluginInfo.Add("CanDo: " + VstPluginCanDo.x2in4out + PluginContext.PluginCommandStub.CanDo(VstCanDoHelper.ToString(VstPluginCanDo.x2in4out)).ToString());
                pluginInfo.Add("CanDo: " + VstPluginCanDo.x4in2out + PluginContext.PluginCommandStub.CanDo(VstCanDoHelper.ToString(VstPluginCanDo.x4in2out)).ToString());
                pluginInfo.Add("CanDo: " + VstPluginCanDo.x4in4out + PluginContext.PluginCommandStub.CanDo(VstCanDoHelper.ToString(VstPluginCanDo.x4in4out)).ToString());
                pluginInfo.Add("CanDo: " + VstPluginCanDo.x4in8out + PluginContext.PluginCommandStub.CanDo(VstCanDoHelper.ToString(VstPluginCanDo.x4in8out)).ToString());
                pluginInfo.Add("CanDo: " + VstPluginCanDo.x8in4out + PluginContext.PluginCommandStub.CanDo(VstCanDoHelper.ToString(VstPluginCanDo.x8in4out)).ToString());
                pluginInfo.Add("CanDo: " + VstPluginCanDo.x8in8out + PluginContext.PluginCommandStub.CanDo(VstCanDoHelper.ToString(VstPluginCanDo.x8in8out)).ToString());
                
                pluginInfo.Add("Program: " + PluginContext.PluginCommandStub.GetProgram());
                pluginInfo.Add("Program Name: " + PluginContext.PluginCommandStub.GetProgramName());

                for (int i = 0; i < PluginContext.PluginInfo.ParameterCount; i++)
                {
                    string name = PluginContext.PluginCommandStub.GetParameterName(i);
                    string label = PluginContext.PluginCommandStub.GetParameterLabel(i);
                    string display = PluginContext.PluginCommandStub.GetParameterDisplay(i);
                    bool canBeAutomated = PluginContext.PluginCommandStub.CanParameterBeAutomated(i);

                    pluginInfo.Add(String.Format("Parameter Index: {0} Parameter Name: {1} Display: {2} Label: {3} Can be automated: {4}", i, name, display, label, canBeAutomated));
                }
                return pluginInfo;
            }
            return null;
        }
    }

    [DefaultPropertyAttribute("ProductName")]
    public class PluginProperties
    {
        public PluginProperties()
        {

        }

        private string _fullPath;
        private long _size;
        private string _productVersion;
        private string _fileVersion;
        private string _companyName;
        private string _productName;
        private string _fileDescription;

        public string FullPath
        {
            get
            {
                return _fullPath;
            }
            set
            {
                _fullPath = value;
            }
        }

        public long Size
        {
            get
            {
                return _size;
            }
            set
            {
                _size = value;
            }
        }
        public string ProductVersion
        {
            get
            {
                return _productVersion;
            }
            set
            {
                _productVersion = value;
            }
        }
        public string FileVersion
        {
            get
            {
                return _fileVersion;
            }
            set
            {
                _fileVersion = value;
            }
        }

        public string CompanyName
        {
            get
            {
                return _companyName;
            }
            set
            {
                _companyName = value;
            }
        }
        public string ProductName
        {
            get
            {
                return _productName;
            }
            set
            {
                _productName = value;
            }
        }

        public string FileDescription
        {
            get
            {
                return _fileDescription;
            }
            set
            {
                _fileDescription = value;
            }
        }

    }
}
