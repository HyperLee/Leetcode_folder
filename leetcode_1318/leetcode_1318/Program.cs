namespace leetcode_1318;

class Program
{
    /// <summary>
    /// 1318. Minimum Flips to Make a OR b Equal to c
    /// https://leetcode.com/problems/minimum-flips-to-make-a-or-b-equal-to-c/description/
    /// 1318. 或运算的最小翻转次数
    /// https://leetcode.cn/problems/minimum-flips-to-make-a-or-b-equal-to-c/description/
    ///
    /// [EN] Given 3 positives numbers a, b and c. Return the minimum flips required in some bits
    /// of a and b to make ( a OR b == c ). (bitwise OR operation).
    /// Flip operation consists of change any single bit 1 to 0 or change the bit 0 to 1
    /// in their binary representation.
    ///
    /// [繁中] 給定三個正整數 a、b 和 c。請回傳需要翻轉 a 和 b 中某些位元的最少次數，
    /// 使得 ( a OR b == c )（位元 OR 運算）。
    /// 翻轉操作是指將二進位表示中的某個單一位元由 1 改為 0，或由 0 改為 1。
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
    /// <param name="a"></param>
    /// <param name="b"></param>
    /// <param name="c"></param>
    /// <returns></returns>
    public int MinFlips(int a, int b, int c)
    {
        int flips = 0;
        while(a > 0 || b > 0 || c > 0)
        {
            int abit = a & 1;
            int bbit = b & 1;
            int cbit = c & 1;

            if((abit | bbit) != cbit)
            {
                if(cbit == 1)
                {
                    flips++;
                }
                else
                {
                    flips += abit + bbit;
                }
            }

             a >>= 1;
             b >>= 1;
             c >>= 1;
        }
        return flips;
    }
}
