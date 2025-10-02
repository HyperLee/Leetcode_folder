namespace leetcode_3100;

class Program
{
    /// <summary>
    /// 3100. Water Bottles II
    /// https://leetcode.com/problems/water-bottles-ii/description/?envType=daily-question&envId=2025-10-02
    /// 3100. 换水问题 II
    /// https://leetcode.cn/problems/water-bottles-ii/description/?envType=daily-question&envId=2025-10-02
    /// 
    /// 給你兩個整數 numBottles 和 numExchange。
    /// numBottles 表示你最初擁有的滿水瓶數量。
    /// 在一次操作中，你可以執行以下操作之一：
    ///     喝任意數量的滿水瓶，將它們變成空瓶。
    ///     用 numExchange 個空瓶交換一個滿水瓶。然後，將 numExchange 增加一。
    /// 注意，你不能在同一個 numExchange 值下交換多批空瓶。例如，如果 numBottles == 3 且 numExchange == 1，你不能用 3 個空瓶交換 3 個滿瓶。
    /// 返回你可以喝的最大水瓶數量。
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
    /// <param name="numBottles"></param>
    /// <param name="numExchange"></param>
    /// <returns></returns>
    public int MaxBottlesDrunk(int numBottles, int numExchange)
    {
        // 當前手上的空瓶數（開始時喝完初始滿瓶會形成空瓶）
        int empty = 0;
        // 總共喝到的瓶數
        int totalDrank = 0;

        // 先喝掉初始的滿瓶
        totalDrank += numBottles;
        empty += numBottles;

        // 當空瓶數夠換新的一瓶時，持續兌換並喝
        while (empty >= numExchange)
        {
            // 用空瓶兌換到的新瓶數
            int newFull = empty / numExchange;
            // 更新總喝瓶數
            totalDrank += newFull;
            // 兌換後剩下的空瓶數（餘數）加上新喝完會得到的空瓶
            empty = (empty % numExchange) + newFull;
            
            numExchange++; // 每次兌換後，numExchange 增加 1
        }

        return totalDrank;
    }
}
