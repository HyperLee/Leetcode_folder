namespace leetcode_055;

class Program
{
    /// <summary>
    /// 55. Jump Game
    /// https://leetcode.com/problems/jump-game/description/
    /// 55. 跳跃游戏
    /// https://leetcode.cn/problems/jump-game/description/
    ///
    /// You are given an integer array nums. You are initially positioned at the array's first index,
    /// and each element in the array represents your maximum jump length at that position.
    /// Return true if you can reach the last index, or false otherwise.
    ///
    /// 給定一個整數陣列 nums。你一開始位於陣列的第一個索引位置，而陣列中的每個元素代表你在該位置
    /// 最多可以跳躍的長度。請回傳你是否能到達最後一個索引；可以則回傳 true，否則回傳 false。
    /// </summary>
    /// <remarks>
    /// 程式進入點會執行固定測試資料，示範兩種貪心解法是否能正確判斷最後一格的可達性。
    /// </remarks>
    /// <param name="args">命令列參數；本範例不使用。</param>
    static void Main(string[] args)
    {
        Program solution = new Program();
        (int[] Nums, bool Expected)[] testCases =
        [
            ([2, 3, 1, 1, 4], true),
            ([3, 2, 1, 0, 4], false),
            ([0], true),
            ([2, 0, 0], true),
        ];

        Console.WriteLine("LeetCode 55. Jump Game");
        Console.WriteLine();

        foreach ((int[] nums, bool expected) in testCases)
        {
            bool result1 = solution.CanJump(nums);
            bool result2 = solution.CanJump2(nums);
            string values = string.Join(", ", nums);

            Console.WriteLine($"nums = [{values}], expected = {expected}");
            Console.WriteLine($"  CanJump : {result1} ({FormatStatus(result1, expected)})");
            Console.WriteLine($"  CanJump2: {result2} ({FormatStatus(result2, expected)})");
            Console.WriteLine();
        }
    }

    /// <summary>
    /// 使用正向貪心判斷是否能抵達最後一格。
    /// 解題概念是維護目前能抵達的最遠索引 <c>rightMost</c>，只要迭代位置仍在可達範圍內，
    /// 就用該位置的跳躍長度更新最遠可達位置；若最遠位置碰到或超過最後一格，代表可以完成。
    /// </summary>
    /// <param name="nums">非空整數陣列；每個元素代表在該索引最多可向右跳躍的步數。</param>
    /// <returns>可以從索引 0 抵達最後一個索引時回傳 <c>true</c>，否則回傳 <c>false</c>。</returns>
    public bool CanJump(int[] nums)
    {
        int n = nums.Length;
        int rightMost = 0;

        for (int i = 0; i < n; i++)
        {
            // 只有目前索引可達，才有資格用它延伸下一段可達範圍。
            if (i <= rightMost)
            {
                rightMost = Math.Max(rightMost, i + nums[i]);

                // 最遠可達位置已覆蓋終點時即可提前結束。
                if (rightMost >= n - 1)
                {
                    return true;
                }
            }
        }
        return false;
    }

    /// <summary>
    /// 使用等價的正向貪心寫法判斷是否能抵達最後一格。
    /// 解題概念是以 <c>max</c> 記錄目前所有可達位置能延伸出的最遠索引；
    /// 若迭代索引大於 <c>max</c>，代表中間存在無法跨越的斷點。
    /// </summary>
    /// <param name="nums">整數陣列；空陣列不符合題目限制，本方法會回傳 <c>false</c>。</param>
    /// <returns>可以從索引 0 抵達最後一個索引時回傳 <c>true</c>，否則回傳 <c>false</c>。</returns>
    public bool CanJump2(int[] nums)
    {
        int n = nums.Length;
        if (n == 0)
        {
            return false;
        }

        int max = 0;
        for (int i = 0; i < n; i++)
        {
            // i 超過 max 時，表示目前位置無法由先前任何位置跳到。
            if (i > max)
            {
                return false;
            }

            max = Math.Max(max, i + nums[i]);

            // max 已覆蓋終點時即可提前成功，不必掃完剩餘元素。
            if (max >= n - 1)
            {
                return true;
            }
        }
        return true;
    }

    /// <summary>
    /// 將實際結果與預期結果轉成測試狀態文字，供範例執行輸出使用。
    /// </summary>
    /// <param name="actual">解法回傳的實際結果。</param>
    /// <param name="expected">測試案例的預期結果。</param>
    /// <returns>結果一致時回傳 <c>PASS</c>，否則回傳 <c>FAIL</c>。</returns>
    private static string FormatStatus(bool actual, bool expected)
    {
        return actual == expected ? "PASS" : "FAIL";
    }
}
