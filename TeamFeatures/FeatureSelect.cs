using Godot;
using SquareGen.Globals;
using System.Collections.Generic;
using static SquareGen.Globals.Feature;

namespace SquareGen.TeamFeatures
{
    public abstract partial class FeatureSelect : TextureButton
    {
        [Signal]
        public delegate void FeatureSelected(FeatureSelect select);

        protected abstract FeatureTypes FeatureType { get; }
        public Feature Feature { get; protected set; } = null;
        public Hero Hero = null;

        private bool HasMenu = false;

        public override void _EnterTree()
        {
            base._EnterTree();
            Connect("pressed", this, nameof(OnPressed));
        }

        private void OnPressed()
        {
            if(!HasMenu)
            {
                CreateMenu();
            }
        }

        protected abstract List<Feature> GetValidFeatures();

        private void CreateMenu()
        {
            Menu menu = Menu.Scene.Instance<Menu>();
            AddChild(menu);
            menu.Populate(GetValidFeatures(), RectGlobalPosition.x + RectSize.x);
            
            HasMenu = true;
            menu.Connect("tree_exited", this, nameof(ClearMenu));
            menu.Connect("OptionSelected", this, nameof(OnOptionSelected));
        }

        private void ClearMenu()
        {
            HasMenu = false;
        }

        protected virtual void OnOptionSelected(Feature feature)
        {
            SetFeature(feature, true);
        }

        public void SetFeature(Feature feature, bool signal)
        {
            Feature = feature;
            if (feature != null)
            {
                TextureNormal = feature.Icon;
            }
            else
            {
                TextureNormal = null;
            }

            if (signal)
            {
                EmitSignal("FeatureSelected", this);
            }
        }
    }
}
