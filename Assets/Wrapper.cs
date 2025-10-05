using System.Runtime.InteropServices;

public static class NativeSorter
{
    [DllImport("SortPlugin")] 
    public static extern void SortArray(int[] array, int size);
}