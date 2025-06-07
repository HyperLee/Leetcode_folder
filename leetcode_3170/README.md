# leetcode_3170

## 題目：3170. 刪除星號以後字典序最小的字串

- [LeetCode 題目連結 (英文)](https://leetcode.com/problems/lexicographically-minimum-string-after-removing-stars/description/?envType=daily-question\&envId=2025-06-07)
- [LeetCode 題目連結 (中文)](https://leetcode.cn/problems/lexicographically-minimum-string-after-removing-stars/description/?envType=daily-question\&envId=2025-06-07)

### 題目描述 (繁體中文)

給定一個字串 s，可能包含任意數量的 '_' 字元。你的任務是移除所有的 '_' 字元。
當還有 '*' 存在時，執行以下操作：

- 刪除最左邊的 '_' 以及其左側最小的非 '_' 字元 (如果有多個最小字元，可以刪除任意一個)。
- 移除所有 '*' 後，回傳字典序最小的結果字串。

---

## 專案結構

- `Program.cs`：主程式與解題邏輯
- `leetcode_3170.csproj`：專案檔

## 解題思路

1. 建立 26 個堆疊 (對應 a~z)，用來記錄每個字母在字串中的索引。
2. 逐字元遍歷字串，遇到字母就將其索引壓入對應堆疊。
3. 遇到星號時，從 a~z 順序尋找最左側 (字典序最小) 的字母，將其標記為星號 (代表移除)。
4. 最後將所有星號過濾掉，組成新字串回傳。

## 執行方式

1. 請確認已安裝 .NET 8.0 SDK 以上版本。
2. 在專案根目錄下執行：

```sh
# 建構專案
 dotnet build
# 執行程式
 dotnet run --project leetcode_3170/leetcode_3170.csproj
```

## 範例輸出

```
輸入: a*b*c* => 輸出: 
輸入: abc*de*f* => 輸出: def
輸入: abac*ba* => 輸出: abcb
輸入: leetcode* => 輸出: leetode
輸入: a*bc*d*e* => 輸出: e
```

---

## 檔案說明

- `Program.cs`：包含 `ClearStars` 函式，依題意移除星號與左側最小字元，並有詳細註解與測試資料。

---

## 授權

本專案僅供學習與 LeetCode 題解參考用途。
