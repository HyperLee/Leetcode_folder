namespace leetcode_1925;

class Program
{
    /// <summary>
    /// 1925. Count Square Sum Triples
    /// https://leetcode.com/problems/count-square-sum-triples/
    /// 1925. 统计平方和三元组的数目 (中文繁體翻譯下方)
    /// https://leetcode.cn/problems/count-square-sum-triples/
    /// 
    /// 一個平方和三元組 (a, b, c) 是指三個整數 a、b、c 滿足 a^2 + b^2 = c^2。
    /// 給定整數 n，請回傳所有滿足 1 <= a, b, c <= n 的平方和三元組的數目。
    /// 
    /// </summary>
    /// <param name="args"></param>
    static void Main(string[] args)
    {
        // 測試範例：輸入 n，我們會列印出兩種方法的結果供比較
        int[] testCases = new int[] { 5, 10, 25 };
        foreach (int n in testCases)
        {
            Console.WriteLine($"n = {n}");
            Console.WriteLine($"CountTriples (優化版, 有序計數): {CountTriples(n)}");
            Console.WriteLine($"CountTriples2 (暴力法, 有序計數): {CountTriples2(n)}");
            Console.WriteLine();
        }
    }

    /// <summary>
    /// 計算平方和三元組的數目（優化版）
    /// 
    /// 解題說明：
    /// 這個方法透過對 a 與 b 進行雙迴圈，計算 c^2 = a^2 + b^2，接著檢查 c 是否為整數且 c <= n
    /// - 為避免重複計數 (a,b) 與 (b,a) 被視為相同組合，我們從 b = a 開始（a <= b），這樣可確保每組 (a,b,c) 只被計數一次。
    /// - 若題目要求計算有序組合（(a,b) 與 (b,a) 視為不同），則只需讓 b 從 1 到 n。
    /// 
    /// 時間複雜度：O(n^2)。
    /// 空間複雜度：O(1)。
    /// </summary>
    /// <param name="n">上限值 n（1 <= a,b,c <= n）</param>
    /// <returns>滿足 a^2 + b^2 = c^2 的三元組數目（視為有序 (a,b) 與 (b,a) 為不同；也就是 LeetCode 題目所要求的有序計數）</returns>
    public static int CountTriples(int n)
    {
        int count = 0;
        for (int a = 1; a <= n; a++)
        {
            // 由於 a 與 b 可互換，從 b = a 開始以避免重複，縮小搜尋範圍
            for (int b = a; b <= n; b++)
            {
                int cSquared = a * a + b * b;
                int c = (int)Math.Sqrt(cSquared);
                if (c * c == cSquared && c <= n)
                {
                    count++;
                }
            }
        }
        // 由於迴圈中我們僅遍歷 a <= b 以避免重複，因此適用於無序計數
        // 但題目要求計算有序三元組 (a,b,c) 且 (a,b) 與 (b,a) 視為不同，
        // 因此我們可以把無序計數乘以 2 來得到有序計數（因為不會有 a == b 時 a^2 + b^2 = c^2 的情況）。
        return count * 2;
    }

    /// <summary>
    /// 計算平方和三元組的數目（暴力窮舉）
    /// 
    /// 解題說明：
    /// 這個方法使用三重迴圈分別遍歷 a、b、c，檢查是否滿足 a^2 + b^2 = c^2。
    /// - 這種方法直觀但效率低，時間複雜度為 O(n^3)，僅適用於 n 較小時的驗證或教學用途。
    /// 
    /// 時間複雜度：O(n^3)。
    /// 空間複雜度：O(1)。
    /// </summary>
    /// <param name="n">上限值 n（1 <= a,b,c <= n）</param>
    /// <returns>滿足 a^2 + b^2 = c^2 的三元組數目（若 a 與 b 視為有序則會包含排列）</returns>
    public static int CountTriples2(int n)
    {
        int res = 0;
        for(int a = 1; a <= n; a++)
        {
            for(int b = 1; b <= n; b++)
            {
                for(int c = 1; c <= n; c++)
                {
                    if(a * a + b * b == c * c)
                    {
                        res++;
                    }
                }
            }
        }
        return res;
    }
}
