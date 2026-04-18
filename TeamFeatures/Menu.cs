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

        public void Populate(List<Feature> features, FeatureSelect origin)
        {
            ScrollContainer.RectPosition = new Vector2(origin.RectGlobalPosition.x + origin.RectSize.x, 0 - ScrollContainer.RectGlobalPosition.y + 50);

            foreach(Feature feature in features)
            {
                MenuOption option = MenuOption.Scene.Instance<MenuOption>();
                Container.AddChild(option);
                option.Init(feature, FeatureTypes.Hero);

                option.Connect("OptionSelected", this, nameof(OnOptionSelected));
            }

            CallDeferred(nameof(VerifyScreenPosition), origin);
        }

        private void VerifyScreenPosition(FeatureSelect origin)
        {
            //Check if the Menu goes off-screen.

            if(ScrollContainer.RectGlobalPosition.x + ScrollContainer.RectSize.x >= GetViewport().GetVisibleRect().Size.x)
            {
                ScrollContainer.RectGlobalPosition = new Vector2(origin.RectGlobalPosition.x - ScrollContainer.RectSize.x, ScrollContainer.RectGlobalPosition.y);
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
