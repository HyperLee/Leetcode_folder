# leetcode_1432

## 題目說明

本專案為 LeetCode 第 1432 題「改變一個整數能得到的最大差值」的 C# 解題程式碼。

- 題目連結：[LeetCode 1432](https://leetcode.com/problems/max-difference-you-can-get-from-changing-an-integer/)
- 中文題目連結：[LeetCode 1432 (中文)](https://leetcode.cn/problems/max-difference-you-can-get-from-changing-an-integer/)

### 題目簡述

給定一個整數 num，對 num 進行兩次如下操作：

1. 選擇一個數字 x (0 <= x <= 9)。
2. 選擇另一個數字 y (0 <= y <= 9)，y 可以等於 x。
3. 將 num 中所有出現的 x 替換為 y。
   兩次操作分別得到 a 和 b，返回 a 和 b 的最大差值。
   注意：a 和 b 不能有前導零，且不能為 0。

## 專案結構

```
leetcode_1432.sln                  # Visual Studio 解決方案檔
leetcode_1432/
    leetcode_1432.csproj           # C# 專案檔
    Program.cs                     # 主程式與解題邏輯
    bin/                           # 編譯輸出目錄
    obj/                           # 中繼檔目錄
```

## 程式說明

### Program.cs

- `MaxDiff(int num)`: 暴力枚舉所有數字替換組合，找出最大與最小值，回傳差值。
- `MaxDiff2(int num)`: 使用貪心法，直接找出最有利於極大化與極小化的替換策略，效率更高。
- 兩個方法皆有詳細註解與解題說明，方便理解。

## 方法比較

| 方法           | 思路                                                                   | 時間複雜度                  | 空間複雜度                  | 優點                 | 缺點                 |
| ------------ | -------------------------------------------------------------------- | ---------------------- | ---------------------- | ------------------ | ------------------ |
| 方法一：MaxDiff  | 暴力枚舉所有數字替換組合，對每一種 x, y 組合都嘗試替換，找出最大與最小值                              | O (1)(數字位數固定，最多 100 組) | O (n)(n 為數字位數，需多次字串處理) | 實作直觀，容易理解，適合所有情境   | 效率較低，若數字位數很多時不建議使用 |
| 方法二：MaxDiff2 | 貪心法，直接針對最大化與最小化進行一次替換，最大值將第一個非 9 替換為 9，最小值將首位非 1 替換為 1 或其他位非 0 替換為 0 | O (n)(n 為數字位數)         | O (n)(只需兩個字串)          | 執行效率高，程式碼簡潔，適合大數位數 | 需理解貪心策略，邏輯較複雜      |

### 選用建議

- 若只需處理位數較小的整數，兩種方法皆可。
- 若需處理大數或追求效率，建議使用方法二 (MaxDiff2)。

## 執行方式

1. 請確認已安裝 .NET 8.0 SDK。
2. 在專案根目錄下執行：

```sh
# 建構專案
 dotnet build
# 執行專案
 dotnet run --project leetcode_1432/leetcode_1432.csproj
```

## Local Function (區域函式) 說明

本題兩種解法皆有使用 C# 的 local function (區域函式)，讓程式碼更精簡、易讀：

- **方法一 (MaxDiff)**：
  - 使用 `change` 區域函式，負責將 num 中所有 x 替換為 y，回傳替換後的字串。
  - 這樣可避免重複撰寫字串替換邏輯，提升可維護性。

- **方法二 (MaxDiff2)**：
  - 使用 `Replace` 區域函式，將字串 s 中所有 x 替換為 y。
  - 讓主流程更聚焦於貪心策略本身，細節交由區域函式處理。

### Local Function 優點

- 只在方法內部使用，避免污染全域命名空間。
- 可直接存取外部變數 (如 num)，方便實作閉包邏輯。
- 增加程式碼可讀性與模組化。

### 兩個 Local Function 差異比較

| 名稱      | 所屬方法     | 參數型態                         | 功能說明                         | 彈性 / 適用性              |
| ------- | -------- | ---------------------------- | ---------------------------- | --------------------- |
| change  | MaxDiff  | int x, int y                 | 將外部 num 轉字串，所有 x 替換為 y，回傳新字串 | 專為本題暴力枚舉設計，直接存取外部 num |
| Replace | MaxDiff2 | ref string s, char x, char y | 將傳入字串 s 中所有 x 替換為 y (原地修改)   | 汎用性高，可重複用於任意字串        |

#### 補充說明

- `change` 是閉包，直接存取外部變數 (num)，設計上只適合本題這種情境。
- `Replace` 則是傳入 ref 參數，對任意字串進行原地替換，結構較通用。
- 兩者皆善用 local function 的封裝特性，但 `Replace` 更偏向工具型、可重用性高。

## 參考資料

- [LeetCode 1432 題解 (中文)](https://leetcode.cn/problems/max-difference-you-can-get-from-changing-an-integer/solutions/514358/gai-bian-yi-ge-zheng-shu-neng-de-dao-de-0byhw/)

---

本專案僅供學習與 LeetCode 題目解法參考。
