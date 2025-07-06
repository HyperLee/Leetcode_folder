# proj initialization

## 專案初始化步驟

### 1. 產生 `.gitignore`

```bash
dotnet new gitignore
```

---

### 2. 產生 `.editorconfig`

```bash
dotnet new editorconfig
```

---

### 3. 修改 `.editorconfig` 以強制程式碼風格

請將以下 `.editorconfig` 設定合併到專案根目錄的 `.editorconfig` 檔案：

```txt
[*]
insert_final_newline = true

# 設定預設字元集為 UTF-8-bom
[*.{cs, csx, vb, vbx, txt, htnl, css, js, json, yml, yaml, xml, config, ini, sh,
ps1, psm1, ps1mxl, csproj, sln, gitignore, gitattributes,
editorconfig, md, markdown, txt, asciidoc, adoc, asc, asciidoc, txt, ipynb, py}]
charset = utf-8-bom
```

---

Let's do this step by step to ensure the project initialization is complete.