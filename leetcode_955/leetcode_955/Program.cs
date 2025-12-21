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
    /// 演算法說明：
    /// - 我們從左到右逐欄檢查（j 從 0 到 m-1）。若保留第 j 欄不會讓目前已保留欄組成的字串序列失序，則保留並把每個字串的第 j 個字元附加到累積字串 a[i]；否則刪除該欄（ans++）。
    /// - 保留欄會把相鄰字串分成若干「已確定相對順序的群組」，在後續欄位比較中可僅於每個群內檢查是否非減序，這能減少比較並增加保留欄的機會。
    /// 範例：
    /// strs = [ "ac", "ad", "ba", "bb" ]
    /// 第 0 欄 (a,a,b,b) 已是非減序，保留後可把資料分成兩群：[ac,ad] 與 [ba,bb]，因此檢查第 1 欄時只需檢查群內 (c<=d, a<=b)，不需比較 d 與 a。
    /// 因此保留已升序的欄通常比較有利；若某欄會破壞排序則必須刪除（否則無法達成字典序）。
    /// </summary>
    /// <param name="strs">輸入的字串陣列，所有字串長度相同（n 個字串，每個長度 m）。</param>
    /// <returns>最少刪除的欄位數。</returns>
    public int MinDeletionSize(string[] strs)
    {
        if (strs == null || strs.Length <= 1)
            return 0;

        int n = strs.Length;       // 字串數量
        int m = strs[0].Length;    // 每個字串的長度（欄數）
        string[] a = new string[n]; // a[i] 為保留欄位組合起來之第 i 個字串（累積字串，用於比較）
        for (int i = 0; i < n; i++)
        {
            a[i] = string.Empty;
        }

        int ans = 0; // 要刪除的欄數
        // 對每一欄 j 做決策：刪除或保留
        for (int j = 0; j < m; j++)
        {
            bool deleteColumn = false; // 表示是否要刪除第 j 欄
            // 檢查保留第 j 欄後，累積字串是否會破壞相鄰字串的非減序關係
            for (int i = 0; i < n - 1; i++)
            {
                // 將第 j 欄的字元暫時附加到目前的累積字串比較
                string left = a[i] + strs[i][j];
                string right = a[i + 1] + strs[i + 1][j];
                // 若 left > right，表示第 j 欄會造成失序，必須刪除此欄
                if (string.Compare(left, right, StringComparison.Ordinal) > 0)
                {
                    ans++;
                    deleteColumn = true;
                    break;
                }
            }

            if (deleteColumn)
                continue;

            // 若第 j 欄可以保留，將該欄字元正式附加到累積字串 a 中，
            // 這會讓某些相鄰字串對變成「已確定順序」，後續欄位比較時不需再比較跨群的字元。
            for (int i = 0; i < n; i++)
            {
                a[i] += strs[i][j];
            }
        }

        return ans;
    }
}
