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
        /// 繁體中文題目說明:
        /// 給定一個整數陣列 nums，請回傳一個陣列 answer，其中 answer[i] 等於 nums 中除 nums[i] 之外所有元素的乘積。
        /// 要求算法在 O(n) 時間複雜度內完成，且不得使用除法運算。此題保證任一前綴或後綴的乘積可放入 32-bit 整數中。
        /// 
        /// 解題方式 很特殊
        /// 左右分別計算乘積
        /// 要注意的是，這個解法不使用除法，並且時間複雜度為 O(n)。
        /// 
        /// 如果可以使用除法，就可以把全部數字相乘，然後除以 index i 的數字，就可以得到 index i 的乘積
        /// 但是如果某個 index 數值為 0 就會導致錯誤。
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            RunSamples();
        }

        /// <summary>
        /// 執行五組固定測試資料，分別驗證最佳化左右乘積與前後綴陣列兩種解法。
        /// 每種解法都使用獨立的輸入副本，並將實際結果與預期陣列比對後輸出 PASS 或 FAIL。
        /// 此方法不接收輸入，會在主控台列出每組結果及通過項目總數。
        /// </summary>
        private static void RunSamples()
        {
            (int[] Input, int[] Expected)[] testCases =
            [
                ([1, 2, 3, 4], [24, 12, 8, 6]),
                ([-1, 1, 0, -3, 3], [0, 0, 9, 0, 0]),
                ([0, 2, 0, 4], [0, 0, 0, 0]),
                ([-2, -3, -4], [12, 8, 6]),
                ([5, -2], [-2, 5])
            ];

            int passedChecks = 0;
            int totalChecks = testCases.Length * 2;

            for (int i = 0; i < testCases.Length; i++)
            {
                int[] input = testCases[i].Input;
                int[] expected = testCases[i].Expected;
                int[] optimizedResult = ProductExceptSelf([.. input]);
                int[] arrayResult = ProductExceptSelfWithArrays([.. input]);
                bool optimizedPassed = optimizedResult.SequenceEqual(expected);
                bool arrayPassed = arrayResult.SequenceEqual(expected);

                passedChecks += optimizedPassed ? 1 : 0;
                passedChecks += arrayPassed ? 1 : 0;

                Console.WriteLine($"案例 {i + 1}");
                Console.WriteLine($"輸入：[{string.Join(", ", input)}]");
                Console.WriteLine($"預期：[{string.Join(", ", expected)}]");
                Console.WriteLine(
                    $"解法一（最佳化左右乘積）：[{string.Join(", ", optimizedResult)}] => {(optimizedPassed ? "PASS" : "FAIL")}");
                Console.WriteLine(
                    $"解法二（前後綴陣列）：[{string.Join(", ", arrayResult)}] => {(arrayPassed ? "PASS" : "FAIL")}");
                Console.WriteLine();
            }

            Console.WriteLine($"總結：{passedChecks}/{totalChecks} 項驗證通過");
        }


        /// <summary>
        /// 計算陣列中每個位置「除自身以外」所有元素的乘積。
        /// 先將每個位置左側的乘積寫入結果陣列，再由右向左用單一變數累積右側乘積，
        /// 因此能在 O(n) 時間內完成，且不計回傳陣列時只使用 O(1) 額外空間。
        /// 輸入長度須至少為 2，且任一前綴、後綴與答案乘積皆須在 32-bit 整數範圍內。
        /// </summary>
        /// <param name="nums">要計算的整數陣列；方法不會修改此陣列。</param>
        /// <returns>新陣列，其中每個位置為原陣列除該位置以外所有元素的乘積。</returns>
        public static int[] ProductExceptSelf(int[] nums)
        {
            int n = nums.Length;
            int[] result = new int[n];

            // result[i] 在第一輪只保存 nums[i] 左側的乘積；空的一側以乘法單位元素 1 表示。
            result[0] = 1;
            for (int i = 1; i < n; i++)
            {
                result[i] = nums[i - 1] * result[i - 1];
            }

            // 由右向左累積右側乘積，與已保存的左側乘積相乘後即為最終答案。
            int rightProduct = 1;
            for (int i = n - 1; i >= 0; i--)
            {
                result[i] *= rightProduct;
                rightProduct *= nums[i];
            }

            return result;
        }

        /// <summary>
        /// 以兩個輔助陣列計算每個位置「除自身以外」所有元素的乘積。
        /// 前綴陣列保存各位置左側乘積，後綴陣列保存右側乘積，最後逐項相乘得到答案；
        /// 此設計讓中間狀態更直觀，時間複雜度為 O(n)，額外空間複雜度為 O(n)。
        /// 輸入長度須至少為 2，且任一前綴、後綴與答案乘積皆須在 32-bit 整數範圍內。
        /// </summary>
        /// <param name="nums">要計算的整數陣列；方法不會修改此陣列。</param>
        /// <returns>新陣列，其中每個位置為原陣列除該位置以外所有元素的乘積。</returns>
        public static int[] ProductExceptSelfWithArrays(int[] nums)
        {
            int n = nums.Length;
            int[] leftProducts = new int[n];
            int[] rightProducts = new int[n];
            int[] result = new int[n];

            // 兩端外側沒有元素，因此左右空乘積都以 1 作為起點。
            leftProducts[0] = 1;
            rightProducts[n - 1] = 1;

            for (int i = 1; i < n; i++)
            {
                leftProducts[i] = leftProducts[i - 1] * nums[i - 1];
            }

            for (int i = n - 2; i >= 0; i--)
            {
                rightProducts[i] = rightProducts[i + 1] * nums[i + 1];
            }

            // 每個位置的答案由不包含自身的左側乘積與右側乘積組成。
            for (int i = 0; i < n; i++)
            {
                result[i] = leftProducts[i] * rightProducts[i];
            }

            return result;
        }
    }
}
