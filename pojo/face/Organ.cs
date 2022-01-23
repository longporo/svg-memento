using System;
using svg_memento.pojo.style;

namespace svg_memento.pojo.face
{
    [Serializable]
    public abstract class Organ
    {
        public string OrganName;
        
        public Style Style;
        
        public abstract void SetStyleA();
        
        public abstract void SetStyleB();

        public void SetStyle(Style newStyle)
        {
            // bakup previous show state
            newStyle.IsShown = this.Style.IsShown;
            this.Style = newStyle;
        }

        public void Reset()
        {
            this.SetStyleA();
        }

        public void MoveUp(int value)
        {
            this.Style.TransY = 0 - value;
        }
        public void MoveDown(int value)
        {
            this.Style.TransY = value;
        }

        public void MoveLeft(int value)
        {
            this.Style.TransX = 0 - value;
        }

        public void MoveRight(int value)
        {
            this.Style.TransX = value;
        }
    }
}