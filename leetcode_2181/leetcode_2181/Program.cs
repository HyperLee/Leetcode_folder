namespace leetcode_2181
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
        /// 2181. Merge Nodes in Between Zeros
        /// https://leetcode.com/problems/merge-nodes-in-between-zeros/description/?envType=daily-question&envId=2024-07-04
        /// 2181. 合并零之间的节点
        /// https://leetcode.cn/problems/merge-nodes-in-between-zeros/description/
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            ListNode list = new ListNode(0);
            list.next = new ListNode(3);
            list.next.next = new ListNode(1);
            list.next.next.next = new ListNode(0);
            list.next.next.next.next = new ListNode(4);
            list.next.next.next.next.next = new ListNode(5);
            list.next.next.next.next.next.next = new ListNode(2);
            list.next.next.next.next.next.next.next = new ListNode(0);

            var res = MergeNodes(list);
            while (res != null)
            {
                Console.WriteLine("Ans:" + res.val);
                res = res.next;
            }

            Console.ReadKey();
        }


        /// <summary>
        /// https://leetcode.cn/problems/merge-nodes-in-between-zeros/solutions/1301107/he-bing-ling-zhi-jian-de-jie-dian-by-lee-zo9b/
        /// https://leetcode.cn/problems/merge-nodes-in-between-zeros/solutions/2613668/2181-he-bing-ling-zhi-jian-de-jie-dian-b-fcg8/
        /// https://leetcode.cn/problems/merge-nodes-in-between-zeros/solutions/1278813/di-281chang-zhou-sai-hong-hai-you-xi-zhu-6qcc/
        /// 
        /// 要注意看題目說明
        /// 1. 頭尾皆為0
        /// 2. 不會連續兩個node.val 都為0
        /// 
        /// 遇到node.val 為0
        /// 下一個node開始就要累加 計算val
        /// 直到遇到下一個0為止
        /// 
        /// 遇到0就把node.next指向下個目標
        /// 
        /// 本解法為原地修改
        /// 沒有開額外的新的List來當結果用
        /// 
        /// </summary>
        /// <param name="head"></param>
        /// <returns></returns>
        public static ListNode MergeNodes(ListNode head)
        {
            if(head == null)
            {
                return null;
            }

            // 開頭node.val為0, 故直接指向下一個node
            // 用來遍歷head的當前節點位置
            ListNode temp = head.next;

            while (temp != null && temp.next != null)
            {
                if(temp.next.val != 0)
                {
                    // 累加 當下node.val與下個node.val
                    temp.val += temp.next.val;
                    // temp已經累加next.val, 所以直接指向下下個目標
                    temp.next = temp.next.next;
                }
                else
                {
                    // node.val為0, 就要跳過. 指向下下個node
                    // 題目說明有說, 不會連續兩個node都為0
                    temp.next = temp.next.next;
                    temp = temp.next;
                }

            }

            // 要指向下一個
            return head.next;
        }
    }
}
