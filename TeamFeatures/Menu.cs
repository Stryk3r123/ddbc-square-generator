using Godot;
using SquareGen.Globals;
using System.Collections.Generic;
using static SquareGen.Globals.Feature;

namespace SquareGen.TeamFeatures
{
    public partial class Menu : CanvasLayer
    {
        public static readonly PackedScene Scene = ResourceLoader.Load<PackedScene>("res://Menu.tscn");

        [Signal]
        public delegate void OptionSelected(Feature feature);

        private VBoxContainer Container = null;
        private ScrollContainer ScrollContainer = null;

        public override void _Ready()
        {
            ScrollContainer = GetNode<ScrollContainer>("ScrollContainer");
            Container = ScrollContainer.GetNode<VBoxContainer>("Container");
        }

        public void Populate(List<Feature> features, float x)
        {
            ScrollContainer.RectPosition = new Vector2(x, 0 - ScrollContainer.RectGlobalPosition.y + 50);

            foreach(Feature feature in features)
            {
                MenuOption option = MenuOption.Scene.Instance<MenuOption>();
                Container.AddChild(option);
                option.Init(feature, FeatureTypes.Hero);

                option.Connect("OptionSelected", this, nameof(OnOptionSelected));
            }
        }

        public override void _Input(InputEvent @event)
        {
            if(@event is InputEventMouseButton click && click.ButtonIndex == (int)ButtonList.Left && !click.Pressed)
            {
                QueueFree();
            }
        }

        private void OnOptionSelected(Feature feature)
        {
            EmitSignal("OptionSelected", feature);
            QueueFree();
        }
    }
}
