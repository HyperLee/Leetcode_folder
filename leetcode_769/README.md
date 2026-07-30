# LeetCode 769：Max Chunks To Make Sorted／最多能完成排序的區塊

這是一個以 C# 撰寫的 .NET 10 主控台專案，保留兩種線性掃描解法，分別從
「前綴是否已包含正確值域」與「目前區塊的值域是否等於索引範圍」判斷合法切點。

- [英文題目：769. Max Chunks To Make Sorted](https://leetcode.com/problems/max-chunks-to-make-sorted/)
- [中文題目：769. 最多能完成排序的塊](https://leetcode.cn/problems/max-chunks-to-make-sorted/)

## 題目說明

給定長度為 `n` 的整數陣列 `arr`，其中包含 `0` 到 `n - 1` 的每個整數且各出現一次。
目標是把陣列切成數個連續區塊，將每個區塊個別升冪排序，再依原順序串接。串接結果必須
等於整個陣列升冪排序後的結果，也就是 `[0, 1, ..., n - 1]`。

請回傳最多能切出的區塊數量。

## 限制條件

- `n == arr.Length`
- `1 <= n <= 10`
- `0 <= arr[i] < n`
- `arr` 中每個元素都不同，因此它是 `[0, n - 1]` 的排列
- 兩個公開方法只處理題目契約內的有效輸入，不另外定義無效輸入行為
- `MaxChunksToSorted` 與 `MaxChunksToSorted2` 都不修改輸入，也不寫入主控台

## 解題概念與出發點

因為 `arr` 是 `[0, n - 1]` 的排列，完整排序後索引 `i` 的值必定是 `i`。若一個前綴
`[0, i]` 能獨立排序並留在原位置，它就必須恰好包含 `0` 到 `i`；同理，從左邊界 `j`
開始的候選區塊 `[j, i]` 必須恰好包含 `j` 到 `i`。

這個「索引範圍等於值域」的不變量提供兩種判定方式：

1. 只追蹤整個前綴的最大值，判斷前綴 `[0, i]` 是否完整。
2. 追蹤目前候選區塊的最小值與最大值，直接判斷 `[j, i]` 是否完整。

## 解法比較

| 解法 | 切點條件 | 時間複雜度 | 輔助空間 | 結果空間 | 修改輸入 |
| --- | --- | --- | --- | --- | --- |
| `MaxChunksToSorted` | `prefixMax == i` | `O(n)` | `O(1)` | `O(1)` | 否 |
| `MaxChunksToSorted2` | `min == left && max == right` | `O(n)` | `O(1)` | `O(1)` | 否 |

兩個方法都只掃描一次陣列。第一種寫法較精簡；第二種顯式保存每個候選區塊的值域，
較容易直接觀察區塊為何能獨立排序。

## 解法一：前綴最大值

### 設計說明

`MaxChunksToSorted` 從左至右維護 `m`，也就是目前前綴 `[0, i]` 的最大值。

當 `m == i` 時：

- 前綴共有 `i + 1` 個互不相同的值。
- 所有值都屬於完整排列 `[0, n - 1]`。
- 前綴最大值沒有超過 `i`，因此前綴內每個值都落在 `[0, i]`。
- `[0, i]` 也正好只有 `i + 1` 種可能值，所以它必定完整包含 `0` 到 `i`。

因此此前綴排序後一定會成為 `[0, 1, ..., i]`，可以在 `i` 後方建立切點。反之，若
`m > i`，代表前綴包含一個應該出現在右側的值，目前還不能切分。

### `[1, 0, 2, 3, 4]` 範例演示

| 索引 `i` | `arr[i]` | 更新後 `m` | `m == i` | 累計區塊 | 已確認區塊 |
| ---: | ---: | ---: | --- | ---: | --- |
| 0 | 1 | 1 | 否 | 0 | — |
| 1 | 0 | 1 | 是 | 1 | `[1, 0]` |
| 2 | 2 | 2 | 是 | 2 | `[2]` |
| 3 | 3 | 3 | 是 | 3 | `[3]` |
| 4 | 4 | 4 | 是 | 4 | `[4]` |

個別排序後得到 `[0, 1]`、`[2]`、`[3]`、`[4]`，串接結果為
`[0, 1, 2, 3, 4]`，所以答案是 `4`。

## 解法二：目前區塊的最小值與最大值

### 設計說明

`MaxChunksToSorted2` 使用 `j` 表示目前候選區塊的左邊界，`i` 表示右邊界，並維護：

- `min`：`arr[j..i]` 中的最小值。
- `max`：`arr[j..i]` 中的最大值。

若 `min == j` 且 `max == i`，候選區塊內共有 `i - j + 1` 個互不相同的整數，所有值
又都落在同樣具有 `i - j + 1` 個可能值的範圍 `[j, i]`，因此該區塊必定恰好包含
`j` 到 `i`。排序後每個值都會落回相同索引範圍，可以確認一個區塊。

確認後把 `j` 移到 `i + 1`，並將 `min`、`max` 還原為哨兵值 `n` 與 `-1`，從下一個
索引重新收集區塊值域。哨兵能保證新區塊讀入第一個元素時，最小值與最大值都會正確更新。

### `[1, 0, 2, 3, 4]` 範例演示

此例 `n = 5`，初始狀態為 `j = 0`、`min = 5`、`max = -1`。

| `i` | `arr[i]` | `j` | 更新後 `min` | 更新後 `max` | 符合邊界 | 動作 |
| ---: | ---: | ---: | ---: | ---: | --- | --- |
| 0 | 1 | 0 | 1 | 1 | 否 | 繼續擴張 |
| 1 | 0 | 0 | 0 | 1 | 是 | 確認 `[0, 1]`，令 `j = 2` |
| 2 | 2 | 2 | 2 | 2 | 是 | 確認 `[2, 2]`，令 `j = 3` |
| 3 | 3 | 3 | 3 | 3 | 是 | 確認 `[3, 3]`，令 `j = 4` |
| 4 | 4 | 4 | 4 | 4 | 是 | 確認 `[4, 4]`，令 `j = 5` |

共確認四個值域與索引範圍一致的區塊，因此答案同樣是 `4`。

## 可執行驗證案例

專案沒有獨立的 test project；`Main` 是可重複執行的 console acceptance harness。
每組案例會為兩個公開方法建立獨立輸入副本，並分別驗證回傳結果與輸入未被修改，因此
7 組案例共有 28 項檢查。任一項失敗時，process exit code 會設為 `1`。

| 案例 | 輸入 | 預期 | 驗證重點 |
| --- | --- | ---: | --- |
| Official example 1 | `[4, 3, 2, 1, 0]` | 1 | 整體只能形成一塊 |
| Official example 2 | `[1, 0, 2, 3, 4]` | 4 | 雙元素與單元素區塊 |
| Minimum input | `[0]` | 1 | 最小有效長度 |
| Already sorted | `[0, 1, 2, 3, 4]` | 5 | 每個位置都能切分 |
| Multi-element prefix | `[2, 0, 1, 3, 4]` | 3 | 前三項必須合併 |
| Delayed prefix boundary | `[1, 2, 0, 3]` | 2 | 前綴最大值延後形成切點 |
| Maximum-length mixed chunks | `[0, 2, 1, 4, 3, 5, 7, 6, 9, 8]` | 6 | `n = 10` 與多種區塊大小 |

## 建置與執行

請從此 README 所在的 `leetcode_769` 目錄執行：

```bash
dotnet restore leetcode_769/leetcode_769.csproj
dotnet build leetcode_769/leetcode_769.csproj --nologo
dotnet run --no-build --project leetcode_769/leetcode_769.csproj
```

以下是完成建置後執行第三個命令的完整輸出：

```text
LeetCode 769 acceptance harness

Case: Official example 1
Input: [4, 3, 2, 1, 0]
PASS | MaxChunksToSorted result | Expected: 1 | Actual: 1
PASS | MaxChunksToSorted input preserved | Expected: True | Actual: True
PASS | MaxChunksToSorted2 result | Expected: 1 | Actual: 1
PASS | MaxChunksToSorted2 input preserved | Expected: True | Actual: True

Case: Official example 2
Input: [1, 0, 2, 3, 4]
PASS | MaxChunksToSorted result | Expected: 4 | Actual: 4
PASS | MaxChunksToSorted input preserved | Expected: True | Actual: True
PASS | MaxChunksToSorted2 result | Expected: 4 | Actual: 4
PASS | MaxChunksToSorted2 input preserved | Expected: True | Actual: True

Case: Minimum input
Input: [0]
PASS | MaxChunksToSorted result | Expected: 1 | Actual: 1
PASS | MaxChunksToSorted input preserved | Expected: True | Actual: True
PASS | MaxChunksToSorted2 result | Expected: 1 | Actual: 1
PASS | MaxChunksToSorted2 input preserved | Expected: True | Actual: True

Case: Already sorted
Input: [0, 1, 2, 3, 4]
PASS | MaxChunksToSorted result | Expected: 5 | Actual: 5
PASS | MaxChunksToSorted input preserved | Expected: True | Actual: True
PASS | MaxChunksToSorted2 result | Expected: 5 | Actual: 5
PASS | MaxChunksToSorted2 input preserved | Expected: True | Actual: True

Case: Multi-element prefix
Input: [2, 0, 1, 3, 4]
PASS | MaxChunksToSorted result | Expected: 3 | Actual: 3
PASS | MaxChunksToSorted input preserved | Expected: True | Actual: True
PASS | MaxChunksToSorted2 result | Expected: 3 | Actual: 3
PASS | MaxChunksToSorted2 input preserved | Expected: True | Actual: True

Case: Delayed prefix boundary
Input: [1, 2, 0, 3]
PASS | MaxChunksToSorted result | Expected: 2 | Actual: 2
PASS | MaxChunksToSorted input preserved | Expected: True | Actual: True
PASS | MaxChunksToSorted2 result | Expected: 2 | Actual: 2
PASS | MaxChunksToSorted2 input preserved | Expected: True | Actual: True

Case: Maximum-length mixed chunks
Input: [0, 2, 1, 4, 3, 5, 7, 6, 9, 8]
PASS | MaxChunksToSorted result | Expected: 6 | Actual: 6
PASS | MaxChunksToSorted input preserved | Expected: True | Actual: True
PASS | MaxChunksToSorted2 result | Expected: 6 | Actual: 6
PASS | MaxChunksToSorted2 input preserved | Expected: True | Actual: True

Summary: 28/28 checks passed.
```

## 專案結構

```plaintext
.
├── docs/
│   └── readme-template.md
├── leetcode_769/
│   ├── Program.cs
│   └── leetcode_769.csproj
├── AGENTS.md
├── leetcode_769.sln
└── README.md
```
