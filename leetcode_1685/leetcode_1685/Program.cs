using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace leetcode_1685
{
    internal class Program
    {
        /// <summary>
        /// 1685. Sum of Absolute Differences in a Sorted Array
        /// https://leetcode.com/problems/sum-of-absolute-differences-in-a-sorted-array/?envType=daily-question&envId=2023-11-25
        /// 1685. 有序数组中差绝对值之和
        /// https://leetcode.cn/problems/sum-of-absolute-differences-in-a-sorted-array/
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            int[] input = { 2, 3, 5 };
            //Console.Write(GetSumAbsoluteDifferences(input));
            var res = GetSumAbsoluteDifferences(input);
            foreach(int i in res)
            {
                Console.Write(i + ", ");
            }



            Console.ReadKey();
        }


        /// <summary>
        /// ref:
        /// 1. https://leetcode.cn/problems/sum-of-absolute-differences-in-a-sorted-array/solutions/2223451/jian-dan-yi-dong-qian-zhui-he-by-kind-mo-35ub/
        /// 2. https://leetcode.cn/problems/sum-of-absolute-differences-in-a-sorted-array/solutions/1702898/by-stormsunshine-gtx2/
        /// 
        /// 我们要计算 nums 数组中第 i 个数与其他数的绝对差值之和，可以将其拆分为三部分：
        /// 1. 与前面 i - 1 个数的绝对差值之和。  ==>前綴和
        /// 2. 与后面 n - i 个数的绝对差值之和。  ==>後綴和
        /// 3. 与自身的绝对差值（为 0）。         ==>自己與自己的差異, 會為0. 自己相減取絕對值
        /// 
        /// 本寫法參考c#範例
        /// 
        /// 从左到右遍历数组 nums，对于下标 i，执行如下操作。
        /// 1. 计算 result[i] = (rightSum − leftSum) + nums × (2 × i − n )。
        /// 2. 将 leftSum 增加 nums[i] ，将 rightSum 减少 nums[i] 。
        /// 遍历结束之后即可得到结果数组 result。
        /// 
        /// 公式推倒部分 需要多加研究. 目前初略看懂 但自己推不太會推
        /// </summary>
        /// <param name="nums"></param>
        /// <returns></returns>
        public static int[] GetSumAbsoluteDifferences(int[] nums)
        {
            int n = nums.Length;
            int[] result = new int[n];
            // 左右分別是前 i - 1 總和 與 n - i 總和
            int left = 0, right = 0;

            for(int i = 0; i < n; i++)
            {
                // 右邊取加總之後值
                right += nums[i];
            }

            for(int i = 0; i < n; i++)
            {
                // 公式推導出來
                result[i] = right - left + nums[i] * (2 * i - n);
                left += nums[i];
                right -= nums[i];
            }

            return result;
        }

    }
}
