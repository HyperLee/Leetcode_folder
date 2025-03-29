namespace leetcode_217
{
    internal class Program
    {
        /// <summary>
        /// 217. Contains Duplicate
        /// https://leetcode.com/problems/contains-duplicate/
        /// 217. 存在重复元素
        /// https://leetcode.cn/problems/contains-duplicate/
        /// 
        /// 給你一個整數陣列nums。如果任一值在數組中出現至少兩次，則返回true；如果數組中每個元素互不相同，則返回false。
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            // 測試案例1: 有重複元素
            int[] nums1 = { 1, 2, 3, 1 };
            Console.WriteLine("測試案例1 (有重複元素):");
            Console.WriteLine($"Input: [{string.Join(", ", nums1)}]");
            Console.WriteLine("method1: " + ContainsDuplicate(nums1));
            Console.WriteLine("method2: " + ContainsDuplicate2(nums1));
            Console.WriteLine();

            // 測試案例2: 沒有重複元素
            int[] nums2 = { 1, 2, 3, 4 };
            Console.WriteLine("測試案例2 (沒有重複元素):");
            Console.WriteLine($"Input: [{string.Join(", ", nums2)}]");
            Console.WriteLine("method1: " + ContainsDuplicate(nums2));
            Console.WriteLine("method2: " + ContainsDuplicate2(nums2));
            Console.WriteLine();

            // 測試案例3: 所有元素都重複
            int[] nums3 = { 1, 1, 1, 1 };
            Console.WriteLine("測試案例3 (所有元素都重複):");
            Console.WriteLine($"Input: [{string.Join(", ", nums3)}]");
            Console.WriteLine("method1: " + ContainsDuplicate(nums3));
            Console.WriteLine("method2: " + ContainsDuplicate2(nums3));
            Console.WriteLine();

            // 測試案例4: 空陣列
            int[] nums4 = { };
            Console.WriteLine("測試案例4 (空陣列):");
            Console.WriteLine($"Input: [{string.Join(", ", nums4)}]");
            Console.WriteLine("method1: " + ContainsDuplicate(nums4));
            Console.WriteLine("method2: " + ContainsDuplicate2(nums4));
            Console.WriteLine();

            // 測試案例5: 單一元素
            int[] nums5 = { 1 };
            Console.WriteLine("測試案例5 (單一元素):");
            Console.WriteLine($"Input: [{string.Join(", ", nums5)}]");
            Console.WriteLine("method1: " + ContainsDuplicate(nums5));
            Console.WriteLine("method2: " + ContainsDuplicate2(nums5));
        }


        /// <summary>
        /// 陣列相鄰位置比對
        /// 
        /// 1. 陣列先排序，排序後相同數字會在相鄰位置
        /// 2. 因為已經排序過，所以找出相鄰 index 是不是相同數字即可
        /// 3. 因題目說至少連續兩次(含)，所以只需要比對 i 與 i + 1
        /// 
        /// 時間複雜度: O(NlogN)，其中 N 陣列長度。需要對陣列排序。
        /// 空間複雜度: O(logN)，其中 N 陣列長度。
        /// </summary>
        /// <param name="nums"></param>
        /// <returns></returns>
        public static bool ContainsDuplicate(int[] nums)
        {
            Array.Sort(nums);
            // 迴圈條件要注意, nums.Length - 1
            // 不要造成溢位問題
            for (int i = 0; i < nums.Length - 1; i++)
            {
                if (nums[i] == nums[i + 1])
                {
                    return true;
                }
            }

            return false;
        }


        /// <summary>
        /// 使用 Dictionary 統計每個輸入的數字出現的次數(頻率)
        /// 只要遇到次數(頻率)為 2
        /// 就停止運算
        /// 直接回傳 true
        /// 節省時間
        /// 
        /// 時間複雜度: O(N)，其中 N 陣列長度。
        /// 空間複雜度: O(N)，其中 N 陣列長度。
        /// </summary>
        /// <param name="nums"></param>
        /// <returns></returns>

        public static bool ContainsDuplicate2(int[] nums)
        {
            Dictionary<int, int> dic = new Dictionary<int, int>();

            foreach(int num in nums)
            {
                if (dic.ContainsKey(num))
                {
                    // 次數(頻率)為 2, 即可停止. 回傳答案
                    // 不需要持續累加
                    return true;
                }
                else
                {
                    //dic.Add(num, 1);
                    dic[num] = 1;  // 改用索引器寫法
                }
            }

            return false;
        }
    }
}
