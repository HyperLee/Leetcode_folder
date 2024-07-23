namespace leetcode_1636
{
    internal class Program
    {
        /// <summary>
        /// 1636. Sort Array by Increasing Frequency
        /// https://leetcode.com/problems/sort-array-by-increasing-frequency/description/?envType=daily-question&envId=2024-07-23
        /// 1636. 按照频率将数组升序排序
        /// https://leetcode.cn/problems/sort-array-by-increasing-frequency/description/
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            int[] input = { 1, 5, 0, 5 };
            
            var res = FrequencySort(input);

            Console.WriteLine("ans: ");
            foreach (int i in res)
            {
                Console.Write(i + ", ");
            }

            Console.ReadKey();
        }


        /// <summary>
        /// 要注意: 
        /// If multiple values have the same frequency, sort them in decreasing order.
        /// 如果有多个值的频率相同，请你按照数值本身将它们 降序 排序。
        /// 
        /// 輸入的陣列是遞增
        /// 輸出頻率也是遞增
        /// 但是相同頻率, 數字是遞減
        /// 
        /// https://leetcode.cn/problems/sort-array-by-increasing-frequency/solutions/1831531/an-zhao-pin-lu-jiang-shu-zu-sheng-xu-pai-z2db/
        /// https://leetcode.cn/problems/sort-array-by-increasing-frequency/solutions/1833402/by-ac_oier-c3xc/
        /// https://leetcode.cn/problems/sort-array-by-increasing-frequency/solutions/1522014/by-stormsunshine-stv8/
        /// 
        /// 本題重點是最後的排序
        /// 相同頻率, 數字大小要遞減才是難點
        /// 
        /// 1.頻率不同, 依據"頻率"遞增排序
        /// 2.頻率相同, 依據"數字"遞減排序
        /// 
        /// lisr sort 排序ref:
        /// https://dotblogs.com.tw/shanna/2019/09/09/213800
        /// https://www.cnblogs.com/tomin/archive/2011/09/20/2182483.html
        /// https://www.hicsharp.com/a/7620ddb5eb644e448b06e0b8bbb97f41
        /// https://hackmd.io/@jiesen/r1awIjwlF
        /// </summary>
        /// <param name="nums"></param>
        /// <returns></returns>
        public static int[] FrequencySort(int[] nums)
        {
            // key: array input,  value: frequency
            // 統計nums頻率
            Dictionary<int, int> dic = new Dictionary<int, int>();
            // nums依順序加入list
            List<int> list = new List<int>();
            
            // 兩個同時執行, 就不用分開寫兩個foreach
            foreach (int num in nums)
            {
                if(!dic.ContainsKey(num))
                {
                    // 不存在就新增頻率
                    dic.Add(num, 1);
                }
                else
                {
                    // 存在就增加頻率次數
                    dic[num]++;
                }

                list.Add(num);
            }

            // *依照題目要求來排序
            // cnt1,2 是頻率
            // a, b   是比較數字
            list.Sort((a, b) => {
                int cnt1 = dic[a], cnt2 = dic[b];
                //return cnt1 != cnt2 ? cnt1 - cnt2 : b - a;
                if (cnt1 != cnt2)
                {
                    // 頻率; 遞增
                    return cnt1.CompareTo(cnt2);
                }
                else
                {
                    // 數字; 遞減

                    // a在前面, 開頭要取負號
                    // return -a.CompareTo(b);

                    // b在前面, 這樣就不用取負號
                    return b.CompareTo(a);
                }
            });

            return list.ToArray();
        }
    }
}
