namespace leetcode_771;

class Program
{
    /// <summary>
    /// 771. Jewels and Stones
    /// https://leetcode.com/problems/jewels-and-stones/description/
    /// 771. 寶石與石頭
    /// https://leetcode.cn/problems/jewels-and-stones/description/
    ///
    /// You're given strings jewels representing the types of stones that are jewels,
    /// and stones representing the stones you have. Each character in stones is a type
    /// of stone you have. You want to know how many of the stones you have are also jewels.
    /// Letters are case sensitive, so "a" is considered a different type of stone from "A".
    ///
    /// 給你一個字串 jewels，代表寶石的種類；另一個字串 stones，代表你擁有的石頭。
    /// stones 中的每個字元代表你擁有的一種石頭。
    /// 你想知道你擁有的石頭中，有多少顆也是寶石。
    /// 字母區分大小寫，因此 "a" 與 "A" 被視為不同種類的石頭。
    /// </summary>
    /// <param name="args"></param>
    static void Main(string[] args)
    {
        Program program = new Program();

        // 測試範例 1: jewels = "aA", stones = "aAAbbbb" => 預期輸出 3
        string jewels1 = "aA";
        string stones1 = "aAAbbbb";
        Console.WriteLine($"範例 1: jewels = \"{jewels1}\", stones = \"{stones1}\"");
        Console.WriteLine($"  方法一 (Contains):  {program.NumJewelsInStones(jewels1, stones1)}");
        Console.WriteLine($"  方法二 (暴力法):    {program.NumJewelsInStones2(jewels1, stones1)}");
        Console.WriteLine($"  方法三 (HashSet):   {program.NumJewelsInStones3(jewels1, stones1)}");

        // 測試範例 2: jewels = "z", stones = "ZZ" => 預期輸出 0 (大小寫不同)
        string jewels2 = "z";
        string stones2 = "ZZ";
        Console.WriteLine($"\n範例 2: jewels = \"{jewels2}\", stones = \"{stones2}\"");
        Console.WriteLine($"  方法一 (Contains):  {program.NumJewelsInStones(jewels2, stones2)}");
        Console.WriteLine($"  方法二 (暴力法):    {program.NumJewelsInStones2(jewels2, stones2)}");
        Console.WriteLine($"  方法三 (HashSet):   {program.NumJewelsInStones3(jewels2, stones2)}");

        // 測試範例 3: jewels = "abc", stones = "aabbccdd" => 預期輸出 6
        string jewels3 = "abc";
        string stones3 = "aabbccdd";
        Console.WriteLine($"\n範例 3: jewels = \"{jewels3}\", stones = \"{stones3}\"");
        Console.WriteLine($"  方法一 (Contains):  {program.NumJewelsInStones(jewels3, stones3)}");
        Console.WriteLine($"  方法二 (暴力法):    {program.NumJewelsInStones2(jewels3, stones3)}");
        Console.WriteLine($"  方法三 (HashSet):   {program.NumJewelsInStones3(jewels3, stones3)}");
    }

    /// <summary>
    /// 方法一：使用 string.Contains 方法
    /// 
    /// 解題思路：
    /// 遍歷 stones 中的每個字元，利用 string.Contains() 判斷該字元是否存在於 jewels 中。
    /// 若存在則計數器加一，最終回傳計數器的值。
    ///
    /// 時間複雜度：O(m * n)，其中 m 為 stones 長度，n 為 jewels 長度。
    /// 空間複雜度：O(1)，不需要額外空間。
    /// </summary>
    /// <param name="jewels">寶石種類字串，每個字元代表一種寶石</param>
    /// <param name="stones">擁有的石頭字串，每個字元代表一顆石頭</param>
    /// <returns>stones 中屬於寶石的數量</returns>
    public int NumJewelsInStones(string jewels, string stones)
    {
        int cnt = 0;

        // 遍歷每一顆石頭
        for(int i = 0; i < stones.Length; i++)
        {
            // 利用 string.Contains() 檢查該石頭是否為寶石
            if(jewels.Contains(stones[i]))
            {
                cnt++;
            }
        }

        return cnt;
    }

    /// <summary>
    /// 方法二：暴力法（雙層迴圈）
    ///
    /// 解題思路：
    /// 使用雙層 for 迴圈，外層遍歷 stones 的每個字元，內層遍歷 jewels 的每個字元。
    /// 若外層的石頭字元與內層的寶石字元相同，計數器加一並跳出內層迴圈（避免重複計算）。
    ///
    /// 時間複雜度：O(m * n)，其中 m 為 stones 長度，n 為 jewels 長度。
    /// 空間複雜度：O(1)，不需要額外空間。
    /// </summary>
    /// <param name="jewels">寶石種類字串，每個字元代表一種寶石</param>
    /// <param name="stones">擁有的石頭字串，每個字元代表一顆石頭</param>
    /// <returns>stones 中屬於寶石的數量</returns>
    public int NumJewelsInStones2(string jewels, string stones)
    {
        int jewelsCount = 0;
        int jewelsLength = jewels.Length;
        int stonesLength = stones.Length;

        // 外層迴圈：遍歷每一顆石頭
        for(int i = 0; i < stonesLength; i++)
        {
            char stone = stones[i];

            // 內層迴圈：逐一比對每種寶石
            for(int j = 0; j < jewelsLength; j++)
            {
                char jewel = jewels[j];

                // 若石頭與寶石相同，計數加一並跳出內層迴圈
                if(stone == jewel)
                {
                    jewelsCount++;
                    break;
                }
            }
        }

        return jewelsCount;
    }

    /// <summary>
    /// 方法三：HashSet 雜湊集合
    ///
    /// 解題思路：
    /// 方法二中，每顆石頭都要遍歷一次 jewels，導致時間複雜度較高。
    /// 改用 HashSet 儲存 jewels 中的所有寶石字元，將查詢時間從 O(n) 降為 O(1)。
    /// 先遍歷 jewels 將每個字元加入 HashSet，
    /// 再遍歷 stones，若該字元存在於 HashSet 中，則為寶石，計數器加一。
    ///
    /// 時間複雜度：O(m + n)，其中 m 為 stones 長度，n 為 jewels 長度。
    /// 空間複雜度：O(n)，需要 HashSet 儲存 jewels 中的字元。
    /// </summary>
    /// <param name="jewels">寶石種類字串，每個字元代表一種寶石</param>
    /// <param name="stones">擁有的石頭字串，每個字元代表一顆石頭</param>
    /// <returns>stones 中屬於寶石的數量</returns>
    public int NumJewelsInStones3(string jewels, string stones)
    {
        int jewelsCount = 0;
        HashSet<char> jewelsSet = new HashSet<char>();
        int jewelsLength = jewels.Length;
        int stonesLength = stones.Length;

        // 將所有寶石字元加入 HashSet，以便 O(1) 查詢
        for (int i = 0; i < jewelsLength; i++)
        {
            jewelsSet.Add(jewels[i]);
        }

        // 遍歷每一顆石頭，透過 HashSet 快速判斷是否為寶石
        for (int i = 0; i < stonesLength; i++)
        {
            if (jewelsSet.Contains(stones[i]))
            {
                jewelsCount++;
            }
        }

        return jewelsCount;
    }
}
