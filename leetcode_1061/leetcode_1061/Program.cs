namespace leetcode_1061;

class Program
{
    /// <summary>
    /// 1061. Lexicographically Smallest Equivalent String
    /// https://leetcode.com/problems/lexicographically-smallest-equivalent-string/description/?envType=daily-question&envId=2025-06-05
    /// 1061. 依字典序排列最小的等效字串
    /// https://leetcode.cn/problems/lexicographically-smallest-equivalent-string/description/?envType=daily-question&envId=2025-06-05
    /// 
    /// 題目描述
    /// 你會得到兩個長度相同的字串 s1 和 s2，以及另一個字串 baseStr。
    /// 我們定義：若 s1[i] 和 s2[i] 在同一位置上，則 s1[i] 與 s2[i] 是等價的字元。
    /// 舉個例子：如果 s1 = "abc" 且 s2 = "cde"，那麼我們有以下等價關係：
    /// 'a' 等價於 'c'
    /// 'b' 等價於 'd'
    /// 'c' 等價於 'e'
    /// 這些等價字元遵循等價關係的三個基本規則：
    /// 1. 反身性（Reflexivity）：每個字元與自己等價，例如 'a' == 'a'。
    /// 2. 對稱性（Symmetry）：如果字元 a 與字元 b 等價，那麼字元 b 也與字元 a 等價。
    /// 3. 轉移性（Transitivity）：如果字元 a 與字元 b 等價，且字元 b 與字元 c 等價，那麼字元 a 也與字元 c 等價。
    /// 基於這些等價關係，許多字串可能都是 baseStr 的等價字串。
    /// 例如，根據 s1 = "abc" 與 s2 = "cde" 的資訊：
    /// baseStr = "eed" 的等價字串可以是 "acd" 或 "aab"。
    /// 在所有等價字串中，"aab" 是字典序最小的。
    /// 
    /// 任務
    /// 請你根據從 s1 和 s2 得到的等價關係，回傳 baseStr 所有等價字串中，字典序最小的那一個。
    /// 注意：字典序是指字母表中的順序，例如 'a' < 'b' < 'c' < ... < 'z'。
    /// 
    /// </summary>
    /// <param name="args"></param>
    static void Main(string[] args)
    {
        var solution = new Program();
        string s1 = "parker", s2 = "morris", baseStr = "parser";
        string result = solution.SmallestEquivalentString(s1, s2, baseStr);
        Console.WriteLine(result);
    }


    /// <summary>
    /// 
    /// </summary>
    /// <param name="s1"></param>
    /// <param name="s2"></param>
    /// <param name="baseStr"></param>
    /// <returns></returns>
    public string SmallestEquivalentString(string s1, string s2, string baseStr)
    {
        // 建立 UnionFind 結構來處理等價關係，一共 26 個小寫英文字母（a ~ z）
        var uf = new UnionFind(26);

        // 根據 s1 和 s2 中的對應字元建立等價關係
        for (int i = 0; i < s1.Length; i++)
        {
            // 將字元轉換為 0~25 的索引
            int index1 = s1[i] - 'a';
            int index2 = s2[i] - 'a';
            // 將 s1[i] 和 s2[i] 合併到同一個集合
            uf.Union(index1, index2); 
        }
        
        // 建立一個字元映射表，將每個字元映射到其等價類中的最小字元
        char[] result = new char[baseStr.Length];

        // 根據 baseStr 中的字元，將其轉換為對應的最小字元
        for (int i = 0; i < baseStr.Length; i++)
        {
            // 找到該字元所屬等價類的代表元素（最小字典序字元）
            int root = uf.Find(baseStr[i] - 'a');
            // 將該代表元素轉回字元後放入結果中
            result[i] = (char)(root + 'a');
        }

        // 將結果字元陣列轉成字串並回傳
        return new string(result);
    }
}

public class UnionFind
{
    /// <summary>
    /// 儲存每個節點的父節點（初始每個節點的父節點是自己）
    /// </summary>
    private int[] parent;


    /// <summary>
    /// 建構子：初始化 parent 陣列
    /// </summary>
    /// <param name="size"></param>
    public UnionFind(int size)
    {
        parent = new int[size];
        for (int i = 0; i < size; i++)
        {
            // 一開始每個節點都是自己獨立的集合
            parent[i] = i;
        }
    }


    /// <summary>
    /// 查找 x 的根節點（同時進行路徑壓縮以加速未來查找）
    /// </summary>
    /// <param name="x"></param>
    /// <returns></returns>
    public int Find(int x)
    {
        if (parent[x] != x)
        {
            // 如果 x 的父節點不是自己，則往上找直到找到根節點
            // 並在回溯的過程中壓縮路徑，讓所有節點直接指向根節點
            parent[x] = Find(parent[x]); // 路徑壓縮
        }

        // 回傳根節點
        return parent[x];
    }


    /// <summary>
    /// 合併 x 和 y 所屬的集合
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    public void Union(int x, int y)
    {
        // 找出 x 的根節點
        int rootX = Find(x);
        // 找出 y 的根節點
        int rootY = Find(y);

        if (rootX != rootY)
        {
            // 為了保證字典序最小，將較小的根作為新的根
            if (rootX < rootY)
            { 
                parent[rootY] = rootX; // 將 rootY 的父節點設為 rootX
            }
            else
            {
                parent[rootX] = rootY; // 將 rootX 的父節點設為 rootY
            }
        }
        // 如果根相同，代表已經在同一個集合，無需合併
    }
}
