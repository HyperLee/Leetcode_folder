namespace leetcode_3170;

class Program
{
    /// <summary>
    /// 3170. Lexicographically Minimum String After Removing Stars
    /// https://leetcode.com/problems/lexicographically-minimum-string-after-removing-stars/description/?envType=daily-question&envId=2025-06-07
    /// 3170. 刪除星號以後字典序最小的字串
    /// https://leetcode.cn/problems/lexicographically-minimum-string-after-removing-stars/description/?envType=daily-question&envId=2025-06-07
    /// 
    /// 題目描述（繁體中文翻譯）：
    /// 給定一個字串 s，可能包含任意數量的 '*' 字元。你的任務是移除所有的 '*' 字元。
    /// 當還有 '*' 存在時，執行以下操作：
    /// 刪除最左邊的 '*' 以及其左側最小的非 '*' 字元（如果有多個最小字元，可以刪除任意一個）。
    /// 移除所有 '*' 後，回傳字典序最小的結果字串。
    /// </summary>
    /// <param name="args"></param>
    static void Main(string[] args)
    {
        // 測試資料
        var program = new Program();
        string[] testCases = {
            "a*b*c*",      // 預期: ""
            "abc*de*f*",  // 預期: "def"
            "abac*ba*",   // 預期: "abcb"
            "leetcode*",  // 預期: "leetode"
            "a*bc*d*e*"   // 預期: "e"
        };
        foreach (var test in testCases)
        {
            string result = program.ClearStars(test);
            Console.WriteLine($"輸入: {test} => 輸出: {result}");
        }
    }


    /// <summary>
    /// 根據題目規則，移除所有星號及其左側最小的非星號字元，並回傳字典序最小的結果字串。
    /// 解題說明：
    /// 1. 先建立 26 個堆疊（對應 a~z），用來記錄每個字母在字串中的索引。
    /// 2. 逐字元遍歷字串，遇到字母就將其索引壓入對應堆疊。
    /// 3. 遇到星號時，從 a~z 順序尋找最左側（字典序最小）的字母，將其標記為星號（代表移除）。
    /// 4. 最後將所有星號過濾掉，組成新字串回傳。
    /// 流程：
    /// - 先記錄每個字母出現的位置。
    /// - 每遇到一個星號，優先移除左側最小的字母。
    /// - 最終剩下的字元即為答案。
    /// </summary>
    /// <param name="s">輸入字串，僅包含小寫英文字母與星號</param>
    /// <returns>移除所有星號後的字典序最小字串</returns> 
    public string ClearStars(string s)
    {
        // 建立 26 個堆疊，分別對應 a~z
        Stack<int>[] cnt = new Stack<int>[26];
        for (int i = 0; i < 26; i++)
        {
            // 初始化每個堆疊
            cnt[i] = new Stack<int>();
        }

        // 將字串轉為字元陣列，方便標記移除
        char[] arr = s.ToCharArray();
        // 遍歷字元陣列
        for (int i = 0; i < arr.Length; i++)
        {
            // 如果不是星號，則將字母的索引壓入對應堆疊
            if (arr[i] != '*')
            {
                // 將字母的索引壓入對應堆疊
                cnt[arr[i] - 'a'].Push(i);
            }
            else 
            {
                // 遇到星號，從 a~z 找最小的字母並移除
                for (int j = 0; j < 26; j++)
                {
                    if (cnt[j].Count > 0)
                    {
                        // 將該字母標記為星號（代表移除）
                        arr[cnt[j].Pop()] = '*';
                        break;
                    }
                }
            }
        }

        // 過濾掉所有星號，組成新字串
        return new string(Array.FindAll(arr, c => c != '*'));
    }

}
