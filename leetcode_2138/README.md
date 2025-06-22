# Leetcode 2138: 將字串分組為固定長度

本專案為 Leetcode 第 2138 題「Divide a String Into Groups of Size k」的 C# 解題程式碼，並包含兩種不同的解法實作與詳細說明。

## 題目簡述

給定一個字串 `s`，請將其分組為每組長度為 `k` 的子字串。若最後一組不足 `k`，則以指定的填充字元 `fill` 補足。最終回傳分組後的字串陣列。

- Leetcode 題目連結：
  - [英文版](https://leetcode.com/problems/divide-a-string-into-groups-of-size-k/)
  - [中文版](https://leetcode.cn/problems/divide-a-string-into-groups-of-size-k/)

## 專案結構

- `Program.cs`：主程式與兩種解法
- `method1_doc.md`：方法一（陣列實作）說明
- `method2_doc.md`：方法二（List 實作）說明

## 方法一：陣列實作

- 先計算分組數量，建立固定長度的字串陣列。
- 使用 for 迴圈依序擷取每組子字串，若不足 `k` 則用 `PadRight` 補齊。
- 優點：記憶體分配明確，適合已知組數的情境。
- 缺點：需預先計算組數，彈性較低。

詳細說明請見 [`method1_doc.md`](./leetcode_2138/method1_doc.md)。

## 方法二：List 實作

- 使用 `List<string>` 動態儲存每組子字串。
- 以 while 迴圈每次擷取長度為 `k` 的子字串，最後一組若不足 `k` 則用填充字元補齊。
- 優點：彈性高，適合不確定組數的情境。
- 缺點：最後仍需轉為陣列，會有一次額外複製。

詳細說明請見 [`method2_doc.md`](./leetcode_2138/method2_doc.md)。

## 方法比較

| 項目         | 方法一（陣列）         | 方法二（List）         |
|--------------|------------------------|------------------------|
| 資料結構     | 陣列                   | List                   |
| 彈性         | 較低（需預先分配）     | 較高（動態擴充）       |
| 記憶體分配   | 一次分配               | 動態分配，最後轉陣列   |
| 適用場景     | 已知組數、效能優先     | 組數不確定、彈性需求   |
| 可讀性       | 清楚                   | 直觀                   |
| **時間複雜度** | O(n)                  | O(n)                  |
| **空間複雜度** | O(n)                  | O(n)                  |

> n 為字串長度。兩種方法皆需遍歷整個字串並儲存所有分組，故時間與空間複雜度相同。

## 執行方式

1. 需安裝 .NET 8.0 SDK 以上版本。
2. 在專案根目錄執行：

   ```zsh
   dotnet build
   dotnet run --project leetcode_2138/leetcode_2138.csproj
   ```

## 參考文件
- [方法一說明](./leetcode_2138/method1_doc.md)
- [方法二說明](./leetcode_2138/method2_doc.md)

---

如有建議或問題，歡迎提出！
