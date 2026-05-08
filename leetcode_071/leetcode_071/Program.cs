namespace leetcode_071;

class Program
{
    /// <summary>
    /// 71. Simplify Path
    /// https://leetcode.com/problems/simplify-path/description/
    ///
    /// You are given an absolute path for a Unix-style file system, which always begins with a slash '/'.
    /// Your task is to transform this absolute path into its simplified canonical path.
    ///
    /// The rules of a Unix-style file system are as follows:
    /// A single period '.' represents the current directory.
    /// A double period '..' represents the previous/parent directory.
    /// Multiple consecutive slashes such as '//' and '///' are treated as a single slash '/'.
    /// Any sequence of periods that does not match the rules above should be treated as a valid directory or file name.
    /// For example, '...' and '....' are valid directory or file names.
    ///
    /// The simplified canonical path should follow these rules:
    /// The path must start with a single slash '/'.
    /// Directories within the path must be separated by exactly one slash '/'.
    /// The path must not end with a slash '/', unless it is the root directory.
    /// The path must not have any single or double periods ('.' and '..') used to denote current or parent directories.
    ///
    /// Return the simplified canonical path.
    ///
    /// 71. 簡化路徑
    /// https://leetcode.cn/problems/simplify-path/description/
    ///
    /// 給定一個 Unix 風格檔案系統的絕對路徑，該路徑一定以斜線 '/' 開頭。
    /// 你的任務是將這個絕對路徑轉換為簡化後的標準路徑。
    ///
    /// Unix 風格檔案系統的規則如下：
    /// 單一句點 '.' 表示目前目錄。
    /// 兩個句點 '..' 表示上一層／父目錄。
    /// 多個連續斜線，例如 '//' 和 '///'，會被視為單一斜線 '/'。
    /// 任何不符合上述規則的句點序列都應被視為有效的目錄或檔案名稱。
    /// 例如，'...' 和 '....' 都是有效的目錄或檔案名稱。
    ///
    /// 簡化後的標準路徑應遵守以下規則：
    /// 路徑必須以單一斜線 '/' 開頭。
    /// 路徑中的目錄之間必須剛好以一個斜線 '/' 分隔。
    /// 路徑不得以斜線 '/' 結尾，除非該路徑是根目錄。
    /// 路徑中不得包含用來表示目前目錄或父目錄的單一或雙重句點（'.' 和 '..'）。
    ///
    /// 回傳簡化後的標準路徑。
    /// </summary>
    /// <param name="args"></param>
    static void Main(string[] args)
    {
        Program solution = new Program();
        var testCases = new (string Path, string Expected)[]
        {
            ("/home/", "/home"),
            ("/home//foo/", "/home/foo"),
            ("/home/user/Documents/../Pictures", "/home/user/Pictures"),
            ("/../", "/"),
            ("/.../a/../b/c/../d/./", "/.../b/d")
        };

        foreach (var (path, expected) in testCases)
        {
            string actual = solution.SimplifyPath(path);

            Console.WriteLine($"Input: {path}");
            Console.WriteLine($"Output: {actual}");
            Console.WriteLine($"Expected: {expected}");
            Console.WriteLine($"Result: {(actual == expected ? "PASS" : "FAIL")}");
            Console.WriteLine();
        }
    }


    /// <summary>
    /// 將 Unix-style 絕對路徑簡化為標準路徑。
    /// 解題概念：使用堆疊保存仍有效的目錄名稱；遇到 "." 忽略，遇到 ".." 則回到上一層，
    /// 其他片段視為合法目錄名稱加入堆疊。
    /// 輸入條件：<paramref name="path"/> 為以 "/" 開頭的絕對路徑，可包含連續斜線、"."、".." 與一般目錄名稱。
    /// 輸出結果：回傳以單一 "/" 開頭、目錄間只用一個 "/" 分隔、不以多餘斜線結尾的標準路徑。
    /// </summary>
    /// <param name="path">待簡化的 Unix-style 絕對路徑。</param>
    /// <returns>簡化後的標準絕對路徑。</returns>
    public string SimplifyPath(string path)
    {
        Stack<string> directories = new Stack<string>();

        foreach (string segment in path.Split('/', StringSplitOptions.RemoveEmptyEntries))
        {
            if (segment == ".")
            {
                continue;
            }

            if (segment == "..")
            {
                // 只有在堆疊中已有有效目錄時才回上一層；根目錄不能再往上。
                if (directories.Count > 0)
                {
                    directories.Pop();
                }

                continue;
            }

            directories.Push(segment);
        }

        // Stack 會由最後加入的目錄開始列舉，反轉後即可組回根到目標的路徑。
        return directories.Count == 0 ? "/" : "/" + string.Join("/", directories.Reverse());
    }
}
