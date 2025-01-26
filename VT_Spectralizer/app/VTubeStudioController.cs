using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualBasic;
using VTS.Core;

namespace VT_Spectralizer.app;

public class VTubeStudioController : VTS.Core.CoreVTSPlugin
{
    private IVTSLogger _vtsLogger;

    public bool IsConnected = false;
    public bool paramsCreated = false;
    private int _interval = 100;

    private string plugin_name = "VTSpectralizer";
    private string developer = "ShimizuAise";
    private string icon = "";

    private string PluginIcon;

    public bool updatingParams = false;

    private VTSParameterInjectionValue VTSpec_toggle = new VTSParameterInjectionValue();
    private VTSParameterInjectionValue VTSpec_subBass = new VTSParameterInjectionValue();
    private VTSParameterInjectionValue VTSpec_bass = new VTSParameterInjectionValue();
    private VTSParameterInjectionValue VTSpec_lowMid = new VTSParameterInjectionValue();
    private VTSParameterInjectionValue VTSpec_midRange = new VTSParameterInjectionValue();
    private VTSParameterInjectionValue VTSpec_upperMid = new VTSParameterInjectionValue();
    private VTSParameterInjectionValue VTSpec_presence = new VTSParameterInjectionValue();
    private VTSParameterInjectionValue VTSpec_brilliance = new VTSParameterInjectionValue();
    private VTSParameterInjectionValue VTSpec_volume = new VTSParameterInjectionValue();

    // Reference to Form1 to invoke UI updates
    private readonly FormMain _form;

    public VTubeStudioController(IVTSLogger logger, int updateIntervalMs, string pluginName, string pluginAuthor, string pluginIcon, FormMain form) : base(logger, updateIntervalMs, pluginName, pluginAuthor, pluginIcon)
    {
        _form = form;
        _vtsLogger = logger;
        _interval = updateIntervalMs;
        plugin_name = pluginName;
        developer = pluginAuthor;
        PluginIcon = pluginIcon;
    }

    public async Task<bool> ConnectToVtubeStudio()
    {
        try
        {

            await this.InitializeAsync(new WebSocketImpl(_vtsLogger), new NewtonsoftJsonUtilityImpl(),
                new TokenStorageImpl("./token"),
                () => UpdateLogInvoker("Disconnected"));
            UpdateLogInvoker($"VTube studio connected!");
            IsConnected = true;
            CreateAllParams();
        }
        catch (Exception exception)
        {
            UpdateLogInvoker($"Attempt to connect to vtube studio error {exception.Message}");
        }

        return IsConnected;
    }


    public void CreateAllParams()
    {
        CreateParam("VTSpec_toggle", "Used to Toggle params when this is on.", 1, this.VTSpec_toggle);
        CreateParam("VTSpec_subBass", "Audio Frequency Sub Bass", 1, this.VTSpec_subBass);
        CreateParam("VTSpec_bass", "Audio Frequency Bass", 1, this.VTSpec_bass);
        CreateParam("VTSpec_lowMid", "Audio Frequency Low-Mid", 1, this.VTSpec_lowMid);
        CreateParam("VTSpec_midRange", "Audio Frequency Midrange", 1, this.VTSpec_midRange);
        CreateParam("VTSpec_upperMid", "Audio Frequency Upper-Mid", 1, this.VTSpec_upperMid);
        CreateParam("VTSpec_presence", "Audio Frequency Presence", 1, this.VTSpec_presence);
        CreateParam("VTSpec_brilliance", "Audio Frequency Brilliance", 1, this.VTSpec_brilliance);
        CreateParam("VTSpec_volume", "Audio Frequency Volume", 1, this.VTSpec_volume);
        paramsCreated = true;
    }

    private void CreateParam(string paramName, string paramDescriptionKey, int paramMax, VTSParameterInjectionValue value)
    {
        value.id = paramName;
        value.value = 0;
        value.weight = 1;
        if (this.IsAuthenticated)
        {
            VTSCustomParameter newParam = new VTSCustomParameter();
            newParam.defaultValue = 0;
            newParam.min = 0;
            newParam.max = paramMax;
            newParam.parameterName = paramName;
            newParam.explanation = paramDescriptionKey;

            UpdateLogInvoker(string.Format("Creating tracking parameter: {0}", paramName));
            
            this.AddCustomParameter(
                newParam,
                (s) => UpdateLogInvoker(string.Format("Successfully created parameter in VTube Studio: {0}", paramName)),
                (e) => UpdateLogInvoker(string.Format("Error while injecting Parameter Data {0} into VTube Studio: {1} - {2}",
                        paramName, e.data.errorID, e.data.message))
            );
        }
    }

    public async void SendParams(float[] values, float toggleValue)
    {
        if (!IsAuthenticated) return;
        VTSParameterInjectionValue VTSpec_toggle_value = new VTSParameterInjectionValue() { id = "VTSpec_toggle", value = toggleValue, weight = 1 };
        VTSParameterInjectionValue VTSpec_subBass_value = new VTSParameterInjectionValue() { id = "VTSpec_subBass", value = values[0], weight = 1 };
        VTSParameterInjectionValue VTSpec_bass_value = new VTSParameterInjectionValue() { id = "VTSpec_bass", value = values[1], weight = 1 };
        VTSParameterInjectionValue VTSpec_lowMid_value = new VTSParameterInjectionValue() { id = "VTSpec_lowMid", value = values[2], weight = 1 };
        VTSParameterInjectionValue VTSpec_midRange_value = new VTSParameterInjectionValue() { id = "VTSpec_midRange", value = values[3], weight = 1 }; 
        VTSParameterInjectionValue VTSpec_upperMid_value = new VTSParameterInjectionValue() { id = "VTSpec_upperMid", value = values[4], weight = 1 }; 
        VTSParameterInjectionValue VTSpec_presence_value = new VTSParameterInjectionValue() { id = "VTSpec_presence", value = values[5], weight = 1 }; 
        VTSParameterInjectionValue VTSpec_brilliance_value = new VTSParameterInjectionValue() { id = "VTSpec_brilliance", value = values[6], weight = 1 };
        VTSParameterInjectionValue VTSpec_volume_value = new VTSParameterInjectionValue() { id = "VTSpec_volume", value = values.Max(), weight = 1 };

        try
        {
            await this.InjectParameterValues(new[]
            {
                VTSpec_toggle_value, VTSpec_subBass_value, VTSpec_bass_value, VTSpec_lowMid_value, VTSpec_midRange_value, VTSpec_upperMid_value, VTSpec_presence_value, VTSpec_brilliance_value, VTSpec_volume_value
            });
        }
        catch (Exception ex) { Console.WriteLine(ex.Message); }
        
    }

    private void UpdateLogInvoker(string message)
    {
        try
        {
            _form.Invoke(new Action(() =>
            {
                // Update Form1 UI controls (labels) from the background thread
                _form.UpdateLog(message);
            }));
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
        }
    }
}
