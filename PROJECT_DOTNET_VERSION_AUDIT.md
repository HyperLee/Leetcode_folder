# .NET 專案版本與檔案完整性稽核報告

> 掃描日期：2026-07-29
> Git commit：`941f67de3027aed2055a906aea04a55721632741`
> 掃描範圍：repository 根目錄下所有 `leetcode_*` 題目資料夾及 Git 追蹤的 `.csproj`。

## 結論摘要

- 題目資料夾共 **608** 個，主專案 **608** 個，測試專案 **4** 個，合計 **612** 個 `.csproj`。
- **所有 612 個專案均為 `Microsoft.NET.Sdk` SDK-style、`net10.0` 現代 .NET 專案。**
- 未發現 `<TargetFrameworkVersion>`、`net4x` 或 `v4.x` 目標；目前 **.NET Framework 專案數為 0**。
- 名詞說明：產品名稱從 .NET 5 起由「.NET Core」統一改稱「.NET」；本 repository 全部專案目前統一使用 `net10.0`。

## TargetFramework 統計

| TargetFramework | 專案檔數 | 題目資料夾數 |
| --- | ---: | ---: |
| `net10.0` | 612 | 608 |
| **合計** | **612** | **608** |

### 測試專案

| 專案檔 | TargetFramework |
| --- | --- |
| `leetcode_111/leetcode_111.Tests/leetcode_111.Tests.csproj` | `net10.0` |
| `leetcode_187/leetcode_187.Tests/leetcode_187.Tests.csproj` | `net10.0` |
| `leetcode_316/leetcode_316.Tests/leetcode_316.Tests.csproj` | `net10.0` |
| `leetcode_2154/leetcode_2154.Tests/leetcode_2154.Tests.csproj` | `net10.0` |

## 題目根目錄檔案完整性統計

本節只檢查每個 `leetcode_*` 題目根目錄；repository 根目錄或巢狀 C# 專案目錄中的同名檔案，不視為該題已具備。

| 檢查項目 | 缺少資料夾數 | 已具備資料夾數 |
| --- | ---: | ---: |
| `.editorconfig` | 0 | 608 |
| `.gitignore` | 0 | 608 |
| `README.md` | 162 | 446 |
| 三者皆缺少 | 0 | 608 |

## 深度稽核附註

- 全部專案已統一為 SDK-style `net10.0`；版本統計直接取自每個 `.csproj`。
- 四個測試專案仍歸屬各自題目資料夾，不在根 README 另增題目資料列。
- 專案副檔名掃描結果只有 `.csproj`；沒有 `.vbproj` 或 `.fsproj`。非 SDK-style 專案數為 0。

## 全部專案明細

