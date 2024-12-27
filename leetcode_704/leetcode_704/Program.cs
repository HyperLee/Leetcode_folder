namespace leetcode_704
{
    internal class Program
    {
        /// <summary>
        /// 704. Binary Search
        /// https://leetcode.com/problems/binary-search/description/
        /// 
        /// 704. 二分查找
        /// https://leetcode.cn/problems/binary-search/description/
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            int[] input = { -1, 0, 3, 5, 9, 12 };
            int target = 2;
            Console.WriteLine("res: " + Search(input, target));
        }


        /// <summary>
        /// 二分法查找
        /// 
        /// 1. 每次查找時從陣列的中間 element 開始, 如果中間 element 正好是要查找的 element, 則搜尋結束
        /// 2. 如果某一特定 element 大於或者小於中間 element, 則陣列大於或小於中間 element 的那一半中查找,
        /// 而且跟開始一樣從中間 element 開始比較
        /// 3. 如果在某一步驟陣列為空, 則代表找不到
        /// 
        /// 此題目為標準 二分法練習題目
        /// </summary>
        /// <param name="nums"></param>
        /// <param name="target"></param>
        /// <returns></returns>
        public static int Search(int[] nums, int target)
        {
            // 左邊界
            int left = 0;
            // 右邊界
            int right = nums.Length - 1;

            while (left <= right)
            {
                // 中間值; 此寫法是避免溢位
                int middle = left + (right - left) / 2;

                if (nums[middle] > target)
                {
                    // 比目標大右邊界左移(縮小)
                    right = middle - 1;
                }
                else if (nums[middle] < target)
                {
                    // 比目標小左邊界右移(放大)
                    left = middle + 1;
                }
                else
                {
                    // 找到目標
                    return middle;
                }
            }

            // 找不到目標
            return -1;
        }
    }
}
