using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace leetcode_380
{
    internal class Program
    {
        /// <summary>
        /// 380. Insert Delete GetRandom O(1)
        /// https://leetcode.com/problems/insert-delete-getrandom-o1/description/?envType=daily-question&envId=2024-01-16
        /// 380. O(1) 时间插入、删除和获取随机元素
        /// https://leetcode.cn/problems/insert-delete-getrandom-o1/description/
        /// 
        /// 實作 Hash + 變長數組
        /// 解法說明步驟, 需要看官方說明
        /// 
        /// 可觀摩,但是難度有點高
        /// 
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {

        }

        // 資料列
        public IList<int> nums;
        // 紀錄資料列中, 每一筆 element 與 儲存位置
        public Dictionary<int, int> indices;
        public Random random;

        public RandomizedSet()
        {
            nums = new List<int>();
            indices = new Dictionary<int, int>();
            random = new Random();
        }


        /// <summary>
        /// 新增
        /// 
        /// 1. 在变长数组的末尾添加 val；
        /// 2. 在添加 val 之前的变长数组长度为 val 所在下标 index，将 val 和下标 index 存入哈希表；
        /// 3. 返回 true。
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        public bool Insert(int val)
        {
            // 已存在就不能新增, 回傳false
            if(indices.ContainsKey(val))
            {
                return false;
            }

            int index = nums.Count();
            // 新的 element 加入尾端
            nums.Add(val);
            // 紀錄 該 element的 位置
            indices.Add(val, index);

            return true;
        }


        /// <summary>
        /// 刪除
        /// 
        /// 1. 从哈希表中获得 val 的下标 index；
        /// 2. 将变长数组的最后一个元素 last 移动到下标 index 处，在哈希表中将 last 的下 标更新为 index；
        /// 3. 在变长数组中删除最后一个元素，在哈希表中删除 val；
        /// 4. 返回 true。
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        public bool Remove(int val)
        {
            // 不存在不能刪除, 回傳 false
            if(!indices.ContainsKey(val))
            {
                return false;
            }

            int index = indices[val];
            int last = nums[nums.Count - 1];
            nums[index] = last;
            indices[last] = index;
            nums.RemoveAt(nums.Count - 1);
            indices.Remove(val);

            return true;
        }


        /// <summary>
        /// 隨機 回傳 資料列中 其中一筆資料
        /// </summary>
        /// <returns></returns>
        public int GetRandom()
        {
            int randomindex = random.Next(nums.Count);

            return nums[randomindex];
        }

    }
}
