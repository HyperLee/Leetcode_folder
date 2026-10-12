# LeetCode 2897：對陣列執行操作使平方和最大

![.NET 10](https://img.shields.io/badge/.NET-10-512BD4) ![C%23](https://img.shields.io/badge/language-C%23-239120)

本專案以兩種 C# 實作示範「位元計數＋貪心集中」：`MaxSum` 逐個建立數字，`MaxSum2` 逐個分配位元。執行時直接跑固定案例，顯示 Expected、Actual、PASS／FAIL，並確認輸入未被修改。

- [題目與限制](#題目與限制)
- [解題出發點與正確性](#解題出發點與正確性)
- [方法一：逐個建立數字-maxsum](#方法一逐個建立數字-maxsum)
- [方法二：逐個分配位元-maxsum2](#方法二逐個分配位元-maxsum2)
- [建置與執行](#建置與執行)
- [固定測試與實際輸出](#固定測試與實際輸出)

## 題目與限制

題目：**Apply Operations on Array to Maximize Sum of Squares**。

來源：[LeetCode 英文題目](https://leetcode.com/problems/apply-operations-on-array-to-maximize-sum-of-squares/description/)、[LeetCode 中文題目](https://leetcode.cn/problems/apply-operations-on-array-to-maximize-sum-of-squares/description/)。

給定索引從 0 開始的正整數陣列 `nums` 與正整數 `k`。可以任意次選擇不同索引 `i`、`j`，同步更新：

```text
nums[i] = oldNums[i] AND oldNums[j]
nums[j] = oldNums[i] OR  oldNums[j]
```

兩個新值都以操作前的值計算，不能先改 `nums[i]` 再拿更新後的值算 OR。從最終陣列選出恰好 `k` 個元素，最大化它們的平方和；回傳最大值對 `1000000007` 取模的結果。操作後可以產生 0，雖然原始輸入皆為正整數。

官方限制：

- `1 <= k <= nums.length <= 100000`
- `1 <= nums[i] <= 1000000000`

兩個方法都假設呼叫者提供符合題目限制的輸入，不另訂無效輸入處理規則。

### 官方範例

| nums | k | 結果 | 說明 |
|---|---:|---:|---|
| `[2,6,5,8]` | 2 | 261 | 可形成 `[0,6,0,15]`，選擇 15 與 6：`225 + 36 = 261` |
| `[4,5,4,7]` | 3 | 90 | 原陣列已可選擇 7、5、4：`49 + 25 + 16 = 90` |

範例一的實際操作：

```text
[2,6,5,8]
選 i=0, j=3：2 AND 8 = 0，2 OR 8 = 10
[0,6,5,10]
選 i=2, j=3：5 AND 10 = 0，5 OR 10 = 15
[0,6,0,15]
```

演算法不必輸出這些操作或真的修改陣列，只需要計算最大平方和。

## 解題出發點與正確性

### 1. 操作保留每個位元的 1 數量

觀察單一位元的四種情況：

| 原始兩個位元 | AND | OR | 操作前後的 1 數量 |
|---|---:|---:|---:|
| 0、0 | 0 | 0 | 0 |
| 0、1 | 0 | 1 | 1 |
| 1、0 | 0 | 1 | 1 |
| 1、1 | 1 | 1 | 2 |

因此第 `bit` 位的 1 總數不變，可以用 `cnt[bit]` 表示這個位元有多少份可分配。OR 把不同位元集中到同一個數字；AND 保留兩個數字共同擁有的位元，所以重複的 1 不會憑空消失。

這也說明為什麼不能只做一次所有元素的 OR，然後複製 `k` 次：若某個位元只有一份，就不能出現在兩個選取的數字中。

因為 `1000000000 < 2^30`，只需要檢查位元 0 到 29。原始元素之間的 AND／OR 不會創造原本不存在的更高位元，因此重建數字最多為 `2^30 - 1 = 1073741823`。

### 2. 為什麼集中位元能增加平方和？

對兩個數字 `a`、`b`，令：

- `c = a AND b`：共同位元對應的數值。
- `x`：只在 `a` 出現的位元對應的數值。
- `y`：只在 `b` 出現的位元對應的數值。

這三部分的位元互不重疊，因此 `a = c+x`、`b = c+y`，OR 的數值為 `c+x+y`。一次操作前後，兩個元素的完整平方和差為：

```text
c² + (c+x+y)² - [(c+x)² + (c+y)²] = 2xy >= 0
```

例如 2 與 8 的位元互不重疊，原本平方和是 `4+64=68`；集中後變成 0 與 10，平方和是 100，增加 `2×2×8=32`。

這個式子揭示集中位元的方向，但題目只選 `k` 個元素；還需要證明選取的前 `k` 個數字可以同時達到最大值。

### 3. 前面的數字應取得所有還有庫存的位元

把 `cnt[bit]` 記為 `c_bit`，定義第 `t` 個目標數字（`t` 從 0 開始）：

```text
v_t = 所有滿足 c_bit > t 的位元權重 2^bit 之和
```

也就是每個位元都放到最前面的 `c_bit` 個數字中。前面的數字包含後面數字的所有位元，因此 `v_0 >= v_1 >= ...`。

為什麼這能達到最大平方和？將每個選取數字的平方展開：

```text
v² = 各個已設定的位元權重平方之和
   + 2 × 各對不同位元權重乘積之和
```

在任意選取的 `k` 個元素中：

- 位元 `bit` 最多出現 `min(c_bit, k)` 次。
- 位元 `p` 與 `q` 同時出現，最多有 `min(c_p, c_q, k)` 次。

所有權重及交叉項都是正數。如果每個位元都分配給前面的數字，單一位元的出現次數及任意兩個位元的重疊次數，都恰好達到上述上限。因此這個排列同時最大化所有平方展開項，前 `k` 個數字的平方和就是全域最大值。

### 4. 這個排列真的可以透過合法操作得到嗎？

可以。從所有尚未固定的元素中挑一個作為累積元素，依次與其他元素進行 AND／OR，讓累積元素接收 OR 結果。掃過全部剩餘元素後，累積元素擁有所有仍存在的位元，因此成為 `v_0`。

固定這個元素後，不再對它操作。由於位元數量守恆，剩餘陣列的每個非零位元計數都少一份。在剩餘元素重複相同過程，即可建立 `v_1`、`v_2` 等目標數字。

這是可達性的證明，不是程式採用的操作模擬。程式直接利用計數重建前 `k` 個結果，省去逐對操作。

## 方法一：逐個建立數字 MaxSum

介面：`public int MaxSum(IList<int> nums, int k)`。

### 設計與變數

| 變數 | 用途 |
|---|---|
| `MOD` | 答案取模基數 1000000007 |
| `cnt[30]` | 每個位元尚未使用的 1 數量 |
| `i`（統計階段） | 原始數字目前檢查的位元，0 到 29 |
| `i`（重建階段） | 現在建立第幾個選取數字，0 到 k−1 |
| `j` | 重建時檢查的位元，29 到 0 |
| `num` | 本輪建立的 `long` 數字 |
| `result` | 已建立數字的平方和餘數 |

先對每個輸入元素檢查 30 個位元，以 `(num & (1 << i)) != 0` 判斷該位元是否存在。這裡的 `num` 是 `foreach` 的輸入元素，與後面重建用的 `long num` 處於不同作用域。

接著重複 `k` 輪，每輪把 `num` 重設為 0。只要 `cnt[j] > 0`，便執行 `num |= (1L << j)`，再把 `cnt[j]` 減一。減一代表把該位元的一份庫存用在目前數字，不能在後續輪次重複使用同一份。

一輪掃完才平方並加入答案。即使所有計數都已耗盡，也繼續建立剩餘數字為 0，滿足恰好選取 `k` 個元素的要求。由高位走到低位只是現有實作的順序；因為每個仍有庫存的位元都會被取用，改成由低位到高位也會得到相同數字。

### 範例演示：nums = [2,6,5,8]，k = 2

僅列出位元 0 到 3，其餘計數皆為 0：

| 輸入 num | 二進位 | 貢獻的位元 |
|---:|---|---|
| 2 | `0010` | 位元 1 |
| 6 | `0110` | 位元 1、2 |
| 5 | `0101` | 位元 0、2 |
| 8 | `1000` | 位元 3 |

初始計數 `[cnt[0],cnt[1],cnt[2],cnt[3]] = [1,2,2,1]`。

| 外層 i | j | 使用前 cnt[j] | 動作 | 動作後 num | 動作後計數 [0,1,2,3] |
|---:|---:|---:|---|---:|---|
| 0 | 29 到 4 | 0 | 跳過 | 0 | `[1,2,2,1]` |
| 0 | 3 | 1 | 設定 8，扣一份 | 8 | `[1,2,2,0]` |
| 0 | 2 | 2 | 設定 4，扣一份 | 12 | `[1,2,1,0]` |
| 0 | 1 | 2 | 設定 2，扣一份 | 14 | `[1,1,1,0]` |
| 0 | 0 | 1 | 設定 1，扣一份 | 15 | `[0,1,1,0]` |
| 1 | 29 到 3 | 0 | 新一輪 num=0，跳過 | 0 | `[0,1,1,0]` |
| 1 | 2 | 1 | 設定 4，扣一份 | 4 | `[0,1,0,0]` |
| 1 | 1 | 1 | 設定 2，扣一份 | 6 | `[0,0,0,0]` |
| 1 | 0 | 0 | 跳過 | 6 | `[0,0,0,0]` |

| 輪次結束 | num | result 更新 |
|---|---:|---|
| i=0 | 15 | `(0+15×15)%MOD = 225` |
| i=1 | 6 | `(225+6×6)%MOD = 261` |

## 方法二：逐個分配位元 MaxSum2

介面：`public int MaxSum2(IList<int> nums, int k)`。

### 設計與變數

先統計相同的 `cnt[30]`，再建立 `long[] values = new long[k]`。與方法一不同，這裡不消耗計數；外層逐個處理位元，內層把該位元分配給前面的數字。

| 變數 | 用途 |
|---|---|
| `bit` | 當前處理的位元，0 到 29 |
| `cnt[bit]` | 此位元在原始輸入中的總份數，分配時保持不變 |
| `count` | `Math.Min(cnt[bit], k)`，選取的數字最多能用幾份 |
| `index` | 要設定位元的 `values` 索引，0 到 count−1 |
| `values[index]` | 重建的第 index 個選取數字，型別為 long |
| `value`、`result` | 最後逐個平方累加，以及目前平方和餘數 |

對每個 `bit`，把 `1L << bit` 用 OR 設定到 `values[0]` 到 `values[count-1]`。每個位元都使用相同的前綴分配方向，便可讓不同位元盡量重疊。

若 `cnt[bit] > k`，多出的份數仍存在於未選取元素，不能解讀成從原陣列刪除它們。計算答案只需要前 `k` 個數字，所以不用建立完整 `n` 個元素。最後遍歷 `values`，逐個平方、累加、取模。

### 範例演示：nums = [2,6,5,8]，k = 2

初始 `cnt = [1,2,2,1,...]`，`values = [0,0]`。

| bit | cnt[bit] | count | index 的執行順序 | 設定的權重 | 完成本位元後 values |
|---:|---:|---:|---|---:|---|
| 0 | 1 | 1 | 0 | 1 | `[1,0]` |
| 1 | 2 | 2 | 0、1 | 2 | `[3,2]` |
| 2 | 2 | 2 | 0、1 | 4 | `[7,6]` |
| 3 | 1 | 1 | 0 | 8 | `[15,6]` |
| 4 到 29 | 0 | 0 | 不執行內層迴圈 | — | `[15,6]` |

例如 `bit=2` 時，先讓 `values[0]` 從 3 變成 7，再讓 `values[1]` 從 2 變成 6。所有位元分配完才進入平方累加：

| value | 更新前 result | 更新後 result |
|---:|---:|---:|
| 15 | 0 | 225 |
| 6 | 225 | 261 |

兩種方法重建的數字相同，差別是「先完成一個數字」與「先分配完一個位元」，不是不同的最佳化原理。

## 複雜度與數值安全

令 `n = nums.Count`，位元寬度 `B = 30`。

| 方法 | 統計 | 重建與加總 | 總時間 | 額外空間 |
|---|---|---|---|---|
| MaxSum | O(30n) | O(30k) | O(30n + 30k) | O(30) |
| MaxSum2 | O(30n) | O(30 + Σ min(cnt[bit],k) + k)，上界 O(30k) | O(30n + 30k) | O(30+k) |

位元寬度固定時，時間可寫成 `O(n+k)`；方法一空間為常數，方法二空間為 `O(k)`。表格不包含測試入口為了保護輸入而建立的複本。

- 最大重建數字為 `1073741823`，其平方為 `1152921502459363329`，超過 `int`，但小於 `long.MaxValue = 9223372036854775807`。
- 方法一的 `num`、方法二的 `values` 元素與 `value` 都是 `long`，確保乘法在 `long` 中進行；先以 `int` 相乘再轉型會太晚。
- 每次更新前 `result < MOD`，所以單次 `result + value²` 仍在 `long` 範圍內。完整平方和卻可能超過 `long`，不能等最後才取模。
- `(a+b)%MOD` 與 `((a%MOD)+(b%MOD))%MOD` 相等，因此每輪取模不改變最後餘數。
- 取模不保留大小順序。貪心必須根據原始平方和決定最佳排列，不能比較餘數來選「最大答案」。

## 建置與執行

需要 .NET 10 SDK。從本工作目錄（包含 `leetcode_2897/` 子目錄的這一層）依序執行：

```sh
dotnet restore leetcode_2897/leetcode_2897.csproj
dotnet build leetcode_2897/leetcode_2897.csproj --no-restore
dotnet build leetcode_2897/leetcode_2897.csproj --no-restore -t:Rebuild -p:GenerateDocumentationFile=true -p:TreatWarningsAsErrors=true
dotnet run --project leetcode_2897/leetcode_2897.csproj --no-build
dotnet format leetcode_2897/leetcode_2897.csproj --verify-no-changes --no-restore
git diff --check
```

`--no-build` 執行最後一次建置的程式，因此修改原始碼後必須先重新建置。第三個指令會重新編譯並檢查 XML 文件，將警告視為錯誤。formatter 指令只驗證格式。

本專案沒有測試框架；固定測試由 `Main` 執行，不需輸入參數。任一答案不符或輸入被修改，程式會以退出碼 1 結束；全部通過為 0。此層沒有專案或方案檔，請使用明確的巢狀專案路徑，不以此層的 `dotnet test` 作為驗證指令。

驗證結果：restore、一般 build、嚴格 XML rebuild、範例執行與格式檢查皆成功；兩次建置皆為 0 警告、0 錯誤，`git diff --check` 無診斷。以下輸出取自建置後的實際執行。

## 固定測試與實際輸出

兩種方法各使用獨立輸入複本，呼叫後另與原始案例比較，避免兩種方法互相影響並檢查不修改輸入的契約。答案檢查及輸入保護檢查各有 24 次。

| 輸出案例名稱 | 情境 | nums | k | Expected |
|---|---|---|---:|---:|
| Official example 1 | 官方範例一 | `[2,6,5,8]` | 2 | 261 |
| Official example 2 | 官方範例二 | `[4,5,4,7]` | 3 | 90 |
| Minimum input | 最小輸入 | `[1]` | 1 | 1 |
| Choose one | k=1 | `[2,6,5,8]` | 1 | 225 |
| Choose all | k=n | `[2,6,5,8]` | 4 | 261 |
| Repeated bits | 重複位元 | `[7,7,7]` | 2 | 98 |
| Disjoint bits | 互不重疊位元 | `[1,2,4,8]` | 2 | 225 |
| Unequal bit counts | 不同位元計數 | `[3,5,6]` | 2 | 98 |
| Bits exhausted | 耗盡後補零 | `[1,2]` | 2 | 9 |
| Highest used bit | 位元 29 與平方取模 | `[536870912,1]` | 1 | 207840944 |
| Large squares | 大平方與累加 | `[1000000000,1000000000]` | 2 | 98 |
| Maximum length | 最大長度與 k | 100000 個 `1000000000` | 100000 | 4900000 |

Expected 是固定常數，沒有用兩個待測方法之一產生。較大的答案也可從數學式檢查：`(536870913²)%MOD = 207840944`；`(1000000000²)%MOD = 49`，所以兩份為 98，100000 份為 4900000。

### 實際執行輸出

```text
Official example 1 | MaxSum | Expected: 261 | Actual: 261 | PASS | Input unchanged: PASS
Official example 1 | MaxSum2 | Expected: 261 | Actual: 261 | PASS | Input unchanged: PASS
Official example 2 | MaxSum | Expected: 90 | Actual: 90 | PASS | Input unchanged: PASS
Official example 2 | MaxSum2 | Expected: 90 | Actual: 90 | PASS | Input unchanged: PASS
Minimum input | MaxSum | Expected: 1 | Actual: 1 | PASS | Input unchanged: PASS
Minimum input | MaxSum2 | Expected: 1 | Actual: 1 | PASS | Input unchanged: PASS
Choose one | MaxSum | Expected: 225 | Actual: 225 | PASS | Input unchanged: PASS
Choose one | MaxSum2 | Expected: 225 | Actual: 225 | PASS | Input unchanged: PASS
Choose all | MaxSum | Expected: 261 | Actual: 261 | PASS | Input unchanged: PASS
Choose all | MaxSum2 | Expected: 261 | Actual: 261 | PASS | Input unchanged: PASS
Repeated bits | MaxSum | Expected: 98 | Actual: 98 | PASS | Input unchanged: PASS
Repeated bits | MaxSum2 | Expected: 98 | Actual: 98 | PASS | Input unchanged: PASS
Disjoint bits | MaxSum | Expected: 225 | Actual: 225 | PASS | Input unchanged: PASS
Disjoint bits | MaxSum2 | Expected: 225 | Actual: 225 | PASS | Input unchanged: PASS
Unequal bit counts | MaxSum | Expected: 98 | Actual: 98 | PASS | Input unchanged: PASS
Unequal bit counts | MaxSum2 | Expected: 98 | Actual: 98 | PASS | Input unchanged: PASS
Bits exhausted | MaxSum | Expected: 9 | Actual: 9 | PASS | Input unchanged: PASS
Bits exhausted | MaxSum2 | Expected: 9 | Actual: 9 | PASS | Input unchanged: PASS
Highest used bit | MaxSum | Expected: 207840944 | Actual: 207840944 | PASS | Input unchanged: PASS
Highest used bit | MaxSum2 | Expected: 207840944 | Actual: 207840944 | PASS | Input unchanged: PASS
Large squares | MaxSum | Expected: 98 | Actual: 98 | PASS | Input unchanged: PASS
Large squares | MaxSum2 | Expected: 98 | Actual: 98 | PASS | Input unchanged: PASS
Maximum length | MaxSum | Expected: 4900000 | Actual: 4900000 | PASS | Input unchanged: PASS
Maximum length | MaxSum2 | Expected: 4900000 | Actual: 4900000 | PASS | Input unchanged: PASS
Summary: 24/24 checks passed.
Input preservation: 24/24 checks passed.
```

## 專案結構

```text
leetcode_2897/Program.cs             題目 XML、固定測試與兩種解法
leetcode_2897/leetcode_2897.csproj    .NET 10 主控台專案
.vscode/                            既有建置及除錯設定
docs/readme-template.md             初次建立 README 的範本
README.md                          題目、解法與驗證記錄
```

原始題目 XML 保持完整；入口執行說明另置於 `<remarks>`，解法教學置於方法 XML 與關鍵流程註解。
