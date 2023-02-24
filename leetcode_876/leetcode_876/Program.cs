using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace leetcode_876
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
        /// leetcode 876
        /// https://leetcode.com/problems/middle-of-the-linked-list/
        /// 
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
        }


        /// <summary>
        /// https://leetcode.cn/problems/middle-of-the-linked-list/solution/lian-biao-de-zhong-jian-jie-dian-by-leetcode-solut/
        /// method3 快慢針
        /// 
        /// https://leetcode.cn/problems/middle-of-the-linked-list/solution/kuai-man-zhi-zhen-zhu-yao-zai-yu-diao-shi-by-liwei/
        /// </summary>
        /// <param name="head"></param>
        /// <returns></returns>
        public ListNode MiddleNode(ListNode head)
        {
            ListNode slow = head, fast = head;
            while(fast != null && fast.next !=null)
            {
                slow = slow.next;
                fast = fast.next.next;
            }
            return slow;
        }

    }
}
