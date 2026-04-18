using Godot;
using SquareGen.Globals;
using static SquareGen.Globals.Feature;

namespace SquareGen.TeamFeatures
{
    public partial class MenuOption : TextureButton
    {
        public static readonly PackedScene Scene = ResourceLoader.Load<PackedScene>("res://MenuOption.tscn");

        //[Signal]
        //public delegate void OptionSelectedEventHandler(Feature feature);

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
        }

        public void Init(Feature feature, FeatureTypes type)
        {
            //Call this before use, after MenuOption enters the scene.

            FeatureType = type;
            Feature = feature;

            Icon.Texture = Feature.Icon;
            PointLabel.BbcodeText = "[center]" + feature.Points;

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

            RectMinSize = Container.RectSize;
            Background.RectMinSize = Container.RectSize;
        }
    }
}
