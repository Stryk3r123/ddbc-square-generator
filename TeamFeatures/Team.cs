using Godot;
using System.Collections.Generic;
using SquareGen.Globals;
using static SquareGen.Globals.Feature;

namespace SquareGen.TeamFeatures
{
    public partial class Team : Sprite
    {
        [Signal]
        public delegate void PointsRecalculated();

        private List<Hero> Heroes = new List<Hero>();

        private Dictionary<FeatureTypes, int> Points = new Dictionary<FeatureTypes, int>();
        private Dictionary<FeatureTypes, int> MaxPoints = new Dictionary<FeatureTypes, int>();

        private static Team Instance = null;

        public override void _EnterTree()
        {
            if(Instance != null && IsInstanceValid(Instance))
            {
                Instance.QueueFree();
            }
            Instance = this;

            Points.Add(FeatureTypes.Hero, 0);
            Points.Add(FeatureTypes.Skill, 0);
            Points.Add(FeatureTypes.Trinket, 0);

            MaxPoints.Add(FeatureTypes.Hero, 12);
            MaxPoints.Add(FeatureTypes.Skill, 40);
            MaxPoints.Add(FeatureTypes.Trinket, 25);
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

        public static void RecalculatePoints()
        {
            if(Instance == null)
            {
                return;
            }

            Instance.Points[FeatureTypes.Hero] = 0;
            Instance.Points[FeatureTypes.Skill] = 0;
            Instance.Points[FeatureTypes.Trinket] = 0;

            foreach (Feature hero in GetHeroTypes())
            {
                if(hero == null)
                {
                    continue;
                }
                Instance.Points[FeatureTypes.Hero] += hero.Points;
            }
            foreach (Feature skill in GetSkills())
            {
                if(skill == null)
                {
                    continue;
                }
                Instance.Points[FeatureTypes.Skill] += skill.Points;
            }
            foreach (Feature trinket in GetTrinkets())
            {
                if(trinket == null)
                {
                    continue;
                }
                Instance.Points[FeatureTypes.Trinket] += trinket.Points;
            }

            Instance.EmitSignal("PointsRecalculated");
        }

        public static bool CanAfford(int points, FeatureTypes type)
        {
            //Check if the player has enough points to afford a feature with the given points and type.

            if(Instance == null)
            {
                return true;
            }

            return Instance.MaxPoints[type] - Instance.Points[type] >= points;
        }

        public static void ConnectSignal(string signal, Object subscriber, string func)
        {
            Instance?.Connect(signal, subscriber, func);
        }

        public static Dictionary<FeatureTypes, int> GetPoints()
        {
            return Instance?.Points;
        }

        public static Dictionary<FeatureTypes, int> GetMaxPoints()
        {
            return Instance?.MaxPoints;
        }
    }
}
