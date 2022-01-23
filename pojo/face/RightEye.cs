using System;
using svg_memento.pojo.style;

namespace svg_memento.pojo.face
{
    [Serializable]
    public class RightEye : Organ
    {
        public RightEye()
        {
            this.Style = new RightEyeStyleA();
            this.OrganName = "Right Eye";
        }

        public override void SetStyleA()
        {
            this.SetStyle(new RightEyeStyleA());
        }

        public override void SetStyleB()
        {
            this.SetStyle(new RightEyeStyleB());
        }
    }
}