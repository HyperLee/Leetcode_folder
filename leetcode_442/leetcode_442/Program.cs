namespace leetcode_442
{
    internal class Program
    {
        /// <summary>
        /// 442. Find All Duplicates in an Array
        /// https://leetcode.com/problems/find-all-duplicates-in-an-array/description/?envType=daily-question&envId=2024-03-25
        /// 442. 数组中重复的数据
        /// https://leetcode.cn/problems/find-all-duplicates-in-an-array/description/
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            int[] input = { 4, 3, 2, 7, 8, 2, 3, 1 };
            var output = FindDuplicates(input);
            foreach (int i in output) 
            {
                Console.WriteLine(i + ", ");
            }

            Console.ReadKey();

        }


        /// <summary>
        ///  range [1, n]
        ///  把array排序之後, 一個一個比對即可
        ///  
        /// </summary>
        /// <param name="nums"></param>
        /// <returns></returns>
        public static IList<int> FindDuplicates(int[] nums)
        {
            IList<int> list = new List<int>();
            Array.Sort(nums);

            for(int i = 1; i < nums.Length; i++)
            {
                // 連續相同, 就塞入 list
                if (nums[i - 1] == nums[i])
                {
                    list.Add(nums[i]);
                }

            }

            return list;
        }
    }
}
