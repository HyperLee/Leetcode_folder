using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace leetcode_234
{
    class Program
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
        /// leetcode 234
        /// https://leetcode.com/problems/palindrome-linked-list/
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            ListNode l1 = new ListNode(1);
            l1.next = new ListNode(2);
            l1.next.next = new ListNode(2);
            l1.next.next.next = new ListNode(1);
            //l1.next.next.next.next = new ListNode(9);
            //l1.next.next.next.next.next = new ListNode(9);
            //l1.next.next.next.next.next.next = new ListNode(9);

            //var res = IsPalindrome(l1);
            //while (res != null)
            //{
            //    Console.WriteLine("function1:" + res.val);
            //    //Console.WriteLine(res.val);
            //    res = res.next;
            //}

            Console.WriteLine(IsPalindrome2(l1));
            Console.ReadKey();

        }


         static ListNode realHead;
         static bool isCrossed = false;
        /// <summary>
        /// https://ithelp.ithome.com.tw/articles/10224510
        /// 
        /// 兩個指標 realHead 會 從頭往後 開始比較；head 會 從尾往前 開始比較
        /// head 從右往左
        /// realhead 從左往右
        /// 前後比對 直到交叉代表到中間位置, 即可停止
        /// 只要有遇到 readhead != head 代表 不一致
        /// 前後比 類似雙指針
        /// </summary>
        /// <param name="head"></param>
        /// <returns></returns>
        public static  bool IsPalindrome(ListNode head)
        {
            if (head == null)
                return true;

            if (realHead == null)
                realHead = head;

            bool result = true;

            if (head.next != null)
                result = result & IsPalindrome(head.next);

            if (isCrossed)
                return result;

            if (head == realHead || realHead.next == head)
                isCrossed = true;

            result = result & (head.val == realHead.val);
            realHead = realHead.next;

            return result;
        }

        /// <summary>
        /// https://leetcode.cn/problems/palindrome-linked-list/solution/hui-wen-lian-biao-by-leetcode-solution/
        /// 官方解法
        /// 方法一
        /// 将值复制到数组(List)中后用双指针法
        /// 這方法 比 遞迴 好懂 就雙指針 前後比對
        /// 不同就是錯誤
        /// 
        /// 一共为两个步骤：
        /// 1. 复制链表值到数组列表中。
        /// 2. 使用双指针法判断是否为回文。
        /// 
        /// 在编码的过程中，注意我们比较的是节点值的大小，而不是节点本身。正确的比较方式
        /// 是：node_1.val == node_2.val，而 node_1 == node_2 是错误的。
        /// </summary>
        /// <param name="head"></param>
        /// <returns></returns>
        public static bool IsPalindrome2(ListNode head)
        {
            List<int> vals = new List<int>();

            // 将链表的值复制到数组中
            ListNode currentNode = head;
            while (currentNode != null)
            {
                vals.Add(currentNode.val);
                currentNode = currentNode.next;
            }

            // 使用双指针判断是否回文
            int front = 0;
            int back = vals.Count - 1;
            while (front < back)
            {
                if (vals[back] != vals[front])
                {
                    return false;
                }
                front++;
                back--;
            }
            return true;
        }


    }
}
