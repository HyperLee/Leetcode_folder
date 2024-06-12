namespace leetcode_075
{
    internal class Program
    {
        /// <summary>
        /// 75. Sort Colors
        /// https://leetcode.com/problems/sort-colors/?envType=daily-question&envId=2024-06-12
        /// 75. 颜色分类
        /// https://leetcode.cn/problems/sort-colors/description/
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            int[] input = { 2, 0, 2, 1, 1, 0 };
            SortColors(input);
            SortColors2(input);
            CountingSortAlgorithm(input);
            Console.ReadKey();

        }



        /// <summary>
        /// 
        /// </summary>
        /// <param name="nums"></param>
        public static void SortColors(int[] nums)
        {
            BubbleSortAlgorithm(nums);

            PrintArray(nums);
            Console.WriteLine();
        }


        /// <summary>
        /// bubble sort
        /// </summary>
        /// <param name="arr"></param>
        public static void BubbleSortAlgorithm(int[] arr)
        {
            int n = arr.Length;
            for (int i = 0; i < n - 1; i++)
            {
                for (int j = 0; j < n - i - 1; j++)
                {
                    if (arr[j] > arr[j + 1])
                    {
                        // swap arr[j] and arr[j+1]
                        int temp = arr[j];
                        arr[j] = arr[j + 1];
                        arr[j + 1] = temp;
                    }
                }
            }
        }



        /// <summary>
        /// print
        /// </summary>
        /// <param name="arr"></param>
        public static void PrintArray(int[] arr)
        {
            foreach (int num in arr)
            {
                Console.Write(num + " ");
            }
        }


        /// <summary>
        /// https://leetcode.cn/problems/sort-colors/solutions/1522053/by-stormsunshine-bhqo/
        /// 计数排序
        /// </summary>
        /// <param name="nums"></param>
        public static void SortColors2(int[] nums)
        {
            // 先計數, 每個element 數量
            int[] counts = new int[3];
            foreach(int num in nums)
            {
                counts[num]++;
            }

            // 依據結果, 再去排序
            int n = nums.Length;
            for(int i = 0, j = 0; i < n; i++)
            {
                while (counts[j] == 0)
                {
                    j++;
                }

                nums[i] = j;
                counts[j]--;
            }

            foreach (int num in nums)
            {
                Console.Write(num + " ");
            }
            Console.WriteLine();

        }


        /// <summary>
        /// 計數排序
        /// </summary>
        /// <param name="arr"></param>
        public static void CountingSortAlgorithm(int[] arr)
        {
            int n = arr.Length;
            int[] output = new int[n];

            // Find the maximum element of the array
            int max = arr[0];
            for (int i = 1; i < n; i++)
            {
                if (arr[i] > max)
                    max = arr[i];
            }

            // Create a count array to store count of individual elements
            int[] count = new int[max + 1];

            // Initialize count array with all zeros
            for (int i = 0; i <= max; ++i)
            {
                count[i] = 0;
            }

            // Store count of each character
            for (int i = 0; i < n; ++i)
            {
                ++count[arr[i]];
            }

            // Change count[i] so that count[i] now contains actual position of this element in output array
            // 修改計數數組
            for (int i = 1; i <= max; ++i)
            {
                count[i] += count[i - 1];
            }

            // Build the output array
            // 構建排序後的數組
            for (int i = n - 1; i >= 0; i--)
            {
                output[count[arr[i]] - 1] = arr[i];
                --count[arr[i]];
            }

            // Copy the output array to arr, so that arr now contains sorted characters
            // 複製回原始數組
            for (int i = 0; i < n; ++i)
            {
                arr[i] = output[i];
            }


            // print 
            foreach (int num in arr)
            {
                Console.Write(num + " ");
            }
            Console.WriteLine();
        }

    }
}
