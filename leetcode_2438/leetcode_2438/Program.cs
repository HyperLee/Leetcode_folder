namespace leetcode_2438;

class Program
{
    /// <summary>
    /// 2438. Range Product Queries of Powers
    /// https://leetcode.com/problems/range-product-queries-of-powers/description/?envType=daily-question&envId=2025-08-11
    /// 2438. 二的幂数组中查询範圍內的乘積
    /// https://leetcode.cn/problems/range-product-queries-of-powers/description/?envType=daily-question&envId=2025-08-11
    /// 
    /// # 題目描述
    /// 給定一個正整數 n，存在一個 0 索引的陣列 powers，由最少數量的 2 的冪次方組成，且這些數字的總和為 n。powers 陣列已排序（非遞減），且只有一種方式可以組成。
    /// 
    /// 你還會得到一個 0 索引的二維整數陣列 queries，其中 queries[i] = [lefti, righti]。每個 queries[i] 代表一個查詢，你需要找出所有 powers[j]，其中 lefti <= j <= righti 的乘積。
    /// 
    /// 請回傳一個長度與 queries 相同的陣列 answers，其中 answers[i] 是第 i 個查詢的答案。由於答案可能過大，請對每個答案取模 10^9 + 7。
    /// </summary>
    /// <param name="args"></param>
    static void Main(string[] args)
    {
        // 測試資料
        int n = 15; // 二進位 1111, powers = [1,2,4,8]
        int[][] queries = new int[][]
        {
            new int[] {1, 2}, // 2*4=8
            new int[] {0, 3}, // 1*2*4*8=64
            new int[] {2, 2}  // 4
        };

        var prog = new Program();
        int[] ans = prog.ProductQueries(n, queries);
        Console.WriteLine("[" + string.Join(", ", ans) + "]");
        // 預期輸出: [8, 64, 4]
    }

    private const int MOD = 1000000007;

    /// <summary>
    /// 方法一：直接遍歷計算。
    /// 解題說明：
    /// 將 n 以二進位分解，若第 k 位為 1，則分解陣列包含 2^k。
    /// 例如 n=11，二進位為 1011，分解為 [1,2,8]。
    /// 對於每個查詢 [left, right]，直接遍歷分解陣列計算乘積。
    /// 單次查詢 O(log n)，總查詢數不超過 435。
    /// 
    /// 步驟1: 只要 n 的某一位是 1，就把那一位代表的 2 的冪次方（例如 2^0, 2^1, 2^2, ...）加入 bins。
    /// bins 最後就是所有「成立」的 2 的冪次方集合。
    ///
    /// 步驟2: 對於每個查詢 [left, right]，直接遍歷分解陣列計算乘積。
    /// 這段就是針對每個查詢 [left, right]，計算 powers 陣列（即 bins）中從 left 到 right 的所有元素的乘積，
    /// 並對 10^9+7 取餘，最後回傳所有查詢的答案。
    /// </summary>
    /// <param name="n">要分解的正整數</param>
    /// <param name="queries">查詢區間陣列，每個元素為 [left, right]</param>
    /// <returns>每個查詢對應的乘積結果，取模 10^9+7</returns>
    public int[] ProductQueries(int n, int[][] queries)
    {
        // 將 n 以二進位分解，bins 儲存所有 2 的冪次方
        var bins = new List<int>();
        int rep = 1;
        while (n > 0)
        {
            // 若最低位為 1，加入對應的 2 的冪
            if (n % 2 == 1)
            {
                bins.Add(rep);
            }
            n /= 2;
            rep *= 2;
        }

        int[] res = new int[queries.Length];
        // 針對每個查詢，計算指定區間的乘積
        for (int i = 0; i < queries.Length; i++)
        {
            long cur = 1;
            int start = queries[i][0];
            int end = queries[i][1];
            for (int j = start; j <= end; j++)
            {
                // 乘上 bins[j]，並對 MOD 取餘
                cur = (cur * bins[j]) % MOD;
            }
            res[i] = (int)cur;
        }
        return res;
    }
}
