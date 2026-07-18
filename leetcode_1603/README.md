# LeetCode 1603 - Design Parking System（設計停車系統）

- [LeetCode English](https://leetcode.com/problems/design-parking-system/)
- [LeetCode 繁體中文](https://leetcode.cn/problems/design-parking-system/)

## 題目說明

`ParkingSystem` 在建立時取得大型、中型與小型車位數量。每次呼叫 `AddCar(carType)`，
系統僅能在相符車種仍有空位時接受車輛；成功回傳 `true` 並消耗一個車位，否則回傳
`false`。

## 限制條件與 API

- `0 <= big, medium, small <= 1000`
- `carType` 為 `1`（大型）、`2`（中型）或 `3`（小型）。
- 最多呼叫 `AddCar` 1000 次。

```csharp
public sealed class ParkingSystem
{
    public ParkingSystem(int big, int medium, int small);
    public bool AddCar(int carType);
}
```

## 設計、不變量與取捨

採用三個私有 instance `int` 欄位，分別保存大、中、小型的剩餘車位。這個設計直接對應
題目的三種車型：一次呼叫只讀寫相符的一個欄位，因此不會誤扣其他車種。若車位已滿，
方法立即回傳 `false` 而不扣減，計數器便永遠不會變成負值。

相較於以陣列和 `carType - 1` 索引的方案，三個明確欄位少了索引轉換與越界風險，也更
容易看出各計數器彼此獨立；代價是車種若擴充時需要新增欄位與分支，但本題固定只有三種。

- 時間複雜度：每次 `AddCar` 為 `O(1)`。
- 結果空間：`O(1)`；每次只回傳一個布林值。
- 輔助空間：`O(1)`；物件只保存三個整數計數器。

## 走查

以 `(big, medium, small) = (1, 1, 0)` 為例：

```plaintext
AddCar(1) -> true，剩餘 (0, 1, 0)
AddCar(2) -> true，剩餘 (0, 0, 0)
AddCar(3) -> false，小型車位仍為 0
AddCar(1) -> false，大型車位仍為 0
```

## Acceptance harness

| Case | 驗證重點 |
| --- | --- |
| Official example | 官方 `(1,1,0)` 操作序列。 |
| All zero | 三種容量皆為零。 |
| Mixed independent counters | 不同車種的計數器獨立消耗。 |
| Exhausted type does not affect others | 一種停滿後仍可停其他車種。 |
| Repeated zero-capacity rejection is stable | 重複拒絕不會讓容量變負。 |
| Instance isolation | 兩個物件沒有 static 狀態互相污染。 |
| Maximum capacities | 三種容量上限的第一次停車皆可成功。 |
| Exact call-limit spot check | 容量 999 時，第 1000 次呼叫為 false。 |

## 建置與執行

以下命令的工作目錄是 `leetcode_1603/` 題目根目錄：

```bash
dotnet build leetcode_1603/leetcode_1603.csproj --nologo
dotnet run --no-build --project leetcode_1603/leetcode_1603.csproj
```

本題沒有正式測試專案；`Main` 中的 deterministic acceptance harness 是可重複執行的
驗證入口。以下為 fresh run 的完整輸出：

```text
Case: Official example
Input: capacities=(1,1,0), operations=[1,2,3,1]
Expected: [true,true,false,false]
Actual: [true,true,false,false]
Result: PASS

Case: All zero
Input: capacities=(0,0,0), operations=[1,2,3]
Expected: [false,false,false]
Actual: [false,false,false]
Result: PASS

Case: Mixed independent counters
Input: capacities=(1,2,1), operations=[2,1,2,3,2,1,3]
Expected: [true,true,true,true,false,false,false]
Actual: [true,true,true,true,false,false,false]
Result: PASS

Case: Exhausted type does not affect others
Input: capacities=(1,1,1), operations=[1,1,2,3]
Expected: [true,false,true,true]
Actual: [true,false,true,true]
Result: PASS

Case: Repeated zero-capacity rejection is stable
Input: capacities=(0,0,1), operations=[1,1,3,3,3]
Expected: [false,false,true,false,false]
Actual: [false,false,true,false,false]
Result: PASS

Case: Instance isolation
Input: two capacities=(1,0,0) instances; first operations=[1,1], second operations=[1]
Expected: first=[true,false], second first add=true
Actual: first=[true,false], second first add=true
Result: PASS

Case: Maximum capacities
Input: capacities=(1000,1000,1000), operations=[1,2,3]
Expected: [true,true,true]
Actual: [true,true,true]
Result: PASS

Case: Exact call-limit spot check
Input: capacities=(999,0,0), operation=1 repeated 1000 times
Expected: first 999=true; call 1000=false
Actual: first 999=true; call 1000=false
Result: PASS

Summary: 8/8 checks passed.
```

## 最終專案結構

```plaintext
leetcode_1603/
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
└── leetcode_1603/
    ├── Program.cs
    └── leetcode_1603.csproj
```
