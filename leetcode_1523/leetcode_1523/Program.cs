namespace leetcode_1523;

class Program
{
    /// <summary>
    /// 1523. Count Odd Numbers in an Interval Range
    /// https://leetcode.com/problems/count-odd-numbers-in-an-interval-range/description/?envType=daily-question&envId=2025-12-07
    /// 1523. 在區間範圍內統計奇數數目
    ///
    /// Description:
    /// Given two non-negative integers low and high. Return the count of odd numbers between low and high (inclusive).
    ///
    /// 繁體中文題目描述：
    /// 給定兩個非負整數 low 和 high，請回傳在區間 [low, high]（包含邊界）內的奇數數量。
    /// https://leetcode.cn/problems/count-odd-numbers-in-an-interval-range/description/?envType=daily-question&envId=2025-12-07
    /// 
    /// 
    /// </summary>
    /// <param name="args"></param> <summary>
    /// 
    /// </summary>
    /// <param name="args"></param>
    static void Main(string[] args)
    {

    }

    /// <summary>
    /// 計算區間 [low, high] 中奇數的數量。
    /// 解法：高於或等於 0 的整數，高的奇數計數為 (high + 1) / 2，低的奇數計數為 low / 2，兩者相減得到區間內奇數數量。
    /// </summary>
    /// <param name="low"></param>
    /// <param name="high"></param>
    /// <returns></returns>
    public int CountOdds(int low, int high)
    {
        return (high + 1) / 2 - (low / 2);
    }

    /// <summary>
    /// 計算區間 [low, high] 中奇數的數量。
    /// </summary>
    /// <param name="low"></param>
    /// <param name="high"></param>
    /// <returns></returns>
    public int CountOdds2(int low, int high)
    {
        int count = 0;
        for(int i = low; i <= high; i++)
        {
            if(i % 2 != 0)
            {
                count++;
            }
        }
        return count;
    }
}

