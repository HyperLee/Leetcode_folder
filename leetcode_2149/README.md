# LeetCode 2149 — Rearrange Array Elements by Sign

> 按正負號重排陣列｜.NET 10 主控台專案

- [English problem](https://leetcode.com/problems/rearrange-array-elements-by-sign/)
- [中文題目](https://leetcode.cn/problems/rearrange-array-elements-by-sign/)

## 題目說明

給定一個長度為偶數的整數陣列 `nums`，其中正整數與負整數的數量相等。回傳一個新陣列，
使每一對相鄰元素的符號相反、第一個元素為正數，且所有同號元素都維持在原陣列中的相對順序。
題目不要求原地修改輸入。

題目限制：

- `2 <= nums.length <= 2 * 10^5`
- `nums.length` 為偶數
- `1 <= |nums[i]| <= 10^5`
- 正整數與負整數數量相等

## 解法

公開 API：

```csharp
public static int[] RearrangeArray(int[] nums)
public static int[] RearrangeArray2(int[] nums)
```

兩個方法都只讀取 `nums`，不修改輸入、不輸出主控台，也不加入題目契約外的 invalid-input
行為。兩者都回傳獨立的新陣列。

### 解法一：直接配置正負索引

`RearrangeArray` 建立與輸入等長的結果陣列，使用兩個寫入索引：

- `positiveIndex` 從 `0` 開始，每次增加 `2`，只放正數。
- `negativeIndex` 從 `1` 開始，每次增加 `2`，只放負數。

輸入由左至右只掃描一次。每個符號的元素都按遇到的順序寫入專屬索引序列，因此同時保證
以正數開頭、符號交錯，以及正數與負數各自的相對順序。

### 解法二：分組後交錯合併

`RearrangeArray2` 先依輸入順序把正數與負數分別加入兩個 `List<int>`。接著使用相同的分組
索引，依序把一個正數與一個負數寫入結果。

這個版本的「先分類、再合併」步驟更直觀，但除了結果陣列外，還需要保存兩個分組。

### 核心不變量與易錯處

- 偶數索引必須放正數，奇數索引必須放負數，結果才會以正數開頭並持續交錯。
- 掃描輸入與寫入每個符號分組時都只能向前，否則會破壞同號元素的相對順序。
- 不可只把所有正數放前面、負數放後面；這不符合相鄰元素符號相反的要求。
- 公開方法必須回傳新陣列；若直接改寫 `nums`，即使結果正確也會破壞本專案的純函式契約。
- 題目保證沒有 `0`，因此 `value > 0` 以外的有效值都可視為負數。

### 逐步範例

以 `nums = [3,1,-2,-5,2,-4]` 為例：

```plaintext
正數的原順序：[3,1,2]
負數的原順序：[-2,-5,-4]

索引 0、1 放入 3、-2
索引 2、3 放入 1、-5
索引 4、5 放入 2、-4

結果：[3,-2,1,-5,2,-4]
```

### 複雜度

令 `n` 為陣列長度。回傳的新陣列屬於結果空間，與演算法為完成題目額外使用的輔助空間分開計算。

| 方法 | 時間 | 輔助空間 | 結果空間 |
| --- | --- | --- | --- |
| `RearrangeArray` | `O(n)` | `O(1)` | `O(n)` |
| `RearrangeArray2` | `O(n)` | `O(n)` | `O(n)` |

## Acceptance Harness

`Main` 是唯一的 console I/O 邊界。七個確定性案例各以兩份獨立輸入呼叫兩個公開 API；每個
方法各檢查精確輸出與輸入未修改，因此共有 28 個檢查。任何失敗都會將 process exit code
設為 `1`。長度上限案例仍比較完整陣列，但輸出只顯示頭尾摘要，避免列印 200,000 個元素。

| # | 輸入摘要 | 預期 | 驗證目的 |
| ---: | --- | --- | --- |
| 1 | `[3,1,-2,-5,2,-4]` | `[3,-2,1,-5,2,-4]` | 官方範例一與一般混合順序 |
| 2 | `[-1,1]` | `[1,-1]` | 官方範例二與最小有效長度 |
| 3 | `[1,2,3,-1,-2,-3]` | `[1,-1,2,-2,3,-3]` | 正數分組在前 |
| 4 | `[-3,-2,-1,3,2,1]` | `[3,-3,2,-2,1,-1]` | 負數分組在前及兩邊相對順序 |
| 5 | `[5,-1,4,-2]` | `[5,-1,4,-2]` | 已符合交錯的輸入 |
| 6 | `[100000,-100000,100000,-100000]` | 原陣列順序 | 數值上下限與重複值 |
| 7 | 正數 `1..100000` 後接負數 `-1..-100000` | 逐對交錯 | 長度 200,000 上限與線性處理 |

## 建置與執行

已從 repository 根目錄驗證：

```bash
dotnet build leetcode_2149/leetcode_2149/leetcode_2149.csproj --nologo
dotnet run --no-build --project leetcode_2149/leetcode_2149/leetcode_2149.csproj
```

若直接開啟題目根目錄 `leetcode_2149/`，使用：

```bash
dotnet build leetcode_2149/leetcode_2149.csproj --nologo
dotnet run --no-build --project leetcode_2149/leetcode_2149.csproj
```

以下是 fresh run 的完整輸出：

```text
Case: 1 - Official example 1
Input: nums=[3,1,-2,-5,2,-4]
PASS RearrangeArray result | Expected: [3,-2,1,-5,2,-4] | Actual: [3,-2,1,-5,2,-4]
PASS RearrangeArray input preserved | Expected: True | Actual: True
PASS RearrangeArray2 result | Expected: [3,-2,1,-5,2,-4] | Actual: [3,-2,1,-5,2,-4]
PASS RearrangeArray2 input preserved | Expected: True | Actual: True

Case: 2 - Official example 2 and minimum length
Input: nums=[-1,1]
PASS RearrangeArray result | Expected: [1,-1] | Actual: [1,-1]
PASS RearrangeArray input preserved | Expected: True | Actual: True
PASS RearrangeArray2 result | Expected: [1,-1] | Actual: [1,-1]
PASS RearrangeArray2 input preserved | Expected: True | Actual: True

Case: 3 - Positive group before negative group
Input: nums=[1,2,3,-1,-2,-3]
PASS RearrangeArray result | Expected: [1,-1,2,-2,3,-3] | Actual: [1,-1,2,-2,3,-3]
PASS RearrangeArray input preserved | Expected: True | Actual: True
PASS RearrangeArray2 result | Expected: [1,-1,2,-2,3,-3] | Actual: [1,-1,2,-2,3,-3]
PASS RearrangeArray2 input preserved | Expected: True | Actual: True

Case: 4 - Negative group before positive group
Input: nums=[-3,-2,-1,3,2,1]
PASS RearrangeArray result | Expected: [3,-3,2,-2,1,-1] | Actual: [3,-3,2,-2,1,-1]
PASS RearrangeArray input preserved | Expected: True | Actual: True
PASS RearrangeArray2 result | Expected: [3,-3,2,-2,1,-1] | Actual: [3,-3,2,-2,1,-1]
PASS RearrangeArray2 input preserved | Expected: True | Actual: True

Case: 5 - Already alternating
Input: nums=[5,-1,4,-2]
PASS RearrangeArray result | Expected: [5,-1,4,-2] | Actual: [5,-1,4,-2]
PASS RearrangeArray input preserved | Expected: True | Actual: True
PASS RearrangeArray2 result | Expected: [5,-1,4,-2] | Actual: [5,-1,4,-2]
PASS RearrangeArray2 input preserved | Expected: True | Actual: True

Case: 6 - Value limits with duplicates
Input: nums=[100000,-100000,100000,-100000]
PASS RearrangeArray result | Expected: [100000,-100000,100000,-100000] | Actual: [100000,-100000,100000,-100000]
PASS RearrangeArray input preserved | Expected: True | Actual: True
PASS RearrangeArray2 result | Expected: [100000,-100000,100000,-100000] | Actual: [100000,-100000,100000,-100000]
PASS RearrangeArray2 input preserved | Expected: True | Actual: True

Case: 7 - Maximum length
Input: nums=[1..100000,-1..-100000] (length 200000)
PASS RearrangeArray result | Expected: [1,-1,2,...,-99999,100000,-100000] (length 200000) | Actual: [1,-1,2,...,-99999,100000,-100000] (length 200000)
PASS RearrangeArray input preserved | Expected: True | Actual: True
PASS RearrangeArray2 result | Expected: [1,-1,2,...,-99999,100000,-100000] (length 200000) | Actual: [1,-1,2,...,-99999,100000,-100000] (length 200000)
PASS RearrangeArray2 input preserved | Expected: True | Actual: True

Summary: 28/28 checks passed.
```

## 專案結構

```plaintext
leetcode_2149/
├── .editorconfig
├── .gitattributes
├── .gitignore
├── .vscode/
│   ├── launch.json
│   └── tasks.json
├── AGENTS.md
├── README.md
├── docs/
│   └── readme-template.md
└── leetcode_2149/
    ├── Program.cs
    └── leetcode_2149.csproj
```
