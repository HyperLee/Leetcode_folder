namespace leetcode_2138;

class Program
{
    /// <summary>
    /// <para>
    /// 2138. Divide a String Into Groups of Size k
    /// https://leetcode.com/problems/divide-a-string-into-groups-of-size-k/description/
    ///
    /// Partition string s into groups of size k. The first group contains the first k characters, the second contains the next k, and so on; every character belongs to exactly one group. If fewer than k characters remain, append fill until the last group is complete. Removing this padding and concatenating the groups must reproduce s.
    ///
    /// Given s, k, and fill, return the array of groups.
    ///
    /// Example 1:
    /// Input: s = "abcdefghi", k = 3, fill = "x"
    /// Output: ["abc","def","ghi"]
    /// Explanation: The three groups each use 3 source characters, so no fill is needed.
    ///
    /// Example 2:
    /// Input: s = "abcdefghij", k = 3, fill = "x"
    /// Output: ["abc","def","ghi","jxx"]
    /// Explanation: The first three groups are complete. The 4th contains 'j', so append 'x' twice.
    ///
    /// Constraints:
    /// - 1 &lt;= s.length &lt;= 100
    /// - s contains lowercase English letters only.
    /// - 1 &lt;= k &lt;= 100
    /// - fill is a lowercase English letter.
    /// </para>
    /// <para>
    /// 2138. 將字串拆分為若干長度為 K 的組
    /// https://leetcode.cn/problems/divide-a-string-into-groups-of-size-k/description/
    ///
    /// 將字串 s 劃分成大小為 k 的群組。第一組包含前 k 個字元，第二組包含接下來的 k 個，依此類推；每個字元恰好屬於一組。若最後不足 k 個字元，就附加 fill 直到完整。移除這些填充字元並依序串接所有群組後，必須還原 s。
    ///
    /// 給定 s、k、fill，回傳群組陣列。
    ///
    /// 範例 1：
    /// 輸入：s = "abcdefghi", k = 3, fill = "x"
    /// 輸出：["abc","def","ghi"]
    /// 說明：三個群組都各使用 3 個原始字元，因此不需填充。
    ///
    /// 範例 2：
    /// 輸入：s = "abcdefghij", k = 3, fill = "x"
    /// 輸出：["abc","def","ghi","jxx"]
    /// 說明：前三組已完整；第 4 組只有 'j'，因此附加兩次 'x'。
    ///
    /// 限制條件：
    /// - 1 &lt;= s.length &lt;= 100
    /// - s 僅包含小寫英文字母。
    /// - 1 &lt;= k &lt;= 100
    /// - fill 是小寫英文字母。
    /// </para>
    /// </summary>
    /// <param name="args"></param> 
    static void Main(string[] args)
    {
        // 測試資料
        string s = "abcdefghi";
        int k = 3;
        char fill = 'x';
        var program = new Program();
        string[] groups = program.DivideString(s, k, fill);
        Console.WriteLine($"輸入: s = {s}, k = {k}, fill = '{fill}'");
        Console.WriteLine("分組結果:");
        foreach (var group in groups)
        {
            Console.WriteLine(group);
        }
    }


    /// <summary>
    /// 將字串 s 拆分為每組長度為 k 的子字串，並使用 fill 字元填充不足 k 的部分。
    /// 
    /// 【解題說明概念】
    /// 先計算分組數量，建立固定長度的字串陣列，for 迴圈依序擷取每組子字串，若不足 k 則用 PadRight 補齊。
    /// 
    /// 【時間複雜度】O(n)，n 為字串長度，需遍歷每個字元一次。
    /// 【空間複雜度】O(n)，需儲存所有分組結果。
    /// </summary>
    /// <param name="s">輸入字串</param>
    /// <param name="k">每組長度</param>
    /// <param name="fill">填充字元</param>
    /// <returns>分組後的字串陣列</returns>
    public string[] DivideString(string s, int k, char fill)
    {
        // 計算需要的組數，等同於無條件進位
        int groupCount = (s.Length + k - 1) / k;
        string[] result = new string[groupCount];

        for (int i = 0; i < groupCount; i++)
        {
            // 計算每組的起始位置
            int start = i * k;
            // 取得當前組的字串，長度最多為 k，若剩餘不足 k 則取剩下的長度
            string group = s.Substring(start, Math.Min(k, s.Length - start));

            // 如果當前組的長度小於 k，則用 fill 字元補齊
            if (group.Length < k)
            {
                group = group.PadRight(k, fill);
            }

            // 將分組結果存入陣列
            result[i] = group;
        }

        return result;
    }


    /// <summary>
    /// 將字串 s 拆分為每組長度為 k 的子字串，並使用 fill 字元填充不足 k 的部分。
    /// 這個方法使用 List<string> 來動態添加每組字串。
    /// 
    /// 【解題說明概念】
    /// 以 while 迴圈每次擷取長度為 k 的子字串，動態加入 List，最後一組若不足 k 則用填充字元補齊。
    /// 
    /// 【時間複雜度】O(n)，n 為字串長度，需遍歷每個字元一次。
    /// 【空間複雜度】O(n)，需儲存所有分組結果。
    /// </summary>
    /// <param name="s">輸入字串</param>
    /// <param name="k">每組長度</param>
    /// <param name="fill">填充字元</param>
    /// <returns>分組後的字串陣列</returns>
    public string[] DivideString2(string s, int k, char fill)
    {
        // 建立動態 List 來儲存分組結果
        List<string> res = new List<string>();
        int n = s.Length;
        int curr = 0;

        // 以 while 迴圈每次擷取長度為 k 的子字串
        while (curr < n)
        {
            int end = Math.Min(curr + k, n); // 計算本組結束位置，避免超出字串長度
            res.Add(s.Substring(curr, end - curr)); // 加入本組子字串
            curr += k; // 移動到下一組起始位置
        }

        // 處理最後一組不足 k 的情況，補齊填充字元
        string lastGroup = res[res.Count - 1];
        if (lastGroup.Length < k)
        {
            // 用填充字元補齊
            lastGroup += new string(fill, k - lastGroup.Length);
            // 更新最後一組
            res[res.Count - 1] = lastGroup;
        }
        // 轉為陣列回傳
        return res.ToArray();
    }
}
