namespace leetcode_2540
{
    internal class Program
    {
        /// <summary>
        /// 2540. Minimum Common Value
        /// https://leetcode.com/problems/minimum-common-value/description/?envType=daily-question&envId=2024-03-09
        /// 2540. 最小公共值
        /// https://leetcode.cn/problems/minimum-common-value/
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            int[] num1 = { 1, 2, 3, 4 };
            int[] num2 = { 2, 4 };

            Console.WriteLine("GetCommon : " + GetCommon(num1, num2));
            Console.WriteLine("GetCommon2: " + GetCommon2(num1, num2));
            Console.ReadKey();
        }


        /// <summary>
        /// 使用Dictionary去紀錄nums1裡面的element
        /// 再去nums2裡面 比對是否存在
        /// 
        /// 不過此方法 應該可以改成 Hash即可
        /// 畢竟使用 Dictionary 我只會使用到Key而已
        /// 沒有用到value
        /// 再來因為題目給的都已經排序好了(遞增排序)
        /// 出現同樣重複 只取最前面的
        /// 所以 Dictionary的value用不上
        /// 乾脆省掉
        /// </summary>
        /// <param name="nums1"></param>
        /// <param name="nums2"></param>
        /// <returns></returns>
        public static int GetCommon(int[] nums1, int[] nums2)
        {
            Dictionary<int, int> dic = new Dictionary<int, int>();
            foreach (int num in nums1) 
            {
                if(!dic.ContainsKey(num))
                {
                    dic.Add(num, 1);
                }
            }

            foreach (int x in nums2) 
            {
                if(dic.ContainsKey(x))
                {
                    return x;
                }
            }

            return -1;
        }


        /// <summary>
        /// 方法2
        /// HashSet
        /// </summary>
        /// <param name="nums1"></param>
        /// <param name="nums2"></param>
        /// <returns></returns>
        public static int GetCommon2(int[] nums1, int[] nums2)
        {
            HashSet<int> ints = new HashSet<int>();
            foreach (int num in nums1) 
            {
                ints.Add(num);
            }

            foreach(int x in nums2)
            {
                if(ints.Contains(x))
                {
                    return x;
                }
            }

            return -1;
        }
    }
}
