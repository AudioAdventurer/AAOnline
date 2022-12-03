using System;
using AudioAdventurer.Library.Common.Interfaces;
using AudioAdventurer.Library.Common.Objects;

namespace AudioAdventurer.Library.Common.Helpers;

public static class ConfigHelper
{
    public static IConfig LoadFromEnvironment(string prefix)
    {
        var config = new Config();

        var envVars = Environment.GetEnvironmentVariables(EnvironmentVariableTarget.Machine);

        foreach (var key in envVars.Keys)
        {
            var tempKey = key.ToString();

            if (tempKey != null)
            {
                if (tempKey.StartsWith(prefix))
                {
                    var configKey = tempKey.Substring(prefix.Length);
                    var value = envVars[key] as string;

                    config.Values.Add(configKey, value);
                }
            }
        }

        return config;
    }
}