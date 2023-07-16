using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace leetcode_015
{
    class Program
    {
        /// <summary>
        /// leetcode _ 015 3Sum
        /// https://leetcode.com/problems/3sum/
        /// 三数之和
        /// https://leetcode.cn/problems/3sum/
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            int[] array1 = new int[] { -1, 0, 1, 2, -1, -4 };
            //Console.WriteLine(ThreeSum(array1));
            ThreeSum(array1);
            Console.ReadKey();

        }


        /// <summary>
        /// https://ithelp.ithome.com.tw/articles/10219594
        /// 
        /// ILIST
        /// https://docs.microsoft.com/zh-tw/dotnet/api/system.collections.ilist?view=net-6.0
        /// 
        /// LIST<T>
        /// https://docs.microsoft.com/zh-tw/dotnet/api/system.collections.generic.ilist-1?view=net-6.0
        /// 
        /// 可參考, 排序 + 雙指針 概念
        /// https://leetcode.cn/problems/3sum/solution/san-shu-zhi-he-by-leetcode-solution/
        /// 「不重复」的本质是什么？我们保持三重循环的大框架不变，只需要保证：
        /// 第二重循环枚举到的元素不小于当前第一重循环枚举到的元素；
        /// 第三重循环枚举到的元素不小于当前第二重循环枚举到的元素。
        /// 三個不能取同一個位置的數值, 需要不同位置
        /// => 排序
        /// 
        /// 可搭配網址圖片比較好理解, 三個相對位置
        /// </summary>
        /// <param name="nums"></param>
        /// <returns></returns>
        public static IList<IList<int>> ThreeSum(int[] nums)
        {
            List<IList<int>> result = new List<IList<int>>();
            List<int> temp;

            Array.Sort(nums);

            // first不會與second與third重疊
            for (int first = 0; first < nums.Length - 2; first++)
            {
                // The numbers of first, second, third should be indexes.
                if (first > 0 && nums[first] == nums[first - 1])
                {
                    // 項目7 另外判斷 first 是否已經重複，若重複則跳過此次迴圈，因為答案也會是一樣的
                    continue;
                }

                /* // 需要注意 first > 0 需要寫在前面 否則會 出錯 邊界有問題
                if (nums[first] == nums[first - 1] && first > 0)
                {
                    // 項目7 另外判斷 first 是否已經重複，若重複則跳過此次迴圈，因為答案也會是一樣的
                    continue;
                }
                */

                // second 起始位置在first右邊
                int second = first + 1;

                // third 起始位置在 nums最後
                int third = nums.Length - 1;

                while (second < third)
                {
                    // 項目8 另外判斷 second 是否已經重複，若重複則 second++，並跳過此次迴圈，因為答案也會是一樣的 
                    if (nums[second] == nums[second - 1] && second > first + 1)
                    {
                        second++;
                        continue;
                    }
                    int sum = nums[first] + nums[second] + nums[third];
                    if (sum == 0)
                    {
                        temp = new List<int>
                        {
                            nums[first],
                            nums[second],
                            nums[third]
                        };
                        result.Add(temp);
                        second++;
                        third--;
                    }
                    else if (sum < 0)
                    {
                        second++;
                    }
                    else
                    {
                        third--;
                    }
                }
            }
            //List<string> strings = result.ConvertAll<string>(x => x.ToString());
            //Console.WriteLine(String.Join(", ", strings));

            //List<string> strings = result.Select(i => i.ToString()).ToList();
            //Console.WriteLine(String.Join(", ", strings));

            //foreach (var dinosaur in result)
            //{
            //    Console.WriteLine("After: " + dinosaur);
            //}
            //for (int i = 0; i < result.Count; i++)
            //{
            //    Console.WriteLine($" {result[i]}");
            //}

            // result 裡面 還有一組 temp的list
            // 所以要跑兩次 才能輸出
            foreach (var temp2 in result)
            {
                Console.WriteLine("====================");
                foreach(var out2 in temp2)
                {
                    Console.WriteLine("輸出結果: " + out2);
                }
            }

            return result;

        }


    }
}
