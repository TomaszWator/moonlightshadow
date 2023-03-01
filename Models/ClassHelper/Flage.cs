using System;

namespace MoonlightShadow.Models.ClassHelper
{
    public class Flage
    {
        public bool Value { get; set; }

        public override string ToString()
        {
            if(Value == true) return "Tak";
            else return "Nie";
        }
    }
}