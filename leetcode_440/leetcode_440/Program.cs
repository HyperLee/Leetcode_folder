namespace leetcode_440;

class Program
{
    /// <summary>
    /// 440. K-th Smallest in Lexicographical Order
    /// https://leetcode.com/problems/k-th-smallest-in-lexicographical-order/description/?envType=daily-question&envId=2025-06-09
    /// 440. 字典序的第K小数字
    /// https://leetcode.cn/problems/k-th-smallest-in-lexicographical-order/description/?envType=daily-question&envId=2025-06-09
    /// 
    /// 給定兩個整數 n 和 k，返回範圍 [1, n] 中字典序第 k 小的整數。
    /// 
    /// </summary>
    /// <param name="args"></param>
    static void Main(string[] args)
    {
        // 測試資料
        Program p = new Program();
        Console.WriteLine(p.FindKthNumber(13, 2)); // 輸出: 10
        Console.WriteLine(p.FindKthNumber(100, 10)); // 輸出: 17
        Console.WriteLine(p.FindKthNumber(1000, 100)); // 輸出: 117
    }


    /// <summary>
    /// 找到字典序第 k 小的數字
    /// 解題說明：
    /// 這個方法利用字典樹（Trie）結構的遍歷思想，計算每個節點（數字）下有多少個數字，
    /// 根據 k 的大小決定往下（進入子節點）或往右（同層下一個節點）移動，直到找到第 k 小的數字。
    /// 時間複雜度：O(log n * log n)
    /// </summary>
    /// <param name="n">最大數字</param>
    /// <param name="k">第 k 小</param>
    /// <returns>字典序第 k 小的數字</returns>
    public int FindKthNumber(int n, int k)
    {
        int curr = 1; // 從1開始，因為字典序第一個數字是1
        k--; // 因為已經在1了，所以k先減1

        while (k > 0)
        {
            int steps = GetSteps(curr, n); // 計算以curr為根的子樹有多少個數字
            if (steps <= k)
            {
                curr++; // 如果步數小於等於k，則移動到同層下一個節點
                k -= steps; // 減去這棵子樹的所有節點數
            }
            else
            {
                curr *= 10; // 否則進入下一層（進入子節點）
                k--; // 減去一個節點
            }
        }

        return curr;
    }


    /// <summary>
    /// 計算以 curr 為根的子樹，在 [1, n] 範圍內有多少個數字
    /// 解題說明：
    /// 這個方法模擬字典樹每一層的節點數，逐層累加，直到超過 n 為止。
    /// </summary>
    /// <param name="curr">目前的根節點</param>
    /// <param name="n">最大數字</param>
    /// <returns>以 curr 為根的子樹節點數</returns>
    public int GetSteps(int curr, int n)
    {
        int steps = 0;
        long first = curr; // 當前層的起始數字
        long last = curr + 1; // 下一個兄弟節點的起始數字
        while (first <= n)
        {
            // 累加這一層的節點數，不能超過 n
            steps += (int)Math.Min(n + 1, last) - (int)first;
            first *= 10; // 進入下一層
            last *= 10;
        }
        return steps;
    }
}
