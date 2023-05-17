using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace leetcode_024
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
        /// 24. Swap Nodes in Pairs
        /// https://leetcode.com/problems/swap-nodes-in-pairs/description/
        /// 24. 两两交换链表中的节点
        /// https://leetcode.cn/problems/swap-nodes-in-pairs/
        /// 
        /// list相鄰的node前後交換,
        /// 交換完之後head放在node.next
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            ListNode li = new ListNode(1);
            li.next = new ListNode(2);
            li.next.next = new ListNode(3); 
            li.next.next.next = new ListNode(4);
            //li.next.next.next.next = new ListNode(5);

            var res = SwapPairs(li);
            while (res != null)
            {
                Console.WriteLine("list.value:" + res.val);
                res = res.next;
            }

            Console.ReadKey();
        }


        /// <summary>
        /// 遞迴方法
        /// https://leetcode.cn/problems/swap-nodes-in-pairs/solution/liang-liang-jiao-huan-lian-biao-zhong-de-jie-di-91/
        /// 
        /// 相鄰兩個交換, 所以至少需要兩個
        /// 當為null or 只有一個node 便無法交換
        /// 奇數最後一個無法交換
        /// 偶數才能全部交換
        /// 
        /// 交換方法
        /// 原始head頭節點:  變成newhead的二節點
        /// 原始head二節點:  變成newhead的頭節點
        /// 
        /// 交換完之後 newhead.next = head
        /// 就能繼續交換之後的node
        /// </summary>
        /// <param name="head"></param>
        /// <returns></returns>
        public static ListNode SwapPairs(ListNode head)
        {

            if (head == null || head.next == null)
            {
                return head;
            }

            ListNode newHead = head.next;
            head.next = SwapPairs(newHead.next);
            newHead.next = head;

            return newHead;

        }


    }
}
