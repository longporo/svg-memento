using System;

namespace svg_memento.pojo.style
{
    [Serializable]
    public abstract class StyleB : Style
    {
        protected StyleB()
        {
            StyleName = "StyleB";
        }
    }
}