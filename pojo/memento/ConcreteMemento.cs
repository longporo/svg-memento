using svg_memento.pojo.face;
using svg_memento.utils;

namespace svg_memento.pojo.memento
{
    /// <summary>
    /// The ConcreteMemento stores the snapshot of the Originator's state
    /// </summary>
    public class ConcreteMemento : IMemento
    {
        private Face State;

        public ConcreteMemento(Face state)
        {
            this.State = ObjectUtils.DeepClone(state);
        }

        public Face GetState()
        {
            return this.State;
        }
    }
}