using SquareGen.Globals;
using System.Collections.Generic;
using static SquareGen.Globals.Feature;

namespace SquareGen.TeamFeatures
{
    public partial class SkillSelect : FeatureSelect
    {
        public override FeatureTypes FeatureType { get; } = FeatureTypes.Skill;

        protected override List<Feature> GetValidFeatures()
        {
            if(Hero == null)
            {
                return new List<Feature>();
            }

            return Hero.GetValidSkills();
        }
    }
}
