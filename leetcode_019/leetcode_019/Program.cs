using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace leetcode_019
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
        /// 19. Remove Nth Node From End of List
        /// https://leetcode.com/problems/remove-nth-node-from-end-of-list/description/?envType=daily-question&envId=2024-03-03
        /// 19. 删除链表的倒数第 N 个结点
        /// https://leetcode.cn/problems/remove-nth-node-from-end-of-list/description/
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            ListNode l1 = new ListNode(1);
            l1.next = new ListNode(2);
            l1.next.next = new ListNode(3);
            l1.next.next.next = new ListNode(4);
            l1.next.next.next.next = new ListNode(5);

            int n = 2;

            var res = RemoveNthFromEnd(l1, n);

            while (res != null)
            {
                Console.WriteLine("Ans:" + res.val);
                res = res.next;
            }

            Console.ReadKey();
        }


        /// <summary>
        /// 方法三：双指针
        /// https://leetcode.cn/problems/remove-nth-node-from-end-of-list/solutions/450350/shan-chu-lian-biao-de-dao-shu-di-nge-jie-dian-b-61/
        /// https://leetcode.cn/problems/remove-nth-node-from-end-of-list/solutions/1235509/19-shan-chu-lian-biao-de-dao-shu-di-n-ge-xvf6/
        /// 
        /// 新增兩個 指針
        /// 其中 first 比 second 多走 n 個 node
        /// 當 first 走到結尾( first.next 指向空)時候 
        /// 此時 secone 剛好是 倒數第 n 個位置
        /// 
        /// 答案輸出
        /// 只要把 second.next 指向 下下個即可
        /// 也就是掠過 下一個(要刪除的 node)
        /// 再把 second 串接回去 輸出的 listnode 即可
        /// 
        /// 新增 dummy 用意是 有可能 被刪除的 node 是 head
        /// </summary>
        /// <param name="head"></param>
        /// <param name="n"></param>
        /// <returns></returns>
        public static ListNode RemoveNthFromEnd(ListNode head, int n)
        {
            // 新 list, head 從第二個 node 開始串接; 第一個 node 插入 0
            ListNode dummy = new ListNode(0, head);
            // 前面走
            ListNode first = head;
            // 後面走
            ListNode second = dummy;

            // 先找出 first, second 兩者相差 n 個位置
            for(int i = 0; i < n; i++)
            {
                first = first.next;
            }

            // 一起往後走. 走到 first 停為止 定位出要刪除 node 位置
            while(first != null)
            {
                first = first.next;
                second = second.next;
            }

            // 要刪除之 node.
            // 指向下下一個 亦即 刪除第 n 個. 因為多走一個 next
            second.next = second.next.next;

            // 指向 dummy 第二個 node 開始; 第一個 node 是 0
            ListNode res = dummy.next;

            return res;

        }
    }
}
