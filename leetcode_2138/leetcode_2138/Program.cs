namespace leetcode_2138;

class Program
{
    /// <summary>
    /// 2138. Divide a String Into Groups of Size k
    /// https://leetcode.com/problems/divide-a-string-into-groups-of-size-k/description/?envType=daily-question&envId=2025-06-22
    /// 2138. 将字符串拆分为若干长度为 k 的组
    /// https://leetcode.cn/problems/divide-a-string-into-groups-of-size-k/description/?envType=daily-question&envId=2025-06-22
    /// 
    /// 給定一個字串 s，可以用以下方式將其分組為每組長度為 k ：
    /// 第一組包含字串的前 k 個字元，第二組包含接下來的 k 個字元，依此類推。每個元素只能屬於一個組。
    /// 對於最後一組，如果剩下的字元不足 k 個，則使用填充字元 fill 來補足。
    /// 分組後，將最後一組的填充字元移除並將所有組串接起來，應該能還原為原始字串 s。
    /// 給定字串 s、每組長度 k 及填充字元 fill，請回傳一個字串陣列，表示分組後的每一組內容。
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
            lastGroup += new string(fill, k - lastGroup.Length);
            res[res.Count - 1] = lastGroup;
        }
        // 轉為陣列回傳
        return res.ToArray();
    }
}
