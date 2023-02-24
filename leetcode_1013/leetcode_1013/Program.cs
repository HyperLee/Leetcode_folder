using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace leetcode_1013
{
    internal class Program
    {
        /// <summary>
        /// leetcode 1013 Partition Array Into Three Parts With Equal Sum
        /// https://leetcode.com/problems/partition-array-into-three-parts-with-equal-sum/
        /// 
        /// 
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            int[] input = new int[] { 0, 2, 1, -6, 6, -7, 9, 1, 2, 0, 1 };

            Console.WriteLine(CanThreePartsEqualSum(input));

            Console.ReadKey();

        }



        /// <summary>
        /// https://leetcode.cn/problems/partition-array-into-three-parts-with-equal-sum/solution/1013-jiang-shu-zu-fen-cheng-he-xiang-deng-de-san-2/
        /// 
        /// https://leetcode.cn/problems/partition-array-into-three-parts-with-equal-sum/solution/by-mulberry_qs-7utz/
        /// 本題解法 前綴和概念 
        /// 前n個加總得和
        /// 
        /// 1. 将数组转化为前面下标值的和并且加上自身 <前綴和 前面和之加總>
        ///     例：[0,2,1,-6,6,-7,9,1,2,0,1] -> [0,2,3,-3,3,-4,5,6,8,8,9]
        /// 2. 判断是否可以三等分，不存在三等分直接返回false
        /// 3. 获得总和被三等分的值并且定义一个计数器为1
        /// 4. 因为数组转化为前面值的和，所以总和三等分后，获取分割点就可以转化为
        ///    分割点值 = 三等分单值 * 计数器
        /// 5. 因为数组最后的一个值肯定是三等分单值 * 3，所以不需要遍历到最后一个 
        ///     避免[1,-1,1,-1]的情况
        /// 6. 判断计数器是否为3
        /// </summary>
        /// <param name="arr"></param>
        /// <returns></returns>
        public static bool CanThreePartsEqualSum(int[] arr)
        {
            int length = arr.Length;
            for (int i = 1; i < length; i++)
            {
                arr[i] += arr[i - 1];
            }

            // 前 i個之和 總和sum % 3是不是 等於0
            // 0: 可以三等分,否則為否
            // 总和不是3的倍数，直接返回false
            if (arr[length - 1] % 3 != 0)
            {
                return false;
            }

            // 获得总和被三等分的值并且定义一个计数器为1
            int sum = arr[length - 1] / 3;
            int time = 1;

            // 因为数组最后的一个值肯定是三等分单值 * 3 所以不需要遍历到最后一个
            for (int i = 0; i < length - 1 && time < 3; i++)
            {
                // 分割点值 = 三等分单值 * 计数器
                if (arr[i] == sum * time)
                {
                    time++;
                }
            }

            return time == 3;
        }


    }
}
