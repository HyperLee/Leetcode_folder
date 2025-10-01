namespace leetcode_1518;

class Program
{
    /// <summary>
    /// 1518. Water Bottles
    /// https://leetcode.com/problems/water-bottles/description/?envType=daily-question&envId=2025-10-01
    /// 1518. 换水问题
    /// https://leetcode.cn/problems/water-bottles/description/?envType=daily-question&envId=2025-10-01
    /// 
    /// 有 numBottles 瓶水，最初都是滿的。你可以用 numExchange 個空瓶從市場換一瓶滿的水。喝一瓶滿的水會變成空瓶。給定兩個整數 numBottles 和 numExchange，返回你可以喝的最大水瓶數。
    /// </summary>
    /// <param name="args"></param>
    static void Main(string[] args)
    {
        Console.WriteLine("Hello, World!");
    }

    /// <summary>
    /// 模擬解法, 依據題目說明來操作    
    /// 
    /// </summary>
    /// <param name="numBottles"></param>
    /// <param name="numExchange"></param>
    /// <returns></returns>
    public int NumWaterBottles(int numBottles, int numExchange)
    {
        int bottle = numBottles;
        int res = numBottles;
        while (bottle >= numExchange)
        {
            bottle -= numExchange;
            res++;
            bottle++;
        }
        return res;
    }
}
