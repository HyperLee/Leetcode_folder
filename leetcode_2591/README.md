# LeetCode 2591：Distribute Money to Maximum Children

以 C# 與 .NET 10 實作 LeetCode 2591「將錢分給最多的兒童」。專案保留三個常數時間解法，並由 `Main` 使用固定測資逐一驗證所有實作。

- [題目連結（LeetCode）](https://leetcode.com/problems/distribute-money-to-maximum-children/)
- [題目連結（力扣）](https://leetcode.cn/problems/distribute-money-to-maximum-children/)
- [解題共同出發點](#解題共同出發點)
- [三種實作](#三種實作)
- [建置與執行](#建置與執行)

## 題目說明

給定兩個整數：

- `money`：必須全部分完的金額。
- `children`：必須分到錢的兒童人數。

分配必須同時滿足：

1. 所有金額都要分配完畢。
2. 每位兒童至少得到 1 元。
3. 任何兒童都不能剛好得到 4 元。

目標是讓最多兒童剛好得到 8 元。如果連「每人至少 1 元」都無法達成，回傳 `-1`。

### 限制條件

```text
1 <= money <= 200
2 <= children <= 30
```

### 官方範例

#### 範例一

```text
輸入：money = 20, children = 3
輸出：1
```

一種合法分配為 `[8, 9, 3]`，只有一位兒童得到 8 元。若嘗試讓兩人各得 8 元，最後一人只能得到 4 元，違反題目規則。

#### 範例二

```text
輸入：money = 16, children = 2
輸出：2
```

可以直接分成 `[8, 8]`，兩人都剛好得到 8 元。

## 解題共同出發點

三個方法都建立在同一個轉換上：

1. 先給每位兒童 1 元，因此先執行 `money -= children`。
2. 某位兒童已經有 1 元，要變成 8 元只需要再給 7 元。
3. 所以 `money / 7` 代表理論上最多可以把多少人補到 8 元。

例如 `money = 20`、`children = 3`：

```text
先給每人 1 元：20 - 3 = 17
能湊出的 7 元組數：17 / 7 = 2
剩餘金額：17 % 7 = 3
```

不能只看到兩組 7 元就回答 2。若讓兩人各拿到 8 元，第三人會取得原本的 1 元加剩下的 3 元，也就是禁止的 4 元。因此仍需要處理剩餘金額與剩餘人數。

### 兩個關鍵特殊情況

#### 所有人都先得到 8 元，但還有餘額

以 `money = 17`、`children = 2` 為例：

```text
先給每人 1 元後剩下 15 元
15 / 7 = 2，暫時認為兩人都能得到 8 元
補完兩組 7 元後仍剩 1 元
```

所有人都已經得到 8 元，剩下的 1 元仍必須分出去，只能讓其中一人變成 9 元。因此答案要從 2 修正成 1，合法分配可以是 `[8, 9]`。

#### 最後一人會剛好得到 4 元

以 `money = 12`、`children = 2` 為例：

```text
先給每人 1 元後剩下 10 元
先補一組 7 元，暫時得到 [8, 1]
剩下 3 元且只剩一人尚未補到 8 元
```

如果把 3 元全部給最後一人，會得到 `[8, 4]`，這是不合法的。必須拆掉原本的 8 元並重新分配，例如 `[6, 6]`，所以答案從 1 修正成 0。

## 三種實作

> `DistMoney2` 與 `DistMoney3` 並不是兩種不同演算法；兩者使用相同的貪心策略，差別在於程式步驟是否展開。

### 解法一：`DistMoney` 商數與餘數分類

這個方法直接使用「每人先取得 1 元」後的商數與餘數進行分類。

1. `money < 0`：原始金額不足以讓每人至少取得 1 元，回傳 `-1`。
2. `money / 7 == children` 且 `money % 7 == 0`：所有人都能剛好取得 8 元，直接回傳 `children`。
3. `money / 7 == children - 1` 且 `money % 7 == 3`：最後一人會取得 `1 + 3 = 4` 元，必須拆掉一組 8 元，因此回傳 `children - 2`。
4. 其他情況：答案為 `Math.Min(children - 1, money / 7)`。只要不是所有人剛好都取得 8 元，就至少要保留一人吸收餘額。

#### 範例流程：`money = 20, children = 3`

| 步驟 | 狀態 | 說明 |
| --- | --- | --- |
| 每人先給 1 元 | `money = 17` | 已使用 3 元 |
| 計算商數與餘數 | `17 / 7 = 2 ... 3` | 暫時可湊出兩組 8 元 |
| 檢查特殊條件 | `2 == 3 - 1` 且餘數為 `3` | 最後一人會取得 4 元 |
| 修正答案 | `children - 2 = 1` | 拆掉一組 8 元 |

最後回傳 `1`，合法分配範例為 `[8, 9, 3]`。

### 解法二：`DistMoney2` 展開式貪心

這個方法把貪心分配與兩個修正步驟分開書寫，適合觀察每個狀態如何改變。

1. 先給每人 1 元；不足時回傳 `-1`。
2. 使用 `Math.Min(money / 7, children)`，盡可能讓多人取得額外 7 元。
3. 扣除已使用的金額與已補到 8 元的人數。
4. 若剩餘人數是 0 但還有錢，代表必須破壞一組 8 元，答案減 1。
5. 若剩餘一人且剩餘 3 元，該人會得到 4 元，答案也要減 1。

#### 範例流程：`money = 12, children = 2`

| 步驟 | `money` | `children` | `ans` |
| --- | ---: | ---: | ---: |
| 每人先給 1 元 | 10 | 2 | 尚未計算 |
| 盡量補 7 元 | 10 | 2 | 1 |
| 扣除一組 7 元與一人 | 3 | 1 | 1 |
| 最後一人會得到 4 元 | 3 | 1 | 0 |

最後回傳 `0`，例如重新分成 `[6, 6]`。

### 解法三：`DistMoney3` 精簡貪心

這個方法保留 `DistMoney2` 的同一組狀態，但把兩個需要減少答案的特殊情況合併：

```csharp
if ((children == 0 && money > 0) || (children == 1 && money == 3))
{
    cnt--;
}
```

- `children == 0 && money > 0`：所有人都已得到 8 元，卻仍有金額必須分配。
- `children == 1 && money == 3`：最後一人會得到禁止的 4 元。

#### 範例流程：`money = 17, children = 2`

| 步驟 | `money` | `children` | `cnt` |
| --- | ---: | ---: | ---: |
| 每人先給 1 元 | 15 | 2 | 尚未計算 |
| 盡量補 7 元 | 15 | 2 | 2 |
| 扣除兩組 7 元與兩人 | 1 | 0 | 2 |
| 無人可以吸收剩餘 1 元 | 1 | 0 | 1 |

最後回傳 `1`，合法分配範例為 `[8, 9]`。

## 複雜度分析

| 方法 | 時間複雜度 | 額外空間複雜度 | 設計重點 |
| --- | --- | --- | --- |
| `DistMoney` | O(1) | O(1) | 以商數、餘數直接分類 |
| `DistMoney2` | O(1) | O(1) | 展開顯示貪心狀態與兩個修正 |
| `DistMoney3` | O(1) | O(1) | 合併兩個特殊條件的精簡貪心 |

輸入數值大小不會增加迴圈次數；每個方法都只進行固定次數的算術與條件判斷。

## 可執行測試案例

`Main` 會讓三個方法分別執行以下六組案例：

| `money` | `children` | 預期結果 | 驗證目的 |
| ---: | ---: | ---: | --- |
| 20 | 3 | 1 | 官方範例與禁止 4 元的特殊修正 |
| 16 | 2 | 2 | 所有人剛好取得 8 元 |
| 2 | 3 | -1 | 金額不足以讓每人至少取得 1 元 |
| 12 | 2 | 0 | 剩餘一人與 3 元的特殊修正 |
| 17 | 2 | 1 | 所有人先取得 8 元後仍有餘額 |
| 9 | 2 | 1 | 一般合法分配 `[8, 1]` |

總共會執行 `6 × 3 = 18` 次比對。

## 建置與執行

需求：

- [.NET 10 SDK](https://dotnet.microsoft.com/download/dotnet/10.0)

在儲存庫的 `leetcode_2591` 工作區根目錄執行：

```powershell
dotnet build .\leetcode_2591\leetcode_2591.csproj
dotnet run --project .\leetcode_2591\leetcode_2591.csproj
dotnet format .\leetcode_2591\leetcode_2591.csproj --verify-no-changes
git diff --check
```

### 執行輸出

```text
LeetCode 2591 - Distribute Money to Maximum Children

Case 1: money = 20, children = 3, expected = 1
  DistMoney  actual = 1, expected = 1 => PASS
  DistMoney2 actual = 1, expected = 1 => PASS
  DistMoney3 actual = 1, expected = 1 => PASS

Case 2: money = 16, children = 2, expected = 2
  DistMoney  actual = 2, expected = 2 => PASS
  DistMoney2 actual = 2, expected = 2 => PASS
  DistMoney3 actual = 2, expected = 2 => PASS

Case 3: money = 2, children = 3, expected = -1
  DistMoney  actual = -1, expected = -1 => PASS
  DistMoney2 actual = -1, expected = -1 => PASS
  DistMoney3 actual = -1, expected = -1 => PASS

Case 4: money = 12, children = 2, expected = 0
  DistMoney  actual = 0, expected = 0 => PASS
  DistMoney2 actual = 0, expected = 0 => PASS
  DistMoney3 actual = 0, expected = 0 => PASS

Case 5: money = 17, children = 2, expected = 1
  DistMoney  actual = 1, expected = 1 => PASS
  DistMoney2 actual = 1, expected = 1 => PASS
  DistMoney3 actual = 1, expected = 1 => PASS

Case 6: money = 9, children = 2, expected = 1
  DistMoney  actual = 1, expected = 1 => PASS
  DistMoney2 actual = 1, expected = 1 => PASS
  DistMoney3 actual = 1, expected = 1 => PASS

18/18 tests passed.
```

## 專案結構

```text
leetcode_2591/
├── README.md
├── docs/
│   └── readme-template.md
└── leetcode_2591/
    ├── leetcode_2591.csproj
    └── Program.cs
```
