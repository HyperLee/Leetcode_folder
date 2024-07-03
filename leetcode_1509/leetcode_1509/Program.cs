namespace leetcode_1509
{
    internal class Program
    {
        /// <summary>
        /// 1509. Minimum Difference Between Largest and Smallest Value in Three Moves
        /// https://leetcode.com/problems/minimum-difference-between-largest-and-smallest-value-in-three-moves/description/?envType=daily-question&envId=2024-07-03
        /// 1509. 三次操作后最大值与最小值的最小差
        /// https://leetcode.cn/problems/minimum-difference-between-largest-and-smallest-value-in-three-moves/description/
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            int[] input = { 1, 5, 0, 10, 14};
            Console.WriteLine(MinDifference(input));
            Console.ReadKey();
        }


        /// <summary>
        /// https://leetcode.cn/problems/minimum-difference-between-largest-and-smallest-value-in-three-moves/solutions/336428/san-ci-cao-zuo-hou-zui-da-zhi-yu-zui-xiao-zhi-de-2/
        /// https://leetcode.cn/problems/minimum-difference-between-largest-and-smallest-value-in-three-moves/solutions/326880/minimum-difference-by-ikaruga/
        /// https://leetcode.cn/problems/minimum-difference-between-largest-and-smallest-value-in-three-moves/solutions/1530726/by-stormsunshine-pqnh/
        /// https://leetcode.cn/problems/minimum-difference-between-largest-and-smallest-value-in-three-moves/solutions/824266/c-kan-diao-zhu-mu-lang-ma-feng-tian-ping-lf0m/
        /// 長度4以內肯定都可以為0
        /// 因為可以修改三個數字
        /// 只要讓四個數字都相同
        /// 就可以使得最大最小差異值為 0
        /// 
        /// 需要注意的是長度超過四的狀況
        /// 將最大數變小 or 最小值變大
        /// 其實就跟刪除差不多意思
        /// 長度三, 然後最大最小要比較
        /// 共有下面四種方式
        /// 1. 三個最大
        /// 2. 兩個最大, 一個最小
        /// 3. 一個最大, 兩個最小
        /// 4.  三個最小
        /// 
        /// 詳細可以多參考幾篇文章
        /// 流程數學推導
        /// 
        /// </summary>
        /// <param name="nums"></param>
        /// <returns></returns>
        public static int MinDifference(int[] nums)
        {
            int n = nums.Length;

            if(n <= 4)
            {
                // 最多可修改3個數字,
                // 所以當長度為4時候都改相同數字
                // 就可以讓 差異值 變為 0
                return 0;
            }

            // 排序; 可得出 最大與最小數值
            Array.Sort(nums);
            // 取最小值, 預設要給最大
            int res = int.MaxValue;

            // 從四種方式中, 最大與最小中找出最小差異值
            for(int i = 0; i < 4; i++)
            {
                // 最多移動三次
                //var temp = nums[n - 4 + i] - nums[i];
                res = Math.Min(res, nums[n - 4 + i] - nums[i]);
            }

            return res;
        }
    }
}
