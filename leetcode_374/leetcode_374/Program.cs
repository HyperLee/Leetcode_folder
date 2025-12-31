namespace leetcode_374;

/// <summary>
/// LeetCode 374. Guess Number Higher or Lower 解題類別
/// </summary>
class Program
{
    /// <summary>
    /// 模擬的答案（用於測試）
    /// </summary>
    private static int _pick;

    /// <summary>
    /// 374. Guess Number Higher or Lower
    /// https://leetcode.com/problems/guess-number-higher-or-lower/
    /// 374. 猜数字大小
    /// https://leetcode.cn/problems/guess-number-higher-or-lower/description/
    /// 
    /// 繁體中文翻譯：
    /// 我們正在玩一個猜數字遊戲：我從 1 到 n 選一個數字（選定不變），
    /// 你要猜出我選的數字。每次你猜錯，我會告訴你猜的數字是太大還是太小。
    /// 使用預先定義的 API `int guess(int num)`，其回傳值為：
    /// -1：你猜的數字比答案大（num > pick）。
    /// 1：你猜的數字比答案小（num < pick）。
    /// 0：你猜的數字等於答案（num == pick）。
    /// 回傳我選的數字（pick）。
    /// 
    /// </summary>
    /// <param name="args">命令列參數</param>
    static void Main(string[] args)
    {
        Console.WriteLine("=== LeetCode 374. Guess Number Higher or Lower ===\n");

        var solution = new Program();

        // 測試案例 1：n = 10, pick = 6
        _pick = 6;
        int result1 = solution.GuessNumber(10);
        Console.WriteLine($"測試案例 1: n = 10, pick = 6");
        Console.WriteLine($"結果: {result1}, 預期: 6, 通過: {result1 == 6}\n");

        // 測試案例 2：n = 1, pick = 1
        _pick = 1;
        int result2 = solution.GuessNumber(1);
        Console.WriteLine($"測試案例 2: n = 1, pick = 1");
        Console.WriteLine($"結果: {result2}, 預期: 1, 通過: {result2 == 1}\n");

        // 測試案例 3：n = 2, pick = 1
        _pick = 1;
        int result3 = solution.GuessNumber(2);
        Console.WriteLine($"測試案例 3: n = 2, pick = 1");
        Console.WriteLine($"結果: {result3}, 預期: 1, 通過: {result3 == 1}\n");

        // 測試案例 4：n = 100, pick = 73
        _pick = 73;
        int result4 = solution.GuessNumber(100);
        Console.WriteLine($"測試案例 4: n = 100, pick = 73");
        Console.WriteLine($"結果: {result4}, 預期: 73, 通過: {result4 == 73}\n");

        // 測試案例 5：邊界測試 n = 2147483647, pick = 2147483647
        _pick = 2147483647;
        int result5 = solution.GuessNumber(2147483647);
        Console.WriteLine($"測試案例 5: n = 2147483647, pick = 2147483647");
        Console.WriteLine($"結果: {result5}, 預期: 2147483647, 通過: {result5 == 2147483647}\n");

        Console.WriteLine("=== 所有測試完成 ===");
    }

    /// <summary>
    /// 模擬 LeetCode 提供的 guess API
    /// </summary>
    /// <param name="num">猜測的數字</param>
    /// <returns>
    /// -1：猜測的數字比答案大（num > pick）
    ///  1：猜測的數字比答案小（num &lt; pick）
    ///  0：猜測的數字等於答案（num == pick）
    /// </returns>
    private static int guess(int num)
    {
        if (num > _pick)
        {
            return -1;
        }

        if (num < _pick)
        {
            return 1;
        }

        return 0;
    }

    /// <summary>
    /// 使用二分查找法猜測數字
    /// 
    /// <para><b>解題思路：</b></para>
    /// <para>
    /// 記選出的數字為 pick，猜測的數字為 x。根據題目描述：
    /// - 若 guess(x) ≤ 0 則說明 x ≥ pick（猜的數字太大或正確）
    /// - 若 guess(x) > 0 則說明 x &lt; pick（猜的數字太小）
    /// </para>
    /// 
    /// <para><b>演算法步驟：</b></para>
    /// <list type="number">
    ///   <item>初始化搜尋區間 [left, right] = [1, n]</item>
    ///   <item>計算中間值 mid = left + (right - left) / 2（避免整數溢位）</item>
    ///   <item>呼叫 guess(mid) 判斷：
    ///     <list type="bullet">
    ///       <item>若 ≤ 0：答案在 [left, mid]，縮小右邊界</item>
    ///       <item>若 > 0：答案在 [mid+1, right]，縮小左邊界</item>
    ///     </list>
    ///   </item>
    ///   <item>重複步驟 2-3 直到 left == right，此時即為答案</item>
    /// </list>
    /// 
    /// <para><b>時間複雜度：</b>O(log n) - 每次迭代將搜尋範圍減半</para>
    /// <para><b>空間複雜度：</b>O(1) - 只使用常數額外空間</para>
    /// </summary>
    /// <param name="n">數字範圍的上界，答案在 [1, n] 之間</param>
    /// <returns>猜測的正確答案 pick</returns>
    /// <example>
    /// <code>
    ///  範例：n = 10, pick = 6
    ///  二分查找過程：
    ///  第 1 次：left=1, right=10, mid=5, guess(5)=1 (太小) → left=6
    ///  第 2 次：left=6, right=10, mid=8, guess(8)=-1 (太大) → right=8
    ///  第 3 次：left=6, right=8, mid=7, guess(7)=-1 (太大) → right=7
    ///  第 4 次：left=6, right=7, mid=6, guess(6)=0 (正確) → right=6
    ///  結束：left=6, right=6, 回傳 6
    /// </code>
    /// </example>
    public int GuessNumber(int n)
    {
        // 初始化搜尋區間的左右邊界
        int left = 1, right = n;

        // 持續二分查找，直到區間縮小為一個點
        while (left < right)
        {
            // 計算中間值，使用 left + (right - left) / 2 避免 (left + right) 可能造成的整數溢位
            int mid = left + (right - left) / 2;

            // 根據 guess API 的回傳值判斷答案所在區間
            if (guess(mid) <= 0)
            {
                // guess(mid) 回傳 -1 或 0，表示 mid >= pick
                // 答案在區間 [left, mid] 中，縮小右邊界
                right = mid;
            }
            else
            {
                // guess(mid) 回傳 1，表示 mid < pick
                // 答案在區間 [mid+1, right] 中，縮小左邊界
                left = mid + 1;
            }
        }

        // 迴圈結束時 left == right，區間縮為一個點，即為答案
        return left;
    }
}
