# Leetcode Folder

這個 repository 是一份持續累積的 LeetCode C# 解題集合。根目錄負責提供總覽、使用方式與完整題目索引；每一題的題目描述、演算法推導、複雜度與補充筆記，則放在各自的 `leetcode_*` 題目資料夾內。

## 專案概況

- 題目資料夾：608 個 `leetcode_*` 目錄，題號範圍從 1 到 3783。
- 題目 README：312 個題目根目錄目前已有 `README.md`，其餘題目可先從 `Program.cs` 或內層補充文件閱讀。
- C# 專案：608 個題目目前可找到非測試 `.csproj`。
- 測試專案：目前有 3 個 `.Tests.csproj`，分別位於 `leetcode_111`、`leetcode_2154`、`leetcode_316`。
- TargetFramework 分布：`net8.0` (294)、`net10.0` (176)、`.NET Framework v4.8` (132)、`net9.0` (6)。

## 閱讀方式

建議依照以下順序閱讀一題：

1. 先進入對應題號資料夾，例如 `leetcode_001/`。
2. 若有題目根目錄 `README.md`，先閱讀該文件取得題目摘要、解法方向與複雜度。
3. 進入內層同名 C# 專案資料夾，閱讀 `Program.cs` 的實作、註解與內建輸出範例。
4. 若題目資料夾內有 `Describe.md`、`解題說明.md`、`memo.md`、`*.prompt.md` 或圖解檔，通常代表該題有額外推導、除錯紀錄或教學補充。

根 README 不重複搬運每題的演算法細節，避免首頁變成難以維護的巨型解題書；這裡只提供全域導覽與索引。

## 專案結構

多數題目採用以下結構：

```text
leetcode_001/
|-- README.md                    # 題目說明與解法摘要，部分題目尚未補齊
|-- leetcode_001.sln 或 .slnx     # 單題 solution，部分題目可能沒有
|-- leetcode_001/
|   |-- Program.cs                # C# 解法與示範入口
|   `-- leetcode_001.csproj       # 單題 C# 專案
`-- docs/ 或其他補充檔案           # 少數題目會有額外筆記、圖解或計畫文件
```

少數較舊題目使用 .NET Framework 4.8，會看到 `App.config`、`Properties/AssemblyInfo.cs` 與 `<TargetFrameworkVersion>v4.8</TargetFrameworkVersion>`。較新的題目多使用 SDK-style `.csproj`，Target 以 `net8.0`、`net9.0`、`net10.0` 為主。

## 建置與執行

建議從 repository 根目錄執行單題專案。以 `leetcode_001` 為例：

```powershell
dotnet build .\leetcode_001\leetcode_001\leetcode_001.csproj
dotnet run --project .\leetcode_001\leetcode_001\leetcode_001.csproj
```

另一個範例：

```powershell
dotnet build .\leetcode_3100\leetcode_3100\leetcode_3100.csproj
dotnet run --project .\leetcode_3100\leetcode_3100\leetcode_3100.csproj
```

若題目有測試專案，可直接指定 `.Tests.csproj`：

```powershell
dotnet test .\leetcode_111\leetcode_111.Tests\leetcode_111.Tests.csproj
```

這個 repository 沒有根目錄統一 solution；主要工作流是「挑一題、進入該題、建置或執行該題」。

## 維護約定

- 根目錄 `README.md`：只放 repository 介紹、使用方式、結構說明與完整索引。
- 題目根目錄 `README.md`：放該題的題目摘要、核心想法、演算法推導、複雜度與範例。
- `Program.cs`：保留可執行示範與主要解法；若有多種解法，優先讓命名與註解能對應 README。
- 補充文件：可放推導草稿、圖解、除錯紀錄或替代解法，但避免把臨時對話內容當成最終說明。
- 新增題目時，建議沿用 `leetcode_題號/leetcode_題號/Program.cs` 與同名 `.csproj` 結構，方便索引與工具掃描。

## 完整題目索引

下表依題號數字排序。`README` 欄位標示 `未補` 代表該題根目錄尚未有正式 README；仍可先閱讀 `Program.cs` 或題目資料夾內的其他補充文件。

