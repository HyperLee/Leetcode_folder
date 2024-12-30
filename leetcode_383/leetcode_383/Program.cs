namespace leetcode_383
{
    internal class Program
    {
        /// <summary>
        /// 383. Ransom Note
        /// https://leetcode.com/problems/ransom-note/
        /// 
        /// 383. 赎金信
        /// https://leetcode.cn/problems/ransom-note/description/
        /// 
        /// 给你两个字符串：ransomNote 和 magazine ，判断 ransomNote 能不能由 magazine 里面的字符构成。
        /// 如果可以，返回 true ；否则返回 false 。
        /// magazine 中的每个字符只能在 ransomNote 中使用一次。
        /// 
        /// ransomNote 能不能組合 建立出 magazine
        /// 題目主要是說，有個犯人想要從雜誌 (magazine) 上剪貼他想要的字到勒索信 
        /// (ransomNote) 上
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            string ransomNote = "ab", magazine = "adcb";
            Console.WriteLine("res: " + CanConstruct(ransomNote, magazine));
        }


        /// <summary>
        /// https://www.delftstack.com/zh-tw/howto/csharp/how-to-remove-item-from-list-in-csharp/
        /// https://www.796t.com/content/1550172424.html
        /// C# 使用 RemoveAt() 方法從 List 中刪除元素
        /// RemoveAt() 方法根據該元素的索引號從 List 中刪除該元素。我們已經知道 C# 中的索引以 0 開頭。
        /// 因此，選擇索引號時要小心。此方法的正確語法如下：
        /// 
        /// indexof() ：在字串中從前向後定位字元和字串；所有的返回值都是指在字串的絕對位置，如為空則為- 1
        /// 
        /// https://ithelp.ithome.com.tw/articles/10221926
        /// 
        /// 劫匪信小 雜誌 大
        /// 小字串要存在於大字串裡面
        /// 大字串可以有多餘的單字
        /// 
        /// 每一個英文字母只能用一次 比對到 就去除
        /// 只要劫匪信能比對出來存在於 雜誌中 即可
        /// 使用contain會出錯, 條件有說 magazine 中的每个字符只能在 ransomNote 中使用一次。
        /// 
        /// 解法
        /// 1. 若 magazine 上的字元不夠在 ransomNote 上使用，return false
        /// 2. 將 magazine 及 ransomNote 轉換成 List<char>，這樣就可以使用 IndexOf(s) 及 
        ///    RemoveAt(index)
        /// 3. 判斷 magazine 裡有沒有 ransomNote 要的字元 
        ///    若有的話，就剪貼上去 (magazines.RemoveAt(index))
        ///    若 沒有 的話就 return false，因為不夠用啦～
        /// </summary>
        /// <param name="ransomNote"></param>
        /// <param name="magazine"></param>
        /// <returns></returns>
        public static bool CanConstruct(string ransomNote, string magazine)
        {
            // 雜誌文字比劫匪信還少, 直接回傳錯誤
            if (magazine.Length < ransomNote.Length)
            {
                return false;
            }

            // 轉成 char list 比對用
            var ransomNotes = ransomNote.ToCharArray().ToList();
            var magazines = magazine.ToCharArray().ToList();

            // 判斷 劫匪信件 每個文字
            foreach (var s in ransomNotes)
            {
                // 能不能從 雜誌中 找出相對應
                int index = magazines.IndexOf(s);

                if (index >= 0)
                {
                    // 有找到就移除
                    magazines.RemoveAt(index);
                }
                else
                {
                    // 找不到就錯誤
                    return false;
                }
            }
            return true;
        }
    }
}
