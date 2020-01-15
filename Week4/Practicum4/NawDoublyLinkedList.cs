using Alg1.Practica.Utils;
using Alg1.Practica.Utils.Models;

namespace Alg1.Practica.Practicum4
{
    public class NawDoublyLinkedList
    {
        public DoubleLink First { get; set; }
        public DoubleLink Last { get; set; }

        public void InsertHead(NAW naw)
        {
            DoubleLink newItem = new DoubleLink { Naw = naw, Next = First, Previous = null };
            if (First == null)
                Last = newItem;
            else
                First.Previous = newItem;
            First = newItem;
        }

        public NAW ItemAtIndex(int index)
        {
            throw new System.NotImplementedException();
        }

        public DoubleLink SwapLinkWithNext(DoubleLink link)
        {
            if (link.Next == null)
                return null;

            DoubleLink baselink = link;
            DoubleLink nextBaselink = link.Next;
            DoubleLink leftBaselinkNeedsAdjustment = baselink.Previous;
            DoubleLink rightBaselinkNeedsAdjustment = nextBaselink.Next;

            if (First == baselink)
                First = nextBaselink;
            if (leftBaselinkNeedsAdjustment != null)
                leftBaselinkNeedsAdjustment.Next = nextBaselink;

            nextBaselink.Previous = leftBaselinkNeedsAdjustment;
            nextBaselink.Next = baselink;
            baselink.Previous = nextBaselink;
            baselink.Next = rightBaselinkNeedsAdjustment;

            if (rightBaselinkNeedsAdjustment != null)
                rightBaselinkNeedsAdjustment.Previous = baselink;

            if (Last == nextBaselink)
                Last = baselink;

            return nextBaselink;
        }

        public void BubbleSort()
        {
            bool isSorted = false;

            while (!isSorted)
            {
                isSorted = true;
                DoubleLink current = First;
                while (current != null && current.Next != null)
                {
                    if (current.Naw.CompareTo(current.Next.Naw) > 0)
                        SwapLinkWithNext(current);
                    isSorted = false;

                    current = current.Next;
                }
            }
        }

        public BigO OrderOfBubbleSort()
        {
            return BigO.N2;
        }
    }
}
