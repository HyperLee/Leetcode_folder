using System.Diagnostics.CodeAnalysis;
using static System.Formats.Asn1.AsnWriter;

namespace Leetcode_2593
{
    internal class Program
    {
        /// <summary>
        /// 2593. Find Score of an Array After Marking All Elements
        /// https://leetcode.com/problems/find-score-of-an-array-after-marking-all-elements/description/?envType=daily-question&envId=2024-12-13
        /// 
        /// 2593. 标记所有元素后数组的分数
        /// https://leetcode.cn/problems/find-score-of-an-array-after-marking-all-elements/description/
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            int[] input = { 2, 1, 3, 4, 5, 2 };
            Console.WriteLine("res: " + FindScore(input));
            Console.ReadKey();
        }


        /// <summary>
        /// ref:
        /// https://leetcode.cn/problems/find-score-of-an-array-after-marking-all-elements/solutions/2177231/dai-zhao-xia-biao-qu-pai-xu-pythonjavacg-umuv/
        /// https://leetcode.cn/problems/find-score-of-an-array-after-marking-all-elements/solutions/2790961/2593-biao-ji-suo-you-yuan-su-hou-shu-zu-mqljy/
        /// 
        /// 根據題目描述方式
        /// 模擬其作法
        /// 0. 初始化 Score = 0
        /// 1. 選擇陣列中尚未標記的最小整數。如果有相同數值，則選擇索引最小的那一個。
        /// 2. 將選定整數的值加到 Score 中。
        /// 3. 標記選定的元素及其相鄰的兩個元素（如果存在）。
        /// 4. 重複上述步驟直到陣列中的所有元素都被標記為止。
        /// </summary>
        /// <param name="nums"></param>
        /// <returns></returns>
        public static long FindScore(int[] nums)
        {
            long Score = 0;
            int n = nums.Length;
            // 模擬描述
            int[][] numIndices = new int[n][];
            // 依照順序給予 [element_value][index]
            for(int i = 0; i < n; i++)
            {
                numIndices[i] = new int[2];
                // element_value
                numIndices[i][0] = nums[i];
                // index
                numIndices[i][1] = i;
            }

            // 排序, 排序的方法为首先按照元素值升序排序
            // ，当元素值相等时按照下标升序排序。
            Array.Sort(numIndices, (a, b) =>
            {
                if (a[0] != b[0])
                {
                    // asc
                    return a[0] - b[0];
                    // desc
                    //return b[0] - a[0];
                }
                else
                {
                    // asc
                    return a[1] - b[1];
                    // desc
                    //return b[1] - a[1];
                }
            });

            // hash 紀錄被標記 element index
            bool[] marked = new bool[n];
            for(int i = 0; i < n; i++)
            {
                int num = numIndices[i][0], index = numIndices[i][1];
                // 選定未標記項目
                if (!marked[index])
                {
                    Console.WriteLine("numIndices num: " + num);
                    // 加入 Score
                    Score += num;
                    // 加入後標記選定項目
                    marked[index] = true;

                    // 標定項目的左右兩邊 index 也要一同標記. (index - 1 與 index + 1)
                    if (index > 0)
                    {
                        // 左邊界標記
                        marked[index - 1] = true;
                    }

                    if(index < n - 1)
                    {
                        // 右邊界標記
                        // 注意因為是取 index + 1, 所以條件式是 n - 1.
                        // 不然會超出邊界
                        marked[index + 1] = true;
                    }
                }
            }

            return Score;
        }
    }
}
