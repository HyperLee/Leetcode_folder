# LeetCode 69 - Sqrt(x)

這個專案使用 C# 實作 LeetCode 69「Sqrt(x)」。目標是在不能使用內建指數函式或平方根函式的前提下，回傳非負整數 `x` 的整數平方根，也就是真實平方根無條件捨去後的值。

## 題目說明

給定一個非負整數 `x`，回傳 `x` 的平方根，並將結果無條件捨去到最接近的整數。

範例：

```text
Input: x = 4
Output: 2
```

```text
Input: x = 8
Output: 2
Explanation: sqrt(8) 約為 2.82842，無條件捨去後為 2。
```

## 限制條件

- `0 <= x <= 2^31 - 1`
- 回傳值必須是非負整數。
- 不可使用內建指數函式、平方根函式或等價運算子。

## 解題概念與出發點

整數平方根可以改寫成一個搜尋問題：找出最大的整數 `k`，使得 `k * k <= x`。

如果某個數 `mid` 的平方小於或等於 `x`，代表 `mid` 是候選答案，但右側可能還有更大的合法答案；如果 `mid * mid > x`，代表 `mid` 太大，答案只能在左側。這個判斷具備單調性，因此適合使用二分搜尋。

## 解法一：二分搜尋

目前實作位於 `leetcode_069/Program.cs` 的 `MySqrt(int x)`。

設計重點：

- 搜尋範圍從 `left = 0` 到 `right = x`。
- 每次取中間值 `mid = left + (right - left) / 2`，避免 `left + right` 在大數輸入時溢位。
- 使用 `long square = (long)mid * mid` 計算平方，避免 `int` 乘法在接近 `int.MaxValue` 時溢位。
- 當 `square <= x` 時，代表 `mid` 是目前可行答案，記錄到 `answer`，再把 `left` 往右移。
- 當 `square > x` 時，代表 `mid` 過大，把 `right` 往左移。
- 迴圈結束後，`answer` 就是小於或等於真實平方根的最大整數。

複雜度：

- 時間複雜度：`O(log x)`
- 空間複雜度：`O(1)`

### 範例演示：x = 8

| 步驟 | left | right | mid | mid * mid | 判斷 | answer |
| --- | ---: | ---: | ---: | ---: | --- | ---: |
| 初始 | 0 | 8 | - | - | 尚未開始 | 0 |
| 1 | 0 | 8 | 4 | 16 | `16 > 8`，往左找 | 0 |
| 2 | 0 | 3 | 1 | 1 | `1 <= 8`，記錄 1 並往右找 | 1 |
| 3 | 2 | 3 | 2 | 4 | `4 <= 8`，記錄 2 並往右找 | 2 |
| 4 | 3 | 3 | 3 | 9 | `9 > 8`，往左找 | 2 |

最後 `left = 3`、`right = 2`，搜尋結束，回傳 `answer = 2`。

### 範例演示：x = 2147483647

`2147483647` 是 `int.MaxValue`。搜尋過程中 `mid` 可能非常大，如果直接使用 `int` 計算 `mid * mid`，平方值會超過 `int` 可表示範圍。因此實作先將 `mid` 轉成 `long` 再相乘。

最終找到的最大合法整數是 `46340`：

```text
46340 * 46340 = 2147395600 <= 2147483647
46341 * 46341 = 2147488281 > 2147483647
```

所以 `MySqrt(2147483647)` 回傳 `46340`。

## 執行方式

建置專案：

```bash
dotnet build leetcode_069/leetcode_069.csproj
```

執行內建範例資料：

```bash
dotnet run --project leetcode_069/leetcode_069.csproj
```

目前輸出：

```text
MySqrt(0) = 0; expected = 0; PASS
MySqrt(1) = 1; expected = 1; PASS
MySqrt(4) = 2; expected = 2; PASS
MySqrt(8) = 2; expected = 2; PASS
MySqrt(15) = 3; expected = 3; PASS
MySqrt(2147395599) = 46339; expected = 46339; PASS
MySqrt(2147483647) = 46340; expected = 46340; PASS
```

## 專案結構

```text
.
├── docs/
│   └── readme-template.md
├── leetcode_069/
│   ├── Program.cs
│   └── leetcode_069.csproj
└── README.md
```

> [!NOTE]
> 目前沒有獨立測試專案；範例資料放在 `Main` 進入點中，可透過 `dotnet run` 快速驗證主要案例與邊界案例。
