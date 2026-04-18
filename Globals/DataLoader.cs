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
            LoadFeatures(FeatureTypes.Hero, "heroes.csv");
            LoadFeatures(FeatureTypes.Skill, "skills.csv");
            LoadFeatures(FeatureTypes.Trinket, "trinkets.csv");
        }

        private void LoadFeatures(FeatureTypes type, string file)
        {
            string featureStrings = ResourceLoader.Load<string>(DATA_DIR + file);
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
    }
}
