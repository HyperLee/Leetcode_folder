using System.Text;

namespace leetcode_049;

class Program
{
    /// <summary>
    /// 49. Group Anagrams
    /// https://leetcode.com/problems/group-anagrams/description/
    ///
    /// English:
    /// Given an array of strings strs, group the anagrams together. You can return the answer in any order.
    ///
    /// 繁體中文:
    /// 給定一個字串陣列 strs，請將所有字母異位詞分組。你可以用任意順序回傳答案。
    ///
    /// 49. 字母異位詞分組
    /// https://leetcode.cn/problems/group-anagrams/description/?envType=study-plan-v2&envId=top-interview-150
    /// </summary>
    /// <param name="args">Command-line arguments.</param>
    static void Main(string[] args)
    {
        Console.WriteLine("Hello, World!");
    }

    /// <summary>
    /// 思路就是弄个字典，把每个字符串排序比较，排序的 string 作为 key
    /// ,值为 strs[i]，遍历完 strs ,在从 dic 取值
    /// 
    /// Key:   將輸入的 strs 經過排序過後的 str
    /// value: 排序前的原始輸入字串 strs[i]
    /// 
    ///  字母异位词:同樣 char, 不同排序組合而成的一個單字或是片段
    ///  題目要求很簡單, 將同樣的 字母异位词 進行排列
    ///  相同的放在一起即可
    ///  
    ///  所以做法就是
    ///  1.遍歷每個輸入的單字, 將單字從新排列 ( 字母异位词 具有相同的 char )
    ///  2.判斷每個輸入的單字是不是相同的排列, 相同就加入, 不同就新增
    ///  3.輸出資料, 這裡要注意. 輸出資料是輸出原先輸入的單字,將相同的字母异位词放在一起輸出
    ///  
    /// 宣告部分需要注意, 是 IList<IList<string>> 輸入, 輸出
    /// 
    /// dic.value 寫入 res 裡面, 方法 1 與 2 
    /// 其實差不多意思, 做法不同而已
    /// 方法一類似陣列取 value 而已
    /// 方法二就正規寫法
    /// 
    /// res Console 輸出要用兩層
    /// 因為是陣列裡面還有很多筆資料
    /// 第一層是 Group 大區分
    /// 第二層才是 Group 內詳細資料
    /// </summary>
    /// <param name="strs"></param>
    /// <returns></returns>
    public IList<IList<string>> GroupAnagrams(string[] strs)
    {
        Dictionary<string, IList<string>> dic = new Dictionary<string, IList<string>>();
        IList<IList<string>> res = new List<IList<string>>();

        for(int i = 0; i < strs.Length; i++)
        {
            // 遍歷每個輸入的單字, 形態轉為 char
            char[] arr = strs[i].ToArray();
            // 排序
            Array.Sort(arr);
            // 形態轉回字串
            string str = new string(arr);

            if(dic.ContainsKey(str))
            {
                // 已經存在就加入
                // str 這個 key, 加入 strs[i] 這個 value
                dic[str].Add(strs[i]);
            }
            else
            {
                // 不存在就新增
                // str 這個 key, 加入 strs[i] 這個 value
                dic[str] = new List<string>{strs[i]};
            }
        }

        // 依序將 dic.Keys 裡面的 value 取出來, 放到 res 輸出
        // method 1
        // foreach(var item in dic.Keys)
        // {
        //     res.Add(dic[item]);
        // }

        // 依序將 dic.Keys 裡面的 value 取出來, 放到 res 輸出
        // method2
        foreach(KeyValuePair<string, IList<string>> kvp in dic)
        {
            res.Add(kvp.Value);
        }

        return res;
    }

    /// <summary>
    /// 方法二：計數
    /// 由于互为字母异位词的两个字符串包含的字母相同，因此两个字符串中的相同字母出现的次数一定是相同的，故可以将每个字母出现
    /// 的次数使用字符串表示，作为哈希表的键。
    /// 
    /// 由于字符串只包含小写字母，因此对于每个字符串，可以使用长度为 26 的数组记录每个字母出现的次数。需要注意的是，在使用数
    /// 组作为哈希表的键时，不同语言的支持程度不同，因此不同语言的实现方式也不同。
    /// </summary>
    /// <param name="strs"></param>
    /// <returns></returns>
    public IList<IList<string>> GroupAnagrams2(string[] strs)
    {
        Dictionary<string, IList<string>> dic = new Dictionary<string, IList<string>>();
        foreach(string str in strs)
        {
            int[] counts = new int[26];
            // 統計每個字母出現的次數
            foreach(char c in str)
            {
                counts[c - 'a']++;
            }

            // 組合成為一 key
            // 例如 "a1e1t1"
            StringBuilder sb = new StringBuilder();
            for(int i = 0; i < 26; i++)
            {
                if(counts[i] != 0)
                {
                    sb.Append((char)('a' + i));
                    sb.Append(counts[i]);
                }
            }

            string key = sb.ToString();

            // 如果 key 不存在, 先建立新的 List
            if(!dic.ContainsKey(key))
            {
                dic[key] = new List<string>();
            }

            dic[key].Add(str);
        }

        return dic.Values.Select(list => (IList<string>)list).ToList();
    }
}
