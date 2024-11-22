namespace leetcode_1072
{
    internal class Program
    {
        /// <summary>
        /// 1072. Flip Columns For Maximum Number of Equal Rows
        /// https://leetcode.com/problems/flip-columns-for-maximum-number-of-equal-rows/description/?envType=daily-question&envId=2024-11-22
        /// 
        /// 1072. 按列翻转得到最大值等行数
        /// https://leetcode.cn/problems/flip-columns-for-maximum-number-of-equal-rows/description/
        /// 
        /// 你得到了一個 m×n 的二元矩陣 matrix。
        /// 你可以選擇矩陣中的任意列，並翻轉該列中的每個單元格（即，將單元格的值從 0 改為 1，或從 1 改為 0）。
        /// 返回在經過若干次翻轉後，所有數值均相等的行的最大數量。
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            int[][] input = new int[][]
            {
                 new int[]{ 0, 1},
                 new int[]{ 1, 0}
            };

            Console.WriteLine("res: " + MaxEqualRowsAfterFlips(input));
            Console.ReadKey();
        }


        /// <summary>
        /// ref:
        /// https://leetcode.cn/problems/flip-columns-for-maximum-number-of-equal-rows/solutions/2267115/an-lie-fan-zhuan-de-dao-zui-da-zhi-deng-teeig/
        /// https://leetcode.cn/problems/flip-columns-for-maximum-number-of-equal-rows/solutions/2270101/ni-xiang-si-wei-pythonjavacgo-by-endless-915k/
        /// https://leetcode.cn/problems/flip-columns-for-maximum-number-of-equal-rows/solutions/2636264/1072-an-lie-fan-zhuan-de-dao-zui-da-zhi-mnhul/
        /// 
        /// 如果翻转固定的某些列，可以使得两个不同的行都变成由相同元素组成的行，那么我们称这两行为本质相同的行。
        /// 例如 001 和 110 就是本质相同的行。
        /// 
        /// 本质相同的行有什么特点呢？可以发现，本质相同的行只存在两种情况，一种是由 0 开头的行，另一种是由 1 开头的行。
        /// 在开头的元素确定以后，由于翻转的列已经固定，所以可以推断出后续所有元素是 0 还是 1。
        /// 
        /// 为了方便统计本质相同的行的数量，我们让由 1 开头的行全部翻转，翻转后行内元素相同的行即为本质相同的行。
        /// 之后我们将每一行转成字符串形式存储到哈希表中，遍历哈希表得到最多的本质相同的行的数量即为答案。
        /// 
        ///  异或(XOR) ^ 的性质：相同为 0，相异为 1
        ///  如果 matrix[i][0] 为 0：matrix[i][j] ^ 0 异或的结果还是 matrix[i][j]；
        ///  如果 matrix[i][0] 为 1：matrix[i][j] ^ 1 异或的结果还是 取反，即翻转；+'0' 转成字符 
        /// </summary>
        /// <param name="matrix"></param>
        /// <returns></returns>
        public static int MaxEqualRowsAfterFlips(int[][] matrix)
        {
            // 行 (左右)
            int m = matrix.Length;
            // 列 (上下)
            int n = matrix[0].Length;
            // key: 每一行(字串樣式) value: 出現次數
            // 將每一行轉成 string 形式儲存到 hash table
            IDictionary<string, int> dic = new Dictionary<string, int>();
            // 遍歷行
            for(int i = 0; i < m; i++)
            {
                // 暫存的 char, 要轉成 string 用
                char[] arr = new char[n];
                // 默認都是 0
                Array.Fill(arr, '0');
                // 遍歷列
                for(int j = 0; j < n; j++)
                {
                    // 如果 matrix[i][0] 為 1, 則對該行元素進行翻轉
                    // 不是很懂 '0' + 的用意, 不加上 leetcode 也可以編譯過
                    arr[j] = (char)('0' + (matrix[i][j] ^ matrix[i][0]));

                    // 對比用
                    var tempshow = (char)((matrix[i][j] ^ matrix[i][0]));
                }
                // 轉成 string
                string s = new string(arr);
                // 將每一行轉成 string 形式儲存至 hash table
                if(dic.ContainsKey(s))
                {
                    dic[s]++;
                }
                else
                {
                    
                    dic.Add(s, 1);
                }
            }

            int res = 0;
            // 遍歷 hash table
            foreach(KeyValuePair<string, int> kvp in dic)
            {
                // 取出最多本質相同行的數量
                res = Math.Max(res, kvp.Value);
            }

            return res;
        }
    }
}
