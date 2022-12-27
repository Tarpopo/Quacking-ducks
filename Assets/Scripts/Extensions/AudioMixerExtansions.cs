using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public static class AudioMixerExtansions
{
    public static string[] GetExposedParameters(this AudioMixer audioMixer)
    {
        var array = (Array)audioMixer.GetType().GetProperty("exposedParameters")?.GetValue(audioMixer, null);
        if (array == null) return default;
        var exposedParams = new string[array.Length];
        for (int i = 0; i < array.Length; i++)
        {
            var o = array.GetValue(i);
            var parameter = (string)o.GetType().GetField("name").GetValue(o);
            exposedParams[i] = parameter;
        }

        return exposedParams;
    }
}