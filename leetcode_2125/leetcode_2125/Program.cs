namespace leetcode_2125;

class Program
{
    /// <summary>
    /// 2125. Number of Laser Beams in a Bank
    /// https://leetcode.com/problems/number-of-laser-beams-in-a-bank/description/?envType=daily-question&envId=2025-10-27
    /// 2125. 銀行中的激光束數量
    /// https://leetcode.cn/problems/number-of-laser-beams-in-a-bank/description/?envType=daily-question&envId=2025-10-27
    /// 
    /// 銀行中的雷射束數量
    /// 
    /// 銀行內部安裝了防盜安全設備。你被給予一個 0 索引的二進位字串陣列 bank，表示銀行的平面圖，這是一個 m x n 的二維矩陣。
    /// bank[i] 表示第 i 行，由 '0' 和 '1' 組成。'0' 表示單元格為空，而 '1' 表示單元格安裝了安全設備。
    /// 
    /// 如果滿足以下兩個條件，則在兩個安全設備之間存在一條雷射束：
    /// 
    /// 兩個設備位於兩個不同的行：r1 和 r2，其中 r1 < r2。
    /// 對於 r1 < i < r2 的每一行 i，該行中都沒有安全設備。
    /// 
    /// 雷射束是獨立的，即一條雷射束不會干擾或與其他雷射束合併。
    /// 
    /// 返回銀行中雷射束的總數。
    /// 
    /// </summary>
    /// <param name="args"></param>
    static void Main(string[] args)
    {
        Program program = new Program();

        // 測試案例 1
        string[] bank1 = { "011001", "000000", "010100", "001000" };
        int result1 = program.NumberOfBeams(bank1);
        Console.WriteLine($"測試案例 1: {result1} (預期: 8)");

        // 測試案例 2
        string[] bank2 = { "000", "111", "000" };
        int result2 = program.NumberOfBeams(bank2);
        Console.WriteLine($"測試案例 2: {result2} (預期: 0)");

        // 測試案例 3
        string[] bank3 = { "1", "0", "1" };
        int result3 = program.NumberOfBeams(bank3);
        Console.WriteLine($"測試案例 3: {result3} (預期: 0)");
    }

    /// <summary>
    /// 計算銀行中雷射束的總數量
    /// 
    /// 解題思路：
    /// 1. 雷射束只會在「有安全設備的行」之間產生，中間不能有其他安全設備的行
    /// 2. 兩行之間的雷射束數量 = 第一行的設備數量 × 第二行的設備數量
    /// 3. 使用兩個變數追蹤：
    ///    - prevcount: 前一個有設備的行的設備數量
    ///    - currcount: 當前行的設備數量
    /// 4. 當找到有設備的行時，計算與前一行之間的雷射束數量
    /// 
    /// 時間複雜度：O(m × n)，m 為行數，n 為列數
    /// 空間複雜度：O(1)，只使用常數額外空間
    /// </summary>
    /// <param name="bank">表示銀行平面圖的二進位字串陣列，'1' 代表安全設備，'0' 代表空單元格</param>
    /// <returns>銀行中雷射束的總數</returns>
    public int NumberOfBeams(string[] bank)
    {
        // 雷射束總數
        int laser = 0;
        
        // 前一個有設備的行的設備數量
        int prevcount = 0;
        
        // m: 行數, n: 列數
        int m = bank.Length, n = bank[0].Length;

        // 遍歷每一行
        for(int i = 0; i < m; i++)
        {
            // 當前行的設備數量
            int currcount = 0;
            
            // 遍歷當前行的每一列，計算設備數量
            for(int j = 0; j < n; j++)
            {
                if (bank[i][j] == '1')
                {
                    // 找到安全設備，累計計數
                    currcount++;
                }
            }

            // 只有當當前行有設備時才計算雷射束
            if (currcount > 0)
            {
                // 計算當前行與前一個有設備的行之間的雷射束數量
                // 雷射束數量 = 前一行設備數 × 當前行設備數
                laser += prevcount * currcount;
                
                // 更新前一行設備數量為當前行，供下一次計算使用
                prevcount = currcount;
            }

        }

        return laser;
    }
}
