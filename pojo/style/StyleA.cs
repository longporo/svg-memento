using System;

namespace svg_memento.pojo.style
{
    [Serializable]
    public abstract class StyleA : Style
    {
        protected StyleA()
        {
            StyleName = "StyleA";
        }
    }
}