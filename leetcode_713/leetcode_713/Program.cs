namespace leetcode_713
{
    internal class Program
    {
        /// <summary>
        /// 713. Subarray Product Less Than K
        /// https://leetcode.com/problems/subarray-product-less-than-k/?envType=daily-question&envId=2024-03-27
        /// 
        /// 713. 乘积小于 K 的子数组
        /// https://leetcode.cn/problems/subarray-product-less-than-k/description/
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            int[] input = { 10, 5, 2, 6 };
            int k = 100;
            Console.WriteLine(NumSubarrayProductLessThanK(input, k));
            Console.ReadKey();
        }



        /// <summary>
        /// 滑動視窗概念題型
        /// https://leetcode.cn/problems/subarray-product-less-than-k/solutions/1463527/cheng-ji-xiao-yu-k-de-zi-shu-zu-by-leetc-92wl/
        /// https://leetcode.cn/problems/subarray-product-less-than-k/solutions/1732841/by-stormsunshine-628t/
        /// 
        /// [start, end] => 把這視窗(範圍)持續往右走, 找出新的組合
        /// 
        /// end一直往右走, 表示將乘積變大
        /// 當發現乘積超過 k
        /// 此時把start往右走, 可縮小乘積數值
        /// 
        /// 要保持[start, end]不能超過乘積
        /// 兩者持續往右走, 找出新的sub-array
        /// 
        /// 
        /// end往右走是乘法, 因為加入新的element.
        /// start往右走是除法, 因為去除該element.
        /// </summary>
        /// <param name="nums"></param>
        /// <param name="k"></param>
        /// <returns></returns>
        public static int NumSubarrayProductLessThanK(int[] nums, int k)
        {
            int count = 0;
            int product = 1;
            int length = nums.Length;
            int start = 0, end = 0;

            while(end < length) 
            {
                // 加入新的end element
                product *= nums[end];

                while(start <= end && product >= k)
                {
                    // 當 product太大, 
                    // 把 start往右移動(去除該element), 也可說是除法.表示乘積太大
                    product /= nums[start];
                    start++;
                }

                // 累計 計算sub-array 數量
                count += end - start + 1;
                // end 往右走
                end++;
            }

            return count;
        }
    }
}
