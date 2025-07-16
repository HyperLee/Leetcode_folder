# leetcode_3201 專案說明

## 題目簡介

本專案針對 LeetCode 第 3201 題「找出有效子序列的最大長度 I」進行解題與程式碼實作。

- 題目連結：[LeetCode 英文版](https://leetcode.com/problems/find-the-maximum-length-of-valid-subsequence-i/description/)
- 題目連結：[LeetCode 中文版](https://leetcode.cn/problems/find-the-maximum-length-of-valid-subsequence-i/description/?envType=daily-question&envId=2025-07-16)

### 題目描述
給定一個整數陣列 `nums`，定義一個子序列為有效，若滿足：
```
(sub[0] + sub[1]) % 2 == (sub[1] + sub[2]) % 2 == ... == (sub[x - 2] + sub[x - 1]) % 2
```
請回傳 `nums` 最長有效子序列的長度。

---

## 專案結構
- `Program.cs`：主程式，包含兩種解法與測試範例。

---

## 兩種解法詳細說明與比較

### 解法一：動態規劃（MaximumLength）

#### 原理
- 透過模運算移項可得 `(sub[i] - sub[i+2]) % 2 == 0`，代表偶數項彼此同餘、奇數項彼此同餘。
- 問題等價於：求一個子序列，其偶數位都同餘、奇數位都同餘。
- 使用動態規劃，維護一個二維陣列 `f[y, x]`，表示最後兩項模 2 分別為 y 和 x 的子序列長度。

#### 實作
```csharp
public int MaximumLength(int[] nums)
{
    int k = 2;
    if (nums is null || nums.Length == 0)
    {
        return 0;
    }
    int ans = 0;
    int[,] f = new int[k, k];
    foreach (var num in nums)
    {
        int x = num % k;
        for (int y = 0; y < k; y++)
        {
            f[y, x] = f[x, y] + 1;
            ans = Math.Max(ans, f[y, x]);
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
