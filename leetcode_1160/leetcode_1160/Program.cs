using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace leetcode_1160
{
    internal class Program
    {
        /// <summary>
        /// 1160. Find Words That Can Be Formed by Characters
        /// https://leetcode.com/problems/find-words-that-can-be-formed-by-characters/?envType=daily-question&envId=2023-12-02
        /// 1160. 拼写单词
        /// https://leetcode.cn/problems/find-words-that-can-be-formed-by-characters/
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            string[] input = { "cat", "bt", "hat", "tree" };
            string chars = "atach";
            Console.WriteLine(CountCharacters(input, chars));
            Console.ReadKey();
        }


        /// <summary>
        /// https://leetcode.cn/problems/find-words-that-can-be-formed-by-characters/solutions/2350010/zui-zhi-jie-de-si-lu-ba-zi-dian-li-de-me-pthh/
        /// 把字典里的每个字母（包括重复的），拿出来和单词进行一一比对（不比对顺序）
        /// ，有一个匹配不上就说明不能字典里的字母不能组成该单词
        /// 
        /// words裡面的字詞 由chars裡面的char 組合而成
        /// 且chars裡面的每個char只能使用一次.
        /// 只要能組合成功就是 true
        /// 否則就是false
        /// 
        /// 最後 把為true的每個word長度最累加
        /// 就是題目要求的
        /// 
        /// </summary>
        /// <param name="words"></param>
        /// <param name="chars"></param>
        /// <returns></returns>
        public static int CountCharacters(string[] words, string chars)
        {
            int count = 0;
            
            foreach(string word in words)
            {
                bool flag = false;
                string newchars = chars.ToString().Trim();
                
                // 每個word裡面的單字(char)取出來跟chars裡面的char 比對是否存在
                foreach(var item in word)
                {
                    if(newchars.Contains(item))
                    {
                        flag = true;

                        // 移除 已找到的那個char, 因不能使用第二次
                        newchars = newchars.Remove(newchars.IndexOf(item), 1);
                        newchars.Trim();
                    }
                    else
                    {
                        flag = false;
                        break;
                    }
                }

                // 每個word比對結束, 要把chars還原. 因為之前有移除
                newchars = chars;
                
                // 比對成功, 就累加成功的word長度
                if(flag == true)
                {
                    count += word.Length;
                }
            }
            return count;
        }

    }
}
