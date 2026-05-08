# leetcode_071 - 71. Simplify Path

![C#](https://img.shields.io/badge/C%23-console-239120)
![.NET](https://img.shields.io/badge/.NET-net10.0-512BD4)
![LeetCode](https://img.shields.io/badge/LeetCode-71%20Simplify%20Path-F89F1B)

這是一個使用 C# 撰寫的 LeetCode 71. Simplify Path 練習專案。程式位於 [`leetcode_071/Program.cs`](leetcode_071/Program.cs)，目前提供一種堆疊解法：

- `SimplifyPath(string path)`：解析 Unix-style 絕對路徑，移除多餘斜線、目前目錄與父目錄符號，回傳標準路徑。

## 快速連結

- [題目說明](#題目說明)
- [限制條件](#限制條件)
- [解題概念與出發點](#解題概念與出發點)
- [解法說明](#解法說明)
- [流程演示](#流程演示)
- [建置與執行](#建置與執行)

## 題目說明

給定一個 Unix-style 檔案系統的絕對路徑 `path`，路徑一定以 `/` 開頭。請將它轉換成簡化後的 canonical path。

Unix-style 路徑規則如下：

- 單一 `.` 表示目前目錄。
- 雙點 `..` 表示上一層目錄。
- 多個連續斜線，例如 `//` 或 `///`，都視為單一 `/`。
- 其他句點組合，例如 `...` 或 `....`，是合法目錄或檔案名稱，不應被當成特殊符號。

簡化後的路徑必須符合：

- 必須以單一 `/` 開頭。
- 目錄之間只能用一個 `/` 分隔。
- 除非結果是根目錄 `/`，否則不能以 `/` 結尾。
- 不能保留用來表示目前目錄或父目錄的 `.`、`..` 片段。

## 限制條件

- `1 <= path.Length <= 3000`
- `path` 是有效的 Unix-style 絕對路徑，且以 `/` 開頭。
- `path` 只包含英文字母、數字、`.`、`/` 與 `_`。

## 解題概念與出發點

路徑可以視為由 `/` 分隔出的片段序列。真正需要保留的是「從根目錄走到目標位置後，仍然有效的目錄名稱」。

關鍵觀察：

- 空片段代表連續斜線或頭尾斜線，不影響路徑。
- `.` 代表原地不動，可以忽略。
- `..` 代表回到上一層，因此要移除最近加入的有效目錄。
- 其他字串都是一般目錄名稱，即使是 `...` 也要保留。

這種「最近加入、最先被上一層回退移除」的行為正好符合堆疊。

## 解法說明

### 解法一：堆疊保存有效目錄

`SimplifyPath(string path)` 使用 `Stack<string>` 保存目前仍有效的目錄名稱。

處理步驟：

1. 用 `/` 切割路徑，並透過 `StringSplitOptions.RemoveEmptyEntries` 忽略連續斜線產生的空片段。
2. 逐一檢查每個片段：
   - 若是 `.`，代表目前目錄，直接略過。
   - 若是 `..`，代表回上一層；堆疊有資料時才 `Pop()`，根目錄不能再往上。
   - 其他片段視為合法目錄名稱，加入堆疊。
3. 掃描完成後，堆疊列舉順序會從最後加入的目錄開始，因此先反轉再用 `/` 串接。
4. 若堆疊為空，代表結果是根目錄 `/`。

核心判斷如下：

```csharp
if (segment == ".")
{
    continue;
}

if (segment == "..")
{
    if (directories.Count > 0)
    {
        directories.Pop();
    }

    continue;
}

directories.Push(segment);
```

### 複雜度分析

令 `n` 為 `path` 長度。

- 時間複雜度：`O(n)`，每個路徑片段只會被檢查一次，最後組合字串也與輸入長度同階。
- 空間複雜度：`O(n)`，最壞情況下所有片段都是有效目錄名稱，需要全部保存在堆疊中。

## 流程演示

### 解法一範例：`/home/user/Documents/../Pictures`

| 片段 | 動作 | 堆疊內容 |
| --- | --- | --- |
| `home` | 加入有效目錄 | `home` |
| `user` | 加入有效目錄 | `home`, `user` |
| `Documents` | 加入有效目錄 | `home`, `user`, `Documents` |
| `..` | 回上一層，移除 `Documents` | `home`, `user` |
| `Pictures` | 加入有效目錄 | `home`, `user`, `Pictures` |

反轉堆疊後由根目錄串接，結果為 `/home/user/Pictures`。

### 解法一範例：`/.../a/../b/c/../d/./`

| 片段 | 動作 | 堆疊內容 |
| --- | --- | --- |
| `...` | `...` 是合法名稱，加入堆疊 | `...` |
| `a` | 加入有效目錄 | `...`, `a` |
| `..` | 回上一層，移除 `a` | `...` |
| `b` | 加入有效目錄 | `...`, `b` |
| `c` | 加入有效目錄 | `...`, `b`, `c` |
| `..` | 回上一層，移除 `c` | `...`, `b` |
| `d` | 加入有效目錄 | `...`, `b`, `d` |
| `.` | 目前目錄，略過 | `...`, `b`, `d` |

結果為 `/.../b/d`。

## 建置與執行

請先安裝支援 `net10.0` 的 .NET SDK，並在專案根目錄執行：

```powershell
dotnet build .\leetcode_071\leetcode_071.csproj
dotnet run --project .\leetcode_071\leetcode_071.csproj
```

目前沒有獨立測試專案；`Program.Main` 會執行內建案例並印出實際輸出、預期輸出與比對結果。

| 輸入 | 預期輸出 |
| --- | --- |
| `/home/` | `/home` |
| `/home//foo/` | `/home/foo` |
| `/home/user/Documents/../Pictures` | `/home/user/Pictures` |
| `/../` | `/` |
| `/.../a/../b/c/../d/./` | `/.../b/d` |

執行時每筆案例都應顯示 `Result: PASS`。

## 專案結構

```text
.
├── docs/
│   └── readme-template.md
├── leetcode_071/
│   ├── Program.cs
│   └── leetcode_071.csproj
├── leetcode_071.slnx
└── README.md
```

## 參考連結

- [LeetCode 題目：Simplify Path](https://leetcode.com/problems/simplify-path/description/)
- [LeetCode CN 題目：簡化路徑](https://leetcode.cn/problems/simplify-path/description/)
