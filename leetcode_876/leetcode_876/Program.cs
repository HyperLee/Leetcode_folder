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
        /// 876. Middle of the Linked List
        /// https://leetcode.com/problems/middle-of-the-linked-list/
        /// 876. 链表的中间结点
        /// https://leetcode.cn/problems/middle-of-the-linked-list/description/
        /// 
        /// 輸出中間節點往後的node資料
        /// 如果中間節點有兩個 那就輸出後面那一個開始
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            ListNode listNode = new ListNode(1);
            listNode.next = new ListNode(2);
            listNode.next.next = new ListNode(3);
            listNode.next.next.next = new ListNode(4);

            //Console.WriteLine(MiddleNode(listNode));
            var res = MiddleNode2(listNode);
            while (res != null)
            {
                Console.WriteLine("Ans:" + res.val);
                res = res.next;
            }
            Console.ReadKey();
        }


        /// <summary>
        /// https://leetcode.cn/problems/middle-of-the-linked-list/solution/lian-biao-de-zhong-jian-jie-dian-by-leetcode-solut/
        /// method3 快慢針
        /// slow每次走一步
        /// fast每次走兩步
        /// 這樣當fast走到結束時候
        /// slow走道中間
        /// 
        /// https://leetcode.cn/problems/middle-of-the-linked-list/solution/kuai-man-zhi-zhen-zhu-yao-zai-yu-diao-shi-by-liwei/
        /// </summary>
        /// <param name="head"></param>
        /// <returns></returns>
        public static ListNode MiddleNode(ListNode head)
        {
            ListNode slow = head, fast = head;

            // 快針不能為空且能繼續走
            while(fast != null && fast.next !=null)
            {
                // 每次走一步
                slow = slow.next;
                // 每次走兩步
                fast = fast.next.next;
            }

            return slow;
        }


        /// <summary>
        /// 先統計總長度 n
        /// 再去運算 中間節點  n / 2 位置
        /// 
        /// </summary>
        /// <param name="head"></param>
        /// <returns></returns>
        public static ListNode MiddleNode2(ListNode head)
        {
            int n = 0;
            ListNode cur = head;
            // 計算總長度
            while (cur != null)
            {
                n++;
                cur = cur.next;
            }

            int k = 0;
            cur = head;
            // 取 中間值位置出來
            while (k < n / 2)
            {
                k++;
                cur = cur.next;
            }
            return cur;
        }

    }
}
