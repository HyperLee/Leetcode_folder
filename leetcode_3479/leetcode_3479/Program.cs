using System.Net.Mail;

namespace leetcode_3479;

class Program
{
    /// <summary>
    /// 3479. Fruits Into Baskets III
    /// https://leetcode.com/problems/fruits-into-baskets-iii/description/?envType=daily-question&envId=2025-08-06
    /// 3479. 水果成籃III
    /// https://leetcode.cn/problems/fruits-into-baskets-iii/description/?envType=daily-question&envId=2025-08-06
    /// 
    /// 題目描述：
    /// 給定兩個整數陣列 fruits 和 baskets，長度皆為 n，其中 fruits[i] 代表第 i 種水果的數量，baskets[j] 代表第 j 個籃子的容量。
    /// 請依照以下規則，從左到右將水果放入籃子：
    /// 1. 每種水果必須放入容量大於等於該水果數量的最左邊可用籃子。
    /// 2. 每個籃子只能放一種水果。
    /// 3. 若某種水果無法放入任何籃子，則該水果無法放置。
    /// 請回傳所有無法放置的水果種類數量。
    /// 
    /// </summary>
    /// <param name="args"></param>
    static void Main(string[] args)
    {
        // 測試資料
        int[] fruits = {2, 3, 5};
        int[] baskets = {3, 4, 5};
        var program = new Program();
        int result = program.NumOfUnplacedFruits(fruits, baskets);
        Console.WriteLine($"無法放置的水果種類數量: {result}");
    }

    /// <summary>
    /// 計算無法放置的水果種類數量。
    /// 
    /// 解題思路：
    /// 1. 將 baskets 以分塊（每塊大小 m = sqrt(n)）方式分組，並維護每塊的最大容量 maxValues。
    /// 2. 對每種水果，遍歷每個分塊，若該塊最大值 < 水果數量則跳過，否則在該塊內尋找最左邊可用籃子。
    /// 3. 若找到可用籃子則將其容量設為 0（表示已被佔用），並更新該塊最大值。
    /// 4. 若所有分塊都無法放置，則計數器 count 加一。
    /// 
    /// <example>
    /// <code>
    /// int[] fruits = {2, 3, 5};
    /// int[] baskets = {3, 4, 5};
    /// int result = NumOfUnplacedFruits(fruits, baskets); // result = 0
    /// </code>
    /// </example>
    /// </summary>
    /// <param name="fruits">每種水果的數量</param>
    /// <param name="baskets">每個籃子的容量</param>
    /// <returns>無法放置的水果種類數量</returns>
    public int NumOfUnplacedFruits(int[] fruits, int[] baskets)
    {
        // n 為籃子數量
        int n = baskets.Length;
        // m 為分塊大小，取 sqrt(n)
        int m = (int)Math.Sqrt(n);
        // section 為分塊數量
        int section = (n + m - 1) / m;
        int count = 0;
        // maxValues[i] 表示第 i 塊的最大容量
        int[] maxValues = new int[section];
        Array.Fill(maxValues, 0);

        // 初始化每塊的最大值
        for (int i = 0; i < n; i++)
        {
            maxValues[i / m] = Math.Max(maxValues[i / m], baskets[i]);
        }

        // 枚舉每種水果
        foreach (int fruit in fruits)
        {
            int unset = 1; // 標記該水果是否無法放置
            // 遍歷每個分塊
            for (int sec = 0; sec < section; sec++)
            {
                // 若該塊最大值 < 水果數量，跳過
                if (maxValues[sec] < fruit)
                {
                    continue;
                }
                int choose = 0; // 標記是否已找到可用籃子
                maxValues[sec] = 0; // 重算該塊最大值
                // 在該塊內尋找最左邊可用籃子
                for (int i = 0; i < m; i++)
                {
                    // 計算分塊內的實際籃子索引。
                    int pos = sec * m + i;
                    // pos 確保不超出籃子陣列範圍, 
                    // 找到第一個容量足夠且尚未被佔用的籃子。
                    if (pos < n && baskets[pos] >= fruit && choose == 0)
                    {
                        baskets[pos] = 0; // 該籃子已被佔用
                        choose = 1; // 只選一次，後續不再選。
                    }
                    // 更新該塊最大值
                    if (pos < n)
                    {
                        maxValues[sec] = Math.Max(maxValues[sec], baskets[pos]);
                    }
                }
                unset = 0; // 該水果已成功放置
                break;
            }
            count += unset; // 若無法放置則計數
        }
        return count;
    }
}
