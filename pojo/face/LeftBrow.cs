using System;
using svg_memento.pojo.style;

namespace svg_memento.pojo.face
{
    [Serializable]
    public class LeftBrow : Organ
    {
        public LeftBrow()
        {
            this.Style = new LeftBrowStyleA();
            this.OrganName = "Left Brow";
        }

        public override void SetStyleA()
        {
            this.SetStyle(new LeftBrowStyleA());
        }

        public override void SetStyleB()
        {
            this.SetStyle(new LeftBrowStyleB());
        }
    }
}