using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace leetcode_046
{
    internal class Program
    {
        /// <summary>
        /// 46. Permutations
        /// https://leetcode.com/problems/permutations/
        /// 46. 全排列
        /// https://leetcode.cn/problems/permutations/
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            int[] nums = { 1, 2};
            //Console.WriteLine(Permute(nums));
            Permute(nums);
            Console.ReadKey();
        }


        public static IList<IList<int>> permutations = new List<IList<int>>();
        public static IList<int> temp = new List<int>(); //  用于存储当前排列; 將nums依序塞入temp生成全排列
        public static int n; // nums長度


        /// <summary>
        /// https://leetcode.cn/problems/permutations/solution/by-stormsunshine-rw7r/
        /// 
        /// https://leetcode.cn/problems/permutations/solution/quan-pai-lie-by-leetcode-solution-2/
        /// 回到上一層節點時候需要狀態重置, 請看影片04:00
        /// 
        /// </summary>
        /// <param name="nums"></param>
        /// <returns></returns>
        public static IList<IList<int>> Permute(int[] nums)
        {
            foreach (int num in nums)
            {
                temp.Add(num);
            }

            n = nums.Length;

            Backtrack(0);


            foreach(var value in permutations)
            {
                Console.Write("[");
                foreach(var item in value)
                {
                    Console.Write(item + ", ");
                }
                Console.Write("]");
                Console.WriteLine(" ");

            }

            return permutations;

        }


        /// <summary>
        /// 遞迴, 回朔 排列 
        /// 
        /// 如果 index = n，则当前排列中的所有元素都已经确定，将当前排列添加到结果列表中。
        /// 如果 index < n，则对于每个 index ≤ i < n，执行如下操作。
        /// 1. 将 temp[index] 和 temp[i] 的值交换，然后将 index+1 作为当前下标继续搜索。
        /// 2. 将 temp[index] 和 temp[i] 的值再次交换，恢复到交换之前的状态。
        /// 
        /// https://leetcode.cn/problems/permutations/solution/quan-pai-lie-by-leetcode-solution-2/
        /// 回到上一層節點時候需要狀態重置, 請看影片04:00
        /// 類似樹狀結構的DFS, 回到上一層,從上一層再往其他層推 所以需要 資料重製
        /// </summary>
        /// <param name="index"></param>
        public static void Backtrack(int index)
        {
            if (index == n)
            {
                // 排列組合完成加入
                permutations.Add(new List<int>(temp));
            }
            else
            {
                for (int i = index; i < n; i++)
                {
                    // 找出可能排列
                    Swap(temp, index, i);
                    // index+1 作为当前下标继续搜索
                    Backtrack(index + 1);
                    // 撤銷排列; 狀態重置
                    Swap(temp, index, i);

                }

            }

        }


        /// <summary>
        /// 为了得到数组 nums 的全排列，需要依次确定排列中的每个位置的元素
        /// ，可以通过交换 temp 中的元素实现。
        /// 
        /// </summary>
        /// <param name="temp"></param>
        /// <param name="index1"></param>
        /// <param name="index2"></param>
        public static void Swap(IList<int> temp, int index1, int index2)
        {
            int curr = temp[index1];
            temp[index1] = temp[index2];
            temp[index2] = curr;

        }


    }
}
