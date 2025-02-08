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
        /// 
        /// 解題方式 很特殊
        /// 左右分別計算乘積
        /// 要注意的是，這個解法不使用除法，並且時間複雜度為 O(n)。
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
            // 陣列長度
            int length = nums.Length;
            // 初始化陣列 res
            int[] res = new int[length];

            // res[i] 表示 index i 左側所有元素的乘積
            // 因為 index 為 0 的元素左側沒有元素, 所以 res[0] = 1
            res[0] = 1;
            //由左至右計算左側乘積
            for (int i = 1; i < length; i++)
            {
                // 計算左側乘積
                res[i] = nums[i - 1] * res[i - 1];
            }

            // R 為右側所有元素的乘積
            // 剛開始右邊沒有元素, 所以 R = 1
            int R = 1;
            // 由右至左計算右側乘積
            for (int i = length - 1; i >= 0; i--)
            {
                // 計算右側乘積並更新結果
                // R 為 index i 右側所有元素的乘積
                // res[i] 為 index i 左側所有元素的乘積
                // 兩者相乘即為 index i 處的乘積
                // 更新 res[i] 的值，然後 R 更新為 index i 右側的所有元素的乘積
                res[i] = res[i] * R;
                // R 更新為 index i 右側的所有元素的乘積
                R *= nums[i];
            }

            return res;
        }
    }
}
