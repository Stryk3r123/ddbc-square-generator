namespace SquareGen.Globals
{
    public readonly struct Feature
    {
        public readonly string Name;
        public readonly int Points;
        public readonly string Hero;

        public Feature(string name, int points, string hero)
        {
            Name = name;
            Points = points;
            Hero = hero;
        }
    }
}
