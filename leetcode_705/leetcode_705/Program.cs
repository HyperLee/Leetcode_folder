using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace leetcode_705
{
    internal class Program
    {
        /// <summary>
        /// 705. Design HashSet
        /// https://leetcode.com/problems/design-hashset/
        /// 705. 设计哈希集合
        /// https://leetcode.cn/problems/design-hashset/
        /// 
        /// 本題目 不是很懂
        /// 就大概觀摩別人一下
        /// 
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {

        }


        // BASE, 需為質數; 依據參考資料來看,可自行設定
        // 此次設計為127,   hash大小範圍 為 [0,BASE − 1]
        private const int BASE = 127;
        private LinkedList<int>[] set;

        /// <summary>
        /// 本題比較複雜
        /// 由於不是很懂原理
        /// 故只能多參考看看
        /// 
        /// 參考網址:
        /// https://leetcode.cn/problems/design-hashset/solution/by-stormsunshine-jzjo/
        /// https://leetcode.cn/problems/design-hashset/solution/she-ji-ha-xi-ji-he-by-leetcode-solution-xp4t/
        /// 
        /// 
        /// https://zh.wikipedia.org/wiki/%E8%B4%A8%E6%95%B0
        /// https://zh.wikipedia.org/zh-tw/%E6%95%A3%E5%88%97%E5%87%BD%E6%95%B8
        /// https://learn.microsoft.com/zh-tw/dotnet/api/system.security.cryptography.keyedhashalgorithm?view=net-7.0
        /// https://learn.microsoft.com/zh-tw/troubleshoot/developer/visualstudio/csharp/language-compilers/compute-hash-values
        /// 
        /// 设哈希表的大小为 base，则可以设计一个简单的哈希函数：hash(x)=x  mod  base。
        /// 老實說不是很懂這句話意思
        /// 
        /// </summary>
        public MyHashSet()
        {
            set = new LinkedList<int>[BASE];
            for (int i = 0; i < BASE; i++)
            {
                set[i] = new LinkedList<int>();
            }

        }


        /// <summary>
        /// hash 相同key只會存依次
        /// 不會有重複的key 值
        /// </summary>
        /// <param name="key"></param>
        public void Add(int key)
        {
            int index = key % BASE;
            LinkedList<int> list = set[index];
            foreach (int element in list)
            {
                if (element == key)
                {
                    return;
                }
            }
            list.AddLast(key);

        }

        public void Remove(int key)
        {
            int index = key % BASE;
            LinkedList<int> list = set[index];
            foreach (int element in list)
            {
                if (element == key)
                {
                    list.Remove(element);
                    break;
                }
            }

        }

        public bool Contains(int key)
        {
            int index = key % BASE;
            LinkedList<int> list = set[index];
            foreach (int element in list)
            {
                if (element == key)
                {
                    return true;
                }
            }
            return false;

        }

    }
}
