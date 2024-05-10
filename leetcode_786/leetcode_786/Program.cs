using System;
using System.ComponentModel;
using System.Data.Common;
using System.Xml.Linq;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace leetcode_786
{
    internal class Program
    {
        /// <summary>
        /// 786. K-th Smallest Prime Fraction
        /// https://leetcode.com/problems/k-th-smallest-prime-fraction/?envType=daily-question&envId=2024-05-10
        /// 786. 第 K 个最小的质数分数
        /// https://leetcode.cn/problems/k-th-smallest-prime-fraction/description/
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            int[] input = { 1, 2, 3, 5 };
            int k = 3;

            KthSmallestPrimeFraction(input, k);

            Console.ReadKey();
        }



        /// <summary>
        /// ref: 方法一：自定义排序
        /// https://leetcode.cn/problems/k-th-smallest-prime-fraction/solutions/1127103/di-k-ge-zui-xiao-de-su-shu-fen-shu-by-le-argw/
        /// https://leetcode.cn/problems/k-th-smallest-prime-fraction/solutions/1127751/gong-shui-san-xie-yi-ti-shuang-jie-you-x-8ymk/
        /// https://leetcode.cn/problems/k-th-smallest-prime-fraction/solutions/2726838/786-di-k-ge-zui-xiao-de-zhi-shu-fen-shu-pu5wt/
        /// 
        /// 此方法需要注意
        /// 正常比較方法是
        /// a/b 與 c/d 比較大小
        /// 但是這邊用
        /// a * d < b * c 來取代上述方法計算比較
        /// 因浮點數計算會有誤差問題
        /// 詳細推導方式 要去看上述ref連結說明
        /// 
        /// 以长度为 2 的整数数组返回你的答案, 这里 answer[0] == arr[i] 且 answer[1] == arr[j] 。
        /// </summary>
        /// <param name="arr">輸入資料, array</param>
        /// <param name="k">第K個最小分數</param>
        /// <returns></returns>
        public static int[] KthSmallestPrimeFraction(int[] arr, int k)
        {
            int n = arr.Length;
            List<int[]> list = new List<int[]>();

            for (int i = 0; i < n; i++)
            {
                for(int j = i + 1; j < n; j++)
                {
                    // list裡面的int[][] 陣列是二維陣列; 將 int[] arr 資料塞入 list[] 裡面 
                    list.Add(new int[] { arr[i], arr[j] });
                }
            }

            // 排序 升序排序 小至大; a * d < b * c
            list.Sort((x, y) => x[0] * y[1] - y[0] * x[1]);

            int count = 0;
            // 使用 foreach 迴圈來迭代 List 中的每個陣列
            foreach (int[] array in list)
            {
                // 使用內嵌的 foreach 迴圈來迭代每個陣列中的元素
                foreach (int element in array)
                {
                    // 輸出第 k - 1 筆資料
                    if (count == k - 1)
                    {
                        Console.Write(element + ", ");
                    }
                }

                count++;
                //Console.WriteLine(); // 換行以分隔每個陣列
            }
            
            return list[k - 1];
        }
    }
}
