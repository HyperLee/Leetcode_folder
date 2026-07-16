namespace leetcode_1160;

class Program
{
    /// <summary>
    /// 1160. Find Words That Can Be Formed by Characters
    /// https://leetcode.com/problems/find-words-that-can-be-formed-by-characters/description/
    /// 1160. 拼写单词
    /// https://leetcode.cn/problems/find-words-that-can-be-formed-by-characters/description/
    /// 
    /// 
    /// </summary>
    /// <param name="args"></param>
    static void Main(string[] args)
    {
        Console.WriteLine("Hello, World!");
    }

    /// <summary>
    /// https://leetcode.cn/problems/find-words-that-can-be-formed-by-characters/solutions/2350010/zui-zhi-jie-de-si-lu-ba-zi-dian-li-de-me-pthh/
    /// 把字典里的每个字母（包括重复的），拿出来和单词进行一一比对（不比对顺序）
    /// ，有一个匹配不上就说明不能字典里的字母不能组成该单词
    /// 
    /// words裡面的字詞 由chars裡面的char 組合而成
    /// 且chars裡面的每個char只能使用一次.
    /// 只要能組合成功就是 true
    /// 否則就是false
    /// 
    /// 最後 把為true的每個word長度最累加
    /// 就是題目要求的
    /// </summary>
    /// <param name="words"></param>
    /// <param name="chars"></param>
    /// <returns></returns>
    public int CountCharacters(string[] words, string chars)
    {
        int count = 0;
        foreach(string word in words)
        {
            bool flag = false;
            string newchars = chars.ToString().Trim();

            // 每個word裡面的單字(char)取出來chars裡面的char比對是否存在
            foreach(var item in word)
            {
                if(newchars.Contains(item))
                {
                    flag = true;
                    // 移除已找到的那個char, 因不能使用第二次
                    newchars = newchars.Remove(newchars.IndexOf(item), 1);
                    newchars.Trim();
                }
                else
                {
                    flag = false;
                    break;
                }
            }

            // 每個word比對結束,要把chars還原.因為之前有移除
            newchars = chars;
            // 比對成功,就累加成功的word長度
            if(flag == true)
            {
                count += word.Length;
            }
        }
        return count;
    }

    /// <summary>
    /// 解法二:哈希表记数
    /// 显然，对于一个单词 word，只要其中的每个字母的数量都不大于 chars 中对应的字母的数量，那么就可以用 chars 中的字母
    /// 拼写出 word。所以我们只需要用一个哈希表存储 chars 中每个字母的数量，再用一个哈希表存储 word 中每个字母的数量
    /// 最后将这两个哈希表的键值对逐一进行比较即可。
    /// </summary>
    /// <param name="words"></param>
    /// <param name="chars"></param>
    /// <returns></returns>
    public int CountCharacters2(string[] words, string chars)
    {
        Dictionary<char, int> charsCnt = new Dictionary<char, int>();

        foreach (char c in chars)
        {
            if (charsCnt.ContainsKey(c))
            {
                charsCnt[c]++;
            }
            else
            {
                charsCnt[c] = 1;
            }
        }

        int ans = 0;

        foreach (string word in words)
        {
            Dictionary<char, int> wordCnt = new Dictionary<char, int>();

            foreach (char c in word)
            {
                if (wordCnt.ContainsKey(c))
                {
                    wordCnt[c]++;
                }
                else
                {
                    wordCnt[c] = 1;
                }
            }

            bool isAns = true;

            foreach (char c in word)
            {
                int charsCount = charsCnt.TryGetValue(c, out int availableCount)
                    ? availableCount
                    : 0;

                int wordCount = wordCnt.TryGetValue(c, out int requiredCount)
                    ? requiredCount
                    : 0;

                if (charsCount < wordCount)
                {
                    isAns = false;
                    break;
                }
            }

            if (isAns)
            {
                ans += word.Length;
            }
        }

        return ans;
    }    
}
