# LeetCode 209 — Minimum Size Subarray Sum

![.NET 10](https://img.shields.io/badge/.NET-10.0-512BD4?logo=dotnet&logoColor=white)

以 C# 實作 LeetCode 209「最小大小子陣列總和」。專案保留暴力法與滑動視窗兩種解法，並在 `Main` 直接執行固定案例，以預期值與 PASS/FAIL 對照驗證結果。

## 題目說明

給定正整數陣列 `nums` 與正整數 `target`，找出總和大於或等於 `target` 的**最短連續子陣列**長度。若不存在符合條件的子陣列，回傳 `0`。

例如 `target = 7`、`nums = [2, 3, 1, 2, 4, 3]` 時，`[4, 3]` 的總和為 `7`、長度為 `2`，因此答案是 `2`。

## 限制條件

題目正式限制請以 [LeetCode 209](https://leetcode.com/problems/minimum-size-subarray-sum/) 為準：

- `1 <= target <= 10^9`
- `1 <= nums.length <= 10^5`
- `1 <= nums[i] <= 10^4`
- `nums` 中所有元素皆為正整數。

最後一項是本題的關鍵：視窗右界向右擴張時總和只會增加；左界右移、移除元素時總和只會減少。這使得滑動視窗可以安全地在一次線性掃描中找出答案；若允許負數，必須改用其他策略。

## 解題概念與出發點

目標是「長度最短」，而不是「總和最大」。因此每次找到總和達標的連續區間後，都要嘗試縮短它。

專案提供兩種方法：

| 方法 | 核心想法 | 時間複雜度 | 空間複雜度 | 適用情境 |
| --- | --- | --- | --- | --- |
| 暴力法 `MinSubArrayLen` | 枚舉每個起點，向右累加直到首次達標 | `O(n^2)` | `O(1)` | 用來建立直覺、驗證小型輸入 |
| 滑動視窗 `MinSubArrayLen2` | 右界擴張、達標後收縮左界 | `O(n)` | `O(1)` | 正式解法，適用題目最大限制 |

## 解法一：暴力法

### 設計說明

外層迴圈逐一選擇子陣列起點 `i`；內層迴圈從 `i` 向右累加到 `j`。一旦 `sum >= target`，目前視窗就是固定起點 `i` 時最短的有效視窗，便可更新全域最短長度並停止該起點的內層迴圈。

這個提早停止成立的原因是輸入全為正整數：再加入任何元素都只會讓視窗更長，不可能改善固定起點下的答案。

### 經典案例演示

輸入：`target = 7`，`nums = [2, 3, 1, 2, 4, 3]`

| 起點 `i` | 首次達標的子陣列 | 總和 | 長度 | 目前最短長度 |
| --- | --- | --- | --- | --- |
| 0 | `[2, 3, 1, 2]` | 8 | 4 | 4 |
| 1 | `[3, 1, 2, 4]` | 10 | 4 | 4 |
| 2 | `[1, 2, 4]` | 7 | 3 | 3 |
| 3 | `[2, 4, 3]` | 9 | 3 | 3 |
| 4 | `[4, 3]` | 7 | 2 | 2 |
| 5 | 無法達標 | 3 | — | 2 |

最終答案為 `2`。最壞情況下，每個起點都可能掃到陣列末端，時間複雜度為 `O(n^2)`。

## 解法二：滑動視窗

### 設計說明

維護一個由 `left` 與 `right` 表示的連續視窗，以及視窗總和 `sum`：

1. `right` 由左至右移動，將新元素加入 `sum`。
2. 當 `sum >= target` 時，當前視窗有效，先以 `right - left + 1` 更新答案。
3. 接著移除 `nums[left]` 並將 `left` 右移，在仍達標時持續重複，以找出同一個 `right` 下最短的有效視窗。
4. 迴圈結束後，若從未達標，代表答案仍是初始哨兵值，回傳 `0`。

每個元素只會被 `right` 加入一次，也只會被 `left` 移除一次，因此總操作數與陣列長度成正比。

### 經典案例演示

輸入：`target = 7`，`nums = [2, 3, 1, 2, 4, 3]`

| `right` 加入值 | 視窗與總和 | 動作 | 最短長度 |
| --- | --- | --- | --- |
| 2 | `[2]`，2 | 未達標，繼續擴張 | — |
| 3 | `[2, 3]`，5 | 未達標，繼續擴張 | — |
| 1 | `[2, 3, 1]`，6 | 未達標，繼續擴張 | — |
| 2 | `[2, 3, 1, 2]`，8 | 記錄長度 4；移除 2 後總和變 6 | 4 |
| 4 | `[3, 1, 2, 4]`，10 | 記錄 4；移除 3 後仍為 7，再記錄 3；移除 1 後變 6 | 3 |
| 3 | `[2, 4, 3]`，9 | 記錄 3；移除 2 後仍為 7，再記錄 `[4, 3]` 的長度 2；移除 4 後變 3 | 2 |

答案為 `2`。此法時間複雜度為 `O(n)`，額外空間複雜度為 `O(1)`。

### 其他範例流程

- **剛好命中：** `target = 4`、`nums = [1, 4, 4]`。滑動視窗遇到第一個 `4` 時，單一元素視窗已經達標，立即得到最短可能長度 `1`。
- **無解：** `target = 11`、`nums = [1, 1, 1, 1, 1, 1, 1, 1]`。整個陣列總和只有 `8`，視窗從未達標，因此兩種方法都回傳 `0`。

## 可執行範例

`Main` 會執行下列案例，並各自驗證暴力法與滑動視窗：

| 案例 | `target` | `nums` | 預期結果 |
| --- | --- | --- | --- |
| Canonical | 7 | `[2, 3, 1, 2, 4, 3]` | 2 |
| Exact match | 4 | `[1, 4, 4]` | 1 |
| No solution | 11 | `[1, 1, 1, 1, 1, 1, 1, 1]` | 0 |

實際執行輸出如下：

```text
LeetCode 209 - Minimum Size Subarray Sum

Case: Canonical
Input: target = 7, nums = [2, 3, 1, 2, 4, 3]
Expected: 2
Brute Force: 2 (PASS)
Sliding Window: 2 (PASS)

Case: Exact match
Input: target = 4, nums = [1, 4, 4]
Expected: 1
Brute Force: 1 (PASS)
Sliding Window: 1 (PASS)

Case: No solution
Input: target = 11, nums = [1, 1, 1, 1, 1, 1, 1, 1]
Expected: 0
Brute Force: 0 (PASS)
Sliding Window: 0 (PASS)

Summary: 6/6 checks passed.
```

## 建置與執行

需安裝 .NET 10 SDK。

```bash
dotnet restore leetcode_209/leetcode_209.csproj
dotnet build leetcode_209/leetcode_209.csproj --no-restore
dotnet run --project leetcode_209/leetcode_209.csproj --no-build
```

提交前可檢查多餘空白與換行：

```bash
git diff --check
```

## 專案結構

```text
.
├── leetcode_209/
│   ├── Program.cs                 # 兩種解法與可執行範例
│   └── leetcode_209.csproj        # .NET 10 主控台專案設定
├── docs/
│   └── readme-template.md         # README 建立參考範本
└── README.md
```
