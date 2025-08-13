# leetcode_326: Power of Three

> 判斷一個整數是否為 3 的冪

---

## 專案簡介

本專案為 LeetCode 第 326 題「Power of Three」的 C# 解法，並提供詳細解題思路與範例測試。適合用於學習數學性質判斷、基礎演算法設計與 C# 語法。

## 題目說明

[LeetCode 326. Power of Three](https://leetcode.com/problems/power-of-three/)

> 給定一個整數 `n`，判斷其是否為 3 的冪。
>
> - 一個整數 n 是 3 的冪，代表存在一個整數 x 使得 n == 3^x。
> - 範圍：-2^31 <= n <= 2^31 - 1

## 解法說明

:::info
本專案採用「試除法」判斷 n 是否為 3 的冪。
:::

### 方法一：試除法

1. 只要 n 不為 0 且能被 3 整除，就持續將 n 除以 3。
2. 最終若 n 能化簡為 1，則 n 為 3 的冪。
3. 若 n 為 0 或負數，或無法被 3 整除，則 n 不是 3 的冪。

#### 程式碼片段

```csharp
public static bool IsPowerOfThree(int n)
{
    while (n != 0 && n % 3 == 0)
    {
        n /= 3;
    }
    return n == 1;
}
```

### 時間複雜度
- O(log₃ n)：每次除以 3，最多 log₃ n 次。

### 空間複雜度
- O(1)：僅使用常數空間。

## 執行方式

1. 確認已安裝 [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
2. 於專案根目錄執行：

```sh
# 建置專案
$ dotnet build

# 執行程式
$ dotnet run --project leetcode_326/leetcode_326.csproj
```

## 範例輸出

```text
n = 27, IsPowerOfThree = True
n = 0, IsPowerOfThree = False
n = 9, IsPowerOfThree = True
n = 45, IsPowerOfThree = False
n = 1, IsPowerOfThree = True
n = 3, IsPowerOfThree = True
n = 81, IsPowerOfThree = True
n = 10, IsPowerOfThree = False
```

## 目錄結構

```text
leetcode_326.sln
leetcode_326/
├── leetcode_326.csproj
├── Program.cs
└── ...
```

## 參考連結
- [LeetCode 題目說明](https://leetcode.com/problems/power-of-three/)
- [LeetCode 中文說明](https://leetcode.cn/problems/power-of-three/)

:::tip
如需更多 LeetCode 題解，歡迎參考本專案其他資料夾！
:::
