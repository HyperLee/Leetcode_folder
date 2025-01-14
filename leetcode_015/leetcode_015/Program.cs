namespace leetcode_015
{
    internal class Program
    {
        /// <summary>
        /// 15. 3Sum
        /// https://leetcode.com/problems/3sum/
        /// 
        /// 15. 三数之和
        /// https://leetcode.cn/problems/3sum/
        /// 
        /// 給定一個整數陣列 nums，返回所有滿足以下條件的三元組 [nums[i], nums[j], nums[k]]：
        ///  i != j, i != k, and j != k, and nums[i] + nums[j] + nums[k] == 0.
        /// 請注意，解集合中不得包含重複的三元組。
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            int[] array1 = new int[] { -1, 0, 1, 2, -1, -4 };
            //Console.WriteLine(ThreeSum(array1));
            var res = ThreeSum(array1);

            // res 是 IList<IList<int>>
            // list 裡面還有 list 所以要跑兩次 才能輸出
            foreach (var temp2 in res)
            {
                Console.WriteLine("====================");
                foreach (var out2 in temp2)
                {
                    Console.WriteLine("輸出結果: " + out2);
                }
            }
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
        /// 
        /// 排序是為了避免枚舉重覆
        /// a <= b <= c
        /// 只需要 (a, b, c)
        /// 如果出現 (b, a, c) or (c, b, a)
        /// 就是為了避免這問題
        /// 
        /// </summary>
        /// <param name="nums"></param>
        /// <returns></returns>
        public static IList<IList<int>> ThreeSum(int[] nums)
        {
            List<IList<int>> result = new List<IList<int>>();
            List<int> temp;

            // 排序 asc; 避免枚舉重覆
            Array.Sort(nums);

            // first不會與second與third重疊; 枚舉 first
            for (int first = 0; first < nums.Length - 2; first++)
            {
                // The numbers of first, second, third should be indexes.
                if (first > 0 && nums[first] == nums[first - 1])
                {
                    // 項目7 另外判斷 first 是否已經重複，若重複則跳過此次迴圈，因為答案也會是一樣的
                    // 需要和上一次枚舉的數不同
                    continue;
                }

                // second 起始位置在first右邊
                int second = first + 1;

                // third 起始位置在 nums最後(最右邊)
                int third = nums.Length - 1;

                // 枚舉 second, 雙指針
                while (second < third)
                {
                    // 項目8 另外判斷 second 是否已經重複，若重複則 second++，並跳過此次迴圈，因為答案也會是一樣的 
                    // 如果遇到 連續相同數字 pass
                    if (nums[second] == nums[second - 1] && second > first + 1)
                    {
                        // 需要和上一次枚舉的數不同
                        second++;
                        continue;
                    }

                    int sum = nums[first] + nums[second] + nums[third];
                    if (sum == 0)
                    {
                        // == 0 為解答
                        temp = new List<int>
                        {
                            nums[first],
                            nums[second],
                            nums[third]
                        };
                        result.Add(temp);
                        // 繼續找下一組
                        second++;
                        third--;
                    }
                    else if (sum < 0)
                    {
                        // < 0 代表負數太大，需要將 second 右移 (second++)
                        // 已排序過, 右移數字越大
                        second++;
                    }
                    else
                    {
                        // > 0 代表正數太大，需要將 third 左移 (third--)
                        // 已排序過, 左移數字越小
                        third--;
                    }
                }
            }

            return result;

        }
    }
}
