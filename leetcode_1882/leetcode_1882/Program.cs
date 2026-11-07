namespace leetcode_1882;

class Program
{
    /// <summary>
    /// 1882. Process Tasks Using Servers
    /// https://leetcode.com/problems/process-tasks-using-servers/description/
    /// 1882. 使用伺服器處理任務
    /// https://leetcode.cn/problems/process-tasks-using-servers/description/
    /// English:
    /// 
    /// You are given two 0-indexed integer arrays servers and tasks of lengths n and m respectively.
    /// servers[i] is the weight of the ith server, and tasks[j] is the time needed to process the jth task in seconds.
    /// 
    /// 
    /// Tasks are assigned to the servers using a task queue. Initially, all servers are free, and the queue is empty.
    /// 
    /// 
    /// At second j, the jth task is inserted into the queue (starting with the 0th task being inserted at second 0).
    /// As long as there are free servers and the queue is not empty, the task in the front of the queue will be
    /// assigned to a free server with the smallest weight, and in case of a tie, it is assigned to a free server
    /// with the smallest index.
    /// 
    /// 
    /// If there are no free servers and the queue is not empty, we wait until a server becomes free and immediately
    /// assign the next task. If multiple servers become free at the same time, then multiple tasks from the queue
    /// will be assigned in order of insertion following the weight and index priorities above.
    /// 
    /// 
    /// A server that is assigned task j at second t will be free again at second t + tasks[j].
    /// 
    /// 
    /// Build an array ans of length m, where ans[j] is the index of the server the jth task will be assigned to.
    /// 
    /// Return the array ans.
    /// 繁體中文：
    /// 
    /// 給定兩個索引從 0 開始的整數陣列 servers 與 tasks，長度分別為 n 與 m。
    /// servers[i] 表示第 i 台伺服器的權重，tasks[j] 表示處理第 j 個任務所需的時間，單位為秒。
    /// 
    /// 
    /// 任務透過任務佇列分配給伺服器。最初，所有伺服器皆為閒置狀態，且佇列為空。
    /// 
    /// 
    /// 在第 j 秒，第 j 個任務會被加入佇列（第 0 個任務於第 0 秒加入）。
    /// 只要存在閒置伺服器且佇列不為空，就會將佇列最前端的任務分配給權重最小的閒置伺服器；
    /// 若權重相同，則分配給索引最小的閒置伺服器。
    /// 
    /// 
    /// 若沒有閒置伺服器且佇列不為空，就等待直到有伺服器恢復閒置，並立即分配下一個任務。
    /// 若多台伺服器同時恢復閒置，則依照任務加入佇列的順序，
    /// 遵循上述權重與索引的優先順序，分配佇列中的多個任務。
    /// 
    /// 
    /// 若某台伺服器於第 t 秒被分配第 j 個任務，則該伺服器會於第 t + tasks[j] 秒恢復閒置。
    /// 
    /// 
    /// 建立長度為 m 的陣列 ans，其中 ans[j] 表示第 j 個任務被分配到的伺服器索引。
    /// 
    /// 回傳陣列 ans。
    /// </summary>
    /// <param name="args">命令列引數。</param>
    static void Main(string[] args)
    {
        Console.WriteLine("Hello, World!");
    }
}
