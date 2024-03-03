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

        }


        /// <summary>
        /// 方法三：双指针
        /// https://leetcode.cn/problems/remove-nth-node-from-end-of-list/solutions/450350/shan-chu-lian-biao-de-dao-shu-di-nge-jie-dian-b-61/
        /// https://leetcode.cn/problems/remove-nth-node-from-end-of-list/solutions/1235509/19-shan-chu-lian-biao-de-dao-shu-di-n-ge-xvf6/
        /// 
        /// </summary>
        /// <param name="head"></param>
        /// <param name="n"></param>
        /// <returns></returns>
        public ListNode RemoveNthFromEnd(ListNode head, int n)
        {
            // 新 list, head從第二個node開始串接
            ListNode dummy = new ListNode(0, head);
            // 前面走
            ListNode first = head;
            // 後面走
            ListNode second = dummy;

            // first, second  node 兩者相差 n 個位置
            for(int i = 0; i < n; i++)
            {
                first = first.next;
            }

            // 一起往後走. 走到停為止 即是要刪除node前一個位置
            while(first != null)
            {
                first = first.next;
                second = second.next;
            }

            // 要刪除之node.
            // 走道下下一個 亦即 刪除第n個. 因為多走一個next
            second.next = second.next.next;

            // 指向 dummy第二個node開始
            ListNode res = dummy.next;

            return res;

        }
    }
}
