namespace leetcode_2566;

class Program
{
    /// <summary>
    /// 2566. Maximum Difference by Remapping a Digit
    /// https://leetcode.com/problems/maximum-difference-by-remapping-a-digit/description/
    /// <para>
    /// You are given an integer num. You know that Bob will sneakily remap one of the 10 possible digits (0 to 9) to another digit.
    ///
    /// Return the difference between the maximum and minimum values Bob can make by remapping exactly one digit in num.
    ///
    /// Notes:
    /// - When Bob remaps a digit d1 to another digit d2, Bob replaces all occurrences of d1 in num with d2.
    /// - Bob can remap a digit to itself, in which case num does not change.
    /// - Bob can remap different digits to obtain the minimum and maximum values respectively.
    /// - The resulting number after remapping can contain leading zeroes.
    ///
    /// Example 1:
    /// Input: num = 11891
    /// Output: 99009
    /// Explanation: To achieve the maximum value, Bob can remap digit 1 to digit 9 to yield 99899. To achieve the minimum value, Bob can remap digit 1 to digit 0, yielding 890. The difference between these two numbers is 99009.
    ///
    /// Example 2:
    /// Input: num = 90
    /// Output: 99
    /// Explanation: The maximum value is 99 if 0 is replaced by 9, and the minimum value is 0 if 9 is replaced by 0. Thus, return 99.
    ///
    /// Constraints:
    /// - 1 &lt;= num &lt;= 10^8
    /// </para>
    /// <para>
    /// 2566. 重新映射數字的最大差值
    /// https://leetcode.cn/problems/maximum-difference-by-remapping-a-digit/description/
    ///
    /// 給定一個整數 num。你知道 Bob 會暗中把 10 個可能數字（0 到 9）中的一個重新映射為另一個數字。
    ///
    /// 回傳 Bob 在 num 中恰好重新映射一個數字後，所能得到的最大值與最小值之差。
    ///
    /// 注意事項：
    /// - 當 Bob 將數字 d1 重新映射為數字 d2 時，他會把 num 中所有出現的 d1 都替換成 d2。
    /// - Bob 可以把一個數字映射為它自己，此時 num 不會改變。
    /// - Bob 可以分別重新映射不同數字，以取得最小值與最大值。
    /// - 重新映射後的數字可以包含前導零。
    ///
    /// 範例 1：
    /// 輸入：num = 11891
    /// 輸出：99009
    /// 解釋：為取得最大值，Bob 可以把數字 1 映射為 9，得到 99899。為取得最小值，他可以把數字 1 映射為 0，得到 890。兩數之差為 99009。
    ///
    /// 範例 2：
    /// 輸入：num = 90
    /// 輸出：99
    /// 解釋：若將 0 替換為 9，可得到最大值 99；若將 9 替換為 0，可得到最小值 0。因此回傳 99。
    ///
    /// 限制條件：
    /// - 1 &lt;= num &lt;= 10^8
    /// </para>
    /// </summary>
    /// <param name="args"></param>
    static void Main(string[] args)
    {
        // 測試範例
        int num = 12345;
        var prog = new Program();
        int diff1 = prog.MinMaxDifference(num);
        int diff2 = prog.MinMaxDifference2(num);
        int diff3 = prog.MinMaxDifference3(num);
        Console.WriteLine($"num = {num}");
        Console.WriteLine($"解法一最大差值: {diff1}");
        Console.WriteLine($"解法二最大差值: {diff2}");
        Console.WriteLine($"解法三最大差值: {diff3}");
    }

    /// <summary>
    /// 解法一：暴力枚舉所有可能的數字映射，計算最大與最小值的差。
    /// 解題說明：
    /// 1. 將 num 轉為字串，遍歷所有 0~9 的數字 d1，嘗試將其全部替換為 0~9 的另一個數字 d2（d1 != d2）。
    /// 2. 每次替換後，將結果轉為整數，更新最大值與最小值。
    /// 3. 最後回傳最大值與最小值的差。
    /// 時間複雜度 O (100 * n)，n 為位數。
    /// </summary>
    /// <param name="num"></param>
    /// <returns></returns>
    public int MinMaxDifference(int num)
    {
        string s = num.ToString();
        int max = int.MinValue;
        int min = int.MaxValue;
        // 枚舉所有可能的數字映射
        for (char d1 = '0'; d1 <= '9'; d1++)
        {
            for (char d2 = '0'; d2 <= '9'; d2++)
            {
                if (d1 == d2) continue; // 跳過自身映射
                string mapped = s.Replace(d1, d2); // 替換所有 d1 為 d2
                int val = int.Parse(mapped);
                max = Math.Max(max, val);
                min = Math.Min(min, val);
            }
        }
        return max - min;
    }

    /// <summary>
    ///ref:https://leetcode.cn/problems/maximum-difference-by-remapping-a-digit/solutions/3690212/ti-huan-yi-ge-shu-zi-hou-de-zui-da-chai-3oyg4/?envType=daily-question&envId=2025-06-14
    /// 
    /// 解法二：貪心法，最大值將第一個不是 9 的數字全部替換為 9，最小值將第一個數字全部替換為 0。
    /// 解題說明：
    /// 1. 最大值：從左到右找到第一個不是 9 的數字，將其全部替換為 9。
    /// 2. 最小值：將第一個數字全部替換為 0。
    /// 3. 回傳兩者差值。
    /// 時間複雜度 O (n)，n 為位數。
    /// </summary>
    /// <param name="num"></param>
    /// <returns></returns>
    public int MinMaxDifference2(int num)
    {
        string s = num.ToString();
        string t = s;
        int pos = 0;
        // 找到第一個不是 9 的數字
        while (pos < s.Length && s[pos] == '9')
        {
            pos++;
        }

        if (pos < s.Length)
        {
            s = s.Replace(s[pos], '9'); // 將該數字全部替換為 9
        }

        t = t.Replace(t[0], '0'); // 將第一個數字全部替換為 0

        return int.Parse(s) - int.Parse(t);
    }

    /// <summary>
    ///ref:https://leetcode.cn/problems/maximum-difference-by-remapping-a-digit/solutions/2119447/mei-ju-by-endlesscheng-slfa/?envType=daily-question&envId=2025-06-14
    /// 
    /// 解法三：Java 寫法轉為 C#，同樣採用貪心法。
    /// 1. 最大值：找到第一個不是 9 的字元，將其全部替換為 9。
    /// 2. 最小值：將第一個字元全部替換為 0。
    /// 3. 回傳兩者差值。
    /// 時間複雜度 O (n)，空間複雜度 O (1)。
    /// </summary>
    /// <param name="num"></param>
    /// <returns></returns>
    public int MinMaxDifference3(int num)
    {
        string s = num.ToString();
        int mx = num;
        // 找到第一個不是 '9' 的字元，全部替換為 '9'
        foreach (char c in s)
        {
            if (c != '9')
            {
                mx = int.Parse(s.Replace(c, '9'));
                break;
            }
        }
        // 將第一個字元全部替換為 '0' 得到最小值
        int mn = int.Parse(s.Replace(s[0], '0'));
        return mx - mn;
    }
}