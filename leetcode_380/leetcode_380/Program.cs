namespace leetcode_380;

internal static class Program
{
    private static int s_checks;
    private static int s_passed;

    /// <summary>
    /// 380. Insert Delete GetRandom O(1)
    /// https://leetcode.com/problems/insert-delete-getrandom-o1/
    /// 380. O(1) 時間插入、刪除和取得隨機元素
    /// https://leetcode.cn/problems/insert-delete-getrandom-o1/
    /// Design a set that supports inserting, removing, and returning a random element in average O(1) time.
    /// 設計一個集合，在平均 O(1) 時間內完成插入、刪除，以及隨機回傳現有元素。
    /// </summary>
    private static void Main()
    {
        Console.WriteLine("LeetCode 380 acceptance harness");
        Console.WriteLine();

        RunOfficialSequence();
        RunDuplicateAndMissingCases();
        RunMiddleRemovalCase();
        RunRandomCases();
        RunExtremeValueCase();

        Console.WriteLine();
        Console.WriteLine($"Summary: {s_passed}/{s_checks} checks passed.");

        if (s_passed != s_checks)
        {
            Environment.ExitCode = 1;
        }
    }

    /// <summary>
    /// 執行題目官方示範操作序列，驗證集合在插入、刪除與隨機取值後是否維持正確狀態。
    /// 解題概念是用固定步驟重現題目範例，確認 List 與 Dictionary 的同步更新符合預期。
    /// 輸入條件為無，由方法內部建立測試資料；輸出結果為無，驗證結果會直接列印到主控台。
    /// </summary>
    private static void RunOfficialSequence()
    {
        Console.WriteLine("Case 1: official operation sequence");
        RandomizedSet set = new();

        Check("Insert(1)", true, set.Insert(1));
        Check("Remove(2)", false, set.Remove(2));
        Check("Insert(2)", true, set.Insert(2));
        Check("GetRandom() belongs to {1, 2}", true, IsRandomMember(set, [1, 2]));
        Check("Remove(1)", true, set.Remove(1));
        Check("Insert(2) duplicate", false, set.Insert(2));
        Check("GetRandom() after official sequence", 2, set.GetRandom());
        Console.WriteLine();
    }

    /// <summary>
    /// 驗證重複插入與刪除不存在元素的邊界情境，確認集合只接受唯一值。
    /// 解題概念是刻意覆蓋哈希表查找失敗與重複鍵判斷，檢查平均 O(1) 流程的布林回傳是否正確。
    /// 輸入條件為無，方法內部使用固定測資；輸出結果為無，會將每一步的驗證結果寫到主控台。
    /// </summary>
    private static void RunDuplicateAndMissingCases()
    {
        Console.WriteLine("Case 2: duplicate insertion and missing removal");
        RandomizedSet set = new();

        Check("Insert(7)", true, set.Insert(7));
        Check("Insert(7) duplicate", false, set.Insert(7));
        Check("Remove(8) missing", false, set.Remove(8));
        Check("Remove(7)", true, set.Remove(7));
        Check("Remove(7) after deletion", false, set.Remove(7));
        Console.WriteLine();
    }

    /// <summary>
    /// 驗證刪除中間元素時的索引修補流程，確保尾端元素搬移後仍能被正確存取。
    /// 解題概念是覆蓋 swap-with-last 刪除策略最容易出錯的情境，確認陣列與索引表保持一致。
    /// 輸入條件為無，方法內部建立固定操作序列；輸出結果為無，驗證狀態會列印到主控台。
    /// </summary>
    private static void RunMiddleRemovalCase()
    {
        Console.WriteLine("Case 3: middle-element removal and index repair");
        RandomizedSet set = new();

        Check("Insert(10)", true, set.Insert(10));
        Check("Insert(20)", true, set.Insert(20));
        Check("Insert(30)", true, set.Insert(30));
        Check("Remove(20) from middle", true, set.Remove(20));
        Check("Random member after middle removal", true, IsRandomMember(set, [10, 30]));
        Check("Remove(30) after index repair", true, set.Remove(30));
        Check("GetRandom() after repaired removal", 10, set.GetRandom());
        Console.WriteLine();
    }

