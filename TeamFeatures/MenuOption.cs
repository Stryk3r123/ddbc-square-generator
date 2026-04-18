using Godot;
using SquareGen.Globals;
using static SquareGen.Globals.Feature;

namespace SquareGen.TeamFeatures
{
    public partial class MenuOption : TextureButton
    {
        public static readonly PackedScene Scene = ResourceLoader.Load<PackedScene>("res://MenuOption.tscn");

        [Signal]
        public delegate void OptionSelected(Feature feature);

        private HBoxContainer Container = null;
        private TextureRect Icon = null;
        private RichTextLabel PointLabel = null;
        private ColorRect Background = null;

        private Feature Feature;
        private FeatureTypes FeatureType;

        public override void _Ready()
        {
            Container = GetNode<HBoxContainer>("Container");
            Icon = Container.GetNode<TextureRect>("Icon");
            PointLabel = Container.GetNode<RichTextLabel>("PointContainer/Points");
            Background = GetNode<ColorRect>("Background");

            Connect("pressed", this, nameof(OnPressed));
        }

        public void Init(Feature feature, FeatureTypes type)
        {
            //Call this before use, after MenuOption enters the scene.

            FeatureType = type;
            Feature = feature;

            Icon.Texture = Feature.Icon;
            PointLabel.BbcodeText = "[center]" + feature.Points;

            //TODO properly check points exceeded
            bool exceedsPoints = true;
            switch (type)
            {
                case FeatureTypes.Hero:
                    break;
                case FeatureTypes.Trinket:
                    break;
                case FeatureTypes.Skill:
                    break;
            }
            if (exceedsPoints)
            {
                PointLabel.BbcodeText += POINT_EXCEED_ICON;
            }

            //If point tracking is disabled, hide points and force update size
            if (!Settings.GetSetting(Settings.Keys.TRACK_POINTS))
            {
                PointLabel.Hide();
                Hide();
                Show();
            }

            RectMinSize = Container.RectSize;
            Background.RectMinSize = Container.RectSize;
        }

        private void OnPressed()
        {
            EmitSignal("OptionSelected", Feature);
        }
    }
}
