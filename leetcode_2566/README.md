# leetcode_2566 專案說明文件

## 專案簡介

本專案為 LeetCode 第 2566 題「替換一個數字後的最大差值」的 C# 解題程式碼，包含兩種不同的解法，並可直接於 VS Code 進行建構與偵錯。

## 專案結構

- `leetcode_2566.sln`：Visual Studio 解決方案檔案。
- `leetcode_2566/leetcode_2566.csproj`：C# 專案檔。
- `leetcode_2566/Program.cs`：主程式與解題邏輯。
- `.vscode/launch.json`、`.vscode/tasks.json`：VS Code 偵錯與建構設定檔。

## 題目描述

給定一個整數 num。你知道 Bob 會偷偷地將 10 個可能的數字 (0 到 9) 中的一個重新映射為另一個數字。

請回傳 Bob 透過恰好重新映射一個數字後，所能產生的最大值與最小值之間的差。

- 當 Bob 將數字 d1 重新映射為另一個數字 d2 時，會將 num 中所有出現的 d1 都替換為 d2。
- Bob 可以將一個數字映射為它自己，此時 num 不會改變。
- 為了取得最大值與最小值，Bob 可以分別選擇不同的數字進行映射。
- 重新映射後的結果可以包含前導零。

## 程式碼說明

### 解法一：暴力枚舉

- 枚舉所有數字 d1、d2 (d1 ≠ d2)，將 num 中所有 d1 替換為 d2，計算最大與最小值。
- 時間複雜度 O (100 \* n)，n 為位數。
- 對應函式：`MinMaxDifference(int num)`

### 解法二：貪心法

- 最大值：將第一個不是 9 的數字全部替換為 9。
- 最小值：將第一個數字全部替換為 0。
- 時間複雜度 O (n)。
- 對應函式：`MinMaxDifference2(int num)`
- 參考連結：[LeetCode 討論區解法二](https://leetcode.cn/problems/maximum-difference-by-remapping-a-digit/solutions/3690212/ti-huan-yi-ge-shu-zi-hou-de-zui-da-chai-3oyg4/?envType=daily-question\&envId=2025-06-14)

### 解法三：Java 寫法轉 C#(貪心法)

- 最大值：找到第一個不是 9 的數字，將其全部替換為 9。
- 最小值：將第一個數字全部替換為 0。
- 時間複雜度 O (n)。
- 對應函式：`MinMaxDifference3(int num)`
- 參考連結：[LeetCode 討論區解法三](https://leetcode.cn/problems/maximum-difference-by-remapping-a-digit/solutions/2119447/mei-ju-by-endlesscheng-slfa/?envType=daily-question\&envId=2025-06-14)

## 方法比較

| 比較面向  | 方法一：暴力枚舉          | 方法二：貪心法           | 方法三：Java 寫法轉 C#(貪心法) |
| ----- | ----------------- | ----------------- | -------------------- |
| 時間複雜度 | O (100 × n)，n 為位數 | O (n)，n 為位數       | O (n)，n 為位數          |
| 空間複雜度 | O (1)(只需常數額外變數)   | O (1)(只需常數額外變數)   | O (1)(只需常數額外變數)      |
| 可讀性   | 中等，邏輯直接但巢狀 for 迴圈 | 高，邏輯簡潔，流程易懂       | 高，邏輯簡潔，與方法二類似        |
| 可維護性  | 中等，若需調整映射規則需多處修改  | 高，結構單純，易於維護       | 高，結構單純，易於維護          |
| 易於擴充  | 低，若規則變複雜需重寫邏輯     | 中，適合單一最大 / 最小映射情境 | 中，適合單一最大 / 最小映射情境    |

- 方法一適合所有規則明確、位數不多的情境，能保證遍歷所有可能映射組合。
- 方法二、三適合規則單純、追求效率的場景，尤其是只需最大 / 最小化的題型。

## 執行方式

1. 於 VS Code 開啟本資料夾。
2. 按 F5 或點選「執行與偵錯」即可自動建構並執行主程式。
3. 可於 `Program.cs` 修改測試數字，觀察不同輸出結果。

## 範例輸出

```
num = 12345
解法一最大差值: 82005
解法二最大差值: 82005
```

## 相關連結

- [LeetCode 題目連結](https://leetcode.com/problems/maximum-difference-by-remapping-a-digit/)
- [LeetCode 中文題目連結](https://leetcode.cn/problems/maximum-difference-by-remapping-a-digit/)
