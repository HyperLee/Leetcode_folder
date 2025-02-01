namespace leetcode_238
{
    internal class Program
    {
        /// <summary>
        /// 238. Product of Array Except Self
        /// https://leetcode.com/problems/product-of-array-except-self/description/
        /// 
        /// 238. 除自身以外数组的乘积
        /// https://leetcode.cn/problems/product-of-array-except-self/description/
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            int[] nums = { 1, 2, 3, 4 };
            var res = ProductExceptSelf(nums);
            Console.Write("res: ");
            foreach (var item in res)
            {
                System.Console.Write(item + ", ");
            }

            Console.WriteLine();
        }


        /// <summary>
        /// 238. 除自身以外数组的乘积
        /// 目標是對於一個整數陣列 nums，返回一個陣列 res，其中 res[i] 是 nums 陣列中除了 nums[i] 之外的所有元素的乘積。
        /// 這段程式碼的解法不使用除法，並且時間複雜度為 O(n)。
        /// 
        /// 計算左側乘積
        /// 初始化 res[0] 為 1，因為 res[0] 只包含右側的乘積。
        /// 使用一個迴圈從左到右遍歷 nums 陣列，計算每個元素左側的乘積並存儲在 anresswer 陣列中。
        /// res[i] = nums[i - 1] * res[i - 1] 表示 res[i] 是 nums[i-1] 和 res[i-1] 的乘積。
        /// 
        /// 計算右側乘積並更新結果
        /// 初始化變數 R 為 1，表示右側乘積的初始值。
        /// 使用一個迴圈從右到左遍歷 nums 陣列，計算每個元素右側的乘積並更新 res 陣列。
        /// res[i] = res[i] * R 表示將當前的 res[i] 乘以右側的乘積 R。
        /// 更新 R 為 R * nums[i]，以便在下一次迴圈中使用。
        /// </summary>
        /// <param name="nums"></param>
        /// <returns></returns>
        public static int[] ProductExceptSelf(int[] nums)
        {
            // 数组长度
            int length = nums.Length;
            // 初始化数组 res
            int[] res = new int[length];

            // res[i] 表示索引 i 左侧所有元素的乘积
            // 因为索引为 0 的元素左侧没有元素，所以 res[0] = 1
            res[0] = 1;
            for(int i = 1; i < length; i++)
            {
                // 計算左側乘積
                res[i] = nums[i - 1] * res[i - 1];
            }

            // R 为右侧所有元素的乘积
            // 刚开始右边没有元素，所以 R = 1
            int R = 1;
            for (int i = length - 1; i >= 0; i--)
            {
                // 計算右側乘積並更新結果
                // R 为索引 i 右侧所有元素的乘积
                // res[i] 为索引 i 左侧所有元素的乘积
                // 两者相乘即为索引 i 处的乘积
                // 更新 res[i] 的值，然后 R 更新为索引 i 右侧所有元素的乘积
                res[i] = res[i] * R;
                // R 更新为索引 i 右侧所有元素的乘积
                R *= nums[i];
            }

            return res;
        }
    }
}
