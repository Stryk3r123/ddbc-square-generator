using System.Collections.Generic;
using SquareGen.Globals;
using static SquareGen.Globals.Feature;

namespace SquareGen.TeamFeatures
{
    public partial class TrinketSelect : FeatureSelect
    {
        public override FeatureTypes FeatureType { get; } = FeatureTypes.Trinket;

        protected override List<Feature> GetValidFeatures()
        {
            if (Hero == null)
            {
                return new List<Feature>();
            }

            return Hero.GetValidTrinkets();
        }
    }
}
