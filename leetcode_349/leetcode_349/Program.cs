namespace leetcode_349
{
    internal class Program
    {
        /// <summary>
        /// 349. Intersection of Two Arrays
        /// https://leetcode.com/problems/intersection-of-two-arrays/description/?envType=daily-question&envId=2024-03-10
        /// 349. 两个数组的交集
        /// https://leetcode.cn/problems/intersection-of-two-arrays/description/
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            int[] input1 = { 1, 2, 2, 1 };
            int[] input2 = { 2, 2 };

            //Console.WriteLine(Intersection(input1, input2));
            var res = Intersection(input1, input2);
            foreach (int i in res) 
            {
                Console.WriteLine(i + ", ");
            }
            Console.ReadKey();
        }



        /// <summary>
        /// 先使用dic儲存 nums1裡面有的資料
        /// 再去 nums2 比對
        /// 發現有相同 就 放到 hash裡面
        /// 且因為是hash所以不會重覆資料出現
        /// </summary>
        /// <param name="nums1"></param>
        /// <param name="nums2"></param>
        /// <returns></returns>
        public static int[] Intersection(int[] nums1, int[] nums2)
        {
            Dictionary<int, int> dic = new Dictionary<int, int>();
            HashSet<int> visited = new HashSet<int>();

            foreach (int x in nums1) 
            {
                if(!dic.ContainsKey(x))
                {
                    dic.Add(x, 1);
                }
            }

            foreach (int x in nums2)
            {
                if(dic.ContainsKey(x))
                {
                    visited.Add(x);
                }
            }

            return visited.ToArray();
        }
    }
}
