using System.Collections.Generic;

namespace svg_memento.pojo.memento
{
    /// <summary>
    /// The caretaker is responsible for the state management and has not access to the originator's state
    /// </summary>
    public class Caretaker
    {
        private Originator Originator;
        private Stack<IMemento> UNDO_STACK = new ();
        private Stack<IMemento> REDO_STACK = new ();

        public Caretaker(Originator originator)
        {
            this.Originator = originator;
        }

        /// <summary>
        /// Take a snapshot for the current state and store to UNDO_STACK
        /// </summary>
        public void Backup()
        {
            UNDO_STACK.Push(Originator.Save());
            REDO_STACK.Clear();
        }

        /// <summary>
        /// The undo functionality
        /// </summary>
        /// <returns></returns>
        public bool Undo()
        {
            if (UNDO_STACK.Count == 0)
            {
                return false;
            }
            var memento = UNDO_STACK.Pop();
            REDO_STACK.Push(memento);
            if (UNDO_STACK.Count == 0)
            {
                Originator.Restore();
                return true;
            }
            Originator.Restore(UNDO_STACK.Peek());
            return true;
        }
        
        /// <summary>
        /// The redo functionality
        /// </summary>
        /// <returns></returns>
        public bool Redo()
        {
            if (REDO_STACK.Count == 0)
            {
                return false;
            }
            var memento = REDO_STACK.Pop();
            UNDO_STACK.Push(memento);
            Originator.Restore(memento);
            return true;
        }

    }
}