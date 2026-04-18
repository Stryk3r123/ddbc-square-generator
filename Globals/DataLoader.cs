using Godot;
using System;
using System.Collections.Generic;

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
            LoadFeatures(Heroes, "heroes.csv");
            LoadFeatures(Skills, "skills.csv");
            LoadFeatures(Trinkets, "trinkets.csv");
        }

        private void LoadFeatures(List<Feature> list, string file)
        {
            string featureStrings = ResourceLoader.Load<string>(DATA_DIR + file);
            foreach(string featureString in featureStrings.Split('\n'))
            {
                string[] featureParts = featureString.Split(',');
                list.Add(new Feature(featureParts[0], Convert.ToInt32(featureParts[1]), featureParts[2]));
            }
        }
    }
}
