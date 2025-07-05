namespace leetcode_1394;

class Program
{
    /// <summary>
    /// 1394. Find Lucky Integer in an Array
    /// https://leetcode.com/problems/find-lucky-integer-in-an-array/description/?envType=daily-question&envId=2025-07-05
    /// 
    /// 1394. 找出數組中的幸運數
    /// https://leetcode.cn/problems/find-lucky-integer-in-an-array/description/?envType=daily-question&envId=2025-07-05
    /// 
    /// </summary>
    /// <param name="args"></param> <summary>
    /// 
    /// </summary>
    /// <param name="args"></param>
    static void Main(string[] args)
    {
        // 測試資料
        int[] arr1 = {2, 2, 3, 4};
        int[] arr2 = {1, 2, 2, 3, 3, 3};
        int[] arr3 = {5};
        int[] arr4 = {7,7,7,7,7,7,7};
        int[] arr5 = {1,2,3,4,5,6,7,8,9,10};

        Program p = new Program();
        Console.WriteLine($"arr1: {p.FindLucky(arr1)}"); // 預期: 2
        Console.WriteLine($"arr2: {p.FindLucky(arr2)}"); // 預期: 3
        Console.WriteLine($"arr3: {p.FindLucky(arr3)}"); // 預期: -1
        Console.WriteLine($"arr4: {p.FindLucky(arr4)}"); // 預期: 7
        Console.WriteLine($"arr5: {p.FindLucky(arr5)}"); // 預期: -1
    }

    /// <summary>
    /// 題目解說：
    /// 給定一個整數陣列，找出所有「幸運數」：數值等於其在陣列中出現次數的數字。
    /// 若有多個幸運數，回傳其中最大者；若沒有則回傳 -1。
    /// 解題思路：
    /// 1. 使用字典統計每個數字出現次數。
    /// 2. 遍歷字典，找出所有 key == value 的數字，並記錄最大值。
    /// 3. 回傳最大幸運數，若無則回傳 -1。
    /// </summary>
    /// <param name="arr">輸入的整數陣列</param>
    /// <returns>最大幸運數，若無則回傳 -1</returns>
    public int FindLucky(int[] arr)
    {
        // 建立一個字典來記錄每個數字出現的次數
        Dictionary<int, int> countMap = new Dictionary<int, int>();
        foreach (int num in arr)
        {
            // 如果字典已經有這個數字，次數加一
            if (countMap.ContainsKey(num))
            {
                countMap[num]++;
            }
            else
            {
                // 否則新增這個數字，並設為 1
                countMap[num] = 1;
            }
        }

        int maxLucky = -1; // 用來記錄最大幸運數，預設為 -1
        // 遍歷字典，檢查是否有 key == value 的情況
        foreach(var kvp in countMap)
        {
            // 如果數字等於其出現次數
            if (kvp.Key == kvp.Value)
            {
                // 並且比目前記錄的最大值還大，則更新最大值
                if (kvp.Key > maxLucky)
                {
                    maxLucky = kvp.Key;
                }
            }
        }
        // 回傳最大幸運數，若無則回傳 -1
        return maxLucky;
    }
}
