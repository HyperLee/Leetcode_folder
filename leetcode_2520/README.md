# LeetCode 2520：統計能整除數字的位數

![.NET](https://img.shields.io/badge/.NET-10.0-512BD4?logo=dotnet)
![C%23](https://img.shields.io/badge/C%23-console-239120?logo=csharp)

這是一個以 C# / .NET 10 實作的主控台專案，用逐位模擬解決 LeetCode 2520
「Count the Digits That Divide a Number」。專案內建五筆固定測試資料，可直接執行並核對預期值與實際值。

## 快速連結

- [題目說明](#題目說明)
- [限制條件](#限制條件)
- [解題概念與出發點](#解題概念與出發點)
- [解法一：逐位模擬](#解法一逐位模擬)
- [範例演示流程](#範例演示流程)
- [建置與執行](#建置與執行)

## 題目說明

給定一個正整數 `num`，計算 `num` 的十進位表示中，有多少個數字可以整除 `num`。

若 `num % digit == 0`，代表 `digit` 可以整除 `num`。題目計算的是「數字出現的位置數量」，
不是不同數字的種類數量；因此相同數字重複出現時，每個位置都要分別判斷及計數。

### 官方範例

| 輸入 | 輸出 | 說明 |
| ---: | ---: | --- |
| `7` | `1` | `7` 可以整除自己。 |
| `121` | `2` | 兩個 `1` 都可以整除 `121`，`2` 不行。 |
| `1248` | `4` | `1`、`2`、`4`、`8` 都可以整除 `1248`。 |

題目來源：[LeetCode 2520](https://leetcode.com/problems/count-the-digits-that-divide-a-number/description/)

## 限制條件

- `1 <= num <= 10^9`
- `num` 的十進位表示不包含數字 `0`

第二項限制很重要：程式會使用 `num % digit` 判斷整除。因為合法輸入不會出現 `digit == 0`，
所以演算法不需要額外加入除以零的防護；若把不符合題目限制的數字傳入此解法，其行為不在本題保證範圍內。

## 解題概念與出發點

這題不需要先把整數轉成字串或配置陣列。十進位整數可以用兩個基本運算由右向左巡覽：

1. `temp % 10`：取得目前最低位的數字。
2. `temp /= 10`：移除已經處理過的最低位。

判斷整除時仍需要完整的原始數字，因此程式保留 `num` 不變，另外使用 `temp` 負責拆解數字。
如果直接持續修改 `num`，後續就會拿縮短後的數字當被除數，判斷結果將不再符合題意。

整體流程如下：

1. 將 `temp` 初始化為 `num`，並將計數器 `count` 設為 `0`。
2. 只要 `temp` 尚未變成 `0`，就以 `temp % 10` 取得目前數字 `digit`。
3. 若 `num % digit == 0`，將 `count` 加一。
4. 執行 `temp /= 10`，移除已處理的最低位。
5. 所有位數處理完畢後，回傳 `count`。

## 解法一：逐位模擬

### 設計說明

`CountDigits(int num)` 使用一個暫存整數逐位掃描，不會改變傳入的原始數字：

```csharp
int temp = num;
int count = 0;

while (temp != 0)
{
    int digit = temp % 10;

    if (num % digit == 0)
    {
        count++;
    }

    temp /= 10;
}

return count;
```

這項設計有幾個重點：

- **原始被除數與巡覽狀態分離**：`num` 永遠代表完整輸入；`temp` 只負責逐位移除數字。
- **每個位置獨立判斷**：例如 `121` 的兩個 `1` 會分別讓 `count` 增加，因此答案是 `2`。
- **不需要額外集合**：題目要求計算出現位置，而非去除重複數字，所以不使用 `HashSet<int>`。
- **不需要字串轉換**：取餘數與整數除法已足以巡覽所有十進位數字，額外空間維持常數級。
- **依賴合法輸入條件**：題目保證數字中沒有 `0`，因此 `num % digit` 不會發生除以零。

### 複雜度分析

令 `d` 為 `num` 的十進位位數：

- 時間複雜度：`O(d)`，也可寫成 `O(log10(num))`。每個數字只處理一次。
- 空間複雜度：`O(1)`。只使用 `temp`、`digit` 與 `count` 等固定數量的整數變數。

## 範例演示流程

以 `num = 1248` 為例，初始狀態為 `temp = 1248`、`count = 0`。

| 輪次 | 進入迴圈時的 `temp` | `digit = temp % 10` | `1248 % digit` | 是否整除 | 更新後 `count` | `temp /= 10` 後 |
| ---: | ---: | ---: | ---: | :---: | ---: | ---: |
| 1 | `1248` | `8` | `0` | 是 | `1` | `124` |
| 2 | `124` | `4` | `0` | 是 | `2` | `12` |
| 3 | `12` | `2` | `0` | 是 | `3` | `1` |
| 4 | `1` | `1` | `0` | 是 | `4` | `0` |

第四輪後 `temp` 變成 `0`，迴圈結束並回傳 `4`。雖然巡覽順序是 `8 → 4 → 2 → 1`，
但每一位都會被檢查，因此順序不影響最終計數。

### 重複數字範例：`121`

1. 最低位 `1`：`121 % 1 == 0`，`count` 變成 `1`。
2. 中間位 `2`：`121 % 2 != 0`，`count` 維持 `1`。
3. 最高位 `1`：`121 % 1 == 0`，`count` 變成 `2`。

兩個 `1` 位於不同位置，所以都會計入，最後回傳 `2`。

## 可執行測試設計

`Main` 建立一個 `Program` 解題物件，並將每筆資料交給私有的 `RunTestCase`：

- `RunTestCase` 呼叫 `CountDigits` 取得實際答案。
- 將實際答案與預期答案比較。
- 輸出輸入值、預期值、實際值及 `PASS` 或 `FAIL`。
- 回傳布林值，讓 `Main` 統計通過案例總數。

固定案例同時涵蓋三個官方範例、單一位數，以及接近題目上限的九位數合法輸入。

## 建置與執行

### 環境需求

- [.NET 10 SDK](https://dotnet.microsoft.com/download/dotnet/10.0)

以下命令均從本 README 所在的專案根目錄執行。

### 檢查格式

```powershell
dotnet format .\leetcode_2520\leetcode_2520.csproj --verify-no-changes
```

### 建置

```powershell
dotnet build .\leetcode_2520\leetcode_2520.csproj
```

實際驗證結果為建置成功，`0` 個警告、`0` 個錯誤。

### 執行固定案例

```powershell
dotnet run --project .\leetcode_2520\leetcode_2520.csproj
```

實際輸出：

```text
LeetCode 2520 - Count the Digits That Divide a Number
[PASS] num = 7, expected = 1, actual = 1
[PASS] num = 121, expected = 2, actual = 2
[PASS] num = 1248, expected = 4, actual = 4
[PASS] num = 9, expected = 1, actual = 1
[PASS] num = 999999999, expected = 9, actual = 9

5/5 passed.
```

## 專案結構

```text
leetcode_2520/
├── .vscode/
│   ├── launch.json
│   └── tasks.json
├── docs/
│   └── readme-template.md
├── leetcode_2520/
│   ├── leetcode_2520.csproj
│   └── Program.cs
├── AGENTS.md
└── README.md
```

- `Program.cs`：包含逐位模擬解法、固定測試資料及主控台驗證流程。
- `leetcode_2520.csproj`：定義 .NET 10 主控台專案。
- `docs/readme-template.md`：建立初始 README 時使用的結構與內容指引。
- `.vscode/`：提供 VS Code 的建置與 F5 偵錯設定。
