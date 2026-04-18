using Godot;
using SquareGen.Globals;
using System.Collections.Generic;

namespace SquareGen.TeamFeatures
{
    public partial class HeroSelect : TextureButton
    {
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

        private void CreateMenu()
        {
            Menu menu = Menu.Scene.Instance<Menu>();
            AddChild(menu);
            menu.Populate(RectSize.x);
            
            HasMenu = true;
            menu.Connect("tree_exited", this, nameof(ClearMenu));
            menu.Connect("OptionSelected", this, nameof(OnOptionSelected));
        }

        private void ClearMenu()
        {
            HasMenu = false;
        }

        private void OnOptionSelected(Feature feature)
        {
            TextureNormal = feature.Icon;
        }
    }
}
