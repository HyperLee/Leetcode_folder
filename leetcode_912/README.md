# LeetCode 912：Sort an Array／排序陣列

[英文題目](https://leetcode.com/problems/sort-an-array/) ·
[中文題目](https://leetcode.cn/problems/sort-an-array/)

這個 .NET 10 主控台專案實作原地最大堆積排序。公開方法 `SortArray` 只負責修改並
回傳輸入陣列；`Main` 集中 acceptance harness 的所有輸出。

## 題目說明

給定整數陣列 `nums`，在不使用任何內建排序函式的前提下，將陣列依遞增順序排序
並回傳。解法必須達到 `O(n log n)` 時間複雜度，並盡可能減少額外空間。

## 題目限制

- `1 <= nums.length <= 5 * 10^4`。
- `-5 * 10^4 <= nums[i] <= 5 * 10^4`。

實作只處理 LeetCode 的有效輸入契約，不額外加入 null 或空陣列行為。

## 解法：原地最大堆積排序

公開 API 為 `public static int[] SortArray(int[] nums)`。最大堆保證每個父節點都不小於
子節點，所以根節點始終是目前堆範圍的最大值。實作分成兩階段：

1. 從 `nums.Length / 2 - 1`（最後一個非葉節點）向前執行 `SiftDown`，建立最大堆。
2. 把根節點與未排序區尾端交換，縮小堆範圍，再從根節點迭代下沉以恢復最大堆。

第二階段維持兩個不變量：堆範圍仍符合最大堆性質；堆範圍右側則已是遞增排列且
不再參與交換。最後整個陣列即為遞增順序。

### 容易出錯的地方

- 建堆要從最後一個「非葉」節點開始；葉節點本身已是有效堆。
- `heapSize` 是不含上界，交換到尾端的最大值不得再被 `SiftDown` 觸及。
- 左子節點為 `2 * root + 1`，右子節點為左子節點再加一；選擇較大的子節點交換。
- 方法必須直接修改並回傳同一陣列參考，不能以新陣列掩蓋原地排序契約。

### 設計取捨

Merge sort 同樣能保證 `O(n log n)`，但通常需要 `O(n)` 輔助陣列；隨機化 quicksort
不符合本題已核准的確定性與最壞時間要求。最大堆積排序可在原陣列內完成，代價是
不具穩定性；本題只要求數值排序，不要求相同值保留原相對次序。

## 複雜度

令 `n = nums.Length`：

- 時間複雜度：建堆 `O(n)`，取出所有最大值為 `O(n log n)`，總計 `O(n log n)`。
- 結果空間複雜度：`O(1)`，結果保存在原輸入陣列。
- 輔助空間複雜度：`O(1)`，迭代式 `SiftDown` 不使用遞迴堆疊或額外集合。

## 逐步走查

以 `[5,2,3,1]` 為例：

```plaintext
建堆後：[5,2,3,1]
交換根與索引 3：[1,2,3,5]；修復前三項後：[3,2,1,5]
交換根與索引 2：[1,2,3,5]；修復前兩項後：[2,1,3,5]
交換根與索引 1：[1,2,3,5]
```

## 可執行驗證

專案沒有正式測試專案；`Main` 的確定性驗收程式是目前的驗證機制。任一檢查失敗
都會設定 `Environment.ExitCode = 1`。上限案例分別驗證完整排序結果與回傳參考，
因此八個案例共有九項檢查。

| 案例 | 輸入摘要 | 預期 | 驗證重點 |
|---:|---|---|---|
| 1 | `[5,2,3,1]` | `[1,2,3,5]` | 官方範例一 |
| 2 | `[5,1,1,2,0,0]` | `[0,0,1,1,2,5]` | 官方範例二與重複值 |
| 3 | `[42]` | `[42]` | 最小有效長度 |
| 4 | `[-3,-1,0,2,8]` | 不變 | 已排序輸入 |
| 5 | `[9,7,5,3,1]` | `[1,3,5,7,9]` | 反向排序 |
| 6 | 五個 `6` | 不變 | 全部重複 |
| 7 | 重複 `-50000`、`50000` 與 `0` | 遞增排列 | 數值邊界 |
| 8 | 50,000 至 1 | 1 至 50,000，且同一參考 | 長度上限、完整順序與原地契約 |

## 建置與執行

請把外層 `leetcode_912/` 當成工作目錄：

```bash
dotnet build leetcode_912/leetcode_912.csproj --nologo
dotnet run --no-build --project leetcode_912/leetcode_912.csproj
```

## 最新驗證輸出

以下是 fresh run 的完整輸出，也是本 README 唯一的 `text` fence：

```text
LeetCode 912 acceptance harness

Case 1: Official example 1
Input: [5,2,3,1]
Expected: [1,2,3,5]
Actual: [1,2,3,5]
PASS

Case 2: Official example 2
Input: [5,1,1,2,0,0]
Expected: [0,0,1,1,2,5]
Actual: [0,0,1,1,2,5]
PASS

Case 3: Single element
Input: [42]
Expected: [42]
Actual: [42]
PASS

Case 4: Already sorted
Input: [-3,-1,0,2,8]
Expected: [-3,-1,0,2,8]
Actual: [-3,-1,0,2,8]
PASS

Case 5: Reverse sorted
Input: [9,7,5,3,1]
Expected: [1,3,5,7,9]
Actual: [1,3,5,7,9]
PASS

Case 6: All duplicates
Input: [6,6,6,6,6]
Expected: [6,6,6,6,6]
Actual: [6,6,6,6,6]
PASS

Case 7: Repeated boundary values
Input: [50000,-50000,0,50000,-50000]
Expected: [-50000,-50000,0,50000,50000]
Actual: [-50000,-50000,0,50000,50000]
PASS

Case 8: Maximum length
Input: 50,000 integers in descending order [50000,...,1]
Expected: 50,000 integers in ascending order [1,...,50000]
Actual: 50,000 integers [1,2,3,...,49998,49999,50000]
PASS

Case 8: Maximum length reference identity
Input: the original 50,000-element array reference
Expected: the returned reference is the original input
Actual: same reference
PASS

Summary: 9/9 checks passed.
```

## 專案結構

```plaintext
leetcode_912/
├── .editorconfig
├── .gitattributes
├── .gitignore
├── .vscode/
│   ├── launch.json
│   └── tasks.json
├── AGENTS.md
├── README.md
├── docs/
│   ├── readme-template.md
│   └── superpowers/
│       ├── plans/
│       │   └── 2026-07-16-leetcode-912-net10-migration.md
│       └── specs/
│           └── 2026-07-16-leetcode-912-net10-migration-design.md
└── leetcode_912/
    ├── Program.cs
    └── leetcode_912.csproj
```
