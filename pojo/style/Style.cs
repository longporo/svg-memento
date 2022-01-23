using System;

namespace svg_memento.pojo.style
{
    [Serializable]
    public abstract class Style
    {
        public string StyleName;
        public bool IsShown = false;
        public string Fill = "none";
        public string Stroke = "#000000";
        public string StrokeLinecap = "round";
        public string StrokeLinejoin = "round";
        public string StrokeMiterlimit = "10";
        public string StrokeWidth = "2";
        public string Path;
        
        public int TransX = 0;
        public int TransY = 0;

        public string GenSvgCode()
        {
            string visibility = "hidden";
            if (IsShown)
            {
                visibility = "visible";
            }
            string content = $@"<path visibility=""{visibility}"" fill=""{this.Fill}"" stroke=""{this.Stroke}"" stroke-linecap=""{this.StrokeLinecap}"" stroke-linejoin=""{this.StrokeLinejoin}"" stroke-miterlimit=""{this.StrokeMiterlimit}"" stroke-width=""{this.StrokeWidth}"" d=""{this.Path}""/>";
            if (this.TransY == 0 && this.TransX == 0)
            {
                return content;
            }
            string transHead = $@"<g transform=""translate({this.TransX},{this.TransY})"">";
            string transFoot = "</g>";
            return transHead + Environment.NewLine + content + Environment.NewLine + transFoot;
        }
    }
}