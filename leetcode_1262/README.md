# 1262. 可被三整除的最大和 (Greatest Sum Divisible by Three)

問題：給定一個整數陣列，請找出一個子集合，使其總和能被 3 整除，且總和盡可能大，回傳該最大總和。

LeetCode:

- 英文: [Greatest Sum Divisible by Three](https://leetcode.com/problems/greatest-sum-divisible-by-three)
- 中文（力扣）: [可被三整除的最大和](https://leetcode.cn/problems/greatest-sum-divisible-by-three)

## 🔍 問題摘要

給定一個整數陣列 nums，從中選出某些元素，使選出元素的總和能被 3 整除（sum % 3 == 0），並且該總和為最大，回傳這個最大總和。

## 💡 關鍵觀察與策略（貪心 + 正向思維）

1. 若數字能被 3 整除（`num % 3 == 0`），就可以直接加入總和，因為這類數字不會影響總和的 mod 3，將它們放在 `a`（或程式碼中的 `v[0]`）群組。
2. 剩餘數字依 `num % 3` 分成兩組：
   - `b`（`num % 3 == 1`），
   - `c`（`num % 3 == 2`）。
   我們必須從 `b` 與 `c` 中各自選出若干個數字，使得選取後的總和 mod 3 為 0。

3. 設 `cnt_b` 與 `cnt_c` 為從 `b`、`c` 選取的數量，則這些被選中數字對 mod 3 的貢獻為：

    `(cnt_b * 1 + cnt_c * 2) % 3`，等價於 `(cnt_b - cnt_c) % 3`。

    以下為補充說明（模 3 同餘的推導與直觀理解）：

    - 嚴謹模組算術推導：
       - 在 mod 3 的情況下，2 與 −1 同餘，因為 2 ≡ −1 (mod 3)。
       - 將 2·cnt_c 改寫為 −cnt_c 得：cnt_b + 2·cnt_c ≡ cnt_b − cnt_c (mod 3)。
       - 又或觀察兩者差值為 3·cnt_c，所以差為 3 的整數倍，代表兩者在 mod 3 下同餘。

    - 直觀說明：在「以 3 為模」的世界裡，乘以 2 等同於乘以 −1（因為 2 = 3 − 1），因此把 cnt_c 每個元素算作 +2，會等價於從總和中減去 cnt_c 的數量；兩者餘數相同。

    - 範例驗證：
       - cnt_b = 4, cnt_c = 3：左邊 4 + 3*2 = 10 → 10 % 3 = 1；右邊 4 − 3 = 1 → 1 % 3 = 1。
       - cnt_b = 1, cnt_c = 2：左邊 1 + 2*2 = 5 → 5 % 3 = 2；右邊 1 − 2 = −1 → −1 ≡ 2 (mod 3) → 餘數 2。

    - 實作小提醒：數學上的同餘對所有整數成立，但某些程式語言對負數 `%` 的行為不同（例如 `-1 % 3` 的結果在語言間可能不同），在程式實作時請留意語言規格以避免誤解。

    因此我們需要找到 `cnt_b`, `cnt_c` 使 `(cnt_b - cnt_c) % 3 == 0`。

4. 優化觀察：如果某一群組最多有 `m` 個元素，選取數量若 `<= m - 3`，可以再多選 3 個元素而不改變 mod 3，因此在每一群組中，我們只需考慮以下三種選取數量：`{m - 2, m - 1, m}`。而對兩群組各自皆有 3 個選擇，共需枚舉 3×3=9 種組合。

5. 由於要使總和最大，我們會以貪心方式先從 `b` 與 `c` 各自取最大的元素。因此先將 `b`、`c` 依數值做降冪排序，並在上述 9 種組合中檢查是否滿足模 3 約束，並維護最佳總和。

## ⌛ 複雜度

- **時間複雜度（Time）:** O(n log n)，主要成本為排序 `b` 與 `c`；其餘操作為線性時間。
- **空間複雜度（Space）:** O(n)，需額外儲存分組陣列（`v[0]`, `v[1]`, `v[2]`）。

## ✅ 範例說明流程

以 nums = [3, 6, 5, 1, 8] 為例：

1. 按模 3 分群：
   - a (`v[0]`) = [3, 6]
   - b (`v[1]`) = [1]
   - c (`v[2]`) = [5, 8]

2. 先將 a 的數全部計入：3 + 6 = 9。

3. 將 b 與 c 做降冪排序，並在 `cnt_b`、`cnt_c` 的 9 種合法組合中選取（只需考慮每組的 `{m - 2, m - 1, m}`），滿足 `(cnt_b - cnt_c) % 3 == 0`。

4. 選出使總和最大的組合之後，最終回傳 18（即包含 a 的所有元素，以及選自 b/c 的最大組合）。

## 🔧 範例使用

從 `Program.cs` 範例呼叫：

```csharp
int[] test1 = { 3, 6, 5, 1, 8 };
Console.WriteLine(solution.MaxSumDivThree(test1));  // 18

int[] test2 = { 4 };
Console.WriteLine(solution.MaxSumDivThree(test2));  // 0

int[] test3 = { 1, 2, 3, 4, 4 };
Console.WriteLine(solution.MaxSumDivThree(test3));  // 12
```

## 參考資料

- LeetCode 官方題解
- 力扣中文說明: [可被三整除的最大和（解法）](https://leetcode.cn/problems/greatest-sum-divisible-by-three/solutions/2309835/ke-bei-san-zheng-chu-de-zui-da-he-by-lee-cvzo/)

---
如果你想要逐步追蹤枚舉過程，請查看 `Program.cs` 的邏輯，並執行程式觀察範例輸出。
