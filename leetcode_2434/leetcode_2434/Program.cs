using System.Text;

namespace leetcode_2434;

class Program
{
    /// <summary>
    /// <para>
    /// 2434. Using a Robot to Print the Lexicographically Smallest String
    /// https://leetcode.com/problems/using-a-robot-to-print-the-lexicographically-smallest-string/description/
    ///
    /// You are given string s and a robot currently holding empty string t. Until both strings are empty, repeatedly choose one operation: remove the first character of s and append it to t, or remove the last character of t and have the robot write it on paper. Return the lexicographically smallest string that can be written.
    ///
    /// Example 1:
    /// Input: s = "zza"
    /// Output: "azz"
    /// Explanation: Let p be the written string. Initially p = "", s = "zza", t = "". Apply the first operation three times to get p = "", s = "", t = "zza". Apply the second operation three times to get p = "azz", s = "", t = "".
    ///
    /// Example 2:
    /// Input: s = "bac"
    /// Output: "abc"
    /// Explanation: Let p be the written string. Apply the first operation twice to get p = "", s = "c", t = "ba". Apply the second operation twice to get p = "ab", s = "c", t = "". Apply the first operation once and then the second once to get p = "abc", s = "", t = "".
    ///
    /// Example 3:
    /// Input: s = "bdda"
    /// Output: "addb"
    /// Explanation: Let p be the written string. Initially p = "", s = "bdda", t = "". Apply the first operation four times to get p = "", s = "", t = "bdda". Apply the second operation four times to get p = "addb", s = "", t = "".
    ///
    /// Constraints:
    /// - 1 &lt;= s.length &lt;= 10^5
    /// - s consists only of lowercase English letters.
    /// </para>
    /// <para>
    /// 2434. 使用機器人列印字典序最小的字串
    /// https://leetcode.cn/problems/using-a-robot-to-print-the-lexicographically-smallest-string/description/
    ///
    /// 給定字串 s 與目前持有空字串 t 的機器人。在兩個字串都變空之前，反覆選擇一項操作：移除 s 的第一個字元並附加到 t，或移除 t 的最後一個字元並讓機器人寫到紙上。回傳紙上可寫出的字典序最小字串。
    ///
    /// 範例 1：
    /// 輸入：s = "zza"
    /// 輸出："azz"
    /// 說明：令 p 為已寫出的字串。起初 p = ""、s = "zza"、t = ""。執行第一項操作三次後得到 p = ""、s = ""、t = "zza"；再執行第二項操作三次，得到 p = "azz"、s = ""、t = ""。
    ///
    /// 範例 2：
    /// 輸入：s = "bac"
    /// 輸出："abc"
    /// 說明：令 p 為已寫出的字串。執行第一項操作兩次，得到 p = ""、s = "c"、t = "ba"；執行第二項操作兩次，得到 p = "ab"、s = "c"、t = ""；再各執行第一、第二項操作一次，得到 p = "abc"、s = ""、t = ""。
    ///
    /// 範例 3：
    /// 輸入：s = "bdda"
    /// 輸出："addb"
    /// 說明：令 p 為已寫出的字串。起初 p = ""、s = "bdda"、t = ""。執行第一項操作四次後得到 p = ""、s = ""、t = "bdda"；再執行第二項操作四次，得到 p = "addb"、s = ""、t = ""。
    ///
    /// 限制條件：
    /// - 1 &lt;= s.length &lt;= 10^5
    /// - s 僅由小寫英文字母組成。
    /// </para>
    /// </summary>
    /// <param name="args"></param>
    static void Main(string[] args)
    {
        // 測試資料
        var program = new Program();
        string[] testCases = { "zza", "bac", "bdda", "vzhz", "bydizfve" };
        Console.WriteLine("=== 方法1 ===");
        foreach (var test in testCases)
        {
            string result = program.RobotWithString(test);
            Console.WriteLine($"輸入: {test}，輸出: {result}");
        }
        Console.WriteLine("=== 方法2 ===");
        foreach (var test in testCases)
        {
            string result = program.RobotWithStringV2(test);
            Console.WriteLine($"輸入: {test}，輸出: {result}");
        }
    }

