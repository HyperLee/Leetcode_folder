# LeetCode 47. Permutations II - 全排列 II

[![LeetCode](https://img.shields.io/badge/LeetCode-47-orange?style=flat-square)](https://leetcode.com/problems/permutations-ii/)
[![Difficulty](https://img.shields.io/badge/Difficulty-Medium-yellow?style=flat-square)](https://leetcode.com/problems/permutations-ii/)
[![Language](https://img.shields.io/badge/Language-C%23-blue?style=flat-square)](https://docs.microsoft.com/dotnet/csharp/)

使用 **搜索回溯（Backtracking）** 演算法解決含重複元素的全排列問題。

## 題目描述

給定一組**可能包含重複數字**的整數陣列 `nums`，請回傳所有可能的**不重複排列**，回傳順序不限。

### 範例

**範例 1：**
```
輸入：nums = [1, 1, 2]
輸出：[[1, 1, 2], [1, 2, 1], [2, 1, 1]]
```

**範例 2：**
```
輸入：nums = [1, 2, 3]
輸出：[[1, 2, 3], [1, 3, 2], [2, 1, 3], [2, 3, 1], [3, 1, 2], [3, 2, 1]]
```

### 限制條件

- `1 <= nums.length <= 8`
- `-10 <= nums[i] <= 10`

## 解題思路與出發點

### 問題分析

此題是 [LeetCode 46. Permutations](https://leetcode.com/problems/permutations/) 的進階版本。主要差異在於：

| 題目 | 輸入特性 | 輸出要求 |
|------|----------|----------|
| 46. Permutations | 元素互不相同 | 所有排列 |
| **47. Permutations II** | **可能有重複元素** | **不重複的排列** |

### 核心思想

1. **排序預處理**：先將陣列排序，使相同元素相鄰，便於後續去重判斷
2. **回溯搜索**：使用 Backtracking 演算法逐步建構排列
3. **剪枝去重**：在同一層遞迴中，跳過相同元素以避免重複排列

### 去重條件的關鍵

```csharp
if (i > 0 && nums[i] == nums[i - 1] && !used[i - 1])
{
    continue;
}
```

> [!IMPORTANT]
> **為什麼是 `!used[i-1]` 而不是 `used[i-1]`？**
> 
> - `!used[i-1]`：表示前一個相同元素在「同一層」已被選過並回溯了
> - 這代表我們正在嘗試用相同的值建立相同的分支，必須跳過
> - 確保相同元素只會按照**固定順序**被選取，避免產生重複排列

## 詳細解法說明

### 演算法流程

```
PermuteUnique(nums)
├── 1. 排序陣列：使相同元素相鄰
├── 2. 初始化 used[] 陣列：追蹤元素使用狀態
└── 3. 呼叫 Backtracking() 開始遞迴搜索
        ├── 終止條件：排列長度 == 陣列長度 → 儲存結果
        └── 遍歷每個元素
            ├── 檢查是否已使用 → 跳過
            ├── 檢查去重條件 → 跳過重複分支
            ├── 做選擇：加入元素、標記使用
            ├── 遞迴：繼續建構下一位置
            └── 回溯：撤銷選擇、取消標記
```

### 程式碼結構

| 方法 | 說明 |
|------|------|
| `PermuteUnique()` | 主函式，負責排序、初始化並啟動回溯 |
| `Backtracking()` | 遞迴回溯核心，建構排列並處理去重 |

### 複雜度分析

| 複雜度 | 數值 | 說明 |
|--------|------|------|
| 時間複雜度 | O(n × n!) | 最多 n! 種排列，每個排列複製需 O(n) |
| 空間複雜度 | O(n) | 遞迴堆疊深度 + used 陣列 |

## 範例演示流程

以 `nums = [1, 1, 2]` 為例：

### Step 1：排序

```
排序後：[1, 1, 2]（已排序，相同元素相鄰）
used = [false, false, false]
```

### Step 2：回溯搜索樹

```
                          []
                     ╱    │    ╲
                   1      1̶      2
                 (i=0)  (i=1)  (i=2)
                        跳過
                ╱    ╲         ╱    ╲
               1      2       1      1̶
             (i=1)  (i=2)   (i=0)  (i=1)
                                   跳過
              │      │       │
              2      1       1
            (i=2)  (i=1)   (i=1)
              │      │       │
           [1,1,2] [1,2,1] [2,1,1]
```

### Step 3：去重說明

當 `i=1` 時選擇第二個 `1`：
- `nums[1] == nums[0]`（值相同）
- `used[0] == false`（前一個 1 未被使用，表示已回溯）
- **跳過此分支**，避免產生與 `i=0` 相同的排列

### 最終結果

```
[[1, 1, 2], [1, 2, 1], [2, 1, 1]]
```

## 搜索回溯（Backtracking）補充說明

### 什麼是回溯演算法？

**回溯演算法（Backtracking）** 是一種通過「試錯」來尋找問題解的演算法。當發現當前選擇無法達成目標時，就「回溯」到上一步，嘗試其他選擇。

### 回溯法的基本模板

```csharp
void Backtrack(路徑, 選擇列表)
{
    if (滿足終止條件)
    {
        儲存結果;
        return;
    }
    
    foreach (選擇 in 選擇列表)
    {
        if (不符合條件) continue;  // 剪枝
        
        做選擇;                    // 將選擇加入路徑
        Backtrack(路徑, 選擇列表);  // 遞迴
        撤銷選擇;                  // 回溯，移除選擇
    }
}
```

### 回溯法的核心概念

| 概念 | 說明 | 本題對應 |
|------|------|----------|
| **路徑** | 已經做出的選擇 | `list`（當前排列） |
| **選擇列表** | 當前可以做的選擇 | `nums` 中未使用的元素 |
| **終止條件** | 到達決策樹底層 | `list.Count == nums.Length` |
| **剪枝** | 提前排除不可行分支 | 去重條件判斷 |

### 回溯 vs 動態規劃

| 特性 | 回溯法 | 動態規劃 |
|------|--------|----------|
| 目標 | 找出所有解 | 找出最優解 |
| 思路 | 深度優先遍歷 | 狀態轉移 |
| 適用場景 | 排列、組合、子集 | 最值問題 |

### 回溯法的經典應用

- **排列問題**：全排列（含/不含重複元素）
- **組合問題**：組合總和、子集
- **棋盤問題**：N 皇后、解數獨
- **路徑問題**：迷宮、單詞搜索

> [!TIP]
> 回溯法的效率關鍵在於**剪枝**——越早排除無效分支，效率越高。

## 執行方式

### 環境需求

- [.NET 10 SDK](https://dotnet.microsoft.com/download)

### 建構與執行

```bash
# 建構專案
dotnet build

# 執行程式
dotnet run --project leetcode_047/leetcode_047.csproj
```

### 預期輸出

```
輸入: nums = [1, 1, 2]
輸出:
[
  [1, 1, 2]
  [1, 2, 1]
  [2, 1, 1]
]

輸入: nums = [1, 2, 3]
輸出:
[
  [1, 2, 3]
  [1, 3, 2]
  [2, 1, 3]
  [2, 3, 1]
  [3, 1, 2]
  [3, 2, 1]
]

輸入: nums = [2, 2, 2]
輸出:
[
  [2, 2, 2]
]
```

## 相關題目

| 題號 | 題目 | 難度 | 關聯 |
|------|------|------|------|
| 46 | [Permutations](https://leetcode.com/problems/permutations/) | Medium | 無重複元素的全排列 |
| 31 | [Next Permutation](https://leetcode.com/problems/next-permutation/) | Medium | 下一個排列 |
| 60 | [Permutation Sequence](https://leetcode.com/problems/permutation-sequence/) | Hard | 第 k 個排列 |
| 77 | [Combinations](https://leetcode.com/problems/combinations/) | Medium | 組合問題 |
| 78 | [Subsets](https://leetcode.com/problems/subsets/) | Medium | 子集問題 |
