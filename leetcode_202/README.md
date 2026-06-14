# 202. Happy Number

使用 C# 與 .NET 10 實作 LeetCode 202「Happy Number（快樂數）」的兩種解法，並在 `Main` 入口直接提供可執行的示範資料，方便快速驗證結果與比對不同解題策略。

## 題目連結

- LeetCode: https://leetcode.com/problems/happy-number/description/
- LeetCode 中文: https://leetcode.cn/problems/happy-number/description/

## 題目說明

給定一個整數 `n`，請判斷它是否為快樂數。

所謂快樂數，指的是對同一個數字反覆做以下操作後，最終可以得到 `1`：

1. 取出目前數字的每一位。
2. 將每一位數字平方。
3. 把所有平方值加總，形成新的數字。
4. 持續重複，直到結果變成 `1`，或進入一個不包含 `1` 的循環。

如果最終會得到 `1`，回傳 `true`；否則回傳 `false`。

## 輸入限制與前提

- 題目語意下，`n` 應為正整數。
- 本專案的兩種解法都額外加入防禦式處理：若 `n <= 0`，直接回傳 `false`。
- 核心轉換規則固定為「各位數字平方和」，不涉及字串轉換或額外資料結構以外的特殊技巧。

## 解題概念與出發點

這題的重點不在於把「平方和」算出來，而在於辨識數字轉換過程何時應該停止。

每次把 `n` 轉成下一個數字後，會出現兩種可能：

- 最後收斂到 `1`，表示它是快樂數。
- 進入某個重複循環，例如 `2 -> 4 -> 16 -> 37 -> 58 -> 89 -> 145 -> 42 -> 20 -> 4 ...`，表示它不是快樂數。

因此，解題的核心是「如何偵測循環」。

本專案示範兩種常見思路：

1. 使用 `HashSet<int>` 記錄曾經出現過的中間值。
2. 使用 Floyd Cycle Detection（快慢指標）把轉換序列視為鏈結串列循環問題。

## 解法一：HashSet 記錄已出現數值

### 設計想法

每當我們算出一個新的中間值，就把它放進集合裡。

- 如果某一步先得到 `1`，代表這條路徑成功收斂，答案為 `true`。
- 如果某一步發現這個值以前看過，表示後面會重複同樣的變化流程，不可能再出現新結果，此時即可判定為 `false`。

這種方法直觀、可讀性高，也很適合拿來當這題的第一個版本。

### 演算法流程

1. 若 `n <= 0`，直接回傳 `false`。
2. 建立 `HashSet<int>`，用來記錄每次轉換後出現過的值。
3. 當前數字不是 `1`，而且尚未出現在集合中時：
   1. 先把當前值加入集合。
   2. 計算下一個「各位數字平方和」。
4. 迴圈結束後：
   - 若目前值為 `1`，回傳 `true`。
   - 否則代表已遇到重複值，回傳 `false`。

### 為什麼可行

一旦某個中間值重複，後面的轉換順序就會完全重演。因為每個數字的下一步是固定的，所以重複值等於重複狀態，也就代表進入循環。

### 複雜度

- 時間複雜度：`O(k)`，`k` 為進入 `1` 或循環前的轉換次數。
- 空間複雜度：`O(k)`，因為需要額外儲存已出現過的中間值。

## 解法二：Floyd 快慢指標

### 設計想法

把「數字經過平方和轉換後得到下一個數字」這件事，想成鏈結串列中的「節點指向下一個節點」。

如果這條路徑不是走到 `1`，而是掉進循環，那麼它就和鏈結串列環狀結構完全同型。這時可以使用 Floyd Cycle Detection：

- 慢指標一次走一步。
- 快指標一次走兩步。

若存在循環，快慢指標最終一定會在循環內相遇；若快指標先遇到 `1`，表示整條路徑成功收斂到快樂數。

### 演算法流程

1. 若 `n <= 0`，直接回傳 `false`。
2. 設定 `slow = n`、`fast = n`。
3. 每輪迭代：
   - `slow = Next(slow)`
   - `fast = Next(Next(fast))`
4. 若 `fast == 1`，回傳 `true`。
5. 若 `slow == fast` 且不在 `1`，代表已偵測到循環，回傳 `false`。

### 為什麼可行

只要不是快樂數，數字轉換就會陷入某個循環。Floyd 方法不需要額外集合，就能利用步伐差在循環內完成偵測，因此空間使用量更低。

### 複雜度