    /// <summary>
    /// 驗證多元素集合的隨機結果是否落在合法集合中，以及單一元素時是否必定回傳唯一值。
    /// 解題概念是將隨機性檢查轉成成員資格判斷，避免測試依賴特定亂數順序。
    /// 輸入條件為無，方法內部準備固定測資；輸出結果為無，檢查結果會輸出到主控台。
    /// </summary>
    private static void RunRandomCases()
    {
        Console.WriteLine("Case 4: random membership and exact singleton return");
        RandomizedSet multiple = new();
        multiple.Insert(-5);
        multiple.Insert(0);
        multiple.Insert(5);

        Check("64 random draws belong to {-5, 0, 5}", true, IsRandomMember(multiple, [-5, 0, 5], 64));

        RandomizedSet singleton = new();
        singleton.Insert(42);
        Check("Single-element GetRandom()", 42, singleton.GetRandom());
        Console.WriteLine();
    }

    /// <summary>
    /// 驗證整數極值在插入、刪除與隨機取值時仍能被正確處理。
    /// 解題概念是使用 <c>int.MinValue</c> 與 <c>int.MaxValue</c> 檢查哈希索引與陣列存取不受值域影響。
    /// 輸入條件為無，測資由方法內部建立；輸出結果為無，所有驗證資訊會直接印出。
    /// </summary>
    private static void RunExtremeValueCase()
    {
        Console.WriteLine("Case 5: integer extremes");
        RandomizedSet set = new();

        Check("Insert(int.MinValue)", true, set.Insert(int.MinValue));
        Check("Insert(int.MaxValue)", true, set.Insert(int.MaxValue));
        Check("Random extreme membership", true, IsRandomMember(set, [int.MinValue, int.MaxValue]));
        Check("Remove(int.MinValue)", true, set.Remove(int.MinValue));
        Check("GetRandom() with int.MaxValue remaining", int.MaxValue, set.GetRandom());
    }

    /// <summary>
    /// 連續呼叫 <see cref="RandomizedSet.GetRandom"/>，確認每次結果都屬於預期集合。
    /// 解題概念是把隨機測試縮減為集合成員檢查，只驗證回傳值是否合法，而非要求固定順序。
    /// 輸入條件是 <paramref name="set"/> 不可為空，<paramref name="expectedValues"/> 需列出所有合法值，<paramref name="draws"/> 需大於等於 1。
    /// 輸出結果為布林值；若每次抽樣都命中合法集合則回傳 <see langword="true"/>，否則回傳 <see langword="false"/>。
    /// </summary>
    /// <param name="set">要驗證隨機輸出的 <see cref="RandomizedSet"/>。</param>
    /// <param name="expectedValues">允許被抽到的所有值。</param>
    /// <param name="draws">抽樣次數，預設為 1。</param>
    /// <returns>所有抽樣結果都落在合法集合內時回傳 <see langword="true"/>，否則回傳 <see langword="false"/>。</returns>
    private static bool IsRandomMember(RandomizedSet set, int[] expectedValues, int draws = 1)
    {
        HashSet<int> expected = [.. expectedValues];

        for (int i = 0; i < draws; i++)
        {
            // 題目只要求隨機值來自目前集合，因此測試重點是成員合法性，而不是出現順序或分布比例。
            if (!expected.Contains(set.GetRandom()))
            {
                return false;
            }
        }

        return true;
    }

    /// <summary>
    /// 比對單筆驗證的預期值與實際值，並累計整體通過數。
    /// 解題概念是集中管理驗證輸出格式與統計狀態，讓每個測試案例只關注輸入與預期結果。
    /// 輸入條件為描述文字不可為空，<paramref name="expected"/> 與 <paramref name="actual"/> 需可由預設相等比較器比較。
    /// 輸出結果為無，方法會更新計數器並輸出 PASS 或 FAIL 訊息。
    /// </summary>
    /// <typeparam name="T">要比較的資料型別。</typeparam>
    /// <param name="description">此筆驗證的說明文字。</param>
    /// <param name="expected">預期結果。</param>
    /// <param name="actual">實際結果。</param>
    private static void Check<T>(string description, T expected, T actual)
    {
        s_checks++;
        bool passed = EqualityComparer<T>.Default.Equals(expected, actual);

        if (passed)
        {
            s_passed++;
        }

        Console.WriteLine($"{(passed ? "PASS" : "FAIL")} | {description} | Expected: {expected} | Actual: {actual}");
    }
}