    /// <summary>
    /// ref: https://leetcode.cn/problems/using-a-robot-to-print-the-lexicographically-smallest-string/solutions/3687053/shi-yong-ji-qi-ren-da-yin-zi-dian-xu-zui-hwvo/?envType=daily-question&envId=2025-06-06
    /// 
    /// 解題說明：
    /// 這題的關鍵在於：每次決定是否要將 t 的最後一個字元寫到紙上時，必須確保這個字元不會比 s 剩下的最小字元大，否則會錯失更小的組合。
    /// 1. 預先統計 s 中每個字元剩餘出現次數，方便動態取得 s 剩下的最小字元。
    /// 2. 用堆疊模擬 t，每次從 s 取一個字元 push 進堆疊。
    /// 3. 只要堆疊頂端字元小於等於 s 剩下的最小字元，就 pop 並寫到紙上。
    /// 4. 最後將堆疊剩下的字元依序寫到紙上。
    /// 這樣能保證每次寫到紙上的字元都是當下能取得的最小字元，最終得到字典序最小的結果。
    /// </summary>
    /// <param name="s">原始字串 s</param>
    /// <returns>字典序最小的結果字串</returns>
    public string RobotWithString(string s)
    {
        // 步驟1：統計每個字元剩餘出現次數
        int[] remain = new int[26];
        foreach (char c in s)
        { 
            remain[c - 'a']++;
        }

        // 步驟2：用堆疊模擬 t
        Stack<char> stack = new Stack<char>();
        StringBuilder result = new StringBuilder();
        int minIdx = 0; // 目前 s 剩下的最小字元索引

        foreach (char c in s)
        {
            stack.Push(c); // 將 s 的第一個字元交給機器人（push 進堆疊）
            remain[c - 'a']--; // 更新剩餘次數

            // 動態尋找目前 s 剩下的最小字元
            while (minIdx < 26 && remain[minIdx] == 0)
            { 
                minIdx++; // 找到下一個剩餘的最小字元
            }

            // 步驟3：只要堆疊頂端字元小於等於 s 剩下的最小字元，就 pop 並寫到紙上
            while (stack.Count > 0 && (minIdx == 26 || stack.Peek() <= (char)(minIdx + 'a')))
            { 
                result.Append(stack.Pop()); // 將堆疊頂端字元寫到紙上
            }
        }

        // 步驟4：將堆疊剩下的字元依序寫到紙上
        while (stack.Count > 0)
        { 
            result.Append(stack.Pop());
        }

        return result.ToString();
    }


    /// <summary>
    /// ref:https://leetcode.cn/problems/using-a-robot-to-print-the-lexicographically-smallest-string/solutions/1878827/tan-xin-zhan-by-endlesscheng-ldds/?envType=daily-question&envId=2025-06-06
    /// 解法2：後綴最小值優化法
    /// 先預處理每個位置之後的最小字元（後綴最小值），然後用陣列模擬堆疊，根據貪心策略決定何時出堆疊。
    /// 解題說明：
    /// 這個方法利用後綴最小值陣列，能夠在 O(1) 時間內取得當前 s 剩下的最小字元，
    /// 每次將字元壓入堆疊後，只要堆疊頂端字元小於等於後綴最小值，就可以彈出並寫到紙上，
    /// 這樣能保證每次寫到紙上的字元都是當下能取得的最小字元，最終得到字典序最小的結果。
    /// </summary>
    /// <param name="s">原始字串 s</param>
    /// <returns>字典序最小的結果字串</returns>
    public string RobotWithStringV2(string S)
    {
        char[] s = S.ToCharArray();
        int n = s.Length;
        // 步驟1：計算每個位置之後的最小字元（後綴最小值）
        char[] sufMin = new char[n + 1];
        sufMin[n] = char.MaxValue;
        for (int i = n - 1; i >= 0; i--)
        {
            sufMin[i] = (char)Math.Min(sufMin[i + 1], s[i]); // 從右往左填充後綴最小值
        }

        // 步驟2：用陣列模擬堆疊，並依據貪心策略決定何時出堆疊
        char[] res = new char[n]; // 最終結果
        char[] stack = new char[n]; // 模擬堆疊
        int idx = 0; // 結果字串索引
        int top = -1; // 堆疊頂端索引
        for (int i = 0; i < n; i++)
        {
            stack[++top] = s[i]; // 將當前字元壓入堆疊
            // 只要堆疊頂端字元小於等於後綴最小值，就彈出並寫到結果
            while (top >= 0 && stack[top] <= sufMin[i + 1])
            {
                res[idx++] = stack[top--];
            }
        }
        // 步驟3：回傳結果
        return new string(res);
    }
}
