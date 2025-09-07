namespace leetcode_1304;

class Program
{
    /// <summary>
    /// <summary>
    /// 1304. Find N Unique Integers Sum up to Zero
    /// https://leetcode.com/problems/find-n-unique-integers-sum-up-to-zero/
    /// 1304. 和為零的 N 個不同整數
    /// https://leetcode.cn/problems/find-n-unique-integers-sum-up-to-zero/
    ///
    /// </summary>
    /// <param name="args"></param>
    static void Main(string[] args)
    {
        // 範例示範：輸出 n = 5 的結果
        var p = new Program();
        int n = 5;
        int[] ans = p.SumZero(n);
        Console.WriteLine($"n = {n} -> [{string.Join(", ", ans)}]");
    }

    /// <summary>
    /// 解題說明 (方法一：對稱構造)
    /// 由於 x + (-x) = 0 恒成立，我們可以成對加入互為相反數的整數，例如 1 和 -1、2 和 -2，
    /// 這樣任意成對相加結果為 0。如果 n 為奇數，則再額外加入 0。具體構造：
    /// 令 m = ⌊n/2⌋，當 n 為偶數時回傳
    /// [1,2,...,m, -1,-2,...,-m]
    /// 當 n 為奇數時回傳
    /// [1,2,...,m, -1,-2,...,-m, 0]
    ///
    /// 此方法時間複雜度 O(n)，空間複雜度 O(n)。
    /// 範例: n = 5 -> [-2, -1, 0, 1, 2]
    /// </summary>
    /// <param name="n"></param>
    /// <returns></returns>
    /// <summary>
    /// 回傳一個包含 n 個互不相同整數且總和為 0 的陣列。
    /// 使用對稱構造：成對加入 k 與 -k；若 n 為奇數，加入 0。
    ///
    /// 注意（C# 特性）：在 C# 中使用 `new int[n]` 會將整個陣列元素初始化為整數的預設值 0，
    /// 因此當 n 為奇數且我們只填入前 2*m 個位置時，最後一個位置已經是 0，
    /// 不需要額外賦值才能保證總和為 0。保留顯式賦值只是為了提高可讀性與表達意圖。
    /// </summary>
    /// <param name="n">要回傳的整數數量，n &gt;= 0</param>
    /// <returns>長度為 n，總和為 0 的整數陣列</returns>
    public int[] SumZero(int n)
    {
        // 建立結果陣列
        int[] res = new int[n];

        // m 為對稱對數的數量（向下取整）
        int m = n / 2;

        // 對於 i=0..m-1，放入 i+1 與 -(i+1)
        // 當 n 為偶數時，這會填滿整個陣列
        // 當 n 為奇數時，最後一個元素保留給 0
        for (int i = 0; i < m; i++)
        {
            res[i] = i + 1; // 正數
            res[i + m] = -(i + 1); // 對應的負數
        }

    // 若 n 為奇數，最後一個元素應為 0。
    // 在 C# 中陣列以預設值初始化，因此下列顯式賦值是多餘的，但保留為註解以表達意圖：
    // if (n % 2 == 1)
    // {
    //     res[n - 1] = 0;
    // }

        return res;
    }
}
