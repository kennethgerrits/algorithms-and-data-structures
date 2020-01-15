using System;
using System.Runtime.Remoting.Messaging;
using Alg1.Practica.Practicum1;
using Alg1.Practica.Utils.Models;

namespace Alg1.Practica.Practicum4
{
    public class UndoableNawArray : NawArrayOrdered
    {

        public UndoLink First { get; private set; }
        public UndoLink Current { get; private set; }

        public UndoableNawArray(int size)
            : base(size)
        {
            First = null;
            Current = null;
        }

        public override void Add(NAW item)
        {
            base.Add(item);
            AddOperation(Operation.Add, item);
        }

        private bool DoRemove(NAW naw)
        {
            var index = Find(naw);
            if (index < 0)
                return false;
            RemoveAtIndex(index);
            return true;
        }

        public override bool Remove(NAW item)
        {
            if (!DoRemove(item))
                return false;
            AddOperation(Operation.Remove, item);
            return true;
        }

        public void Undo()
        {
            if (Current == null)
            {
                return;
            }
            else if (Current.Previous == null)
            {
                ReverseOperation(Current);
                Current = null;
                First = null;
            }
            else
            {
                ReverseOperation(Current);
                Current = Current.Previous;
            }

        }

        public void Redo()
        {
            switch (Current)
            {
                case null when First == null:
                    return;
                case null when First != null:
                    Current = First;
                    break;
                default:
                    {
                        if (Current.Next == null)
                        {
                            return;
                        }
                        Current = Current.Next;
                        break;
                    }
            }
            ApplyOperation(Current);
        }

        private void AddOperation(Operation operation, NAW naw)
        {
            UndoLink newUndo = new UndoLink { Naw = naw, Operation = operation };
            if (Current != null)
            {
                Current.Next = newUndo;
                newUndo.Previous = Current;
            }

            if (First == null)
                First = newUndo;

            Current = newUndo;
        }

        private void ApplyOperation(UndoLink link)
        {
            switch (link.Operation)
            {
                case Operation.Add:
                    base.Add(link.Naw);
                    break;

                case Operation.Remove:
                    DoRemove(link.Naw);
                    break;
            }
        }

        private void ReverseOperation(UndoLink link)
        {
            switch (link.Operation)
            {
                case Operation.Add:
                    DoRemove(link.Naw);
                    break;

                case Operation.Remove:
                    base.Add(link.Naw);
                    break;
            }
        }
    }
}
