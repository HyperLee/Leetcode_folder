# LeetCode 2395 — Find Subarrays With Equal Sum

> 和相等的子陣列｜.NET 10 主控台專案

- [English problem](https://leetcode.com/problems/find-subarrays-with-equal-sum/)
- [中文題目](https://leetcode.cn/problems/find-subarrays-with-equal-sum/)

## 題目說明

給定一個索引從 `0` 開始的整數陣列 `nums`，判斷是否存在兩個長度皆為 `2`、
起始索引不同且元素總和相同的子陣列。兩個子陣列可以重疊；只要起始位置不同，
就視為不同的子陣列。

題目限制：

- `2 <= nums.length <= 1000`
- `-10^9 <= nums[i] <= 10^9`

## 解法

公開 API：

```csharp
public static bool FindSubarrays(int[] nums)
public static bool FindSubarrays2(int[] nums)
```

兩個方法都只讀取 `nums`，不修改輸入、不輸出主控台，也不加入題目契約外的
invalid-input 行為。

### 解法一：HashSet 線性掃描

`FindSubarrays` 由左至右計算每個相鄰元素總和，並嘗試加入 `HashSet<int>`。
`HashSet.Add` 回傳 `false` 代表相同總和先前已由另一個起始索引產生，因此可以立即
回傳 `true`。若所有 `n - 1` 個總和都能加入集合，則回傳 `false`。

### 解法二：暴力比較

`FindSubarrays2` 依序選擇第一個長度為 `2` 的子陣列，再與所有較晚起始的子陣列
比較總和。第二個起始索引從 `firstIndex + 1` 開始，因此不會與同一個子陣列比較，
同時仍允許像 `[4,2]` 與 `[2,4]` 這類重疊子陣列。

這個版本不需要額外集合，適合作為直接定義的教學基準；代價是最壞情況會比較
所有相鄰和配對。

### 核心不變量與易錯處

- 長度為 `n` 的陣列恰好有 `n - 1` 個長度為 `2` 的子陣列，起始索引範圍為
  `0` 到 `n - 2`。
- 題目要求起始索引不同，不要求兩個子陣列互不重疊。
- `HashSet.Add` 的布林回傳值已同時完成查找與插入，不需要先 `Contains` 再 `Add`。
- 暴力解法的第二個索引必須從第一個索引的下一格開始，避免自己與自己比較。
- 題目數值限制使兩個元素的總和落在 `[-2 * 10^9, 2 * 10^9]`，仍在 C# `int`
  範圍內。

### 逐步範例

以 `nums = [1,2,2,1]` 為例：

```plaintext
起始索引 0：[1,2]，總和 3
起始索引 1：[2,2]，總和 4
起始索引 2：[2,1]，總和 3

起始索引 2 再次產生總和 3，與起始索引 0 不同，因此回傳 true。
```

### 複雜度

令 `n` 為陣列長度；布林回傳值的結果空間為 `O(1)`。

| 方法 | 時間 | 輔助空間 | 結果空間 |
| --- | --- | --- | --- |
| `FindSubarrays` | `O(n)` | `O(n)` | `O(1)` |
| `FindSubarrays2` | `O(n²)` | `O(1)` | `O(1)` |

## Acceptance Harness

`Main` 是唯一的 console I/O 邊界。七個確定性案例各以獨立輸入呼叫兩個公開 API；
每個方法分別檢查布林結果與輸入未修改，因此共有 28 個檢查。任何失敗都會將 process
exit code 設為 `1`。長度上限案例使用嚴格遞增陣列，使所有相鄰和皆不同，迫使暴力解法
走完最壞情況。

| # | 輸入 | 預期 | 驗證目的 |
| ---: | --- | --- | --- |
| 1 | `[4,2,4]` | `true` | 官方範例一與重疊子陣列 |
| 2 | `[1,2,3,4,5]` | `false` | 官方範例二與所有總和不同 |
| 3 | `[0,0,0]` | `true` | 官方範例三、零值與相同內容 |
| 4 | `[5,-5]` | `false` | 最小有效長度 |
| 5 | `[1,2,2,1]` | `true` | 相隔一組後才出現重複總和 |
| 6 | `[10^9,10^9,-10^9,-10^9]` | `false` | 元素與相鄰和的數值邊界 |
| 7 | `[0..999]` | `false` | 長度 1000 上限與暴力解法最壞路徑 |

## 建置與執行

已從 repository 根目錄驗證：

```bash
dotnet build leetcode_2395/leetcode_2395/leetcode_2395.csproj --nologo
dotnet run --no-build --project leetcode_2395/leetcode_2395/leetcode_2395.csproj
```

若直接開啟題目根目錄 `leetcode_2395/`，使用：

```bash
dotnet build leetcode_2395/leetcode_2395.csproj --nologo
dotnet run --no-build --project leetcode_2395/leetcode_2395.csproj
```

以下是 fresh run 的完整輸出：

```text
Case: Official example 1 [FindSubarrays]
Input: nums=[4,2,4]
PASS result | Expected: True | Actual: True
PASS input preserved | Expected: True | Actual: True

Case: Official example 1 [FindSubarrays2]
Input: nums=[4,2,4]
PASS result | Expected: True | Actual: True
PASS input preserved | Expected: True | Actual: True

Case: Official example 2 [FindSubarrays]
Input: nums=[1,2,3,4,5]
PASS result | Expected: False | Actual: False
PASS input preserved | Expected: True | Actual: True

Case: Official example 2 [FindSubarrays2]
Input: nums=[1,2,3,4,5]
PASS result | Expected: False | Actual: False
PASS input preserved | Expected: True | Actual: True

Case: Official example 3 [FindSubarrays]
Input: nums=[0,0,0]
PASS result | Expected: True | Actual: True
PASS input preserved | Expected: True | Actual: True

Case: Official example 3 [FindSubarrays2]
Input: nums=[0,0,0]
PASS result | Expected: True | Actual: True
PASS input preserved | Expected: True | Actual: True

Case: Minimum length [FindSubarrays]
Input: nums=[5,-5]
PASS result | Expected: False | Actual: False
PASS input preserved | Expected: True | Actual: True

Case: Minimum length [FindSubarrays2]
Input: nums=[5,-5]
PASS result | Expected: False | Actual: False
PASS input preserved | Expected: True | Actual: True

Case: Separated equal sums [FindSubarrays]
Input: nums=[1,2,2,1]
PASS result | Expected: True | Actual: True
PASS input preserved | Expected: True | Actual: True

Case: Separated equal sums [FindSubarrays2]
Input: nums=[1,2,2,1]
PASS result | Expected: True | Actual: True
PASS input preserved | Expected: True | Actual: True

Case: Value limits [FindSubarrays]
Input: nums=[1000000000,1000000000,-1000000000,-1000000000]
PASS result | Expected: False | Actual: False
PASS input preserved | Expected: True | Actual: True

Case: Value limits [FindSubarrays2]
Input: nums=[1000000000,1000000000,-1000000000,-1000000000]
PASS result | Expected: False | Actual: False
PASS input preserved | Expected: True | Actual: True

Case: Maximum length with unique sums [FindSubarrays]
Input: nums=[0..999] (length 1000)
PASS result | Expected: False | Actual: False
PASS input preserved | Expected: True | Actual: True

Case: Maximum length with unique sums [FindSubarrays2]
Input: nums=[0..999] (length 1000)
PASS result | Expected: False | Actual: False
PASS input preserved | Expected: True | Actual: True

Summary: 28/28 checks passed.
```

## 專案結構

```plaintext
leetcode_2395/
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
└── leetcode_2395/
    ├── Program.cs
    └── leetcode_2395.csproj
```
