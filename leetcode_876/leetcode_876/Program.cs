namespace leetcode_876
{
    internal class Program
    {
        /// <summary>
        /// 
        /// </summary>
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

            var res = MiddleNode(listNode);
            Console.Write("method1: ");
            while (res != null)
            {
                Console.Write(res.val + ", ");
                res = res.next;
            }

            Console.WriteLine("");
            Console.Write("method2: ");
            ListNode listNode2 = new ListNode(1);
            listNode2.next = new ListNode(2);
            listNode2.next.next = new ListNode(3);
            listNode2.next.next.next = new ListNode(4);

            var res2 = MiddleNode2(listNode2);
            while (res2 != null)
            {
                Console.Write(res2.val + ", ");
                res2 = res2.next;
            }
            Console.WriteLine("");

        }


        /// <summary>
        /// ref:
        /// https://leetcode.cn/problems/middle-of-the-linked-list/solution/lian-biao-de-zhong-jian-jie-dian-by-leetcode-solut/
        /// https://leetcode.cn/problems/middle-of-the-linked-list/solution/kuai-man-zhi-zhen-zhu-yao-zai-yu-diao-shi-by-liwei/
        /// 
        /// method2 快慢針
        /// slow 每次走一步
        /// fast 每次走兩步
        /// 這樣當 fast 走到結束時候
        /// slow 走到中間
        /// 
        /// while 停止條件判斷: 快針不能為空且能繼續走
        ///  要注意這邊不要寫錯, 快針不為空, 就代表能繼續走
        /// 
        /// 時間複雜度: O(N), N 是節點數量
        /// 空間複雜度: O(1), 常數空間變數 slow, fast
        /// </summary>
        /// <param name="head"></param>
        /// <returns></returns>
        public static ListNode MiddleNode(ListNode head)
        {
            ListNode slow = head, fast = head;

            // 快針不能為空且下一步能繼續走
            while (fast != null && fast.next != null)
            {
                // 每次走一步
                slow = slow.next;
                // 每次走兩步
                fast = fast.next.next;
            }

            return slow;
        }


        /// <summary>
        /// 單指針法:
        /// 
        /// 1. 第一次遍歷, 先統計總長度 n
        /// 2. 第二次遍歷, 再去運算 中間節點  n / 2 位置
        /// 2-1. ListNode index 從 0 開始, 所以走到 n / 2 剛好是中間位置
        /// 
        /// 時間複雜度: O(N), N 是節點數量
        /// 空間複雜度: O(1), 常數空間變數
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
            // cur 重置
            cur = head;
            // 取中間值位置出來
            while (k < n / 2)
            {
                k++;
                cur = cur.next;
            }
            return cur;
        }
    }
}
