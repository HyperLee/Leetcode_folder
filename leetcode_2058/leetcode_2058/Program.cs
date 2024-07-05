namespace leetcode_2058
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
        /// 2058. Find the Minimum and Maximum Number of Nodes Between Critical Points
        /// https://leetcode.com/problems/find-the-minimum-and-maximum-number-of-nodes-between-critical-points/description/?envType=daily-question&envId=2024-07-05
        /// 2058. 找出临界点之间的最小和最大距离
        /// https://leetcode.cn/problems/find-the-minimum-and-maximum-number-of-nodes-between-critical-points/description/
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            ListNode list = new ListNode(5);
            list.next = new ListNode(3);
            list.next.next = new ListNode(1);
            list.next.next.next = new ListNode(2);
            list.next.next.next.next = new ListNode(5);
            list.next.next.next.next.next = new ListNode(1);
            list.next.next.next.next.next.next = new ListNode(2);
            //list.next.next.next.next.next.next.next = new ListNode(0);

            //Console.WriteLine(NodesBetweenCriticalPoints(list));

            var res = NodesBetweenCriticalPoints(list);
            Console.WriteLine("[最小, 最大]  距離");
            foreach (var node in res) 
            {
                Console.Write(node + ", ");
            }

            Console.ReadKey();
        }


        /// <summary>
        /// https://leetcode.cn/problems/find-the-minimum-and-maximum-number-of-nodes-between-critical-points/solutions/1077097/zhao-chu-lin-jie-dian-zhi-jian-de-zui-xi-b08v/
        /// https://leetcode.cn/problems/find-the-minimum-and-maximum-number-of-nodes-between-critical-points/solutions/1075991/go-mo-ni-bian-li-lian-biao-bian-li-lin-j-rx9s/
        /// https://leetcode.cn/problems/find-the-minimum-and-maximum-number-of-nodes-between-critical-points/solutions/2612349/2058-zhao-chu-lin-jie-dian-zhi-jian-de-z-i2az/
        /// 
        /// </summary>
        /// <param name="head"></param>
        /// <returns></returns>
        public static int[] NodesBetweenCriticalPoints(ListNode head)
        {
            // 儲存 最小/最大距離
            int minDis = int.MaxValue;
            int maxDis = 0;

            // 暫存, 分別為, 第一/當前/下一個 臨界點的index位置
            int firstindex = -1;
            int previndex = -1;
            int currindex = -1;

            int index = 1;
            // 第一個node不會是臨界點(最後一個node也不會是臨界點)
            // , 所以curr要從index = 1 開始往下找
            ListNode prev = head, curr = head.next;

            // curr不要是結尾 (頭尾不會有極大/極小值)
            while(curr.next != null)
            {
                ListNode next = curr.next;

                // 局部極大/極小點位置
                if((curr.val > prev.val && curr.val > next.val) || (curr.val < prev.val && curr.val < next.val))
                {
                    if(firstindex < 0)
                    {
                        // 第一個臨界點位置給予 first
                        firstindex = index;
                    }

                    // 視窗滑動 概念, 往前塞
                    previndex = currindex; ;
                    currindex = index;

                    // >= 0, 代表之前已經有寫入臨界點位置, 
                    // 有兩個點即可計算 最大/最小 距離
                    if (previndex >= 0)
                    {
                        // 最小距離代表兩個點之間相鄰, 當前位置 - 前一個位置
                        minDis = Math.Min(minDis, currindex - previndex);
                        // 最大距離代表最遠(頭尾), 當前位置 - 第一個位置
                        maxDis = Math.Max(maxDis, currindex - firstindex);
                    }

                }

                // 往右繼續走遍歷node
                prev = curr;
                curr = next;
                index++;
            }


            if (minDis <= maxDis)
            {
                // 有找到
                return new int[] { minDis, maxDis };
            }
            else
            {
                // 找不到
                return new int[] { -1, -1 };
            }
        }

    }
}
