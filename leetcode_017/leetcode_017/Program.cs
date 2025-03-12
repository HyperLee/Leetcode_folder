using System.Text;

namespace leetcode_017;

class Program
{
        // 電話鍵盤上 0 ~ 9 按鈕, 但是只有2 ~ 9才有蘊含英文字母
        public static string[] lettersArr = { "", "", "abc", "def", "ghi", "jkl", "mno", "pqrs", "tuv", "wxyz" };
        // 
        public static string digits2;
        // 答案
        public static IList<string> combinations;

        /// <summary>
        /// 17. Letter Combinations of a Phone Number
        /// https://leetcode.com/problems/letter-combinations-of-a-phone-number/description/
        /// 17. 电话号码的字母组合
        /// https://leetcode.cn/problems/letter-combinations-of-a-phone-number/
        /// 
        /// 列舉 回朔 題目
        /// 類似 題目 046
        /// 
        /// 電話號碼字母組合的特性
        /// 1. 順序性
        ///     輸入是 "23"，其中：
        ///     2 對應 "abc"
        ///     3 對應 "def"
        ///     組合必須按照輸入數字的順序來生成
        /// 2. 示例說明
        ///     輸入 "23" 時：
        ///     第一個位置只能是 2 對應的字母 (a,b,c)
        ///     第二個位置只能是 3 對應的字母 (d,e,f)
        /// 所以合法的組合是：
        /// ad, ae, af
        /// bd, be, bf
        /// cd, ce, cf
        ///
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            // 測試案例 1: 兩個數字組合
            Console.WriteLine("\n測試案例 1 - 輸入: \"23\"");
            string digits1 = "23";
            var res =  LetterCombinations(digits1);
            Console.WriteLine($"res: {string.Join(", ", res)}");

            // 測試案例 2: 空字串
            Console.WriteLine("\n測試案例 2 - 輸入: \"\"");
            string digits2 = "";
            var res2 =  LetterCombinations(digits2);
            Console.WriteLine($"res: {string.Join(", ", res2)}");

            // 測試案例 3: 單個數字
            Console.WriteLine("\n測試案例 3 - 輸入: \"2\"");
            string digits3 = "2";
            var res3 = LetterCombinations(digits3);
            Console.WriteLine($"res: {string.Join(", ", res3)}");

            // 測試案例 4: 三個數字組合
            Console.WriteLine("\n測試案例 4 - 輸入: \"234\"");
            string digits4 = "234";
            var res4 = LetterCombinations(digits4);
            Console.WriteLine($"res: {string.Join(", ", res4)}");

            // 測試案例 5: 包含 7 和 9 (四個字母的按鍵)
            Console.WriteLine("\n測試案例 5 - 輸入: \"79\"");
            string digits5 = "79";
            var res5 = LetterCombinations(digits5);
            Console.WriteLine($"res: {string.Join(", ", res5)}");
        }


        /// <summary>
        /// https://leetcode.cn/problems/letter-combinations-of-a-phone-number/solution/by-stormsunshine-k2dm/
        /// 
        /// 解題概念:
        /// 1. 使用回溯法(Backtracking)來解決此問題
        /// 2. 建立電話按鍵對應字母的映射表(lettersArr)
        /// 3. 利用遞迴方式逐個處理每個數字對應的字母
        /// 
        /// 解題想法:
        /// 1. 先檢查輸入是否為空，空則直接返回空列表
        /// 2. 對每個數字:
        ///    - 找出對應的字母集合
        ///    - 遍歷字母集合中的每個字母
        ///    - 將字母加入當前組合
        ///    - 遞迴處理下一個數字
        ///    - 回溯，移除最後加入的字母
        /// 3. 當處理完所有數字時，將當前組合加入結果集
        /// 
        /// 时间复杂度：O(4^n)，其中 n 是輸入數字的長度
        /// 空间复杂度：O(n)，遞迴調用棧的深度最大為 n
        /// 
        /// 当给定的字符串 digits 为空时，不存在可以表示的字母组合，因此返回空列表。
        /// 给定的字符串 digits 不为空时，由于每个数字都是 2 到 9，因此都存在对应的字母。
        /// </summary>
        /// <param name="digits">輸入的數字字串</param>
        /// <returns>所有可能的字母組合</returns>
        public static IList<string> LetterCombinations(string digits)
        {
            digits2 = digits;
            combinations = new List<string>();

            // 輸入為空, 直接返回
            if (digits2.Length == 0)
            {
                return combinations;
            }

            // 不為空, 就去回朔找資料
            Backtrack(0, new StringBuilder());


            return combinations;
        }


        /// <summary>
        /// https://leetcode.cn/problems/letter-combinations-of-a-phone-number/solution/by-stormsunshine-k2dm/
        /// 
        /// 回溯的过程中维护一个可变字符串用于存储当前的字母组合。
        /// 
        /// 每次从给定的字符串中读取一个数字并得到该数字对应的所有可能的字母
        /// ，依次将每个可能的字母拼接到可变字符串的末尾。
        /// 
        /// 当给定的字符串遍历结束时，可变字符串即为一个可能的字母组合，
        /// 将该字母组合添加到结果列表中，然后回退并遍历其他可能的字母
        /// 
        /// 1.依據題目輸入順序取出該鍵盤數字
        /// 2.取出該數字對應的的英文字母
        /// 3.將該字母加入 combination (預選, 候選), 再來透過 index + 1 找出下一個按鈕的字母
        /// 4.當長度符合題目需求(digits2) 就將該字母組合加入至 combinations
        /// 5.退回原先長度, 繼續找下一個組合
        /// 
        /// </summary>
        /// <param name="index">index</param>
        /// <param name="sb">加入預選(候選)字母組合</param>
        public static void Backtrack(int index, StringBuilder sb)
        {
            // Step 1: 檢查是否完成組合
            // 當遞迴深度(index)等於輸入字串長度時，表示已經形成一個完整組合
            if (index == digits2.Length)
            {
                combinations.Add(sb.ToString());
            }
            else
            {
                // Step 2: 取得當前數字對應的字母
                // 2.1 將字符轉換為數字 (例如: '2' -> 2)
                int digit = digits2[index] - '0';
                // 2.2 從字母映射數組中獲取對應的字母字串
                string letters = lettersArr[digit];

                // Step 3: 遍歷當前數字對應的所有可能字母
                foreach (char c in letters)
                {
                    // Step 4: 構建組合
                    // 4.1 將當前字母加入組合中
                    sb.Append(c);
                    
                    // Step 5: 遞迴處理下一個數字
                    // 5.1 透過增加索引(index + 1)處理下一個數字的字母
                    Backtrack(index + 1, sb);
                    
                    // Step 6: 回溯
                    // 6.1 移除最後加入的字母，還原組合狀態
                    // 6.2 這樣可以在下一次迭代中嘗試其他字母
                    sb.Length--;
                }
            }
        }

}
