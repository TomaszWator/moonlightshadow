namespace MoonlightShadow.Models.ClassHelper
{
    public class Resolution
    {
        public Unit X { get; set; }
        
        public Unit Y { get; set; }

        public override string ToString()
        {
            return X + " x " + Y;
        }
    }
}