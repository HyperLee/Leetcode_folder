# Leetcode 007 - 整數反轉

## 題目描述

給定一個有符號 32 位元整數 x，請將 x 的數字反轉後回傳。
如果反轉後的數值超出 32 位元有符號整數範圍 [-2^31, 2^31 - 1]，則回傳 0。
假設執行環境不允許你儲存 64 位元整數 (有符號或無符號)。

- [Leetcode 題目連結 (英文)](https://leetcode.com/problems/reverse-integer/description/)
- [Leetcode 題目連結 (中文)](https://leetcode.cn/problems/reverse-integer/description/)

## 解題思路

本題不允許使用輔助堆疊或陣列，需以數學方式逐位反轉整數：

1. 透過 x % 10 取得末尾數字 digit。
2. x /= 10 去掉末尾數字。
3. 將 digit 加入 rev 的末尾 rev = rev * 10 + digit。
4. 每次操作前檢查 rev 是否會溢位，若會則回傳 0。

## 範例程式碼片段

```csharp
public static int Reverse(int x)
{
    int rev = 0;
    while (x != 0)
    {
        if (rev < int.MinValue / 10 || rev > int.MaxValue / 10)
        {
            return 0;
        }
        int digit = x % 10;
        x /= 10;
        rev = rev * 10 + digit;
    }
    return rev;
}
```

## 反轉過程中的溢位檢查說明

在反轉整數的過程中，必須檢查目前反轉後的數字 rev 是否即將發生溢位。C# 的 int 型別有固定範圍（-2,147,483,648 到 2,147,483,647），如果 rev 超出這個範圍，會產生錯誤結果。

程式中使用：

```csharp
if (rev < int.MinValue / 10 || rev > int.MaxValue / 10)
{
    return 0;
}
```

這個判斷式會在每次 while 迴圈中執行。將 int 的最小值和最大值分別除以 10，是因為下一步會將 rev 乘以 10 並加上一個數字。如果 rev 已經小於 int.MinValue / 10 或大於 int.MaxValue / 10，再乘以 10 就一定會超出 int 的範圍，因此這時直接回傳 0，表示反轉後的結果不合法。

這樣的檢查可以有效避免記憶體溢位的問題，是處理整數反轉題目時非常重要的細節。

## 時間複雜度與空間複雜度

- 時間複雜度：O(log₁₀x)，x 的位數決定 for 迴圈次數。
- 空間複雜度：O(1)，僅使用常數額外變數。

## 執行方式

```sh
# 進入專案資料夾
cd leetcode_007
# 建構並執行
dotnet run --project leetcode_007/leetcode_007.csproj
```
