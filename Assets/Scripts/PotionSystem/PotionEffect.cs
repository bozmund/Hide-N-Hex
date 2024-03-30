namespace PotionSystem
{
    public class PotionEffect
    {
        public PotionEffect(string name, string description)
        {
            Name = name;
            Description = description;
        }

        public readonly string Name;
        public readonly string Description;
    }
}