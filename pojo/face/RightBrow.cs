using System;
using svg_memento.pojo.style;

namespace svg_memento.pojo.face
{
    [Serializable]
    public class RightBrow : Organ
    {
        public RightBrow()
        {
            this.Style = new RightBrowStyleA();
            this.OrganName = "Right Brow";
        }

        public override void SetStyleA()
        {
            this.SetStyle(new RightBrowStyleA());
        }

        public override void SetStyleB()
        {
            this.SetStyle(new RightBrowStyleB());
        }
    }
}