| 題目資料夾 | 專案檔 | 類型 | TargetFramework | SDK-style | `.editorconfig` | `.gitignore` | `README.md` |
| --- | --- | --- | --- | --- | --- | --- | --- |
| `leetcode_001` | `leetcode_001/leetcode_001/leetcode_001.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | 有 |
| `leetcode_002` | `leetcode_002/leetcode_002/leetcode_002.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | 有 |
| `leetcode_003` | `leetcode_003/leetcode_003/leetcode_003.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | 有 |
| `leetcode_004` | `leetcode_004/leetcode_004/leetcode_004.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | 有 |
| `leetcode_005` | `leetcode_005/leetcode_005/leetcode_005.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | 有 |
| `leetcode_007` | `leetcode_007/leetcode_007/leetcode_007.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | 有 |
| `leetcode_008` | `leetcode_008/leetcode_008/leetcode_008.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | **缺少** |
| `leetcode_009` | `leetcode_009/leetcode_009/leetcode_009.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | 有 |
| `leetcode_011` | `leetcode_011/leetcode_011/leetcode_011.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | 有 |
| `leetcode_012` | `leetcode_012/leetcode_012/leetcode_012.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | 有 |
| `leetcode_013` | `leetcode_013/leetcode_013/leetcode_013.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | 有 |
| `leetcode_014` | `leetcode_014/leetcode_014/leetcode_014.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | 有 |
| `leetcode_015` | `leetcode_015/leetcode_015/leetcode_015.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | **缺少** |
| `leetcode_016` | `leetcode_016/leetcode_016/leetcode_016.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | 有 |
| `leetcode_017` | `leetcode_017/leetcode_017/leetcode_017.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | **缺少** |
| `leetcode_019` | `leetcode_019/leetcode_019/leetcode_019.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | 有 |
| `leetcode_020` | `leetcode_020/leetcode_020/leetcode_020.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | **缺少** |
| `leetcode_021` | `leetcode_021/leetcode_021/leetcode_021.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | **缺少** |
| `leetcode_023` | `leetcode_023/leetcode_023/leetcode_023.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | **缺少** |
| `leetcode_024` | `leetcode_024/leetcode_024/leetcode_024.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | 有 |
| `leetcode_026` | `leetcode_026/leetcode_026/leetcode_026.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | 有 |
| `leetcode_027` | `leetcode_027/leetcode_027/leetcode_027.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | 有 |
| `leetcode_028` | `leetcode_028/leetcode_028/leetcode_028.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | 有 |
| `leetcode_029` | `leetcode_029/leetcode_029/leetcode_029.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | 有 |
| `leetcode_033` | `leetcode_033/leetcode_033/leetcode_033.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | 有 |
| `leetcode_035` | `leetcode_035/leetcode_035/leetcode_035.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | 有 |
| `leetcode_039` | `leetcode_039/leetcode_039/leetcode_039.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | **缺少** |
| `leetcode_040` | `leetcode_040/leetcode_040/leetcode_040.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | **缺少** |
| `leetcode_042` | `leetcode_042/leetcode_042/leetcode_042.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | **缺少** |
| `leetcode_045` | `leetcode_045/leetcode_045/leetcode_045.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | 有 |
| `leetcode_046` | `leetcode_046/leetcode_046/leetcode_046.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | **缺少** |
| `leetcode_047` | `leetcode_047/leetcode_047/leetcode_047.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | 有 |
| `leetcode_048` | `leetcode_048/leetcode_048/leetcode_048.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | 有 |
| `leetcode_049` | `leetcode_049/leetcode_049/leetcode_049.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | 有 |
| `leetcode_050` | `leetcode_050/leetcode_050/leetcode_050.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | 有 |
| `leetcode_053` | `leetcode_053/leetcode_053/leetcode_053.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | **缺少** |
| `leetcode_054` | `leetcode_054/leetcode_054/leetcode_054.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | **缺少** |
| `leetcode_055` | `leetcode_055/leetcode_055/leetcode_055.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | 有 |
| `leetcode_056` | `leetcode_056/leetcode_056/leetcode_056.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | **缺少** |
| `leetcode_057` | `leetcode_057/leetcode_057/leetcode_057.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | 有 |
| `leetcode_058` | `leetcode_058/leetcode_058/leetcode_058.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | 有 |
| `leetcode_061` | `leetcode_061/leetcode_061/leetcode_061.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | 有 |
| `leetcode_062` | `leetcode_062/leetcode_062/leetcode_062.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | **缺少** |
| `leetcode_066` | `leetcode_066/leetcode_066/leetcode_066.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | 有 |
| `leetcode_067` | `leetcode_067/leetcode_067/leetcode_067.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | **缺少** |
| `leetcode_069` | `leetcode_069/leetcode_069/leetcode_069.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | 有 |
| `leetcode_070` | `leetcode_070/leetcode_070/leetcode_070.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | **缺少** |
| `leetcode_071` | `leetcode_071/leetcode_071/leetcode_071.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | 有 |
| `leetcode_073` | `leetcode_073/leetcode_073/leetcode_073.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | **缺少** |
| `leetcode_074` | `leetcode_074/leetcode_074/leetcode_074.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | 有 |
| `leetcode_075` | `leetcode_075/leetcode_075/leetcode_075.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | **缺少** |
| `leetcode_75` | `leetcode_75/leetcode_75/leetcode_75.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | 有 |
| `leetcode_076` | `leetcode_076/leetcode_076/leetcode_076.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | **缺少** |
| `leetcode_078` | `leetcode_078/leetcode_078/leetcode_078.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | **缺少** |
| `leetcode_079` | `leetcode_079/leetcode_079/leetcode_079.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | **缺少** |
| `leetcode_080` | `leetcode_080/leetcode_080/leetcode_080.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | 有 |
| `leetcode_081` | `leetcode_081/leetcode_081/leetcode_081.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | 有 |
| `leetcode_082` | `leetcode_082/leetcode_082/leetcode_082.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | 有 |
| `leetcode_083` | `leetcode_083/leetcode_083/leetcode_083.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | 有 |
| `leetcode_084` | `leetcode_084/leetcode_084/leetcode_084.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | **缺少** |
| `leetcode_086` | `leetcode_086/leetcode_086/leetcode_086.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | 有 |
| `leetcode_088` | `leetcode_088/leetcode_088/leetcode_088.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | 有 |
| `leetcode_091` | `leetcode_091/leetcode_091/leetcode_091.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | **缺少** |
| `leetcode_094` | `leetcode_094/leetcode_094/leetcode_094.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | 有 |
| `leetcode_98` | `leetcode_98/leetcode_98/leetcode_98.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | 有 |
| `leetcode_100` | `leetcode_100/leetcode_100/leetcode_100.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | **缺少** |
| `leetcode_101` | `leetcode_101/leetcode_101/leetcode_101.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | 有 |
| `leetcode_102` | `leetcode_102/leetcode_102/leetcode_102.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | **缺少** |
| `leetcode_104` | `leetcode_104/leetcode_104/leetcode_104.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | **缺少** |
| `leetcode_105` | `leetcode_105/leetcode_105/leetcode_105.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | 有 |
| `leetcode_108` | `leetcode_108/leetcode_108/leetcode_108.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | 有 |
| `leetcode_110` | `leetcode_110/leetcode_110/leetcode_110.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | 有 |
| `leetcode_111` | `leetcode_111/leetcode_111.Tests/leetcode_111.Tests.csproj` | 測試專案 | `net10.0` | 是 | 有 | 有 | 有 |
| `leetcode_111` | `leetcode_111/leetcode_111/leetcode_111.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | 有 |
| `leetcode_112` | `leetcode_112/leetcode_112/leetcode_112.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | 有 |
| `leetcode_114` | `leetcode_114/leetcode_114/leetcode_114.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | 有 |
| `leetcode_118` | `leetcode_118/leetcode_118/leetcode_118.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | 有 |
| `leetcode_121` | `leetcode_121/leetcode_121/leetcode_121.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | **缺少** |
| `leetcode_122` | `leetcode_122/leetcode_122/leetcode_122.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | **缺少** |
| `leetcode_125` | `leetcode_125/leetcode_125/leetcode_125.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | **缺少** |
| `leetcode_127` | `leetcode_127/leetcode_127/leetcode_127.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | **缺少** |
| `leetcode_128` | `leetcode_128/leetcode_128/leetcode_128.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | 有 |
| `leetcode_129` | `leetcode_129/leetcode_129/leetcode_129.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | 有 |
| `leetcode_133` | `leetcode_133/leetcode_133/leetcode_133.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | 有 |
| `leetcode_134` | `leetcode_134/leetcode_134/leetcode_134.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | 有 |
| `leetcode_135` | `leetcode_135/leetcode_135/leetcode_135.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | 有 |
| `leetcode_136` | `leetcode_136/leetcode_136/leetcode_136.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | 有 |
| `leetcode_137` | `leetcode_137/leetcode_137/leetcode_137.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | 有 |
| `leetcode_139` | `leetcode_139/leetcode_139/leetcode_139.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | **缺少** |
| `leetcode_141` | `leetcode_141/leetcode_141/leetcode_141.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | **缺少** |
| `leetcode_142` | `leetcode_142/leetcode_142/leetcode_142.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | 有 |
| `leetcode_143` | `leetcode_143/leetcode_143/leetcode_143.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | **缺少** |
| `leetcode_144` | `leetcode_144/leetcode_144/leetcode_144.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | 有 |
| `leetcode_145` | `leetcode_145/leetcode_145/leetcode_145.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | 有 |
| `leetcode_146` | `leetcode_146/leetcode_146/leetcode_146.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | 有 |
| `leetcode_148` | `leetcode_148/leetcode_148/leetcode_148.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | 有 |
| `leetcode_150` | `leetcode_150/leetcode_150/leetcode_150.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | **缺少** |
| `leetcode_151` | `leetcode_151/leetcode_151/leetcode_151.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | 有 |
| `leetcode_152` | `leetcode_152/leetcode_152/leetcode_152.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | **缺少** |
| `leetcode_153` | `leetcode_153/leetcode_153/leetcode_153.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | 有 |
| `leetcode_155` | `leetcode_155/leetcode_155/leetcode_155.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | 有 |
| `leetcode_165` | `leetcode_165/leetcode_165/leetcode_165.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | 有 |
| `leetcode_166` | `leetcode_166/leetcode_166/leetcode_166.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | 有 |
| `leetcode_167` | `leetcode_167/leetcode_167/leetcode_167.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | 有 |
| `leetcode_169` | `leetcode_169/leetcode_169/leetcode_169.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | 有 |
| `leetcode_187` | `leetcode_187/leetcode_187.Tests/leetcode_187.Tests.csproj` | 測試專案 | `net10.0` | 是 | 有 | 有 | 有 |
| `leetcode_187` | `leetcode_187/leetcode_187/leetcode_187.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | 有 |
| `leetcode_189` | `leetcode_189/leetcode_189/leetcode_189.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | 有 |
| `leetcode_190` | `leetcode_190/leetcode_190/leetcode_190.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | 有 |
| `leetcode_191` | `leetcode_191/leetcode_191/leetcode_191.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | 有 |
| `leetcode_198` | `leetcode_198/leetcode_198/leetcode_198.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | 有 |
| `leetcode_199` | `leetcode_199/leetcode_199/leetcode_199.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | **缺少** |
| `leetcode_200` | `leetcode_200/leetcode_200/leetcode_200.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | **缺少** |
| `leetcode_202` | `leetcode_202/leetcode_202/leetcode_202.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | 有 |
| `leetcode_203` | `leetcode_203/leetcode_203/leetcode_203.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | 有 |
| `leetcode_205` | `leetcode_205/leetcode_205/leetcode_205.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | 有 |
| `leetcode_206` | `leetcode_206/leetcode_206/leetcode_206.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | **缺少** |
| `leetcode_207` | `leetcode_207/leetcode_207/leetcode_207.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | **缺少** |
| `leetcode_208` | `leetcode_208/leetcode_208/leetcode_208.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | **缺少** |
| `leetcode_209` | `leetcode_209/leetcode_209/leetcode_209.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | 有 |
| `leetcode_211` | `leetcode_211/leetcode_211/leetcode_211.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | **缺少** |
| `leetcode_212` | `leetcode_212/leetcode_212/leetcode_212.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | **缺少** |
| `leetcode_213` | `leetcode_213/leetcode_213/leetcode_213.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | **缺少** |
| `leetcode_215` | `leetcode_215/leetcode_215/leetcode_215.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | 有 |
| `leetcode_217` | `leetcode_217/leetcode_217/leetcode_217.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | **缺少** |
| `leetcode_219` | `leetcode_219/leetcode_219/leetcode_219.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | 有 |
| `leetcode_222` | `leetcode_222/leetcode_222/leetcode_222.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | 有 |
| `leetcode_224` | `leetcode_224/leetcode_224/leetcode_224.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | 有 |
| `leetcode_225` | `leetcode_225/leetcode_225/leetcode_225.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | 有 |
| `leetcode_226` | `leetcode_226/leetcode_226/leetcode_226.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | **缺少** |
| `leetcode_228` | `leetcode_228/leetcode_228/leetcode_228.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | 有 |
| `leetcode_230` | `leetcode_230/leetcode_230/leetcode_230.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | 有 |
| `leetcode_231` | `leetcode_231/leetcode_231/leetcode_231.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | 有 |
| `leetcode_232` | `leetcode_232/leetcode_232/leetcode_232.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | 有 |
| `leetcode_234` | `leetcode_234/leetcode_234/leetcode_234.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | 有 |
| `leetcode_235` | `leetcode_235/leetcode_235/leetcode_235.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | **缺少** |
| `leetcode_236` | `leetcode_236/leetcode_236/leetcode_236.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | **缺少** |
| `leetcode_238` | `leetcode_238/leetcode_238/leetcode_238.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | **缺少** |
| `leetcode_240` | `leetcode_240/leetcode_240/leetcode_240.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | 有 |
| `leetcode_241` | `leetcode_241/leetcode_241/leetcode_241.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | 有 |
| `leetcode_242` | `leetcode_242/leetcode_242/leetcode_242.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | **缺少** |
| `leetcode_252` | `leetcode_252/leetcode_252/leetcode_252.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | **缺少** |
| `leetcode_260` | `leetcode_260/leetcode_260/leetcode_260.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | 有 |
| `leetcode_268` | `leetcode_268/leetcode_268/leetcode_268.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | 有 |
| `leetcode_0273` | `leetcode_0273/leetcode_0273/leetcode_0273.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | **缺少** |
| `leetcode_274` | `leetcode_274/leetcode_274/leetcode_274.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | 有 |
| `leetcode_278` | `leetcode_278/leetcode_278/leetcode_278.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | **缺少** |
| `leetcode_279` | `leetcode_279/leetcode_279/leetcode_279.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | 有 |
| `leetcode_283` | `leetcode_283/leetcode_283/leetcode_283.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | 有 |
| `leetcode_287` | `leetcode_287/leetcode_287/leetcode_287.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | 有 |
| `leetcode_290` | `leetcode_290/leetcode_290/leetcode_290.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | 有 |
| `leetcode_295` | `leetcode_295/leetcode_295/leetcode_295.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | **缺少** |
| `leetcode_297` | `leetcode_297/leetcode_297/leetcode_297.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | **缺少** |
| `leetcode_300` | `leetcode_300/leetcode_300/leetcode_300.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | **缺少** |
| `leetcode_310` | `leetcode_310/leetcode_310/leetcode_310.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | **缺少** |
| `leetcode_316` | `leetcode_316/leetcode_316.Tests/leetcode_316.Tests.csproj` | 測試專案 | `net10.0` | 是 | 有 | 有 | 有 |
| `leetcode_316` | `leetcode_316/leetcode_316/leetcode_316.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | 有 |
| `leetcode_318` | `leetcode_318/leetcode_318/leetcode_318.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | 有 |
| `leetcode_322` | `leetcode_322/leetcode_322/leetcode_322.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | **缺少** |
| `leetcode_326` | `leetcode_326/leetcode_326/leetcode_326.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | 有 |
| `leetcode_328` | `leetcode_328/leetcode_328/leetcode_328.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | 有 |
| `leetcode_338` | `leetcode_338/leetcode_338/leetcode_338.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | **缺少** |
| `leetcode_342` | `leetcode_342/leetcode_342/leetcode_342.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | 有 |
| `leetcode_344` | `leetcode_344/leetcode_344/leetcode_344.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | 有 |
| `leetcode_345` | `leetcode_345/leetcode_345/leetcode_345.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | 有 |
| `leetcode_347` | `leetcode_347/leetcode_347/leetcode_347.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | 有 |
| `leetcode_349` | `leetcode_349/leetcode_349/leetcode_349.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | **缺少** |
| `leetcode_350` | `leetcode_350/leetcode_350/leetcode_350.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | **缺少** |
| `leetcode_371` | `leetcode_371/leetcode_371/leetcode_371.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | **缺少** |
| `leetcode_374` | `leetcode_374/leetcode_374/leetcode_374.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | 有 |
| `leetcode_380` | `leetcode_380/leetcode_380/leetcode_380.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | 有 |
| `leetcode_383` | `leetcode_383/leetcode_383/leetcode_383.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | **缺少** |
| `leetcode_387` | `leetcode_387/leetcode_387/leetcode_387.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | 有 |
| `leetcode_389` | `leetcode_389/leetcode_389/leetcode_389.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | 有 |
| `leetcode_392` | `leetcode_392/leetcode_392/leetcode_392.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | 有 |
| `leetcode_396` | `leetcode_396/leetcode_396/leetcode_396.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | 有 |
| `leetcode_401` | `leetcode_401/leetcode_401/leetcode_401.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | 有 |
| `leetcode_402` | `leetcode_402/leetcode_402/leetcode_402.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | **缺少** |
| `leetcode_409` | `leetcode_409/leetcode_409/leetcode_409.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | **缺少** |
| `leetcode_412` | `leetcode_412/leetcode_412/leetcode_412.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | 有 |
| `leetcode_416` | `leetcode_416/leetcode_416/leetcode_416.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | **缺少** |
| `leetcode_417` | `leetcode_417/leetcode_417/leetcode_417.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | 有 |
| `leetcode_421` | `leetcode_421/leetcode_421/leetcode_421.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | 有 |
| `leetcode_424` | `leetcode_424/leetcode_424/leetcode_424.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | **缺少** |
| `leetcode_435` | `leetcode_435/leetcode_435/leetcode_435.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | 有 |
| `leetcode_438` | `leetcode_438/leetcode_438/leetcode_438.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | **缺少** |
| `leetcode_440` | `leetcode_440/leetcode_440/leetcode_440.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | 有 |
| `leetcode_442` | `leetcode_442/leetcode_442/leetcode_442.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | **缺少** |
| `leetcode_443` | `leetcode_443/leetcode_443/leetcode_443.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | 有 |
| `leetcode_445` | `leetcode_445/leetcode_445/leetcode_445.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | 有 |
| `leetcode_448` | `leetcode_448/leetcode_448/leetcode_448.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | 有 |
| `leetcode_451` | `leetcode_451/leetcode_451/leetcode_451.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | 有 |
| `leetcode_452` | `leetcode_452/leetcode_452/leetcode_452.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | 有 |
| `leetcode_474` | `leetcode_474/leetcode_474/leetcode_474.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | 有 |
| `leetcode_476` | `leetcode_476/leetcode_476/leetcode_476.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | **缺少** |
| `leetcode_491` | `leetcode_491/leetcode_491/leetcode_491.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | 有 |
| `leetcode_501` | `leetcode_501/leetcode_501/leetcode_501.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | 有 |
| `leetcode_502` | `leetcode_502/leetcode_502/leetcode_502.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | 有 |
| `leetcode_506` | `leetcode_506/leetcode_506/leetcode_506.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | **缺少** |
| `leetcode_515` | `leetcode_515/leetcode_515/leetcode_515.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | 有 |
| `leetcode_516` | `leetcode_516/leetcode_516/leetcode_516.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | 有 |
| `leetcode_530` | `leetcode_530/leetcode_530/leetcode_530.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | 有 |
| `leetcode_540` | `leetcode_540/leetcode_540/leetcode_540.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | 有 |
| `leetcode_542` | `leetcode_542/leetcode_542/leetcode_542.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | **缺少** |
| `leetcode_543` | `leetcode_543/leetcode_543/leetcode_543.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | **缺少** |
| `leetcode_560` | `leetcode_560/leetcode_560/leetcode_560.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | 有 |
| `leetcode_567` | `leetcode_567/leetcode_567/leetcode_567.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | 有 |
| `leetcode_572` | `leetcode_572/leetcode_572/leetcode_572.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | **缺少** |
| `leetcode_589` | `leetcode_589/leetcode_589/leetcode_589.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | 有 |
| `leetcode_590` | `leetcode_590/leetcode_590/leetcode_590.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | 有 |
| `leetcode_592` | `leetcode_592/leetcode_592/leetcode_592.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | **缺少** |
| `leetcode_594` | `leetcode_594/leetcode_594/leetcode_594.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | 有 |
| `leetcode_605` | `leetcode_605/leetcode_605/leetcode_605.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | **缺少** |
| `leetcode_617` | `leetcode_617/leetcode_617/leetcode_617.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | 有 |
| `leetcode_621` | `leetcode_621/leetcode_621/leetcode_621.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | **缺少** |
| `leetcode_633` | `leetcode_633/leetcode_633/leetcode_633.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | **缺少** |
| `leetcode_643` | `leetcode_643/leetcode_643/leetcode_643.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | 有 |
| `leetcode_645` | `leetcode_645/leetcode_645/leetcode_645.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | 有 |
| `leetcode_647` | `leetcode_647/leetcode_647/leetcode_647.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | 有 |
| `leetcode_650` | `leetcode_650/leetcode_650/leetcode_650.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | **缺少** |
| `leetcode_653` | `leetcode_653/leetcode_653/leetcode_653.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | 有 |
| `leetcode_655` | `leetcode_655/leetcode_655/leetcode_655.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | 有 |
| `leetcode_657` | `leetcode_657/leetcode_657/leetcode_657.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | 有 |
| `leetcode_662` | `leetcode_662/leetcode_662/leetcode_662.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | 有 |
| `leetcode_669` | `leetcode_669/leetcode_669/leetcode_669.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | 有 |
| `leetcode_678` | `leetcode_678/leetcode_678/leetcode_678.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | **缺少** |
| `leetcode_684` | `leetcode_684/leetcode_684/leetcode_684.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | 有 |
| `leetcode_696` | `leetcode_696/leetcode_696/leetcode_696.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | 有 |
| `leetcode_700` | `leetcode_700/leetcode_700/leetcode_700.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | 有 |
| `leetcode_704` | `leetcode_704/leetcode_704/leetcode_704.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | **缺少** |
| `leetcode_705` | `leetcode_705/leetcode_705/leetcode_705.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | 有 |
| `leetcode_713` | `leetcode_713/leetcode_713/leetcode_713.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | **缺少** |
| `leetcode_717` | `leetcode_717/leetcode_717/leetcode_717.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | 有 |
| `leetcode_721` | `leetcode_721/leetcode_721/leetcode_721.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | **缺少** |
| `leetcode_726` | `leetcode_726/leetcode_726/leetcode_726.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | **缺少** |
| `leetcode_733` | `leetcode_733/leetcode_733/leetcode_733.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | **缺少** |
| `leetcode_735` | `leetcode_735/leetcode_735/leetcode_735.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | 有 |
| `leetcode_739` | `leetcode_739/leetcode_739/leetcode_739.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | 有 |
| `leetcode_744` | `leetcode_744/leetcode_744/leetcode_744.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | 有 |
| `leetcode_746` | `leetcode_746/leetcode_746/leetcode_746.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | 有 |
| `leetcode_752` | `leetcode_752/leetcode_752/leetcode_752.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | **缺少** |
| `leetcode_757` | `leetcode_757/leetcode_757/leetcode_757.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | 有 |
| `leetcode_762` | `leetcode_762/leetcode_762/leetcode_762.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | 有 |
| `leetcode_763` | `leetcode_763/leetcode_763/leetcode_763.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | 有 |
| `leetcode_767` | `leetcode_767/leetcode_767/leetcode_767.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | 有 |
| `leetcode_769` | `leetcode_769/leetcode_769/leetcode_769.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | **缺少** |
| `leetcode_771` | `leetcode_771/leetcode_771/leetcode_771.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | 有 |
| `leetcode_779` | `leetcode_779/leetcode_779/leetcode_779.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | 有 |
| `leetcode_783` | `leetcode_783/leetcode_783/leetcode_783.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | 有 |
| `leetcode_786` | `leetcode_786/leetcode_786/leetcode_786.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | **缺少** |
| `leetcode_788` | `leetcode_788/leetcode_788/leetcode_788.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | 有 |
| `leetcode_804` | `leetcode_804/leetcode_804/leetcode_804.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | 有 |
| `leetcode_808` | `leetcode_808/leetcode_808/leetcode_808.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | 有 |
| `leetcode_812` | `leetcode_812/leetcode_812/leetcode_812.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | 有 |
| `leetcode_819` | `leetcode_819/leetcode_819/leetcode_819.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | 有 |
| `leetcode_837` | `leetcode_837/leetcode_837/leetcode_837.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | 有 |
| `leetcode_840` | `leetcode_840/leetcode_840/leetcode_840.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | **缺少** |
| `leetcode_856` | `leetcode_856/leetcode_856/leetcode_856.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | 有 |
| `leetcode_859` | `leetcode_859/leetcode_859/leetcode_859.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | 有 |
| `leetcode_860` | `leetcode_860/leetcode_860/leetcode_860.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | **缺少** |
| `leetcode_861` | `leetcode_861/leetcode_861/leetcode_861.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | **缺少** |
| `leetcode_865` | `leetcode_865/leetcode_865/leetcode_865.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | 有 |
| `leetcode_868` | `leetcode_868/leetcode_868/leetcode_868.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | 有 |
| `leetcode_869` | `leetcode_869/leetcode_869/leetcode_869.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | 有 |
| `leetcode_872` | `leetcode_872/leetcode_872/leetcode_872.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | 有 |
| `leetcode_875` | `leetcode_875/leetcode_875/leetcode_875.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | 有 |
| `leetcode_876` | `leetcode_876/leetcode_876/leetcode_876.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | **缺少** |
| `leetcode_0885` | `leetcode_0885/leetcode_0885/leetcode_0885.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | **缺少** |
| `leetcode_886` | `leetcode_886/leetcode_886/leetcode_886.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | 有 |
| `leetcode_898` | `leetcode_898/leetcode_898/leetcode_898.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | 有 |
| `leetcode_904` | `leetcode_904/leetcode_904/leetcode_904.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | 有 |
| `leetcode_912` | `leetcode_912/leetcode_912/leetcode_912.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | 有 |
| `leetcode_930` | `leetcode_930/leetcode_930/leetcode_930.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | 有 |
| `leetcode_931` | `leetcode_931/leetcode_931/leetcode_931.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | 有 |
| `leetcode_938` | `leetcode_938/leetcode_938/leetcode_938.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | 有 |
| `leetcode_944` | `leetcode_944/leetcode_944/leetcode_944.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | 有 |
| `leetcode_946` | `leetcode_946/leetcode_946/leetcode_946.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | 有 |
| `leetcode_947` | `leetcode_947/leetcode_947/leetcode_947.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | 有 |
| `leetcode_953` | `leetcode_953/leetcode_953/leetcode_953.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | 有 |
| `leetcode_955` | `leetcode_955/leetcode_955/leetcode_955.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | 有 |
| `leetcode_961` | `leetcode_961/leetcode_961/leetcode_961.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | **缺少** |
| `leetcode_973` | `leetcode_973/leetcode_973/leetcode_973.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | **缺少** |
| `leetcode_974` | `leetcode_974/leetcode_974/leetcode_974.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | 有 |
| `leetcode_976` | `leetcode_976/leetcode_976/leetcode_976.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | 有 |
| `leetcode_977` | `leetcode_977/leetcode_977/leetcode_977.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | 有 |
| `leetcode_979` | `leetcode_979/leetcode_979/leetcode_979.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | **缺少** |
| `leetcode_989` | `leetcode_989/leetcode_989/leetcode_989.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | 有 |
| `leetcode_994` | `leetcode_994/leetcode_994/leetcode_994.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | **缺少** |
| `leetcode_997` | `leetcode_997/leetcode_997/leetcode_997.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | 有 |
| `leetcode_1002` | `leetcode_1002/leetcode_1002/leetcode_1002.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | **缺少** |
| `leetcode_1009` | `leetcode_1009/leetcode_1009/leetcode_1009.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | 有 |
| `leetcode_1010` | `leetcode_1010/leetcode_1010/leetcode_1010.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | 有 |
| `leetcode_1011` | `leetcode_1011/leetcode_1011/leetcode_1011.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | 有 |
| `leetcode_1013` | `leetcode_1013/leetcode_1013/leetcode_1013.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | 有 |
| `leetcode_1015` | `leetcode_1015/leetcode_1015/leetcode_1015.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | 有 |
| `leetcode_1018` | `leetcode_1018/leetcode_1018/leetcode_1018.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | 有 |
| `leetcode_1022` | `leetcode_1022/leetcode_1022/leetcode_1022.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | 有 |
| `leetcode_1026` | `leetcode_1026/leetcode_1026/leetcode_1026.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | 有 |
| `leetcode_1027` | `leetcode_1027/leetcode_1027/leetcode_1027.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | 有 |
| `leetcode_1038` | `leetcode_1038/leetcode_1038/leetcode_1038.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | **缺少** |
| `leetcode_1043` | `leetcode_1043/leetcode_1043/leetcode_1043.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | 有 |
| `leetcode_1046` | `leetcode_1046/leetcode_1046/leetcode_1046.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | 有 |
| `leetcode_1051` | `leetcode_1051/leetcode_1051/leetcode_1051.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | **缺少** |
| `leetcode_1052` | `leetcode_1052/leetcode_1052/leetcode_1052.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | **缺少** |
| `leetcode_1061` | `leetcode_1061/leetcode_1061/leetcode_1061.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | 有 |
| `leetcode_1071` | `leetcode_1071/leetcode_1071/leetcode_1071.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | **缺少** |
| `leetcode_1072` | `leetcode_1072/leetcode_1072/leetcode_1072.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | **缺少** |
| `leetcode_1079` | `leetcode_1079/leetcode_1079/leetcode_1079.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | 有 |
| `leetcode_1105` | `leetcode_1105/leetcode_1105/leetcode_1105.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | **缺少** |
| `leetcode_1110` | `leetcode_1110/leetcode_1110/leetcode_1110.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | **缺少** |
| `leetcode_1137` | `leetcode_1137/leetcode_1137/leetcode_1137.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | 有 |
| `leetcode_1143` | `leetcode_1143/leetcode_1143/leetcode_1143.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | 有 |
| `leetcode_1160` | `leetcode_1160/leetcode_1160/leetcode_1160.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | 有 |
| `leetcode_1161` | `leetcode_1161/leetcode_1161/leetcode_1161.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | 有 |
| `leetcode_1190` | `leetcode_1190/leetcode_1190/leetcode_1190.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | **缺少** |
| `leetcode_1200` | `leetcode_1200/leetcode_1200/leetcode_1200.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | 有 |
| `leetcode_1207` | `leetcode_1207/leetcode_1207/leetcode_1207.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | 有 |
| `leetcode_1218` | `leetcode_1218/leetcode_1218/leetcode_1218.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | 有 |
| `leetcode_1219` | `leetcode_1219/leetcode_1219/leetcode_1219.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | **缺少** |
| `leetcode_1232` | `leetcode_1232/leetcode_1232/leetcode_1232.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | 有 |
| `leetcode_1235` | `leetcode_1235/leetcode_1235/leetcode_1235.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | **缺少** |
| `leetcode_1248` | `leetcode_1248/leetcode_1248/leetcode_1248.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | **缺少** |
| `leetcode_1249` | `leetcode_1249/leetcode_1249/leetcode_1249.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | 有 |
| `leetcode_1262` | `leetcode_1262/leetcode_1262/leetcode_1262.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | 有 |
| `leetcode_1266` | `leetcode_1266/leetcode_1266/leetcode_1266.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | 有 |
| `leetcode_1277` | `leetcode_1277/leetcode_1277/leetcode_1277.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | 有 |
| `leetcode_1290` | `leetcode_1290/leetcode_1290/leetcode_1290.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | 有 |
| `leetcode_1291` | `leetcode_1291/leetcode_1291/leetcode_1291.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | 有 |
| `leetcode_1292` | `leetcode_1292/leetcode_1292/leetcode_1292.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | 有 |
| `leetcode_1298` | `leetcode_1298/leetcode_1298/leetcode_1298.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | 有 |
| `leetcode_1304` | `leetcode_1304/leetcode_1304/leetcode_1304.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | 有 |
| `leetcode_1317` | `leetcode_1317/leetcode_1317/leetcode_1317.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | 有 |
| `leetcode_1318` | `leetcode_1318/leetcode_1318/leetcode_1318.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | 有 |
| `leetcode_1323` | `leetcode_1323/leetcode_1323/leetcode_1323.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | 有 |
| `leetcode_1332` | `leetcode_1332/leetcode_1332/leetcode_1332.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | 有 |
| `leetcode_1334` | `leetcode_1334/leetcode_1334/leetcode_1334.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | **缺少** |
| `leetcode_1337` | `leetcode_1337/leetcode_1337/leetcode_1337.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | 有 |
| `leetcode_1338` | `leetcode_1338/leetcode_1338/leetcode_1338.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | 有 |
| `leetcode_1339` | `leetcode_1339/leetcode_1339/leetcode_1339.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | 有 |
| `leetcode_1342` | `leetcode_1342/leetcode_1342/leetcode_1342.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | 有 |
| `leetcode_1351` | `leetcode_1351/leetcode_1351/leetcode_1351.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | 有 |
| `leetcode_1353` | `leetcode_1353/leetcode_1353/leetcode_1353.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | 有 |
| `leetcode_1356` | `leetcode_1356/leetcode_1356/leetcode_1356.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | 有 |
| `leetcode_1372` | `leetcode_1372/leetcode_1372/leetcode_1372.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | 有 |
| `leetcode_1380` | `leetcode_1380/leetcode_1380/leetcode_1380.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | **缺少** |
| `leetcode_1382` | `leetcode_1382/leetcode_1382/leetcode_1382.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | 有 |
| `leetcode_1394` | `leetcode_1394/leetcode_1394/leetcode_1394.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | 有 |
| `leetcode_1395` | `leetcode_1395/leetcode_1395/leetcode_1395.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | **缺少** |
| `leetcode_1399` | `leetcode_1399/leetcode_1399/leetcode_1399.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | **缺少** |
| `leetcode_1404` | `leetcode_1404/leetcode_1404/leetcode_1404.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | 有 |
| `leetcode_1423` | `leetcode_1423/leetcode_1423/leetcode_1423.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | 有 |
| `leetcode_1424` | `leetcode_1424/leetcode_1424/leetcode_1424.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | 有 |
| `leetcode_1431` | `leetcode_1431/leetcode_1431/leetcode_1431.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | **缺少** |
| `leetcode_1432` | `leetcode_1432/leetcode_1432/leetcode_1432.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | 有 |
| `leetcode_1436` | `leetcode_1436/leetcode_1436/leetcode_1436.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | 有 |
| `leetcode_1437` | `leetcode_1437/leetcode_1437/leetcode_1437.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | 有 |
| `leetcode_1438` | `leetcode_1438/leetcode_1438/leetcode_1438.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | **缺少** |
| `leetcode_1441` | `leetcode_1441/leetcode_1441/leetcode_1441.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | 有 |
| `leetcode_1442` | `leetcode_1442/leetcode_1442/leetcode_1442.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | **缺少** |
| `leetcode_1443` | `leetcode_1443/leetcode_1443/leetcode_1443.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | 有 |
| `leetcode_1458` | `leetcode_1458/leetcode_1458/leetcode_1458.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | 有 |
| `leetcode_1460` | `leetcode_1460/leetcode_1460/leetcode_1460.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | **缺少** |
| `leetcode_1461` | `leetcode_1461/leetcode_1461/leetcode_1461.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | 有 |
| `leetcode_1464` | `leetcode_1464/leetcode_1464/leetcode_1464.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | 有 |
| `leetcode_1470` | `leetcode_1470/leetcode_1470/leetcode_1470.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | 有 |
| `leetcode_1475` | `leetcode_1475/leetcode_1475/leetcode_1475.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | **缺少** |
| `leetcode_1480` | `leetcode_1480/leetcode_1480/leetcode_1480.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | 有 |
| `leetcode_1481` | `leetcode_1481/leetcode_1481/leetcode_1481.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | 有 |
| `leetcode_1482` | `leetcode_1482/leetcode_1482/leetcode_1482.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | **缺少** |
| `leetcode_1488` | `leetcode_1488/leetcode_1488/leetcode_1488.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | 有 |
| `leetcode_1493` | `leetcode_1493/leetcode_1493/leetcode_1493.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | 有 |
| `leetcode_1498` | `leetcode_1498/leetcode_1498/leetcode_1498.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | 有 |
| `leetcode_1502` | `leetcode_1502/leetcode_1502/leetcode_1502.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | 有 |
| `leetcode_1508` | `leetcode_1508/leetcode_1508/leetcode_1508.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | **缺少** |
| `leetcode_1509` | `leetcode_1509/leetcode_1509/leetcode_1509.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | **缺少** |
| `leetcode_1512` | `leetcode_1512/leetcode_1512/leetcode_1512.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | 有 |
| `leetcode_1518` | `leetcode_1518/leetcode_1518/leetcode_1518.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | 有 |
| `leetcode_1523` | `leetcode_1523/leetcode_1523/leetcode_1523.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | 有 |
| `leetcode_1526` | `leetcode_1526/leetcode_1526/leetcode_1526.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | 有 |
| `leetcode_1530` | `leetcode_1530/leetcode_1530/leetcode_1530.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | **缺少** |
| `leetcode_1535` | `leetcode_1535/leetcode_1535/leetcode_1535.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | 有 |
| `leetcode_1539` | `leetcode_1539/leetcode_1539/leetcode_1539.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | 有 |
| `leetcode_1544` | `leetcode_1544/leetcode_1544/leetcode_1544.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | 有 |
| `leetcode_1550` | `leetcode_1550/leetcode_1550/leetcode_1550.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | **缺少** |
| `leetcode_1576` | `leetcode_1576/leetcode_1576/leetcode_1576.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | 有 |
| `leetcode_1578` | `leetcode_1578/leetcode_1578/leetcode_1578.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | 有 |
| `leetcode_1582` | `leetcode_1582/leetcode_1582/leetcode_1582.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | 有 |
| `leetcode_1594` | `leetcode_1594/leetcode_1594/leetcode_1594.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | 有 |
| `leetcode_1603` | `leetcode_1603/leetcode_1603/leetcode_1603.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | 有 |
| `leetcode_1605` | `leetcode_1605/leetode_1605/leetode_1605.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | **缺少** |
| `leetcode_1608` | `leetcode_1608/leetcode_1608/leetcode_1608.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | **缺少** |
| `leetcode_1625` | `leetcode_1625/leetcode_1625/leetcode_1625.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | 有 |
| `leetcode_1626` | `leetcode_1626/leetcode_1626/leetcode_1626.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | 有 |
| `leetcode_1630` | `leetcode_1630/leetcode_1630/leetcode_1630.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | 有 |
| `leetcode_1636` | `leetcode_1636/leetcode_1636/leetcode_1636.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | **缺少** |
| `leetcode_1642` | `leetcode_1642/leetcode_1642/leetcode_1642.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | 有 |
| `leetcode_1652` | `leetcode_1652/leetcode_1652/leetcode_1652.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | **缺少** |
| `leetcode_1653` | `leetcode_1653/leetcode_1653/leetcode_1653.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | 有 |
| `leetcode_1657` | `leetcode_1657/leetcode_1657/leetcode_1657.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | 有 |
| `leetcode_1663` | `leetcode_1663/leetcode_1663/leetcode_1663.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | 有 |
| `leetcode_1680` | `leetcode_1680/leetcode_1680/leetcode_1680.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | 有 |
| `leetcode_1685` | `leetcode_1685/leetcode_1685/leetcode_1685.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | 有 |
| `leetcode_1695` | `leetcode_1695/leetcode_1695/leetcode_1695.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | 有 |
| `leetcode_1700` | `leetcode_1700/leetcode_1700/leetcode_1700.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | **缺少** |
| `leetcode_1701` | `leetcode_1701/leetcode_1701/leetcode_1701.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | **缺少** |
| `leetcode_1704` | `leetcode_1704/leetcode_1704/leetcode_1704.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | 有 |
| `leetcode_1716` | `leetcode_1716/leetcode_1716/leetcode_1716.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | 有 |
| `leetcode_1717` | `leetcode_1717/leetcode_1717/leetcode_1717.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | 有 |
| `leetcode_1721` | `leetcode_1721/leetcode_1721/leetcode_1721.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | 有 |
| `leetcode_1743` | `leetcode_1743/leetcode_1743/leetcode_1743.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | 有 |
| `leetcode_1750` | `leetcode_1750/leetcode_1750/leetcode_1750.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | 有 |
| `leetcode_1759` | `leetcode_1759/leetcode_1759/leetcode_1759.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | 有 |
| `leetcode_1768` | `leetcode_1768/leetcode_1768/leetcode_1768.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | **缺少** |
| `leetcode_1784` | `leetcode_1784/leetcode_1784/leetcode_1784.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | 有 |
| `leetcode_1791` | `leetcode_1791/leetcode_1791/leetcode_1791.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | **缺少** |
| `leetcode_1792` | `leetcode_1792/leetcode_1792/leetcode_1792.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | 有 |
| `leetcode_1802` | `leetcode_1802/leetcode_1802/leetcode_1802.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | 有 |
| `leetcode_1816` | `leetcode_1816/leetcode_1816/leetcode_1816.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | 有 |
| `leetcode_1818` | `leetcode_1818/leetcode_1818/leetcode_1818.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | 有 |
| `leetcode_1822` | `leetcode_1822/leetcode_1822/leetcode_1822.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | 有 |
| `leetcode_1833` | `leetcode_1833/leetcode_1833/leetcode_1833.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | 有 |
| `leetcode_1848` | `leetcode_1848/leetcode_1848/leetcode_1848.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | 有 |
| `leetcode_1857` | `leetcode_1857/leetcode_1857/leetcode_1857.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | 有 |
| `leetcode_1861` | `leetcode_1861/leetcode_1861/leetcode_1861.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | 有 |
| `leetcode_1863` | `leetcode_1863/leetcode_1863/leetcode_1863.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | **缺少** |
| `leetcode_1865` | `leetcode_1865/leetcode_1865/leetcode_1865.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | 有 |
| `leetcode_1877` | `leetcode_1877/leetcode_1877/leetcode_1877.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | 有 |
| `leetcode_1886` | `leetcode_1886/leetcode_1886/leetcode_1886.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | 有 |
| `leetcode_1887` | `leetcode_1887/leetcode_1887/leetcode_1887.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | 有 |
| `leetcode_1897` | `leetcode_1897/leetcode_1897/leetcode_1897.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | 有 |
| `leetcode_1909` | `leetcode_1909/leetcode_1909/leetcode_1909.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | 有 |
| `leetcode_1913` | `leetcode_1913/leetcode_1913/leetcode_1913.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | 有 |
| `leetcode_1920` | `leetcode_1920/leetcode_1920/leetcode_1920.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | 有 |
| `leetcode_1921` | `leetcode_1921/leetcode_1921/leetcode_1921.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | 有 |
| `leetcode_1925` | `leetcode_1925/leetcode_1925/leetcode_1925.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | 有 |
| `leetcode_1930` | `leetcode_1930/leetcode_1930/leetcode_1930.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | 有 |
| `leetcode_1952` | `leetcode_1952/leetcode_1952/leetcode_1952.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | 有 |
| `leetcode_1957` | `leetcode_1957/leetcode_1957/leetcode_1957.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | 有 |
| `leetcode_1971` | `leetcode_1971/leetcode_1971/leetcode_1971.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | 有 |
| `leetcode_1975` | `leetcode_1975/leetcode_1975/leetcode_1975.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | 有 |
| `leetcode_1980` | `leetcode_1980/leetcode_1980/leetcode_1980.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | 有 |
| `leetcode_1984` | `leetcode_1984/leetcode_1984/leetcode_1984.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | 有 |
| `leetcode_1992` | `leetcode_1992/leetcode_1992/leetcode_1992.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | **缺少** |
| `leetcode_2000` | `leetcode_2000/leetcode_2000/leetcode_2000.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | 有 |
| `leetcode_2011` | `leetcode_2011/leetcode_2011/leetcode_2011.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | 有 |
| `leetcode_2016` | `leetcode_2016/leetcode_2016/leetcode_2016.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | 有 |
| `leetcode_2024` | `leetcode_2024/leetcode_2024/leetcode_2024.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | 有 |
| `leetcode_2033` | `leetcode_2033/leetcode_2033/leetcode_2033.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | 有 |
| `leetcode_2037` | `leetcode_2037/leetcode_2037/leetcode_2037.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | **缺少** |
| `leetcode_2038` | `leetcode_2038/leetcode_2038/leetcode_2038.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | 有 |
| `leetcode_2040` | `leetcode_2040/leetcode_2040/leetcode_2040.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | 有 |
| `leetcode_2043` | `leetcode_2043/leetcode_2043/leetcode_2043.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | 有 |
| `leetcode_2044` | `leetcode_2044/leetcode_2044/leetcode_2044.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | 有 |
| `leetcode_2048` | `leetcode_2048/leetcode_2048/leetcode_2048.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | 有 |
| `leetcode_2053` | `leetcode_2053/leetcode_2053/leetcode_2053.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | **缺少** |
| `leetcode_2058` | `leetcode_2058/leetcode_2058/leetcode_2058.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | **缺少** |
| `leetcode_2078` | `leetcode_2078/leetcode_2078/leetcode_2078.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | 有 |
| `leetcode_2081` | `leetcode_2081/leetcode_2081/leetcode_2081.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | 有 |
| `leetcode_2085` | `leetcode_2085/leetcode_2085/leetcode_2085.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | 有 |
| `leetcode_2106` | `leetcode_2106/leetcode_2106/leetcode_2106.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | 有 |
| `leetcode_2108` | `leetcode_2108/leetcode_2108/leetcode_2108.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | 有 |
| `leetcode_2110` | `leetcode_2110/leetcode_2110/leetcode_2110.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | 有 |
| `leetcode_2125` | `leetcode_2125/leetcode_2125/leetcode_2125.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | 有 |
| `leetcode_2129` | `leetcode_2129/leetcode_2129/leetcode_2129.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | **缺少** |
| `leetcode_2130` | `leetcode_2130/leetcode_2130/leetcode_2130.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | 有 |
| `leetcode_2131` | `leetcode_2131/leetcode_2131/leetcode_2131.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | 有 |
| `leetcode_2134` | `leetcode_2134/leetcode_2134/leetcode_2134.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | **缺少** |
| `leetcode_2138` | `leetcode_2138/leetcode_2138/leetcode_2138.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | 有 |
| `leetcode_2149` | `leetcode_2149/leetcode_2149/leetcode_2149.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | 有 |
| `leetcode_2154` | `leetcode_2154/leetcode_2154.Tests/leetcode_2154.Tests.csproj` | 測試專案 | `net10.0` | 是 | 有 | 有 | 有 |
| `leetcode_2154` | `leetcode_2154/leetcode_2154/leetcode_2154.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | 有 |
| `leetcode_2163` | `leetcode_2163/leetcode_2163/leetcode_2163.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | 有 |
| `leetcode_2181` | `leetcode_2181/leetcode_2181/leetcode_2181.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | **缺少** |
| `leetcode_2182` | `leetcode_2182/leetcode_2182/leetcode_2182.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | **缺少** |
| `leetcode_2187` | `leetcode_2187/leetcode_2187/leetcode_2187.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | 有 |
| `leetcode_2200` | `leetcode_2200/leetcode_2200/leetcode_2200.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | 有 |
| `leetcode_2210` | `leetcode_2210/leetcode_2210/leetcode_2210.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | 有 |
| `leetcode_2211` | `leetcode_2211/leetcode_2211/leetcode_2211.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | 有 |
| `leetcode_2215` | `leetcode_2215/leetcode_2215/leetcode_2215.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | 有 |
| `leetcode_2221` | `leetcode_2221/leetcode_2221/leetcode_2221.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | 有 |
| `leetcode_2225` | `leetcode_2225/leetcode_2225/leetcode_2225.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | **缺少** |
| `leetcode_2243` | `leetcode_2243/leetcode_2243/leetcode_2243.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | 有 |
| `leetcode_2244` | `leetcode_2244/leetcode_2244/leetcode_2244.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | 有 |
| `leetcode_2264` | `leetcode_2264/leetcode_2264/leetcode_2264.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | 有 |
| `leetcode_2265` | `leetcode_2265/leetcode_2265/leetcode_2265.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | 有 |
| `leetcode_2285` | `leetcode_2285/leetcode_2285/leetcode_2285.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | **缺少** |
| `leetcode_2322` | `leetcode_2322/leetcode_2322/leetcode_2322.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | 有 |
| `leetcode_2327` | `leetcode_2327/leetcode_2327/leetcode_2327.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | 有 |
| `leetcode_2331` | `leetcode_2331/leetcode_2331/leetcode_2331.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | **缺少** |
| `leetcode_2348` | `leetcode_2348/leetcode_2348/leetcode_2348.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | 有 |
| `leetcode_2353` | `leetcode_2353/leetcode_2353/leetcode_2353.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | 有 |
| `leetcode_2359` | `leetcode_2359/leetcode_2359/leetcode_2359.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | 有 |
| `leetcode_2370` | `leetcode_2370/leetcode_2370/leetcode_2370.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | **缺少** |
| `leetcode_2373` | `leetcode_2373/leetcode_2373/leetcode_2373.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | **缺少** |
| `leetcode_2385` | `leetcode_2385/leetcode_2385/leetcode_2385.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | 有 |
| `leetcode_2390` | `leetcode_2390/leetcode_2390/leetcode_2390.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | 有 |
| `leetcode_2395` | `leetcode_2395/leetcode_2395/leetcode_2395.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | 有 |
| `leetcode_2411` | `leetcode_2411/leetcode_2411/leetcode_2411.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | 有 |
| `leetcode_2418` | `leetcode_2418/leetcode_2418/leetcode_2418.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | 有 |
| `leetcode_2419` | `leetcode_2419/leetcode_2419/leetcode_2419.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | 有 |
| `leetcode_2434` | `leetcode_2434/leetcode_2434/leetcode_2434.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | 有 |
| `leetcode_2438` | `leetcode_2438/leetcode_2438/leetcode_2438.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | 有 |
| `leetcode_2441` | `leetcode_2441/leetcode_2441/leetcode_2441.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | **缺少** |
| `leetcode_2452` | `leetcode_2452/leetcode_2452/leetcode_2452.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | 有 |
| `leetcode_2461` | `leetcode_2461/leetcode_2461/leetcode_2461.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | **缺少** |
| `leetcode_2471` | `leetcode_2471/leetcode_2471/leetcode_2471.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | **缺少** |
| `leetcode_2483` | `leetcode_2483/leetcode_2483/leetcode_2483.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | 有 |
| `leetcode_2485` | `leetcode_2485/leetcode_2485/leetcode_2485.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | **缺少** |
| `leetcode_2486` | `leetcode_2486/leetcode_2486/leetcode_2486.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | **缺少** |
| `leetcode_2515` | `leetcode_2515/leetcode_2515/leetcode_2515.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | 有 |
| `leetcode_2520` | `leetcode_2520/leetcode_2520/leetcode_2520.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | 有 |
| `leetcode_2540` | `leetcode_2540/leetcode_2540/leetcode_2540.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | **缺少** |
| `leetcode_2558` | `leetcode_2558/leetcode_2558/leetcode_2558.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | 有 |
| `leetcode_2561` | `leetcode_2561/leetcode_2561/leetcode_2561.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | 有 |
| `leetcode_2566` | `leetcode_2566/leetcode_2566/leetcode_2566.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | 有 |
| `leetcode_2571` | `leetcode_2571/leetcode_2571/leetcode_2571.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | 有 |
| `leetcode_2582` | `leetcode_2582/leetcode_2582/leetcode_2582.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | **缺少** |
| `leetcode_2591` | `leetcode_2591/leetcode_2591/leetcode_2591.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | 有 |
| `leetcode_2593` | `leetcode_2593/leetcode_2593/leetcode_2593.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | 有 |
| `leetcode_2609` | `leetcode_2609/leetcode_2609/leetcode_2609.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | 有 |
| `leetcode_2615` | `leetcode_2615/leetcode_2615/leetcode_2615.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | 有 |
| `leetcode_2616` | `leetcode_2616/leetcode_2616/leetcode_2616.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | 有 |
| `leetcode_2670` | `leetcode_2670/leetcode_2670/leetcode_2670.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | 有 |
| `leetcode_2678` | `leetcode_2678/leetcode_2678/leetcode_2678.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | 有 |
| `leetcode_2785` | `leetcode_2785/leetcode_2785/leetcode_2785.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | 有 |
| `leetcode_2787` | `leetcode_2787/leetcode_2787/leetcode_2787.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | 有 |
| `leetcode_2799` | `leetcode_2799/leetcode_2799/leetcode_2799.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | **缺少** |
| `leetcode_2816` | `leetcode_2816/leetcode_2816/leetcode_2816.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | **缺少** |
| `leetcode_2833` | `leetcode_2833/leetcode_2833/leetcode_2833.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | 有 |
| `leetcode_2864` | `leetcode_2864/leetcode_2864/leetcode_2864.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | 有 |
| `leetcode_2870` | `leetcode_2870/leetcode_2870/leetcode_2870.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | 有 |
| `leetcode_2906` | `leetcode_2906/leetcode_2906/leetcode_2906.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | 有 |
| `leetcode_2917` | `leetcode_2917/leetcode_2917/leetcode_2917.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | **缺少** |
| `leetcode_2918` | `leetcode_2918/leetcode_2918/leetcode_2918.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | 有 |
| `leetcode_2929` | `leetcode_2929/leetcode_2929/leetcode_2929.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | 有 |
| `leetcode_2962` | `leetcode_2962/leetcode_2962/leetcode_2962.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | **缺少** |
| `leetcode_2966` | `leetcode_2966/leetcode_2966/leetcode_2966.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | 有 |
| `leetcode_2971` | `leetcode_2971/leetcode_2971/leetcode_2971.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | 有 |
| `leetcode_2976` | `leetcode_2976/leetcode_2976/leetcode_2976.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | 有 |
| `leetcode_3000` | `leetcode_3000/leetcode_3000/leetcode_3000.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | 有 |
| `leetcode_3005` | `leetcode_3005/leetcode_3005/leetcode_3005.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | 有 |
| `leetcode_3016` | `leetcode_3016/leetcode_3016/leetcode_3016.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | **缺少** |
| `leetcode_3070` | `leetcode_3070/leetcode_3070/leetcode_3070.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | 有 |
| `leetcode_3074` | `leetcode_3074/leetcode_3074/leetcode_3074.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | 有 |
| `leetcode_3075` | `leetcode_3075/leetcode_3075/leetcode_3075.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | 有 |
| `leetcode_3100` | `leetcode_3100/leetcode_3100/leetcode_3100.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | **缺少** |
| `leetcode_3110` | `leetcode_3110/leetcode_3110/leetcode_3110.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | **缺少** |
| `leetcode_3129` | `leetcode_3129/leetcode_3129/leetcode_3129.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | 有 |
| `leetcode_3136` | `leetcode_3136/leetcode_3136/leetcode_3136.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | 有 |
| `leetcode_3148` | `leetcode_3148/leetcode_3148/leetcode_3148.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | **缺少** |
| `leetcode_3163` | `leetcode_3163/leetcode_3163/leetcode_3163.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | **缺少** |
| `leetcode_3169` | `leetcode_3169/leetcode_3169/leetcode_3169.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | 有 |
| `leetcode_3170` | `leetcode_3170/leetcode_3170/leetcode_3170.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | 有 |
| `leetcode_3186` | `leetcode_3186/leetcode_3186/leetcode_3186.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | 有 |
| `leetcode_3190` | `leetcode_3190/leetcode_3190/leetcode_3190.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | **缺少** |
| `leetcode_3195` | `leetcode_3195/leetcode_3195/leetcode_3195.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | 有 |
| `leetcode_3201` | `leetcode_3201/leetcode_3201/leetcode_3201.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | 有 |
| `leetcode_3202` | `leetcode_3202/leetcode_3202/leetcode_3202.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | 有 |
| `leetcode_3212` | `leetcode_3212/leetcode_3212/leetcode_3212.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | 有 |
| `leetcode_3217` | `leetcode_3217/leetcode_3217/leetcode_3217.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | 有 |
| `leetcode_3227` | `leetcode_3227/leetcode_3227/leetcode_3227.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | 有 |
| `leetcode_3228` | `leetcode_3228/leetcode_3228/leetcode_3228.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | 有 |
| `leetcode_3234` | `leetcode_3234/leetcode_3234/leetcode_3234.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | 有 |
| `leetcode_3304` | `leetcode_3304/leetcode_3304/leetcode_3304.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | 有 |
| `leetcode_3307` | `leetcode_3307/leetcode_3307/leetcode_3307.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | 有 |
| `leetcode_3314` | `leetcode_3314/leetcode_3314/leetcode_3314.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | 有 |
| `leetcode_3318` | `leetcode_3318/leetcode_3318/leetcode_3318.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | 有 |
| `leetcode_3321` | `leetcode_3321/leetcode_3321/leetcode_3321.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | 有 |
| `leetcode_3330` | `leetcode_3330/leetcode_3330/leetcode_3330.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | 有 |
| `leetcode_3354` | `leetcode_3354/leetcode_3354/leetcode_3354.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | 有 |
| `leetcode_3355` | `leetcode_3355/leetcode_3355/leetcode_3355.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | 有 |
| `leetcode_3362` | `leetcode_3362/leetcode_3362/leetcode_3362.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | 有 |
| `leetcode_3370` | `leetcode_3370/leetcode_3370/leetcode_3370.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | 有 |
| `leetcode_3372` | `leetcode_3372/leetcode_3372/leetcode_3372.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | 有 |
| `leetcode_3379` | `leetcode_3379/leetcode_3379/leetcode_3379.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | 有 |
| `leetcode_3381` | `leetcode_3381/leetcode_3381/leetcode_3381.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | 有 |
| `leetcode_3403` | `leetcode_3403/leetcode_3403/leetcode_3403.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | 有 |
| `leetcode_3405` | `leetcode_3405/leetcode_3405/leetcode_3405.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | 有 |
| `leetcode_3408` | `leetcode_3408/leetcode_3408/leetcode_3408.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | 有 |
| `leetcode_3423` | `leetcode_3423/leetcode_3423/leetcode_3423.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | 有 |
| `leetcode_3432` | `leetcode_3432/leetcode_3432/leetcode_3432.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | 有 |
| `leetcode_3439` | `leetcode_3439/leetcode_3439/leetcode_3439.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | 有 |
| `leetcode_3440` | `leetcode_3440/leetcode_3440/leetcode_3440.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | 有 |
| `leetcode_3442` | `leetcode_3442/leetcode_3442/leetcode_3442.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | 有 |
| `leetcode_3445` | `leetcode_3445/leetcode_3445/leetcode_3445.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | 有 |
| `leetcode_3453` | `leetcode_3453/leetcode_3453/leetcode_3453.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | 有 |
| `leetcode_3477` | `leetcode_3477/leetcode_3477/leetcode_3477.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | 有 |
| `leetcode_3479` | `leetcode_3479/leetcode_3479/leetcode_3479.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | 有 |
| `leetcode_3484` | `leetcode_3484/leetcode_3484/leetcode_3484.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | 有 |
| `leetcode_3487` | `leetcode_3487/leetcode_3487/leetcode_3487.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | 有 |
| `leetcode_3488` | `leetcode_3488/leetcode_3488/leetcode_3488.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | **缺少** |
| `leetcode_3507` | `leetcode_3507/leetcode_3507/leetcode_3507.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | 有 |
| `leetcode_3510` | `leetcode_3510/leetcode_3510/leetcode_3510.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | 有 |
| `leetcode_3516` | `leetcode_3516/leetcode_3516/leetcode_3516.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | 有 |
| `leetcode_3531` | `leetcode_3531/leetcode_3531/leetcode_3531.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | 有 |
| `leetcode_3539` | `leetcode_3539/leetcode_3539/leetcode_3539.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | 有 |
| `leetcode_3541` | `leetcode_3541/leetcode_3541/leetcode_3541.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | 有 |
| `leetcode_3542` | `leetcode_3542/leetcode_3542/leetcode_3542.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | 有 |
| `leetcode_3577` | `leetcode_3577/leetcode_3577/leetcode_3577.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | 有 |
| `leetcode_3583` | `leetcode_3583/leetcode_3583/leetcode_3583.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | 有 |
| `leetcode_3607` | `leetcode_3607/leetcode_3607/leetcode_3607.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | 有 |
| `leetcode_3623` | `leetcode_3623/leetcode_3623/leetcode_3623.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | 有 |
| `leetcode_3625` | `leetcode_3625/leetcode_3625/leetcode_3625.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | 有 |
| `leetcode_3650` | `leetcode_3650/leetcode_3650/leetcode_3650.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | 有 |
| `leetcode_3651` | `leetcode_3651/leetcode_3651/leetcode_3651.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | 有 |
| `leetcode_3660` | `leetcode_3660/leetcode_3660/leetcode_3660.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | 有 |
| `leetcode_3713` | `leetcode_3713/leetcode_3713/leetcode_3713.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | 有 |
| `leetcode_3714` | `leetcode_3714/leetcode_3714/leetcode_3714.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | 有 |
| `leetcode_3719` | `leetcode_3719/leetcode_3719/leetcode_3719.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | 有 |
| `leetcode_3740` | `leetcode_3740/leetcode_3740/leetcode_3740.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | 有 |
| `leetcode_3761` | `leetcode_3761/leetcode_3761/leetcode_3761.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | 有 |
| `leetcode_3783` | `leetcode_3783/leetcode_3783/leetcode_3783.csproj` | 主專案 | `net10.0` | 是 | 有 | 有 | 有 |

## 缺少 `.editorconfig` 的資料夾（0）



## 缺少 `.gitignore` 的資料夾（0）



## 缺少 `README.md` 的資料夾（162）

- `leetcode_008`
- `leetcode_015`
- `leetcode_017`
- `leetcode_020`
- `leetcode_021`
- `leetcode_023`
- `leetcode_039`
- `leetcode_040`
- `leetcode_042`
- `leetcode_046`
- `leetcode_053`
- `leetcode_054`
- `leetcode_056`
- `leetcode_062`
- `leetcode_067`
- `leetcode_070`
- `leetcode_073`
- `leetcode_075`
- `leetcode_076`
- `leetcode_078`
- `leetcode_079`
- `leetcode_084`
- `leetcode_091`
- `leetcode_100`
- `leetcode_102`
- `leetcode_104`
- `leetcode_121`
- `leetcode_122`
- `leetcode_125`
- `leetcode_127`
- `leetcode_139`
- `leetcode_141`
- `leetcode_143`
- `leetcode_150`
- `leetcode_152`
- `leetcode_199`
- `leetcode_200`
- `leetcode_206`
- `leetcode_207`
- `leetcode_208`
- `leetcode_211`
- `leetcode_212`
- `leetcode_213`
- `leetcode_217`
- `leetcode_226`
- `leetcode_235`
- `leetcode_236`
- `leetcode_238`
- `leetcode_242`
- `leetcode_252`
- `leetcode_0273`
- `leetcode_278`
- `leetcode_295`
- `leetcode_297`
- `leetcode_300`
- `leetcode_310`
- `leetcode_322`
- `leetcode_338`
- `leetcode_349`
- `leetcode_350`
- `leetcode_371`
- `leetcode_383`
- `leetcode_402`
- `leetcode_409`
- `leetcode_416`
- `leetcode_424`
- `leetcode_438`
- `leetcode_442`
- `leetcode_476`
- `leetcode_506`
- `leetcode_542`
- `leetcode_543`
- `leetcode_572`
- `leetcode_592`
- `leetcode_605`
- `leetcode_621`
- `leetcode_633`
- `leetcode_650`
- `leetcode_678`
- `leetcode_704`
- `leetcode_713`
- `leetcode_721`
- `leetcode_726`
- `leetcode_733`
- `leetcode_752`
- `leetcode_769`
- `leetcode_786`
- `leetcode_840`
- `leetcode_860`
- `leetcode_861`
- `leetcode_876`
- `leetcode_0885`
- `leetcode_961`
- `leetcode_973`
- `leetcode_979`
- `leetcode_994`
- `leetcode_1002`
- `leetcode_1038`
- `leetcode_1051`
- `leetcode_1052`
- `leetcode_1071`
- `leetcode_1072`
- `leetcode_1105`
- `leetcode_1110`
- `leetcode_1190`
- `leetcode_1219`
- `leetcode_1235`
- `leetcode_1248`
- `leetcode_1334`
- `leetcode_1380`
- `leetcode_1395`
- `leetcode_1399`
- `leetcode_1431`
- `leetcode_1438`
- `leetcode_1442`
- `leetcode_1460`
- `leetcode_1475`
- `leetcode_1482`
- `leetcode_1508`
- `leetcode_1509`
- `leetcode_1530`
- `leetcode_1550`
- `leetcode_1605`
- `leetcode_1608`
- `leetcode_1636`
- `leetcode_1652`
- `leetcode_1700`
- `leetcode_1701`
- `leetcode_1768`
- `leetcode_1791`
- `leetcode_1863`
- `leetcode_1992`
- `leetcode_2037`
- `leetcode_2053`
- `leetcode_2058`
- `leetcode_2129`
- `leetcode_2134`
- `leetcode_2181`
- `leetcode_2182`
- `leetcode_2225`
- `leetcode_2285`
- `leetcode_2331`
- `leetcode_2370`
- `leetcode_2373`
- `leetcode_2441`
- `leetcode_2461`
- `leetcode_2471`
- `leetcode_2485`
- `leetcode_2486`
- `leetcode_2540`
- `leetcode_2582`
- `leetcode_2799`
- `leetcode_2816`
- `leetcode_2917`
- `leetcode_2962`
- `leetcode_3016`
- `leetcode_3100`
- `leetcode_3110`
- `leetcode_3148`
- `leetcode_3163`
- `leetcode_3190`
- `leetcode_3488`

## `.editorconfig`、`.gitignore`、`README.md` 三者皆缺少的資料夾（0）



## 統計口徑與驗證邊界

- 版本以每個 Git 追蹤 `.csproj` 內的 `<TargetFramework>` 為準，不依賴根目錄 README 的歷史文字。
- 題目資料夾統計以 repository 根目錄中實際存在的 `leetcode_*` 目錄為準。
- 根 README 維持 608 題一列；四個測試專案只計入 612 個 `.csproj` 總數。
- 本報告只記錄版本與指定檔案的存在狀態，不自動建立其他缺少檔案，也不更新 GitHub Issue。
