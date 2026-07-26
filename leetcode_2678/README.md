# LeetCode 2678：老人的數目

[![.NET](https://img.shields.io/badge/.NET-10.0-512BD4)](https://dotnet.microsoft.com/)
[![Language](https://img.shields.io/badge/Language-C%23-239120)](https://learn.microsoft.com/dotnet/csharp/)

這是一個使用 C# 與 .NET 10 實作的主控台專案，解答
[LeetCode 2678：Number of Senior Citizens](https://leetcode.com/problems/number-of-senior-citizens/description/)。
程式以單次線性掃描讀取每筆乘客資料，直接從固定位置的兩個數字字元組成年齡，
並計算年齡嚴格大於 60 歲的乘客人數。

## 快速導覽

- [題目說明](#題目說明)
- [資料格式與限制](#資料格式與限制)
- [公開 API](#公開-api)
- [解題概念](#解題概念)
- [逐步範例](#逐步範例)
- [複雜度分析](#複雜度分析)
- [建置與執行](#建置與執行)
- [Acceptance harness](#acceptance-harness)
- [專案結構](#專案結構)

## 題目說明

輸入是一個索引從 0 開始的字串陣列 `details`。每個字串都以固定長度 15
壓縮記錄一位乘客的電話、性別、年齡與座位資訊。

需要回傳年齡**嚴格大於 60 歲**的乘客數量。因此 60 歲不計入，61 歲才計入。

- 英文題目：[2678. Number of Senior Citizens](https://leetcode.com/problems/number-of-senior-citizens/description/)
- 中文題目：[2678. 老人的數目](https://leetcode.cn/problems/number-of-senior-citizens/description/)

## 資料格式與限制

每筆資料的索引配置如下：

| 索引 | 長度 | 內容 |
|---|---:|---|
| `0..9` | 10 | 電話號碼 |
| `10` | 1 | 性別：`M`、`F` 或 `O` |
| `11..12` | 2 | 年齡，範圍 `00..99` |
| `13..14` | 2 | 座位號碼 |

題目限制：

- `1 <= details.Length <= 100`
- 每筆 `details[i]` 的長度固定為 15。
- 電話、年齡與座位欄位使用數字字元。
- 性別欄位為 `M`、`F` 或 `O`。
- 電話號碼與座位號碼各自不重複。

例如：

```plaintext
7868190130M7522
          │││ └─ 座位 22
          │└─── 年齡 75
          └──── 性別 M
```

## 公開 API

```csharp
public static int CountSeniors(string[] details)
```

- 輸入：符合題目固定格式的乘客資料陣列。
- 輸出：年齡嚴格大於 60 的乘客數量。
- 解法不會修改輸入，也不會自行輸出到主控台。
- 題目已保證輸入有效，因此不額外定義例外或修正無效資料。

## 解題概念

年齡永遠位於索引 11 與 12。數字字元減去 `'0'` 後即可取得對應數值：

```csharp
int age = ((detail[11] - '0') * 10) + (detail[12] - '0');
```

這個計算遵守兩個不變量：

1. `detail[11]` 是十位數，必須乘以 10。
2. 只有 `age > 60` 才增加計數；`age == 60` 必須維持原計數。

直接使用字元計算，不需要為每筆資料建立兩字元的子字串，也不需要呼叫
`int.Parse`。掃描期間只維護一個累計數量。

## 逐步範例

以官方範例一為例：

```plaintext
details =
[
  "7868190130M7522",
  "5303914400F9211",
  "9273338290F4010"
]
```

| 資料 | 索引 11、12 | 年齡 | 是否 `> 60` | 累計 |
|---|---|---:|---|---:|
| `7868190130M7522` | `7`, `5` | 75 | 是 | 1 |
| `5303914400F9211` | `9`, `2` | 92 | 是 | 2 |
| `9273338290F4010` | `4`, `0` | 40 | 否 | 2 |

最後回傳 `2`。

## 複雜度分析

令 `n = details.Length`：

- 時間複雜度：O(n)
  - 每筆資料只讀取固定的兩個年齡字元。
- 結果空間複雜度：O(1)
  - 公開 API 只回傳一個整數計數。
- 輔助空間複雜度：O(1)
  - 只維護年齡與累計數量，不配置與輸入大小相關的資料結構。

## 建置與執行

需求：

- [.NET 10 SDK](https://dotnet.microsoft.com/download/dotnet/10.0)

請從本題外層資料夾 `leetcode_2678/` 執行：

```powershell
dotnet build .\leetcode_2678\leetcode_2678.csproj --nologo
dotnet run --no-build --project .\leetcode_2678\leetcode_2678.csproj
```

也可以使用根 repository 路徑：

```powershell
dotnet build .\leetcode_2678\leetcode_2678\leetcode_2678.csproj --nologo
dotnet run --no-build --project .\leetcode_2678\leetcode_2678\leetcode_2678.csproj
```

從本題外層資料夾開啟 VS Code 時，可按 F5 使用
`.vscode/launch.json` 中的 `Debug leetcode_2678` 設定。

## Acceptance harness

本專案沒有獨立測試專案或測試框架。`Main` 會執行八組確定性檢查：

| 案例 | 預期 | 防止的錯誤 |
|---|---:|---|
| 官方範例一 | 2 | 基本多筆統計錯誤 |
| 官方範例二 | 0 | 沒有老人時誤計數 |
| 年齡恰好 60 | 0 | 把 `>= 60` 誤當成 `> 60` |
| 年齡恰好 61 | 1 | 嚴格邊界漏計 |
| 最小年齡 00 | 0 | 前導零解析錯誤 |
| 最大年齡 99 | 1 | 兩位數上界解析錯誤 |
| M/F/O 搭配 59/60/61 | 1 | 性別欄位干擾年齡判斷 |
| 100 筆、年齡 00 到 99 | 39 | 輸入上限與完整年齡範圍錯誤 |

每項檢查都會顯示輸入、Expected、Actual 與 PASS/FAIL。若有任一失敗，
程式會設定非零結束碼。

以下是 fresh run 的完整輸出：

```text
LeetCode 2678 - Number of Senior Citizens

[PASS] 官方範例一
  Input:    [7868190130M7522, 5303914400F9211, 9273338290F4010]
  Expected: 2
  Actual:   2

[PASS] 官方範例二
  Input:    [1313579440F2036, 2921522980M5644]
  Expected: 0
  Actual:   0

[PASS] 年齡恰好 60
  Input:    [0000000000M6000]
  Expected: 0
  Actual:   0

[PASS] 年齡恰好 61
  Input:    [0000000001F6101]
  Expected: 1
  Actual:   1

[PASS] 最小年齡 00
  Input:    [0000000002O0002]
  Expected: 0
  Actual:   0

[PASS] 最大年齡 99
  Input:    [0000000003M9903]
  Expected: 1
  Actual:   1

[PASS] 性別不影響年齡判斷
  Input:    [0000000004M5904, 0000000005F6005, 0000000006O6106]
  Expected: 1
  Actual:   1

[PASS] 100 筆上限，年齡 00 到 99
  Input:    100 passenger records with ages 00 through 99
  Expected: 39
  Actual:   39

Summary: 8/8 checks passed.
```

## 專案結構

```plaintext
leetcode_2678/
├── .editorconfig
├── .gitattributes
├── .gitignore
├── .vscode/
│   ├── launch.json
│   └── tasks.json
├── AGENTS.md
├── README.md
├── docs/
│   └── readme-template.md
└── leetcode_2678/
    ├── Program.cs
    └── leetcode_2678.csproj
```

舊式 solution、`App.config` 與手寫 `Properties/AssemblyInfo.cs` 已由
SDK-style 專案取代。
