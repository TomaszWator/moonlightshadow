namespace MoonlightShadow.Models.ClassHelper
{
    public class Unit
    {
        public double Value { get; set; }
        
        public string UnitValue { get; set; }

        public override string ToString()
        {
            return Value + " " + UnitValue;
        }
    }
}