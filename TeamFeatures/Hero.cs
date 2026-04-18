using Godot;
using SquareGen.Globals;
using System.Collections.Generic;

namespace SquareGen.TeamFeatures
{
    public partial class Hero : Node2D
    {
        private HeroSelect HeroSelect = null;
        private List<SkillSelect> SkillSelects = new List<SkillSelect>();
        private List<TrinketSelect> TrinketSelects = new List<TrinketSelect>();

        public override void _Ready()
        {
            foreach(Node node in GetChildren())
            {
                if(node is HeroSelect hero)
                {
                    HeroSelect = hero;
                }
                else if(node is SkillSelect skill)
                {
                    SkillSelects.Add(skill);
                }
                else if(node is TrinketSelect trinket)
                {
                    TrinketSelects.Add(trinket);
                }

                if(node is FeatureSelect featureSelect)
                {
                    featureSelect.Hero = this;
                    node.Connect("FeatureSelected", this, nameof(OnFeatureSelected));
                }
            }
        }

        public Feature GetHeroType()
        {
            if(HeroSelect == null)
            {
                return null;
            }
            return HeroSelect.Feature;
        }

        public List<Feature> GetSkills()
        {
            List<Feature> list = new List<Feature>();
            foreach(SkillSelect select in SkillSelects)
            {
                list.Add(select.Feature);
            }
            return list;
        }

        public List<Feature> GetTrinkets()
        {
            List<Feature> list = new List<Feature>();
            foreach (TrinketSelect select in TrinketSelects)
            {
                list.Add(select.Feature);
            }
            return list;
        }

        public List<Feature> GetValidTrinkets()
        {
            List<Feature> list = Team.GetValidTrinkets(GetHeroType());
            List<Feature> currentTrinkets = GetTrinkets();

            foreach(Feature trinket in currentTrinkets)
            {
                list.Remove(trinket);
            }
            return list;
        }

        public List<Feature> GetValidSkills()
        {
            List<Feature> list = DataLoader.GetSkills(GetHeroType());
            List<Feature> currentSkills = GetSkills();

            foreach (Feature skill in currentSkills)
            {
                list.Remove(skill);
            }
            return list;
        }

        private void OnFeatureSelected(FeatureSelect select)
        {
            if (select == HeroSelect)
            {
                foreach (SkillSelect skill in SkillSelects)
                {
                    skill.SetFeature(null, false);
                }
                foreach (TrinketSelect trinket in TrinketSelects)
                {
                    trinket.SetFeature(null, false);
                }

                //Abom is a special snowflake
                if(select.Feature.Name == "abomination")
                {
                    SkillSelects[0].SetFeature(DataLoader.GetSkill("one", "abomination"), false);
                    SkillSelects[1].SetFeature(DataLoader.GetSkill("two", "abomination"), false);
                    SkillSelects[2].SetFeature(DataLoader.GetSkill("three", "abomination"), false);
                    SkillSelects[3].SetFeature(DataLoader.GetSkill("four", "abomination"), false);
                }
            }
        }
    }
}
