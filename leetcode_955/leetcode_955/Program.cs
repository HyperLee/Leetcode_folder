namespace leetcode_955;

class Program
{
    /// <summary>
    /// 955. Delete Columns to Make Sorted II
    /// https://leetcode.com/problems/delete-columns-to-make-sorted-ii/description/
    /// <para>
    /// You are given an array strs of n strings, all of the same length.
    ///
    /// You may choose any deletion indices and delete the characters at those indices from every string.
    ///
    /// For example, with strs = ["abcdef","uvwxyz"] and deletion indices {0, 2, 3}, the final array is ["bef","vyz"].
    ///
    /// Choose deletion indices answer so that after deletion the final array is in lexicographic order: strs[0] &lt;= strs[1] &lt;= strs[2] &lt;= ... &lt;= strs[n - 1]. Return the minimum possible answer.length.
    ///
    /// Example 1:
    /// Input: strs = ["ca","bb","ac"]
    /// Output: 1
    /// Explanation: After deleting the first column, strs = ["a","b","c"], which is in lexicographic order. At least 1 deletion is required because strs was not initially in lexicographic order, so the answer is 1.
    ///
    /// Example 2:
    /// Input: strs = ["xc","yb","za"]
    /// Output: 0
    /// Explanation: strs is already in lexicographic order, so nothing must be deleted. The rows themselves need not be in lexicographic order; strs[0][0] &lt;= strs[0][1] &lt;= ... is not required.
    ///
    /// Example 3:
    /// Input: strs = ["zyx","wvu","tsr"]
    /// Output: 3
    /// Explanation: Every column must be deleted.
    ///
    /// Constraints:
    /// - n == strs.length
    /// - 1 &lt;= n &lt;= 100
    /// - 1 &lt;= strs[i].length &lt;= 100
    /// - strs[i] consists of lowercase English letters.
    /// </para>
    /// <para>
    /// 955. 刪列造序 II
    /// https://leetcode.cn/problems/delete-columns-to-make-sorted-ii/description/
    ///
    /// 給定包含 n 個等長字串的陣列 strs。
    ///
    /// 可以選擇任意刪除索引，並從每個字串中刪除這些索引位置的字元。
    ///
    /// 例如，strs = ["abcdef","uvwxyz"]，刪除索引為 {0, 2, 3} 時，刪除後的陣列為 ["bef","vyz"]。
    ///
    /// 選擇刪除索引集合 answer，使刪除後的最終陣列按字典順序排列：strs[0] &lt;= strs[1] &lt;= strs[2] &lt;= ... &lt;= strs[n - 1]。回傳 answer.length 的最小可能值。
    ///
    /// 範例 1：
    /// 輸入：strs = ["ca","bb","ac"]
    /// 輸出：1
    /// 解釋：刪除第一欄後，strs = ["a","b","c"]，已按字典順序排列。因 strs 起初並非字典序，至少需要刪除 1 欄，所以答案是 1。
    ///
    /// 範例 2：
    /// 輸入：strs = ["xc","yb","za"]
    /// 輸出：0
    /// 解釋：strs 已按字典順序排列，因此不需刪除。strs 的每一列本身不一定按字典序排列；不要求 strs[0][0] &lt;= strs[0][1] &lt;= ...。
    ///
    /// 範例 3：
    /// 輸入：strs = ["zyx","wvu","tsr"]
    /// 輸出：3
    /// 解釋：必須刪除每一欄。
    ///
    /// 限制條件：
    /// - n == strs.length
    /// - 1 &lt;= n &lt;= 100
    /// - 1 &lt;= strs[i].length &lt;= 100
    /// - strs[i] 由小寫英文字母組成。
    /// </para>
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
