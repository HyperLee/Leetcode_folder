using System.Text;

namespace leetcode_1404;

class Program
{
    /// <summary>
    /// 1404. Number of Steps to Reduce a Number in Binary Representation to One
    /// https://leetcode.com/problems/number-of-steps-to-reduce-a-number-in-binary-representation-to-one/description/?envType=daily-question&envId=2026-02-26
    /// 1404. 將二進位表示減到1 的步驟數
    /// https://leetcode.cn/problems/number-of-steps-to-reduce-a-number-in-binary-representation-to-one/description/?envType=daily-question&envId=2026-02-26
    ///
    /// 題目描述：
    /// 給定一個整數的二進位表示字串 s，根據以下規則計算將其變為 1 所需的步驟數：
    /// 1. 若當前數為偶數，必須除以 2。
    /// 2. 若當前數為奇數，必須加 1。
    /// 保證對於所有測試案例都能達到 1。
    ///
    /// Problem description:
    /// Given the binary representation of an integer as a string s, return the number of steps
    /// to reduce it to 1 under the following rules:
    /// - If the current number is even, you have to divide it by 2.
    /// - If the current number is odd, you have to add 1 to it.
    /// It is guaranteed that you can always reach one for all test cases.
    ///
    /// </summary>
    /// <param name="args"></param> <summary>
    /// 
    /// </summary>
    /// <param name="args"></param>
    static void Main(string[] args)
    {
        // 建立 Program 實例以呼叫解題方法
        Program solution = new Program();

        // 測試資料：依序為 (輸入, 預期步驟數)
        (string input, int expected)[] testCases =
        [
            ("1101", 6),   // 13 → 14 → 7 → 8 → 4 → 2 → 1
            ("10",   1),   // 2  → 1
            ("1",    0),   // 已是 1，不需步驟
            ("1111", 5),   // 15 → 16 → 8 → 4 → 2 → 1（全為 1 的特殊進位情況）
        ];

        Console.WriteLine("LeetCode 1404 - 將二進位表示減到 1 的步驟數");
        Console.WriteLine(new string('-', 50));

        foreach (var (input, expected) in testCases)
        {
            int result = solution.NumSteps(input);
            string status = result == expected ? "PASS" : "FAIL";
            Console.WriteLine($"[{status}] NumSteps(\"{input}\") = {result}  (預期: {expected})");
        }
    }

    /// <summary>
    /// 解法：模擬（Simulation）
    ///
    /// 核心觀察：
    ///   - 偶數 → 除以 2：等同於移除二進位字串最右側的 '0'。
    ///     範例："11010" → "1101"（去掉末尾 0）
    ///
    ///   - 奇數 → 加 1：等同於對最低位的 '1' 執行進位加法。
    ///     從最低位往高位找到第一個 '0'，將其設為 '1'，
    ///     並把它之後的所有 '1' 都改為 '0'（進位歸零）。
    ///     特殊情況：若字串全為 '1'，進位會超出最高位，
    ///     需在最前面插入 '1'（例如 "1111" → "10000"）。
    ///
    /// 時間複雜度：O(n²)，n 為字串長度（每次奇數操作最壞需掃描整串）
    /// 空間複雜度：O(n)，StringBuilder 的額外空間
    /// </summary>
    /// <param name="s">整數的二進位表示字串（不含前導零，且 s[0] == '1'）</param>
    /// <returns>將 s 化為 "1" 所需的總步驟數</returns>
    /// <example>
    /// <code>
    /// NumSteps("1101") // 返回 6
    /// NumSteps("10")   // 返回 1
    /// NumSteps("1")    // 返回 0
    /// </code>
    /// </example>
    public int NumSteps(string s)
    {
        int steps = 0;
        StringBuilder sb = new StringBuilder(s);

        // 持續操作直到字串變為 "1"
        while (sb.ToString() != "1")
        {
            steps++;

            if (sb[sb.Length - 1] == '0')
            {
                // 當前數為偶數：直接截去末尾的 '0'，等同於除以 2
                sb.Length--;
            }
            else
            {
                // 當前數為奇數：模擬二進位加 1
                // 步驟：從最低位往高位掃描
                //   - 遇到 '1' → 進位，將其改為 '0' 後繼續往高位
                //   - 遇到 '0' → 進位停止，將其改為 '1' 後跳出迴圈
                //   - 若已到達最高位（i == 0）且仍需進位，
                //     代表整串皆為 '1'，需在最前方插入 '1'
                for (int i = sb.Length - 1; i >= 0; i--)
                {
                    if (sb[i] == '1')
                    {
                        sb[i] = '0'; // 該位進位歸零
                        if (i == 0)
                        {
                            // 全為 '1' 的特殊情況：最高位仍需進位，擴展字串
                            sb.Insert(0, '1');
                            break;
                        }
                    }
                    else
                    {
                        sb[i] = '1'; // 找到可接收進位的位元，結束進位傳播
                        break;
                    }
                }
            }
        }

        return steps;
    }
}