- 時間複雜度：`O(k)`，`k` 為進入 `1` 或循環前的轉換次數。
- 空間複雜度：`O(1)`，只使用固定數量變數。

### 為什麼這裡用 `do...while`

本專案 `Program.cs` 中的 `IsHappy2(int n)` 採用的是下面這種寫法：

```csharp
int slowRunner = n;
int fastRunner = n;

do
{
    slowRunner = SumOfSquaredDigits(slowRunner);
    fastRunner = SumOfSquaredDigits(SumOfSquaredDigits(fastRunner));
}
while (fastRunner != 1 && slowRunner != fastRunner);
```

這裡使用 `do...while` 的關鍵原因，是 `slowRunner` 與 `fastRunner` 一開始都從同一個起點 `n` 出發。

- 若改成一般 `while (fastRunner != 1 && slowRunner != fastRunner)`，那麼第一次檢查條件時，`slowRunner == fastRunner` 會立刻成立。
- 結果就是迴圈尚未真正開始移動，便直接跳出，無法完成 Floyd 快慢指標需要的「先前進，再比較」。
- `do...while` 則剛好相反：它會先執行一次迴圈內容，再檢查是否繼續，因此非常適合這種「兩個指標同起點」的情境。

也就是說，這段程式其實是在表達：

1. 先讓 `slowRunner` 走一步。
2. 先讓 `fastRunner` 走兩步。
3. 然後再判斷：
   - `fastRunner == 1`：代表序列已收斂到 `1`，所以 `n` 是快樂數。
   - `slowRunner == fastRunner`：代表兩者在非 `1` 的循環內相遇，所以 `n` 不是快樂數。

如果用極短的示意來看，假設 `n = 19`：

- 初始狀態：`slowRunner = 19`、`fastRunner = 19`
- 第 1 輪後：`slowRunner = 82`、`fastRunner = 68`

此時兩個指標都已經真的往前走過，接下來再比較是否到 `1` 或是否相遇，才有意義。這也是這裡 `do...while` 最值得記住的地方：**先跑一次，再判斷是否停止。**

### 另一種常見寫法：先讓快指標走一步，再用 `while`

教學文章中也很常看到另一種等價寫法：

```csharp
int slowRunner = n;
int fastRunner = SumOfSquaredDigits(n); // 許多範例會把這個函式命名為 getNext

while (fastRunner != 1 && slowRunner != fastRunner)
{
    slowRunner = SumOfSquaredDigits(slowRunner);
    fastRunner = SumOfSquaredDigits(SumOfSquaredDigits(fastRunner));
}
```

上面範例中的 `getNext`，在本專案對應的就是 `SumOfSquaredDigits(int value)`。

這種寫法和目前 repo 的版本，本質上仍然是同一個 Floyd 快慢指標演算法；差別只在於「如何避免一開始兩個指標就相等」：

- 目前 repo 的版本：`slowRunner` 與 `fastRunner` 都先設成 `n`，所以要靠 `do...while` 先執行一次。
- 常見 `while` 版本：先讓 `fastRunner` 偷跑一步，因此第一次進入條件判斷時，不會因為 `slowRunner == fastRunner` 而立刻停住。

換句話說，兩者只是初始化策略不同，核心邏輯沒有變：

- `slowRunner` 每次走一步。
- `fastRunner` 每次走兩步。
- 走到 `1` 就代表快樂數。
- 在非 `1` 的地方相遇就代表循環。

### `do...while` 版與 `while` 版比較

| 比較面向 | 目前 repo 的 `do...while` 版 | 常見的 `while` 版 |
| --- | --- | --- |
| 初始化方式 | `slowRunner = n`、`fastRunner = n` | `slowRunner = n`、`fastRunner = SumOfSquaredDigits(n)` |
| 迴圈型式 | `do...while`，先執行再判斷 | `while`，先判斷再執行 |
| 為什麼可行 | 同起點，因此必須先跑一次 | 快指標先偷跑一步，避開起點重合 |
| 初學者可讀性 | 較對稱，較貼近 Floyd 經典寫法 | 對不熟 `do...while` 的讀者通常更直觀 |
| 時間複雜度 | `O(k)` | `O(k)` |
| 空間複雜度 | `O(1)` | `O(1)` |
| 效能差異 | 幾乎可忽略 | 幾乎可忽略 |

實務上，這兩種寫法的效能差異非常小，通常不會是解題時需要在意的重點。真正值得比較的是「哪一種比較容易讓自己日後回看時立刻懂」：

