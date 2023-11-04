using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace leetcode_421
{
    internal class Program
    {
        /// <summary>
        /// 421. Maximum XOR of Two Numbers in an Array
        /// https://leetcode.com/problems/maximum-xor-of-two-numbers-in-an-array/
        /// 421. 数组中两个数的最大异或值
        /// https://leetcode.cn/problems/maximum-xor-of-two-numbers-in-an-array/?envType=daily-question&envId=Invalid%20Date
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            int[] input = { 3, 10, 5, 25, 2, 8 };
            Console.WriteLine(FindMaximumXOR(input));
            Console.ReadKey();
        }

        // 2進位最高位置編號為30
        // 左邊為低位, 左邊為高位
        // 長度共 30
        const int HIGH_BIT = 30;

        /// <summary>
        /// XOR 邏輯 基本知識 複習
        /// https://teatime28.pixnet.net/blog/post/338679642-and%E3%80%81or%E3%80%81nand%E3%80%81nor%E3%80%81xor%E3%80%81xnor-%E7%9C%9F%E5%80%BC%E8%A1%A8(true-table)%E6%95%B4
        /// 
        /// A 	B 	A XOR B
        /// 0 	0 	  0
        /// 0 	1 	  1
        /// 1 	0 	  1
        /// 1 	1 	  0
        /// 兩者相同為0, 其中一個為1才會為1
        /// 
        /// 解决这个问题，我们首先需要利用异或运算的一个性质：
        /// 如果 a ^ b = c 成立，那么a ^ c = b 与 b ^ c = a 均成立。
        /// 
        /// 方法1:哈希表
        /// https://leetcode.cn/problems/maximum-xor-of-two-numbers-in-an-array/solutions/778291/shu-zu-zhong-liang-ge-shu-de-zui-da-yi-h-n9m9/?envType=daily-question&envId=Invalid+Date
        /// https://leetcode.cn/problems/maximum-xor-of-two-numbers-in-an-array/solutions/9289/li-yong-yi-huo-yun-suan-de-xing-zhi-tan-xin-suan-f/?envType=daily-question&envId=Invalid+Date
        /// 我们只用关心这个最大的异或值需要满足什么性质，进而推出这个最大值是
        /// 什么，而不必关心这个异或值是由哪两个数得来的。
        /// 
        /// aj​=x⊕ai​
        /// 假設x為已知為1 那麼只要把aj放入hash中 計算 x⊕ai​ 是不是aj 即可
        /// 
        /// 计算 prek(x) 可以使用右移运算 >>。  往右移動除法 變小
        /// 这 31 个二进制位从低位到高位依次编号为 0,1,⋯ ,30。我们从最高位第 30
        /// 个二进制位开始，依次确定 x 的每一位是 0 还是 1；
        /// 
        /// shift 相關知識
        /// https://learn.microsoft.com/zh-tw/dotnet/csharp/language-reference/operators/bitwise-and-shift-operators#right-shift-operator-
        /// 
        /// </summary>
        /// <param name="nums"></param>
        /// <returns></returns>
        public static int FindMaximumXOR(int[] nums)
        {
            int x = 0;
            // 判斷每位置即可, 是0還是1
            // 預設他為1, 不存在就給0
            for(int k = HIGH_BIT; k >= 0; k--)
            {
                ISet<int> seen = new HashSet<int>();
                // 將所有的 pre^k(a_j) 放入 hash 表裡面
                foreach (int num in nums)
                {
                    // 如果只想保留從最高位開始到第 k 個2進位為止的部分
                    // 只需將其右移 k 位
                    // 取該位置的bit就好
                    seen.Add(num >> k);
                }

                // 目前 x 包含從最高位開始到第 k + 1 個2進位為止的部分
                // 我們將 x 的第 k 個2進位位置為 1 , 即為 x = x * 2 + 1
                int xNext = x * 2 + 1;
                bool found = false;

                // 枚舉 i
                foreach (int num in nums)
                {
                    if (seen.Contains(xNext ^ (num >> k)))
                    {
                        // 判斷該位置是不是存在
                        found = true;
                        break;
                    }
                }

                if (found)
                {
                    x = xNext;
                }
                else
                {
                    // 如果沒有找到滿足等式的 a_i 和 a_j 那麼 x 的第 k 個2進位只能為 0
                    // 即為 x = x * 2
                    x = xNext - 1;
                }

            }

            return x;

        }

    }
}
