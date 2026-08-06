# LeetCode 2053：陣列中第 K 個不重複的字串

[![.NET](https://img.shields.io/badge/.NET-10.0-512BD4)](https://dotnet.microsoft.com/)
[![Language](https://img.shields.io/badge/language-C%23-239120)](https://learn.microsoft.com/dotnet/csharp/)

這是一個以 .NET 10 Console App 實作的 LeetCode 教學專案。專案保留原本的字典解法，並加入暴力逐項比對解法，透過同一組可執行案例比較兩種設計的正確性、時間與空間取捨。

## 快速導覽

- [題目說明](#題目說明)
- [限制條件](#限制條件)
- [解題概念與出發點](#解題概念與出發點)
- [解法一：字典計數](#解法一字典計數)
- [解法二：暴力逐項比對](#解法二暴力逐項比對)
- [解法比較](#解法比較)
- [建置與執行](#建置與執行)
- [實際執行結果](#實際執行結果)

## 題目說明

給定字串陣列 `arr` 與整數 `k`，找出陣列中第 `k` 個 **distinct string**。

這裡的 distinct string 是指「在整個陣列中只出現一次的完整字串」，不是字串中的單一字元。計算第幾個時，必須維持字串在原始陣列中出現的順序，不能先排序。

若不重複字串的數量少於 `k`，回傳空字串 `""`。

題目連結：[2053. Kth Distinct String in an Array](https://leetcode.com/problems/kth-distinct-string-in-an-array/description/)

### 範例

```text
輸入：arr = ["d", "b", "c", "b", "c", "a"], k = 2
輸出："a"
```

`"b"` 與 `"c"` 都出現兩次，因此不屬於 distinct string。只出現一次的字串依原順序為 `"d"`、`"a"`，第 2 個是 `"a"`。

## 限制條件

- `1 <= k <= arr.length <= 1000`
- `1 <= arr[i].length <= 5`
- `arr[i]` 只包含小寫英文字母。

因為題目保證輸入符合上述限制，兩個公開解法都專注於演算法本身，不額外處理 `null`、空陣列或超出範圍的 `k`。

## 解題概念與出發點

這題同時有兩個要求：

1. 判斷某個字串在整個陣列中是否只出現一次。
2. 依照原始輸入順序找出第 `k` 個符合條件的字串。

第一個要求需要知道完整出現次數，因此不能只看目前掃描到的位置就決定答案。第二個要求則表示，即使使用雜湊資料結構計數，最後仍應以 `arr` 為順序來源，而不是依賴資料結構的列舉順序。

本專案提供兩種觀察角度：

- `KthDistinct` 用額外記憶體換取線性時間，適合實際解題。
- `KthDistinctBruteForce` 不配置額外集合，但會重複掃描陣列，適合用來理解最直接的定義式解法。

## 解法一：字典計數

### 設計說明

`KthDistinct` 分成兩輪處理：

1. 第一輪掃描 `arr`，使用 `Dictionary<string, int>` 記錄每個完整字串的出現次數。
2. 第二輪再次掃描原始 `arr`。
3. 當目前字串的次數為 `1`，就將 distinct string 計數加一。
4. 計數等於 `k` 時立即回傳目前字串。
5. 掃描結束仍未找到時，回傳 `string.Empty`。

字典只負責回答「出現幾次」，原陣列則負責回答「出現順序」，兩個責任分開後不會誤用字典的列舉順序。

### 虛擬碼

```text
frequency = 每個字串的出現次數
distinctCount = 0

for value in arr（依原順序）:
    if frequency[value] == 1:
        distinctCount += 1
        if distinctCount == k:
            return value

return ""
```

### 範例演示流程

使用 `arr = ["d", "b", "c", "b", "c", "a"]`、`k = 2`：

| 階段 | 目前資料 | 狀態或判斷 |
| --- | --- | --- |
| 計數完成 | `d:1, b:2, c:2, a:1` | 已知道每個字串的完整頻率 |
| 掃描 `"d"` | 次數為 1 | 第 1 個 distinct string |
| 掃描 `"b"` | 次數為 2 | 略過 |
| 掃描 `"c"` | 次數為 2 | 略過 |
| 再次掃描 `"b"`、`"c"` | 次數都為 2 | 略過 |
| 掃描 `"a"` | 次數為 1 | 第 2 個 distinct string，回傳 `"a"` |

### 複雜度

- 時間複雜度：`O(n)`，建立頻率與依序搜尋各掃描一次陣列。
- 空間複雜度：`O(n)`，最壞情況下字典需要保存所有不同字串。

## 解法二：暴力逐項比對

### 設計說明

`KthDistinctBruteForce` 直接按照題目定義檢查每個候選字串：

1. 從左到右選取一個候選字串。
2. 再掃描整個陣列，計算候選字串的出現次數。
3. 一旦次數大於 `1`，已能確定它不是 distinct string，因此提前停止內層迴圈。
4. 若完整檢查後次數恰好是 `1`，將 distinct string 計數加一。
5. 計數等於 `k` 時回傳候選字串；全部掃描完仍找不到則回傳空字串。

此解法不需要字典或集合，代價是同一批元素會被重複比較。

### 虛擬碼

```text
distinctCount = 0

for candidate in arr（依原順序）:
    occurrenceCount = 0

    for value in arr:
        if value == candidate:
            occurrenceCount += 1
            if occurrenceCount > 1:
                break

    if occurrenceCount == 1:
        distinctCount += 1
        if distinctCount == k:
            return candidate

return ""
```

### 範例演示流程

同樣使用 `arr = ["d", "b", "c", "b", "c", "a"]`、`k = 2`：

| 候選字串 | 比對結果 | distinct 排名 |
| --- | --- | --- |
| `"d"` | 掃描整個陣列後只出現 1 次 | 第 1 個 |
| `"b"` | 找到第 2 次時提前停止 | 不計入 |
| `"c"` | 找到第 2 次時提前停止 | 不計入 |
| 第二個 `"b"` | 找到第 2 次時提前停止 | 不計入 |
| 第二個 `"c"` | 找到第 2 次時提前停止 | 不計入 |
| `"a"` | 掃描整個陣列後只出現 1 次 | 第 2 個，回傳 `"a"` |

### 複雜度

- 時間複雜度：`O(n²)`，最壞情況下每個候選字串都要掃描整個陣列。
- 空間複雜度：`O(1)`，只使用固定數量的索引與計數變數。

## 解法比較

| 解法 | 核心資料結構 | 時間複雜度 | 額外空間 | 特點 |
| --- | --- | --- | --- | --- |
| `KthDistinct` | `Dictionary<string, int>` | `O(n)` | `O(n)` | 效率較佳，計數與順序判斷責任清楚 |
| `KthDistinctBruteForce` | 無 | `O(n²)` | `O(1)` | 寫法直接，適合對照定義與理解空間取捨 |

兩個方法都不會排序或修改 `arr`，因此呼叫前後的輸入內容與順序保持不變。

## 可執行測試設計

`Main` 會執行 6 組固定案例，每組分別驗證兩種解法，共 12 項檢查：

- 官方典型案例。
- 所有字串皆不重複。
- distinct string 數量少於 `k`。
- 單一元素邊界。
- 所有字串都重複。
- 混合重複資料中的原始順序判斷。

每項檢查都列出 Expected、Actual 與 PASS/FAIL。若任一檢查失敗，程式會設定非零結束碼，方便在命令列或自動化流程中偵測錯誤。

## 專案結構

```text
leetcode_2053/
├── README.md
├── docs/
│   └── readme-template.md
├── leetcode_2053.sln
└── leetcode_2053/
    ├── leetcode_2053.csproj
    └── Program.cs
```

## 建置與執行

需求：安裝支援 `net10.0` 的 .NET 10 SDK。

從本 repository 根目錄執行：

```bash
dotnet restore leetcode_2053/leetcode_2053.csproj
dotnet build leetcode_2053/leetcode_2053.csproj --nologo
dotnet run --no-build --project leetcode_2053/leetcode_2053.csproj
```

目前沒有獨立的自動化測試專案；`Main` 中的 12 項自我檢查、成功建置與實際執行結果共同作為驗收依據。

## 實際執行結果

以下內容來自 `dotnet run --no-build --project leetcode_2053/leetcode_2053.csproj` 的實際輸出：

```text
Case: 官方範例一
Input: arr = ["d", "b", "c", "b", "c", "a"], k = 2
KthDistinct:
  Expected: "a"
  Actual:   "a"
  Result:   PASS
KthDistinctBruteForce:
  Expected: "a"
  Actual:   "a"
  Result:   PASS

Case: 官方範例二：全部字串皆不重複
Input: arr = ["aaa", "aa", "a"], k = 1
KthDistinct:
  Expected: "aaa"
  Actual:   "aaa"
  Result:   PASS
KthDistinctBruteForce:
  Expected: "aaa"
  Actual:   "aaa"
  Result:   PASS

Case: 官方範例三：不重複字串不足 k 個
Input: arr = ["a", "b", "a"], k = 3
KthDistinct:
  Expected: ""
  Actual:   ""
  Result:   PASS
KthDistinctBruteForce:
  Expected: ""
  Actual:   ""
  Result:   PASS

Case: 邊界案例：陣列只有一個字串
Input: arr = ["only"], k = 1
KthDistinct:
  Expected: "only"
  Actual:   "only"
  Result:   PASS
KthDistinctBruteForce:
  Expected: "only"
  Actual:   "only"
  Result:   PASS

Case: 全部重複
Input: arr = ["x", "x", "y", "y"], k = 1
KthDistinct:
  Expected: ""
  Actual:   ""
  Result:   PASS
KthDistinctBruteForce:
  Expected: ""
  Actual:   ""
  Result:   PASS

Case: 維持原始順序
Input: arr = ["x", "y", "x", "z", "w", "z"], k = 2
KthDistinct:
  Expected: "w"
  Actual:   "w"
  Result:   PASS
KthDistinctBruteForce:
  Expected: "w"
  Actual:   "w"
  Result:   PASS

Summary: 12/12 checks passed.
```
