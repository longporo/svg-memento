using System;
using svg_memento.pojo.style;

namespace svg_memento.pojo.face
{
    [Serializable]
    public class LeftEye : Organ
    {
        public LeftEye()
        {
            this.Style = new LeftEyeStyleA();
            this.OrganName = "Left Eye";
        }

        public override void SetStyleA()
        {
            this.SetStyle(new LeftEyeStyleA());
        }

        public override void SetStyleB()
        {
            this.SetStyle(new LeftEyeStyleB());
        }
    }
}