# LeetCode 2163: 刪除元素後和的最小差值

## 題目描述

### 2163. Minimum Difference in Sums After Removal of Elements

- **English**: [LeetCode 2163](https://leetcode.com/problems/minimum-difference-in-sums-after-removal-of-elements/)
- **中文**: [LeetCode 2163 中文](https://leetcode.cn/problems/minimum-difference-in-sums-after-removal-of-elements/)

給你一個下標從 0 開始、長度為 3*n 的整數陣列 `nums`。你可以移除 `nums` 中恰好 n 個元素，剩下的 2*n 個元素會被分成兩個部分：

- 前 n 個元素屬於第一部分，和為 `sumfirst`
- 接下來 n 個元素屬於第二部分，和為 `sumsecond`

差值為 `sumfirst - sumsecond`。請返回移除 n 個元素後，兩部分和的最小差值。

### 約束條件

- `nums.length == 3 * n`
- `1 <= n <= 10^5`
- `1 <= nums[i] <= 10^5`

## 解法說明

本專案提供了兩種不同的解法來解決這個問題，兩種方法都使用了優先佇列（Priority Queue）的資料結構，但思路略有不同。

### 解法一：動態分割點方法 (`MinimumDifference`)

#### 核心思路

1. **分割點策略**：在 [n, 2n] 中選擇分割點 k，前 k 個數屬於第一部分，後 3n-k 個數屬於第二部分
2. **優化目標**：第一部分選擇 n 個最小元素，第二部分選擇 n 個最大元素
3. **雙向處理**：
   - 正向處理：維護第一部分的 n 個最小元素
   - 逆向處理：維護第二部分的 n 個最大元素

#### 演算法流程

```text
步驟 1: 初始化大根堆，處理前 n 個元素
步驟 2: 正向掃描 [n, 2n)，動態維護第一部分最小和
步驟 3: 初始化小根堆，處理後 n 個元素  
步驟 4: 逆向掃描 [n, 2n)，動態維護第二部分最大和
步驟 5: 計算並更新最小差值
```

#### 資料結構使用

- **大根堆 (Max Heap)**：維護第一部分的 n 個最小元素，堆頂為最大值便於移除
- **小根堆 (Min Heap)**：維護第二部分的 n 個最大元素，堆頂為最小值便於移除

### 解法二：前後綴預處理方法 (`MinimumDifference2`)

#### 解題思路

1. **障眼法識破**：刪除元素是障眼法，重點在於如何選擇 2n 個數
2. **前後綴處理**：
   - 計算前綴最小和：`preMin[i]` 表示 nums[0] 到 nums[i] 中最小的 n 個元素之和
   - 計算後綴最大和：`sufMax[i]` 表示 nums[i] 到 nums[3n-1] 中最大的 n 個元素之和
3. **分割枚舉**：答案為所有 `preMin[i] - sufMax[i+1]` 中的最小值

#### 演算法步驟

```text
步驟 1: 從右往左，使用小根堆計算後綴最大和陣列
步驟 2: 從左往右，使用大根堆計算前綴最小和並同時計算答案
步驟 3: 返回所有可能分割點的最小差值
```

#### 使用的資料結構

- **小根堆 (Min Heap)**：維護後綴最大和的 n 個最大元素
- **大根堆 (Max Heap)**：維護前綴最小和的 n 個最小元素

## 兩種解法比較分析

### 時間複雜度比較

| 解法 | 時間複雜度 | 空間複雜度 | 說明 |
|------|-----------|-----------|------|
| 解法一 | O(n log n) | O(n) | 兩次線性掃描，每次堆操作 O(log n) |
| 解法二 | O(n log n) | O(n) | 兩次線性掃描，每次堆操作 O(log n) |

### 實作差異對比

| 特徵 | 解法一 | 解法二 |
|------|--------|--------|
| **思路核心** | 動態分割點 | 前後綴預處理 |
| **掃描方向** | 正向 + 逆向 | 逆向 + 正向 |
| **記憶體使用** | part1 陣列 (n+1) + 兩個堆 | sufMax 陣列 (m-n+1) + 兩個堆 |
| **程式碼複雜度** | 相對複雜 | 相對簡潔 |
| **可讀性** | 中等 | 較好 |

### 優缺點分析

#### 解法一優缺點

**優點：**

- 直觀地模擬了分割過程
- 能清楚看到每個分割點的計算過程
- 對理解問題本質有幫助

**缺點：**

- 需要兩次掃描，程式碼稍長
- 索引計算相對複雜
- part1 陣列的索引映射需要小心處理

#### 解法二優缺點

**優點：**

- 思路更加清晰，將問題轉化為前後綴問題
- 程式碼結構更加簡潔
- 更容易理解和實作
- 預處理思想在很多問題中都有應用

**缺點：**

- 需要額外的後綴陣列儲存空間
- 對初學者來說，前後綴思想可能不夠直觀

### 效能測試結果

兩種解法在測試案例上的表現：

```csharp
// 測試案例 1: nums = [3,1,2,6,5,4], n = 2
解法一結果: -8, 解法二結果: -8

// 測試案例 2: nums = [7,9,8,6,2,6], n = 2  
解法一結果: 2, 解法二結果: 2

// 測試案例 3: nums = [7,9,8,6,2,6,1,1,1], n = 3
解法一結果: 7, 解法二結果: 7

兩種解法得到相同的結果，驗證了演算法的正確性 ✓
```

## 專案結構

```text
leetcode_2163/
├── README.md                    # 本說明文件
├── leetcode_2163.sln           # Visual Studio 解決方案檔
├── leetcode_2163/
│   ├── leetcode_2163.csproj    # C# 專案檔
│   ├── Program.cs               # 主程式碼文件
│   ├── bin/                     # 建構輸出目錄
│   └── obj/                     # 建構暫存目錄
└── .github/
    └── instructions/
        └── csharp.instructions.md  # C# 開發指南
```

## 建構與執行

### 前置需求

- .NET 8.0 或更高版本
- Visual Studio Code 或 Visual Studio

### 建構專案

```bash
# 使用 dotnet CLI
dotnet build

# 或使用 VS Code 任務
Ctrl+Shift+P -> Tasks: Run Task -> build
```

### 執行程式

```bash
# 使用 dotnet CLI
dotnet run --project leetcode_2163/leetcode_2163.csproj

# 或直接執行
cd leetcode_2163
dotnet run
```

## 核心演算法解析

### 優先佇列的巧妙運用

兩種解法都巧妙地使用了優先佇列來維護動態的最值集合：

1. **大根堆維護最小集合**：看似矛盾，實際上是為了快速移除集合中的最大元素
2. **小根堆維護最大集合**：同理，為了快速移除集合中的最小元素

### 關鍵洞察

1. **問題轉化**：將「刪除 n 個元素」轉化為「選擇 2n 個元素並合理分組」
2. **貪心策略**：為了最小化差值，第一部分要儘可能小，第二部分要儘可能大
3. **動態維護**：使用堆資料結構動態維護滑動視窗中的極值集合

## 學習重點

1. **優先佇列的靈活運用**：理解如何使用不同類型的堆來解決實際問題
2. **前後綴思想**：將複雜問題分解為前綴和後綴的組合
3. **貪心演算法**：在每個局部選擇中都做最優決策
4. **問題轉化能力**：將表面的「刪除」問題轉化為本質的「選擇」問題

## 參考資料

- [LeetCode 官方題目](https://leetcode.com/problems/minimum-difference-in-sums-after-removal-of-elements/)
- [LeetCode 中文題目](https://leetcode.cn/problems/minimum-difference-in-sums-after-removal-of-elements/)
- [解題參考](https://leetcode.cn/problems/minimum-difference-in-sums-after-removal-of-elements/solutions/1249409/shan-chu-yuan-su-hou-he-de-zui-xiao-chai-ah0j/)

---

**開發者**: [您的名稱]  
**最後更新**: 2025年7月18日  
**程式語言**: C# 12 (.NET 8.0)
