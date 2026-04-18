using Godot;
using System;
using System.Collections.Generic;

using static SquareGen.Globals.Feature;

namespace SquareGen.Globals
{
    public partial class DataLoader : Node
    {
        private const string DATA_DIR = "res://Data/";

        private List<Feature> Heroes = new List<Feature>();
        private List<Feature> Skills = new List<Feature>();
        private List<Feature> Trinkets = new List<Feature>();

        private static DataLoader Instance = null;

        public override void _EnterTree()
        {
            Instance = this;
        }

        public override void _Ready()
        {
            LoadFeatures(FeatureTypes.Hero, "heroes");
            LoadFeatures(FeatureTypes.Skill, "skills");
            LoadFeatures(FeatureTypes.Trinket, "trinkets");
        }

        private void LoadFeatures(FeatureTypes type, string fileName)
        {
            File file = new File();
            file.Open(DATA_DIR + fileName + ".txt", File.ModeFlags.Read);
            string featureStrings = file.GetAsText();
            file.Close();

            foreach(string featureString in featureStrings.Split('\n'))
            {
                string[] featureParts = featureString.Split(',');

                List<Feature> list = null;
                switch (type)
                {
                    case FeatureTypes.Hero:
                        list = Heroes;
                        break;
                    case FeatureTypes.Skill:
                        list = Skills;
                        break;
                    default:
                        list = Trinkets;
                        break;
                }
                list.Add(new Feature(featureParts[0], Convert.ToInt32(featureParts[1]), featureParts[2], type));
            }
        }

        public static List<Feature> GetHeroes()
        {
            if(Instance != null)
            {
                List<Feature> list = new List<Feature>();
                foreach(Feature feature in Instance.Heroes)
                {
                    list.Add(feature);
                }
                return list;
            }

            return null;
        }

        public static List<Feature> GetSkills(string hero)
        {
            if (Instance != null)
            {
                List<Feature> list = new List<Feature>();
                foreach (Feature feature in Instance.Skills)
                {
                    if(feature.Hero == hero)
                    {
                        list.Add(feature);
                    }
                }
                return list;
            }

            return null;
        }

        public static List<Feature> GetTrinkets(string hero)
        {
            if (Instance != null)
            {
                List<Feature> list = new List<Feature>();
                foreach (Feature feature in Instance.Trinkets)
                {
                    if (feature.Hero == hero || feature.Hero == "generic")
                    {
                        list.Add(feature);
                    }
                }
                return list;
            }

            return null;
        }
    }
}
