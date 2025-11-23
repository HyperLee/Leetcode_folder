using System.Collections.Immutable;

namespace leetcode_1262;

class Program
{
    /// <summary>
    /// 1262. Greatest Sum Divisible by Three
    /// https://leetcode.com/problems/greatest-sum-divisible-by-three/description/?envType=daily-question&envId=2025-11-23
    /// 1262. 可被三整除的最大和
    /// https://leetcode.cn/problems/greatest-sum-divisible-by-three/description/?envType=daily-question&envId=2025-11-23
    /// 
    /// </summary>
    /// <param name="args"></param>
    static void Main(string[] args)
    {
        // Demonstration test cases for MaxSumDivThree
        var solution = new Program();

        int[] test1 = { 3, 6, 5, 1, 8 };
        Console.WriteLine($"Input: [3,6,5,1,8] => Output: {solution.MaxSumDivThree(test1)} (Expected: 18)");

        int[] test2 = { 4 };
        Console.WriteLine($"Input: [4] => Output: {solution.MaxSumDivThree(test2)} (Expected: 0)");

        int[] test3 = { 1, 2, 3, 4, 4 };
        Console.WriteLine($"Input: [1,2,3,4,4] => Output: {solution.MaxSumDivThree(test3)} (Expected: 12)");
    }

    /// <summary>
    /// 最大可被三整除的總和
    ///
    /// 解題思路（貪心 + 正向思維）:
    /// 1. 將陣列裡的數字依除以 3 的餘數分為三組: v[0] (餘 0)、v[1] (餘 1)、v[2] (餘 2)。
    ///    - v[0] 中的數字隨時都可以加入答案，因為它們不會影響總和 mod 3。
    /// 2. 我們需要從 v[1] 和 v[2] 中選擇元素，使得選取數量滿足 (cnt1 - cnt2) mod 3 == 0，
    ///    以使總和的餘數為 0。
    /// 3. 觀察發現，若某一組的選取數量少於 |group| - 2，則可以繼續多選 3 個，使模 3 不變。
    ///    因此，對於 v[1] 與 v[2]，只需考慮選取數量在 { |group| - 2, |group| - 1, |group| }。
    /// 4. 我們對 v[1] 與 v[2] 都做降冪排序（貪心選最大的元素），
    ///    然後枚舉最多 3×3=9 種可能的 (cnt1, cnt2) 組合，求出最大的可被 3 整除的和。
    ///
    /// 複雜度:
    /// - 時間複雜度: O(n log n) （排序 v[1]、v[2] 的成本，其餘為線性遍歷）
    /// - 空間複雜度: O(n) （需要額外分組的 list）
    /// </summary>
    /// <param name="nums"></param>
    /// <returns></returns>
    public int MaxSumDivThree(int[] nums)
    {
        // 使用 v[0]、v[1]、v[2] 分別存放除以 3 餘數為 0、1、2 的數字列表
        IList<int>[] v = new IList<int>[3];
        for(int i = 0; i < 3; i++)
        {
            v[i] = new List<int>();
        }

        foreach(int num in nums)
        {
           v[num % 3].Add(num); 
        }
        // 由大到小排序，貪心選最大的元素
        ((List<int>) v[1]).Sort((a, b) => b - a);
        ((List<int>) v[2]).Sort((a, b) => b - a);

        int res = 0;
        int lb = v[1].Count;
        int lc = v[2].Count;
        // 只需考慮 cntb ∈ {lb-2, lb-1, lb} 和 cntc ∈ {lc-2, lc-1, lc}
        for(int cntb = lb - 2; cntb <= lb; cntb++)
        {
            for(int cntc = lc - 2; cntc <= lc; cntc++)
            {
                // 確保 cntb、cntc 不為負，且 cntb 與 cntc 模 3 同餘
                if(cntb >= 0 && cntc >= 0 && (cntb - cntc) % 3 == 0)
                {
                    res = Math.Max(res, GetSum(v[1], 0, cntb) + GetSum(v[2], 0, cntc));
                }
            }
        }
        return res + GetSum(v[0], 0, v[0].Count);
    }

    /// <summary>
    /// 加總指定範圍的元素值（list[start] + ... + list[end-1]）
    ///
    /// 此方法被用來計算在 v[1] 或 v[2] 中，選取前面 cnt 個元素的總和。
    /// </summary>
    /// <param name="list"></param>
    /// <param name="start"></param>
    /// <param name="end"></param>
    /// <returns></returns>
    public int GetSum(IList<int> list, int start, int end)
    {
        int sum = 0;
        // 從 start 開始到 end-1，累加 list 元素
        for(int i = start; i < end; i++)
        {
            sum += list[i];
        }
        return sum;
    }
}
