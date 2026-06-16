namespace leetcode_205;

class Program
{
    /// <summary>
    /// 205. Isomorphic Strings
    /// https://leetcode.com/problems/isomorphic-strings/description/
    /// 205. 同構字串
    /// https://leetcode.cn/problems/isomorphic-strings/description/
    ///
    /// Given two strings s and t, determine if they are isomorphic.
    /// Two strings s and t are isomorphic if the characters in s can be replaced to get t.
    /// All occurrences of a character must be replaced with another character while preserving the order of characters.
    /// No two characters may map to the same character, but a character may map to itself.
    ///
    /// 給定兩個字串 s 與 t，判斷它們是否為同構字串。
    /// 如果可以將 s 中的字元逐一替換後得到 t，則 s 和 t 為同構字串。
    /// 同一個字元在所有出現的位置都必須替換成同一個字元，並且要維持字元順序不變。
    /// 不同字元不能對應到同一個字元，但字元可以對應到自己。
    /// </summary>
    /// <param name="args"></param>
    static void Main(string[] args)
    {
        Console.WriteLine("Hello, World!");
    }

    /// <summary>
    /// 解法一
    /// 
    /// 使用字符首次出现的等价序列
    /// 
    /// 例子： 
    /// 統計s與t每個字母出現的下標拿出來比對
    /// paper中p首次出现下标为0，a为1，e为3，r为4，则paper转为[0, 1, 0, 3, 4]
    /// title中t首次出现下标为0，i为1，l为3，e为4，则title转为[0, 1, 0, 3, 4]
    /// 因为下标数组一致，所以双方同构
    /// 
    /// 儲存s與t兩個字串中
    /// 每一個字母出現的 下標
    /// </summary>
    /// <param name="s"></param>
    /// <param name="t"></param>
    /// <returns></returns>
    public bool IsIsomorphic(string s, string t)
    {
        List<int> indexS = new List<int>();
        List<int> indexT = new List<int>();

        for(int i = 0; i < s.Length; i++)
        {
            indexS.Add(s.IndexOf(s[i]));
        }

        for(int j = 0; j < t.Length; j++)
        {
            indexT.Add(t.IndexOf(t[j]));
        }

        return indexS.SequenceEqual(indexT);
    }

    /// <summary>
    /// 解法二: 使用 Dictionary 統計
    /// 需要我们判断 s 和 t 每个位置上的字符是否都一一对应，即 s 的任意一个字符被 t 中唯一的
    /// 字符对应，同时 t 的任意一个字符被 s 中唯一的字符对应。这也被称为「双射」的关系。
    /// 
    /// 以示例 2 为例，t 中的字符 a 和 r 虽然有唯一的映射 o，但对于 s 中的字符 o 来说其存在两个映射 {a,r}，故不满足条件。
    /// 因此，我们维护两张哈希表，第一张哈希表 s2t 以 s 中字符为键，映射至 t 的字符为值，第二张哈希表 t2s 以 t 中字符为键，映射
    /// 至 s 的字符为值。从左至右遍历两个字符串的字符，不断更新两张哈希表，如果出现冲突（即当前下标 index 对应的字符 s[index]
    /// 已经存在映射且不为 t[index] 或当前下标 index 对应的字符 t[index] 已经存在映射且不为 s[index]）时说明两个字符串无法构成同
    /// 构，返回 false。
    /// 
    /// 如果遍历结束没有出现冲突，则表明两个字符串是同构的，返回 true 即可。
    /// </summary>
    /// <param name="s"></param>
    /// <param name="t"></param>
    /// <returns></returns>
    public bool IsIsomorphic2(string s, string t)
    {
        Dictionary<char, char> sTot = new Dictionary<char, char>();
        Dictionary<char, char> tTos = new Dictionary<char, char>();

        for(int i = 0; i < s.Length; i++)
        {
            char x = s[i];
            char y = t[i];

            if(sTot.TryGetValue(x, out char mappedY) && mappedY != y)
            {
                return false;
            }

            if(tTos.TryGetValue(y, out char mappedX) && mappedX != x)
            {
                return false;
            }

            sTot[x] = y;
            tTos[y] = x;
        }
        return true;
    }
}
