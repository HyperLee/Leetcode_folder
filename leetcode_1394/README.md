# leetcode_1394 專案說明

## 題目簡介

1394. 找出數組中的幸運數 ([LeetCode 連結](https://leetcode.com/problems/find-lucky-integer-in-an-array/))

給定一個整數陣列，找出所有「幸運數」：數值等於其在陣列中出現次數的數字。若有多個幸運數，回傳其中最大者；若沒有則回傳 -1。

---

## 專案結構

- `leetcode_1394/Program.cs`：主程式與解題邏輯，包含兩種解法（Dictionary 與計數陣列）及完整註解
- `.vscode/`：VS Code 偵錯與建構設定

---

## 解法一：Dictionary（字典）解法

### 程式碼邏輯
1. 使用 Dictionary 統計每個數字出現次數。
2. 遍歷所有 key-value，找出 key == value 的數字，並記錄最大值。
3. 回傳最大幸運數，若無則回傳 -1。

### 程式碼片段
```csharp
// 建立一個字典來記錄每個數字出現的次數
Dictionary<int, int> countMap = new Dictionary<int, int>();
foreach (int num in arr)
{
    if (countMap.ContainsKey(num))
        countMap[num]++;
    else
        countMap[num] = 1;
}
int maxLucky = -1;
foreach(var kvp in countMap)
{
    if (kvp.Key == kvp.Value && kvp.Key > maxLucky)
        maxLucky = kvp.Key;
}
return maxLucky;
```

### 優點
- 適用於數字範圍未知或很大時。
- 實作簡單，易於理解。

### 缺點
- 需額外儲存 Dictionary，空間複雜度 O(n)。

### 時間複雜度
- O(n)

---

## 解法二：計數陣列（Array）解法

### 程式碼邏輯
1. 題目數字範圍為 1~500，直接建立 501 長度的計數陣列。
2. 統計每個數字出現次數。
3. 從大到小遍歷所有可能的數字，找到第一個 count[i] == i 即回傳。
4. 若無則回傳 -1。

### 程式碼片段
```csharp
// 建立一個長度為501的計數陣列（題目數字範圍1~500）
int[] count = new int[501];
// 統計每個數字出現的次數
foreach(int num in arr)
{
    count[num]++;
}
// 從大到小遍歷，找出最大且數值等於出現次數的幸運數
for(int i = 500; i >= 1; i--)
{
    if(count[i] == i)
        return i;
}
// 若無幸運數則回傳 -1
return -1;
```

### 優點
- 當數字範圍已知且不大時，效能極佳。
- 陣列存取速度快，常數小。

### 缺點
- 只適用於數字範圍小且連續的情境。
- 若數字範圍很大，空間浪費嚴重。

### 時間複雜度
- O(n + k)，k 為數字範圍（本題 k=500，視為常數）

---

## 測試與執行

- 主程式入口為 `Program.cs`，內含多組測試資料與預期結果。
- 執行方式：
  1. 使用 VS Code 開啟本資料夾。
  2. 按 F5 或點選「執行與偵錯」即可直接執行測試。
  3. 可自行修改 `Main` 內的測試資料。

---

## 效能與比較

| 解法         | 適用情境           | 時間複雜度 | 空間複雜度 | 備註             |
|--------------|--------------------|------------|------------|------------------|
| Dictionary   | 數字範圍未知/很大  | O(n)       | O(n)       | 通用、彈性高      |
| 計數陣列     | 數字範圍小且已知   | O(n)       | O(k)       | 常數小、極快      |

- 本題數字範圍 1~500，建議優先使用計數陣列解法。
- 若遇到數字範圍未知或極大，則建議使用 Dictionary 解法。

---

## 參考
- [LeetCode 1394](https://leetcode.com/problems/find-lucky-integer-in-an-array/)
- [LeetCode 中文站](https://leetcode.cn/problems/find-lucky-integer-in-an-array/)
