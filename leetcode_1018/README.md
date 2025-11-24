# leetcode_1018 — Binary Prefix Divisible By 5

LeetCode 題目 1018 — 可被 5 整除的二進位前綴 (Binary Prefix Divisible By 5)。本專案以 C# (.NET 8) 實作題解，包含簡短說明、範例、推演與公式導出。

---

## 問題描述 (Problem)

English:
> Given a binary array nums (0-indexed). We define x_i as the number whose binary representation is the subarray `nums[0..i]` (from most-significant-bit to least-significant-bit). Return an array of booleans `answer` where `answer[i]` is true if `x_i` is divisible by 5.

中文：
> 給定一個二進位陣列 `nums`（0-indexed）。我們定義 `x_i` 為一個數字，它的二進位表示為子陣列 `nums[0..i]`（從最高位到最低位）。回傳布林陣列 `answer`，使得 `answer[i]` 為 `true` 當且僅當 `x_i` 能被 5 整除。

鏈接：

- [LeetCode — Binary Prefix Divisible By 5](https://leetcode.com/problems/binary-prefix-divisible-by-5/)
- [LeetCode CN — Binary Prefix Divisible By 5](https://leetcode.cn/problems/binary-prefix-divisible-by-5/)

---

## 解題重點 (Approach)

- 直接計算 `x_i` 的數值會隨 i 指數成長，可能導致溢位或不必要的昂貴運算。
- 只需要關心每個前綴 `x_i` 能否被 5 整除，也就是 `x_i % 5 == 0`。
- 我們逐步維持一個餘數 `r = x_i % 5`，每次遇到新的一位 `bit`，只做更新：

  `r = (r * 2 + bit) % 5`

  觀念：由於二進位左移一位相當於乘以 2，加入新位則是加上當前 bit 的數值 (0 或 1)。

- 當且僅當 `r == 0` 時，對應的 prefix 可以被 5 整除。

- 補充說明：可以用十進位的類比去理解位元追加的運算過程。例如，對十進位數字 12 在右邊追加 3，可以寫成 12 ⋅ 10 + 3 = 123；對二進位也是類似，對 `110` 在右邊追加 `1`，可以寫成 `110 ⋅ 2 + 1 = 1101`（或用位運算 `(110 << 1) | 1 = 1101`）。注意本題 `nums` 可能很長，產生的二進位數 `x` 會非常大，但只需判斷 `x % 5 == 0`，因此可以在每次迭代時把 `x` 替換成 `x % 5`，也就是使用 `r = (r * 2 + bit) % 5`，以避免溢位並降低運算成本。

---

## 公式推導 (Derivation)

- 假設前綴 `x_{i-1}` 的值可以表示為整數，且其二進位為 `nums[0..i-1]`。
- 當新增一位 `bit`（`nums[i]`），新的前綴 `x_i` 等於 `x_{i-1}` 左移一位加上 `bit`：

  x_i = x_{i-1} * 2 + bit

- 我們只關心對 5 的餘數，因此：

  `r_i = x_i % 5 = (x_{i-1} * 2 + bit) % 5`

- 由同餘算術 (mod) 性質，可改寫為：

  `r_i = ((x_{i-1} % 5) * 2 + bit) % 5 = (r_{i-1} * 2 + bit) % 5`

- 因此只需要追蹤 `r`，不需要記住整個整數值 `x_i`。

---

## 範例推演 (Step-by-step)

1. 範例 1：`nums = [1, 0, 1]`

   - i = 0: bit = 1
     - r = (0 * 2 + 1) % 5 = 1 -> `false` (1 被 5 除，餘數為 1)
   - i = 1: bit = 0
     - r = (1 * 2 + 0) % 5 = 2 -> `false`
   - i = 2: bit = 1
     - r = (2 * 2 + 1) % 5 = 5 % 5 = 0 -> `true`

   最終輸出: `[false, false, true]`。

2. 範例 2：`nums = [0, 0]`

   - i = 0: bit = 0
     - r = 0 -> `true` (0 能被 5 整除)
   - i = 1: bit = 0
     - r = 0 -> `true`

   最終輸出: `[true, true]`。

3. 範例 3：`nums = [1,1,1,1,1]`

   - i = 0: r=1 => false
   - i = 1: r=(1*2+1)%5 = 3 => false
   - i = 2: r=(3*2+1)%5 = 2 => false
   - i = 3: r=(2*2+1)%5 = 0 => true
   - i = 4: r=(0*2+1)%5 = 1 => false

   最終輸出: `[false, false, false, true, false]`。

---

## 複雜度分析

- 時間複雜度：O(n)，n 為 `nums` 的長度（逐一處理每個元素）。
- 空間複雜度：O(n)，返回結果的空間；若只考慮額外工作空間則 O(1)。

---

## 實作細節 (Implementation)

- 主要實作位於 `leetcode_1018/leetcode_1018/Program.cs`，核心方法：`PrefixesDivBy5(int[] nums)`。
- 程式中使用 `List<bool>` 儲存結果，並透過 `r = (r * 2 + bit) % 5` 維持餘數。
- `Main` 已包含數組測試範例並印出結果。

## 執行方式 (Run)

要在本機上執行範例：

```bash
# 進到專案目錄
cd leetcode_1018

# 建立/建置
dotnet build

# 執行
dotnet run --project leetcode_1018/leetcode_1018.csproj
```

---

## 常見注意事項 (Notes)

- 對於數列一開始的 `0`，`x_i` 會是 0，因此該位置結果為 `true`（0 可以被 5 整除）。
- 使用 `mod` 來維持餘數能有效避免整數溢位，也能將時間與空間降低到最小需要量。

---

## 範例輸出 (Sample Output)

輸入：`[1,0,1]` -> 輸出：`[false, false, true]`

---

如果你想，我可以：

- 將解法抽成 `Solution` 類別，並新增單元測試。
- 加入更多範例或 IRL (即時) benchmark。

歡迎告訴我想要的下一步！
