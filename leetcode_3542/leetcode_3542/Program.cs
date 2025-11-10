namespace leetcode_3542;

class Program
{
    /// <summary>
    /// 3542. Minimum Operations to Convert All Elements to Zero
    /// https://leetcode.com/problems/minimum-operations-to-convert-all-elements-to-zero/description/?envType=daily-question&envId=2025-11-10
    /// 3542. 将所有元素变为 0 的最少操作次数
    /// https://leetcode.cn/problems/minimum-operations-to-convert-all-elements-to-zero/description/?envType=daily-question&envId=2025-11-10
    /// 
    /// 給定一個大小為 n 的陣列 nums，由非負整數組成。你的任務是對陣列應用一些（可能為零）操作，使所有元素變為 0。
    /// 
    /// 在一次操作中，你可以選擇一個子陣列 [i, j]（其中 0 <= i <= j < n），並將該子陣列中最小非負整數的所有出現設為 0。
    /// 
    /// 返回使陣列中所有元素變為 0 所需的最少操作數。
    /// </summary>
    /// <param name="args"></param> <summary>
    /// 
    /// </summary>
    /// <param name="args"></param>
    static void Main(string[] args)
    {
        Console.WriteLine("Hello, World!");
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="nums"></param>
    /// <returns></returns>
    public int MinOperations(int[] nums)
    {
        var s = new List<int>();
        int res = 0;
        foreach(int a in nums)
        {
            while (s.Count > 0 && s[s.Count - 1] > a)
            {
                s.RemoveAt(s.Count - 1);
            }
            if (a == 0)
            {
                continue;
            }
            if(s.Count == 0 || s[s.Count - 1] < a)
            {
                s.Add(a);
                res++;
            }
        }
        return res;
    }
}
