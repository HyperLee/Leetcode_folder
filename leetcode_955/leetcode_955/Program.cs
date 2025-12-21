namespace leetcode_955;

class Program
{
    /// <summary>
    /// 955. Delete Columns to Make Sorted II
    /// https://leetcode.com/problems/delete-columns-to-make-sorted-ii/description/?envType=daily-question&envId=2025-12-21
    /// 955. 刪列造序 II
    /// https://leetcode.cn/problems/delete-columns-to-make-sorted-ii/description/
    /// 
    /// 繁體中文：
    /// 給定一個長度相同的字串陣列 strs（共有 n 個字串）。
    /// 我們可以選擇任意一組刪除索引，並對每個字串移除這些索引位置的字元。
    /// 例如，若 strs = ["abcdef","uvwxyz"] 且刪除索引為 {0, 2, 3}，刪除後的陣列為 ["bef", "vyz"]。
    /// 假設我們選擇了一組刪除索引 answer，使得刪除後的陣列元素呈字典序（即 strs[0] <= strs[1] <= ... <= strs[n - 1]），
    /// 請回傳 answer.length 的最小可能值。
    /// 
    /// </summary>
    /// <param name="args"></param>
    static void Main(string[] args)
    {
        var p = new Program();

        var tests = new (string[] strs, int expected)[]
        {
            (new string[] { "ca", "bb", "ac" }, 1),
            (new string[] { "xc", "yb", "za" }, 0),
            (new string[] { "zyx", "wvu", "tsr" }, 3),
        };

        foreach (var (strs, expected) in tests)
        {
            int res = p.MinDeletionSize(strs);
            Console.WriteLine($"Input: [{string.Join(", ", strs)}] => Output: {res}, Expected: {expected} {(res == expected ? "✅" : "❌")}");
        }
    }

    /// <summary>
    /// 計算最少要刪除的欄位數，使得刪除後的字串陣列呈現字典序（非減序）。
    /// 逐欄檢查，若加入該欄會破壞排序則刪除該欄，否則保留並把該欄字元併入已保留的字串中。
    /// </summary>
    /// <param name="strs">輸入的字串陣列，所有字串長度相同。</param>
    /// <returns>最少刪除的欄位數。</returns>
    public int MinDeletionSize(string[] strs)
    {
        if (strs == null || strs.Length <= 1)
            return 0;

        int n = strs.Length;
        int m = strs[0].Length;
        string[] a = new string[n]; // 最終得到的字串陣列
        for (int i = 0; i < n; i++)
        {
            a[i] = string.Empty;
        }

        int ans = 0;
        for (int j = 0; j < m; j++)
        {
            bool deleteColumn = false;
            for (int i = 0; i < n - 1; i++)
            {
                string left = a[i] + strs[i][j];
                string right = a[i + 1] + strs[i + 1][j];
                if (string.Compare(left, right, StringComparison.Ordinal) > 0)
                {
                    // 第 j 欄會破壞排序，必須刪除
                    ans++;
                    deleteColumn = true;
                    break;
                }
            }

            if (deleteColumn)
                continue;

            // 保留該欄，將字元加到 a 中
            for (int i = 0; i < n; i++)
            {
                a[i] += strs[i][j];
            }
        }

        return ans;
    }
}
