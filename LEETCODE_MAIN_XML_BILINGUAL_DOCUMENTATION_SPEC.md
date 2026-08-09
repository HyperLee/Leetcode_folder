# LeetCode `Main` XML 完整雙語題目文件化開發規格

## 1. 文件目的

本規格定義如何修補本 repository 各 LeetCode 解題專案 `Program.cs` 中，直接附著於程式進入點 `Main` 的 XML `<summary>`，使其一致記錄完整英文題目與完整繁體中文翻譯。

本規格是後續批次修補工作的唯一需求契約。實作者不得因「順便整理」、「提升可讀性」或「讓範例更完整」而擴張修改範圍。

相關追蹤項目：[GitHub Issue #90](https://github.com/HyperLee/Leetcode_folder/issues/90)。Issue 內的數量與清單是 2026-08-09 的掃描快照；實作時必須以目前 checkout 的重新掃描結果為準。

## 2. 目標

完成後，每個適用的解題專案都必須在實際 `Main` 方法正上方具有合法且內容完整的 XML `<summary>`，並包含：

1. 英文題號與官方英文題名。
2. `leetcode.com` 官方英文題目鏈結。
3. 官方完整英文題目內容。
4. 中文題號與繁體中文題名。
5. `leetcode.cn` 中文題目鏈結。
6. 與英文內容逐段對應的完整繁體中文翻譯。

英文與中文內容都必須涵蓋題目敘述、全部範例、限制條件，以及原題存在的 Notes、Follow-up 或其他屬於題目要求的補充段落。

## 3. 非目標

本工作不是演算法翻新、測試補強、README 整理、.NET 升級或 repository 結構修復。下列事項全部不在本規格範圍內：

- 修改任何解題演算法、資料結構或複雜度。
- 修改 `Main` 的方法簽章、方法內容、案例、輸出、exit code 或執行順序。
- 修改 `Main` 以外的 method、type、record、class、namespace 或 using directive。
- 新增第二或第三種解法。
- 新增或調整測試 harness。
- 修改 README、`docs/`、`.csproj`、solution、`.vscode`、`.editorconfig`、`.gitignore`、`AGENTS.md` 或其他設定。
- 建立缺少的專案、搬移檔案或重新命名資料夾。
- 重建已由使用者手動刪除的 `leetcode_75`。
- 以本規格作為 commit、push、PR、Issue 留言或關閉 Issue 的授權。

## 4. 規範用語

本文件使用下列強制程度：

- 「必須」：驗收不可缺少的條件。
- 「不得」：明確禁止的行為。
- 「應」：除非遇到本規格列出的例外，否則必須遵循。
- 「可」：不影響合格與否的選擇。

## 5. 適用範圍與目標辨識

### 5.1 候選資料夾

候選範圍是 repository 根目錄下名稱符合 `leetcode_*` 的一級子資料夾。掃描時必須排除：

- `bin/`、`obj/` 等建置輸出。
- 名稱以 `.Tests` 結尾的測試專案。
- 已不存在的 `leetcode_75`。
- 非一級 `leetcode_*` 解題資料夾。

不得把 Issue #90 的舊清單直接視為目前完整清單。

### 5.2 目標 `Program.cs`

每個候選資料夾應依下列順序辨識目標：

1. 找出非測試專案的 `.csproj`。
2. 找出該專案編譯範圍內的 `Program.cs`。
3. 找出具有 `static Main(...)`、`static int Main(...)`、`static Task Main(...)` 或 `static Task<int> Main(...)` 等合法進入點簽章的方法。
4. 確認欲修補的 `<summary>` 直接附著於該 `Main`，中間只能出現其他 XML 文件標籤、attribute 或空白，不得跨越 type 或其他 method 宣告。

若同一資料夾有多個非測試 `.csproj` 或多個候選 `Main`，實作者不得猜測；該專案必須標記為阻塞案例，待使用者指定目標。

### 5.3 沒有 `Main` 或 `Program.cs` 的案例

若候選資料夾不存在可辨識的 `Program.cs` 或 `Main`：

- 不得建立新檔案。
- 不得改成 top-level statements。
- 不得重構 entry point。
- 必須從本批修改中排除，並在交付報告列出原因。

## 6. 唯一允許修改的區域

每個目標專案只允許修改 `Main` 的 XML `<summary>` 區塊，亦即從直接附著於 `Main` 的 `/// <summary>` 起，到對應的 `/// </summary>` 止。

允許的操作只有：

- 在 `Main` 尚無 `<summary>` 時新增該區塊。
- 補上缺少的題名、鏈結、英文內容或繁體中文內容。
- 修復未閉合、錯誤巢狀或無法被 C# XML 文件解析器接受的 summary 標記。
- 在該 summary 內重新排列段落，使英文與中文結構一致。
- 為符合 XML 規則而轉義 summary 內的特殊字元。

`<param name="args">`、`<remarks>` 等位於 `<summary>` 外的既有文件標籤不在允許修改範圍內。即使內容不完整或格式不一致，也必須保持原狀。

## 7. 題目來源與內容真實性

### 7.1 英文內容的唯一語意基準

英文題目頁 `https://leetcode.com/problems/<slug>/description/` 是題目內容的唯一語意基準。實作者必須從目前可取得的官方頁面核對：

- 題號。
- 英文題名。
- 題目主敘述。
- 函式輸入與要求。
- 回傳值或輸出要求。
- 全部官方範例及其 Explanation。
- 全部 Constraints。
- Notes、Follow-up 或其他規範性補充。

不得使用搜尋結果摘要、第三方題解、README 舊文字或模型記憶取代官方題目內容。

若官方頁面無法存取，或題號、slug、題名互相矛盾，該專案必須標記為阻塞，不得自行補寫或推測。

### 7.2 中文題目頁的用途

中文鏈結固定使用 `https://leetcode.cn/problems/<slug>/description/`。中文頁可用來核對中文慣用題名與術語，但繁體中文內容仍必須以官方英文內容為基準翻譯，避免不同站點版本差異造成漏段。

若 `leetcode.com` 與 `leetcode.cn` 使用不同 slug，必須分別使用各站實際有效的官方鏈結，不得為了形式一致而製造無效 URL。

### 7.3 禁止杜撰

不得：

- 把解法說明誤寫成題目敘述。
- 把 README 的教學內容當成官方題目內容。
- 自行新增官方題目沒有的限制條件、保證或範例。
- 因中文翻譯方便而刪減英文條件。
- 只根據 method 名稱猜測題目。

## 8. 完整英文內容契約

英文區塊必須保留官方題目的完整資訊與順序。最低結構如下：

1. 題目開場與背景。
2. 所有輸入物件、參數及其語意。
3. 要求執行的操作或求解目標。
4. 回傳值、輸出格式或答案合法性。
5. 題目給定的所有保證。
6. `Example 1` 到最後一個官方範例，包含 Input、Output 與 Explanation。
7. 完整 Constraints。
8. 原題存在的 Note、Follow-up 或相同層級補充。

可移除網頁導覽、Premium 標記、公司標籤、Topics、Hints、Editorial、Discussion、Accepted 統計與介面按鈕文字，因為它們不屬於題目本體。

不得把完整英文內容縮寫成一至三句摘要。

## 9. 繁體中文翻譯契約

### 9.1 翻譯範圍

繁體中文區塊必須逐段對應英文區塊，包含相同數量的：

- 主敘述段落。
- 範例。
- Input、Output、Explanation。
- Constraints。
- Notes 與 Follow-up。

### 9.2 翻譯品質

翻譯必須：

- 使用繁體中文與臺灣常用技術詞彙。
- 保留變數名、method 名、class 名與 literal，不翻譯程式識別字。
- 保留數學式、索引、區間邊界、複雜度符號及比較運算的原始語意。
- 將相同術語在同一題內翻譯一致。
- 保持所有「至少」、「至多」、「恰好」、「不同」、「任意順序」等限制語氣。
- 保持範例數值、陣列、字串、tree 表示及輸出完全一致。

不得混入簡體字；專有名詞或 LeetCode 官方題名若沒有自然譯名，可在繁體中文名稱後保留英文名稱。

### 9.3 中英文對齊檢查

審查時必須逐段確認：

- 英文每一段都有中文對應段。
- 英文每一個範例都有中文對應範例。
- 英文每一條 constraint 都有中文對應條件。
- 所有數值、符號、變數名與邏輯量詞一致。

只要其中一項缺漏，就不得判定為完成。

## 10. XML `<summary>` 標準結構

`leetcode_241/leetcode_241/Program.cs` 用來示範 summary 與 `Main` 的附著位置；完整雙語段落結構應採用 `leetcode_1605/leetode_1605/Program.cs` 的雙 `<para>` 方向。`leetcode_241` 本身仍缺英文完整題目，因此也是待修補對象，不能作為內容完整性的合格樣本。

標準結構必須依下列順序：

```csharp
/// <summary>
/// 題號. Official English Title
/// https://leetcode.com/problems/official-english-slug/description/
/// <para>
/// Complete official English statement.
///
/// Example 1:
/// Input: ...
/// Output: ...
/// Explanation: ...
///
/// Constraints:
/// - ...
/// </para>
/// <para>
/// 題號. 完整繁體中文題名
/// https://leetcode.cn/problems/official-chinese-slug/description/
///
/// 與英文逐段對應的完整繁體中文題目敘述。
///
/// 範例 1：
/// 輸入：...
/// 輸出：...
/// 解釋：...
///
/// 限制條件：
/// - ...
/// </para>
/// </summary>
static void Main(string[] args)
```

上述 `...` 是結構示意符號，實際文件不得保留省略號代替題目內容。

允許把兩個題名與鏈結都放在 `<para>` 內；同一批次必須採用一致順序。不得把英文與中文逐行交錯，因為這會增加漏譯與審查難度。

## 11. XML 與 C# 文件註解規則

### 11.1 必要轉義

summary 必須是合法 XML。純文字中的特殊字元必須轉義：

| 原字元 | XML 內容 |
|---|---|
| `<` | `&lt;` |
| `>` | `&gt;` |
| `&` | `&amp;` |

例如 constraint `1 <= nums.length && nums.length <= 100` 必須寫成：

```xml
1 &lt;= nums.length &amp;&amp; nums.length &lt;= 100
```

### 11.2 程式識別字

變數名、type 或短 code literal 可使用 `<c>...</c>`，但不得為了排版而改變原始內容。例如：

```xml
Return the answer for <c>nums</c>.
```

若 code literal 本身含 `<`、`>` 或 `&`，仍必須做 XML 轉義。

### 11.3 HTML 轉換

從官方頁面取得的 HTML 不得直接貼入 summary。`<sup>`、`<sub>`、list、table 或 code 標籤必須轉換為合法且可讀的 XML 文件文字；不得留下瀏覽器專用 markup。

### 11.4 文件邊界

- 每一行必須使用 `///`。
- `<summary>` 與 `</summary>` 必須成對。
- `<para>` 必須正確閉合且不得交錯。
- `<summary>` 不得包住 `<param>`、`<returns>` 或 `Main` 宣告。
- summary 與 `Main` 之間可保留既有 `<remarks>`、`<param>` 或 attribute，但不得把它們移入 summary。

## 12. 現有內容的處理原則

### 12.1 已符合的專案

若重新掃描及人工核對確認六項內容完整，該專案不得修改。格式與推薦範本不同，不構成修改理由，只要：

- summary 正確附著於 `Main`。
- XML 合法。
- 中英文內容完整且對齊。
- 官方鏈結有效。

### 12.2 部分缺漏的專案

只補缺漏內容；但若原有排列會造成不合法 XML 或中英文難以辨識，可在同一 summary 內重新排列。不得藉此改寫 summary 外的註解。

### 12.3 原有內容不正確

若舊 summary 與目前官方題目矛盾，必須以目前官方英文題目修正。交付報告應指出是「內容修正」，而不是只記錄為格式補齊。

### 12.4 未閉合或位置錯誤

若 summary 未閉合、誤附著於 class 或其他 method：

- 可修復或在 `Main` 正上方建立合法 summary。
- 不得移動 class、method 或任何 executable code。
- 修復後必須以 compiler 的 XML 文件診斷驗證。

## 13. 缺漏分類

重新掃描時使用以下代碼，便於與 Issue #90 對照：

| 代碼 | 缺漏 |
|---|---|
| `ET` | 英文題號或題名缺失／不正確 |
| `EL` | 英文官方題目鏈結缺失／無效 |
| `ED` | 英文完整題目內容缺失／不完整 |
| `ZT` | 繁體中文題號或題名缺失／不正確 |
| `ZL` | 中文官方題目鏈結缺失／無效 |
| `ZD` | 完整繁體中文翻譯缺失／不完整 |
| `M` | `Main` 沒有合法且直接附著的 XML `<summary>` |
| `A` | 目標 `Program.cs`、`.csproj` 或 `Main` 存在歧義，需人工決策 |
| `B` | 官方來源無法取得或來源內容互相矛盾，暫時阻塞 |

不得使用單純關鍵字命中就宣告 `ED` 或 `ZD` 合格；完整性必須人工比對官方題目。

## 14. 批次執行策略

### 14.1 批次大小

每批應處理 10 至 20 個專案。若單題內容特別長、專案結構異常或出現來源問題，應縮小批次，不得為了湊足數量降低核對品質。

### 14.2 批次前

每批開始前必須：

1. 確認工作樹現況，保存既有使用者變更，不得覆蓋。
2. 重新掃描候選資料夾。
3. 排除已合格、已刪除與阻塞項目。
4. 確定每個目標的 `.csproj`、`Program.cs` 與 `Main`。
5. 記錄修改前檔案內容或 hash，供範圍驗證。
6. 取得並核對每題官方英文內容。

若工作樹已有與目標 summary 重疊的使用者變更，該專案必須暫停並請使用者決定，不得覆寫。

### 14.3 修改順序

每個專案依序執行：

1. 核對題號、slug 與英文題名。
2. 擷取完整官方英文題目。
3. 產生逐段對應的繁體中文翻譯。
4. 將兩種語言放入 `Main` summary。
5. 驗證 XML 與專案建置。
6. 驗證只有允許區塊改變。
7. 完成人工中英文對齊審查。

不得先大量產生翻譯、最後才一次驗證所有專案。

### 14.4 批次後

每批完成後必須輸出：

- 實際修改的專案與檔案清單。
- 每個專案修補的缺漏代碼。
- 官方英文及中文鏈結。
- build 結果。
- XML 文件診斷結果。
- 範圍差異檢查結果。
- 未完成或阻塞項目及原因。
- 本批未執行的 commit、push、PR 或 Issue 操作。

## 15. 單一專案驗證

### 15.1 Restore 與 build

必須使用明確 `.csproj`，不得在 repository 根目錄直接執行模糊的 `dotnet build` 或 `dotnet test`。

從該題的一級資料夾執行：

```bash
dotnet restore path/to/project.csproj
dotnet build path/to/project.csproj --no-restore --nologo -p:GenerateDocumentationFile=true
```

驗收要求：

- restore exit code 為 0。
- build exit code 為 0。
- 不得出現 XML 文件格式相關診斷，例如 CS1570、CS1584、CS1587。
- 不得修改 `.csproj` 來壓制警告。

若 baseline 在修改前即無法 restore 或 build，實作者不得修復專案；必須保存原始錯誤、確認不是 summary 造成，並將該題列為既有阻塞。

### 15.2 行為不變驗證

因 executable code 不得修改，主要證據是 source diff。若專案在修改前可非互動式執行，還必須比較修改前後：

- exit code。
- stdout。
- stderr。

三者必須完全一致。若 baseline 需要鍵盤輸入、網路、GUI 或其他不適合自動執行的外部條件，交付報告必須說明未執行原因；不得修改程式以迎合驗證環境。

### 15.3 差異範圍

必須執行：

```bash
git diff -- path/to/Program.cs
git diff --check -- path/to/Program.cs
```

人工確認每個 hunk 都位於目標 `Main` 的 `<summary>` 內。除 summary 外，檔案內容必須與修改前完全一致，包括：

- BOM 與 encoding。
- 換行風格。
- namespace 與 using。
- 所有 executable code。
- 其他 XML／一般註解。

## 16. 批次與全 repository 驗證

每批完成後必須：

1. 重新列出本批所有修改檔案。
2. 確認每個修改檔案都是目標 `Program.cs`。
3. 對每個目標執行明確 `.csproj` build。
4. 執行 repository 範圍的 `git diff --check`。
5. 重新執行六項靜態欄位掃描。
6. 對本批逐題完成人工內容完整性與翻譯對齊審查。

全部批次完成後，還必須重新掃描目前存在的所有候選資料夾。掃描結果必須：

- `ET = 0`
- `EL = 0`
- `ED = 0`
- `ZT = 0`
- `ZL = 0`
- `ZD = 0`
- `M = 0`

`A` 與 `B` 必須為 0，或已有使用者明確接受的例外紀錄。已刪除的 `leetcode_75` 不得出現在候選或例外清單。

## 17. 靜態掃描與人工審查的責任邊界

靜態掃描適合驗證：

- summary 是否直接附著於 `Main`。
- 雙語題名與鏈結是否存在。
- 英文與中文段落是否存在。
- XML 標籤是否成對。

靜態掃描不能單獨證明：

- 題目內容是否完整。
- 翻譯是否正確。
- 範例與 constraints 是否全部收錄。
- summary 是否混入非官方條件。

因此「掃描為零缺漏」只是必要條件，不是充分條件；仍必須保留逐題人工核對。

## 18. Git 與 GitHub 交付規則

本規格不授權任何 Git 或 GitHub 寫入。除非當次任務另有明確指示，實作者必須：

- 不建立或切換 branch。
- 不 stage。
- 不 commit。
- 不 push。
- 不建立 PR。
- 不在 Issue #90 留言。
- 不關閉 Issue #90。

若使用者另行授權更新 Issue，留言必須包含重新掃描日期、完成批次、剩餘數量與阻塞項目。只有 Definition of Done 全部成立後，才可建議關閉 Issue；實際關閉仍需明確授權。

## 19. Definition of Done

只有同時符合下列全部條件，Issue #90 的修補工作才算完成：

- [ ] 目前 checkout 的候選資料夾已重新掃描，不依賴舊快照。
- [ ] 已刪除的 `leetcode_75` 未被重建，也未列入目標。
- [ ] 每個適用專案的實際 `Main` 都有直接附著且合法的 XML `<summary>`。
- [ ] 每題均有正確英文題號、官方英文題名及有效英文鏈結。
- [ ] 每題均有完整官方英文敘述、全部範例、全部限制及原題補充段落。
- [ ] 每題均有繁體中文題名及有效中文鏈結。
- [ ] 每題均有與英文逐段對應的完整繁體中文翻譯。
- [ ] 中英文的數值、變數、範例、constraints 與邏輯量詞一致。
- [ ] 所有 XML 特殊字元與標籤合法。
- [ ] 所有可建置專案均以明確 `.csproj` restore/build 成功。
- [ ] 沒有因本工作新增 XML 文件診斷。
- [ ] 所有 diff 都只位於 `Main` 的 `<summary>`。
- [ ] 演算法、執行流程、輸出、README 與專案設定完全未修改。
- [ ] repository 範圍 `git diff --check` 通過。
- [ ] 重新掃描後 `ET/EL/ED/ZT/ZL/ZD/M` 全部為 0。
- [ ] 所有 `A/B` 阻塞均已解決或由使用者明確接受為例外。
- [ ] 交付報告完整列出修改、驗證、阻塞與未執行的發布操作。

## 20. 實作者檢查表

每題修改前：

- [ ] 已確認非測試 `.csproj`。
- [ ] 已確認實際 `Program.cs` 與 `Main`。
- [ ] 已確認工作樹沒有重疊的使用者變更。
- [ ] 已保存修改前內容或 hash。
- [ ] 已開啟並核對官方英文題目頁。
- [ ] 已確認中文題目鏈結與術語。

每題修改後：

- [ ] 英文題名、鏈結與完整內容齊全。
- [ ] 繁體中文題名、鏈結與完整翻譯齊全。
- [ ] 全部範例、Explanation、Constraints 與補充段落齊全。
- [ ] XML 字元已正確轉義。
- [ ] summary 直接附著於 `Main`。
- [ ] restore/build 已通過或已記錄 baseline 阻塞。
- [ ] diff 只在允許區塊。
- [ ] `git diff --check` 通過。
- [ ] 未修改任何禁止項目。

## 21. 審查者檢查表

審查者不得只看 build 或掃描數字，還必須抽取官方英文頁逐題核對：

- [ ] 題號、英文題名與 slug 正確。
- [ ] 英文題目沒有摘要化或漏段。
- [ ] 所有官方範例與 constraints 均存在。
- [ ] 中文為繁體且逐段完整對應。
- [ ] 數值、符號、變數與量詞沒有翻譯錯誤。
- [ ] XML 結構合法、可讀且未混入 HTML。
- [ ] 沒有解法說明、複雜度或個人評論混入題目區塊。
- [ ] diff 沒有越過 `Main` summary 邊界。
- [ ] 沒有修改 README、專案設定或 executable code。
- [ ] 阻塞與例外都有具體證據，不是為了跳過驗證。

## 22. 規格優先序

若後續單次任務與本規格衝突，依下列優先序處理：

1. 使用者在當次任務中的明確指示。
2. 目標路徑內適用的 `AGENTS.md`。
3. 本規格。
4. Issue #90 的歷史清單與描述。

任何較低優先序文件都不得擴張較高優先序已限制的修改範圍。
