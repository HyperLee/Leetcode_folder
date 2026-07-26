# LeetCode 2215 — Find the Difference of Two Arrays

> 找出兩陣列的不同｜.NET 10 主控台專案

- [English problem](https://leetcode.com/problems/find-the-difference-of-two-arrays/)
- [中文題目](https://leetcode.cn/problems/find-the-difference-of-two-arrays/)

## 題目說明

給定兩個索引從 0 開始的整數陣列 `nums1` 與 `nums2`，回傳長度為 2 的列表：

- `answer[0]` 包含只存在於 `nums1`、不存在於 `nums2` 的所有相異整數。
- `answer[1]` 包含只存在於 `nums2`、不存在於 `nums1` 的所有相異整數。

兩個內部列表的元素可依任意順序回傳。

題目限制：

- `1 <= nums1.length, nums2.length <= 1000`
- `-1000 <= nums1[i], nums2[i] <= 1000`

## 解法一：HashSet 雙向差集

建立 `firstOnly` 與 `secondOnly` 兩個 `HashSet<int>` 時，重複值會先被消除。接著執行：

- `firstOnly.ExceptWith(nums2)`：移除所有也出現在 `nums2` 的值。
- `secondOnly.ExceptWith(nums1)`：移除所有也出現在 `nums1` 的值。

完成後，兩個集合分別只保留該側獨有的相異整數。所有集合操作都發生在新建的
`HashSet<int>`，因此兩個輸入陣列保持不變。

公開 API：

```csharp
public static IList<IList<int>> FindDifference(int[] nums1, int[] nums2)
```

方法只回傳答案，不輸出主控台。結果固定有兩個列表，但列表內的列舉順序不屬於題目契約；
acceptance harness 會先排序再比較，不會為了測試而改變公開 API 的語意。

以 `nums1=[1,2,3]`、`nums2=[2,4,6]` 為例：

```plaintext
firstOnly  初始為 {1, 2, 3}
secondOnly 初始為 {2, 4, 6}

firstOnly  移除 nums2 中出現的值後為 {1, 3}
secondOnly 移除 nums1 中出現的值後為 {4, 6}

答案為 [[1, 3], [4, 6]]
```

令 `n = nums1.Length`、`m = nums2.Length`：

- 平均時間複雜度：`O(n + m)`。
- 輔助空間複雜度：`O(n + m)`，用於兩個 HashSet。
- 結果空間複雜度：`O(n + m)`，最壞情況下兩側完全不重疊。

HashSet 操作的單次平均成本為 `O(1)`；複雜度以 .NET `HashSet<T>` 的一般平均情況計算。

## 解法二：固定值域位元狀態表

題目保證每個值都位於 `[-1000, 1000]`，因此 `FindDifference2` 配置一個固定的
`byte[2001]`，並用 `value + 1000` 對應陣列索引。每格使用兩個 bit：

- bit 1（狀態值 `1`）：該值出現在 `nums1`。
- bit 2（狀態值 `2`）：該值出現在 `nums2`。
- 狀態值 `3`：兩側都出現，不屬於任一側差集。

重複值只會重設同一個 bit，因此不需要額外去重。最後由索引 0 掃描到 2000：狀態 `1` 加入
`answer[0]`，狀態 `2` 加入 `answer[1]`，狀態 `0` 與 `3` 忽略。

公開 API：

```csharp
public static IList<IList<int>> FindDifference2(int[] nums1, int[] nums2)
```

這個方法同樣不修改輸入、不輸出主控台。依索引掃描使目前結果自然為升冪，但排序並非公開 API
契約；呼叫端仍應把答案視為任意順序。

以 `nums1=[1,2,3]`、`nums2=[2,4,6]` 為例：

```plaintext
值 1 的狀態為 1：只在 nums1
值 2 的狀態為 3：兩側皆有，忽略
值 3 的狀態為 1：只在 nums1
值 4 的狀態為 2：只在 nums2
值 6 的狀態為 2：只在 nums2

掃描後答案為 [[1, 3], [4, 6]]
```

令 `n = nums1.Length`、`m = nums2.Length`：

- 時間複雜度：`O(n + m + 2001)`，包含兩個輸入與整張固定表的掃描。
- 輔助空間複雜度：`O(2001)`，固定配置一張狀態表。
- 結果空間複雜度：`O(n + m)`，最壞情況下兩側完全不重疊。

## 比較與取捨

| 面向 | 解法一：`FindDifference` | 解法二：`FindDifference2` |
| --- | --- | --- |
| 核心結構 | 兩個 `HashSet<int>` | 一個 `byte[2001]` 位元狀態表 |
| 時間 | 平均 `O(n + m)` | `O(n + m + 2001)` |
| 輔助空間 | `O(n + m)` | `O(2001)` |
| 目前輸出順序 | HashSet 列舉順序，不保證 | 掃描值域，自然升冪 |
| 適用條件 | 不依賴狹窄固定值域 | 必須依賴題目的 `[-1000, 1000]` |

解法二避開雜湊、節點與多個集合配置，常數成本可能較低；但它每次都要掃描完整 2,001 格，
且只適用於已知的小型固定值域。這是實作取捨，不保證任一次 LeetCode Runtime 必然優於解法一。

## 舊版解法整理

舊版 `FindDifference` 的 HashSet 方向正確，但會在公開解法內直接輸出每個結果值，破壞純函式與
可重複驗證契約；它也先建立交集再分別扣除，所需的第三個集合不是必要的。

舊版 `FindDifference2` 明確標示未完成，會把整數直接串接成字串後仍回傳空結果。這種做法無法可靠
區分負數、多位數與元素邊界，例如 `[1,23]` 與 `[12,3]` 都會形成相同字串。現在同名 API 已由
全新且正確的固定值域位元狀態表取代，不沿用任何錯誤的字串拼接邏輯。

## LeetCode 提交快照

| 解法 | Runtime | Beats | 說明 |
| --- | ---: | ---: | --- |
| 解法一 `FindDifference` | 49 ms | 14.39% | 使用者提供的單次、方法專屬提交快照 |
| 解法二 `FindDifference2` | 尚未提供 | 尚未提供 | 不推測或捏造未提交的成績 |

LeetCode 成績會受執行環境與當次取樣影響；上表只記錄已提供的單次快照，不代表穩定效能，
也不能用解法一的數字推導解法二。

## Acceptance Harness

`Main` 執行 8 個確定性案例。每個 API 都收到獨立的輸入副本，並各自檢查結果外層長度、
兩側無序集合答案，以及兩份輸入是否保持不變，共 10 checks／案例、80 個檢查。結果形狀不正確時，
guarded access 會產生 `FAIL` 而不是索引例外；任一檢查失敗都會把 process exit code 設為 `1`。

| # | 輸入摘要 | 預期 `nums1` only | 預期 `nums2` only | 驗證目的 |
| ---: | --- | --- | --- | --- |
| 1 | `[1,2,3]`／`[2,4,6]` | `[1,3]` | `[4,6]` | 官方範例 |
| 2 | `[1,2,3,3]`／`[1,1,2,2]` | `[3]` | `[]` | 官方重複值範例 |
| 3 | `[-1000]`／`[-1000]` | `[]` | `[]` | 最小且相同的值域邊界 |
| 4 | `[-1000]`／`[1000]` | `[-1000]` | `[1000]` | 最小且互異的值域兩端 |
| 5 | `[-2,-1,0,1]`／`[-1,0,2]` | `[-2,1]` | `[2]` | 負數、零與部分重疊 |
| 6 | `[1,1,2,2]`／`[2,2,3,3]` | `[1]` | `[3]` | 兩側重複值去重 |
| 7 | `[1,23,-4]`／`[12,3,-4]` | `[1,23]` | `[3,12]` | 字串拼接碰撞回歸 |
| 8 | `[-1000..-1]`／`[-500..499]` | `[-1000..-501]` | `[0..499]` | 兩個長度 1000 的上限 spot check |

## 建置與執行

已從 repository 根目錄驗證：

```bash
dotnet build leetcode_2215/leetcode_2215/leetcode_2215.csproj --nologo
dotnet run --no-build --project leetcode_2215/leetcode_2215/leetcode_2215.csproj
```

若直接開啟題目根目錄 `leetcode_2215/`，使用：

```bash
dotnet build leetcode_2215/leetcode_2215.csproj --nologo
dotnet run --no-build --project leetcode_2215/leetcode_2215.csproj
```

以下是 fresh run 的完整輸出：

```text
Case: 1 - Official example 1
Input: nums1=[1, 2, 3], nums2=[2, 4, 6]
PASS FindDifference result list count | Expected: 2 | Actual: 2
PASS FindDifference nums1-only values | Expected: True | Actual: True
PASS FindDifference nums2-only values | Expected: True | Actual: True
PASS FindDifference nums1 input preserved | Expected: True | Actual: True
PASS FindDifference nums2 input preserved | Expected: True | Actual: True
PASS FindDifference2 result list count | Expected: 2 | Actual: 2
PASS FindDifference2 nums1-only values | Expected: True | Actual: True
PASS FindDifference2 nums2-only values | Expected: True | Actual: True
PASS FindDifference2 nums1 input preserved | Expected: True | Actual: True
PASS FindDifference2 nums2 input preserved | Expected: True | Actual: True

Case: 2 - Official duplicate example
Input: nums1=[1, 2, 3, 3], nums2=[1, 1, 2, 2]
PASS FindDifference result list count | Expected: 2 | Actual: 2
PASS FindDifference nums1-only values | Expected: True | Actual: True
PASS FindDifference nums2-only values | Expected: True | Actual: True
PASS FindDifference nums1 input preserved | Expected: True | Actual: True
PASS FindDifference nums2 input preserved | Expected: True | Actual: True
PASS FindDifference2 result list count | Expected: 2 | Actual: 2
PASS FindDifference2 nums1-only values | Expected: True | Actual: True
PASS FindDifference2 nums2-only values | Expected: True | Actual: True
PASS FindDifference2 nums1 input preserved | Expected: True | Actual: True
PASS FindDifference2 nums2 input preserved | Expected: True | Actual: True

Case: 3 - Minimum equal boundary values
Input: nums1=[-1000], nums2=[-1000]
PASS FindDifference result list count | Expected: 2 | Actual: 2
PASS FindDifference nums1-only values | Expected: True | Actual: True
PASS FindDifference nums2-only values | Expected: True | Actual: True
PASS FindDifference nums1 input preserved | Expected: True | Actual: True
PASS FindDifference nums2 input preserved | Expected: True | Actual: True
PASS FindDifference2 result list count | Expected: 2 | Actual: 2
PASS FindDifference2 nums1-only values | Expected: True | Actual: True
PASS FindDifference2 nums2-only values | Expected: True | Actual: True
PASS FindDifference2 nums1 input preserved | Expected: True | Actual: True
PASS FindDifference2 nums2 input preserved | Expected: True | Actual: True

Case: 4 - Minimum disjoint boundary values
Input: nums1=[-1000], nums2=[1000]
PASS FindDifference result list count | Expected: 2 | Actual: 2
PASS FindDifference nums1-only values | Expected: True | Actual: True
PASS FindDifference nums2-only values | Expected: True | Actual: True
PASS FindDifference nums1 input preserved | Expected: True | Actual: True
PASS FindDifference nums2 input preserved | Expected: True | Actual: True
PASS FindDifference2 result list count | Expected: 2 | Actual: 2
PASS FindDifference2 nums1-only values | Expected: True | Actual: True
PASS FindDifference2 nums2-only values | Expected: True | Actual: True
PASS FindDifference2 nums1 input preserved | Expected: True | Actual: True
PASS FindDifference2 nums2 input preserved | Expected: True | Actual: True

Case: 5 - Negative values and zero
Input: nums1=[-2, -1, 0, 1], nums2=[-1, 0, 2]
PASS FindDifference result list count | Expected: 2 | Actual: 2
PASS FindDifference nums1-only values | Expected: True | Actual: True
PASS FindDifference nums2-only values | Expected: True | Actual: True
PASS FindDifference nums1 input preserved | Expected: True | Actual: True
PASS FindDifference nums2 input preserved | Expected: True | Actual: True
PASS FindDifference2 result list count | Expected: 2 | Actual: 2
PASS FindDifference2 nums1-only values | Expected: True | Actual: True
PASS FindDifference2 nums2-only values | Expected: True | Actual: True
PASS FindDifference2 nums1 input preserved | Expected: True | Actual: True
PASS FindDifference2 nums2 input preserved | Expected: True | Actual: True

Case: 6 - Duplicates on both sides
Input: nums1=[1, 1, 2, 2], nums2=[2, 2, 3, 3]
PASS FindDifference result list count | Expected: 2 | Actual: 2
PASS FindDifference nums1-only values | Expected: True | Actual: True
PASS FindDifference nums2-only values | Expected: True | Actual: True
PASS FindDifference nums1 input preserved | Expected: True | Actual: True
PASS FindDifference nums2 input preserved | Expected: True | Actual: True
PASS FindDifference2 result list count | Expected: 2 | Actual: 2
PASS FindDifference2 nums1-only values | Expected: True | Actual: True
PASS FindDifference2 nums2-only values | Expected: True | Actual: True
PASS FindDifference2 nums1 input preserved | Expected: True | Actual: True
PASS FindDifference2 nums2 input preserved | Expected: True | Actual: True

Case: 7 - String-concatenation collision regression
Input: nums1=[1, 23, -4], nums2=[12, 3, -4]
PASS FindDifference result list count | Expected: 2 | Actual: 2
PASS FindDifference nums1-only values | Expected: True | Actual: True
PASS FindDifference nums2-only values | Expected: True | Actual: True
PASS FindDifference nums1 input preserved | Expected: True | Actual: True
PASS FindDifference nums2 input preserved | Expected: True | Actual: True
PASS FindDifference2 result list count | Expected: 2 | Actual: 2
PASS FindDifference2 nums1-only values | Expected: True | Actual: True
PASS FindDifference2 nums2-only values | Expected: True | Actual: True
PASS FindDifference2 nums1 input preserved | Expected: True | Actual: True
PASS FindDifference2 nums2 input preserved | Expected: True | Actual: True

Case: 8 - Maximum input lengths
Input: nums1=[-1000..-1] (1000 values), nums2=[-500..499] (1000 values)
PASS FindDifference result list count | Expected: 2 | Actual: 2
PASS FindDifference nums1-only values | Expected: True | Actual: True
PASS FindDifference nums2-only values | Expected: True | Actual: True
PASS FindDifference nums1 input preserved | Expected: True | Actual: True
PASS FindDifference nums2 input preserved | Expected: True | Actual: True
PASS FindDifference2 result list count | Expected: 2 | Actual: 2
PASS FindDifference2 nums1-only values | Expected: True | Actual: True
PASS FindDifference2 nums2-only values | Expected: True | Actual: True
PASS FindDifference2 nums1 input preserved | Expected: True | Actual: True
PASS FindDifference2 nums2 input preserved | Expected: True | Actual: True

Summary: 80/80 checks passed.
```

## 專案結構

```plaintext
leetcode_2215/
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
└── leetcode_2215/
    ├── Program.cs
    └── leetcode_2215.csproj
```
