using System.Collections.Generic;

namespace leetcode_242
{
    internal class Program
    {
        /// <summary>
        /// 242. Valid Anagram
        /// https://leetcode.com/problems/valid-anagram/
        /// 242. 有效的字母異位詞
        /// https://leetcode.cn/problems/valid-anagram/
        /// 
        /// 比對兩輸入字串是否相同
        /// 1.出現字母
        /// 2.每次字母出現的頻率(次數)
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            string s = "aacc";
            string t = "ccac";

            Console.WriteLine("方法1: " + IsAnagram(s, t));
            Console.WriteLine("方法2: " + IsAnagram2(s, t));
        }


        /// <summary>
        /// 方法1 - 使用陣列計數解法
        /// 
        /// 解題概念：
        /// 1. 使用陣列記錄字母出現次數，利用 ASCII 字元轉換計算索引
        /// 2. 同時處理兩個字串，一個加一個減，最後檢查是否所有計數都為零
        /// 
        /// 時間複雜度：O(n)，只需遍歷一次字串
        /// 空間複雜度：O(1)，使用固定大小的陣列(26個小寫字母)
        /// 
        /// 優點：
        /// - 不需要排序，效率較高
        /// - 空間使用固定，不受輸入大小影響
        /// 
        /// 此方法為優化方法2
        /// </summary>
        /// <param name="s">第一個輸入字串</param>
        /// <param name="t">第二個輸入字串</param>
        /// <returns>兩個字串是否為有效的字母異位詞</returns>
        public static bool IsAnagram(string s, string t)
        {
            // 步驟1：先檢查兩個字串長度是否相等，不相等直接返回 false
            if(s.Length != t.Length)
            {
                return false;
            }

            // 步驟2：建立一個長度為26的整數陣列，用於記錄每個字母出現的次數
            int[] charCount = new int[26];

            // 步驟3：遍歷兩個字串，對s中的字母計數加1，對t中的字母計數減1
            for (int i = 0; i < s.Length; i++)
            {
                charCount[s[i] - 'a']++;  // s字串中的字母計數加1
                charCount[t[i] - 'a']--;  // t字串中的字母計數減1
            }

            // 步驟4：檢查計數陣列，如果有非0的值，表示字母出現次數不一致
            for (int i = 0; i < charCount.Length; i++)
            {
                if (charCount[i] != 0)
                {
                    return false;
                }
            }

            // 步驟5：所有計數都為0，表示兩個字串互為字母異位詞
            return true;
        }


        /// <summary>
        /// 方法2 - 使用 Dictionary 計數解法
        /// 
        /// 解題概念：
        /// 1. 使用 Dictionary 儲存字母出現次數，key 為字母，value 為出現次數
        /// 2. 分三步驟處理：
        ///    - 統計第一個字串(s)中字母出現頻率
        ///    - 遍歷第二個字串(t)進行比對和扣除
        ///    - 最後檢查所有計數是否歸零
        /// 3. 無需預先排序，直接統計字母頻率即可判斷
        /// 
        /// 時間複雜度：O(n)，需要三次遍歷
        /// 空間複雜度：O(k)，k 為不同字母的數量，最多 26 個字母
        /// 
        /// 優點：
        /// - 邏輯清晰，容易理解
        /// - 可以快速判斷字母是否存在
        /// 
        /// 缺點：
        /// - 需要三次遍歷，效率較方法1低
        /// - 使用 Dictionary 需要額外的記憶體空間
        /// </summary>
        /// <param name="s">第一個輸入字串</param>
        /// <param name="t">第二個輸入字串</param>
        /// <returns>兩個字串是否為有效的字母異位詞</returns>
        public static bool IsAnagram2(string s, string t)
        {
            // 步驟1：檢查兩個字串長度是否相等，不相等直接返回 false
            if (s.Length != t.Length)
            {
                return false;
            }

            // 建立 Dictionary 用於儲存字母出現次數
            Dictionary<char, int> dic = new Dictionary<char, int>();

            // 步驟2：統計第一個字串(s)中每個字母出現的次數
            foreach (char c in s)
            {
                if (dic.ContainsKey(c))
                {
                    dic[c]++;  // 字母已存在，次數加1
                }
                else
                {
                    dic.Add(c, 1);  // 字母首次出現，新增至Dictionary
                }
            }

            // 步驟3：比對第二個字串(t)，逐一扣除字母出現次數
            foreach (char c in t)
            {
                if (dic.ContainsKey(c))
                {
                    dic[c]--;  // 找到相同字母，扣除一次
                }
                else
                {
                    // 發現t中出現但s中沒有的字母，直接返回false
                    return false;
                }
            }

            // 步驟4：檢查所有字母的計數是否都為0
            foreach (var item in dic)
            {
                if (item.Value != 0)  // 若有非0值，表示字母出現次數不一致
                {
                    return false;
                }
            }

            return true;
        }
    }

}
