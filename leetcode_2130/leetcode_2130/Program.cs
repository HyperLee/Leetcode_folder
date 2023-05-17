using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace leetcode_2130
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
    /// 2130. Maximum Twin Sum of a Linked List
    /// https://leetcode.com/problems/maximum-twin-sum-of-a-linked-list/
    /// 2130. 链表最大孪生和
    /// https://leetcode.cn/problems/maximum-twin-sum-of-a-linked-list/
    /// </summary>
    internal class Program
    {
        static void Main(string[] args)
        {
            ListNode li = new ListNode(5);
            li.next = new ListNode(4);
            li.next.next = new ListNode(2);
            li.next.next.next = new ListNode(1);

            Console.WriteLine(PairSum4(li));
            Console.ReadKey();
        }


        /// <summary>
        /// https://leetcode.cn/problems/maximum-twin-sum-of-a-linked-list/solution/di-yi-fan-ying-jiu-shi-stack-by-aksgoal-vq4i/
        /// 
        /// </summary>
        /// <param name="head"></param>
        /// <returns></returns>
        public static int PairSum(ListNode head)
        {
            if (head == null) 
            {
                return -1;
            }

            Stack<ListNode> stack = new Stack<ListNode>();
            ListNode temp = head;
            ListNode cur = head;

            while (temp != null)
            {
                stack.Push(temp);
                temp = temp.next;
            }
            
            int count = stack.Count;
            int[] nums = new int[count];
            
            for (int i = 0; i < count && cur != null; i++)
            {
                nums[i] = cur.val + stack.Pop().val;
                cur = cur.next;
            }
            
            return nums.Max();

        }


        /// <summary>
        /// https://leetcode.cn/problems/maximum-twin-sum-of-a-linked-list/solution/zhe-ge-ti-mu-yong-lie-biao-bu-shi-ke-yi-rtj9o/
        /// 這個效率比較好
        /// </summary>
        /// <param name="head"></param>
        /// <returns></returns>
        public static int PairSum2(ListNode head)
        {
            List<int> list = new List<int>();
            while (head != null)
            {
                list.Add(head.val);
                head = head.next;
            }

            int max = list[0], temp = 0;
            
            for (int i = 0; i < list.Count / 2; i++)
            {
                temp = list[i] + list[list.Count - 1 - i];
                if (temp > max)
                {
                    max = temp;
                }
            }
            return max;
        }


        /// <summary>
        /// StringBuilder
        /// 看起來不太行
        /// </summary>
        /// <param name="head"></param>
        /// <returns></returns>
        public static int PairSum3(ListNode head)
        {
            StringBuilder sb = new StringBuilder();
            while(head != null)
            {
                sb.Append(head.val);
                head = head.next;
            }

            int temp = 0, max = 0;
            for(int i = 0; i < sb.Length / 2; i++)
            {
                temp = sb[i] + sb[sb.Length - 1 - i];
                if(temp > max)
                {
                    max = temp;
                }
            }
            return max;

        }


        /// <summary>
        /// 嘗試 雙指針
        /// </summary>
        /// <param name="head"></param>
        /// <returns></returns>
        public static int PairSum4(ListNode head)
        {
            // 先把 head塞到 list 之後要算出長度以及取資料
            List<int> list = new List<int> ();
            while (head != null)
            {
                list.Add(head.val);
                head = head.next;
            }

            // 取出 head 長度
            int len = list.ToArray().Length;
            int result = 0;

            for(int i = 0; i < len / 2; i++)
            {
                // 取前後twin 
                int temp = list[i] + list[len - i - 1];
                
                if(temp > result)
                {
                    result = temp;
                }
            }


            return result;

        }

    }
}
