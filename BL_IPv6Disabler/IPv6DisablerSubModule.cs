using TaleWorlds.MountAndBlade;
using TaleWorlds.Library;
using System.Text.Json.Nodes;
using System.Text.Json;
using TaleWorlds.Engine;
using System.Runtime.InteropServices;

namespace BL_IPv6Disabler;

public class IPv6DisablerSubModule : MBSubModuleBase
{
    const string linuxFilePath = "TaleWorlds.Starter.DotNetCore.Linux.runtimeconfig.json";
    // const string windowsFilePath = "TaleWorlds.Starter.DotNetCore.runtimeconfig.json";

    static IPv6DisablerSubModule()
    {
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
        {
            Debug.Print("This fix does not not work on windows machines! Make sure that Ipv6 is disabled in your network adapter configuration!", 0, Debug.DebugColor.Red);
            return;
        }

        string fullPath = Directory.GetCurrentDirectory();
        string directoryName = System.IO.Path.GetFileName(fullPath);

        bool linuxFound = File.Exists(linuxFilePath);

        if (!linuxFound || directoryName != "Linux64_Shipping_Server")
        {
            Debug.Print("Could not locate TaleWorlds.Starter.DotNetCore.Linux.runtimeconfig.json");
            return;
        }

        string jsonContent = File.ReadAllText(linuxFilePath);

        JsonNode? jsonDocument = JsonNode.Parse(jsonContent);

        if (jsonDocument?["runtimeOptions"]?["configProperties"] is not JsonObject configProperties)
        {
            Debug.Print("configProperties was not found.");
            return;
        }

        if (configProperties.ContainsKey("System.Net.DisableIPv6") && configProperties["System.Net.DisableIPv6"]?.GetValue<bool>() == true)
        {
            Debug.Print("IPv6 is already turned off.");
        }
        else
        {
            configProperties["System.Net.DisableIPv6"] = true;
            File.WriteAllText(linuxFilePath, jsonDocument?.ToJsonString(new JsonSerializerOptions { WriteIndented = true }));
            Debug.Print($"IPv6 was disabled in {linuxFilePath}. Shutting down server!");
            Utilities.ExitProcess(0);
        }
    }
}
