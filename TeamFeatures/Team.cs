using Godot;
using System.Collections.Generic;
using SquareGen.Globals;
using System.Linq;

namespace SquareGen.TeamFeatures
{
    public partial class Team : Sprite
    {
        private List<Hero> Heroes = new List<Hero>();

        private static Team Instance = null;

        public override void _EnterTree()
        {
            if(Instance != null)
            {
                Instance.QueueFree();
            }
            Instance = this;
        }

        public override void _Ready()
        {
            foreach(Node node in GetChildren())
            {
                if(node is Hero hero)
                {
                    Heroes.Add(hero);
                }
            }
        }

        private static List<Feature> GetHeroTypes()
        {
            List<Feature> list = new List<Feature>();
            if(Instance == null)
            {
                return list;
            }
            foreach(Hero hero in Instance.Heroes)
            {
                list.Add(hero.GetHeroType());
            }
            return list;
        }

        private static List<Feature> GetSkills()
        {
            List<Feature> list = new List<Feature>();
            if (Instance == null)
            {
                return list;
            }
            foreach (Hero hero in Instance.Heroes)
            {
                list.AddRange(hero.GetSkills());
            }
            return list;
        }

        private static List<Feature> GetTrinkets()
        {
            List<Feature> list = new List<Feature>();
            if (Instance == null)
            {
                return list;
            }
            foreach (Hero hero in Instance.Heroes)
            {
                list.AddRange(hero.GetTrinkets());
            }
            return list;
        }

        public static List<Feature> GetValidHeroes()
        {
            List<Feature> list = DataLoader.GetHeroes();
            if(Instance == null)
            {
                return list;
            }

            List<Feature> heroTypes = GetHeroTypes();
            foreach(Feature heroType in heroTypes)
            {
                list.Remove(heroType);
            }
            return list;
        }

        public static List<Feature> GetValidTrinkets(Feature hero)
        {
            List<Feature> list = DataLoader.GetTrinkets(hero);
            if(Instance == null || hero == null)
            {
                return list;
            }

            List<Feature> trinkets = GetTrinkets();
            foreach(Feature trinket in trinkets)
            {
                if (!IsTrinketValid(trinket))
                {
                    list.Remove(trinket);
                }
            }
            return list;
        }

        private static bool IsTrinketValid(Feature search)
        {
            if(Instance == null)
            {
                return true;
            }
            if(search == null)
            {
                return false;
            }

            List<Feature> trinkets = GetTrinkets();
            int count = 0;
            foreach(Feature trinket in trinkets)
            {
                if(trinket == search)
                {
                    count++;
                }
                if(search.Hero != "generic" || count > 1)
                {
                    return false;
                }
            }

            return true;
        }
    }
}
