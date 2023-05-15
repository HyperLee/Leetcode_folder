using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace leetcode_1721
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
        /// leetcode 1721 Swapping Nodes in a Linked List
        /// https://leetcode.com/problems/swapping-nodes-in-a-linked-list/
        /// 交换链表中的节点
        /// https://leetcode.cn/problems/swapping-nodes-in-a-linked-list/
        /// 
        /// 從head開始, 把第K個跟倒數第K個節點.
        /// 做交換
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            ListNode li = new ListNode(1);
            li.next = new ListNode(2);
            li.next.next = new ListNode(3);
            li.next.next.next = new ListNode(4);
            li.next.next.next.next = new ListNode(5);

            //SwapNodes(li, 2);

            var res = SwapNodes(li, 2);
            while (res != null)
            {
                Console.WriteLine("list.value:" + res.val);
                //Console.WriteLine(res.val);
                res = res.next;
            }

            Console.ReadKey();

        }


        /// <summary>
        /// https://leetcode.cn/problems/swapping-nodes-in-a-linked-list/solution/1721-jiao-huan-lian-biao-zhong-de-jie-di-0pd9/
        /// (the list is 1-indexed).
        /// list索引從1開始
        /// </summary>
        /// <param name="head"></param>
        /// <param name="k"></param>
        /// <returns></returns>
        public static ListNode SwapNodes(ListNode head, int k)
        {
            int n = 0;
            ListNode temp = head;

            // 計算 head 總長度 n 
            while (temp != null)
            {
                n++;
                temp = temp.next;
            }

            ListNode node1 = head, node2 = head;

            // 找出第 k 個節點
            for (int i = 1; i < k; i++)
            {
                node1 = node1.next;
            }

            // 找出倒數第 k 個節點; n - k + 1
            for (int i = 1; i < n - k + 1; i++)
            {
                node2 = node2.next;
            }

            // 交換 兩節點的值
            int val1 = node1.val, val2 = node2.val;
            node1.val = val2;
            node2.val = val1;
            

            return head;

        }

    }
}
