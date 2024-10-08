﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace leetcode_150
{
    internal class Program
    {
        /// <summary>
        /// 150. Evaluate Reverse Polish Notation
        /// https://leetcode.com/problems/evaluate-reverse-polish-notation/description/?envType=daily-question&envId=2024-01-30
        /// 150. 逆波兰表达式求值
        /// https://leetcode.cn/problems/evaluate-reverse-polish-notation/description/
        /// 
        /// 本題目重點 要先看懂 表示法
        /// 才知道如何計算
        /// 
        /// 把題目給的字串按順序輸入
        /// 數字直接push, 符號也是push然後把 最上方兩個數字抓出來
        /// 做運算
        /// 大致上是這樣
        /// 詳細看wiki說明
        /// 
        /// 主要是stack用法
        /// 
        /// 逆波蘭表示法
        /// https://zh.wikipedia.org/zh-tw/%E9%80%86%E6%B3%A2%E5%85%B0%E8%A1%A8%E7%A4%BA%E6%B3%95
        /// https://en.wikipedia.org/wiki/Reverse_Polish_notation
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            string[] input = { "2", "1", "+", "3", "*" };
            Console.WriteLine(EvalRPN(input));
            Console.ReadKey();
         }


        /// <summary>
        /// https://leetcode.cn/problems/evaluate-reverse-polish-notation/solutions/1456145/by-stormsunshine-cxqe/
        /// 採用方法一
        /// 比較直覺類似 wiki說明方式
        /// 
        /// </summary>
        /// <param name="tokens"></param>
        /// <returns></returns>
        public static int EvalRPN(string[] tokens)
        {
            // 回傳答案是 int 型態
            Stack<int> stack = new Stack<int>();    
            int length = tokens.Length;

            for(int i = 0; i < length; i++)
            {
                string token = tokens[i];

                if(IsNumber(token) == true)
                {
                    // 數字就直接push進入stack
                    stack.Push(int.Parse(token));
                }
                else
                {
                    // 遇到符號就要運算( 將數字 pop 出來, 再進行符號運算後.
                    // 在計算完畢數字 push 進入 stack)
                    // 最上方(後進入)兩個數字抓出來運算
                    // stack 先進後出, 所以宣告先大在小合理
                    int num2 = stack.Pop();
                    int num1 = stack.Pop();

                    // 注意 num1 與 num2 順序不能互換. 計算會出錯
                    switch(token)
                    {
                        case "+":
                            stack.Push(num1 + num2);
                            break;
                        case "-":
                            stack.Push(num1 - num2);
                            break;
                        case "*":
                            stack.Push(num1 * num2);
                            break;
                        case "/":
                            stack.Push(num1 / num2);
                            break;
                        default:
                            break;
                    }
                }
            }

            return stack.Pop();
        }


        /// <summary>
        /// 判斷是不是數字
        /// 在 C# 中，char.IsDigit 方法用于判断指定字符是否是一个数字。它返回一个布尔值，表示字符是否是 0 到 9 之间的数字。
        ///
        /// 輸入是 string, 要轉 char 才能判斷
        /// 所以用 陣列方式表達
        /// 
        /// ex:
        /// string str = "A";
        /// char c = str[0]; 
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public static bool IsNumber(string token)
        {
            return char.IsDigit(token[token.Length - 1]);
        }
    }
}
