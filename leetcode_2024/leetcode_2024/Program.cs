using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace leetcode_2024
{
    internal class Program
    {
        /// <summary>
        /// leetcode 2024 Maximize the Confusion of an Exam
        /// https://leetcode.com/problems/maximize-the-confusion-of-an-exam/
        /// 2024. 考试的最大困扰度
        /// https://leetcode.cn/problems/maximize-the-confusion-of-an-exam/
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            string answerKey = "TTFF";
            int k = 2;

            Console.WriteLine(MaxConsecutiveAnswers(answerKey, k));
            Console.ReadKey();
        }


        /// <summary>
        /// https://leetcode.cn/problems/maximize-the-confusion-of-an-exam/solution/kao-shi-de-zui-da-kun-rao-du-by-leetcode-qub5/
        /// 
        /// https://leetcode.cn/problems/maximize-the-confusion-of-an-exam/solution/by-ac_oier-2rii/
        /// 宮水解法比較好懂
        /// 
        /// </summary>
        /// <param name="answerKey"></param>
        /// <param name="k"></param>
        /// <returns></returns>
        public static int MaxConsecutiveAnswers(string answerKey, int k)
        {
            return Math.Max(MaxConsecutiveChar(answerKey, k, 'T'), MaxConsecutiveChar(answerKey, k, 'F'));
        }


        /// <summary>
        /// 滑動視窗 解法
        /// 
        /// 题目求修改次数不超过 k 的前提下，连续段 'T' 或 'F' 的最大长度。
        /// 等价于求一个包含 'F' 或者 'T' 的个数不超过 k 的最大长度窗口。
        /// 
        /// 理解這句話,就大概會懂解法用意
        /// 
        /// 帶入 function 的 ch
        /// 去跟answerkey字串比對, 在滑動視窗[left, right]裡面
        /// 需要替換的文字數量不能超過 k
        /// 當超過k 就需要把 left 往左移動
        /// 找出新的視窗, 
        /// 
        /// T 與 F 都要各跑過一輪
        /// 找出兩者之間最大著
        /// 即為答案
        /// 
        /// </summary>
        /// <param name="answerKey"></param>
        /// <param name="k"></param>
        /// <param name="ch"></param>
        /// <returns></returns>
        public static int MaxConsecutiveChar(string answerKey, int k, char ch) 
        {
            int n = answerKey.Length;
            int ans = 0;
            
            for(int left = 0, right = 0, sum = 0; right < n; right++) 
            {
                // 相同就累加, 統計有多少個可以替換
                if (answerKey[right] == ch)
                {
                    sum++;
                }

                while(sum > k)
                {
                    if (answerKey[left] == ch)
                    {
                        sum--;
                    }

                    left++;
                }

                // 找出 滑動窗口 最大長度
                ans = Math.Max(ans, right - left + 1);
            }

            return ans;
        }


    }
}
