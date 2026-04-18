using SquareGen.Globals;
using System.Collections.Generic;
using static SquareGen.Globals.Feature;

namespace SquareGen.TeamFeatures
{
    public partial class HeroSelect : FeatureSelect
    {
        public override FeatureTypes FeatureType { get; } = FeatureTypes.Hero;

        protected override List<Feature> GetValidFeatures()
        {
            return Team.GetValidHeroes();
        }
    }
}
