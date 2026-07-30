namespace leetcode_042;

class Program
{
    /// <summary>
    /// 42. Trapping Rain Water
    /// https://leetcode.com/problems/trapping-rain-water/description/
    /// 42. 接雨水
    /// https://leetcode.cn/problems/trapping-rain-water/description/
    /// 
    /// 題目描述:
    /// 給定一個非負整數數組 height，其中每個元素代表一個寬度為 1 的柱子的高度。
    /// 計算在這些柱子所形成的容器中，能夠接住多少雨水。
    /// </summary>
    /// <remarks>
    /// 使用固定案例依序驗證雙指標、動態規劃與單調棧三種解法，
    /// 並輸出各解法的預期值、實際值與 PASS/FAIL 結果。
    /// </remarks>
    /// <param name="args">命令列參數；本範例不使用此參數。</param>
    static void Main(string[] args)
    {
        (string Name, int[] Height, int Expected)[] testCases =
        [
            ("官方範例一", [0, 1, 0, 2, 1, 0, 1, 3, 2, 1, 2, 1], 6),
            ("官方範例二", [4, 2, 0, 3, 2, 5], 9),
            ("高度上界的單一柱子", [100000], 0),
            ("遞減排列不會積水", [5, 4, 3, 2, 1], 0),
            ("相同高度形成凹槽", [2, 0, 2], 2)
        ];

        int passedChecks = 0;
        int totalChecks = testCases.Length * 3;

        for (int index = 0; index < testCases.Length; index++)
        {
            (string name, int[] height, int expected) = testCases[index];
            int twoPointersResult = Trap(height);
            int dynamicProgrammingResult = Trap2(height);
            int monotonicStackResult = Trap3(height);
            bool twoPointersPassed = twoPointersResult == expected;
            bool dynamicProgrammingPassed = dynamicProgrammingResult == expected;
            bool monotonicStackPassed = monotonicStackResult == expected;

            passedChecks += twoPointersPassed ? 1 : 0;
            passedChecks += dynamicProgrammingPassed ? 1 : 0;
            passedChecks += monotonicStackPassed ? 1 : 0;

            Console.WriteLine($"案例 {index + 1}：{name}");
            Console.WriteLine($"輸入：[{string.Join(", ", height)}]");
            Console.WriteLine($"Expected：{expected}");
            Console.WriteLine(
                $"Trap（雙指標）Actual：{twoPointersResult} => {(twoPointersPassed ? "PASS" : "FAIL")}");
            Console.WriteLine(
                $"Trap2（動態規劃）Actual：{dynamicProgrammingResult} => {(dynamicProgrammingPassed ? "PASS" : "FAIL")}");
            Console.WriteLine(
                $"Trap3（單調棧）Actual：{monotonicStackResult} => {(monotonicStackPassed ? "PASS" : "FAIL")}");
            Console.WriteLine();
        }

        Console.WriteLine($"總結：{passedChecks}/{totalChecks} 項驗證通過");
    }

    /// <summary>
    /// 使用雙指標計算柱狀圖可承接的雨水總量。
    /// 從陣列兩端向中間移動，持續維護左右最高柱；較低的一側已具備足以確定水位的另一側邊界，
    /// 因此可立即累加該位置的積水量。輸入應為符合題目限制的非負整數陣列，空陣列則回傳 0；
    /// 最終回傳全部位置的積水單位數。時間複雜度為 O(n)，額外空間複雜度為 O(1)。
    /// </summary>
    /// <param name="height">各柱高度組成的非負整數陣列；正式題目限制長度至少為 1。</param>
    /// <returns>所有柱子之間能承接的雨水總量；空陣列回傳 0。</returns>
    public static int Trap(int[] height)
    {
        if (height.Length == 0)
        {
            return 0;
        }

        int left = 0;
        int right = height.Length - 1;
        int leftMax = 0;
        int rightMax = 0;
        int trappedWater = 0;

        while (left < right)
        {
            leftMax = Math.Max(leftMax, height[left]);
            rightMax = Math.Max(rightMax, height[right]);

            // 較低的最高邊界決定目前可確認的水位，另一側尚未掃描的高度不會改變此結果。
            if (leftMax <= rightMax)
            {
                trappedWater += leftMax - height[left];
                left++;
            }
            else
            {
                trappedWater += rightMax - height[right];
                right--;
            }
        }

        return trappedWater;
    }

    /// <summary>
    /// 使用動態規劃計算柱狀圖可承接的雨水總量。
    /// 先建立每個位置左側與右側的最高柱陣列，再以兩側最高柱的較小值減去目前柱高，
    /// 得到各位置的積水量。輸入應為符合題目限制的非負整數陣列，空陣列則回傳 0；
    /// 最終回傳全部位置的積水單位數。時間複雜度為 O(n)，額外空間複雜度為 O(n)。
    /// </summary>
    /// <param name="height">各柱高度組成的非負整數陣列；正式題目限制長度至少為 1。</param>
    /// <returns>所有柱子之間能承接的雨水總量；空陣列回傳 0。</returns>
    public static int Trap2(int[] height)
    {
        if (height.Length == 0)
        {
            return 0;
        }

        int[] leftMax = new int[height.Length];
        int[] rightMax = new int[height.Length];
        leftMax[0] = height[0];
        rightMax[^1] = height[^1];

        // 前後兩次掃描讓每個位置都能直接取得左右最高邊界。
        for (int index = 1; index < height.Length; index++)
        {
            leftMax[index] = Math.Max(leftMax[index - 1], height[index]);
        }

        for (int index = height.Length - 2; index >= 0; index--)
        {
            rightMax[index] = Math.Max(rightMax[index + 1], height[index]);
        }

        int trappedWater = 0;

        for (int index = 0; index < height.Length; index++)
        {
            trappedWater += Math.Min(leftMax[index], rightMax[index]) - height[index];
        }

        return trappedWater;
    }

    /// <summary>
    /// 使用單調遞減棧計算柱狀圖可承接的雨水總量。
    /// 棧內保存尚未找到右邊界的柱子索引；遇到較高柱時，依序彈出凹槽底部，
    /// 由新的棧頂與目前柱子形成左右邊界並計算該層水量。輸入應為符合題目限制的非負整數陣列，
    /// 空陣列則回傳 0；最終回傳全部凹槽的積水單位數。時間複雜度為 O(n)，額外空間複雜度為 O(n)。
    /// </summary>
    /// <param name="height">各柱高度組成的非負整數陣列；正式題目限制長度至少為 1。</param>
    /// <returns>所有柱子之間能承接的雨水總量；空陣列回傳 0。</returns>
    public static int Trap3(int[] height)
    {
        if (height.Length == 0)
        {
            return 0;
        }

        Stack<int> indices = new();
        int trappedWater = 0;

        for (int index = 0; index < height.Length; index++)
        {
            while (indices.Count > 0 && height[index] > height[indices.Peek()])
            {
                int bottom = indices.Pop();

                if (indices.Count == 0)
                {
                    // 沒有左邊界時無法形成封閉凹槽。
                    break;
                }

                int leftBoundary = indices.Peek();
                int width = index - leftBoundary - 1;
                int boundedHeight =
                    Math.Min(height[leftBoundary], height[index]) - height[bottom];

                trappedWater += width * boundedHeight;
            }

            indices.Push(index);
        }

        return trappedWater;
    }
}
