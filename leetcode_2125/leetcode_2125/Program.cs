namespace leetcode_2125;

class Program
{
    /// <summary>
    /// <para>
    /// 2125. Number of Laser Beams in a Bank
    /// https://leetcode.com/problems/number-of-laser-beams-in-a-bank/description/
    ///
    /// Security devices are active in a bank. A 0-indexed binary string array bank represents an m x n floor plan: bank[i] is row i, '0' is empty, and '1' contains a device.
    ///
    /// There is one laser beam between two devices when they are on different rows r1 and r2 with r1 &lt; r2, and every row i with r1 &lt; i &lt; r2 contains no devices. Beams are independent and do not interfere or join. Return the total number of beams.
    ///
    /// Images: https://assets.leetcode.com/uploads/2021/12/24/laser1.jpg and https://assets.leetcode.com/uploads/2021/12/24/laser2.jpg
    ///
    /// Example 1:
    /// Input: bank = ["011001","000000","010100","001000"]
    /// Output: 8
    /// Explanation: The 8 beams connect bank[0][1] to bank[2][1], bank[0][1] to bank[2][3], bank[0][2] to bank[2][1], bank[0][2] to bank[2][3], bank[0][5] to bank[2][1], bank[0][5] to bank[2][3], bank[2][1] to bank[3][2], and bank[2][3] to bank[3][2]. No beam connects row 0 to row 3 because row 2 contains devices.
    ///
    /// Example 2:
    /// Input: bank = ["000","111","000"]
    /// Output: 0
    /// Explanation: No two devices are on two different rows.
    ///
    /// Constraints:
    /// - m == bank.length
    /// - n == bank[i].length
    /// - 1 &lt;= m, n &lt;= 500
    /// - bank[i][j] is '0' or '1'.
    /// </para>
    /// <para>
    /// 2125. 銀行中的雷射光束數量
    /// https://leetcode.cn/problems/number-of-laser-beams-in-a-bank/description/
    ///
    /// 銀行中的防盜裝置已啟用。從 0 開始索引的二元字串陣列 bank 表示 m x n 的平面圖：bank[i] 是第 i 列，'0' 表示空格，'1' 表示有裝置。
    ///
    /// 若兩個裝置位於不同列 r1、r2 且 r1 &lt; r2，並且每個滿足 r1 &lt; i &lt; r2 的列 i 都沒有裝置，兩者之間就有一條雷射光束。各光束互相獨立，不會干擾或合併。回傳光束總數。
    ///
    /// 圖片：https://assets.leetcode.com/uploads/2021/12/24/laser1.jpg 與 https://assets.leetcode.com/uploads/2021/12/24/laser2.jpg
    ///
    /// 範例 1：
    /// 輸入：bank = ["011001","000000","010100","001000"]
    /// 輸出：8
    /// 說明：8 條光束分別連接 bank[0][1] 與 bank[2][1]、bank[0][1] 與 bank[2][3]、bank[0][2] 與 bank[2][1]、bank[0][2] 與 bank[2][3]、bank[0][5] 與 bank[2][1]、bank[0][5] 與 bank[2][3]、bank[2][1] 與 bank[3][2]、bank[2][3] 與 bank[3][2]。第 0 列與第 3 列之間沒有光束，因為第 2 列有裝置。
    ///
    /// 範例 2：
    /// 輸入：bank = ["000","111","000"]
    /// 輸出：0
    /// 說明：沒有兩個裝置位於兩個不同列。
    ///
    /// 限制條件：
    /// - m == bank.length
    /// - n == bank[i].length
    /// - 1 &lt;= m, n &lt;= 500
    /// - bank[i][j] 是 '0' 或 '1'。
    /// </para>
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
