namespace leetcode_3477;

class Program
{
    /// <summary>
    /// 3477. Fruits Into Baskets II
    /// https://leetcode.com/problems/fruits-into-baskets-ii/description/?envType=daily-question&envId=2025-08-05
    /// 3477. 水果成籃II
    /// https://leetcode.cn/problems/fruits-into-baskets-ii/description/?envType=daily-question&envId=2025-08-05
    /// 
    /// 題目描述：
    /// 給定兩個整數陣列 fruits 和 baskets，長度皆為 n，fruits[i] 代表第 i 種水果的數量，baskets[j] 代表第 j 個籃子的容量。
    /// 
    /// 從左到右，依下列規則放置水果：
    /// 1. 每種水果必須放入容量大於等於該水果數量的最左邊可用籃子。
    /// 2. 每個籃子只能放一種水果。
    /// 3. 若某種水果無法放入任何籃子，則該水果無法被放置。
    /// 
    /// 請回傳所有無法被放置的水果種類數量。
    /// </summary>
    /// <param name="args"></param>
    static void Main(string[] args)
    {
        // 測試資料 1
        int[] fruits1 = { 2, 3, 5 };
        int[] baskets1 = { 3, 4, 2 };
        int res1 = new Program().NumOfUnplacedFruits((int[])fruits1.Clone(), (int[])baskets1.Clone());
        Console.WriteLine($"TestCase 1: 未放置水果種類數量 = {res1}");

        // 測試資料 2
        int[] fruits2 = { 1, 2, 3 };
        int[] baskets2 = { 1, 2, 3 };
        int res2 = new Program().NumOfUnplacedFruits((int[])fruits2.Clone(), (int[])baskets2.Clone());
        Console.WriteLine($"TestCase 2: 未放置水果種類數量 = {res2}");

        // 測試資料 3
        int[] fruits3 = { 4, 5, 6 };
        int[] baskets3 = { 1, 2, 3 };
        int res3 = new Program().NumOfUnplacedFruits((int[])fruits3.Clone(), (int[])baskets3.Clone());
        Console.WriteLine($"TestCase 3: 未放置水果種類數量 = {res3}");
    }

    /// <summary>
    /// 方法一：模擬
    /// 思路及解法：
    /// 由於本題數據量很小，直接枚舉每種水果，然後從左到右枚舉每個籃子，觀察當前水果是否有籃子可以裝下。
    /// 存在兩種情況：
    /// 1. 如果能找到裝得下當前水果的籃子（容量 >= 水果數量），則該籃子不能再用，容量設為 0。
    /// 2. 如果找不到裝得下的籃子，則計數器 count 加一。
    /// </summary>
    /// <param name="fruits">每種水果的數量陣列</param>
    /// <param name="baskets">每個籃子的容量陣列</param>
    /// <returns>無法被放置的水果種類數量</returns>
    public int NumOfUnplacedFruits(int[] fruits, int[] baskets)
    {
        int count = 0; // 記錄無法被放置的水果種類數量
        int n = baskets.Length;
        foreach (int fruit in fruits)
        {
            int unset = 1; // 標記當前水果是否能被放置，1 表示尚未放置
            for (int i = 0; i < n; i++)
            {
                // 如果當前籃子容量足夠放下該水果
                if (fruit <= baskets[i])
                {
                    baskets[i] = 0; // 該籃子已被佔用，容量設為 0
                    unset = 0; // 該水果已成功放置
                    break; // 跳出籃子循環，處理下一種水果
                }
            }
            count += unset; // 若未放置，計數器加一
        }
        return count;
    }
}
