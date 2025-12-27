namespace leetcode_1013;

class Program
{
    /// <summary>
    /// 1013. Partition Array Into Three Parts With Equal Sum
    /// https://leetcode.com/problems/partition-array-into-three-parts-with-equal-sum/description/
    /// 1013. 将数组分成和相等的三个部分 (简体中文)
    /// https://leetcode.cn/problems/partition-array-into-three-parts-with-equal-sum/description/
    /// 1013. 將陣列分成和相等的三個部分 (繁體中文)
    /// 
    /// 題目描述（繁體中文）：
    /// 給定一個整數陣列 `arr`，若能將陣列分成三個非空的連續部分且三部分的總和相等，則回傳 `true`。
    /// 形式上，若存在索引 `i` 與 `j`（滿足 `i + 1 < j`），使得
    /// `arr[0] + ... + arr[i] == arr[i+1] + ... + arr[j-1] == arr[j] + ... + arr[arr.Length - 1]`，
    /// 則表示可分割，否則回傳 `false`。
    /// </summary>
    /// <param name="args"></param>
    static void Main(string[] args)
    {
        var solution = new Program();

        // 測試案例 1：可以三等分 [0,2,1,-6,6,-7,9,1,2,0,1] 總和為 9，每部分為 3
        int[] arr1 = [0, 2, 1, -6, 6, -7, 9, 1, 2, 0, 1];
        Console.WriteLine($"測試案例 1: [{string.Join(", ", arr1)}]");
        Console.WriteLine($"結果: {solution.CanThreePartsEqualSum(arr1)}");  // 預期: True
        Console.WriteLine();

        // 測試案例 2：無法三等分 [0,2,1,-6,6,7,9,-1,2,0,1] 總和為 21，無法被 3 整除
        int[] arr2 = [0, 2, 1, -6, 6, 7, 9, -1, 2, 0, 1];
        Console.WriteLine($"測試案例 2: [{string.Join(", ", arr2)}]");
        Console.WriteLine($"結果: {solution.CanThreePartsEqualSum(arr2)}");  // 預期: False
        Console.WriteLine();

        // 測試案例 3：特殊情況 [3,3,6,5,-2,2,5,1,-9,4] 總和為 18，每部分為 6
        int[] arr3 = [3, 3, 6, 5, -2, 2, 5, 1, -9, 4];
        Console.WriteLine($"測試案例 3: [{string.Join(", ", arr3)}]");
        Console.WriteLine($"結果: {solution.CanThreePartsEqualSum(arr3)}");  // 預期: True
        Console.WriteLine();

        // 測試案例 4：邊界情況 [1,-1,1,-1] 總和為 0，但無法分成三個非空連續部分
        int[] arr4 = [1, -1, 1, -1];
        Console.WriteLine($"測試案例 4: [{string.Join(", ", arr4)}]");
        Console.WriteLine($"結果: {solution.CanThreePartsEqualSum(arr4)}");  // 預期: True (0+0+0)
        Console.WriteLine();

        // 測試案例 5：全為零 [0,0,0,0,0]
        int[] arr5 = [0, 0, 0, 0, 0];
        Console.WriteLine($"測試案例 5: [{string.Join(", ", arr5)}]");
        Console.WriteLine($"結果: {solution.CanThreePartsEqualSum(arr5)}");  // 預期: True
    }

    /// <summary>
    /// 判斷是否能將陣列分成和相等的三個部分。
    /// <para>
    /// <b>解題思路：前綴和（Prefix Sum）</b>
    /// </para>
    /// <para>
    /// 核心概念：將陣列轉換為前綴和陣列後，透過尋找分割點來判斷是否可三等分。
    /// </para>
    /// <para>
    /// <b>演算法步驟：</b>
    /// <list type="number">
    ///   <item>
    ///     <description>建構前綴和陣列：將每個位置的值改為從索引 0 到該位置的累加和。</description>
    ///   </item>
    ///   <item>
    ///     <description>檢查總和是否能被 3 整除，若不能則直接回傳 false。</description>
    ///   </item>
    ///   <item>
    ///     <description>計算目標值（總和 / 3），並尋找分割點。</description>
    ///   </item>
    ///   <item>
    ///     <description>遍歷陣列（不含最後一個元素），當前綴和等於 target * count 時，代表找到一個分割點。</description>
    ///   </item>
    ///   <item>
    ///     <description>若找到至少 2 個分割點（count >= 3），則可三等分。</description>
    ///   </item>
    /// </list>
    /// </para>
    /// <para>
    /// <b>時間複雜度：</b> O(n)，只需遍歷陣列兩次。
    /// </para>
    /// <para>
    /// <b>空間複雜度：</b> O(1)，原地修改陣列。
    /// </para>
    /// </summary>
    /// <param name="arr">輸入的整數陣列</param>
    /// <returns>若能分成三個和相等的非空連續部分則回傳 true，否則回傳 false</returns>
    /// <remarks>
    /// 參考資料：
    /// <list type="bullet">
    ///   <item><see href="https://leetcode.cn/problems/partition-array-into-three-parts-with-equal-sum/solution/1013-jiang-shu-zu-fen-cheng-he-xiang-deng-de-san-2/">LeetCode 官方題解</see></item>
    ///   <item><see href="https://leetcode.cn/problems/partition-array-into-three-parts-with-equal-sum/solution/by-mulberry_qs-7utz/">前綴和解法詳解</see></item>
    /// </list>
    /// </remarks>
    /// <example>
    /// <code>
    ///  範例 1：可以三等分
    /// int[] arr1 = [0, 2, 1, -6, 6, -7, 9, 1, 2, 0, 1];
    /// bool result1 = CanThreePartsEqualSum(arr1); // 回傳 true
    ///  前綴和：[0, 2, 3, -3, 3, -4, 5, 6, 8, 8, 9]
    ///  總和 9，目標值 3，分割點在索引 2 和 6
    /// 
    ///  範例 2：無法三等分
    /// int[] arr2 = [0, 2, 1, -6, 6, 7, 9, -1, 2, 0, 1];
    /// bool result2 = CanThreePartsEqualSum(arr2); // 回傳 false
    ///  總和 21，無法被 3 整除
    /// </code>
    /// </example>
    public bool CanThreePartsEqualSum(int[] arr)
    {
        int n = arr.Length;

        // 1. 將陣列轉換為前綴和（Prefix Sum）：每個元素改為從索引 0 到該位置的累加和
        for (int i = 1; i < n; i++)
        {
            arr[i] += arr[i - 1];
        }

        // 2. 判斷總和是否可被 3 整除，若不可則直接回傳 false
        if (arr[n - 1] % 3 != 0)
        {
            return false;
        }

        // 3. 取得每部分的目標值（總和 / 3），並將計數器初始化為 1
        int target = arr[n - 1] / 3;
        int count = 1;

        // 5. 因為陣列最後一個前綴和必定是 target * 3，所以不需遍歷到最後一個元素
        //    以避免像 [1, -1, 1, -1] 這類邊界狀況導致錯誤判斷
        for (int i = 0; i < n - 1; i++)
        {
            // 4. 當前綴和等於 target * count 時，代表找到一個分割點
            if (arr[i] == target * count)
            {
                count++;
            }
        }

        // 6. 若找到至少兩個分割點（count >= 3）則表示可三等分
        return count >= 3;
    }
}
