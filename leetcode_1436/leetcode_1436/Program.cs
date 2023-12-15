using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace leetcode_1436
{
    internal class Program
    {
        /// <summary>
        /// 1436. Destination City
        /// https://leetcode.com/problems/destination-city/?envType=daily-question&envId=2023-12-15
        /// 1436. 旅行终点站
        /// https://leetcode.cn/problems/destination-city/
        /// 
        /// IList<IList<string>> 宣告與初始化方法
        /// https://stackoverflow.com/questions/9158483/how-can-i-initialize-a-ilistiliststring
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            IList<IList<string>> matrix = new List<IList<string>>();
            matrix.Add(new List<string> { "London", "New York" });
            matrix.Add(new List<string> { "New York", "Lima" });
            matrix.Add(new List<string> { "Lima", "Sao Paulo" });

            Console.WriteLine(DestCity(matrix));
            Console.ReadKey();
        }



        /// <summary>
        /// 官方解法:
        /// 我们可以遍历 cityBi，返回不在 cityAi 中的城市，即为答案。
        /// https://leetcode.cn/problems/destination-city/solutions/1026156/lu-xing-zhong-dian-zhan-by-leetcode-solu-pscd/
        /// 
        /// 
        /// 
        /// 代码实现时，可以先将所有 cityAi​ 存于一哈希表中
        /// ，然后遍历 cityBi​ 并查询 cityBi 是否在哈希表中。
        /// </summary>
        /// <param name="paths"></param>
        /// <returns></returns>
        public static string DestCity(IList<IList<string>> paths)
        {
            ISet<string> citiesA = new HashSet<string>();
            // 將全部paths中的 cityAi​ 存入 hash表中
            foreach (IList<string> path in paths) 
            {
                citiesA.Add(path[0]);
            }

            // 找出 cityBi 不存在於 cityAi 中的那個
            foreach (IList<string> path in paths) 
            {
                if (!citiesA.Contains(path[1]))
                {
                    return path[1];
                }
            }

            return "";

        }

    }
}
