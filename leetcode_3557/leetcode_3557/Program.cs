namespace leetcode_3557;

class Program
{
    /// <summary>
    /// 3577. Count the Number of Computer Unlocking Permutations
    /// https://leetcode.com/problems/count-the-number-of-computer-unlocking-permutations/description/?envType=daily-question&envId=2025-12-10
    /// 3577. 统计计算机解锁顺序排列数
    /// https://leetcode.cn/problems/count-the-number-of-computer-unlocking-permutations/description/?envType=daily-question&envId=2025-12-10
    /// 給定一個長度為 n 的整數陣列 complexity。
    ///
    /// 房間裡有 n 台已鎖定的電腦，標號從 0 到 n - 1，且每台電腦各有唯一的密碼。編號為 i 的電腦之密碼複雜度為 complexity[i]。
    ///
    /// 標號為 0 的電腦的密碼已經解密，作為根節點。其他所有電腦必須使用它或其他先前已解鎖的電腦來解鎖，依照以下規則：
    ///
    /// - 你可以使用電腦 j 的密碼來解密電腦 i 的密碼，當且僅當 j < i 且 complexity[j] < complexity[i]（也就是 j 必須小於 i，且複雜度更低）。
    /// - 要解鎖電腦 i，你必須已經解鎖某台 j，滿足 j < i 且 complexity[j] < complexity[i]。
    ///
    /// 求有多少種排列 [0, 1, 2, ..., n - 1] 表示有效的解鎖順序，且從只有電腦 0 為已解鎖開始。
    ///
    /// 由於答案可能很大，請將結果對 10^9 + 7 取模。
    ///
    /// 注意：編號為 0 的電腦的密碼是已解密的，而不是排列中第一個位置的電腦。
    /// </summary>
    /// <param name="args"></param>
    static void Main(string[] args)
    {
        Console.WriteLine("Hello, World!");
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="complexity"></param>
    /// <returns></returns>
    public int CountPermutations(int[] complexity)
    {
        int n = complexity.Length;
        long res = 1;
        const int MOD = 1_000_000_007;
        for(int i = 1; i < n; i++)
        {
            if(complexity[i] <= complexity[0])
            {
                return 0;
            }
            res = res * i % MOD;
        }

        return (int)res;
    }
}
