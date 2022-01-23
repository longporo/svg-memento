using System;

namespace svg_memento.pojo.face
{
    [Serializable]
    public class Face
    {
        public Organ LeftBrow;
        public Organ RightBrow;
        public Organ LeftEye;
        public Organ RightEye;
        public Organ Mouth;

        public Face()
        {
            this.LeftBrow = new LeftBrow();
            this.RightBrow = new RightBrow();
            this.LeftEye = new LeftEye();
            this.RightEye = new RightEye();
            this.Mouth = new Mouth();
        }
    }
}