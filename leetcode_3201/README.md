# leetcode_3201 專案說明

## 題目簡介

本專案針對 LeetCode 第 3201 題「找出有效子序列的最大長度 I」進行解題與程式碼實作，提供兩種不同的解法並進行詳細比較。

- **LeetCode 英文版**：[Find the Maximum Length of Valid Subsequence I](https://leetcode.com/problems/find-the-maximum-length-of-valid-subsequence-i/description/)
- **LeetCode 中文版**：[找出有效子序列的最大長度 I](https://leetcode.cn/problems/find-the-maximum-length-of-valid-subsequence-i/description/?envType=daily-question&envId=2025-07-16)

### 題目描述

給定一個整數陣列 `nums`。定義一個長度為 x 的 nums 子序列被稱為**有效**，若滿足：

```text
(sub[0] + sub[1]) % 2 == (sub[1] + sub[2]) % 2 == ... == (sub[x - 2] + sub[x - 1]) % 2
```

請回傳 `nums` 最長有效子序列的長度。

**注意**：子序列是可以從原陣列刪除部分元素（或不刪除）且不改變剩餘元素順序所得到的陣列。

---

## 專案結構

```text
leetcode_3201/
├── Program.cs             # 主程式，包含兩種解法與測試範例
├── README.md             # 專案說明文件
└── leetcode_3201.csproj  # C# 專案檔案
```

---

## 兩種解法詳細說明與比較

### 🔥 解法一：動態規劃 - 考察子序列的最後兩項（MaximumLength）

#### 核心思路

透過數學分析發現，有效子序列的約束條件可以轉化為：

- **偶數項** `sub[0], sub[2], sub[4], ...` 都關於模 2 同餘
- **奇數項** `sub[1], sub[3], sub[5], ...` 都關於模 2 同餘

#### 數學推導

對於等式 `(a+b) % 2 = (b+c) % 2`，可以推導出：

```text
(a+b-(b+c)) % 2 = 0
(a-c) % 2 = 0
```

這意味著 `sub[i]` 與 `sub[i+2]` 關於模 2 同餘。

#### 動態規劃實作

```csharp
public int MaximumLength(int[] nums)
{
    int k = 2; // 模 2，只考慮餘數 0（偶數）和 1（奇數）
    
    if (nums is null || nums.Length == 0)
        return 0;
    
    int ans = 0;
    // f[y, x]: 表示最後兩項模 k 分別為 y 和 x 的子序列長度
    int[,] f = new int[k, k]; 
    
    foreach (var num in nums)
    {
        int x = num % k;
        for (int y = 0; y < k; y++)
        {
            // 狀態轉移：f[y, x] = f[x, y] + 1
            f[y, x] = f[x, y] + 1;
            ans = Math.Max(ans, f[y, x]);
        }
    }
    return ans;
}
```

#### 優缺點分析

- **優點**：
  - 時間複雜度：O(n × k) = O(n)，其中 k = 2
  - 空間複雜度：O(k²) = O(1)
  - 效能優異，適合大數據量處理
- **缺點**：
  - 需要理解動態規劃狀態轉移，學習曲線較陡

---

### 🎯 解法二：枚舉元素的奇偶性（MaximumLengthEnum）

#### 核心觀察

根據有效子序列的定義，可以發現：

- 子序列中所有**奇數位置**（index 1,3,5...）元素奇偶性相同
- 子序列中所有**偶數位置**（index 0,2,4...）元素奇偶性相同

#### 四種奇偶性模式

```text
模式 {0,0}：全為偶數 → [偶,偶,偶,偶,...]
模式 {0,1}：偶奇交替 → [偶,奇,偶,奇,...]  
模式 {1,0}：奇偶交替 → [奇,偶,奇,偶,...]
模式 {1,1}：全為奇數 → [奇,奇,奇,奇,...]
```

#### 貪心策略實作

```csharp
public int MaximumLengthEnum(int[] nums)
{
    if (nums is null || nums.Length == 0)
        return 0;
        
    int res = 0;
    int[,] patterns = new int[4, 2] 
    { 
        { 0, 0 }, // 模式1：全為偶數
        { 0, 1 }, // 模式2：偶奇交替
        { 1, 0 }, // 模式3：奇偶交替  
        { 1, 1 }  // 模式4：全為奇數
    };
    
    // 枚舉四種奇偶性模式
    for (int i = 0; i < 4; i++)
    {
        int cnt = 0;
        foreach (int num in nums)
        {
            // 貪心策略：符合模式就立即加入子序列
            if (num % 2 == patterns[i, cnt % 2])
                cnt++;
        }
        res = Math.Max(res, cnt);
    }
    return res;
}
```

#### 演算法範例分析

以 `nums = [1,2,1,1,2,1,2]` 為例：

**模式 {0,1} - 偶奇交替**：

```text
遍歷過程：
1(奇,位置0需偶,×) → 2(偶,位置0需偶,✓,cnt=1) → 1(奇,位置1需奇,✓,cnt=2) 
→ 1(奇,位置2需偶,×) → 2(偶,位置2需偶,✓,cnt=3) → 1(奇,位置3需奇,✓,cnt=4) 
→ 2(偶,位置4需偶,✓,cnt=5)

結果：cnt = 5，對應子序列 [2,1,2,1,2]
```

#### 優缺點分析

- **優點**：
  - 思路直觀，容易理解和實作
  - 便於除錯與驗證
  - 適合教學和程式碼展示
- **缺點**：
  - 時間複雜度：O(4n) = O(n)，但有 4 倍常數開銷
  - 空間複雜度：O(1)

---

## 解法效能比較

| 解法 | 時間複雜度 | 空間複雜度 | 易懂性 | 執行效率 | 適用場景 |
|------|------------|------------|--------|----------|----------|
| MaximumLength | O(n) | O(1) | ⭐⭐ | ⭐⭐⭐⭐⭐ | 大數據量、效能優先 |
| MaximumLengthEnum | O(n) | O(1) | ⭐⭐⭐⭐⭐ | ⭐⭐⭐⭐ | 教學、理解、中小型資料 |

### 選擇建議

- **追求效能**：選擇 `MaximumLength`（動態規劃）
- **追求易懂**：選擇 `MaximumLengthEnum`（枚舉奇偶性）
- **學習演算法**：建議兩種方法都理解

---

## 測試範例與預期結果

### 測試資料

```csharp
// 測試資料 1
int[] nums1 = { 1, 2, 3, 4 };
Console.WriteLine($"Input: [1,2,3,4]");
Console.WriteLine($"MaximumLength Output: {new Program().MaximumLength(nums1)}"); // 輸出: 4
Console.WriteLine($"MaximumLengthEnum Output: {new Program().MaximumLengthEnum(nums1)}"); // 輸出: 4

// 測試資料 2  
int[] nums2 = { 1, 2, 1, 1, 2, 1, 2 };
Console.WriteLine($"Input: [1,2,1,1,2,1,2]");
Console.WriteLine($"MaximumLength Output: {new Program().MaximumLength(nums2)}"); // 輸出: 5
Console.WriteLine($"MaximumLengthEnum Output: {new Program().MaximumLengthEnum(nums2)}"); // 輸出: 5

// 測試資料 3
int[] nums3 = { 1, 3 };
Console.WriteLine($"Input: [1,3]");
Console.WriteLine($"MaximumLength Output: {new Program().MaximumLength(nums3)}"); // 輸出: 2
Console.WriteLine($"MaximumLengthEnum Output: {new Program().MaximumLengthEnum(nums3)}"); // 輸出: 2
```

### 結果分析

- **範例 1 `[1,2,3,4]`**：最長有效子序列為 `[1,2,3,4]`（長度 4）
- **範例 2 `[1,2,1,1,2,1,2]`**：最長有效子序列為 `[2,1,2,1,2]`（長度 5）
- **範例 3 `[1,3]`**：最長有效子序列為 `[1,3]`（長度 2）

---

## 執行方式

### 編譯與執行

```bash
# 編譯專案
dotnet build

# 執行程式
dotnet run

# 或直接執行編譯後的檔案
./bin/Debug/net8.0/leetcode_3201
```

### 預期輸出

```text
Input: [1,2,3,4]  Output: 4
Input: [1,2,3,4]  MaximumLengthEnum Output: 4
Input: [1,2,1,1,2,1,2]  Output: 5
Input: [1,2,1,1,2,1,2]  MaximumLengthEnum Output: 5
Input: [1,3]  Output: 2
Input: [1,3]  MaximumLengthEnum Output: 2
```

---

## 參考資料

### 官方題解

- [動態規劃解法詳解](https://leetcode.cn/problems/find-the-maximum-length-of-valid-subsequence-i/solutions/2826593/deng-jie-zhuan-huan-dong-tai-gui-hua-pyt-7l4b/?envType=daily-question&envId=2025-07-16)
- [枚舉奇偶性解法](https://leetcode.cn/problems/find-the-maximum-length-of-valid-subsequence-i/solutions/3717152/zhao-chu-you-xiao-zi-xu-lie-de-zui-da-ch-1n3j/?envType=daily-question&envId=2025-07-16)

### 相關題目

- [LeetCode 3202: 找出有效子序列的最大長度 II](https://leetcode.cn/problems/find-the-maximum-length-of-valid-subsequence-ii/solutions/2826591/deng-jie-zhuan-huan-dong-tai-gui-hua-pyt-z2fs/)

---

## 技術規格

- **程式語言**：C# 12.0
- **目標框架**：.NET 8.0
- **開發環境**：Visual Studio Code
- **專案類型**：Console Application

---

## 聯絡方式

如有任何問題或建議，歡迎透過以下方式聯絡：

- **GitHub Issues**：提交問題或功能請求
- **Pull Request**：歡迎程式碼貢獻與改進
        }
    }
    return ans;
}
```

#### 優缺點
- **優點**：
  - 時間複雜度 O(n)，空間複雜度 O(1)。
  - 適合大數據量，效能佳。
- **缺點**：
  - 需要理解動態規劃狀態轉移，較不直觀。

---

### 解法二：枚舉奇偶性（MaximumLengthEnum）

#### 原理
- 根據題目定義，子序列中所有奇數下標的元素奇偶性相同，所有偶數下標的元素奇偶性相同。
- 枚舉所有可能的奇偶性組合（共 4 種），分別計算最大長度。

#### 實作
```csharp
public int MaximumLengthEnum(int[] nums)
{
    int res = 0;
    int[,] patterns = new int[4, 2] { { 0, 0 }, { 0, 1 }, { 1, 0 }, { 1, 1 } };
    for (int i = 0; i < 4; i++)
    {
        int cnt = 0;
        foreach (int num in nums)
        {
            if (num % 2 == patterns[i, cnt % 2])
            {
                cnt++;
            }
        }
        res = Math.Max(res, cnt);
    }
    return res;
}
```

#### 優缺點
- **優點**：
  - 思路直觀，容易理解。
  - 便於 debug 與驗證。
- **缺點**：
  - 雖然理論上也是 O(n)，但每次都要枚舉 4 種模式，略有額外常數開銷。

---

## 兩種解法比較
| 解法 | 時間複雜度 | 空間複雜度 | 易懂性 | 適用場景 |
|------|------------|------------|--------|----------|
| MaximumLength | O(n) | O(1) | 較難 | 大數據量、效能要求 |
| MaximumLengthEnum | O(n) | O(1) | 易懂 | 小型資料、教學展示 |

- **MaximumLength** 適合追求效能、理解動態規劃的場景。
- **MaximumLengthEnum** 適合初學者、需要直觀解釋的場合。

---

## 測試範例

```csharp
int[] nums1 = { 1, 2, 3, 4 };
Console.WriteLine(new Program().MaximumLength(nums1)); // Output: 3
Console.WriteLine(new Program().MaximumLengthEnum(nums1)); // Output: 3

int[] nums2 = { 1, 2, 1, 1, 2, 1, 2 };
Console.WriteLine(new Program().MaximumLength(nums2)); // Output: 6
Console.WriteLine(new Program().MaximumLengthEnum(nums2)); // Output: 6

int[] nums3 = { 1, 3 };
Console.WriteLine(new Program().MaximumLength(nums3)); // Output: 2
Console.WriteLine(new Program().MaximumLengthEnum(nums3)); // Output: 2
```

---

## 參考資料
- [LeetCode 題解（動態規劃）](https://leetcode.cn/problems/find-the-maximum-length-of-valid-subsequence-i/solutions/2826593/deng-jie-zhuan-huan-dong-tai-gui-hua-pyt-7l4b/?envType=daily-question&envId=2025-07-16)
- [LeetCode 題解（枚舉）](https://leetcode.cn/problems/find-the-maximum-length-of-valid-subsequence-i/solutions/3717152/zhao-chu-you-xiao-zi-xu-lie-de-zui-da-ch-1n3j/?envType=daily-question&envId=2025-07-16)

---

## 聯絡方式
如有任何問題，歡迎於 Issues 留言或 PR 討論。
