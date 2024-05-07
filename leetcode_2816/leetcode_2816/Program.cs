using System.Collections.Generic;

namespace leetcode_2816
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
        /// 2816. Double a Number Represented as a Linked List
        /// https://leetcode.com/problems/double-a-number-represented-as-a-linked-list/?envType=daily-question&envId=2024-05-07
        /// 2816. 翻倍以链表形式表示的数字
        /// https://leetcode.cn/problems/double-a-number-represented-as-a-linked-list/description/
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            ListNode head = new ListNode(5);
            head.next = new ListNode(1);
            head.next.next = new ListNode(1);

            var res = DoubleIt(head);
            while (res != null)
            {
                Console.WriteLine("Ans:" + res.val);
                res = res.next;
            }

            Console.ReadKey();
        }



        /// <summary>
        /// ref: 解法2
        /// https://leetcode.cn/problems/double-a-number-represented-as-a-linked-list/solutions/2580849/2816-fan-bei-yi-lian-biao-xing-shi-biao-eek5p/
        /// https://leetcode.cn/problems/double-a-number-represented-as-a-linked-list/solutions/2385962/o1-kong-jian-zuo-fa-kan-cheng-shi-head-y-1dco/
        /// 
        /// 上述兩鏈結為參考來源
        /// 務必要詳細多看幾次
        /// 非常精妙方法
        /// 原本是想說list取出之後,把資料反轉
        /// 再乘2即可
        /// 但是這樣效率有點差
        /// 
        /// -- 題外話 兩數相加可參考 leetcode 445
        /// 
        /// 題目要求要把 list node 數值 乘上兩倍 為回傳答案
        /// 因為是兩倍所以要考慮計算時候的 進位問題
        /// 我們是從左往右計算(高位 到 低位)
        /// 所以要判斷 進位問題
        /// 當 下一個位數 數值 > 4   (>= 5 會有進位問題要考慮)
        /// 當前位置 curr node val 要加一
        /// 因為是每個node都要兩倍, 所以當 curr node val >= 5 時候
        /// 上一個node 就要 + 1
        /// 因為進位
        /// 
        /// head node val >= 5 時候
        /// 開頭要再插入一個新個node , 數值給 0
        /// 用來處理head 進位問題
        /// </summary>
        /// <param name="head"></param>
        /// <returns></returns>
        public static ListNode DoubleIt(ListNode head)
        {
            // list head value >= 5, 要插入一個新節點 (進位問題)
            if(head.val >= 5)
            {
                head = new ListNode(0, head);
            }

            for(var cur = head; cur != null; cur = cur.next)
            {
                cur.val = cur.val * 2 % 10;

                // 如果下一個node val >= 5, 要把cur val +1 因為要考慮進位問題
                if(cur.next != null && cur.next.val >= 5)
                {
                    cur.val++;
                }
            }

            return head;
        }
    }
}
