namespace leetcode_1460
{
    internal class Program
    {
        /// <summary>
        /// 1460. Make Two Arrays Equal by Reversing Subarrays
        /// https://leetcode.com/problems/make-two-arrays-equal-by-reversing-subarrays/description/?envType=daily-question&envId=2024-08-03
        /// 
        /// 1460. 通过翻转子数组使两个数组相等
        /// https://leetcode.cn/problems/make-two-arrays-equal-by-reversing-subarrays/description/
        /// </summary>
        /// <remarks>
        /// 程式進入點會以固定案例驗證排序法與計數法，並輸出每個案例的預期與實際結果。
        /// 所有案例通過時結束碼為 0，否則結束碼為 1。
        /// </remarks>
        /// <param name="args">命令列參數；本範例不使用。</param>
        static void Main(string[] args)
        {
            var testCases = new[]
            {
                (Name: "一般排列順序不同", Target: new[] { 1, 2, 3, 4 }, Arr: new[] { 2, 4, 1, 3 }, Expected: true),
                (Name: "重複值頻率相同", Target: new[] { 1, 1, 2, 3 }, Arr: new[] { 3, 1, 2, 1 }, Expected: true),
                (Name: "重複值頻率不同", Target: new[] { 1, 1, 2, 3 }, Arr: new[] { 1, 2, 2, 3 }, Expected: false),
                (Name: "單一元素邊界", Target: new[] { 1000 }, Arr: new[] { 1000 }, Expected: true),
                (Name: "空陣列額外案例", Target: Array.Empty<int>(), Arr: Array.Empty<int>(), Expected: true)
            };

            bool allPassed = true;
            foreach (var testCase in testCases)
            {
                allPassed &= RunCase(testCase.Name, testCase.Target, testCase.Arr, testCase.Expected);
            }

            Console.WriteLine($"全部案例: {(allPassed ? "PASS" : "FAIL")}");
            Environment.ExitCode = allPassed ? 0 : 1;
        }

        /// <summary>
        /// 執行單一案例，使用獨立輸入驗證兩種解法並回報是否符合預期。
        /// </summary>
        /// <param name="name">案例名稱。</param>
        /// <param name="target">目標陣列。</param>
        /// <param name="arr">待比較的陣列。</param>
        /// <param name="expected">案例預期結果。</param>
        /// <returns>兩種解法都符合預期時回傳 true，否則回傳 false。</returns>
        private static bool RunCase(string name, int[] target, int[] arr, bool expected)
        {
            bool sortResult = CanBeEqual((int[])target.Clone(), (int[])arr.Clone());
            bool countResult = CanBeEqual2((int[])target.Clone(), (int[])arr.Clone());
            bool passed = sortResult == expected && countResult == expected;

            Console.WriteLine($"案例：{name}");
            Console.WriteLine($"target = [{string.Join(", ", target)}]");
            Console.WriteLine($"arr = [{string.Join(", ", arr)}]");
            Console.WriteLine($"Expected: {expected}");
            Console.WriteLine($"CanBeEqual Actual: {sortResult}");
            Console.WriteLine($"CanBeEqual2 Actual: {countResult}");
            Console.WriteLine($"Result: {(passed ? "PASS" : "FAIL")}");
            Console.WriteLine();

            return passed;
        }


        /// <summary>
        /// 題目說明
        /// 給兩個陣列 target 與 arr
        /// 可以無限制翻轉,只要讓兩個陣列相同即可
        /// 
        /// 排序方法
        /// 
        /// 實際上不需要翻轉
        /// 1. 先把兩陣列排序
        /// 2. 比對陣列資料是否相同
        /// 3. 上述兩步驟即可
        /// 
        /// 都不限制翻轉比對次數了, 直接對比資料就好
        /// </summary>
        /// <remarks>
        /// 只要兩個陣列長度相同且元素與出現次數相同，就能透過翻轉子陣列重新排列成相同內容。
        /// 此方法會直接排序輸入陣列，因此會改變 target 與 arr 的元素順序。
        /// </remarks>
        /// <param name="target">目標陣列，會在方法中直接排序。</param>
        /// <param name="arr">待比較的陣列，會在方法中直接排序。</param>
        /// <returns>兩個排序後的陣列內容相同時回傳 true，否則回傳 false。</returns>
        public static bool CanBeEqual(int[] target, int[] arr)
        {
            Array.Sort(target);
            Array.Sort(arr);

            bool result = true;
            for(int i = 0; i < target.Length; i++)
            {
                // 反轉只能改變元素順序，排序後比較即可確認元素與出現次數都相同。
                // 比對兩個 陣列是否相同
                if (target[i] != arr[i])
                {
                    return false;
                }
            }


            return result;
        }


        /// <summary>
        /// Hash table 方法
        /// 
        /// 利用 Dictionary<> 去比對
        /// 
        /// 也可以使用兩個 dic 最後在去比對
        /// 但是這邊只使用一個
        /// 一開始新增資料
        /// 最後去比對資料
        /// 
        /// 時間複雜度: O(n)
        /// 空間複雜度: O(n)
        /// </summary>
        /// <remarks>
        /// 先統計 target 中每個值的頻率，再逐一消耗 arr 的值；不修改輸入陣列。
        /// 只要所有值都能配對且頻率沒有超過 target，就代表兩個陣列可透過翻轉重新排列成相同內容。
        /// </remarks>
        /// <param name="target">目標陣列，用來建立每個值的可用次數。</param>
        /// <param name="arr">待比較的陣列，用來扣除已使用的值。</param>
        /// <returns>兩個陣列的元素與出現次數相同時回傳 true，否則回傳 false。</returns>
        public static bool CanBeEqual2(int[] target, int[] arr)
        {
            Dictionary<int, int> dic1 = new Dictionary<int, int>();
            bool res = true;

            // 先累計 target 中每個值的可用次數，再用 arr 逐一扣除。
            // 把 target 資料 放到 dic1 裡面
            foreach (var item in target)
            {
                if(!dic1.ContainsKey(item))
                {
                    dic1.Add(item, 1);
                }
                else
                {
                    dic1[item]++;
                }
            }


            // arr 與 dic1 比對
            // 資料與次數都要比對
            // 沒有資料 或是 次數不同 都是錯誤
            foreach(var item2 in arr)
            {
                if(!dic1.ContainsKey(item2))
                {
                    return false;
                }
                else
                {
                    dic1[item2]--;
                }

                // 找不到值或扣除後變成負數，代表 arr 使用了 target 沒有或過量的元素。
                if (dic1[item2] < 0)
                {
                    return false;
                }
            }

            return res;

        }

    }
}
