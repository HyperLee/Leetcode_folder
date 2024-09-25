using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace leetcode_086
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
        /// 86. Partition List
        /// https://leetcode.com/problems/partition-list/
        /// 86. 分隔链表
        /// https://leetcode.cn/problems/partition-list/
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            ListNode input = new ListNode(1);
            input.next = new ListNode(4);
            input.next.next = new ListNode(3);
            input.next.next.next = new ListNode(2);
            input.next.next.next.next = new ListNode(5);
            input.next.next.next.next.next = new ListNode(2);

            int x = 3;

            //Console.WriteLine(Partition(input, x));
            // ListNode 輸出方式 範例
            var res = Partition(input, x);
            
            Console.WriteLine("ANS: " );
            while (res != null)
            {
                Console.Write(res.val + ", ");
                res = res.next;
            }

            Console.ReadKey();
        }


        /// <summary>
        /// https://leetcode.cn/problems/partition-list/solutions/543768/fen-ge-lian-biao-by-leetcode-solution-7ade/
        /// 
        /// 直观来说我们只需维护两个链表 small 和 large 即可，small 链表按顺序存储所有小于 x 的节点，
        /// large 链表按顺序存储所有大于等于 x 的节点。
        /// 遍历完原链表后，我们只要将 small 链表尾节点指向 large 链表的头节点即能完成对链表的分隔。
        /// 
        /// 1. 小於 x 的 node 放前面, 大於等於放後面
        /// 2. 輸入相對順序不能變
        /// 
        /// 遍历结束后，我们将 large 的 next 指针置空，这是因为当前节点复用的是原链表的节点，而其 next 指针可能指向一个小于 x 的节点，我们需要切断这个引用。
        /// 
        /// ListNode 開頭插入 0, 避免為空衍生問題
        /// 之後取資料直接取 next 即可
        /// </summary>
        /// <param name="head">輸入 linked list</param>
        /// <param name="x">特定值 x</param>
        /// <returns></returns>
        public static ListNode Partition(ListNode head, int x)
        {
            // 專門放 小於 x; 開頭 node val = 0
            ListNode small = new ListNode(0);
            ListNode smallHead = small;
            // 專門放 大於等於 x; 開頭 node val = 0
            ListNode large = new ListNode(0);
            ListNode largeHead = large;

            while(head != null)
            {
                if(head.val < x)
                {
                    // 小於 x
                    small.next = head;
                    small = small.next;
                }
                else
                {
                    // 大於等於 x
                    large.next = head;
                    large = large.next;
                }

                head = head.next;
            }

            // large 結束指向 null
            large.next = null;

            // small 結尾接續上 large
            // largeHead 開頭是 0, 所以要取 next
            small.next = largeHead.next;

            // smallHead 開頭是 0, 所以要取 next
            return smallHead.next;
        }


    }
}