/// <summary>
/// 使用動態陣列加上值到索引的對照表，實作平均 O(1) 的插入、刪除與隨機取值集合。
/// 解題概念是讓 List 負責 O(1) 隨機存取，Dictionary 負責 O(1) 查找元素位置，並在刪除時以尾端元素補洞。
/// 輸入條件是元素值可重複呼叫 API 操作，但集合本身只保留唯一值；輸出結果依各 API 定義回傳布林值或隨機元素。
/// </summary>
public class RandomizedSet
{
    private readonly List<int> _values;
    private readonly Dictionary<int, int> _indices;
    private readonly Random _random;

    /// <summary>
    /// 初始化空集合與對應索引表，為平均 O(1) 操作建立基礎資料結構。
    /// 解題概念是同步準備動態陣列、索引字典與亂數來源，讓後續 API 可直接操作共享狀態。
    /// 輸入條件為無；輸出結果為一個可接受 Insert、Remove、GetRandom 操作的空集合實例。
    /// </summary>
    public RandomizedSet()
    {
        _values = [];
        _indices = [];
        _random = new Random();
    }

    /// <summary>
    /// 將新值加入集合，並記錄它在動態陣列中的索引位置。
    /// 解題概念是先用字典判斷是否已存在，若不存在就把值附加到尾端，使插入維持平均 O(1)。
    /// 輸入條件是 <paramref name="val"/> 可為任意整數；輸出結果為布林值，表示此次插入是否真的新增元素。
    /// </summary>
    /// <param name="val">要插入集合的整數值。</param>
    /// <returns>值原本不存在並成功插入時回傳 <see langword="true"/>，若已存在則回傳 <see langword="false"/>。</returns>
    public bool Insert(int val)
    {
        // Dictionary 先做存在性判斷，避免為了檢查重複值而退化成線性搜尋。
        if (_indices.ContainsKey(val))
        {
            return false;
        }

        // 新元素永遠追加在尾端，索引就是加入前的 Count，因此可以同步維持兩份結構一致。
        _indices[val] = _values.Count;
        _values.Add(val);
        return true;
    }

    /// <summary>
    /// 從集合中刪除指定值，並在必要時以尾端元素補上被移除位置，維持資料緊密排列。
    /// 解題概念是利用字典先找到目標索引，再以 swap-with-last 避免中間刪除造成 O(n) 位移。
    /// 輸入條件是 <paramref name="val"/> 可為任意整數；輸出結果為布林值，表示集合中是否真的存在並刪除了該元素。
    /// </summary>
    /// <param name="val">要自集合中移除的整數值。</param>
    /// <returns>值存在且成功移除時回傳 <see langword="true"/>，否則回傳 <see langword="false"/>。</returns>
    public bool Remove(int val)
    {
        // 找不到索引就代表元素不存在，可直接在 O(1) 內回傳失敗。
        if (!_indices.TryGetValue(val, out int index))
        {
            return false;
        }

        int lastIndex = _values.Count - 1;
        int lastValue = _values[lastIndex];

        // 把尾端元素搬到待刪位置，避免 List 中間刪除導致後續元素整段左移。
        _values[index] = lastValue;
        // 尾端元素的新位置已改變，Dictionary 必須同步修正索引，才能維持 O(1) 查找正確性。
        _indices[lastValue] = index;
        _values.RemoveAt(lastIndex);
        _indices.Remove(val);
        return true;
    }

    /// <summary>
    /// 從目前集合中等機率挑選一個元素並回傳。
    /// 解題概念是利用 List 的索引隨機存取能力，搭配亂數索引在平均 O(1) 內取得結果。
    /// 輸入條件是集合不可為空，這也符合題目保證；輸出結果為集合內其中一個現存整數值。
    /// </summary>
    /// <returns>目前集合中的任一元素。</returns>
    public int GetRandom()
    {
        return _values[_random.Next(_values.Count)];
    }
}
