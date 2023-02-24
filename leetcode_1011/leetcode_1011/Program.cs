using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace leetcode_1011
{
    internal class Program
    {
        /// <summary>
        /// leetcode 1011 Capacity To Ship Packages Within D Days
        /// 在 D 天内送达包裹的能力
        /// https://leetcode.com/problems/capacity-to-ship-packages-within-d-days/description/
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            int[] weight = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
            int days = 5;


            Console.WriteLine(ShipWithinDays(weight, days));
            Console.ReadKey();
        }


        /*
            要在 D 天内运完所有包裹，那么每天至少的承载量为 sum / D
            但是，因为一次至少运 1 个包裹，而这个包裹的重量可大可小，那么可能 weights[i] > sum / D
            假设包裹最大的重量为 maxWeight
            因此，最低承载量应该为 capacity = max(sum / D, maxWeight);

            最直接的方法，就是直接从 capacity 开始，每次 capacity++ 进行尝试，但这样效率很低
            因此我们可以使用二分查找， left = capacity， right = sum，即最低承载量为 capacity,最高的承载量为 包裹总量
            我们判断承载量 mid 是否能在 D 天内装完（无需刚好 D 天，只需要 [1, D] 即可），如果不能，表示承载量太小
            ，则 left = mid + 1，否则 right = mid
            因为必定存在一个答案，因此当退出循环， left = right 时，就是答案
        */

        /// <summary>
        /// https://leetcode.cn/problems/capacity-to-ship-packages-within-d-days/solution/zai-d-tian-nei-song-da-bao-guo-de-neng-l-ntml/
        /// 改寫C語言 解法
        /// 
        /// 二分查找的初始左右边界应当如何计算呢？
        /// 对于左边界而言，由于我们不能「拆分」一个包裹，因此船的运载能力不能小于所有包裹中最重的那个的重
        /// 量，即左边界为数组 weights 中元素的最大值。
        /// 
        /// 对于右边界而言，船的运载能力也不会大于所有包裹的重量之和，即右边界为数组 weights 中元素的和。
        /// 
        /// 二分查找，根据题意，结果一定落在【max(weights), sum(weights）】
        /// 这个区间之间，因为左端点对应每天一条船，右端点对应只有一条超级大船。
        ///  然后利用二分法，每一次循环模拟运货的过程，然后根据需要的天数与 输入 D 的关系来调整区间左右端点。
        /// </summary>
        /// <param name="weights"></param>
        /// <param name="days"></param>
        /// <returns></returns>
        public static int ShipWithinDays(int[] weights, int days)
        {
            int left = 0, right = 0;
            for(int i = 0; i < weights.Length; i++)
            {
                left = Math.Max(left, weights[i]);
                right += weights[i];
            }

            // 在[maxWei, sumWei]内进行二分查找，寻找能D天运完包裹的单天运载能力的最小值
            while (left < right)
            {
                // mid 即为当前运送的capacity
                // 二分法取法 可以查看wiki資訊 , 此兩方法 意思一樣, 下面方法為避免溢位而已
                //int mid = (left + right) / 2;
                int mid = left + ((right - left) / 2); 

                // need:需要運送天數, cur:當前這一天運送貨物重量之加總
                int need = 1, cur = 0; 

                for(int i = 0; i < weights.Length; i++)
                {
                    // 当天已超重
                    if (cur + weights[i] > mid)
                    {
                        // 当前货运不动 需要新的一艘船才行 !?
                        ++need;
                        cur = 0;
                    }

                    // 沒超重 繼續裝載
                    cur += weights[i];
                }

                if (need <= days)
                {
                    //  需要天数小于等于D，说明负载能力可能过剩
                    //  这里不是right = mid-1，因为mid是一个合法的结果，不能丢弃
                    right = mid;
                }
                else
                {
                    // 当前的capacity太小了，不够，需要更大容量才能及时运完
                    // 負重能力不足,需要更多天數
                    left = mid + 1;
                }
            }

            return left;



        }

    }
}