- 若你喜歡初始化對稱、並且接受 `do...while` 的語意，現在 repo 裡這版很整齊。
- 若你平常較少用 `do...while`，那麼先讓 `fastRunner` 前進一步、再使用一般 `while` 的版本，往往會更好讀。

## 各位數平方和的核心轉換

兩種解法都依賴同一個輔助函式 `SumOfSquaredDigits(int value)`。

它的做法是：

1. 用 `% 10` 取得個位數。
2. 將該位數平方後加總。
3. 用 `/= 10` 去掉個位數。
4. 重複直到數字變成 `0`。

例如 `19` 的轉換如下：

- `19 % 10 = 9`，累加 `9 * 9 = 81`
- `19 / 10 = 1`
- `1 % 10 = 1`，再累加 `1 * 1 = 1`
- 總和為 `82`

## 範例演示流程

### 範例一：`19` 是快樂數

轉換序列如下：

`19 -> 82 -> 68 -> 100 -> 1`

逐步拆解：

1. `19 = 1^2 + 9^2 = 1 + 81 = 82`
2. `82 = 8^2 + 2^2 = 64 + 4 = 68`
3. `68 = 6^2 + 8^2 = 36 + 64 = 100`
4. `100 = 1^2 + 0^2 + 0^2 = 1`

因為最終得到 `1`，所以 `19` 是快樂數。

### 範例二：`2` 不是快樂數

轉換序列如下：

`2 -> 4 -> 16 -> 37 -> 58 -> 89 -> 145 -> 42 -> 20 -> 4 -> ...`

逐步觀察：

1. `2 = 2^2 = 4`
2. `4 = 4^2 = 16`
3. `16 = 1^2 + 6^2 = 1 + 36 = 37`
4. `37 = 3^2 + 7^2 = 9 + 49 = 58`
5. `58 = 5^2 + 8^2 = 25 + 64 = 89`
6. `89 = 8^2 + 9^2 = 64 + 81 = 145`
7. `145 = 1^2 + 4^2 + 5^2 = 1 + 16 + 25 = 42`
8. `42 = 4^2 + 2^2 = 16 + 4 = 20`
9. `20 = 2^2 + 0^2 = 4`

此時再次回到 `4`，代表已進入循環，永遠不會走到 `1`，所以 `2` 不是快樂數。

## 兩種解法比較

| 面向 | HashSet 解法 | Floyd 快慢指標 |
| --- | --- | --- |
| 核心思路 | 記錄看過的值，重複就停止 | 用快慢速度差偵測循環 |
| 可讀性 | 高，容易理解 | 稍抽象，但技巧性強 |
| 額外空間 | `O(k)` | `O(1)` |
| 適合情境 | 初學、快速實作、重視可讀性 | 想節省空間、熟悉循環偵測技巧 |

## 專案結構

```text
leetcode_202/
├── README.md
├── docs/
│   └── readme-template.md
└── leetcode_202/
    ├── Program.cs
    └── leetcode_202.csproj
```

## 驗證指令

> [!NOTE]
> 這個 repo 根目錄目前沒有 `.sln` 或根層 `csproj`，因此 `dotnet build` 與 `dotnet test` 直接在根目錄執行會得到 `MSB1003`。實際驗證請指定專案檔路徑。

### 建置

```bash
dotnet build leetcode_202/leetcode_202.csproj
```

### 執行範例

```bash
dotnet run --project leetcode_202/leetcode_202.csproj
```

## 實際執行輸出

以下為本專案目前實際驗證過的輸出：

```text
n=1 | HashSet=True | Floyd=True | Expected=True | Match=True
n=7 | HashSet=True | Floyd=True | Expected=True | Match=True
n=19 | HashSet=True | Floyd=True | Expected=True | Match=True
n=2 | HashSet=False | Floyd=False | Expected=False | Match=True
n=20 | HashSet=False | Floyd=False | Expected=False | Match=True
```

## 實作重點摘要

- `Main` 直接提供固定測試資料，方便肉眼驗證兩種解法是否一致。
- `IsHappy(int n)` 使用 `HashSet<int>` 偵測重複狀態。
- `IsHappy2(int n)` 使用 Floyd 快慢指標，在不額外儲存歷史值的情況下判斷是否進入循環。
- `SumOfSquaredDigits(int value)` 抽出共用轉換邏輯，讓兩種解法都能重複使用同一套規則。
