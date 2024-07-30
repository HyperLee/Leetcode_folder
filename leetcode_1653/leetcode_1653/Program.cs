namespace leetcode_1653
{
    internal class Program
    {
        /// <summary>
        /// 1653. Minimum Deletions to Make String Balanced
        /// https://leetcode.com/problems/minimum-deletions-to-make-string-balanced/description/?envType=daily-question&envId=2024-07-30
        /// 
        /// 1653. 使字符串平衡的最少删除次数
        /// https://leetcode.cn/problems/minimum-deletions-to-make-string-balanced/description/
        /// 
        /// 所謂平衡就是 不能出現 索引 index (i, j)
        /// s[i] = 'b'
        /// s[j] = 'a'
        ///  => b 在 a 前面, 這就是不平衡
        /// 
        /// 簡單說下列三種方式 就是平衡
        /// 1. 全a 
        /// => 把 b 全部刪除
        /// 2. 全b 
        /// => 把 a 全部刪除
        /// 3. a, b 混合,但是 a 在前 b 在後( a 在 b 左邊)
        /// => 依據 case 刪除相鄰的 b or a 都可以達成
        /// 
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            string s = "aabb";
            Console.WriteLine("方法1: " + MinimumDeletions(s));
            Console.WriteLine("方法2: " + MinimumDeletions2(s));
            Console.ReadKey();

        }


        /// <summary>
        /// ref:
        /// https://leetcode.cn/problems/minimum-deletions-to-make-string-balanced/solutions/2147032/shi-zi-fu-chuan-ping-heng-de-zui-shao-sh-l5lk/
        /// https://leetcode.cn/problems/minimum-deletions-to-make-string-balanced/solutions/2149746/qian-hou-zhui-fen-jie-yi-zhang-tu-miao-d-dor2/
        /// https://leetcode.cn/problems/minimum-deletions-to-make-string-balanced/solutions/2150153/python3javacgo-yi-ti-shuang-jie-dong-tai-5ej8/
        /// https://leetcode.cn/problems/minimum-deletions-to-make-string-balanced/solutions/2638921/1653-shi-zi-fu-chuan-ping-heng-de-zui-sh-8giz/
        /// 
        /// 想像連續兩個char中間劃出一條分隔線
        /// ba => b | a
        /// 分隔線左邊的 b 視為 leftb
        /// 分隔線右邊的 a 視為 righta
        /// 將 leftb + righta = 刪除次數(至少需要這麼多次)
        /// 根據上述推理
        /// 再去遍歷找出能不能達到更少次數的刪除方法
        /// 
        /// a/b 只需要刪除其中一個, 即可達到平衡
        /// 不需要兩個同時都刪除
        /// 
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static int MinimumDeletions(string s)
        {
            int leftb = 0, righta = 0;

            foreach (char c in s)
            {
                if (c == 'a')
                {
                    // 總共幾個a可以刪除
                    righta++;
                }
            }

            int res = righta;
            foreach(char c in s)
            {
                // a/b 只需要刪除其中一個,即可達到平衡
                if(c == 'a')
                {
                    righta--;
                }
                else
                {
                    leftb++;
                }

                res = Math.Min(res, leftb + righta);
            }

            return res;
        }


        /// <summary>
        /// 這個方法比較好理解,單純一點
        /// 
        /// 1. res: 需要刪除次數
        /// 2. leftb: 分隔線左邊的 b 視為 leftb
        /// 3. b 右邊發現有 a, 就累計刪除次數.
        ///    同時將 b 累計次數刪除. 
        ///    因已增加刪除次數, 所以原先的 b 就可以保留不需重覆刪除
        /// 
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>

        public static int MinimumDeletions2(string s)
        {
            int leftb = 0, res = 0;

            foreach (char c in s) 
            {
                if (c == 'a')
                {
                    // 只要發現 b 右邊有 a 就累計刪除次數
                    // 同時扣除 leftb (因為已經刪除 a , 不平衡已變成平衡)
                    if (leftb > 0)
                    {
                        res++;
                        leftb--;
                    }
                }
                else
                {
                    // 累計幾個 b
                    leftb++;
                }
            }

            return res;
        }
    }
}
