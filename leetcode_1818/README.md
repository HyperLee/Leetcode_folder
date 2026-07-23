# 1818. Minimum Absolute Sum Difference／最小絕對差值和

給定等長正整數陣列 `nums1` 與 `nums2`，可以把 `nums1` 最多一個元素替換為
`nums1` 中已存在的值，求最小可能的逐項絕對差值和，並對 `1_000_000_007` 取模。
本專案保留「排序副本加上前驅／後繼 lower bound」的單一純函式解法。

- [LeetCode English](https://leetcode.com/problems/minimum-absolute-sum-difference/)
- [LeetCode 中文](https://leetcode.cn/problems/minimum-absolute-sum-difference/)

## 題目說明與限制條件

對每個索引 `i`，原始成本為 `abs(nums1[i] - nums2[i])`。一次替換只能選用
`nums1` 原先已有的數值，且最多執行一次；所以應找出一個位置，讓它的成本減少量最大。

- `1 <= nums1.length, nums2.length <= 100000`
- `nums1.length == nums2.length`
- `1 <= nums1[i], nums2[i] <= 100000`
- `MinAbsoluteSumDiff` 只處理題目保證的有效輸入，不加入額外的無效輸入行為

## 解法：排序副本與 lower bound

複製 `nums1` 後排序，因而不會改動呼叫端輸入。對每個目標 `nums2[i]`，以
`BinarySearch` 找到排序副本中第一個 `>= nums2[i]` 的索引。最接近目標的候選只可能是：

- 該索引的後繼值（若存在）；
- 前一個索引的前驅值（若存在）。

核心不變量：對每個位置，`maximumReduction` 一直是已處理位置中可以由**一次**替換
省下的最大成本；因此最終答案就是全部原始成本減去 `maximumReduction`。`BinarySearch`
維持答案在半開區間 `[left, right)` 中，結束時 `left` 是第一個不小於目標的索引，或
陣列長度。

容易出錯之處：

- 只檢查後繼或只檢查前驅，會錯過另一側更近的值。
- lower bound 等於 `0` 或等於長度時，不能讀取不存在的前驅或後繼。
- 先排序 `nums1` 本身會破壞題目輸入；必須排序副本。
- 絕對差值總和最高可超過 `int`，累加時必須使用 `long`，最後再取模。
- 替換最多一次，不可把每個索引各自套用最佳候選後全部相加。

## 逐步走查

以 `nums1 = [1, 4, 5]`、`nums2 = [1, 8, 5]` 為例，排序副本仍為 `[1, 4, 5]`。
索引 1 的原始成本是 `abs(4 - 8) = 4`；lower bound 回傳長度 3，因此只有前驅 `5` 可用，
替換後成本為 `abs(5 - 8) = 3`，可省 1。原始總和為 4，扣除最大節省量後得到 3。

## 複雜度

| 方法 | 時間 | 結果空間 | 輔助空間 |
| --- | --- | --- | --- |
| `MinAbsoluteSumDiff`（排序副本 + lower bound） | `O(n log n)` | `O(1)` | `O(n)` |

排序副本需 `O(n log n)`；每個位置的二分搜尋是 `O(log n)`，總計仍是 `O(n log n)`。
副本佔用 `O(n)` 輔助空間，解法不會修改 `nums1` 或 `nums2`。

## Acceptance Harness

`Main` 執行九個確定性案例，每案各驗證答案與兩個輸入陣列皆未改變，合計 18 項檢查。
每項都會印出 Case、Input、Expected、Actual 與 PASS/FAIL；任何失敗都會將 process
exit code 設為 1。

| # | 案例 | 預期答案 | 驗證目的 |
| ---: | --- | ---: | --- |
| 1 | Official example 1 | 3 | 官方範例 |
| 2 | Official example 2 | 0 | 官方範例、零成本 |
| 3 | Official example 3 | 20 | 官方範例 |
| 4 | Minimum valid input | 99999 | 最小長度與值域差距 |
| 5 | Predecessor is better | 3 | lower bound 右側越界時的前驅 |
| 6 | Successor is better | 4 | 後繼比前驅更接近 |
| 7 | Duplicate values | 2 | 重複排序值 |
| 8 | Value boundary | 99999 | 值域兩端 |
| 9 | n = 100000 modulo spot check | 999699939 | 上限與模數；巨大輸入只描述、不完整列印 |

## 建置與執行

已從 repository 根目錄驗證：

```bash
dotnet build leetcode_1818/leetcode_1818/leetcode_1818.csproj --nologo
dotnet run --no-build --project leetcode_1818/leetcode_1818/leetcode_1818.csproj
```

若直接開啟題目根目錄 `leetcode_1818/`，使用：

```bash
dotnet build leetcode_1818/leetcode_1818.csproj --nologo
dotnet run --no-build --project leetcode_1818/leetcode_1818.csproj
```

以下是 fresh run 的完整輸出：

```text
Case: Official example 1 - Answer
Input: nums1=[1, 7, 5], nums2=[2, 3, 5]
Expected: 3
Actual: 3
Result: PASS

Case: Official example 1 - Input arrays preserved
Input: nums1=[1, 7, 5], nums2=[2, 3, 5]
Expected: nums1 and nums2 unchanged
Actual: nums1 and nums2 unchanged
Result: PASS

Case: Official example 2 - Answer
Input: nums1=[2, 4, 6, 8, 10], nums2=[2, 4, 6, 8, 10]
Expected: 0
Actual: 0
Result: PASS

Case: Official example 2 - Input arrays preserved
Input: nums1=[2, 4, 6, 8, 10], nums2=[2, 4, 6, 8, 10]
Expected: nums1 and nums2 unchanged
Actual: nums1 and nums2 unchanged
Result: PASS

Case: Official example 3 - Answer
Input: nums1=[1, 10, 4, 4, 2, 7], nums2=[9, 3, 5, 1, 7, 4]
Expected: 20
Actual: 20
Result: PASS

Case: Official example 3 - Input arrays preserved
Input: nums1=[1, 10, 4, 4, 2, 7], nums2=[9, 3, 5, 1, 7, 4]
Expected: nums1 and nums2 unchanged
Actual: nums1 and nums2 unchanged
Result: PASS

Case: Minimum valid input - Answer
Input: nums1=[1], nums2=[100000]
Expected: 99999
Actual: 99999
Result: PASS

Case: Minimum valid input - Input arrays preserved
Input: nums1=[1], nums2=[100000]
Expected: nums1 and nums2 unchanged
Actual: nums1 and nums2 unchanged
Result: PASS

Case: Predecessor is better - Answer
Input: nums1=[1, 4, 5], nums2=[1, 8, 5]
Expected: 3
Actual: 3
Result: PASS

Case: Predecessor is better - Input arrays preserved
Input: nums1=[1, 4, 5], nums2=[1, 8, 5]
Expected: nums1 and nums2 unchanged
Actual: nums1 and nums2 unchanged
Result: PASS

Case: Successor is better - Answer
Input: nums1=[1, 4, 10], nums2=[8, 4, 8]
Expected: 4
Actual: 4
Result: PASS

Case: Successor is better - Input arrays preserved
Input: nums1=[1, 4, 10], nums2=[8, 4, 8]
Expected: nums1 and nums2 unchanged
Actual: nums1 and nums2 unchanged
Result: PASS

Case: Duplicate values - Answer
Input: nums1=[1, 1, 5], nums2=[3, 1, 5]
Expected: 2
Actual: 2
Result: PASS

Case: Duplicate values - Input arrays preserved
Input: nums1=[1, 1, 5], nums2=[3, 1, 5]
Expected: nums1 and nums2 unchanged
Actual: nums1 and nums2 unchanged
Result: PASS

Case: Value boundary - Answer
Input: nums1=[1, 100000], nums2=[100000, 1]
Expected: 99999
Actual: 99999
Result: PASS

Case: Value boundary - Input arrays preserved
Input: nums1=[1, 100000], nums2=[100000, 1]
Expected: nums1 and nums2 unchanged
Actual: nums1 and nums2 unchanged
Result: PASS

Case: n = 100000 modulo spot check - Answer
Input: n=100000; nums1=100000 copies of 1; nums2=99999 copies of 99998 followed by 100000
Expected: 999699939
Actual: 999699939
Result: PASS

Case: n = 100000 modulo spot check - Input arrays preserved
Input: n=100000; nums1=100000 copies of 1; nums2=99999 copies of 99998 followed by 100000
Expected: nums1 and nums2 unchanged
Actual: nums1 and nums2 unchanged
Result: PASS

Summary: 18/18 checks passed.
```

## 專案結構

```plaintext
leetcode_1818/
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
└── leetcode_1818/
    ├── Program.cs
    └── leetcode_1818.csproj
```
