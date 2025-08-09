# leetcode_231: Power of Two

> 判斷一個整數是否為 2 的冪

[![LeetCode - Power of Two](https://img.shields.io/badge/LeetCode-231-blue)](https://leetcode.com/problems/power-of-two/)

---

## 專案簡介

本專案為 LeetCode 第 231 題「Power of Two」的 C# 解題範例，包含兩種常見且高效的位運算解法，並附有詳細註解與測試資料，適合學習位運算技巧與 C# 程式設計。

## 題目說明

> 給定一個整數 n，判斷其是否為 2 的冪。
>
> - 若存在一個整數 x 使得 n == 2^x，則 n 為 2 的冪。

- [LeetCode 題目連結 (英文)](https://leetcode.com/problems/power-of-two/)
- [LeetCode 題目連結 (中文)](https://leetcode.cn/problems/power-of-two/)

## 解法說明
> 兩種解法皆為 O(1) 時間複雜度，適合面試與高效運算場景。

### 解法一：n & (n - 1)

這個技巧利用了二進位的特性：

- 若 n 為 2 的冪，則其二進位僅有一個 1，其餘皆為 0，例如 8 = 1000。
- n - 1 會將 n 的最低位 1 變為 0，且該位之後的所有 0 變為 1。例如 8 - 1 = 7 = 0111。
- n & (n - 1) 會將 n 的最低位 1 移除，若結果為 0，代表 n 僅有一個 1。
- 只有正整數才有意義，0 或負數皆不可能是 2 的冪。

**舉例說明：**

- n = 8 (1000)
- n - 1 = 7 (0111)
- n & (n - 1) = 1000 & 0111 = 0000

因此，若 n > 0 且 n & (n - 1) == 0，則 n 為 2 的冪。

### 解法二：n & -n

這個技巧利用了補數（two's complement）與位運算：

- -n 在電腦中以補數表示，等於 ~n + 1。
- n & -n 會保留 n 的最低位 1，其餘皆為 0。
- 若 n 僅有一個 1（即為 2 的冪），則 n & -n == n。
- 若 n 有多個 1，n & -n 只會留下最低位的 1，與 n 不相等。

**原理推導：**

假設 n 的二進位為 (a10...0)，a 為高位，1 為最低位的 1，0...0 為後續 0。
- -n 的二進位為 (a'10...0)，a' 為 a 取反。
- n & -n 只保留最低位的 1。

**舉例說明：**

- n = 8 (1000)
- -n = -8 (1000)
- n & -n = 1000 & 1000 = 1000 == n

因此，若 n > 0 且 n & -n == n，則 n 為 2 的冪。

> [!TIP]
> 兩種解法皆為 O(1) 時間複雜度，適合面試與高效運算場景。

## 專案結構

```text
leetcode_231.sln
leetcode_231/
  leetcode_231.csproj
  Program.cs   # 主程式與解法
```

## 如何執行

1. 安裝 [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
2. 在專案根目錄執行：

   ```sh
   dotnet run --project leetcode_231/leetcode_231.csproj
   ```

3. 終端機將輸出各測試案例的判斷結果。

## 範例輸出

```text
n = 1, IsPowerOfTwo = True
n = 2, IsPowerOfTwo = True
n = 3, IsPowerOfTwo = False
n = 4, IsPowerOfTwo = True
n = 8, IsPowerOfTwo = True
n = 16, IsPowerOfTwo = True
n = 31, IsPowerOfTwo = False
n = 64, IsPowerOfTwo = True
n = 1024, IsPowerOfTwo = True
n = 0, IsPowerOfTwo = False
n = -2, IsPowerOfTwo = False
n = -8, IsPowerOfTwo = False
```

## 參考資料

- [LeetCode Discuss - Power of Two](https://leetcode.com/problems/power-of-two/solutions/)
- [位運算技巧教學](https://oi-wiki.org/lang/cpp/bit/)

- [LeetCode 力扣官方解答（含圖解）](https://leetcode.cn/problems/power-of-two/solutions/796201/2de-mi-by-leetcode-solution-rny3/?envType=daily-question&envId=2025-08-09)
- [二進位極簡思路](https://leetcode.cn/problems/power-of-two/solutions/12689/power-of-two-er-jin-zhi-ji-jian-by-jyd/?envType=daily-question&envId=2025-08-09)
- [嚴格證明與一行寫法](https://leetcode.cn/problems/power-of-two/solutions/2973442/yan-ge-zheng-ming-yi-xing-xie-fa-pythonj-h04o/?envType=daily-question&envId=2025-08-09)

> [!NOTE]
> 本專案僅供學習與參考，歡迎交流改進！
