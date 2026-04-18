using Godot;
using SquareGen.Globals;
using System.Collections.Generic;
using static SquareGen.Globals.Feature;

namespace SquareGen.TeamFeatures
{
    public partial class Menu : ScrollContainer
    {
        public static readonly PackedScene Scene = ResourceLoader.Load<PackedScene>("res://Menu.tscn");

        //[Signal]
        //public delegate void OptionSelectedEventHandler(Feature feature);

        private VBoxContainer Container = null;

        public override void _Ready()
        {
            Container = GetNode<VBoxContainer>("Container");
        }

        public void Populate(float x)
        {
            RectPosition = new Vector2(x, 0 - RectGlobalPosition.y + 50);

            List<Feature> features = DataLoader.GetHeroes();
            foreach(Feature feature in features)
            {
                MenuOption option = MenuOption.Scene.Instance<MenuOption>();
                Container.AddChild(option);
                option.Init(feature, FeatureTypes.Hero);
            }
        }

        public override void _Input(InputEvent @event)
        {
            if(@event is InputEventMouseButton click && !click.Pressed)
            {
                QueueFree();
            }
            if(@event is InputEventScreenTouch touch && !touch.Pressed)
            {
                QueueFree();
            }
        }
    }
}
