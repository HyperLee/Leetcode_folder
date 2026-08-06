# LeetCode 2461：長度為 K 子陣列中的最大和

本專案使用 .NET 10 console application 示範 LeetCode 2461「Maximum Sum of Distinct Subarrays With Length K」。程式保留原本的頻率表滑動視窗解法，並加入一個使用最後出現位置與前綴和的比較解法；`Main` 會以固定案例執行兩種 API 並自動驗證結果。

## 題目說明

給定整數陣列 `nums` 與整數 `k`，請從所有長度恰好為 `k` 的連續子陣列中，找出「每個元素都不重複」的子陣列，並回傳其中最大的元素總和。

如果沒有任何長度為 `k` 且元素互不相同的子陣列，回傳 `0`。子陣列是陣列中連續且非空的一段元素序列。

題目原文與限制條件：[LeetCode 2461](https://leetcode.com/problems/maximum-sum-of-distinct-subarrays-with-length-k/description/)

### 限制條件

- `1 <= k <= nums.Length <= 10^5`
- `1 <= nums[i] <= 10^5`
- 輸入符合上述條件，因此兩種 API 不另外處理 `null`、空陣列或非法的 `k`。
- 最大總和可能超過 32 位元整數範圍，實作使用 `long` 保存窗口總和與答案。

## 解題概念與出發點

直接枚舉每個長度為 `k` 的子陣列，再逐一檢查重複值，最差會重複掃描大量元素。觀察相鄰的兩個固定長度窗口：右移一格時，只會有一個元素離開左側，並有一個元素從右側加入。因此可以把前一個窗口的狀態保留下來，只更新這兩個變化。

每個候選窗口需要同時維護兩件事：

1. 窗口內元素總和，避免每次重新加總。
2. 元素是否重複，判斷目前窗口是否符合「全部相異」。

本專案使用兩種不同的狀態設計：

- 解法一固定窗口的左右邊界，透過頻率表知道每個值出現幾次。
- 解法二以每個值的最後出現位置推進左界，再用前綴和取得任意窗口總和。

兩種解法都不修改 `nums`，因此可以直接重複使用同一組輸入；`Main` 仍為每個 API 傳入獨立副本，讓測試 fixture 不會因未來的實作變更而互相污染。

## 解法一：固定窗口 + 頻率 Dictionary

API：`MaximumSubarraySum(int[] nums, int k)`

### 維護的狀態

- `windowSum`：目前長度為 `k` 的窗口總和。
- `counts`：`Dictionary<int, int>`，記錄每個元素在窗口中的出現次數。
- `maxSum`：目前看過的合法窗口最大總和。

### 設計流程

1. 先把 `nums[0..k-1]` 放入第一個窗口，同時累加 `windowSum` 與 `counts`。
2. 因為窗口長度已經是 `k`，所以 `counts.Count == k` 就代表窗口內沒有重複值，可以用 `windowSum` 初始化答案。
3. 從索引 `k` 開始逐格右移窗口：
   - 從總和扣除離開左側的 `outgoing`，再加入右側的 `incoming`。
   - 將 `outgoing` 的頻率減一；頻率變成零時，從 Dictionary 移除該鍵。
   - 將 `incoming` 的頻率加一或建立新鍵。
4. 如果更新後 `counts.Count == k`，就以目前總和更新 `maxSum`。

### 正確性關鍵

固定窗口永遠包含 `k` 個元素。若不同元素的數量也正好是 `k`，代表這 `k` 個元素彼此皆不同；若數量小於 `k`，至少有一個元素重複，因此不能更新答案。

### 複雜度

- 時間：O(n)，每個元素在加入與移除時各處理一次。
- 額外空間：O(k)，頻率表最多保存目前窗口中的不同值。

## 解法二：最後出現位置 + 前綴和

API：`MaximumSubarraySum2(int[] nums, int k)`

### 維護的狀態

- `lastSeenIndex`：記錄每個值最後一次出現的索引。
- `left`：目前合法窗口的左界。
- `prefixSums[i]`：`nums[0..i-1]` 的總和，因此窗口 `[left, right]` 的總和為 `prefixSums[right + 1] - prefixSums[left]`。
- `maxSum`：目前看過的合法窗口最大總和。

### 設計流程

1. 由左到右掃描 `right`，先建立目前元素的前綴和。
2. 如果 `nums[right]` 之前出現過，而且上次出現位置仍在目前窗口內，就將 `left` 推進到 `previousIndex + 1`，直接跳過重複值。
3. 更新 `nums[right]` 的最後出現位置。
4. 若窗口長度超過 `k`，將 `left` 推進到 `right - k + 1`，維持固定長度。
5. 窗口長度恰好為 `k` 時，用前綴和在 O(1) 時間取得總和並更新答案。

### 正確性關鍵

`left` 只會向右移動。每當重複值進入窗口，左界跨過該值先前的位置，因此窗口內不會保留兩個相同值；當窗口太長時再移除最左側元素，仍能維持「長度不超過 `k`」的條件。

### 複雜度

- 時間：O(n)，每個索引只會被掃描一次，左界也只向右移動。
- 額外空間：O(n)，需要保存長度 `n + 1` 的前綴和與最後出現位置表。

## 兩種解法比較

| 比較項目 | `MaximumSubarraySum` | `MaximumSubarraySum2` |
| --- | --- | --- |
| 重複判斷 | 窗口內頻率 Dictionary | 最後出現位置 Dictionary |
| 窗口邊界 | 固定以 `i - k` 與 `i` 右移 | 由重複位置與固定長度共同推進 `left` |
| 總和計算 | 移除左值、加入右值 | 前綴和區間相減 |
| 時間複雜度 | O(n) | O(n) |
| 額外空間 | O(k) | O(n) |
| 輸入是否修改 | 否 | 否 |

## 官方案例演示

輸入：`nums = [1, 5, 4, 2, 9, 9, 9]`、`k = 3`

### 解法一的窗口移動

| 窗口 | 總和 | 是否全部相異 | 答案更新 |
| --- | ---: | --- | ---: |
| `[1, 5, 4]` | 10 | 是 | `maxSum = 10` |
| `[5, 4, 2]` | 11 | 是 | `maxSum = 11` |
| `[4, 2, 9]` | 15 | 是 | `maxSum = 15` |
| `[2, 9, 9]` | 20 | 否，9 重複 | 不更新 |
| `[9, 9, 9]` | 27 | 否，9 重複 | 不更新 |

第一個窗口先建立頻率表 `{1:1, 5:1, 4:1}`。每次右移都只處理一個離開值與一個加入值；當 `counts.Count == k` 時，窗口中的三個元素才全部相異，因此最後答案為 `15`。

### 解法二的左界與前綴和

此案例的前綴和為 `[0, 1, 6, 10, 12, 21, 30, 39]`。

| `right` | 新加入值 | 窗口調整原因 | `left` | 合法窗口總和 |
| ---: | ---: | --- | ---: | ---: |
| 0 | 1 | 窗口尚未達到 k | 0 | — |
| 1 | 5 | 窗口尚未達到 k | 0 | — |
| 2 | 4 | 長度為 3 且無重複 | 0 | 10 |
| 3 | 2 | 無重複但長度變成 4，左界右移 | 1 | 11 |
| 4 | 9 | 無重複但長度變成 4，左界右移 | 2 | 15 |
| 5 | 9 | 上次 9 在索引 4，跨過重複值 | 5 | — |
| 6 | 9 | 上次 9 在索引 5，跨過重複值 | 6 | — |

例如 `right = 4` 時，窗口是 `[4, 2, 9]`，總和由 `prefixSums[5] - prefixSums[2]` 得到 `21 - 6 = 15`。遇到索引 5 的第二個 `9` 時，`left` 直接跳到 5，避免逐項搜尋重複值。

## Main 可執行驗證案例

`Main` 會對每個案例分別執行兩個 API，列出 `Expected`、`Actual` 與 `PASS/FAIL`。目前共 7 個案例、14 個解法驗證項目：

| 案例 | `n` | `k` | 預期結果 | 覆蓋重點 |
| --- | ---: | ---: | ---: | --- |
| 官方案例 | 7 | 3 | 15 | 一般滑動窗口與重複值 |
| 全部重複 | 3 | 3 | 0 | 沒有合法窗口 |
| 重複後仍有合法窗口 | 5 | 3 | 8 | 左界需要跨過重複值 |
| k 等於 1 | 3 | 1 | 5 | 最小窗口 |
| 整個陣列皆不重複 | 4 | 4 | 10 | `k == nums.Length` |
| 交錯重複 | 5 | 2 | 5 | 多個重複窗口交錯出現 |
| 長整數總和 | 100000 | 100000 | 5000050000 | `long` 總和與最大限制 |

案例失敗時 `Main` 回傳非零結束碼，方便從 shell 或 CI 判斷執行失敗；程式不使用互動式按鍵等待。

## 建置與執行

請在本專案根目錄 `/Users/qiuzili/Leetcode/Leetcode_folder/leetcode_2461` 執行：

```bash
dotnet restore leetcode_2461/leetcode_2461.csproj
dotnet build leetcode_2461/leetcode_2461.csproj --nologo
dotnet run --no-build --project leetcode_2461/leetcode_2461.csproj
```

本專案目前沒有自動化測試專案，因此以 build 加上 `Main` 的 deterministic harness 作為驗證方式。完成修改後可使用以下命令檢查差異中的多餘空白：

```bash
git diff --check
```

## 實際執行結果

以下內容由最近一次 `dotnet run --no-build --project leetcode_2461/leetcode_2461.csproj` 直接產生：

```text
案例：官方案例（n = 7, k = 3）
  Expected: 15
  MaximumSubarraySum Actual: 15 -> PASS
  MaximumSubarraySum2 Actual: 15 -> PASS
  案例結果: PASS

案例：全部重複（n = 3, k = 3）
  Expected: 0
  MaximumSubarraySum Actual: 0 -> PASS
  MaximumSubarraySum2 Actual: 0 -> PASS
  案例結果: PASS

案例：重複後仍有合法窗口（n = 5, k = 3）
  Expected: 8
  MaximumSubarraySum Actual: 8 -> PASS
  MaximumSubarraySum2 Actual: 8 -> PASS
  案例結果: PASS

案例：k 等於 1（n = 3, k = 1）
  Expected: 5
  MaximumSubarraySum Actual: 5 -> PASS
  MaximumSubarraySum2 Actual: 5 -> PASS
  案例結果: PASS

案例：整個陣列皆不重複（n = 4, k = 4）
  Expected: 10
  MaximumSubarraySum Actual: 10 -> PASS
  MaximumSubarraySum2 Actual: 10 -> PASS
  案例結果: PASS

案例：交錯重複（n = 5, k = 2）
  Expected: 5
  MaximumSubarraySum Actual: 5 -> PASS
  MaximumSubarraySum2 Actual: 5 -> PASS
  案例結果: PASS

案例：長整數總和（n = 100000, k = 100000）
  Expected: 5000050000
  MaximumSubarraySum Actual: 5000050000 -> PASS
  MaximumSubarraySum2 Actual: 5000050000 -> PASS
  案例結果: PASS

總結：14/14 項驗證通過
```

## 專案結構

```text
leetcode_2461/
├── leetcode_2461/
│   ├── Program.cs
│   └── leetcode_2461.csproj
├── docs/
│   └── readme-template.md
└── README.md
```
