# Leetcode 3403 - 從盒子中找出字典序最大的字串 I

## 題目連結

- [LeetCode 英文題目](https://leetcode.com/problems/find-the-lexicographically-largest-string-from-the-box-i/description/?envType=daily-question\&envId=2025-06-04)
- [LeetCode 中文題目](https://leetcode.cn/problems/find-the-lexicographically-largest-string-from-the-box-i/description/?envType=daily-question\&envId=2025-06-04)

## 題目描述 (繁體中文)

給定一個字串 `word` 和一個整數 `numFriends`。
Alice 為她的 numFriends 位朋友舉辦一個遊戲。遊戲有多輪，每一輪：

- 將 word 拆分成 numFriends 個非空字串，且每一輪的拆分方式都不能與之前完全相同。
- 將所有拆分後的字串放入盒子中。
  請找出所有輪結束後，盒子中字典序最大的字串。

## 解題方法

本專案提供三種解法，並詳細說明每種步驟與效率比較：

### 1. 枚舉法

**步驟說明**：

1. 計算所有長度為 n - numFriends + 1 的子字串 (n 為 word 長度)。
2. 從 word 的每個起始位置 i，取出長度為 n - numFriends + 1 的子字串 (若剩餘長度不足則取到結尾)。
3. 比較所有子字串，找出字典序最大的那一個。
4. 回傳該子字串。

**優缺點與複雜度**：

- 優點：實作簡單，邏輯直觀。
- 缺點：每次都要建立新子字串，對於長字串效能較差。
- 時間複雜度：O (n^2)
- 空間複雜度：O (n)

### 2. DFS + 回溯法

**步驟說明**：

1. 使用 DFS 遞迴枚舉所有將 word 拆分成 numFriends 個非空子字串的方式。
2. 每次拆分時，將目前拆分的所有子字串與目前最大字串做字典序比較，更新最大值。
3. 遞迴結束時回溯，移除最後一個拆分，確保狀態正確。
4. 最終回傳所有拆分方式中字典序最大的子字串。

**優缺點與複雜度**：

- 優點：能完整枚舉所有拆分組合，邏輯嚴謹。
- 缺點：組合數量呈指數成長，效能極差，僅適合小資料量或教學用途。
- 時間複雜度：O (2^n)
- 空間複雜度：O (n)

### 3. 雙指針法

**步驟說明**：

1. 先利用 LastSubstring 函式，找出字串 s 的所有子字串中，字典序最大的那一段 (即最靠後的子字串)。
2. 使用雙指針 i, j，逐步比較字元，若發現更大的子字串則更新 i。
3. 最後回傳最大子字串的前 n - numFriends + 1 個字元。

**優缺點與複雜度**：

- 優點：效率最高，適合大資料量，無需額外空間。
- 缺點：理解難度較高，程式碼較不直觀。
- 時間複雜度：O (n)
- 空間複雜度：O (1)

## 三種解法效率與複雜度比較

| 方法        | 時間複雜度  | 空間複雜度 | 適用情境       | 優點       | 缺點     |
| --------- | ------ | ----- | ---------- | -------- | ------ |
| 枚舉法       | O(n^2) | O(n)  | 一般情境       | 實作簡單     | 效率較低   |
| DFS + 回溯法 | O(2^n) | O(n)  | 小資料量 / 教學  | 完整枚舉所有組合 | 極易超時   |
| 雙指針法      | O(n)   | O(1)  | 大資料量 / 高效率 | 最快、最省空間  | 程式較難理解 |

## 執行方式

```bash
# 編譯
$ dotnet build

# 執行
$ dotnet run
```

## 範例輸出

```
word = leetcode, numFriends = 3
枚舉法結果: tcode
DFS+回溯法結果: tcode
雙指針法結果: tcode
```

## 檔案說明

- `Program.cs`：主程式與三種解法的實作

---

如需更多 Leetcode 題解，歡迎參考原始碼與註解！