| 題號 | 資料夾 | README | 專案檔 | Target |
| ---: | --- | --- | --- | --- |
| 1 | [leetcode_001](leetcode_001/) | [README](leetcode_001/README.md) | [leetcode_001.csproj](leetcode_001/leetcode_001/leetcode_001.csproj) | net8.0 |
| 2 | [leetcode_002](leetcode_002/) | [README](leetcode_002/README.md) | [leetcode_002.csproj](leetcode_002/leetcode_002/leetcode_002.csproj) | net10.0 |
| 3 | [leetcode_003](leetcode_003/) | 未補 | [leetcode_003.csproj](leetcode_003/leetcode_003/leetcode_003.csproj) | net8.0 |
| 4 | [leetcode_004](leetcode_004/) | 未補 | [leetcode_004.csproj](leetcode_004/leetcode_004/leetcode_004.csproj) | net8.0 |
| 5 | [leetcode_005](leetcode_005/) | [README](leetcode_005/README.md) | [leetcode_005.csproj](leetcode_005/leetcode_005/leetcode_005.csproj) | net10.0 |
| 7 | [leetcode_007](leetcode_007/) | [README](leetcode_007/README.md) | [leetcode_007.csproj](leetcode_007/leetcode_007/leetcode_007.csproj) | net8.0 |
| 8 | [leetcode_008](leetcode_008/) | 未補 | [leetcode_008.csproj](leetcode_008/leetcode_008/leetcode_008.csproj) | net8.0 |
| 9 | [leetcode_009](leetcode_009/) | [README](leetcode_009/README.md) | [leetcode_009.csproj](leetcode_009/leetcode_009/leetcode_009.csproj) | net10.0 |
| 11 | [leetcode_011](leetcode_011/) | [README](leetcode_011/README.md) | [leetcode_011.csproj](leetcode_011/leetcode_011/leetcode_011.csproj) | net10.0 |
| 12 | [leetcode_012](leetcode_012/) | [README](leetcode_012/README.md) | [leetcode_012.csproj](leetcode_012/leetcode_012/leetcode_012.csproj) | net10.0 |
| 13 | [leetcode_013](leetcode_013/) | [README](leetcode_013/README.md) | [leetcode_013.csproj](leetcode_013/leetcode_013/leetcode_013.csproj) | net10.0 |
| 14 | [leetcode_014](leetcode_014/) | [README](leetcode_014/README.md) | [leetcode_014.csproj](leetcode_014/leetcode_014/leetcode_014.csproj) | net10.0 |
| 15 | [leetcode_015](leetcode_015/) | 未補 | [leetcode_015.csproj](leetcode_015/leetcode_015/leetcode_015.csproj) | net8.0 |
| 16 | [leetcode_016](leetcode_016/) | [README](leetcode_016/README.md) | [leetcode_016.csproj](leetcode_016/leetcode_016/leetcode_016.csproj) | net8.0 |
| 17 | [leetcode_017](leetcode_017/) | 未補 | [leetcode_017.csproj](leetcode_017/leetcode_017/leetcode_017.csproj) | net8.0 |
| 19 | [leetcode_019](leetcode_019/) | [README](leetcode_019/README.md) | [leetcode_019.csproj](leetcode_019/leetcode_019/leetcode_019.csproj) | net10.0 |
| 20 | [leetcode_020](leetcode_020/) | 未補 | [leetcode_020.csproj](leetcode_020/leetcode_020/leetcode_020.csproj) | net8.0 |
| 21 | [leetcode_021](leetcode_021/) | 未補 | [leetcode_021.csproj](leetcode_021/leetcode_021/leetcode_021.csproj) | net8.0 |
| 23 | [leetcode_023](leetcode_023/) | 未補 | [leetcode_023.csproj](leetcode_023/leetcode_023/leetcode_023.csproj) | net8.0 |
| 24 | [leetcode_024](leetcode_024/) | [README](leetcode_024/README.md) | [leetcode_024.csproj](leetcode_024/leetcode_024/leetcode_024.csproj) | net10.0 |
| 26 | [leetcode_026](leetcode_026/) | [README](leetcode_026/README.md) | [leetcode_026.csproj](leetcode_026/leetcode_026/leetcode_026.csproj) | net10.0 |
| 27 | [leetcode_027](leetcode_027/) | [README](leetcode_027/README.md) | [leetcode_027.csproj](leetcode_027/leetcode_027/leetcode_027.csproj) | net10.0 |
| 28 | [leetcode_028](leetcode_028/) | [README](leetcode_028/README.md) | [leetcode_028.csproj](leetcode_028/leetcode_028/leetcode_028.csproj) | net10.0 |
| 29 | [leetcode_029](leetcode_029/) | [README](leetcode_029/README.md) | [leetcode_029.csproj](leetcode_029/leetcode_029/leetcode_029.csproj) | net8.0 |
| 33 | [leetcode_033](leetcode_033/) | [README](leetcode_033/README.md) | [leetcode_033.csproj](leetcode_033/leetcode_033/leetcode_033.csproj) | net10.0 |
| 35 | [leetcode_035](leetcode_035/) | [README](leetcode_035/README.md) | [leetcode_035.csproj](leetcode_035/leetcode_035/leetcode_035.csproj) | net10.0 |
| 39 | [leetcode_039](leetcode_039/) | 未補 | [leetcode_039.csproj](leetcode_039/leetcode_039/leetcode_039.csproj) | net8.0 |
| 40 | [leetcode_040](leetcode_040/) | 未補 | [leetcode_040.csproj](leetcode_040/leetcode_040/leetcode_040.csproj) | net8.0 |
| 42 | [leetcode_042](leetcode_042/) | 未補 | [leetcode_042.csproj](leetcode_042/leetcode_042/leetcode_042.csproj) | net9.0 |
| 45 | [leetcode_045](leetcode_045/) | [README](leetcode_045/README.md) | [leetcode_045.csproj](leetcode_045/leetcode_045/leetcode_045.csproj) | net10.0 |
| 46 | [leetcode_046](leetcode_046/) | 未補 | [leetcode_046.csproj](leetcode_046/leetcode_046/leetcode_046.csproj) | net8.0 |
| 47 | [leetcode_047](leetcode_047/) | [README](leetcode_047/README.md) | [leetcode_047.csproj](leetcode_047/leetcode_047/leetcode_047.csproj) | net10.0 |
| 48 | [leetcode_048](leetcode_048/) | [README](leetcode_048/README.md) | [leetcode_048.csproj](leetcode_048/leetcode_048/leetcode_048.csproj) | net10.0 |
| 49 | [leetcode_049](leetcode_049/) | [README](leetcode_049/README.md) | [leetcode_049.csproj](leetcode_049/leetcode_049/leetcode_049.csproj) | net10.0 |
| 50 | [leetcode_050](leetcode_050/) | [README](leetcode_050/README.md) | [leetcode_050.csproj](leetcode_050/leetcode_050/leetcode_050.csproj) | net10.0 |
| 53 | [leetcode_053](leetcode_053/) | 未補 | [leetcode_053.csproj](leetcode_053/leetcode_053/leetcode_053.csproj) | net8.0 |
| 54 | [leetcode_054](leetcode_054/) | 未補 | [leetcode_054.csproj](leetcode_054/leetcode_054/leetcode_054.csproj) | net8.0 |
| 55 | [leetcode_055](leetcode_055/) | [README](leetcode_055/README.md) | [leetcode_055.csproj](leetcode_055/leetcode_055/leetcode_055.csproj) | net10.0 |
| 56 | [leetcode_056](leetcode_056/) | 未補 | [leetcode_056.csproj](leetcode_056/leetcode_056/leetcode_056.csproj) | net8.0 |
| 57 | [leetcode_057](leetcode_057/) | [README](leetcode_057/README.md) | [leetcode_057.csproj](leetcode_057/leetcode_057/leetcode_057.csproj) | net8.0 |
| 58 | [leetcode_058](leetcode_058/) | [README](leetcode_058/README.md) | [leetcode_058.csproj](leetcode_058/leetcode_058/leetcode_058.csproj) | net10.0 |
| 61 | [leetcode_061](leetcode_061/) | [README](leetcode_061/README.md) | [leetcode_061.csproj](leetcode_061/leetcode_061/leetcode_061.csproj) | net10.0 |
| 62 | [leetcode_062](leetcode_062/) | 未補 | [leetcode_062.csproj](leetcode_062/leetcode_062/leetcode_062.csproj) | net8.0 |
| 66 | [leetcode_066](leetcode_066/) | [README](leetcode_066/README.md) | [leetcode_066.csproj](leetcode_066/leetcode_066/leetcode_066.csproj) | net10.0 |
| 67 | [leetcode_067](leetcode_067/) | 未補 | [leetcode_067.csproj](leetcode_067/leetcode_067/leetcode_067.csproj) | net8.0 |
| 69 | [leetcode_069](leetcode_069/) | [README](leetcode_069/README.md) | [leetcode_069.csproj](leetcode_069/leetcode_069/leetcode_069.csproj) | net10.0 |
| 70 | [leetcode_070](leetcode_070/) | 未補 | [leetcode_070.csproj](leetcode_070/leetcode_070/leetcode_070.csproj) | net8.0 |
| 71 | [leetcode_071](leetcode_071/) | [README](leetcode_071/README.md) | [leetcode_071.csproj](leetcode_071/leetcode_071/leetcode_071.csproj) | net10.0 |
| 73 | [leetcode_073](leetcode_073/) | 未補 | [leetcode_073.csproj](leetcode_073/leetcode_073/leetcode_073.csproj) | net8.0 |
| 74 | [leetcode_074](leetcode_074/) | [README](leetcode_074/README.md) | [leetcode_074.csproj](leetcode_074/leetcode_074/leetcode_074.csproj) | net10.0 |
| 75 | [leetcode_075](leetcode_075/) | 未補 | [leetcode_075.csproj](leetcode_075/leetcode_075/leetcode_075.csproj) | net8.0 |
| 75 | [leetcode_75](leetcode_75/) | [README](leetcode_75/README.md) | [leetcode_75.csproj](leetcode_75/leetcode_75/leetcode_75.csproj) | net8.0 |
| 76 | [leetcode_076](leetcode_076/) | 未補 | [leetcode_076.csproj](leetcode_076/leetcode_076/leetcode_076.csproj) | net8.0 |
| 78 | [leetcode_078](leetcode_078/) | 未補 | [leetcode_078.csproj](leetcode_078/leetcode_078/leetcode_078.csproj) | net8.0 |
| 79 | [leetcode_079](leetcode_079/) | 未補 | [leetcode_079.csproj](leetcode_079/leetcode_079/leetcode_079.csproj) | net8.0 |
| 80 | [leetcode_080](leetcode_080/) | [README](leetcode_080/README.md) | [leetcode_080.csproj](leetcode_080/leetcode_080/leetcode_080.csproj) | net10.0 |
| 81 | [leetcode_081](leetcode_081/) | [README](leetcode_081/README.md) | [leetcode_081.csproj](leetcode_081/leetcode_081/leetcode_081.csproj) | net10.0 |
| 82 | [leetcode_082](leetcode_082/) | [README](leetcode_082/README.md) | [leetcode_082.csproj](leetcode_082/leetcode_082/leetcode_082.csproj) | net10.0 |
| 83 | [leetcode_083](leetcode_083/) | [README](leetcode_083/README.md) | [leetcode_083.csproj](leetcode_083/leetcode_083/leetcode_083.csproj) | net10.0 |
| 84 | [leetcode_084](leetcode_084/) | 未補 | [leetcode_084.csproj](leetcode_084/leetcode_084/leetcode_084.csproj) | net8.0 |
| 86 | [leetcode_086](leetcode_086/) | [README](leetcode_086/README.md) | [leetcode_086.csproj](leetcode_086/leetcode_086/leetcode_086.csproj) | net10.0 |
| 88 | [leetcode_088](leetcode_088/) | [README](leetcode_088/README.md) | [leetcode_088.csproj](leetcode_088/leetcode_088/leetcode_088.csproj) | net10.0 |
| 91 | [leetcode_091](leetcode_091/) | 未補 | [leetcode_091.csproj](leetcode_091/leetcode_091/leetcode_091.csproj) | net8.0 |
| 94 | [leetcode_094](leetcode_094/) | [README](leetcode_094/README.md) | [leetcode_094.csproj](leetcode_094/leetcode_094/leetcode_094.csproj) | net10.0 |
| 98 | [leetcode_98](leetcode_98/) | [README](leetcode_98/README.md) | [leetcode_98.csproj](leetcode_98/leetcode_98/leetcode_98.csproj) | net8.0 |
| 100 | [leetcode_100](leetcode_100/) | 未補 | [leetcode_100.csproj](leetcode_100/leetcode_100/leetcode_100.csproj) | net8.0 |
| 101 | [leetcode_101](leetcode_101/) | [README](leetcode_101/README.md) | [leetcode_101.csproj](leetcode_101/leetcode_101/leetcode_101.csproj) | net10.0 |
| 102 | [leetcode_102](leetcode_102/) | 未補 | [leetcode_102.csproj](leetcode_102/leetcode_102/leetcode_102.csproj) | net8.0 |
| 104 | [leetcode_104](leetcode_104/) | 未補 | [leetcode_104.csproj](leetcode_104/leetcode_104/leetcode_104.csproj) | net8.0 |
| 105 | [leetcode_105](leetcode_105/) | [README](leetcode_105/README.md) | [leetcode_105.csproj](leetcode_105/leetcode_105/leetcode_105.csproj) | net8.0 |
| 108 | [leetcode_108](leetcode_108/) | [README](leetcode_108/README.md) | [leetcode_108.csproj](leetcode_108/leetcode_108/leetcode_108.csproj) | net10.0 |
| 110 | [leetcode_110](leetcode_110/) | [README](leetcode_110/README.md) | [leetcode_110.csproj](leetcode_110/leetcode_110/leetcode_110.csproj) | net10.0 |
| 111 | [leetcode_111](leetcode_111/) | [README](leetcode_111/README.md) | [leetcode_111.csproj](leetcode_111/leetcode_111/leetcode_111.csproj) | net10.0 |
| 112 | [leetcode_112](leetcode_112/) | [README](leetcode_112/README.md) | [leetcode_112.csproj](leetcode_112/leetcode_112/leetcode_112.csproj) | net10.0 |
| 114 | [leetcode_114](leetcode_114/) | [README](leetcode_114/README.md) | [leetcode_114.csproj](leetcode_114/leetcode_114/leetcode_114.csproj) | net10.0 |
| 118 | [leetcode_118](leetcode_118/) | [README](leetcode_118/README.md) | [leetcode_118.csproj](leetcode_118/leetcode_118/leetcode_118.csproj) | net8.0 |
| 121 | [leetcode_121](leetcode_121/) | 未補 | [leetcode_121.csproj](leetcode_121/leetcode_121/leetcode_121.csproj) | net8.0 |
| 122 | [leetcode_122](leetcode_122/) | 未補 | [leetcode_122.csproj](leetcode_122/leetcode_122/leetcode_122.csproj) | net8.0 |
| 125 | [leetcode_125](leetcode_125/) | 未補 | [leetcode_125.csproj](leetcode_125/leetcode_125/leetcode_125.csproj) | net8.0 |
| 127 | [leetcode_127](leetcode_127/) | 未補 | [leetcode_127.csproj](leetcode_127/leetcode_127/leetcode_127.csproj) | net8.0 |
| 128 | [leetcode_128](leetcode_128/) | [README](leetcode_128/README.md) | [leetcode_128.csproj](leetcode_128/leetcode_128/leetcode_128.csproj) | net10.0 |
| 129 | [leetcode_129](leetcode_129/) | 未補 | [leetcode_129.csproj](leetcode_129/leetcode_129/leetcode_129.csproj) | .NET Framework v4.8 |
| 133 | [leetcode_133](leetcode_133/) | [README](leetcode_133/README.md) | [leetcode_133.csproj](leetcode_133/leetcode_133/leetcode_133.csproj) | net10.0 |
| 134 | [leetcode_134](leetcode_134/) | 未補 | [leetcode_134.csproj](leetcode_134/leetcode_134/leetcode_134.csproj) | .NET Framework v4.8 |
| 135 | [leetcode_135](leetcode_135/) | [README](leetcode_135/README.md) | [leetcode_135.csproj](leetcode_135/leetcode_135/leetcode_135.csproj) | net8.0 |
| 136 | [leetcode_136](leetcode_136/) | [README](leetcode_136/README.md) | [leetcode_136.csproj](leetcode_136/leetcode_136/leetcode_136.csproj) | net10.0 |
| 137 | [leetcode_137](leetcode_137/) | 未補 | [leetcode_137.csproj](leetcode_137/leetcode_137/leetcode_137.csproj) | .NET Framework v4.8 |
| 139 | [leetcode_139](leetcode_139/) | 未補 | [leetcode_139.csproj](leetcode_139/leetcode_139/leetcode_139.csproj) | net8.0 |
| 141 | [leetcode_141](leetcode_141/) | 未補 | [leetcode_141.csproj](leetcode_141/leetcode_141/leetcode_141.csproj) | net8.0 |
| 142 | [leetcode_142](leetcode_142/) | [README](leetcode_142/README.md) | [leetcode_142.csproj](leetcode_142/leetcode_142/leetcode_142.csproj) | net10.0 |
| 143 | [leetcode_143](leetcode_143/) | 未補 | [leetcode_143.csproj](leetcode_143/leetcode_143/leetcode_143.csproj) | net8.0 |
| 144 | [leetcode_144](leetcode_144/) | [README](leetcode_144/README.md) | [leetcode_144.csproj](leetcode_144/leetcode_144/leetcode_144.csproj) | net10.0 |
| 145 | [leetcode_145](leetcode_145/) | [README](leetcode_145/README.md) | [leetcode_145.csproj](leetcode_145/leetcode_145/leetcode_145.csproj) | net10.0 |
| 146 | [leetcode_146](leetcode_146/) | [README](leetcode_146/README.md) | [leetcode_146.csproj](leetcode_146/leetcode_146/leetcode_146.csproj) | net8.0 |
| 148 | [leetcode_148](leetcode_148/) | 未補 | [leetcode_148.csproj](leetcode_148/leetcode_148/leetcode_148.csproj) | .NET Framework v4.8 |
| 150 | [leetcode_150](leetcode_150/) | 未補 | [leetcode_150.csproj](leetcode_150/leetcode_150/leetcode_150.csproj) | net8.0 |
| 151 | [leetcode_151](leetcode_151/) | [README](leetcode_151/README.md) | [leetcode_151.csproj](leetcode_151/leetcode_151/leetcode_151.csproj) | net10.0 |
| 152 | [leetcode_152](leetcode_152/) | 未補 | [leetcode_152.csproj](leetcode_152/leetcode_152/leetcode_152.csproj) | net8.0 |
| 153 | [leetcode_153](leetcode_153/) | [README](leetcode_153/README.md) | [leetcode_153.csproj](leetcode_153/leetcode_153/leetcode_153.csproj) | net8.0 |
| 155 | [leetcode_155](leetcode_155/) | [README](leetcode_155/README.md) | [leetcode_155.csproj](leetcode_155/leetcode_155/leetcode_155.csproj) | net8.0 |
| 165 | [leetcode_165](leetcode_165/) | [README](leetcode_165/README.md) | [leetcode_165.csproj](leetcode_165/leetcode_165/leetcode_165.csproj) | net8.0 |
| 166 | [leetcode_166](leetcode_166/) | [README](leetcode_166/README.md) | [leetcode_166.csproj](leetcode_166/leetcode_166/leetcode_166.csproj) | net8.0 |
| 167 | [leetcode_167](leetcode_167/) | 未補 | [leetcode_167.csproj](leetcode_167/leetcode_167/leetcode_167.csproj) | .NET Framework v4.8 |
| 169 | [leetcode_169](leetcode_169/) | 未補 | [leetcode_169.csproj](leetcode_169/leetcode_169/leetcode_169.csproj) | .NET Framework v4.8 |
| 187 | [leetcode_187](leetcode_187/) | 未補 | [leetcode_187.csproj](leetcode_187/leetcode_187/leetcode_187.csproj) | .NET Framework v4.8 |
| 189 | [leetcode_189](leetcode_189/) | 未補 | [leetcode_189.csproj](leetcode_189/leetcode_189/leetcode_189.csproj) | .NET Framework v4.8 |
| 190 | [leetcode_190](leetcode_190/) | [README](leetcode_190/README.md) | [leetcode_190.csproj](leetcode_190/leetcode_190/leetcode_190.csproj) | net10.0 |
| 191 | [leetcode_191](leetcode_191/) | 未補 | [leetcode_191.csproj](leetcode_191/leetcode_191/leetcode_191.csproj) | .NET Framework v4.8 |
| 198 | [leetcode_198](leetcode_198/) | 未補 | [leetcode_198.csproj](leetcode_198/leetcode_198/leetcode_198.csproj) | .NET Framework v4.8 |
| 199 | [leetcode_199](leetcode_199/) | 未補 | [leetcode_199.csproj](leetcode_199/leetcode_199/leetcode_199.csproj) | net8.0 |
| 200 | [leetcode_200](leetcode_200/) | 未補 | [leetcode_200.csproj](leetcode_200/leetcode_200/leetcode_200.csproj) | net8.0 |
| 202 | [leetcode_202](leetcode_202/) | 未補 | [leetcode_202.csproj](leetcode_202/leetcode_202/leetcode_202.csproj) | .NET Framework v4.8 |
| 203 | [leetcode_203](leetcode_203/) | 未補 | [leetcode_203.csproj](leetcode_203/leetcode_203/leetcode_203.csproj) | .NET Framework v4.8 |
| 205 | [leetcode_205](leetcode_205/) | 未補 | [leetcode_205.csproj](leetcode_205/leetcode_205/leetcode_205.csproj) | .NET Framework v4.8 |
| 206 | [leetcode_206](leetcode_206/) | 未補 | [leetcode_206.csproj](leetcode_206/leetcode_206/leetcode_206.csproj) | net8.0 |
| 207 | [leetcode_207](leetcode_207/) | 未補 | [leetcode_207.csproj](leetcode_207/leetcode_207/leetcode_207.csproj) | net8.0 |
| 208 | [leetcode_208](leetcode_208/) | 未補 | [leetcode_208.csproj](leetcode_208/leetcode_208/leetcode_208.csproj) | net8.0 |
| 209 | [leetcode_209](leetcode_209/) | 未補 | [leetcode_209.csproj](leetcode_209/leetcode_209/leetcode_209.csproj) | .NET Framework v4.8 |
| 211 | [leetcode_211](leetcode_211/) | 未補 | [leetcode_211.csproj](leetcode_211/leetcode_211/leetcode_211.csproj) | net8.0 |
| 212 | [leetcode_212](leetcode_212/) | 未補 | [leetcode_212.csproj](leetcode_212/leetcode_212/leetcode_212.csproj) | net8.0 |
| 213 | [leetcode_213](leetcode_213/) | 未補 | [leetcode_213.csproj](leetcode_213/leetcode_213/leetcode_213.csproj) | net8.0 |
| 215 | [leetcode_215](leetcode_215/) | 未補 | [leetcode_215.csproj](leetcode_215/leetcode_215/leetcode_215.csproj) | .NET Framework v4.8 |
| 217 | [leetcode_217](leetcode_217/) | 未補 | [leetcode_217.csproj](leetcode_217/leetcode_217/leetcode_217.csproj) | net8.0 |
| 219 | [leetcode_219](leetcode_219/) | 未補 | [leetcode_219.csproj](leetcode_219/leetcode_219/leetcode_219.csproj) | .NET Framework v4.8 |
| 222 | [leetcode_222](leetcode_222/) | 未補 | [leetcode_222.csproj](leetcode_222/leetcode_222/leetcode_222.csproj) | .NET Framework v4.8 |
| 224 | [leetcode_224](leetcode_224/) | [README](leetcode_224/README.md) | [leetcode_224.csproj](leetcode_224/leetcode_224/leetcode_224.csproj) | net9.0 |
| 225 | [leetcode_225](leetcode_225/) | 未補 | [leetcode_225.csproj](leetcode_225/leetcode_225/leetcode_225.csproj) | .NET Framework v4.8 |
| 226 | [leetcode_226](leetcode_226/) | 未補 | [leetcode_226.csproj](leetcode_226/leetcode_226/leetcode_226.csproj) | net8.0 |
| 228 | [leetcode_228](leetcode_228/) | 未補 | [leetcode_228.csproj](leetcode_228/leetcode_228/leetcode_228.csproj) | .NET Framework v4.8 |
| 230 | [leetcode_230](leetcode_230/) | [README](leetcode_230/README.md) | [leetcode_230.csproj](leetcode_230/leetcode_230/leetcode_230.csproj) | net8.0 |
| 231 | [leetcode_231](leetcode_231/) | [README](leetcode_231/README.md) | [leetcode_231.csproj](leetcode_231/leetcode_231/leetcode_231.csproj) | net8.0 |
| 232 | [leetcode_232](leetcode_232/) | [README](leetcode_232/README.md) | [leetcode_232.csproj](leetcode_232/leetcode_232/leetcode_232.csproj) | net8.0 |
| 234 | [leetcode_234](leetcode_234/) | [README](leetcode_234/README.md) | [leetcode_234.csproj](leetcode_234/leetcode_234/leetcode_234.csproj) | net10.0 |
| 235 | [leetcode_235](leetcode_235/) | 未補 | [leetcode_235.csproj](leetcode_235/leetcode_235/leetcode_235.csproj) | net8.0 |
| 236 | [leetcode_236](leetcode_236/) | 未補 | [leetcode_236.csproj](leetcode_236/leetcode_236/leetcode_236.csproj) | net8.0 |
| 238 | [leetcode_238](leetcode_238/) | 未補 | [leetcode_238.csproj](leetcode_238/leetcode_238/leetcode_238.csproj) | net8.0 |
| 240 | [leetcode_240](leetcode_240/) | [README](leetcode_240/README.md) | [leetcode_240.csproj](leetcode_240/leetcode_240/leetcode_240.csproj) | net8.0 |
| 241 | [leetcode_241](leetcode_241/) | [README](leetcode_241/README.md) | [leetcode_241.csproj](leetcode_241/leetcode_241/leetcode_241.csproj) | net8.0 |
| 242 | [leetcode_242](leetcode_242/) | 未補 | [leetcode_242.csproj](leetcode_242/leetcode_242/leetcode_242.csproj) | net8.0 |
| 252 | [leetcode_252](leetcode_252/) | 未補 | [leetcode_252.csproj](leetcode_252/leetcode_252/leetcode_252.csproj) | net8.0 |
| 260 | [leetcode_260](leetcode_260/) | 未補 | [leetcode_260.csproj](leetcode_260/leetcode_260/leetcode_260.csproj) | .NET Framework v4.8 |
| 268 | [leetcode_268](leetcode_268/) | 未補 | [leetcode_268.csproj](leetcode_268/leetcode_268/leetcode_268.csproj) | .NET Framework v4.8 |
| 273 | [leetcode_0273](leetcode_0273/) | 未補 | [leetcode_0273.csproj](leetcode_0273/leetcode_0273/leetcode_0273.csproj) | net8.0 |
| 274 | [leetcode_274](leetcode_274/) | 未補 | [leetcode_274.csproj](leetcode_274/leetcode_274/leetcode_274.csproj) | .NET Framework v4.8 |
| 278 | [leetcode_278](leetcode_278/) | 未補 | [leetcode_278.csproj](leetcode_278/leetcode_278/leetcode_278.csproj) | net8.0 |
| 279 | [leetcode_279](leetcode_279/) | [README](leetcode_279/README.md) | [leetcode_279.csproj](leetcode_279/leetcode_279/leetcode_279.csproj) | net10.0 |
| 283 | [leetcode_283](leetcode_283/) | [README](leetcode_283/README.md) | [leetcode_283.csproj](leetcode_283/leetcode_283/leetcode_283.csproj) | net10.0 |
| 287 | [leetcode_287](leetcode_287/) | 未補 | [leetcode_287.csproj](leetcode_287/leetcode_287/leetcode_287.csproj) | .NET Framework v4.8 |
| 290 | [leetcode_290](leetcode_290/) | 未補 | [leetcode_290.csproj](leetcode_290/leetcode_290/leetcode_290.csproj) | .NET Framework v4.8 |
| 295 | [leetcode_295](leetcode_295/) | 未補 | [leetcode_295.csproj](leetcode_295/leetcode_295/leetcode_295.csproj) | net9.0 |
| 297 | [leetcode_297](leetcode_297/) | 未補 | [leetcode_297.csproj](leetcode_297/leetcode_297/leetcode_297.csproj) | net9.0 |
| 300 | [leetcode_300](leetcode_300/) | 未補 | [leetcode_300.csproj](leetcode_300/leetcode_300/leetcode_300.csproj) | net8.0 |
| 310 | [leetcode_310](leetcode_310/) | 未補 | [leetcode_310.csproj](leetcode_310/leetcode_310/leetcode_310.csproj) | net8.0 |
| 316 | [leetcode_316](leetcode_316/) | [README](leetcode_316/README.md) | [leetcode_316.csproj](leetcode_316/leetcode_316/leetcode_316.csproj) | net10.0 |
| 318 | [leetcode_318](leetcode_318/) | 未補 | [leetcode_318.csproj](leetcode_318/leetcode_318/leetcode_318.csproj) | .NET Framework v4.8 |
| 322 | [leetcode_322](leetcode_322/) | 未補 | [leetcode_322.csproj](leetcode_322/leetcode_322/leetcode_322.csproj) | net8.0 |
| 326 | [leetcode_326](leetcode_326/) | [README](leetcode_326/README.md) | [leetcode_326.csproj](leetcode_326/leetcode_326/leetcode_326.csproj) | net8.0 |
| 328 | [leetcode_328](leetcode_328/) | [README](leetcode_328/README.md) | [leetcode_328.csproj](leetcode_328/leetcode_328/leetcode_328.csproj) | net10.0 |
| 338 | [leetcode_338](leetcode_338/) | 未補 | [leetcode_338.csproj](leetcode_338/leetcode_338/leetcode_338.csproj) | net8.0 |
| 342 | [leetcode_342](leetcode_342/) | [README](leetcode_342/README.md) | [leetcode_342.csproj](leetcode_342/leetcode_342/leetcode_342.csproj) | net8.0 |
| 344 | [leetcode_344](leetcode_344/) | [README](leetcode_344/README.md) | [leetcode_344.csproj](leetcode_344/leetcode_344/leetcode_344.csproj) | net10.0 |
| 345 | [leetcode_345](leetcode_345/) | 未補 | [leetcode_345.csproj](leetcode_345/leetcode_345/leetcode_345.csproj) | .NET Framework v4.8 |
| 347 | [leetcode_347](leetcode_347/) | 未補 | [leetcode_347.csproj](leetcode_347/leetcode_347/leetcode_347.csproj) | .NET Framework v4.8 |
| 349 | [leetcode_349](leetcode_349/) | 未補 | [leetcode_349.csproj](leetcode_349/leetcode_349/leetcode_349.csproj) | net8.0 |
| 350 | [leetcode_350](leetcode_350/) | 未補 | [leetcode_350.csproj](leetcode_350/leetcode_350/leetcode_350.csproj) | net8.0 |
| 371 | [leetcode_371](leetcode_371/) | 未補 | [leetcode_371.csproj](leetcode_371/leetcode_371/leetcode_371.csproj) | net8.0 |
| 374 | [leetcode_374](leetcode_374/) | [README](leetcode_374/README.md) | [leetcode_374.csproj](leetcode_374/leetcode_374/leetcode_374.csproj) | net10.0 |
| 380 | [leetcode_380](leetcode_380/) | 未補 | [leetcode_380.csproj](leetcode_380/leetcode_380/leetcode_380.csproj) | .NET Framework v4.8 |
| 383 | [leetcode_383](leetcode_383/) | 未補 | [leetcode_383.csproj](leetcode_383/leetcode_383/leetcode_383.csproj) | net8.0 |
| 387 | [leetcode_387](leetcode_387/) | [README](leetcode_387/README.md) | [leetcode_387.csproj](leetcode_387/leetcode_387/leetcode_387.csproj) | net10.0 |
| 389 | [leetcode_389](leetcode_389/) | 未補 | [leetcode_389.csproj](leetcode_389/leetcode_389/leetcode_389.csproj) | .NET Framework v4.8 |
| 392 | [leetcode_392](leetcode_392/) | [README](leetcode_392/README.md) | [leetcode_392.csproj](leetcode_392/leetcode_392/leetcode_392.csproj) | net10.0 |
| 396 | [leetcode_396](leetcode_396/) | [README](leetcode_396/README.md) | [leetcode_396.csproj](leetcode_396/leetcode_396/leetcode_396.csproj) | net10.0 |
| 401 | [leetcode_401](leetcode_401/) | [README](leetcode_401/README.md) | [leetcode_401.csproj](leetcode_401/leetcode_401/leetcode_401.csproj) | net10.0 |
| 402 | [leetcode_402](leetcode_402/) | 未補 | [leetcode_402.csproj](leetcode_402/leetcode_402/leetcode_402.csproj) | net8.0 |
| 409 | [leetcode_409](leetcode_409/) | 未補 | [leetcode_409.csproj](leetcode_409/leetcode_409/leetcode_409.csproj) | net8.0 |
| 412 | [leetcode_412](leetcode_412/) | 未補 | [leetcode_412.csproj](leetcode_412/leetcode_412/leetcode_412.csproj) | .NET Framework v4.8 |
| 416 | [leetcode_416](leetcode_416/) | 未補 | [leetcode_416.csproj](leetcode_416/leetcode_416/leetcode_416.csproj) | net8.0 |
| 417 | [leetcode_417](leetcode_417/) | [README](leetcode_417/README.md) | [leetcode_417.csproj](leetcode_417/leetcode_417/leetcode_417.csproj) | net8.0 |
| 421 | [leetcode_421](leetcode_421/) | 未補 | [leetcode_421.csproj](leetcode_421/leetcode_421/leetcode_421.csproj) | .NET Framework v4.8 |
| 424 | [leetcode_424](leetcode_424/) | 未補 | [leetcode_424.csproj](leetcode_424/leetcode_424/leetcode_424.csproj) | net8.0 |
| 435 | [leetcode_435](leetcode_435/) | 未補 | [leetcode_435.csproj](leetcode_435/leetcode_435/leetcode_435.csproj) | .NET Framework v4.8 |
| 438 | [leetcode_438](leetcode_438/) | 未補 | [leetcode_438.csproj](leetcode_438/leetcode_438/leetcode_438.csproj) | net8.0 |
| 440 | [leetcode_440](leetcode_440/) | [README](leetcode_440/README.md) | [leetcode_440.csproj](leetcode_440/leetcode_440/leetcode_440.csproj) | net8.0 |
| 442 | [leetcode_442](leetcode_442/) | 未補 | [leetcode_442.csproj](leetcode_442/leetcode_442/leetcode_442.csproj) | net8.0 |
| 443 | [leetcode_443](leetcode_443/) | 未補 | [leetcode_443.csproj](leetcode_443/leetcode_443/leetcode_443.csproj) | .NET Framework v4.8 |
| 445 | [leetcode_445](leetcode_445/) | 未補 | [leetcode_445.csproj](leetcode_445/leetcode_445/leetcode_445.csproj) | .NET Framework v4.8 |
| 448 | [leetcode_448](leetcode_448/) | 未補 | [leetcode_448.csproj](leetcode_448/leetcode_448/leetcode_448.csproj) | .NET Framework v4.8 |
| 451 | [leetcode_451](leetcode_451/) | [README](leetcode_451/README.md) | [leetcode_451.csproj](leetcode_451/leetcode_451/leetcode_451.csproj) | net10.0 |
| 452 | [leetcode_452](leetcode_452/) | 未補 | [leetcode_452.csproj](leetcode_452/leetcode_452/leetcode_452.csproj) | .NET Framework v4.8 |
| 474 | [leetcode_474](leetcode_474/) | [README](leetcode_474/README.md) | [leetcode_474.csproj](leetcode_474/leetcode_474/leetcode_474.csproj) | net8.0 |
| 476 | [leetcode_476](leetcode_476/) | 未補 | [leetcode_476.csproj](leetcode_476/leetcode_476/leetcode_476.csproj) | net8.0 |
| 491 | [leetcode_491](leetcode_491/) | [README](leetcode_491/README.md) | [leetcode_491.csproj](leetcode_491/leetcode_491/leetcode_491.csproj) | net10.0 |
| 501 | [leetcode_501](leetcode_501/) | 未補 | [leetcode_501.csproj](leetcode_501/leetcode_501/leetcode_501.csproj) | .NET Framework v4.8 |
| 502 | [leetcode_502](leetcode_502/) | 未補 | [leetcode_502.csproj](leetcode_502/leetcode_502/leetcode_502.csproj) | .NET Framework v4.8 |
| 506 | [leetcode_506](leetcode_506/) | 未補 | [leetcode_506.csproj](leetcode_506/leetcode_506/leetcode_506.csproj) | net8.0 |
| 515 | [leetcode_515](leetcode_515/) | 未補 | [leetcode_515.csproj](leetcode_515/leetcode_515/leetcode_515.csproj) | .NET Framework v4.8 |
| 516 | [leetcode_516](leetcode_516/) | 未補 | [leetcode_516.csproj](leetcode_516/leetcode_516/leetcode_516.csproj) | .NET Framework v4.8 |
| 530 | [leetcode_530](leetcode_530/) | 未補 | [leetcode_530.csproj](leetcode_530/leetcode_530/leetcode_530.csproj) | .NET Framework v4.8 |
| 540 | [leetcode_540](leetcode_540/) | [README](leetcode_540/README.md) | [leetcode_540.csproj](leetcode_540/leetcode_540/leetcode_540.csproj) | net10.0 |
| 542 | [leetcode_542](leetcode_542/) | 未補 | [leetcode_542.csproj](leetcode_542/leetcode_542/leetcode_542.csproj) | net8.0 |
| 543 | [leetcode_543](leetcode_543/) | 未補 | [leetcode_543.csproj](leetcode_543/leetcode_543/leetcode_543.csproj) | net8.0 |
| 560 | [leetcode_560](leetcode_560/) | [README](leetcode_560/README.md) | [leetcode_560.csproj](leetcode_560/leetcode_560/leetcode_560.csproj) | net10.0 |
| 567 | [leetcode_567](leetcode_567/) | [README](leetcode_567/README.md) | [leetcode_567.csproj](leetcode_567/leetcode_567/leetcode_567.csproj) | net10.0 |
| 572 | [leetcode_572](leetcode_572/) | 未補 | [leetcode_572.csproj](leetcode_572/leetcode_572/leetcode_572.csproj) | net8.0 |
| 589 | [leetcode_589](leetcode_589/) | 未補 | [leetcode_589.csproj](leetcode_589/leetcode_589/leetcode_589.csproj) | .NET Framework v4.8 |
| 590 | [leetcode_590](leetcode_590/) | 未補 | [leetcode_590.csproj](leetcode_590/leetcode_590/leetcode_590.csproj) | .NET Framework v4.8 |
| 592 | [leetcode_592](leetcode_592/) | 未補 | [leetcode_592.csproj](leetcode_592/leetcode_592/leetcode_592.csproj) | net8.0 |
| 594 | [leetcode_594](leetcode_594/) | [README](leetcode_594/README.md) | [leetcode_594.csproj](leetcode_594/leetcode_594/leetcode_594.csproj) | net8.0 |
| 605 | [leetcode_605](leetcode_605/) | 未補 | [leetcode_605.csproj](leetcode_605/leetcode_605/leetcode_605.csproj) | net8.0 |
| 617 | [leetcode_617](leetcode_617/) | [README](leetcode_617/README.md) | [leetcode_617.csproj](leetcode_617/leetcode_617/leetcode_617.csproj) | net10.0 |
| 621 | [leetcode_621](leetcode_621/) | 未補 | [leetcode_621.csproj](leetcode_621/leetcode_621/leetcode_621.csproj) | net8.0 |
| 633 | [leetcode_633](leetcode_633/) | 未補 | [leetcode_633.csproj](leetcode_633/leetcode_633/leetcode_633.csproj) | net8.0 |
| 643 | [leetcode_643](leetcode_643/) | 未補 | [leetcode_643.csproj](leetcode_643/leetcode_643/leetcode_643.csproj) | .NET Framework v4.8 |
| 645 | [leetcode_645](leetcode_645/) | 未補 | [leetcode_645.csproj](leetcode_645/leetcode_645/leetcode_645.csproj) | .NET Framework v4.8 |
| 647 | [leetcode_647](leetcode_647/) | [README](leetcode_647/README.md) | [leetcode_647.csproj](leetcode_647/leetcode_647/leetcode_647.csproj) | net10.0 |
| 650 | [leetcode_650](leetcode_650/) | 未補 | [leetcode_650.csproj](leetcode_650/leetcode_650/leetcode_650.csproj) | net8.0 |
| 653 | [leetcode_653](leetcode_653/) | 未補 | [leetcode_653.csproj](leetcode_653/leetcode_653/leetcode_653.csproj) | .NET Framework v4.8 |
| 655 | [leetcode_655](leetcode_655/) | [README](leetcode_655/README.md) | [leetcode_655.csproj](leetcode_655/leetcode_655/leetcode_655.csproj) | net10.0 |
| 657 | [leetcode_657](leetcode_657/) | [README](leetcode_657/README.md) | [leetcode_657.csproj](leetcode_657/leetcode_657/leetcode_657.csproj) | net10.0 |
| 662 | [leetcode_662](leetcode_662/) | 未補 | [leetcode_662.csproj](leetcode_662/leetcode_662/leetcode_662.csproj) | .NET Framework v4.8 |
| 669 | [leetcode_669](leetcode_669/) | [README](leetcode_669/README.md) | [leetcode_669.csproj](leetcode_669/leetcode_669/leetcode_669.csproj) | net10.0 |
| 678 | [leetcode_678](leetcode_678/) | 未補 | [leetcode_678.csproj](leetcode_678/leetcode_678/leetcode_678.csproj) | net8.0 |
| 684 | [leetcode_684](leetcode_684/) | 未補 | [leetcode_684.csproj](leetcode_684/leetcode_684/leetcode_684.csproj) | .NET Framework v4.8 |
| 696 | [leetcode_696](leetcode_696/) | [README](leetcode_696/README.md) | [leetcode_696.csproj](leetcode_696/leetcode_696/leetcode_696.csproj) | net10.0 |
| 700 | [leetcode_700](leetcode_700/) | [README](leetcode_700/README.md) | [leetcode_700.csproj](leetcode_700/leetcode_700/leetcode_700.csproj) | net10.0 |
| 704 | [leetcode_704](leetcode_704/) | 未補 | [leetcode_704.csproj](leetcode_704/leetcode_704/leetcode_704.csproj) | net8.0 |
| 705 | [leetcode_705](leetcode_705/) | 未補 | [leetcode_705.csproj](leetcode_705/leetcode_705/leetcode_705.csproj) | .NET Framework v4.8 |
| 713 | [leetcode_713](leetcode_713/) | 未補 | [leetcode_713.csproj](leetcode_713/leetcode_713/leetcode_713.csproj) | net8.0 |
| 717 | [leetcode_717](leetcode_717/) | [README](leetcode_717/README.md) | [leetcode_717.csproj](leetcode_717/leetcode_717/leetcode_717.csproj) | net8.0 |
| 721 | [leetcode_721](leetcode_721/) | 未補 | [leetcode_721.csproj](leetcode_721/leetcode_721/leetcode_721.csproj) | net8.0 |
| 726 | [leetcode_726](leetcode_726/) | 未補 | [leetcode_726.csproj](leetcode_726/leetcode_726/leetcode_726.csproj) | net8.0 |
| 733 | [leetcode_733](leetcode_733/) | 未補 | [leetcode_733.csproj](leetcode_733/leetcode_733/leetcode_733.csproj) | net8.0 |
| 735 | [leetcode_735](leetcode_735/) | 未補 | [leetcode_735.csproj](leetcode_735/leetcode_735/leetcode_735.csproj) | .NET Framework v4.8 |
| 739 | [leetcode_739](leetcode_739/) | 未補 | [leetcode_739.csproj](leetcode_739/leetcode_739/leetcode_739.csproj) | .NET Framework v4.8 |
| 744 | [leetcode_744](leetcode_744/) | 未補 | [leetcode_744.csproj](leetcode_744/leetcode_744/leetcode_744.csproj) | .NET Framework v4.8 |
| 746 | [leetcode_746](leetcode_746/) | 未補 | [leetcode_746.csproj](leetcode_746/leetcode_746/leetcode_746.csproj) | .NET Framework v4.8 |
| 752 | [leetcode_752](leetcode_752/) | 未補 | [leetcode_752.csproj](leetcode_752/leetcode_752/leetcode_752.csproj) | net8.0 |
| 757 | [leetcode_757](leetcode_757/) | [README](leetcode_757/README.md) | [leetcode_757.csproj](leetcode_757/leetcode_757/leetcode_757.csproj) | net8.0 |
| 762 | [leetcode_762](leetcode_762/) | [README](leetcode_762/README.md) | [leetcode_762.csproj](leetcode_762/leetcode_762/leetcode_762.csproj) | net10.0 |
| 763 | [leetcode_763](leetcode_763/) | [README](leetcode_763/README.md) | [leetcode_763.csproj](leetcode_763/leetcode_763/leetcode_763.csproj) | net10.0 |
| 767 | [leetcode_767](leetcode_767/) | 未補 | [leetcode_767.csproj](leetcode_767/leetcode_767/leetcode_767.csproj) | .NET Framework v4.8 |
| 769 | [leetcode_769](leetcode_769/) | 未補 | [leetcode_769.csproj](leetcode_769/leetcode_769/leetcode_769.csproj) | net8.0 |
| 771 | [leetcode_771](leetcode_771/) | [README](leetcode_771/README.md) | [leetcode_771.csproj](leetcode_771/leetcode_771/leetcode_771.csproj) | net10.0 |
| 779 | [leetcode_779](leetcode_779/) | 未補 | [leetcode_779.csproj](leetcode_779/leetcode_779/leetcode_779.csproj) | .NET Framework v4.8 |
| 783 | [leetcode_783](leetcode_783/) | [README](leetcode_783/README.md) | [leetcode_783.csproj](leetcode_783/leetcode_783/leetcode_783.csproj) | net10.0 |
| 786 | [leetcode_786](leetcode_786/) | 未補 | [leetcode_786.csproj](leetcode_786/leetcode_786/leetcode_786.csproj) | net8.0 |
| 788 | [leetcode_788](leetcode_788/) | [README](leetcode_788/README.md) | [leetcode_788.csproj](leetcode_788/leetcode_788/leetcode_788.csproj) | net10.0 |
| 804 | [leetcode_804](leetcode_804/) | [README](leetcode_804/README.md) | [leetcode_804.csproj](leetcode_804/leetcode_804/leetcode_804.csproj) | net10.0 |
| 808 | [leetcode_808](leetcode_808/) | [README](leetcode_808/README.md) | [leetcode_808.csproj](leetcode_808/leetcode_808/leetcode_808.csproj) | net8.0 |
| 812 | [leetcode_812](leetcode_812/) | [README](leetcode_812/README.md) | [leetcode_812.csproj](leetcode_812/leetcode_812/leetcode_812.csproj) | net8.0 |
| 819 | [leetcode_819](leetcode_819/) | 未補 | [leetcode_819.csproj](leetcode_819/leetcode_819/leetcode_819.csproj) | .NET Framework v4.8 |
| 837 | [leetcode_837](leetcode_837/) | [README](leetcode_837/README.md) | [leetcode_837.csproj](leetcode_837/leetcode_837/leetcode_837.csproj) | net8.0 |
| 840 | [leetcode_840](leetcode_840/) | 未補 | [leetcode_840.csproj](leetcode_840/leetcode_840/leetcode_840.csproj) | net8.0 |
| 856 | [leetcode_856](leetcode_856/) | [README](leetcode_856/README.md) | [leetcode_856.csproj](leetcode_856/leetcode_856/leetcode_856.csproj) | net10.0 |
| 859 | [leetcode_859](leetcode_859/) | 未補 | [leetcode_859.csproj](leetcode_859/leetcode_859/leetcode_859.csproj) | .NET Framework v4.8 |
| 860 | [leetcode_860](leetcode_860/) | 未補 | [leetcode_860.csproj](leetcode_860/leetcode_860/leetcode_860.csproj) | net8.0 |
| 861 | [leetcode_861](leetcode_861/) | 未補 | [leetcode_861.csproj](leetcode_861/leetcode_861/leetcode_861.csproj) | net8.0 |
| 865 | [leetcode_865](leetcode_865/) | [README](leetcode_865/README.md) | [leetcode_865.csproj](leetcode_865/leetcode_865/leetcode_865.csproj) | net10.0 |
| 868 | [leetcode_868](leetcode_868/) | [README](leetcode_868/README.md) | [leetcode_868.csproj](leetcode_868/leetcode_868/leetcode_868.csproj) | net10.0 |
| 869 | [leetcode_869](leetcode_869/) | [README](leetcode_869/README.md) | [leetcode_869.csproj](leetcode_869/leetcode_869/leetcode_869.csproj) | net8.0 |
| 872 | [leetcode_872](leetcode_872/) | 未補 | [leetcode_872.csproj](leetcode_872/leetcode_872/leetcode_872.csproj) | .NET Framework v4.8 |
| 875 | [leetcode_875](leetcode_875/) | 未補 | [leetcode_875.csproj](leetcode_875/leetcode_875/leetcode_875.csproj) | .NET Framework v4.8 |
| 876 | [leetcode_876](leetcode_876/) | 未補 | [leetcode_876.csproj](leetcode_876/leetcode_876/leetcode_876.csproj) | net8.0 |
| 885 | [leetcode_0885](leetcode_0885/) | 未補 | [leetcode_0885.csproj](leetcode_0885/leetcode_0885/leetcode_0885.csproj) | net8.0 |
| 886 | [leetcode_886](leetcode_886/) | [README](leetcode_886/README.md) | [leetcode_886.csproj](leetcode_886/leetcode_886/leetcode_886.csproj) | net10.0 |
| 898 | [leetcode_898](leetcode_898/) | [README](leetcode_898/README.md) | [leetcode_898.csproj](leetcode_898/leetcode_898/leetcode_898.csproj) | net8.0 |
| 904 | [leetcode_904](leetcode_904/) | [README](leetcode_904/README.md) | [leetcode_904.csproj](leetcode_904/leetcode_904/leetcode_904.csproj) | net10.0 |
| 912 | [leetcode_912](leetcode_912/) | 未補 | [leetcode_912.csproj](leetcode_912/leetcode_912/leetcode_912.csproj) | .NET Framework v4.8 |
| 930 | [leetcode_930](leetcode_930/) | 未補 | [leetcode_930.csproj](leetcode_930/leetcode_930/leetcode_930.csproj) | .NET Framework v4.8 |
| 931 | [leetcode_931](leetcode_931/) | 未補 | [leetcode_931.csproj](leetcode_931/leetcode_931/leetcode_931.csproj) | .NET Framework v4.8 |
| 938 | [leetcode_938](leetcode_938/) | [README](leetcode_938/README.md) | [leetcode_938.csproj](leetcode_938/leetcode_938/leetcode_938.csproj) | net10.0 |
| 944 | [leetcode_944](leetcode_944/) | [README](leetcode_944/README.md) | [leetcode_944.csproj](leetcode_944/leetcode_944/leetcode_944.csproj) | net10.0 |
| 946 | [leetcode_946](leetcode_946/) | [README](leetcode_946/README.md) | [leetcode_946.csproj](leetcode_946/leetcode_946/leetcode_946.csproj) | net10.0 |
| 947 | [leetcode_947](leetcode_947/) | [README](leetcode_947/README.md) | [leetcode_947.csproj](leetcode_947/leetcode_947/leetcode_947.csproj) | net10.0 |
| 953 | [leetcode_953](leetcode_953/) | [README](leetcode_953/README.md) | [leetcode_953.csproj](leetcode_953/leetcode_953/leetcode_953.csproj) | net10.0 |
| 955 | [leetcode_955](leetcode_955/) | [README](leetcode_955/README.md) | [leetcode_955.csproj](leetcode_955/leetcode_955/leetcode_955.csproj) | net10.0 |
| 961 | [leetcode_961](leetcode_961/) | 未補 | [leetcode_961.csproj](leetcode_961/leetcode_961/leetcode_961.csproj) | net8.0 |
| 973 | [leetcode_973](leetcode_973/) | 未補 | [leetcode_973.csproj](leetcode_973/leetcode_973/leetcode_973.csproj) | net8.0 |
| 974 | [leetcode_974](leetcode_974/) | 未補 | [leetcode_974.csproj](leetcode_974/leetcode_974/leetcode_974.csproj) | .NET Framework v4.8 |
| 976 | [leetcode_976](leetcode_976/) | [README](leetcode_976/README.md) | [leetcode_976.csproj](leetcode_976/leetcode_976/leetcode_976.csproj) | net8.0 |
| 977 | [leetcode_977](leetcode_977/) | 未補 | [leetcode_977.csproj](leetcode_977/leetcode_977/leetcode_977.csproj) | .NET Framework v4.8 |
| 979 | [leetcode_979](leetcode_979/) | 未補 | [leetcode_979.csproj](leetcode_979/leetcode_979/leetcode_979.csproj) | net8.0 |
| 989 | [leetcode_989](leetcode_989/) | [README](leetcode_989/README.md) | [leetcode_989.csproj](leetcode_989/leetcode_989/leetcode_989.csproj) | net10.0 |
| 994 | [leetcode_994](leetcode_994/) | 未補 | [leetcode_994.csproj](leetcode_994/leetcode_994/leetcode_994.csproj) | net8.0 |
| 997 | [leetcode_997](leetcode_997/) | 未補 | [leetcode_997.csproj](leetcode_997/leetcode_997/leetcode_997.csproj) | .NET Framework v4.8 |
| 1002 | [leetcode_1002](leetcode_1002/) | 未補 | [leetcode_1002.csproj](leetcode_1002/leetcode_1002/leetcode_1002.csproj) | net8.0 |
| 1009 | [leetcode_1009](leetcode_1009/) | [README](leetcode_1009/README.md) | [leetcode_1009.csproj](leetcode_1009/leetcode_1009/leetcode_1009.csproj) | net10.0 |
| 1010 | [leetcode_1010](leetcode_1010/) | [README](leetcode_1010/README.md) | [leetcode_1010.csproj](leetcode_1010/leetcode_1010/leetcode_1010.csproj) | net10.0 |
| 1011 | [leetcode_1011](leetcode_1011/) | [README](leetcode_1011/README.md) | [leetcode_1011.csproj](leetcode_1011/leetcode_1011/leetcode_1011.csproj) | net10.0 |
| 1013 | [leetcode_1013](leetcode_1013/) | [README](leetcode_1013/README.md) | [leetcode_1013.csproj](leetcode_1013/leetcode_1013/leetcode_1013.csproj) | net10.0 |
| 1015 | [leetcode_1015](leetcode_1015/) | [README](leetcode_1015/README.md) | [leetcode_1015.csproj](leetcode_1015/leetcode_1015/leetcode_1015.csproj) | net8.0 |
| 1018 | [leetcode_1018](leetcode_1018/) | [README](leetcode_1018/README.md) | [leetcode_1018.csproj](leetcode_1018/leetcode_1018/leetcode_1018.csproj) | net8.0 |
| 1022 | [leetcode_1022](leetcode_1022/) | [README](leetcode_1022/README.md) | [leetcode_1022.csproj](leetcode_1022/leetcode_1022/leetcode_1022.csproj) | net10.0 |
| 1026 | [leetcode_1026](leetcode_1026/) | [README](leetcode_1026/README.md) | [leetcode_1026.csproj](leetcode_1026/leetcode_1026/leetcode_1026.csproj) | net10.0 |
| 1027 | [leetcode_1027](leetcode_1027/) | [README](leetcode_1027/README.md) | [leetcode_1027.csproj](leetcode_1027/leetcode_1027/leetcode_1027.csproj) | net10.0 |
| 1038 | [leetcode_1038](leetcode_1038/) | 未補 | [leetcode_1038.csproj](leetcode_1038/leetcode_1038/leetcode_1038.csproj) | net8.0 |
| 1043 | [leetcode_1043](leetcode_1043/) | 未補 | [leetcode_1043.csproj](leetcode_1043/leetcode_1043/leetcode_1043.csproj) | .NET Framework v4.8 |
| 1046 | [leetcode_1046](leetcode_1046/) | [README](leetcode_1046/README.md) | [leetcode_1046.csproj](leetcode_1046/leetcode_1046/leetcode_1046.csproj) | net10.0 |
| 1051 | [leetcode_1051](leetcode_1051/) | 未補 | [leetcode_1051.csproj](leetcode_1051/leetcode_1051/leetcode_1051.csproj) | net8.0 |
| 1052 | [leetcode_1052](leetcode_1052/) | 未補 | [leetcode_1052.csproj](leetcode_1052/leetcode_1052/leetcode_1052.csproj) | net8.0 |
| 1061 | [leetcode_1061](leetcode_1061/) | [README](leetcode_1061/README.md) | [leetcode_1061.csproj](leetcode_1061/leetcode_1061/leetcode_1061.csproj) | net8.0 |
| 1071 | [leetcode_1071](leetcode_1071/) | 未補 | [leetcode_1071.csproj](leetcode_1071/leetcode_1071/leetcode_1071.csproj) | net8.0 |
| 1072 | [leetcode_1072](leetcode_1072/) | 未補 | [leetcode_1072.csproj](leetcode_1072/leetcode_1072/leetcode_1072.csproj) | net8.0 |
| 1079 | [leetcode_1079](leetcode_1079/) | [README](leetcode_1079/README.md) | [leetcode_1079.csproj](leetcode_1079/leetcode_1079/leetcode_1079.csproj) | net10.0 |
| 1105 | [leetcode_1105](leetcode_1105/) | 未補 | [leetcode_1105.csproj](leetcode_1105/leetcode_1105/leetcode_1105.csproj) | net8.0 |
| 1110 | [leetcode_1110](leetcode_1110/) | 未補 | [leetcode_1110.csproj](leetcode_1110/leetcode_1110/leetcode_1110.csproj) | net8.0 |
| 1137 | [leetcode_1137](leetcode_1137/) | [README](leetcode_1137/README.md) | [leetcode_1137.csproj](leetcode_1137/leetcode_1137/leetcode_1137.csproj) | net10.0 |
| 1143 | [leetcode_1143](leetcode_1143/) | [README](leetcode_1143/README.md) | [leetcode_1143.csproj](leetcode_1143/leetcode_1143/leetcode_1143.csproj) | net10.0 |
| 1160 | [leetcode_1160](leetcode_1160/) | 未補 | [leetcode_1160.csproj](leetcode_1160/leetcode_1160/leetcode_1160.csproj) | .NET Framework v4.8 |
| 1161 | [leetcode_1161](leetcode_1161/) | [README](leetcode_1161/README.md) | [leetcode_1161.csproj](leetcode_1161/leetcode_1161/leetcode_1161.csproj) | net10.0 |
| 1190 | [leetcode_1190](leetcode_1190/) | 未補 | [leetcode_1190.csproj](leetcode_1190/leetcode_1190/leetcode_1190.csproj) | net8.0 |
| 1200 | [leetcode_1200](leetcode_1200/) | [README](leetcode_1200/README.md) | [leetcode_1200.csproj](leetcode_1200/leetcode_1200/leetcode_1200.csproj) | net10.0 |
| 1207 | [leetcode_1207](leetcode_1207/) | 未補 | [leetcode_1207.csproj](leetcode_1207/leetcode_1207/leetcode_1207.csproj) | .NET Framework v4.8 |
| 1218 | [leetcode_1218](leetcode_1218/) | 未補 | [leetcode_1218.csproj](leetcode_1218/leetcode_1218/leetcode_1218.csproj) | .NET Framework v4.8 |
| 1219 | [leetcode_1219](leetcode_1219/) | 未補 | [leetcode_1219.csproj](leetcode_1219/leetcode_1219/leetcode_1219.csproj) | net8.0 |
| 1232 | [leetcode_1232](leetcode_1232/) | 未補 | [leetcode_1232.csproj](leetcode_1232/leetcode_1232/leetcode_1232.csproj) | .NET Framework v4.8 |
| 1235 | [leetcode_1235](leetcode_1235/) | 未補 | [leetcode_1235.csproj](leetcode_1235/leetcode_1235/leetcode_1235.csproj) | net8.0 |
| 1248 | [leetcode_1248](leetcode_1248/) | 未補 | [leetcode_1248.csproj](leetcode_1248/leetcode_1248/leetcode_1248.csproj) | net8.0 |
| 1249 | [leetcode_1249](leetcode_1249/) | [README](leetcode_1249/README.md) | [leetcode_1249.csproj](leetcode_1249/leetcode_1249/leetcode_1249.csproj) | net10.0 |
| 1262 | [leetcode_1262](leetcode_1262/) | [README](leetcode_1262/README.md) | [leetcode_1262.csproj](leetcode_1262/leetcode_1262/leetcode_1262.csproj) | net8.0 |
| 1266 | [leetcode_1266](leetcode_1266/) | [README](leetcode_1266/README.md) | [leetcode_1266.csproj](leetcode_1266/leetcode_1266/leetcode_1266.csproj) | net10.0 |
| 1277 | [leetcode_1277](leetcode_1277/) | [README](leetcode_1277/README.md) | [leetcode_1277.csproj](leetcode_1277/leetcode_1277/leetcode_1277.csproj) | net8.0 |
| 1290 | [leetcode_1290](leetcode_1290/) | [README](leetcode_1290/README.md) | [leetcode_1290.csproj](leetcode_1290/leetcode_1290/leetcode_1290.csproj) | net8.0 |
| 1291 | [leetcode_1291](leetcode_1291/) | 未補 | [leetcode_1291.csproj](leetcode_1291/leetcode_1291/leetcode_1291.csproj) | .NET Framework v4.8 |
| 1292 | [leetcode_1292](leetcode_1292/) | [README](leetcode_1292/README.md) | [leetcode_1292.csproj](leetcode_1292/leetcode_1292/leetcode_1292.csproj) | net10.0 |
| 1298 | [leetcode_1298](leetcode_1298/) | [README](leetcode_1298/README.md) | [leetcode_1298.csproj](leetcode_1298/leetcode_1298/leetcode_1298.csproj) | net8.0 |
| 1304 | [leetcode_1304](leetcode_1304/) | [README](leetcode_1304/README.md) | [leetcode_1304.csproj](leetcode_1304/leetcode_1304/leetcode_1304.csproj) | net8.0 |
| 1317 | [leetcode_1317](leetcode_1317/) | [README](leetcode_1317/README.md) | [leetcode_1317.csproj](leetcode_1317/leetcode_1317/leetcode_1317.csproj) | net8.0 |
| 1318 | [leetcode_1318](leetcode_1318/) | [README](leetcode_1318/README.md) | [leetcode_1318.csproj](leetcode_1318/leetcode_1318/leetcode_1318.csproj) | net10.0 |
| 1323 | [leetcode_1323](leetcode_1323/) | [README](leetcode_1323/README.md) | [leetcode_1323.csproj](leetcode_1323/leetcode_1323/leetcode_1323.csproj) | net8.0 |
| 1332 | [leetcode_1332](leetcode_1332/) | [README](leetcode_1332/README.md) | [leetcode_1332.csproj](leetcode_1332/leetcode_1332/leetcode_1332.csproj) | net10.0 |
| 1334 | [leetcode_1334](leetcode_1334/) | 未補 | [leetcode_1334.csproj](leetcode_1334/leetcode_1334/leetcode_1334.csproj) | net8.0 |
| 1337 | [leetcode_1337](leetcode_1337/) | 未補 | [leetcode_1337.csproj](leetcode_1337/leetcode_1337/leetcode_1337.csproj) | .NET Framework v4.8 |
| 1338 | [leetcode_1338](leetcode_1338/) | [README](leetcode_1338/README.md) | [leetcode_1338.csproj](leetcode_1338/leetcode_1338/leetcode_1338.csproj) | net10.0 |
| 1339 | [leetcode_1339](leetcode_1339/) | [README](leetcode_1339/README.md) | [leetcode_1339.csproj](leetcode_1339/leetcode_1339/leetcode_1339.csproj) | net10.0 |
| 1342 | [leetcode_1342](leetcode_1342/) | [README](leetcode_1342/README.md) | [leetcode_1342.csproj](leetcode_1342/leetcode_1342/leetcode_1342.csproj) | net10.0 |
| 1351 | [leetcode_1351](leetcode_1351/) | 未補 | [leetcode_1351.csproj](leetcode_1351/leetcode_1351/leetcode_1351.csproj) | .NET Framework v4.8 |
| 1353 | [leetcode_1353](leetcode_1353/) | [README](leetcode_1353/README.md) | [leetcode_1353.csproj](leetcode_1353/leetcode_1353/leetcode_1353.csproj) | net8.0 |
| 1356 | [leetcode_1356](leetcode_1356/) | [README](leetcode_1356/README.md) | [leetcode_1356.csproj](leetcode_1356/leetcode_1356/leetcode_1356.csproj) | net10.0 |
| 1372 | [leetcode_1372](leetcode_1372/) | 未補 | [leetcode_1372.csproj](leetcode_1372/leetcode_1372/leetcode_1372.csproj) | .NET Framework v4.8 |
| 1380 | [leetcode_1380](leetcode_1380/) | 未補 | [leetcode_1380.csproj](leetcode_1380/leetcode_1380/leetcode_1380.csproj) | net8.0 |
| 1382 | [leetcode_1382](leetcode_1382/) | [README](leetcode_1382/README.md) | [leetcode_1382.csproj](leetcode_1382/leetcode_1382/leetcode_1382.csproj) | net10.0 |
| 1394 | [leetcode_1394](leetcode_1394/) | [README](leetcode_1394/README.md) | [leetcode_1394.csproj](leetcode_1394/leetcode_1394/leetcode_1394.csproj) | net8.0 |
| 1395 | [leetcode_1395](leetcode_1395/) | 未補 | [leetcode_1395.csproj](leetcode_1395/leetcode_1395/leetcode_1395.csproj) | net8.0 |
| 1399 | [leetcode_1399](leetcode_1399/) | 未補 | [leetcode_1399.csproj](leetcode_1399/leetcode_1399/leetcode_1399.csproj) | net8.0 |
| 1404 | [leetcode_1404](leetcode_1404/) | [README](leetcode_1404/README.md) | [leetcode_1404.csproj](leetcode_1404/leetcode_1404/leetcode_1404.csproj) | net10.0 |
| 1423 | [leetcode_1423](leetcode_1423/) | 未補 | [leetcode_1423.csproj](leetcode_1423/leetcode_1423/leetcode_1423.csproj) | .NET Framework v4.8 |
| 1424 | [leetcode_1424](leetcode_1424/) | 未補 | [leetcode_1424.csproj](leetcode_1424/leetcode_1424/leetcode_1424.csproj) | .NET Framework v4.8 |
| 1431 | [leetcode_1431](leetcode_1431/) | 未補 | [leetcode_1431.csproj](leetcode_1431/leetcode_1431/leetcode_1431.csproj) | net8.0 |
| 1432 | [leetcode_1432](leetcode_1432/) | [README](leetcode_1432/README.md) | [leetcode_1432.csproj](leetcode_1432/leetcode_1432/leetcode_1432.csproj) | net8.0 |
| 1436 | [leetcode_1436](leetcode_1436/) | 未補 | [leetcode_1436.csproj](leetcode_1436/leetcode_1436/leetcode_1436.csproj) | .NET Framework v4.8 |
| 1437 | [leetcode_1437](leetcode_1437/) | [README](leetcode_1437/README.md) | [leetcode_1437.csproj](leetcode_1437/leetcode_1437/leetcode_1437.csproj) | net8.0 |
| 1438 | [leetcode_1438](leetcode_1438/) | 未補 | [leetcode_1438.csproj](leetcode_1438/leetcode_1438/leetcode_1438.csproj) | net8.0 |
| 1441 | [leetcode_1441](leetcode_1441/) | 未補 | [leetcode_1441.csproj](leetcode_1441/leetcode_1441/leetcode_1441.csproj) | .NET Framework v4.8 |
| 1442 | [leetcode_1442](leetcode_1442/) | 未補 | [leetcode_1442.csproj](leetcode_1442/leetcode_1442/leetcode_1442.csproj) | net8.0 |
| 1443 | [leetcode_1443](leetcode_1443/) | [README](leetcode_1443/README.md) | [leetcode_1443.csproj](leetcode_1443/leetcode_1443/leetcode_1443.csproj) | net10.0 |
| 1458 | [leetcode_1458](leetcode_1458/) | [README](leetcode_1458/README.md) | [leetcode_1458.csproj](leetcode_1458/leetcode_1458/leetcode_1458.csproj) | net10.0 |
| 1460 | [leetcode_1460](leetcode_1460/) | 未補 | [leetcode_1460.csproj](leetcode_1460/leetcode_1460/leetcode_1460.csproj) | net8.0 |
| 1461 | [leetcode_1461](leetcode_1461/) | [README](leetcode_1461/README.md) | [leetcode_1461.csproj](leetcode_1461/leetcode_1461/leetcode_1461.csproj) | net10.0 |
| 1464 | [leetcode_1464](leetcode_1464/) | 未補 | [leetcode_1464.csproj](leetcode_1464/leetcode_1464/leetcode_1464.csproj) | .NET Framework v4.8 |
| 1470 | [leetcode_1470](leetcode_1470/) | [README](leetcode_1470/README.md) | [leetcode_1470.csproj](leetcode_1470/leetcode_1470/leetcode_1470.csproj) | net10.0 |
| 1475 | [leetcode_1475](leetcode_1475/) | 未補 | [leetcode_1475.csproj](leetcode_1475/leetcode_1475/leetcode_1475.csproj) | net8.0 |
| 1480 | [leetcode_1480](leetcode_1480/) | [README](leetcode_1480/README.md) | [leetcode_1480.csproj](leetcode_1480/leetcode_1480/leetcode_1480.csproj) | net10.0 |
| 1481 | [leetcode_1481](leetcode_1481/) | 未補 | [leetcode_1481.csproj](leetcode_1481/leetcode_1481/leetcode_1481.csproj) | .NET Framework v4.8 |
| 1482 | [leetcode_1482](leetcode_1482/) | 未補 | [leetcode_1482.csproj](leetcode_1482/leetcode_1482/leetcode_1482.csproj) | net8.0 |
| 1488 | [leetcode_1488](leetcode_1488/) | [README](leetcode_1488/README.md) | [leetcode_1488.csproj](leetcode_1488/leetcode_1488/leetcode_1488.csproj) | net8.0 |
| 1493 | [leetcode_1493](leetcode_1493/) | 未補 | [leetcode_1493.csproj](leetcode_1493/leetcode_1493/leetcode_1493.csproj) | .NET Framework v4.8 |
| 1498 | [leetcode_1498](leetcode_1498/) | [README](leetcode_1498/README.md) | [leetcode_1498.csproj](leetcode_1498/leetcode_1498/leetcode_1498.csproj) | net8.0 |
| 1502 | [leetcode_1502](leetcode_1502/) | 未補 | [leetcode_1502.csproj](leetcode_1502/leetcode_1502/leetcode_1502.csproj) | .NET Framework v4.8 |
| 1508 | [leetcode_1508](leetcode_1508/) | 未補 | [leetcode_1508.csproj](leetcode_1508/leetcode_1508/leetcode_1508.csproj) | net8.0 |
| 1509 | [leetcode_1509](leetcode_1509/) | 未補 | [leetcode_1509.csproj](leetcode_1509/leetcode_1509/leetcode_1509.csproj) | net8.0 |
| 1512 | [leetcode_1512](leetcode_1512/) | 未補 | [leetcode_1512.csproj](leetcode_1512/leetcode_1512/leetcode_1512.csproj) | .NET Framework v4.8 |
| 1518 | [leetcode_1518](leetcode_1518/) | [README](leetcode_1518/README.md) | [leetcode_1518.csproj](leetcode_1518/leetcode_1518/leetcode_1518.csproj) | net8.0 |
| 1523 | [leetcode_1523](leetcode_1523/) | [README](leetcode_1523/README.md) | [leetcode_1523.csproj](leetcode_1523/leetcode_1523/leetcode_1523.csproj) | net10.0 |
| 1526 | [leetcode_1526](leetcode_1526/) | [README](leetcode_1526/README.md) | [leetcode_1526.csproj](leetcode_1526/leetcode_1526/leetcode_1526.csproj) | net8.0 |
| 1530 | [leetcode_1530](leetcode_1530/) | 未補 | [leetcode_1530.csproj](leetcode_1530/leetcode_1530/leetcode_1530.csproj) | net8.0 |
| 1535 | [leetcode_1535](leetcode_1535/) | 未補 | [1535.csproj](leetcode_1535/1535/1535.csproj) | .NET Framework v4.8 |
| 1539 | [leetcode_1539](leetcode_1539/) | 未補 | [leetcode_1539.csproj](leetcode_1539/leetcode_1539/leetcode_1539.csproj) | .NET Framework v4.8 |
| 1544 | [leetcode_1544](leetcode_1544/) | 未補 | [leetcode_1544.csproj](leetcode_1544/leetcode_1544/leetcode_1544.csproj) | .NET Framework v4.8 |
| 1550 | [leetcode_1550](leetcode_1550/) | 未補 | [leetcode_1550.csproj](leetcode_1550/leetcode_1550/leetcode_1550.csproj) | net8.0 |
| 1576 | [leetcode_1576](leetcode_1576/) | [README](leetcode_1576/README.md) | [leetcode_1576.csproj](leetcode_1576/leetcode_1576/leetcode_1576.csproj) | net10.0 |
| 1578 | [leetcode_1578](leetcode_1578/) | [README](leetcode_1578/README.md) | [leetcode_1578.csproj](leetcode_1578/leetcode_1578/leetcode_1578.csproj) | net8.0 |
| 1582 | [leetcode_1582](leetcode_1582/) | [README](leetcode_1582/README.md) | [leetcode_1582.csproj](leetcode_1582/leetcode_1582/leetcode_1582.csproj) | net10.0 |
| 1594 | [leetcode_1594](leetcode_1594/) | [README](leetcode_1594/README.md) | [leetcode_1594.csproj](leetcode_1594/leetcode_1594/leetcode_1594.csproj) | net10.0 |
| 1603 | [leetcode_1603](leetcode_1603/) | 未補 | [leetcode_1603.csproj](leetcode_1603/leetcode_1603/leetcode_1603.csproj) | .NET Framework v4.8 |
| 1605 | [leetcode_1605](leetcode_1605/) | 未補 | [leetode_1605.csproj](leetcode_1605/leetode_1605/leetode_1605.csproj) | net8.0 |
| 1608 | [leetcode_1608](leetcode_1608/) | 未補 | [leetcode_1608.csproj](leetcode_1608/leetcode_1608/leetcode_1608.csproj) | net8.0 |
| 1625 | [leetcode_1625](leetcode_1625/) | [README](leetcode_1625/README.md) | [leetcode_1625.csproj](leetcode_1625/leetcode_1625/leetcode_1625.csproj) | net8.0 |
| 1626 | [leetcode_1626](leetcode_1626/) | [README](leetcode_1626/README.md) | [leetcode_1626.csproj](leetcode_1626/leetcode_1626/leetcode_1626.csproj) | net10.0 |
| 1630 | [leetcode_1630](leetcode_1630/) | 未補 | [leetcode_1630.csproj](leetcode_1630/leetcode_1630/leetcode_1630.csproj) | .NET Framework v4.8 |
| 1636 | [leetcode_1636](leetcode_1636/) | 未補 | [leetcode_1636.csproj](leetcode_1636/leetcode_1636/leetcode_1636.csproj) | net8.0 |
| 1642 | [leetcode_1642](leetcode_1642/) | 未補 | [leetcode_1642.csproj](leetcode_1642/leetcode_1642/leetcode_1642.csproj) | .NET Framework v4.8 |
| 1652 | [leetcode_1652](leetcode_1652/) | 未補 | [leetcode_1652.csproj](leetcode_1652/leetcode_1652/leetcode_1652.csproj) | net8.0 |
| 1653 | [leetcode_1653](leetcode_1653/) | [README](leetcode_1653/README.md) | [leetcode_1653.csproj](leetcode_1653/leetcode_1653/leetcode_1653.csproj) | net10.0 |
| 1657 | [leetcode_1657](leetcode_1657/) | 未補 | [leetcode_1657.csproj](leetcode_1657/leetcode_1657/leetcode_1657.csproj) | .NET Framework v4.8 |
| 1663 | [leetcode_1663](leetcode_1663/) | [README](leetcode_1663/README.md) | [leetcode_1663.csproj](leetcode_1663/leetcode_1663/leetcode_1663.csproj) | net10.0 |
| 1680 | [leetcode_1680](leetcode_1680/) | [README](leetcode_1680/README.md) | [leetcode_1680.csproj](leetcode_1680/leetcode_1680/leetcode_1680.csproj) | net10.0 |
| 1685 | [leetcode_1685](leetcode_1685/) | 未補 | [leetcode_1685.csproj](leetcode_1685/leetcode_1685/leetcode_1685.csproj) | .NET Framework v4.8 |
| 1695 | [leetcode_1695](leetcode_1695/) | [README](leetcode_1695/README.md) | [leetcode_1695.csproj](leetcode_1695/leetcode_1695/leetcode_1695.csproj) | net8.0 |
| 1700 | [leetcode_1700](leetcode_1700/) | 未補 | [leetcode_1700.csproj](leetcode_1700/leetcode_1700/leetcode_1700.csproj) | net8.0 |
| 1701 | [leetcode_1701](leetcode_1701/) | 未補 | [leetcode_1701.csproj](leetcode_1701/leetcode_1701/leetcode_1701.csproj) | net8.0 |
| 1704 | [leetcode_1704](leetcode_1704/) | [README](leetcode_1704/README.md) | [leetcode_1704.csproj](leetcode_1704/leetcode_1704/leetcode_1704.csproj) | net10.0 |
| 1716 | [leetcode_1716](leetcode_1716/) | [README](leetcode_1716/README.md) | [leetcode_1716.csproj](leetcode_1716/leetcode_1716/leetcode_1716.csproj) | net8.0 |
| 1717 | [leetcode_1717](leetcode_1717/) | [README](leetcode_1717/README.md) | [leetcode_1717.csproj](leetcode_1717/leetcode_1717/leetcode_1717.csproj) | net8.0 |
| 1721 | [leetcode_1721](leetcode_1721/) | 未補 | [leetcode_1721.csproj](leetcode_1721/leetcode_1721/leetcode_1721.csproj) | .NET Framework v4.8 |
| 1743 | [leetcode_1743](leetcode_1743/) | 未補 | [leetcode_1743.csproj](leetcode_1743/leetcode_1743/leetcode_1743.csproj) | .NET Framework v4.8 |
| 1750 | [leetcode_1750](leetcode_1750/) | 未補 | [leetcode_1750.csproj](leetcode_1750/leetcode_1750/leetcode_1750.csproj) | .NET Framework v4.8 |
| 1759 | [leetcode_1759](leetcode_1759/) | 未補 | [leetcode_1759.csproj](leetcode_1759/leetcode_1759/leetcode_1759.csproj) | .NET Framework v4.8 |
| 1768 | [leetcode_1768](leetcode_1768/) | 未補 | [leetcode_1768.csproj](leetcode_1768/leetcode_1768/leetcode_1768.csproj) | net8.0 |
| 1784 | [leetcode_1784](leetcode_1784/) | [README](leetcode_1784/README.md) | [leetcode_1784.csproj](leetcode_1784/leetcode_1784/leetcode_1784.csproj) | net10.0 |
| 1791 | [leetcode_1791](leetcode_1791/) | 未補 | [leetcode_1791.csproj](leetcode_1791/leetcode_1791/leetcode_1791.csproj) | net8.0 |
| 1792 | [leetcode_1792](leetcode_1792/) | [README](leetcode_1792/README.md) | [leetcode_1792.csproj](leetcode_1792/leetcode_1792/leetcode_1792.csproj) | net8.0 |
| 1802 | [leetcode_1802](leetcode_1802/) | 未補 | [leetcode_1802.csproj](leetcode_1802/leetcode_1802/leetcode_1802.csproj) | .NET Framework v4.8 |
| 1816 | [leetcode_1816](leetcode_1816/) | 未補 | [leetcode_1816.csproj](leetcode_1816/leetcode_1816/leetcode_1816.csproj) | .NET Framework v4.8 |
| 1818 | [leetcode_1818](leetcode_1818/) | 未補 | [leetcode_1818.csproj](leetcode_1818/leetcode_1818/leetcode_1818.csproj) | .NET Framework v4.8 |
| 1822 | [leetcode_1822](leetcode_1822/) | 未補 | [leetcode_1822.csproj](leetcode_1822/leetcode_1822/leetcode_1822.csproj) | .NET Framework v4.8 |
| 1833 | [leetcode_1833](leetcode_1833/) | [README](leetcode_1833/README.md) | [leetcode_1833.csproj](leetcode_1833/leetcode_1833/leetcode_1833.csproj) | net10.0 |
| 1848 | [leetcode_1848](leetcode_1848/) | [README](leetcode_1848/README.md) | [leetcode_1848.csproj](leetcode_1848/leetcode_1848/leetcode_1848.csproj) | net10.0 |
| 1857 | [leetcode_1857](leetcode_1857/) | [README](leetcode_1857/README.md) | [leetcode_1857.csproj](leetcode_1857/leetcode_1857/leetcode_1857.csproj) | net8.0 |
| 1861 | [leetcode_1861](leetcode_1861/) | [README](leetcode_1861/README.md) | [leetcode_1861.csproj](leetcode_1861/leetcode_1861/leetcode_1861.csproj) | net10.0 |
| 1863 | [leetcode_1863](leetcode_1863/) | 未補 | [leetcode_1863.csproj](leetcode_1863/leetcode_1863/leetcode_1863.csproj) | net8.0 |
| 1865 | [leetcode_1865](leetcode_1865/) | [README](leetcode_1865/README.md) | [leetcode_1865.csproj](leetcode_1865/leetcode_1865/leetcode_1865.csproj) | net8.0 |
| 1877 | [leetcode_1877](leetcode_1877/) | [README](leetcode_1877/README.md) | [leetcode_1877.csproj](leetcode_1877/leetcode_1877/leetcode_1877.csproj) | net10.0 |
| 1886 | [leetcode_1886](leetcode_1886/) | [README](leetcode_1886/README.md) | [leetcode_1886.csproj](leetcode_1886/leetcode_1886/leetcode_1886.csproj) | net10.0 |
| 1887 | [leetcode_1887](leetcode_1887/) | 未補 | [leetcode_1887.csproj](leetcode_1887/leetcode_1887/leetcode_1887.csproj) | .NET Framework v4.8 |
| 1897 | [leetcode_1897](leetcode_1897/) | 未補 | [leetcode_1897.csproj](leetcode_1897/leetcode_1897/leetcode_1897.csproj) | .NET Framework v4.8 |
| 1909 | [leetcode_1909](leetcode_1909/) | 未補 | [leetcode_1909.csproj](leetcode_1909/leetcode_1909/leetcode_1909.csproj) | .NET Framework v4.8 |
| 1913 | [leetcode_1913](leetcode_1913/) | 未補 | [leetcode_1913.csproj](leetcode_1913/leetcode_1913/leetcode_1913.csproj) | .NET Framework v4.8 |
| 1920 | [leetcode_1920](leetcode_1920/) | [README](leetcode_1920/README.md) | [leetcode_1920.csproj](leetcode_1920/leetcode_1920/leetcode_1920.csproj) | net8.0 |
| 1921 | [leetcode_1921](leetcode_1921/) | 未補 | [leetcode_1921.csproj](leetcode_1921/leetcode_1921/leetcode_1921.csproj) | .NET Framework v4.8 |
| 1925 | [leetcode_1925](leetcode_1925/) | [README](leetcode_1925/README.md) | [leetcode_1925.csproj](leetcode_1925/leetcode_1925/leetcode_1925.csproj) | net10.0 |
| 1930 | [leetcode_1930](leetcode_1930/) | 未補 | [leetcode_1930.csproj](leetcode_1930/leetcode_1930/leetcode_1930.csproj) | .NET Framework v4.8 |
| 1952 | [leetcode_1952](leetcode_1952/) | 未補 | [leetcode_1952.csproj](leetcode_1952/leetcode_1952/leetcode_1952.csproj) | .NET Framework v4.8 |
| 1957 | [leetcode_1957](leetcode_1957/) | [README](leetcode_1957/README.md) | [leetcode_1957.csproj](leetcode_1957/leetcode_1957/leetcode_1957.csproj) | net8.0 |
| 1971 | [leetcode_1971](leetcode_1971/) | [README](leetcode_1971/README.md) | [leetcode_1971.csproj](leetcode_1971/leetcode_1971/leetcode_1971.csproj) | net10.0 |
| 1975 | [leetcode_1975](leetcode_1975/) | [README](leetcode_1975/README.md) | [leetcode_1975.csproj](leetcode_1975/leetcode_1975/leetcode_1975.csproj) | net10.0 |
| 1980 | [leetcode_1980](leetcode_1980/) | [README](leetcode_1980/README.md) | [leetcode_1980.csproj](leetcode_1980/leetcode_1980/leetcode_1980.csproj) | net10.0 |
| 1984 | [leetcode_1984](leetcode_1984/) | [README](leetcode_1984/README.md) | [leetcode_1984.csproj](leetcode_1984/leetcode_1984/leetcode_1984.csproj) | net10.0 |
| 1992 | [leetcode_1992](leetcode_1992/) | 未補 | [leetcode_1992.csproj](leetcode_1992/leetcode_1992/leetcode_1992.csproj) | net8.0 |
| 2000 | [leetcode_2000](leetcode_2000/) | 未補 | [leetcode_2000.csproj](leetcode_2000/leetcode_2000/leetcode_2000.csproj) | .NET Framework v4.8 |
| 2011 | [leetcode_2011](leetcode_2011/) | [README](leetcode_2011/README.md) | [leetcode_2011.csproj](leetcode_2011/leetcode_2011/leetcode_2011.csproj) | net8.0 |
| 2016 | [leetcode_2016](leetcode_2016/) | [README](leetcode_2016/README.md) | [leetcode_2016.csproj](leetcode_2016/leetcode_2016/leetcode_2016.csproj) | net8.0 |
| 2024 | [leetcode_2024](leetcode_2024/) | 未補 | [leetcode_2024.csproj](leetcode_2024/leetcode_2024/leetcode_2024.csproj) | .NET Framework v4.8 |
| 2033 | [leetcode_2033](leetcode_2033/) | [README](leetcode_2033/README.md) | [leetcode_2033.csproj](leetcode_2033/leetcode_2033/leetcode_2033.csproj) | net10.0 |
| 2037 | [leetcode_2037](leetcode_2037/) | 未補 | [leetcode_2037.csproj](leetcode_2037/leetcode_2037/leetcode_2037.csproj) | net8.0 |
| 2038 | [leetcode_2038](leetcode_2038/) | 未補 | [leetcode_2038.csproj](leetcode_2038/leetcode_2038/leetcode_2038.csproj) | .NET Framework v4.8 |
| 2040 | [leetcode_2040](leetcode_2040/) | [README](leetcode_2040/README.md) | [leetcode_2040.csproj](leetcode_2040/leetcode_2040/leetcode_2040.csproj) | net8.0 |
| 2043 | [leetcode_2043](leetcode_2043/) | [README](leetcode_2043/README.md) | [leetcode_2043.csproj](leetcode_2043/leetcode_2043/leetcode_2043.csproj) | net8.0 |
| 2044 | [leetcode_2044](leetcode_2044/) | [README](leetcode_2044/README.md) | [leetcode_2044.csproj](leetcode_2044/leetcode_2044/leetcode_2044.csproj) | net8.0 |
| 2048 | [leetcode_2048](leetcode_2048/) | [README](leetcode_2048/README.md) | [leetcode_2048.csproj](leetcode_2048/leetcode_2048/leetcode_2048.csproj) | net8.0 |
| 2053 | [leetcode_2053](leetcode_2053/) | 未補 | [leetcode_2053.csproj](leetcode_2053/leetcode_2053/leetcode_2053.csproj) | net8.0 |
| 2058 | [leetcode_2058](leetcode_2058/) | 未補 | [leetcode_2058.csproj](leetcode_2058/leetcode_2058/leetcode_2058.csproj) | net8.0 |
| 2078 | [leetcode_2078](leetcode_2078/) | [README](leetcode_2078/README.md) | [leetcode_2078.csproj](leetcode_2078/leetcode_2078/leetcode_2078.csproj) | net10.0 |
| 2081 | [leetcode_2081](leetcode_2081/) | [README](leetcode_2081/README.md) | [leetcode_2081.csproj](leetcode_2081/leetcode_2081/leetcode_2081.csproj) | net8.0 |
| 2085 | [leetcode_2085](leetcode_2085/) | 未補 | [leetcode_2085.csproj](leetcode_2085/leetcode_2085/leetcode_2085.csproj) | .NET Framework v4.8 |
| 2106 | [leetcode_2106](leetcode_2106/) | [README](leetcode_2106/README.md) | [leetcode_2106.csproj](leetcode_2106/leetcode_2106/leetcode_2106.csproj) | net8.0 |
| 2108 | [leetcode_2108](leetcode_2108/) | 未補 | [leetcode_2108.csproj](leetcode_2108/leetcode_2108/leetcode_2108.csproj) | .NET Framework v4.8 |
| 2110 | [leetcode_2110](leetcode_2110/) | [README](leetcode_2110/README.md) | [leetcode_2110.csproj](leetcode_2110/leetcode_2110/leetcode_2110.csproj) | net10.0 |
| 2125 | [leetcode_2125](leetcode_2125/) | [README](leetcode_2125/README.md) | [leetcode_2125.csproj](leetcode_2125/leetcode_2125/leetcode_2125.csproj) | net8.0 |
| 2129 | [leetcode_2129](leetcode_2129/) | 未補 | [leetcode_2129.csproj](leetcode_2129/leetcode_2129/leetcode_2129.csproj) | net8.0 |
| 2130 | [leetcode_2130](leetcode_2130/) | 未補 | [leetcode_2130.csproj](leetcode_2130/leetcode_2130/leetcode_2130.csproj) | .NET Framework v4.8 |
| 2131 | [leetcode_2131](leetcode_2131/) | [README](leetcode_2131/README.md) | [leetcode_2131.csproj](leetcode_2131/leetcode_2131/leetcode_2131.csproj) | net8.0 |
| 2134 | [leetcode_2134](leetcode_2134/) | 未補 | [leetcode_2134.csproj](leetcode_2134/leetcode_2134/leetcode_2134.csproj) | net8.0 |
| 2138 | [leetcode_2138](leetcode_2138/) | [README](leetcode_2138/README.md) | [leetcode_2138.csproj](leetcode_2138/leetcode_2138/leetcode_2138.csproj) | net8.0 |
| 2149 | [leetcode_2149](leetcode_2149/) | 未補 | [leetcode_2149.csproj](leetcode_2149/leetcode_2149/leetcode_2149.csproj) | .NET Framework v4.8 |
| 2154 | [leetcode_2154](leetcode_2154/) | [README](leetcode_2154/README.md) | [leetcode_2154.csproj](leetcode_2154/leetcode_2154/leetcode_2154.csproj) | net8.0 |
| 2163 | [leetcode_2163](leetcode_2163/) | [README](leetcode_2163/README.md) | [leetcode_2163.csproj](leetcode_2163/leetcode_2163/leetcode_2163.csproj) | net8.0 |
| 2181 | [leetcode_2181](leetcode_2181/) | 未補 | [leetcode_2181.csproj](leetcode_2181/leetcode_2181/leetcode_2181.csproj) | net8.0 |
| 2182 | [leetcode_2182](leetcode_2182/) | 未補 | [leetcode_2182.csproj](leetcode_2182/leetcode_2182/leetcode_2182.csproj) | net8.0 |
| 2187 | [leetcode_2187](leetcode_2187/) | 未補 | [leetcode_2187.csproj](leetcode_2187/leetcode_2187/leetcode_2187.csproj) | .NET Framework v4.8 |
| 2200 | [leetcode_2200](leetcode_2200/) | [README](leetcode_2200/README.md) | [leetcode_2200.csproj](leetcode_2200/leetcode_2200/leetcode_2200.csproj) | net8.0 |
| 2210 | [leetcode_2210](leetcode_2210/) | [README](leetcode_2210/README.md) | [leetcode_2210.csproj](leetcode_2210/leetcode_2210/leetcode_2210.csproj) | net8.0 |
| 2211 | [leetcode_2211](leetcode_2211/) | [README](leetcode_2211/README.md) | [leetcode_2211.csproj](leetcode_2211/leetcode_2211/leetcode_2211.csproj) | net10.0 |
| 2215 | [leetcode_2215](leetcode_2215/) | 未補 | [leetcode_2215.csproj](leetcode_2215/leetcode_2215/leetcode_2215.csproj) | .NET Framework v4.8 |
| 2221 | [leetcode_2221](leetcode_2221/) | [README](leetcode_2221/README.md) | [leetcode_2221.csproj](leetcode_2221/leetcode_2221/leetcode_2221.csproj) | net8.0 |
| 2225 | [leetcode_2225](leetcode_2225/) | 未補 | [leetcode_2225.csproj](leetcode_2225/leetcode_2225/leetcode_2225.csproj) | net8.0 |
| 2243 | [leetcode_2243](leetcode_2243/) | 未補 | [leetcode_2243.csproj](leetcode_2243/leetcode_2243/leetcode_2243.csproj) | .NET Framework v4.8 |
| 2244 | [leetcode_2244](leetcode_2244/) | [README](leetcode_2244/README.md) | [leetcode_2244.csproj](leetcode_2244/leetcode_2244/leetcode_2244.csproj) | net10.0 |
| 2264 | [leetcode_2264](leetcode_2264/) | [README](leetcode_2264/README.md) | [leetcode_2264.csproj](leetcode_2264/leetcode_2264/leetcode_2264.csproj) | net8.0 |
| 2265 | [leetcode_2265](leetcode_2265/) | 未補 | [leetcode_2265.csproj](leetcode_2265/leetcode_2265/leetcode_2265.csproj) | .NET Framework v4.8 |
| 2285 | [leetcode_2285](leetcode_2285/) | 未補 | [leetcode_2285.csproj](leetcode_2285/leetcode_2285/leetcode_2285.csproj) | net8.0 |
| 2322 | [leetcode_2322](leetcode_2322/) | [README](leetcode_2322/README.md) | [leetcode_2322.csproj](leetcode_2322/leetcode_2322/leetcode_2322.csproj) | net8.0 |
| 2327 | [leetcode_2327](leetcode_2327/) | [README](leetcode_2327/README.md) | [leetcode_2327.csproj](leetcode_2327/leetcode_2327/leetcode_2327.csproj) | net8.0 |
| 2331 | [leetcode_2331](leetcode_2331/) | 未補 | [leetcode_2331.csproj](leetcode_2331/leetcode_2331/leetcode_2331.csproj) | net8.0 |
| 2348 | [leetcode_2348](leetcode_2348/) | [README](leetcode_2348/README.md) | [leetcode_2348.csproj](leetcode_2348/leetcode_2348/leetcode_2348.csproj) | net8.0 |
| 2353 | [leetcode_2353](leetcode_2353/) | [README](leetcode_2353/README.md) | [leetcode_2353.csproj](leetcode_2353/leetcode_2353/leetcode_2353.csproj) | net8.0 |
| 2359 | [leetcode_2359](leetcode_2359/) | [README](leetcode_2359/README.md) | [leetcode_2359.csproj](leetcode_2359/leetcode_2359/leetcode_2359.csproj) | net8.0 |
| 2370 | [leetcode_2370](leetcode_2370/) | 未補 | [leetcode_2370.csproj](leetcode_2370/leetcode_2370/leetcode_2370.csproj) | net8.0 |
| 2373 | [leetcode_2373](leetcode_2373/) | 未補 | [leetcode_2373.csproj](leetcode_2373/leetcode_2373/leetcode_2373.csproj) | net8.0 |
| 2385 | [leetcode_2385](leetcode_2385/) | 未補 | [leetcode_2385.csproj](leetcode_2385/leetcode_2385/leetcode_2385.csproj) | .NET Framework v4.8 |
| 2390 | [leetcode_2390](leetcode_2390/) | 未補 | [leetcode_2390.csproj](leetcode_2390/leetcode_2390/leetcode_2390.csproj) | .NET Framework v4.8 |
| 2395 | [leetcode_2395](leetcode_2395/) | 未補 | [leetcode_2395.csproj](leetcode_2395/leetcode_2395/leetcode_2395.csproj) | .NET Framework v4.8 |
| 2411 | [leetcode_2411](leetcode_2411/) | [README](leetcode_2411/README.md) | [leetcode_2411.csproj](leetcode_2411/leetcode_2411/leetcode_2411.csproj) | net8.0 |
| 2418 | [leetcode_2418](leetcode_2418/) | 未補 | [leetcode_2418.csproj](leetcode_2418/leetcode_2418/leetcode_2418.csproj) | .NET Framework v4.8 |
| 2419 | [leetcode_2419](leetcode_2419/) | [README](leetcode_2419/README.md) | [leetcode_2419.csproj](leetcode_2419/leetcode_2419/leetcode_2419.csproj) | net8.0 |
| 2434 | [leetcode_2434](leetcode_2434/) | [README](leetcode_2434/README.md) | [leetcode_2434.csproj](leetcode_2434/leetcode_2434/leetcode_2434.csproj) | net8.0 |
| 2438 | [leetcode_2438](leetcode_2438/) | [README](leetcode_2438/README.md) | [leetcode_2438.csproj](leetcode_2438/leetcode_2438/leetcode_2438.csproj) | net8.0 |
| 2441 | [leetcode_2441](leetcode_2441/) | 未補 | [leetcode_2441.csproj](leetcode_2441/leetcode_2441/leetcode_2441.csproj) | net8.0 |
| 2452 | [leetcode_2452](leetcode_2452/) | [README](leetcode_2452/README.md) | [leetcode_2452.csproj](leetcode_2452/leetcode_2452/leetcode_2452.csproj) | net10.0 |
| 2461 | [leetcode_2461](leetcode_2461/) | 未補 | [leetcode_2461.csproj](leetcode_2461/leetcode_2461/leetcode_2461.csproj) | net8.0 |
| 2471 | [leetcode_2471](leetcode_2471/) | 未補 | [leetcode_2471.csproj](leetcode_2471/leetcode_2471/leetcode_2471.csproj) | net8.0 |
| 2483 | [leetcode_2483](leetcode_2483/) | [README](leetcode_2483/README.md) | [leetcode_2483.csproj](leetcode_2483/leetcode_2483/leetcode_2483.csproj) | net10.0 |
| 2485 | [leetcode_2485](leetcode_2485/) | 未補 | [leetcode_2485.csproj](leetcode_2485/leetcode_2485/leetcode_2485.csproj) | net8.0 |
| 2486 | [leetcode_2486](leetcode_2486/) | 未補 | [leetcode_2486.csproj](leetcode_2486/leetcode_2486/leetcode_2486.csproj) | net8.0 |
| 2515 | [leetcode_2515](leetcode_2515/) | [README](leetcode_2515/README.md) | [leetcode_2515.csproj](leetcode_2515/leetcode_2515/leetcode_2515.csproj) | net10.0 |
| 2520 | [leetcode_2520](leetcode_2520/) | 未補 | [leetcode_2520.csproj](leetcode_2520/leetcode_2520/leetcode_2520.csproj) | .NET Framework v4.8 |
| 2540 | [leetcode_2540](leetcode_2540/) | 未補 | [leetcode_2540.csproj](leetcode_2540/leetcode_2540/leetcode_2540.csproj) | net8.0 |
| 2558 | [leetcode_2558](leetcode_2558/) | 未補 | [leetcode_2558.csproj](leetcode_2558/leetcode_2558/leetcode_2558.csproj) | .NET Framework v4.8 |
| 2561 | [leetcode_2561](leetcode_2561/) | [README](leetcode_2561/README.md) | [leetcode_2561.csproj](leetcode_2561/leetcode_2561/leetcode_2561.csproj) | net8.0 |
| 2566 | [leetcode_2566](leetcode_2566/) | [README](leetcode_2566/README.md) | [leetcode_2566.csproj](leetcode_2566/leetcode_2566/leetcode_2566.csproj) | net8.0 |
| 2571 | [leetcode_2571](leetcode_2571/) | [README](leetcode_2571/README.md) | [leetcode_2571.csproj](leetcode_2571/leetcode_2571/leetcode_2571.csproj) | net10.0 |
| 2582 | [leetcode_2582](leetcode_2582/) | 未補 | [leetcode_2582.csproj](leetcode_2582/leetcode_2582/leetcode_2582.csproj) | net8.0 |
| 2591 | [leetcode_2591](leetcode_2591/) | 未補 | [leetcode_2591.csproj](leetcode_2591/leetcode_2591/leetcode_2591.csproj) | .NET Framework v4.8 |
| 2593 | [leetcode_2593](leetcode_2593/) | [README](leetcode_2593/README.md) | [leetcode_2593.csproj](leetcode_2593/leetcode_2593/leetcode_2593.csproj) | net8.0 |
| 2609 | [leetcode_2609](leetcode_2609/) | 未補 | [leetcode_2609.csproj](leetcode_2609/leetcode_2609/leetcode_2609.csproj) | .NET Framework v4.8 |
| 2615 | [leetcode_2615](leetcode_2615/) | [README](leetcode_2615/README.md) | [leetcode_2615.csproj](leetcode_2615/leetcode_2615/leetcode_2615.csproj) | net10.0 |
| 2616 | [leetcode_2616](leetcode_2616/) | 未補 | [leetcode_2616.csproj](leetcode_2616/leetcode_2616/leetcode_2616.csproj) | .NET Framework v4.8 |
| 2670 | [leetcode_2670](leetcode_2670/) | 未補 | [leetcode_2670.csproj](leetcode_2670/leetcode_2670/leetcode_2670.csproj) | .NET Framework v4.8 |
| 2678 | [leetcode_2678](leetcode_2678/) | 未補 | [leetcode_2678.csproj](leetcode_2678/leetcode_2678/leetcode_2678.csproj) | .NET Framework v4.8 |
| 2785 | [leetcode_2785](leetcode_2785/) | 未補 | [leetcode_2785.csproj](leetcode_2785/leetcode_2785/leetcode_2785.csproj) | .NET Framework v4.8 |
| 2787 | [leetcode_2787](leetcode_2787/) | [README](leetcode_2787/README.md) | [leetcode_2787.csproj](leetcode_2787/leetcode_2787/leetcode_2787.csproj) | net8.0 |
| 2799 | [leetcode_2799](leetcode_2799/) | 未補 | [leetcode_2799.csproj](leetcode_2799/leetcode_2799/leetcode_2799.csproj) | net8.0 |
| 2816 | [leetcode_2816](leetcode_2816/) | 未補 | [leetcode_2816.csproj](leetcode_2816/leetcode_2816/leetcode_2816.csproj) | net8.0 |
| 2833 | [leetcode_2833](leetcode_2833/) | [README](leetcode_2833/README.md) | [leetcode_2833.csproj](leetcode_2833/leetcode_2833/leetcode_2833.csproj) | net10.0 |
| 2864 | [leetcode_2864](leetcode_2864/) | 未補 | [leetcode_2864.csproj](leetcode_2864/leetcode_2864/leetcode_2864.csproj) | .NET Framework v4.8 |
| 2870 | [leetcode_2870](leetcode_2870/) | 未補 | [leetcode_2870.csproj](leetcode_2870/leetcode_2870/leetcode_2870.csproj) | .NET Framework v4.8 |
| 2906 | [leetcode_2906](leetcode_2906/) | [README](leetcode_2906/README.md) | [leetcode_2906.csproj](leetcode_2906/leetcode_2906/leetcode_2906.csproj) | net10.0 |
| 2917 | [leetcode_2917](leetcode_2917/) | 未補 | [leetcode_2917.csproj](leetcode_2917/leetcode_2917/leetcode_2917.csproj) | net8.0 |
| 2918 | [leetcode_2918](leetcode_2918/) | [README](leetcode_2918/README.md) | [leetcode_2918.csproj](leetcode_2918/leetcode_2918/leetcode_2918.csproj) | net8.0 |
| 2929 | [leetcode_2929](leetcode_2929/) | [README](leetcode_2929/README.md) | [leetcode_2929.csproj](leetcode_2929/leetcode_2929/leetcode_2929.csproj) | net8.0 |
| 2962 | [leetcode_2962](leetcode_2962/) | 未補 | [leetcode_2962.csproj](leetcode_2962/leetcode_2962/leetcode_2962.csproj) | net8.0 |
| 2966 | [leetcode_2966](leetcode_2966/) | [README](leetcode_2966/README.md) | [leetcode_2966.csproj](leetcode_2966/leetcode_2966/leetcode_2966.csproj) | net8.0 |
| 2971 | [leetcode_2971](leetcode_2971/) | 未補 | [leetcode_2971.csproj](leetcode_2971/leetcode_2971/leetcode_2971.csproj) | .NET Framework v4.8 |
| 2976 | [leetcode_2976](leetcode_2976/) | [README](leetcode_2976/README.md) | [leetcode_2976.csproj](leetcode_2976/leetcode_2976/leetcode_2976.csproj) | net10.0 |
| 3000 | [leetcode_3000](leetcode_3000/) | [README](leetcode_3000/README.md) | [leetcode_3000.csproj](leetcode_3000/leetcode_3000/leetcode_3000.csproj) | net8.0 |
| 3005 | [leetcode_3005](leetcode_3005/) | [README](leetcode_3005/README.md) | [leetcode_3005.csproj](leetcode_3005/leetcode_3005/leetcode_3005.csproj) | net8.0 |
| 3016 | [leetcode_3016](leetcode_3016/) | 未補 | [leetcode_3016.csproj](leetcode_3016/leetcode_3016/leetcode_3016.csproj) | net8.0 |
| 3070 | [leetcode_3070](leetcode_3070/) | [README](leetcode_3070/README.md) | [leetcode_3070.csproj](leetcode_3070/leetcode_3070/leetcode_3070.csproj) | net10.0 |
| 3074 | [leetcode_3074](leetcode_3074/) | [README](leetcode_3074/README.md) | [leetcode_3074.csproj](leetcode_3074/leetcode_3074/leetcode_3074.csproj) | net10.0 |
| 3075 | [leetcode_3075](leetcode_3075/) | [README](leetcode_3075/README.md) | [leetcode_3075.csproj](leetcode_3075/leetcode_3075/leetcode_3075.csproj) | net10.0 |
| 3100 | [leetcode_3100](leetcode_3100/) | 未補 | [leetcode_3100.csproj](leetcode_3100/leetcode_3100/leetcode_3100.csproj) | net8.0 |
| 3110 | [leetcode_3110](leetcode_3110/) | 未補 | [leetcode_3110.csproj](leetcode_3110/leetcode_3110/leetcode_3110.csproj) | net8.0 |
| 3129 | [leetcode_3129](leetcode_3129/) | [README](leetcode_3129/README.md) | [leetcode_3129.csproj](leetcode_3129/leetcode_3129/leetcode_3129.csproj) | net10.0 |
| 3136 | [leetcode_3136](leetcode_3136/) | [README](leetcode_3136/README.md) | [leetcode_3136.csproj](leetcode_3136/leetcode_3136/leetcode_3136.csproj) | net8.0 |
| 3148 | [leetcode_3148](leetcode_3148/) | 未補 | [leetcode_3148.csproj](leetcode_3148/leetcode_3148/leetcode_3148.csproj) | net8.0 |
| 3163 | [leetcode_3163](leetcode_3163/) | 未補 | [leetcode_3163.csproj](leetcode_3163/leetcode_3163/leetcode_3163.csproj) | net8.0 |
| 3169 | [leetcode_3169](leetcode_3169/) | [README](leetcode_3169/README.md) | [leetcode_3169.csproj](leetcode_3169/leetcode_3169/leetcode_3169.csproj) | net8.0 |
| 3170 | [leetcode_3170](leetcode_3170/) | [README](leetcode_3170/README.md) | [leetcode_3170.csproj](leetcode_3170/leetcode_3170/leetcode_3170.csproj) | net8.0 |
| 3186 | [leetcode_3186](leetcode_3186/) | [README](leetcode_3186/README.md) | [leetcode_3186.csproj](leetcode_3186/leetcode_3186/leetcode_3186.csproj) | net8.0 |
| 3190 | [leetcode_3190](leetcode_3190/) | 未補 | [leetcode_3190.csproj](leetcode_3190/leetcode_3190/leetcode_3190.csproj) | net8.0 |
| 3195 | [leetcode_3195](leetcode_3195/) | [README](leetcode_3195/README.md) | [leetcode_3195.csproj](leetcode_3195/leetcode_3195/leetcode_3195.csproj) | net8.0 |
| 3201 | [leetcode_3201](leetcode_3201/) | [README](leetcode_3201/README.md) | [leetcode_3201.csproj](leetcode_3201/leetcode_3201/leetcode_3201.csproj) | net8.0 |
| 3202 | [leetcode_3202](leetcode_3202/) | [README](leetcode_3202/README.md) | [leetcode_3202.csproj](leetcode_3202/leetcode_3202/leetcode_3202.csproj) | net8.0 |
| 3212 | [leetcode_3212](leetcode_3212/) | [README](leetcode_3212/README.md) | [leetcode_3212.csproj](leetcode_3212/leetcode_3212/leetcode_3212.csproj) | net10.0 |
| 3217 | [leetcode_3217](leetcode_3217/) | [README](leetcode_3217/README.md) | [leetcode_3217.csproj](leetcode_3217/leetcode_3217/leetcode_3217.csproj) | net8.0 |
| 3227 | [leetcode_3227](leetcode_3227/) | [README](leetcode_3227/README.md) | [leetcode_3227.csproj](leetcode_3227/leetcode_3227/leetcode_3227.csproj) | net8.0 |
| 3228 | [leetcode_3228](leetcode_3228/) | [README](leetcode_3228/README.md) | [leetcode_3228.csproj](leetcode_3228/leetcode_3228/leetcode_3228.csproj) | net8.0 |
| 3234 | [leetcode_3234](leetcode_3234/) | [README](leetcode_3234/README.md) | [leetcode_3234.csproj](leetcode_3234/leetcode_3234/leetcode_3234.csproj) | net8.0 |
| 3304 | [leetcode_3304](leetcode_3304/) | [README](leetcode_3304/README.md) | [leetcode_3304.csproj](leetcode_3304/leetcode_3304/leetcode_3304.csproj) | net8.0 |
| 3307 | [leetcode_3307](leetcode_3307/) | [README](leetcode_3307/README.md) | [leetcode_3307.csproj](leetcode_3307/leetcode_3307/leetcode_3307.csproj) | net9.0 |
| 3314 | [leetcode_3314](leetcode_3314/) | [README](leetcode_3314/README.md) | [leetcode_3314.csproj](leetcode_3314/leetcode_3314/leetcode_3314.csproj) | net10.0 |
| 3318 | [leetcode_3318](leetcode_3318/) | [README](leetcode_3318/README.md) | [leetcode_3318.csproj](leetcode_3318/leetcode_3318/leetcode_3318.csproj) | net8.0 |
| 3321 | [leetcode_3321](leetcode_3321/) | [README](leetcode_3321/README.md) | [leetcode_3321.csproj](leetcode_3321/leetcode_3321/leetcode_3321.csproj) | net8.0 |
| 3330 | [leetcode_3330](leetcode_3330/) | [README](leetcode_3330/README.md) | [leetcode_3330.csproj](leetcode_3330/leetcode_3330/leetcode_3330.csproj) | net8.0 |
| 3354 | [leetcode_3354](leetcode_3354/) | [README](leetcode_3354/README.md) | [leetcode_3354.csproj](leetcode_3354/leetcode_3354/leetcode_3354.csproj) | net8.0 |
| 3355 | [leetcode_3355](leetcode_3355/) | [README](leetcode_3355/README.md) | [leetcode_3355.csproj](leetcode_3355/leetcode_3355/leetcode_3355.csproj) | net8.0 |
| 3362 | [leetcode_3362](leetcode_3362/) | [README](leetcode_3362/README.md) | [leetcode_3362.csproj](leetcode_3362/leetcode_3362/leetcode_3362.csproj) | net8.0 |
| 3370 | [leetcode_3370](leetcode_3370/) | [README](leetcode_3370/README.md) | [leetcode_3370.csproj](leetcode_3370/leetcode_3370/leetcode_3370.csproj) | net8.0 |
| 3372 | [leetcode_3372](leetcode_3372/) | [README](leetcode_3372/README.md) | [leetcode_3372.csproj](leetcode_3372/leetcode_3372/leetcode_3372.csproj) | net8.0 |
| 3379 | [leetcode_3379](leetcode_3379/) | [README](leetcode_3379/README.md) | [leetcode_3379.csproj](leetcode_3379/leetcode_3379/leetcode_3379.csproj) | net10.0 |
| 3381 | [leetcode_3381](leetcode_3381/) | [README](leetcode_3381/README.md) | [leetcode_3381.csproj](leetcode_3381/leetcode_3381/leetcode_3381.csproj) | net8.0 |
| 3403 | [leetcode_3403](leetcode_3403/) | [README](leetcode_3403/README.md) | [leetcode_3403.csproj](leetcode_3403/leetcode_3403/leetcode_3403.csproj) | net8.0 |
| 3405 | [leetcode_3405](leetcode_3405/) | [README](leetcode_3405/README.md) | [leetcode_3405.csproj](leetcode_3405/leetcode_3405/leetcode_3405.csproj) | net8.0 |
| 3408 | [leetcode_3408](leetcode_3408/) | [README](leetcode_3408/README.md) | [leetcode_3408.csproj](leetcode_3408/leetcode_3408/leetcode_3408.csproj) | net8.0 |
| 3423 | [leetcode_3423](leetcode_3423/) | [README](leetcode_3423/README.md) | [leetcode_3423.csproj](leetcode_3423/leetcode_3423/leetcode_3423.csproj) | net8.0 |
| 3432 | [leetcode_3432](leetcode_3432/) | [README](leetcode_3432/README.md) | [leetcode_3432.csproj](leetcode_3432/leetcode_3432/leetcode_3432.csproj) | net10.0 |
| 3439 | [leetcode_3439](leetcode_3439/) | [README](leetcode_3439/README.md) | [leetcode_3439.csproj](leetcode_3439/leetcode_3439/leetcode_3439.csproj) | net8.0 |
| 3440 | [leetcode_3440](leetcode_3440/) | [README](leetcode_3440/README.md) | [leetcode_3440.csproj](leetcode_3440/leetcode_3440/leetcode_3440.csproj) | net9.0 |
| 3442 | [leetcode_3442](leetcode_3442/) | [README](leetcode_3442/README.md) | [leetcode_3442.csproj](leetcode_3442/leetcode_3442/leetcode_3442.csproj) | net8.0 |
| 3445 | [leetcode_3445](leetcode_3445/) | [README](leetcode_3445/README.md) | [leetcode_3445.csproj](leetcode_3445/leetcode_3445/leetcode_3445.csproj) | net8.0 |
| 3453 | [leetcode_3453](leetcode_3453/) | [README](leetcode_3453/README.md) | [leetcode_3453.csproj](leetcode_3453/leetcode_3453/leetcode_3453.csproj) | net10.0 |
| 3477 | [leetcode_3477](leetcode_3477/) | [README](leetcode_3477/README.md) | [leetcode_3477.csproj](leetcode_3477/leetcode_3477/leetcode_3477.csproj) | net8.0 |
| 3479 | [leetcode_3479](leetcode_3479/) | [README](leetcode_3479/README.md) | [leetcode_3479.csproj](leetcode_3479/leetcode_3479/leetcode_3479.csproj) | net8.0 |
| 3484 | [leetcode_3484](leetcode_3484/) | [README](leetcode_3484/README.md) | [leetcode_3484.csproj](leetcode_3484/leetcode_3484/leetcode_3484.csproj) | net8.0 |
| 3487 | [leetcode_3487](leetcode_3487/) | [README](leetcode_3487/README.md) | [leetcode_3487.csproj](leetcode_3487/leetcode_3487/leetcode_3487.csproj) | net8.0 |
| 3488 | [leetcode_3488](leetcode_3488/) | 未補 | [leetcode_3488.csproj](leetcode_3488/leetcode_3488/leetcode_3488.csproj) | net10.0 |
| 3507 | [leetcode_3507](leetcode_3507/) | [README](leetcode_3507/README.md) | [leetcode_3507.csproj](leetcode_3507/leetcode_3507/leetcode_3507.csproj) | net10.0 |
| 3510 | [leetcode_3510](leetcode_3510/) | [README](leetcode_3510/README.md) | [leetcode_3510.csproj](leetcode_3510/leetcode_3510/leetcode_3510.csproj) | net10.0 |
| 3516 | [leetcode_3516](leetcode_3516/) | [README](leetcode_3516/README.md) | [leetcode_3516.csproj](leetcode_3516/leetcode_3516/leetcode_3516.csproj) | net8.0 |
| 3531 | [leetcode_3531](leetcode_3531/) | [README](leetcode_3531/README.md) | [leetcode_3531.csproj](leetcode_3531/leetcode_3531/leetcode_3531.csproj) | net10.0 |
| 3539 | [leetcode_3539](leetcode_3539/) | [README](leetcode_3539/README.md) | [leetcode_3539.csproj](leetcode_3539/leetcode_3539/leetcode_3539.csproj) | net8.0 |
| 3541 | [leetcode_3541](leetcode_3541/) | [README](leetcode_3541/README.md) | [leetcode_3541.csproj](leetcode_3541/leetcode_3541/leetcode_3541.csproj) | net8.0 |
| 3542 | [leetcode_3542](leetcode_3542/) | [README](leetcode_3542/README.md) | [leetcode_3542.csproj](leetcode_3542/leetcode_3542/leetcode_3542.csproj) | net8.0 |
| 3577 | [leetcode_3577](leetcode_3577/) | [README](leetcode_3577/README.md) | [leetcode_3577.csproj](leetcode_3577/leetcode_3577/leetcode_3577.csproj) | net10.0 |
| 3583 | [leetcode_3583](leetcode_3583/) | [README](leetcode_3583/README.md) | [leetcode_3583.csproj](leetcode_3583/leetcode_3583/leetcode_3583.csproj) | net10.0 |
| 3607 | [leetcode_3607](leetcode_3607/) | [README](leetcode_3607/README.md) | [leetcode_3607.csproj](leetcode_3607/leetcode_3607/leetcode_3607.csproj) | net8.0 |
| 3623 | [leetcode_3623](leetcode_3623/) | [README](leetcode_3623/README.md) | [leetcode_3623.csproj](leetcode_3623/leetcode_3623/leetcode_3623.csproj) | net10.0 |
| 3625 | [leetcode_3625](leetcode_3625/) | [README](leetcode_3625/README.md) | [leetcode_3625.csproj](leetcode_3625/leetcode_3625/leetcode_3625.csproj) | net10.0 |
| 3650 | [leetcode_3650](leetcode_3650/) | [README](leetcode_3650/README.md) | [leetcode_3650.csproj](leetcode_3650/leetcode_3650/leetcode_3650.csproj) | net10.0 |
| 3651 | [leetcode_3651](leetcode_3651/) | [README](leetcode_3651/README.md) | [leetcode_3651.csproj](leetcode_3651/leetcode_3651/leetcode_3651.csproj) | net10.0 |
| 3660 | [leetcode_3660](leetcode_3660/) | [README](leetcode_3660/README.md) | [leetcode_3660.csproj](leetcode_3660/leetcode_3660/leetcode_3660.csproj) | net10.0 |
| 3713 | [leetcode_3713](leetcode_3713/) | [README](leetcode_3713/README.md) | [leetcode_3713.csproj](leetcode_3713/leetcode_3713/leetcode_3713.csproj) | net10.0 |
| 3714 | [leetcode_3714](leetcode_3714/) | [README](leetcode_3714/README.md) | [leetcode_3714.csproj](leetcode_3714/leetcode_3714/leetcode_3714.csproj) | net10.0 |
| 3719 | [leetcode_3719](leetcode_3719/) | [README](leetcode_3719/README.md) | [leetcode_3719.csproj](leetcode_3719/leetcode_3719/leetcode_3719.csproj) | net10.0 |
| 3740 | [leetcode_3740](leetcode_3740/) | [README](leetcode_3740/README.md) | [leetcode_3740.csproj](leetcode_3740/leetcode_3740/leetcode_3740.csproj) | net10.0 |
| 3761 | [leetcode_3761](leetcode_3761/) | [README](leetcode_3761/README.md) | [leetcode_3761.csproj](leetcode_3761/leetcode_3761/leetcode_3761.csproj) | net10.0 |
| 3783 | [leetcode_3783](leetcode_3783/) | [README](leetcode_3783/README.md) | [leetcode_3783.csproj](leetcode_3783/leetcode_3783/leetcode_3783.csproj) | net10.0 |
