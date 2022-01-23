using svg_memento.pojo.face;

namespace svg_memento.pojo.memento
{
    /// <summary>
    /// The Memento interface provides some methods to get the data of state and does not expose the Originator's state
    /// </summary>
    public interface IMemento
    {
        Face GetState();
    }
}