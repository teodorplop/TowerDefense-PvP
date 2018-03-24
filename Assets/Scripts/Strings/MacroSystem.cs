using System;
using System.Collections.Generic;

public class MacroSystem {
    private static Dictionary<string, object> macros = new Dictionary<string, object>();
    private static Dictionary<string, Delegate> macroEvents = new Dictionary<string, Delegate>();

    public delegate void UpdateLabelDelegate();

    public static void SetMacroValue(string macro, object value) {
        macros[macro] = value;
        if (macroEvents.ContainsKey(macro)) {
            macroEvents[macro].DynamicInvoke();
        }
    }

    public static void Register(UpdateLabelDelegate updateLabel, string macroText) {
        string[] macroArray = GetMacros(macroText);
        foreach (string str in macroArray) {
            if (!macroEvents.ContainsKey(str)) {
                macroEvents.Add(str, updateLabel);
            } else {
                Delegate tempDel = macroEvents[str];
                macroEvents[str] = Delegate.Combine(tempDel, updateLabel);
            }
        }
    }

    public static void Unregister(UpdateLabelDelegate updateLabel, string macroText) {
        string[] macroArray = GetMacros(macroText);
        foreach (string str in macroArray) {
            if (macroEvents.ContainsKey(str)) {
                Delegate currentDel = Delegate.Remove(macroEvents[str], updateLabel);
                if (currentDel == null) {
                    macroEvents.Remove(str);
                } else {
                    macroEvents[str] = currentDel;
                }
            }
        }
    }

    public static string TranslateMacros(string text) {
        string result = "";
        int lastI = 0, i, j;
        i = text.IndexOf('{');
        j = text.IndexOf('}', i + 1);
        while (i != -1 && j != -1) {
            if (i - 1 >= lastI) {
                result += text.Substring(lastI, (i - 1) - lastI + 1);
            }
            string macroString = text.Substring(i + 1, (j - 1) - (i + 1) + 1);
            result += Strings.GetText(GetMacroValue(macroString).ToString());

            lastI = j + 1;
            if (lastI + 1 >= text.Length) {
                i = j = -1;
            } else {
                i = text.IndexOf('{', lastI);
                j = text.IndexOf('}', i + 1);
            }
        }
        result += text.Substring(lastI);

        return result;
    }

    private static string[] GetMacros(string text) {
        List<string> macros = new List<string>();

        int lastI, i, j;
        i = text.IndexOf('{');
        j = text.IndexOf('}', i + 1);
        while (i != -1 && j != -1) {
            string macroString = text.Substring(i + 1, (j - 1) - (i + 1) + 1);
            macros.Add(macroString);

            lastI = j + 1;
            if (lastI + 1 >= text.Length) {
                i = j = -1;
            } else {
                i = text.IndexOf('{', lastI);
                j = text.IndexOf('}', i + 1);
            }
        }

        return macros.ToArray();
    } 

    private static object GetMacroValue(string macro) {
        return macros.ContainsKey(macro) ? macros[macro] : macro;
    }
}
