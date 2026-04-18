using Godot;
using SquareGen.Globals;
using System.Collections.Generic;
using static SquareGen.Globals.Feature;

namespace SquareGen.TeamFeatures
{
    public partial class PointCounts : VBoxContainer
    {
        private Dictionary<FeatureTypes, RichTextLabel> Counters = new Dictionary<FeatureTypes, RichTextLabel>();

        public override void _Ready()
        {
            Counters.Add(FeatureTypes.Hero, GetNode<RichTextLabel>("Heroes"));
            Counters.Add(FeatureTypes.Skill, GetNode<RichTextLabel>("Skills"));
            Counters.Add(FeatureTypes.Trinket, GetNode<RichTextLabel>("Trinkets"));

            Team.ConnectSignal("PointsRecalculated", this, nameof(OnPointsRecalculated));
            Settings.ConnectSignal("SettingUpdated", this, nameof(OnSettingUpdated));

            OnPointsRecalculated();
            OnSettingUpdated(Settings.Keys.TRACK_POINTS);
        }

        private void OnPointsRecalculated()
        {
            Dictionary<FeatureTypes, int> points = Team.GetPoints();
            Dictionary<FeatureTypes, int> maxPoints = Team.GetMaxPoints();

            foreach(FeatureTypes type in points.Keys)
            {
                RichTextLabel label = Counters[type];
                label.BbcodeText = label.Name + ": " + points[type];
                if(points[type] > maxPoints[type])
                {
                    label.BbcodeText += POINT_EXCEED_ICON;
                }
            }
        }

        private void OnSettingUpdated(string setting)
        {
            if(setting == Settings.Keys.TRACK_POINTS)
            {
                Visible = Settings.GetSetting(setting);
            }
        }
    }
}
