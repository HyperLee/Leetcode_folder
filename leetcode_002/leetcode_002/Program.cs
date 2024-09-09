using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace leetcode_002
{
    class Program
    {
        public class ListNode
        {
            public int val;
            public ListNode next;
            public ListNode(int x) { val = x; }
        }

        /// <summary>
        /// 2. Add Two Numbers
        /// https://leetcode.com/problems/add-two-numbers/
        /// 2. 两数相加
        /// https://leetcode.cn/problems/add-two-numbers/description/
        /// 
        /// 題目會給兩個 linklist 將他們相加
        /// 每個 node 指儲存一位數字 (0 ~ 9)
        /// 逆向儲存(由右至左方向)
        /// 如果 sum 超過 10 往右 node 進位
        /// 
        /// 兩個方法大同小異
        /// 方法一比較好理解
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            ListNode l1 = new ListNode(2);
            l1.next = new ListNode(4);
            l1.next.next = new ListNode(3);
            //l1.next.next.next = new ListNode(9);
            //l1.next.next.next.next = new ListNode(9);
            //l1.next.next.next.next.next = new ListNode(9);
            //l1.next.next.next.next.next.next = new ListNode(9);

            ListNode l2 = new ListNode(5);
            l2.next = new ListNode(6);
            l2.next.next = new ListNode(4);
            //l2.next.next.next = new ListNode(9);

            var res = addTwoNumbers(l1, l2);
            while (res != null)
            {
                Console.WriteLine("function1:" + res.val);
                //Console.WriteLine(res.val);
                res = res.next;
            }
            Console.WriteLine("-----------------");
            var res3 = addTwoNumbers2(l1, l2);
            while (res3 != null)
            {
                Console.WriteLine("function2:" + res3.val);
                //Console.WriteLine(res.val);
                res3 = res3.next;
            }
            Console.WriteLine("-----------------end");

            Console.ReadKey();
        }


        /// <summary>
        /// ref:
        /// https://www.itread01.com/content/1545328944.html
        /// 
        /// 最後輸出是 head.next
        /// 因為 head 宣告時候是 0
        /// 從下一個 node 才是 相加之後的新 node
        /// 
        /// 先判斷是否進位
        /// 再來把 node value 加總
        /// 接續就把 node 指向下個 node
        /// 
        /// </summary>
        /// <param name="l1"></param>
        /// <param name="l2"></param>
        /// <returns></returns>
        public static ListNode addTwoNumbers(ListNode l1, ListNode l2)
        {
            // 暫存
            ListNode l3 = new ListNode(0);
            // 輸出答案
            ListNode head = l3;
            int sum = 0;
            while (l1 != null || l2 != null)
            {
                // 判斷是否進位
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

                // 儲存在 l3 中
                l3.next = new ListNode(sum % 10);
                l3 = l3.next;
            }

            // 判斷最後一項是否和大於 9，大於則需要再添加一個 1.
            if (sum > 9)
            {
                l3.next = new ListNode(1);
            }

            return head.next;
        }


        /// <summary>
        /// ref:
        /// https://blog.csdn.net/weixin_41969800/article/details/121118863
        /// 
        /// 最後輸出是 resultnode.next
        /// 因為 resultnode 宣告時候是 0
        /// 從下一個 node 才是 相加之後的新 node
        /// 
        /// 本方法是把 相加 先做
        /// 再來判斷 是否進位
        /// 之後再把 node 指向 下一個 node
        /// </summary>
        /// <param name="l1"></param>
        /// <param name="l2"></param>
        /// <returns></returns>
        public static ListNode addTwoNumbers2(ListNode l1, ListNode l2)
        {
            ListNode resultnode = new ListNode(0);
            ListNode curr = resultnode;

            int carry = 0;
            while (l1 != null || l2 != null)
            {
                int d1 = l1 == null ? 0 : l1.val;
                int d2 = l2 == null ? 0 : l2.val;

                int sum = d1 + d2 + carry;
                // 判斷是否進位;
                // carry = 1, 下一輪就要加上去
                carry = sum > 9 ? 1 : 0;

                curr.next = new ListNode(sum % 10);
                curr = curr.next;

                if (l1 != null)
                {
                    l1 = l1.next;
                }

                if (l2 != null)
                {
                    l2 = l2.next;
                }
            }

            // 判斷最後是否還是要進位
            if (carry == 1)
            {
                curr.next = new ListNode(1);
            }

            return resultnode.next;

        }


    }
}
