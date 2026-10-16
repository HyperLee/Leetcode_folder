# 3723. 數位平方和的最大值

[LeetCode 題目（繁體中文）](https://leetcode.cn/problems/maximize-sum-of-squares-of-digits/description/) · [LeetCode 題目（英文）](https://leetcode.com/problems/maximize-sum-of-squares-of-digits/description/)

## 題目說明

給定正整數 `num` 和 `sum`，請找出一個恰好有 `num` 位、各位數字總和恰好為 `sum` 的正整數，使它的各位數字平方和最大。若多個整數的平方和相同，回傳數值最大的整數；若無法組成，回傳空字串。

例如 `num = 2`、`sum = 3` 時，符合條件的數字有 `12`、`21` 和 `30`。前兩者的平方和是 `1² + 2² = 5`，而 `30` 的平方和是 `3² + 0² = 9`，因此答案為 `"30"`。

## 限制條件

- `1 ≤ num ≤ 2 × 10^5`
- `1 ≤ sum ≤ 2 × 10^6`
- 每一位數字介於 `0` 與 `9`。
- `num` 位數字的最大總和是 `9 × num`；因此 `sum > 9 × num` 時無解。

## 解題概念與出發點

把每一位數字記為 `dᵢ`，目標是在 `0 ≤ dᵢ ≤ 9` 且 `Σdᵢ = sum` 的條件下，最大化 `Σdᵢ²`。若把數位和平均分散，平方和會比較小；把它集中在較少的數位上，平方和會更大。

平方函數是凸函數。考慮兩個數位 `a`、`b`，其中 `a ≥ b > 0` 且 `a < 9`。把 `b` 的一點移到 `a`，總和不變，而平方和增加：

```text
(a + 1)² + (b - 1)² - a² - b² = 2(a - b + 1) > 0
```

反覆套用這個交換方式，最佳配置會盡量填滿 `9`，最多只留下另一個非零餘數，其餘數位都是 `0`。平方和只取決於數字有哪些，不取決於排列順序；要在分數相同時取最大整數，則將較大的數字放在較高位。因此依序輸出 `9`、餘數、`0`。

## 解法一：貪心建構與 `StringBuilder`

`MaxSumOfSquares` 先檢查容量上限，再用整數除法與餘數一次算出要放幾個 `9`、是否有一個非零餘數，以及還要補多少個 `0`。

1. 若 `sum > 9 × num`，回傳空字串。
2. 計算 `fullNines = sum / 9`，在結果前面加入這麼多個 `9`。
3. 計算 `remainder = sum % 9`；若不為 `0`，將它接在 `9` 之後。
4. 以 `0` 補足到 `num` 位，讓結果符合指定位數，並保持所有高位數字盡可能大。

### 程式碼與步驟對照

| 程式碼 | 對應動作 |
| --- | --- |
| `if (sum > 9 * num)` | 檢查 `num` 位最多能容納的數位和；超過上限就回傳空字串。 |
| `fullNines = sum / 9`、`remainder = sum % 9` | 用商決定 `9` 的個數，用餘數決定是否還要放一個 `1` 至 `8`。 |
| `result.Append('9', fullNines)` | 使用 `StringBuilder` 重複加入指定數量的字元 `9`。 |
| `result.Append((char)('0' + remainder))` | 餘數大於 `0` 時，將數字餘數轉成對應字元，例如 `2` 轉為 `'2'`。 |
| `result.Append('0', num - result.Length)` | 依目前長度補上剩餘的 `0`，使結果剛好有 `num` 位。 |
| `return result.ToString()` | 將 `StringBuilder` 的內容轉成方法回傳的字串。 |

通過容量檢查後，已加入的 `9` 與餘數字元不會超過 `num` 個，因此補零數量 `num - result.Length` 不會是負數。題目又保證 `sum ≥ 1`，所以結果至少有一個非零數字排在前面，不會產生前導 `0`。

### 範例演示：`num = 4`、`sum = 20`

| 步驟 | 計算或動作 | 結果字串 |
| --- | --- | --- |
| 1 | `20 ≤ 9 × 4`，所以可組成 | 空字串 |
| 2 | `fullNines = 20 / 9 = 2`，加入兩個 `9` | `99` |
| 3 | `remainder = 20 % 9 = 2`，接上餘數 | `992` |
| 4 | 目前長度為 3，補 `4 - 3 = 1` 個 `0` | `9920` |

答案 `9920` 的數位和是 `9 + 9 + 2 + 0 = 20`，平方和是 `9² + 9² + 2² + 0² = 166`。把 `2` 放在 `9` 前面雖然平方和相同，但整數較小，所以先放高位的 `9` 也符合平手規則。

### 題目範例與邊界案例

| 輸入 `(num, sum)` | 建構結果 | 說明 |
| --- | --- | --- |
| `(2, 3)` | `30` | 沒有完整的 `9`，餘數 `3` 後補一個 `0`。 |
| `(2, 17)` | `98` | 一個 `9` 加餘數 `8`；`89` 與 `98` 分數相同，取較大的 `98`。 |
| `(1, 10)` | 空字串 | 一位數的總和最多為 `9`，但目標總和是 `10`。 |
| `(1, 1)` | `1` | 最小有效位數與數位和。 |
| `(2, 18)` | `99` | 每一位都填滿 `9`，達到容量上限。 |

### 正確性說明

交換論證顯示：只要兩個非零數位尚可把數值從較小者移到未滿 `9` 的較大者，平方和就會嚴格增加。因此最大平方和的配置必定先用盡可能多的 `9`，再放最多一個餘數，其餘為 `0`。程式依此構造結果；由於大數位由高位至低位排列，若有相同平方和的排列，輸出的整數也最大。

### 複雜度

- 時間：`O(num)`，需建立並回傳 `num` 個字元。
- 空間：回傳字串為 `O(num)`；此外 `StringBuilder` 也需要 `O(num)` 的中間緩衝區，轉成字串時會建立最終結果。

## 解法二：`string.Create` 直接建構

此解法沿用前面的貪心配置：先放盡可能多個 `9`，再放一個非零餘數，其餘補 `0`。差別在結果字串的建立方式：`string.Create` 先配置正確長度的最終字串，並把可寫入的 `Span<char>` 傳給 callback，因此可以直接填入結果，不必先建立 `StringBuilder` 中間內容再轉成字串。

```csharp
public string MaxSumOfSquaresWithStringCreate(int num, int sum)
{
    if (sum > 9 * num)
    {
        return string.Empty;
    }

    int fullNines = sum / 9;
    int remainder = sum % 9;

    return string.Create(
        num,
        (FullNines: fullNines, Remainder: remainder),
        static (digits, state) =>
        {
            digits.Fill('0');
            digits[..state.FullNines].Fill('9');
            if (state.Remainder > 0)
            {
                digits[state.FullNines] = (char)('0' + state.Remainder);
            }
        });
}
```

### 程式碼與步驟對照

| 程式碼 | 對應動作 |
| --- | --- |
| `if (sum > 9 * num)` | 檢查位數容量；超過上限時回傳空字串。 |
| `(FullNines: fullNines, Remainder: remainder)` | 將要填入的 `9` 數量與餘數傳給 callback。這是值 tuple，作為 `string.Create` 的狀態資料。 |
| `static (digits, state) =>` | 建立不捕捉外部變數的 callback；`digits` 是最終字串內容的可寫 `Span<char>`，`state` 是上述 tuple。 |
| `digits.Fill('0')` | 先將 `num` 個位置全部填成 `0`。 |
| `digits[..state.FullNines].Fill('9')` | 將開頭 `fullNines` 個位置改填為 `9`。 |
| `digits[state.FullNines] = ...` | 餘數非零時，將其轉成字元並填在 `9` 之後。 |

容量檢查通過後，`fullNines` 不會大於 `num`。若餘數大於 `0`，則 `fullNines < num`，所以餘數索引仍在字串範圍內。題目又保證 `sum ≥ 1`，因此最終結果不會是全零字串，也不會有前導零。

### 範例演示：`num = 4`、`sum = 20`

1. `20 ≤ 9 × 4`，可組成四位數。
2. `fullNines = 20 / 9 = 2`、`remainder = 20 % 9 = 2`。
3. `digits.Fill('0')` 後為 `0000`；填入兩個 `9` 後為 `9900`。
4. 將餘數 `2` 填在索引 `2`，結果為 `9920`。

其配置與解法一相同，因此平方和仍是 `9² + 9² + 2² + 0² = 166`；較大的數字放在高位，也符合同分時取最大整數的規則。

### 正確性說明

兩種方法使用相同的 `fullNines`、`remainder` 與數位排列順序，差異只在如何寫入結果字串。解法一透過 `StringBuilder.Append` 逐段建立內容；本解法把同一組字元直接寫入最終字串，所以沿用前述交換論證與平手處理，得到相同的最佳答案。

### 複雜度

- 時間：`O(num)`，初始化並填入 `num` 個字元。
- 額外工作空間：不計回傳字串為 `O(1)`；回傳字串本身仍需 `O(num)` 空間。

## 兩種方法優缺點比較

| 面向 | 解法一：`StringBuilder` | 解法二：`string.Create` |
| --- | --- | --- |
| 數位配置 | 貪心：`9`、餘數、`0` | 相同的貪心配置 |
| 時間複雜度 | `O(num)` | `O(num)` |
| 空間 | 回傳字串 `O(num)`，另有 `O(num)` 的 builder 中間緩衝區 | 回傳字串 `O(num)`，不另建 `O(num)` 的文字緩衝區；額外工作空間 `O(1)` |
| 優點 | 步驟直觀，透過 `Append` 容易閱讀與維護 | 直接填入最終字串，省去 builder 中間內容及轉字串時的複製 |
| 取捨 | 中間緩衝區與最終字串會同時占用記憶體 | callback、tuple 狀態與 `Span<char>` 切片較不直觀 |

兩種方法都必須配置 `num` 個字元作為回傳結果，所以總空間皆為 `O(num)`。`string.Create` 的優勢是少一份與輸出同級的中間儲存；這是配置方式上的差異，沒有基準測試時不推論實際執行時間一定較快。若優先考慮易讀性，解法一已足夠；若希望減少大型輸出時的中間配置，可採用解法二。

## 建置與執行範例

在 workspace 根目錄執行：

```powershell
dotnet build .\leetcode_3723\leetcode_3723.csproj
dotnet run --project .\leetcode_3723\leetcode_3723.csproj
```

`Main` 會以題目範例、不可行輸入、最小有效輸入、最大數位和，以及多個 `9` 加餘數等六個案例，分別檢查兩種方法。結果不符合預期時會輸出 `FAIL`，並以非零結束碼離開；全部通過時共為 `12/12` 項檢查。

預期執行輸出：

```text
PASS Example 1 (StringBuilder): num=2, sum=3, expected="30", actual="30"
PASS Example 1 (string.Create): num=2, sum=3, expected="30", actual="30"
PASS Example 2 (StringBuilder): num=2, sum=17, expected="98", actual="98"
PASS Example 2 (string.Create): num=2, sum=17, expected="98", actual="98"
PASS Example 3 (impossible) (StringBuilder): num=1, sum=10, expected="", actual=""
PASS Example 3 (impossible) (string.Create): num=1, sum=10, expected="", actual=""
PASS Minimum feasible (StringBuilder): num=1, sum=1, expected="1", actual="1"
PASS Minimum feasible (string.Create): num=1, sum=1, expected="1", actual="1"
PASS Maximum digit sum (StringBuilder): num=2, sum=18, expected="99", actual="99"
PASS Maximum digit sum (string.Create): num=2, sum=18, expected="99", actual="99"
PASS Multiple 9s and remainder (StringBuilder): num=4, sum=20, expected="9920", actual="9920"
PASS Multiple 9s and remainder (string.Create): num=4, sum=20, expected="9920", actual="9920"
Summary: 12/12 checks passed.
```

## 專案結構

- `leetcode_3723/Program.cs`：題目解法與可執行範例案例。
- `leetcode_3723/leetcode_3723.csproj`：目標為 .NET 10 的 console 專案。
- `docs/readme-template.md`：README 初始範本。
