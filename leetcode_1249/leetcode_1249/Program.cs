using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace leetcode_1249
{
    class Program
    {
        /// <summary>
        /// leetcode 1249
        /// https://leetcode.com/problems/minimum-remove-to-make-valid-parentheses/
        /// 
        /// https://leetcode-cn.com/problems/minimum-remove-to-make-valid-parentheses/solution/yi-chu-wu-xiao-gua-hao-by-leetcode/
        /// 
        /// https://leetcode-cn.com/problems/minimum-remove-to-make-valid-parentheses/solution/c-bu-yao-zhan-by-kang-kang-49/
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            string s = "";
            s = "(()";

            Console.WriteLine(MinRemoveToMakeValid(s));

            //Console.WriteLine(MinRemoveToMakeValid2(s));

            Console.ReadKey();
        }


        /// <summary>
        /// 沒有使用stack 方式
        /// 速度好一點
        /// https://leetcode-cn.com/problems/minimum-remove-to-make-valid-parentheses/solution/yi-chu-wu-xiao-gua-hao-by-leetcode/
        /// 官方解法
        /// 方法三：改进的使用 StringBuilder 的两步法
        /// 思路
        /// 这是方法二的简化版本，只需要保持平衡即可，不需要栈，也不需要执行两次完整的字符串扫描。在完成第
        /// 一步扫描后，查看有多少个需要删除的 "("，然后从右侧开始扫描，删除对应个数的 "(" 即可。事实证
        /// 明，如果删除最右边的 "("，一定可以保证字符串平衡。
        /// 
        /// 完成第一步扫描后，字符串中就不包含无效的 ")"。接下来考虑一种算法可以删除多余的 "("，使字符串有效。
        /// 要让一个 "(" 有效，必须在该 "(" 后面有比 "(" 数量更多的 ")"。在 s 中如果所有的 "(" 和 
        /// ")" 都一一匹配，则 s 是有效的。
        /// 
        /// 因此，从右边开始根据余量删除 "("，每次删除都可以在保证字符串有效的情况下，修改余量。如果任何
        /// 的 "(" 都是无效的，则说明在第一个 "(" 之前就存在 ")" 了，但是无效的 ")" 在第一步时就已经删
        /// 除了，所以第二步中不存在这样的情况。
        /// 综上，这是一个可行的方法。
        /// 
        /// 算法
        /// 为了避免第二步迭代（这会增加算法的复杂性），需要记录第一步扫描中字符串有多少个 "("。这样就可
        /// 以在第二次扫描时计算出保留的 "(" 数量和删除的 "(" 数量。一旦达到足够数量的 "("，就可以直接
        /// 删除后面的 "("。
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static string MinRemoveToMakeValid(string s)
        {
            // Parse 1: Remove all invalid ")"
            StringBuilder sb = new StringBuilder();
            int openCount = 0; // 左
            int balanceCount = 0; // 右
            for (int i = 0; i < s.Length; i++)
            {
                if (s[i] == '(')
                {
                    openCount++;
                    balanceCount++;
                }
                else if (s[i] == ')')
                {
                    if (balanceCount == 0) continue;
                    balanceCount--;
                }
                sb.Append(s[i]);
            }

            // Parse 2: Remove the rightmost "("
            string str = sb.ToString();
            sb = new StringBuilder();
            int BalancedOpenCount = openCount - balanceCount; // 需要幹掉的 = 左 - 右, 當需要幹掉 = 0 就是 不用了 直接塞值就好
            for (int i = 0; i < str.Length; i++)
            {
                if (str[i] == '(')
                {
                    if (BalancedOpenCount <= 0) continue;
                    BalancedOpenCount--;
                }
                sb.Append(str[i]);
            }

            return sb.ToString();
        }


        public struct KVStack
        {
            public int index; public char c;

            public KVStack(int index, char c)
            {
                this.index = index;
                this.c = c;
            }
        }


        public static string MinRemoveToMakeValid2(string s)
        {

            List<KVStack> stack = new List<KVStack>();

            for (int i = 0; i < s.Length; i++)
            {
                if (s[i] == '(')
                {
                    stack.Add(new KVStack(i, s[i]));
                }
                if (s[i] == ')')
                {
                    if (stack.Count != 0)
                    {
                        char top = stack[stack.Count - 1].c;
                        if (top == '(')
                        {
                            stack.RemoveAt(stack.Count - 1);
                        }
                        else
                        {
                            stack.Add(new KVStack(i, s[i]));
                        }
                    }
                    else
                    {
                        stack.Add(new KVStack(i, s[i]));
                    }

                }


            }

            // List<int> removeIndex = new List<int>();
            //while (stack.Count != 0) {
            //    removeIndex.Add(stack.Pop().index);
            //}
            stack.Sort((x, y) => {
                if (x.index < y.index) return -1;
                else if (x.index == y.index) return 0;
                else return 1;
            });


            // removeIndex.Sort();

            StringBuilder sbuilder = new StringBuilder(s);

            for (int i = stack.Count - 1; i >= 0; i--)
            {
                //s =  s.Remove(removeIndex[i], 1);
                sbuilder.Remove(stack[i].index, 1);
            }




            return sbuilder.ToString();


        }

    }
}
