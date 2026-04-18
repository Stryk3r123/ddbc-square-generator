using Godot;

namespace SquareGen.Globals
{
    public readonly struct Feature
    {
        public enum FeatureTypes
        {
            Hero,
            Skill,
            Trinket
        }

        private const string ASSET_DIR = "res://Assets/";
        private const string HERO_SUFFIX = "_portrait_roster";
        private const string TRINKET_PREFIX = "inv_trinket+";
        private const string SKILL_PREFIX = ".ability.";

        public readonly string Name;
        public readonly int Points;
        public readonly string Hero;
        public readonly Texture Icon;

        public Feature(string name, int points, string hero, FeatureTypes type)
        {
            Name = name;
            Points = points;
            Hero = hero;

            string iconPath = ASSET_DIR;
            if(type == FeatureTypes.Trinket)
            {
                iconPath += TRINKET_PREFIX;
            }
            else if(type == FeatureTypes.Skill)
            {
                iconPath += hero + "/" + SKILL_PREFIX;
            }
            iconPath += name;
            if(type == FeatureTypes.Hero)
            {
                iconPath += HERO_SUFFIX;
            }
            iconPath += ".png";

            Icon = ResourceLoader.Load<Texture>(iconPath);
        }
    }
}
