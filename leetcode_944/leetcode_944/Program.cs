namespace leetcode_944;

class Program
{
    /// <summary>
    /// 944. Delete Columns to Make Sorted
    /// https://leetcode.com/problems/delete-columns-to-make-sorted/description/?envType=daily-question&envId=2025-12-20
    /// 944. 刪列造序
    /// https://leetcode.cn/problems/delete-columns-to-make-sorted/description/?envType=daily-question&envId=2025-12-20
    /// 
    /// 繁體中文題意：
    /// 給定一個包含 n 個字串的陣列 strs，所有字串長度相同，將每個字串按行排列成一個網格。
    /// 刪除那些不按字典序由上至下排序的欄位（column）。回傳需刪除的欄位數量。
    /// 範例：strs = ["abc", "bce", "cae"]，排列為：
    /// abc
    /// bce
    /// cae
    /// 第 0 欄 (a,b,c) 與 第 2 欄 (c,e,e) 是排序的；第 1 欄 (b,c,a) 不是，因此需刪除 1 個欄位。
    /// </summary>
    /// <param name="args"></param> <summary>
    /// 
    /// </summary>
    /// <param name="args"></param>
    static void Main(string[] args)
    {
        var solver = new Program();

        // 範例 1
        string[] test1 = new[] { "abc", "bce", "cae" };
        Console.WriteLine($"Input: [\"abc\",\"bce\",\"cae\"] => Output: {solver.MinDeletionSize(test1)}"); // 預期 1

        // 範例 2: 已排序的欄位
        string[] test2 = new[] { "a", "b" };
        Console.WriteLine($"Input: [\"a\",\"b\"] => Output: {solver.MinDeletionSize(test2)}"); // 預期 0

        // 範例 3: 全部欄位需刪除
        string[] test3 = new[] { "zyx", "wvu", "tsr" };
        Console.WriteLine($"Input: [\"zyx\",\"wvu\",\"tsr\"] => Output: {solver.MinDeletionSize(test3)}"); // 預期 3
    }

    /// <summary>
    /// 計算需刪除的欄位數量（Delete Columns to Make Sorted）。
    /// 解法：逐欄掃描；對於第 j 欄，從上到下檢查任意相鄰字元是否違反非遞減性（strs[i-1][j] > strs[i][j]），
    /// 若違反則該欄必須刪除，結果加 1 並進入下一欄（可提前中斷該欄檢查）。
    /// 時間複雜度：O(rows * cols)，空間複雜度：O(1)。
    /// </summary>
    /// <param name="strs">字串陣列，所有字串長度相同</param>
    /// <returns>需刪除的欄位數量</returns>
    public int MinDeletionSize(string[] strs)
    {
        // 橫：列數（rows）
        int rows = strs.Length;
        // 直：欄數（cols），所有字串長度相同
        int cols = strs[0].Length;
        // 結果：需刪除的欄位數
        int res = 0;

        // 對每一欄 j，從上到下檢查是否遞增（非遞減）
        // 一旦發現 strs[i - 1][j] > strs[i][j]，代表該欄必須刪除，直接跳出內層迴圈檢查下一欄
        for (int j = 0; j < cols; j++)
        {
            for (int i = 1; i < rows; i++)
            {
                if (strs[i - 1][j] > strs[i][j])
                {
                    // 這欄不符合條件，計數並提前中斷
                    res++;
                    break;
                }
            }
        }

        return res;
    }
}
