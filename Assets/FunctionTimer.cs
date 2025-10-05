using System;
using System.Diagnostics;
using System.Threading.Tasks;
using UnityEngine;

public class FunctionTimer : MonoBehaviour
{
    private int[] array;

    async void Start()
    {
        
        array = new int[1_000_000];
        System.Random rnd = new System.Random();

        for (int i = 0; i < array.Length; i++)
            array[i] = rnd.Next(1_000_000, 1_000_000_000);

        await CompareSortsAsync();
    }

    private async Task CompareSortsAsync()
    {
        await Task.Run(() =>
        {
            int[] managedArray = (int[])array.Clone();
            int[] nativeArray = (int[])array.Clone();

            Stopwatch stopwatch = new Stopwatch();

            
            stopwatch.Start();
            ManagedSorter.SortArray(managedArray);
            stopwatch.Stop();
            double managedTime = stopwatch.Elapsed.TotalMilliseconds;

           
            stopwatch.Restart();
            NativeSorter.SortArray(nativeArray, nativeArray.Length);
            stopwatch.Stop();
            double nativeTime = stopwatch.Elapsed.TotalMilliseconds;

            
            UnityEngine.Debug.Log($"Managed C# Sort took {managedTime:F3} ms");
            UnityEngine.Debug.Log($"Native C++ Sort took {nativeTime:F3} ms");

            double diff = ((managedTime - nativeTime) / managedTime) * 100.0;
            UnityEngine.Debug.Log($"C++ was {diff:F2}% faster.");
        });
    }
}