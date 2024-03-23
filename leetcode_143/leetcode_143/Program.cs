namespace leetcode_143
{
    internal class Program
    {
        public class ListNode
        {
            public int val;
            public ListNode next;
            public ListNode(int val = 0, ListNode next = null)
            {
                this.val = val;
                this.next = next;
            }
        }


        /// <summary>
        /// 143. Reorder List
        /// https://leetcode.com/problems/reorder-list/description/?envType=daily-question&envId=2024-03-23
        /// 143. 重排链表
        /// https://leetcode.cn/problems/reorder-list/description/
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            ListNode node = new ListNode(1);
            node.next = new ListNode(2);
            node.next.next = new ListNode(3);
            node.next.next.next = new ListNode(4);

            ReorderList(node);
            Console.ReadKey();
        }


        /// <summary>
        /// 解法一, 左右指針
        /// https://leetcode.cn/problems/reorder-list/solutions/452867/zhong-pai-lian-biao-by-leetcode-solution/
        /// https://leetcode.cn/problems/reorder-list/solutions/1394353/by-stormsunshine-4k3r/
        /// </summary>
        /// <param name="head"></param>
        public static void ReorderList(ListNode head)
        {
            IList<ListNode> nodes = new List<ListNode>();
            ListNode node = head;

            while (node != null) 
            {
                nodes.Add(node);
                node = node.next;
            }

            int left = 0;
            int right = nodes.Count - 1;

            while (left < right)
            {
                nodes[left].next = nodes[right];
                left++;

                if(left < right)
                {
                    nodes[right].next = nodes[left];
                    right--;
                }
            }

            nodes[left].next = null;

            /*
            var output = nodes;
            foreach(var value in output)
            {
                Console.Write(value.val + ", ");
            }
            */

        }


    }
}
