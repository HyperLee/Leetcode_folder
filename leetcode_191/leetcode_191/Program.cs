using System.Numerics;

namespace leetcode_191;

class Program
{
    /// <summary>
    /// 191. Number of 1 Bits
    /// https://leetcode.com/problems/number-of-1-bits/description/
    /// 191. 位1的个数
    /// https://leetcode.cn/problems/number-of-1-bits/description/
    ///
    /// Given a positive integer n, write a function that returns the number of set bits in its binary representation (also known as the Hamming weight).
    /// 給定一個正整數 n，請撰寫一個函式，回傳它在二進位表示中 1 的個數（也稱為 Hamming weight）。
    /// </summary>
    /// <param name="args"></param>
    static void Main(string[] args)
    {
        Console.WriteLine("Hello, World!");
    }

    /// <summary>
    /// 解法一: 循环检查二进制位
    /// 檢查第 i 位置時候
    /// 可以讓 n 與 2^i 進行運算比對
    /// 只要第 i 位置為 1 結果不為 0
    /// 就代表第 i 位置不為空
    /// 
    /// 1 << i
    /// => 由左往右看
    /// 左移幾次
    /// 1 << 1 => 2,  2^1
    /// 1 << 2 => 4,  2^2
    /// 1 << 3 = >8,  2^3
    /// 前面 1 代表是 2 的次方
    /// 後面 數字 代表是 幾次方
    /// 
    /// uint 是 32 位元.
    /// </summary>
    /// <param name="n"></param>
    /// <returns></returns>
    public int HammingWeight(int n)
    {
        int res = 0;
        for(int i = 0; i < 32; i++)
        {
            // 判斷 i 位置是不是 1
            if((n & (1 << i)) != 0)
            {
                res++;
            }
        }
        return res;
    }

    /// <summary>
    /// 解法二: 逐位判断
    /// 根据 与运算 定义，设二进制数字 n ，则有：
    /// 若 n&1=0 ，则 n 二进制 最右一位 为 0 。
    /// 若 n&1=1 ，则 n 二进制 最右一位 为 1 。
    /// 根据以上特点，考虑以下 循环判断 ：
    /// 判断 n 最右一位是否为 1 ，根据结果计数。
    /// 将 n 右移一位（本题要求把数字 n 看作无符号数，因此使用 无符号右移 操作）。
    /// 
    ///
    /// 算法流程：
    /// 1. 初始化数量统计变量 res=0 。
    /// 2. 循环逐位判断： 当 n=0 时跳出。
    /// - res += n & 1 ： 若 n&1=1 ，则统计数 res 加一。
    /// - n >>= 1 ： 将二进制数字 n 无符号右移一位。
    /// 3. 返回统计数量 res 。
    /// </summary>
    /// <param name="n"></param>
    /// <returns></returns>
    public int HammingWeight2(int n)
    {
        int res = 0;
        while(n != 0)
        {
            res += n & 1;
            n >>= 1;
        }
        return res;
    }

    /// <summary>
    /// 解法三:巧用 n&(n−1)
    /// (n−1) 作用： 二进制数字 n 最右边的 1 变成 0 ，此 1 右边的 0 都变成 1 。
    /// n&(n−1) 作用： 二进制数字 n 最右边的 1 变成 0 ，其余不变。
    /// 
    /// 算法流程：
    /// 1. 初始化数量统计变量 res 。
    /// 2. 循环消去最右边的 1 ：当 n=0 时跳出。
    /// - res += 1 ： 统计变量加 1 。
    /// - n &= n - 1 ： 消去数字 n 最右边的 1 。
    /// 3. 返回统计数量 res 。
    /// </summary>
    /// <param name="n"></param>
    /// <returns></returns>
    public int HammingWeight3(int n)
    {
        int res = 0;
        while(n != 0)
        {
            res++;
            n &= n - 1;
        }
        return res;
    }
}
