using DotNetty.Common.Utilities;
using Lucene.Net.Util;
using NetTopologySuite.Index.HPRtree;
using NetTopologySuite.Utilities;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace leetcode_347
{
    internal class Program
    {
        /// <summary>
        /// leetcode 347 . Top K Frequent Elements
        /// https://leetcode.com/problems/top-k-frequent-elements/
        /// 347. 前 K 个高频元素
        /// https://leetcode.cn/problems/top-k-frequent-elements/
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            int[] input = { 1, 1, 1, 2, 2, 3 };
            int k = 1;

            //Console.WriteLine(TopKFrequent2(input, k));
            TopKFrequent2(input, k);
            Console.ReadKey();

        }


        /// <summary>
        /// Dictionary 統計次數
        /// 
        /// </summary>
        /// <param name="nums"></param>
        /// <param name="k"></param>
        /// <returns></returns>
        public static int[] TopKFrequent(int[] nums, int k)
        {
            Dictionary<int, int> dic = new Dictionary<int, int>();
            foreach (int x in nums) 
            {
                if(dic.ContainsKey(x))
                {
                    dic[x]++;
                }
                else
                {
                    
                    dic.Add(x, 1);
                }
            }

            var sort = dic.OrderByDescending(x => x.Value);
            //StringBuilder result = new StringBuilder();
            int[] result2 = new int[k];
            int tid = 0;
            bool isadd = false;
            foreach (var item in sort)
            {
                if (item.Value >= k)
                {
                    //for (int i = 0; i < item.Value; i++)
                    //{
                    //    result = result.Append(item.Key);
                    //}
                    //result = result.Append(item.Key);
                    //result2.Append(item.Key);

                    result2[tid] = item.Key;
                    tid++;
                    isadd = true;
                }
            }

            if (isadd == false)
            {
                for (int i = 0; i < k; i++)
                {
                    result2[i] = nums[i];
                }
            }

            //Console.WriteLine(result.ToString());
            //int[] output = new int[nums.Length];
            //output = new[] { int.Parse(result.ToString()) };

            //for(int i = 0; i < output.Length; i++)
            //{
            //    Console.WriteLine(output[i]);
            //    output.a
            //}

            for (int i = 0; i < result2.Length; i++)
            {
                Console.WriteLine(result2[i]);
            }

            //return new[] { int.Parse(result.ToString() )};
            return result2;


        }


        /// <summary>
        /// 解法三：桶排序法
        /// 首先依旧使用哈希表统计频率，统计完成后，创建一个数组，将频率作为数组下标，对于出现频率
        /// 不同的数字集合，存入对应的数组下标即可。
        /// https://leetcode.cn/problems/top-k-frequent-elements/solution/leetcode-di-347-hao-wen-ti-qian-k-ge-gao-pin-yuan-/
        /// 
        /// 先用字典获取频数，再以频数为键构建列表，最后再反向读取列表中的数据到K个即
        /// https://leetcode.cn/problems/top-k-frequent-elements/solution/xian-yong-zi-dian-huo-qu-pin-shu-zai-yi-pin-shu-we/
        /// </summary>
        /// <param name="nums"></param>
        /// <param name="k"></param>
        /// <returns></returns>
        public static int[] TopKFrequent2(int[] nums, int k)
        {
            // 使用字典，统计每个元素出现的次数，元素为键，元素出现的次数为值
            Dictionary<int, int> dic = new Dictionary<int, int>();
            for (int i = 0; i < nums.Length; ++i)
            {
                if (dic.ContainsKey(nums[i]))
                {
                    dic[nums[i]]++;
                }
                else
                {
                    dic.Add(nums[i], 1);
                }
            }

            // 桶排序
            // 将频率作为数组下标，对于出现频率不同的数字集合，存入对应的数组下标
            List<int>[] lists = new List<int>[nums.Length + 1];
            foreach (var item in dic)
            {
                // 获取出现的次数作为下标
                if (lists[item.Value] == null)
                {
                    lists[item.Value] = new List<int>();
                }
                lists[item.Value].Add(item.Key);
            }

            // 倒序遍历数组获取出现顺序从大到小的排列
            // 小於k的由大至小撈取出來, 撈取出來個數要跟k相等
            // 撈取 key 不重覆
            List<int> res = new List<int>();
            for (int i = lists.Length - 1; i >= 0; --i)
            {
                if (lists[i] == null)
                {
                    continue;
                }

                // i : 次數 !? 頻率
                // j : nums 輸入數字
                int j = 0;
                while (res.Count < k)
                {
                    var temp_value = lists[i][j];
                    Console.WriteLine("temp_value: " + temp_value);
                    
                    res.Add(lists[i][j]);
                    
                    j++;
                    if (j >= lists[i].Count)
                    {
                        // 避免取空 null 出錯
                        break;
                    }
                }
            }

            // 輸出顯示
            Console.Write("[ ");
            foreach (var str in res)
            {
                
                if(res.Count > 1)
                {
                    Console.Write(str + ", ");
                }
                else
                {
                    Console.Write(str);
                }
            }
            Console.Write(" ]");

            return res.ToArray();
        }


    }
}
