using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace leetcode_080
{
    internal class Program
    {
        /// <summary>
        /// 80. Remove Duplicates from Sorted Array II
        /// https://leetcode.com/problems/remove-duplicates-from-sorted-array-ii/
        /// 80. 删除有序数组中的重复项 II
        /// https://leetcode.cn/problems/remove-duplicates-from-sorted-array-ii/
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            int[] input = { 1, 1, 1, 2, 2, 3 };
            //Console.WriteLine(RemoveDuplicates(input));

            RemoveDuplicates2(input);

            Console.ReadKey();
        }


        /// <summary>
        /// 網路上神奇解法
        /// 參考
        /// https://leetcode.cn/problems/remove-duplicates-from-sorted-array-ii/solutions/702970/gong-shui-san-xie-guan-yu-shan-chu-you-x-glnq/
        /// 通用解法 for 保留 k 位數
        /// 
        /// 由于是保留 k 个相同数字，对于前 k 个数字，我们可以直接保留
        /// 对于后面的任意数字，能够保留的前提是：
        /// 与当前写入的位置前面的第 k 个元素进行比较，不相同则保留
        /// 
        /// </summary>
        /// <param name="nums"></param>
        /// <returns></returns>
        public static int RemoveDuplicates(int[] nums)
        {
            return process(nums, 2);

        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="nums"></param>
        /// <param name="k"></param>
        /// <returns></returns>
        public static int process (int[] nums, int k)
        {
            int u = 0;

            foreach (int x in nums) 
            {
                if(u < k || nums[u - k] != x)
                {
                    nums[u++] = x;
                }
            }

            return u;
        }


        /// <summary>
        /// 官方解法
        /// https://leetcode.cn/problems/remove-duplicates-from-sorted-array-ii/solutions/702644/shan-chu-pai-xu-shu-zu-zhong-de-zhong-fu-yec2/
        /// https://leetcode.cn/problems/remove-duplicates-from-sorted-array-ii/solutions/2855042/bao-mu-ji-de-bian-se-tu-shi-mo-ni-c-pyth-zn6z/
        /// 
        /// 同樣的數字最多保留兩個,
        /// 輸入的序列是有排序過的
        /// 不需要考虑数组中超出新长度后面的元素。
        /// 
        /// 我们定义两个指针 slow 和 fast 分别为慢指针和快指针，
        /// 其中慢指针表示处理出的数组的长度，快指针表示已经检查过的数组的长度
        /// ，即 nums[fast] 表示待检查的第一个元素
        /// ， nums[slow - 2] 为上一个应该被保留的元素所移动到的指定位置。
        /// 
        ///  快指針: 簡單說就是 遍歷 陣列 nums
        ///  慢指針: 用於將不同 element 填入結果 array
        /// 
        /// 請參考類似題目
        /// Leetcode_026
        /// Leetcode_027
        /// 
        /// 因為相同 element 只能出現兩次
        /// 所以當出現第三次時候, 要進行替換
        /// 把第三次的 index 與 下一個不同 element value index 交換
        /// 
        /// 輸入的 nums 已經排序過遞增, 所以相同元素.
        /// 一定在隔壁而已
        /// 只需要處理相同 > 2 即可
        /// 從第三個相同的 element 開始處理
        /// 
        /// </summary>
        /// <param name="nums"></param>
        /// <returns></returns>
        public static int RemoveDuplicates2(int[] nums)
        {
            int n = nums.Length;
            // 最多保留兩個, 距離直接設定2
            int fast = 2, slow = 2;

            if(n <= 2)
            {
                return n;
            }

            while(fast < n)
            {
                // 相同元素最多只能出現兩次 
                if (nums[slow - 2] != nums[fast])
                {
                    // 把超過兩次的 index value 交換
                    nums[slow] = nums[fast];
                    slow++;
                }
                fast++;
            }

            Console.WriteLine("長度:" + slow);
            Console.WriteLine();
            Console.Write("修正後: [");
            foreach (int x in nums)
            {
                Console.Write(x + ", ");
            }
            Console.WriteLine("]");

            return slow;

        }

    }
}
