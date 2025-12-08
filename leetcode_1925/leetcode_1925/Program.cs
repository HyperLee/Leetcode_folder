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
        Console.WriteLine("Hello, World!");
    }

    /// <summary>
    /// 计算平方和三元组的数目
    /// </summary>
    /// <param name="n"></param>
    /// <returns></returns>
    public int CountTriples(int n)
    {
        int count = 0;
        for (int a = 1; a <= n; a++)
        {
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
        return count;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="n"></param>
    /// <returns></returns>
    public int CountTriples2(int n)
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
