using Godot;
using SquareGen.Globals;
using System.Collections.Generic;

namespace SquareGen.TeamFeatures
{
    public partial class HeroSelect : TextureButton
    {
        private List<Feature> Heroes = new List<Feature>();
        private int CurrentHero = -1;

        public override void _EnterTree()
        {
            base._EnterTree();
            Connect("pressed", this, nameof(OnPressed));
        }

        public override void _Ready()
        {
            Heroes = DataLoader.GetHeroes();
        }

        private void OnPressed()
        {
            CurrentHero++;
            if(CurrentHero == Heroes.Count)
            {
                CurrentHero = 0;
            }
            TextureNormal = Heroes[CurrentHero].Icon;
        }
    }
}
