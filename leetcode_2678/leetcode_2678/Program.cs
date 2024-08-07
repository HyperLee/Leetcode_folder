﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace leetcode_2678
{
    internal class Program
    {
        /// <summary>
        /// 2678. Number of Senior Citizens
        /// https://leetcode.com/problems/number-of-senior-citizens/
        /// 2678. 老人的数目
        /// https://leetcode.cn/problems/number-of-senior-citizens/?envType=daily-question&envId=Invalid%20Date
        /// 
        /// 輸入字串規格: 總長度 15
        /// ex: 7868190130M7522
        /// 前十碼: 手機號碼
        /// 下一碼: 性別
        /// 下兩碼: 年紀
        /// 最後兩碼: 座位號
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            string[] input = { "7868190130M7522", "5303914400F9211", "9273338290F4010" };
            Console.WriteLine("res: " + CountSeniors(input));
            Console.ReadKey();
        }


        /// <summary>
        /// 迴圈 跑全部測資
        /// 字串擷取 取出 年紀
        /// 符合就++
        /// 
        /// 字串擷取年紀: 第11碼開始取兩碼即可
        /// </summary>
        /// <param name="details"></param>
        /// <returns></returns>
        public static int CountSeniors(string[] details)
        {
            int count = 0;
            for(int i = 0; i < details.Length; i++)
            {
                string temp = details[i];
                /* // 方法1
                temp = temp.Substring(11, 2);

                if(int.Parse(temp) > 60)
                {
                    count++;
                }
                */

                // 方法2:
                if(int.Parse(temp.Substring(11, 2)) > 60)
                {
                    count++;
                }
            }

            return count;
        }
    }
}
