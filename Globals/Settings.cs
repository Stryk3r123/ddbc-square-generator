using Godot;
using System.Collections.Generic;

namespace SquareGen.Globals
{
    public partial class Settings : Node
    {
        public static class Keys
        {
            public const string TRACK_POINTS = "TrackPoints";
        }

        [Signal]
        public delegate void SettingUpdated(string setting);

        private Dictionary<string, bool> SettingValues = new Dictionary<string, bool>();

        private static Settings Instance = null;

        public override void _EnterTree()
        {
            Instance = this;
        }

        public override void _Ready()
        {
            SetSetting(Keys.TRACK_POINTS, false);
        }

        public static bool GetSetting(string key)
        {
            if(Instance == null)
            {
                return false;
            }
            if (!Instance.SettingValues.ContainsKey(key))
            {
                return false;
            }
            return Instance.SettingValues[key];
        }

        public static void SetSetting(string key, bool value)
        {
            if (Instance == null)
            {
                return;
            }

            if (Instance.SettingValues.ContainsKey(key))
            {
                Instance.SettingValues[key] = value;
            }
            else
            {
                Instance.SettingValues.Add(key, value);
            }

            Instance.EmitSignal("SettingUpdated", key);
        }

        public static void ConnectSignal(string signal, Object subscriber, string func)
        {
            Instance?.Connect(signal, subscriber, func);
        }
    }
}
