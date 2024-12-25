namespace leetcode_001
{
    internal class Program
    {
        /// <summary>
        ///  LeetCode 1. Two Sum
        ///  https://leetcode.com/problems/two-sum/
        ///  1. 两数之和
        /// https://leetcode.cn/problems/two-sum/description/
        /// 
        ///  Given nums = [2, 7, 11, 15], target = 9,
        ///  Because nums[0] + nums[1] = 2 + 7 = 9,
        ///  return [0, 1].
        ///  回傳 index 位置
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            int[] nums = { 2, 7, 11, 15 };
            int target = 9;
            var res = TwoSum(nums, target);
            Console.WriteLine($"ref: [{res[0]},{res[1]}]");
        }


        /// <summary>
        /// https://www.itread01.com/content/1543410439.html
        /// https://ithelp.ithome.com.tw/articles/10217042
        /// ContainsKey
        /// https://vimsky.com/zh-tw/examples/usage/c-sharp-dictionary-containskey-method.html
        /// 
        /// 使用哈希表，可以将寻找 target - x 的时间复杂度降低到从 O(N)降低到 O(1)。
        /// 这样我们创建一个哈希表，对于每一个 x，我们首先查询哈希表中是否存在 target - x，然后将 x 插
        /// 入到哈希表中，即可保证不会让 x 和自己匹配。
        /// 
        /// int[] nums 相同 element 只能使用一次
        /// 透過 Dictionary 去紀錄 元素
        /// 也可以使用 Hashset 取代
        /// </summary>
        /// <param name="nums"></param>
        /// <param name="target"></param>
        /// <returns></returns>
        public static int[] TwoSum(int[] nums, int target)
        {
            // 當作 hash table 使用, key: nums.Value;  value: nums.index
            Dictionary<int, int> dic = new Dictionary<int, int>();
            for(int i = 0; i < nums.Length; i++)
            {
                // 相減之後餘數.
                int left = target - nums[i];
                
                // left 存在就直接呼叫出來取出該 key 的 value
                if(dic.ContainsKey(left))
                {
                    // dic[left] : dic.Value
                    // string aa = dic[left].ToString();
                    return new int[] { dic[left], i };
                }

                // 該 nums[i] 不存在就加入 dic.
                if (!dic.ContainsKey(nums[i]))
                {
                    dic.Add(nums[i], i);
                }
            }

            return null;
        }
    }
}
