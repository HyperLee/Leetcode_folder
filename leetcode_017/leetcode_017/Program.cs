using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace leetcode_017
{
    internal class Program
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
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            string digits = "23";
            LetterCombinations(digits);
            Console.ReadKey();


        }


        /// <summary>
        /// https://leetcode.cn/problems/letter-combinations-of-a-phone-number/solution/by-stormsunshine-k2dm/
        /// 
        /// 当给定的字符串 digits 为空时，不存在可以表示的字母组合，因此返回空列表。
        /// 给定的字符串 digits 不为空时，由于每个数字都是 2 到 9，因此都存在对应的字母。
        /// 
        /// </summary>
        /// <param name="digits"></param>
        /// <returns></returns>
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


            // 答案輸出
            Console.Write("res: ");
            foreach(var item in combinations)
            {
                Console.Write(item + ", ");
            }

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
        /// <param name="combination">加入預選(候選)字母組合</param>
        public static void Backtrack(int index, StringBuilder combination)
        {
            // 排列暫存的組合長度符合 digits2 長度(題目輸入長度) 就加入答案
            if (index == digits2.Length)
            {
                combinations.Add(combination.ToString());
            }
            else
            {
                // 取出 digits2 數字; 取出 digits2 中第 index 位置資料
                int digit = digits2[index] - '0';
                // 取出數字映射的英文字母
                string letters = lettersArr[digit];

                // 找出所有可能之組合
                foreach (char c in letters)
                {
                    // 將對應的字母加入排列暫存(預選;候選)
                    combination.Append(c);
                    // 遞迴; 加入之後, 計算下一種之組合(index + 1) (找下個預選者)
                    Backtrack(index + 1, combination);
                    // 回歸; 退回原先狀態(字母), 再找下個新的組合
                    combination.Length--;
                }

            }

        }


    }
}
