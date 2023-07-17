using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace leetcode_445
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
        /// 445. Add Two Numbers II
        /// https://leetcode.com/problems/add-two-numbers-ii/description/
        /// 445. 两数相加 II
        /// https://leetcode.cn/problems/add-two-numbers-ii/
        /// 
        /// leetcode 02 addTwoNumbers 進階題目
        /// 本題目差異為 l1, l2的長度不同
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            ListNode l1 = new ListNode(7);
            l1.next = new ListNode(2);
            l1.next.next = new ListNode(4);
            l1.next.next.next = new ListNode(3);

            ListNode l2 = new ListNode(5);
            l2.next = new ListNode(6);
            l2.next.next = new ListNode(4);

            Console.WriteLine(AddTwoNumbers2(l1, l2));
            //AddTwoNumbers2(l1, l2);

            Console.ReadKey();
        }


        /// <summary>
        /// leetcode - 02 解法
        /// 此方法在 l1, l2 長度不同時候
        /// 會加錯位數
        /// 需要克服 兩邊長度不同,導致累加不同位數問題
        /// </summary>
        /// <param name="l1"></param>
        /// <param name="l2"></param>
        /// <returns></returns>
        public static ListNode AddTwoNumbers(ListNode l1, ListNode l2)
        {
            ListNode l3 = new ListNode(0);
            ListNode head = l3;
            int sum = 0;
            while (l1 != null || l2 != null)
            {
                sum = sum > 9 ? 1 : 0;
                if (l1 != null)
                {
                    sum += l1.val;
                    l1 = l1.next;
                }

                if (l2 != null)
                {
                    sum += l2.val;
                    l2 = l2.next;
                }

                l3.next = new ListNode(sum % 10);
                l3 = l3.next;

            }

            if (sum > 9)
            {
                l3.next = new ListNode(1);
            }

            return head.next;

        }


        /// <summary>
        /// https://leetcode.cn/problems/add-two-numbers-ii/solution/liang-shu-xiang-jia-ii-by-leetcode-solution/
        /// 方法2:
        /// 使用stack 導致翻轉(反轉) 順序
        /// 在相加
        /// 
        /// 方法3:
        /// 似乎應該可以有一種做法
        /// 轉成數列
        /// 在相加 應該也可以
        /// 
        /// </summary>
        /// <param name="l1"></param>
        /// <param name="l2"></param>
        /// <returns></returns>

        public static ListNode AddTwoNumbers2(ListNode l1, ListNode l2)
        {
            Stack<int> stack1 = new Stack<int>();
            Stack<int> stack2 = new Stack<int>();

            while (l1 != null)
            {
                stack1.Push(l1.val);
                l1 = l1.next;
            }

            while (l2 != null)
            {
                stack2.Push(l2.val);
                l2 = l2.next;
            }

            int carry = 0;
            ListNode ans = null;

            while (stack1.Count > 0 || stack2.Count > 0 || carry != 0)
            {
                int a = stack1.Count == 0 ? 0 : stack1.Pop();
                int b = stack2.Count == 0 ? 0 : stack2.Pop();
                int cur = a + b + carry;
                // 檢查進位
                carry = cur / 10;
                // 塞入位數位置
                cur %= 10;

                // 翻轉順序
                ListNode curnode = new ListNode(cur);
                curnode.next = ans;
                ans = curnode;
            }

            return ans;
        }

    }
}
