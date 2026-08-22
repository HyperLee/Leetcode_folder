# LeetCode 3622：Check Divisibility by Digit Sum and Product

[![.NET 10](https://img.shields.io/badge/.NET-10.0-512BD4?logo=dotnet&logoColor=white)](https://dotnet.microsoft.com/)
[![LeetCode 3622](https://img.shields.io/badge/LeetCode-3622-orange?logo=leetcode&logoColor=white)](https://leetcode.com/problems/check-divisibility-by-digit-sum-and-product/description/)

這是一個以 .NET 10 console project 實作的 LeetCode 3622 解題教學。程式保留兩種可以互相對照的解法，並由 `Main` 使用固定測試資料直接執行驗證。

- [題目說明](#題目說明)
- [解題概念與出發點](#解題概念與出發點)
- [解法一：整數模擬](#解法一整數模擬)
- [解法二：字串走訪](#解法二字串走訪)
- [兩種解法比較](#兩種解法比較)
- [可執行測試資料](#可執行測試資料)
- [執行方式](#執行方式)

## 題目說明

給定一個正整數 `n`，請計算：

1. `n` 的數位總和：所有數字相加。
2. `n` 的數位乘積：所有數字相乘。

令：

```text
divisor = digitSum + digitProduct
```

如果 `n % divisor == 0`，回傳 `true`；否則回傳 `false`。

官方題目、範例與限制條件可參考 [LeetCode 3622](https://leetcode.com/problems/check-divisibility-by-digit-sum-and-product/description/)。

### 範例

#### 範例一：`n = 99`

```text
digitSum = 9 + 9 = 18
digitProduct = 9 * 9 = 81
divisor = 18 + 81 = 99
99 % 99 = 0
```

因此回傳 `true`。

#### 範例二：`n = 23`

```text
digitSum = 2 + 3 = 5
digitProduct = 2 * 3 = 6
divisor = 5 + 6 = 11
23 % 11 = 1
```

因此回傳 `false`。

### 限制條件

- `n` 是正整數。
- `1 <= n <= 10^6`。
- 最大輸入至多有 7 位數，因此逐位處理的成本很低。
- 因為 `n` 為正整數，所以至少有一個非零數位，`digitSum` 至少為 1，最後的除數不會是 0。

## 解題概念與出發點

這題的核心不是搜尋或複雜資料結構，而是忠實地把題目中的兩個數學量算出來：

1. 每個數位都必須被讀取一次。
2. 數位總和用加法累積，初始值為 `0`。
3. 數位乘積用乘法累積，初始值必須是乘法單位元 `1`。
4. 最後使用原始的 `n` 做整除判斷。

實作中刻意保留兩種數位走訪方式：

- 整數模擬法展示 `% 10` 與 `/ 10` 的基本數位操作。
- 字串走訪法展示把數字轉成文字後，如何將字元數字轉回整數。

兩種方法計算出的數學結果相同，因此可以在同一個 executable harness 中用相同測試資料互相驗證。

## 解法一：整數模擬

對應程式 API：`CheckDivisibility(int n)`。

### 設計流程

1. 將輸入保存為 `original`，因為後續會不斷修改工作中的 `n`。
2. 設定 `digitSum = 0`、`digitProduct = 1`。
3. 只要工作中的 `n` 仍大於 0，就重複以下動作：
   - `n % 10` 取得目前最右側的數位。
   - `n /= 10` 移除已經處理的最右側數位。
   - 將該數位加入總和，並乘入乘積。
4. 迴圈結束後，判斷 `original % (digitSum + digitProduct) == 0`。

### 範例演示：`n = 99`

| 步驟 | 取出前的工作值 | 取出數位 | `digitSum` | `digitProduct` |
| --- | ---: | ---: | ---: | ---: |
| 1 | 99 | 9 | 9 | 9 |
| 2 | 9 | 9 | 18 | 81 |

處理完成後，工作值變成 `0`，而原始值仍是 `99`：

```text
divisor = 18 + 81 = 99
original % divisor = 99 % 99 = 0
```

結果為 `true`。

### 關鍵細節

- `original` 不能省略，否則迴圈結束後 `n` 已經是 `0`，無法正確進行最後判斷。
- `digitProduct` 必須從 `1` 開始；若從 `0` 開始，所有輸入的乘積都會錯誤地維持為 `0`。
- 遇到數位 `0` 時，乘積會變成 `0`，這是題目定義的正常結果，不需要額外分支。

### 複雜度

- 時間複雜度：`O(d)`，其中 `d` 是 `n` 的位數。
- 空間複雜度：`O(1)`，只使用固定數量的整數變數。

## 解法二：字串走訪

對應程式 API：`CheckDivisibilityByString(int n)`。

### 設計流程

1. 使用 `n.ToString()` 取得十進位數字字串。
2. 從左到右走訪每一個 `char`。
3. 透過 `character - '0'` 將數字字元轉成整數數位。
4. 使用與解法一相同的方式累積數位總和與數位乘積。
5. 直接使用未被修改的 `n` 做整除判斷。

### 範例演示：`n = 23`

字串內容為 `"23"`，逐字元處理如下：

| 步驟 | 字元 | 轉換後數位 | `digitSum` | `digitProduct` |
| --- | --- | ---: | ---: | ---: |
| 1 | `'2'` | 2 | 2 | 2 |
| 2 | `'3'` | 3 | 5 | 6 |

處理完成後：

```text
divisor = 5 + 6 = 11
n % divisor = 23 % 11 = 1
```

結果為 `false`。

### 含有 0 的案例：`n = 10`

字串走訪會讀取 `1` 與 `0`：

```text
digitSum = 1 + 0 = 1
digitProduct = 1 * 0 = 0
divisor = 1 + 0 = 1
10 % 1 = 0
```

因此結果為 `true`。這也示範了數位乘積遇到 `0` 時不需要特殊處理。

### 複雜度

- 時間複雜度：`O(d)`，每個數位字元只走訪一次。
- 空間複雜度：`O(d)`，需要保存由 `n.ToString()` 建立的字串；本題限制下 `d` 至多為 7。

## 兩種解法比較

| 比較項目 | 整數模擬 | 字串走訪 |
| --- | --- | --- |
| 數位取得方式 | `% 10` 與 `/ 10` | `ToString()` 後逐字元走訪 |
| 是否修改工作中的輸入 | 會修改工作副本，因此要保存 `original` | 不修改 `n` |
| 時間複雜度 | `O(d)` | `O(d)` |
| 額外空間 | `O(1)` | `O(d)` |
| 主要教學重點 | 整數數位操作與保留原值 | 字元轉數字與字串走訪 |

在 LeetCode 的限制下，兩者都足夠有效率。若重視額外空間，整數模擬法較精簡；若希望讓每個數位的處理流程更直觀，字串走訪法較容易閱讀。

## 可執行測試資料

`Main` 會使用下列固定案例，並讓兩種解法都執行一次：

| 案例 | 輸入 | Expected | 覆蓋重點 |
| --- | ---: | ---: | --- |
| 官方範例：總和等於原數 | 99 | `True` | 多位數且可以整除 |
| 官方範例：無法整除 | 23 | `False` | 多位數且無法整除 |
| 單一數位 | 1 | `False` | 最小輸入與乘法初始值 |
| 含有 0，數位乘積為 0 | 10 | `True` | 乘積變成 0 的情況 |
| 含有 0 且除數不整除 | 101 | `False` | 0 與非整除結果同時出現 |
| 限制上限 | 1,000,000 | `True` | 最大允許輸入 |

每個案例會產生兩筆檢查，因此完整 harness 共執行 12 筆檢查。只要有一筆失敗，程式就會輸出 `FAIL` 並以非零結束碼結束。

### Fresh run transcript

以下內容取自完成 build 後執行 `dotnet run --project leetcode_3622/leetcode_3622.csproj --no-build` 的實際輸出：

```text
LeetCode 3622：Check Divisibility by Digit Sum and Product
========================================================================
案例：官方範例：總和等於原數，n = 99
  [解法一：整數模擬] Expected = True, Actual = True, PASS
  [解法二：字串走訪] Expected = True, Actual = True, PASS
案例：官方範例：無法整除，n = 23
  [解法一：整數模擬] Expected = False, Actual = False, PASS
  [解法二：字串走訪] Expected = False, Actual = False, PASS
案例：單一數位，n = 1
  [解法一：整數模擬] Expected = False, Actual = False, PASS
  [解法二：字串走訪] Expected = False, Actual = False, PASS
案例：含有 0，數位乘積為 0，n = 10
  [解法一：整數模擬] Expected = True, Actual = True, PASS
  [解法二：字串走訪] Expected = True, Actual = True, PASS
案例：含有 0 且除數不整除，n = 101
  [解法一：整數模擬] Expected = False, Actual = False, PASS
  [解法二：字串走訪] Expected = False, Actual = False, PASS
案例：限制上限，n = 1000000
  [解法一：整數模擬] Expected = True, Actual = True, PASS
  [解法二：字串走訪] Expected = True, Actual = True, PASS
------------------------------------------------------------------------
總結：12/12 項測試通過。
```

## 執行方式

請在 `Leetcode_folder/leetcode_3622` 執行以下命令：

### 還原與建置

```bash
dotnet restore leetcode_3622/leetcode_3622.csproj
dotnet build leetcode_3622/leetcode_3622.csproj --nologo
```

### 執行 executable harness

```bash
dotnet run --project leetcode_3622/leetcode_3622.csproj --no-build
```

建議先完成建置，再使用 `--no-build` 執行最近一次成功建置的 DLL，確保 README transcript 對應目前原始碼。

### XML 文件與品質檢查

```bash
dotnet build leetcode_3622/leetcode_3622.csproj --nologo -p:GenerateDocumentationFile=true -warnaserror:CS1570,CS1571
dotnet format leetcode_3622/leetcode_3622.csproj --verify-no-changes --no-restore
git diff --check
```

專案目前沒有獨立測試專案，因此 executable harness 的固定輸出是行為驗收依據。

## 專案結構

```text
leetcode_3622/
├── docs/
│   └── readme-template.md
├── leetcode_3622/
│   ├── Program.cs
│   └── leetcode_3622.csproj
├── AGENTS.md
└── README.md
```

- `leetcode_3622/Program.cs`：題目入口、兩種解法與固定測試 harness。
- `leetcode_3622/leetcode_3622.csproj`：目標框架為 `net10.0` 的 SDK-style project。
- `docs/readme-template.md`：README 初始建立時使用的範本。