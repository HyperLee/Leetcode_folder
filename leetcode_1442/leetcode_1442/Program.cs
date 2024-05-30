namespace leetcode_1442
{
    internal class Program
    {
        /// <summary>
        /// 1442. Count Triplets That Can Form Two Arrays of Equal XOR
        /// https://leetcode.com/problems/count-triplets-that-can-form-two-arrays-of-equal-xor/description/?envType=daily-question&envId=2024-05-30
        /// 1442. 形成两个异或相等数组的三元组数目
        /// https://leetcode.cn/problems/count-triplets-that-can-form-two-arrays-of-equal-xor/description/
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            int[] input = { 2, 3, 1, 6, 7 };
            Console.WriteLine(CountTriplets(input));
            Console.ReadKey();
        }


        /// <summary>
        /// ref:
        /// https://leetcode.cn/problems/count-triplets-that-can-form-two-arrays-of-equal-xor/solutions/579281/xing-cheng-liang-ge-yi-huo-xiang-deng-sh-jud0/
        /// https://leetcode.cn/problems/count-triplets-that-can-form-two-arrays-of-equal-xor/solutions/782679/gong-shui-san-xie-xiang-jie-shi-yong-qia-7gzm/
        /// https://leetcode.cn/problems/count-triplets-that-can-form-two-arrays-of-equal-xor/solutions/1636078/by-stormsunshine-vo87/
        /// 
        /// 根据按位异或运算的性质，a = b 等价于 a ⊕ b = 0。  << 需要注意是關鍵
        /// 從範圍[i, k]範圍內去找出 j 數值
        /// 極為題目所求的 (i, j, k) 資料, i < j <= k
        /// i: 開頭
        /// k: 結尾
        /// j: 從 i + 1 ~ k 內去找
        /// 共 k - i 種取法
        /// 
        /// </summary>
        /// <param name="arr"></param>
        /// <returns></returns>
        public static int CountTriplets(int[] arr)
        {
            int count = 0;
            int n = arr.Length;

            // [i, k]範圍內元素要等於 0, i < j <= k
            for(int i = 0; i < n; i++)
            {
                for(int k = i + 1; k < n; k++)
                {
                    int xor = 0;

                    // 因已確認範圍[i, k]內, j就要取 i + 1 ~ k內範圍
                    // 共 k - i 種取法
                    for(int j = i; j <= k; j++)
                    {
                        xor ^= arr[j];
                    }

                    // 根据按位异或运算的性质，a = b 等价于 a ⊕ b = 0。
                    if (xor == 0)
                    {
                        count += k - i;
                    }
                }
            }

            return count;

        }
    }
}
