using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace AEC.RTA.FFTW
{
    // Various Flags used by FFTW
    #region Enums
    /// <summary>
    /// FFTW planner flags
    /// </summary>
    [Flags]
    public enum fftw_flags : uint
    {
        /// <summary>
        /// Tells FFTW to find an optimized plan by actually computing several FFTs and measuring their execution time. 
        /// Depending on your machine, this can take some time (often a few seconds). Default (0x0). 
        /// </summary>
        Measure = 0,
        /// <summary>
        /// Specifies that an out-of-place transform is allowed to overwrite its 
        /// input array with arbitrary data; this can sometimes allow more efficient algorithms to be employed.
        /// </summary>
        DestroyInput = 1,
        /// <summary>
        /// Rarely used. Specifies that the algorithm may not impose any unusual alignment requirements on the input/output 
        /// arrays (i.e. no SIMD). This flag is normally not necessary, since the planner automatically detects 
        /// misaligned arrays. The only use for this flag is if you want to use the guru interface to execute a given 
        /// plan on a different array that may not be aligned like the original. 
        /// </summary>
        Unaligned = 2,
        /// <summary>
        /// Not used.
        /// </summary>
        ConserveMemory = 4,
        /// <summary>
        /// Like Patient, but considers an even wider range of algorithms, including many that we think are 
        /// unlikely to be fast, to produce the most optimal plan but with a substantially increased planning time. 
        /// </summary>
        Exhaustive = 8,
        /// <summary>
        /// Specifies that an out-of-place transform must not change its input array. 
        /// </summary>
        /// <remarks>
        /// This is ordinarily the default, 
        /// except for c2r and hc2r (i.e. complex-to-real) transforms for which DestroyInput is the default. 
        /// In the latter cases, passing PreserveInput will attempt to use algorithms that do not destroy the 
        /// input, at the expense of worse performance; for multi-dimensional c2r transforms, however, no 
        /// input-preserving algorithms are implemented and the planner will return null if one is requested.
        /// </remarks>
        PreserveInput = 16,
        /// <summary>
        /// Like Measure, but considers a wider range of algorithms and often produces a 搈ore optimal?plan 
        /// (especially for large transforms), but at the expense of several times longer planning time 
        /// (especially for large transforms).
        /// </summary>
        Patient = 32,
        /// <summary>
        /// Specifies that, instead of actual measurements of different algorithms, a simple heuristic is 
        /// used to pick a (probably sub-optimal) plan quickly. With this flag, the input/output arrays 
        /// are not overwritten during planning. 
        /// </summary>
        Estimate = 64
    }

    /// <summary>
    /// Defines direction of operation
    /// </summary>
    public enum fftw_direction : int
    {
        /// <summary>
        /// Computes a regular DFT
        /// </summary>
        Forward = -1,
        /// <summary>
        /// Computes the inverse DFT
        /// </summary>
        Backward = 1
    }

    /// <summary>
    /// Kinds of real-to-real transforms
    /// </summary>
    public enum fftw_kind : uint
    {
        R2HC = 0,
        HC2R = 1,
        DHT = 2,
        REDFT00 = 3,
        REDFT01 = 4,
        REDFT10 = 5,
        REDFT11 = 6,
        RODFT00 = 7,
        RODFT01 = 8,
        RODFT10 = 9,
        RODFT11 = 10
    }
    #endregion

    public class fftwf
    {
        /// <summary>
        /// Allocates FFTW-optimized unmanaged memory
        /// </summary>
        /// <param name="length">Amount to allocate, in bytes</param>
        /// <returns>Pointer to allocated memory</returns>
        [DllImport(@"libfftw3f-3.dll",
             EntryPoint = "fftwf_malloc",
             ExactSpelling = true,
             CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr malloc(int length);


        /// <summary>
        /// Deallocates memory allocated by FFTW malloc
        /// </summary>
        /// <param name="mem">Pointer to memory to release</param>
        [DllImport("libfftw3f-3.dll",
             EntryPoint = "fftwf_free",
             ExactSpelling = true,
             CallingConvention = CallingConvention.Cdecl)]
        public static extern void free(IntPtr mem);


        /// <summary>
        /// Creates a plan for a 1-dimensional complex-to-complex DFT
        /// </summary>
        /// <param name="n">The logical size of the transform</param>
        /// <param name="direction">Specifies the direction of the transform</param>
        /// <param name="input">Pointer to an array of 8-byte complex numbers</param>
        /// <param name="output">Pointer to an array of 8-byte complex numbers</param>
        /// <param name="flags">Flags that specify the behavior of the planner</param>
        [DllImport("libfftw3f-3.dll",
             EntryPoint = "fftwf_plan_dft_1d",
             ExactSpelling = true,
             CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr dft_1d(int n, IntPtr input, IntPtr output,
            fftw_direction direction, fftw_flags flags);

        

        /// <summary>
        /// Creates a plan for a 1-dimensional real-to-complex DFT
        /// </summary>
        /// <param name="n">Number of REAL (input) elements in the transform</param>
        /// <param name="input">Pointer to an array of 4-byte real numbers</param>
        /// <param name="output">Pointer to an array of 8-byte complex numbers</param>
        /// <param name="flags">Flags that specify the behavior of the planner</param>
        [DllImport("libfftw3f-3.dll",
             EntryPoint = "fftwf_plan_dft_r2c_1d",
             ExactSpelling = true,
             CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr dft_r2c_1d(int n, IntPtr input, IntPtr output, fftw_flags flags);



        /// <summary>
        /// Deallocates an FFTW plan and all associated resources
        /// </summary>
        /// <param name="plan">Pointer to the plan to release</param>
        [DllImport("libfftw3f-3.dll",
             EntryPoint = "fftwf_destroy_plan",
             ExactSpelling = true,
             CallingConvention = CallingConvention.Cdecl)]
        public static extern void destroy_plan(IntPtr plan);

        /// <summary>
        /// Clears all memory used by FFTW, resets it to initial state. Does not replace destroy_plan and free
        /// </summary>
        /// <remarks>After calling fftw_cleanup, all existing plans become undefined, and you should not 
        /// attempt to execute them nor to destroy them. You can however create and execute/destroy new plans, 
        /// in which case FFTW starts accumulating wisdom information again. 
        /// fftw_cleanup does not deallocate your plans; you should still call fftw_destroy_plan for this purpose.</remarks>
        [DllImport("libfftw3f-3.dll",
             EntryPoint = "fftwf_cleanup",
             ExactSpelling = true,
             CallingConvention = CallingConvention.Cdecl)]
        public static extern void cleanup();

        /// <summary>
        /// Executes an FFTW plan, provided that the input and output arrays still exist
        /// </summary>
        /// <param name="plan">Pointer to the plan to execute</param>
        /// <remarks>execute (and equivalents) is the only function in FFTW guaranteed to be thread-safe.</remarks>
        [DllImport("libfftw3f-3.dll",
             EntryPoint = "fftwf_execute",
             ExactSpelling = true,
             CallingConvention = CallingConvention.Cdecl)]
        public static extern void execute(IntPtr plan);
    }

    public class fftw
    {
        /// <summary>
        /// Allocates FFTW-optimized unmanaged memory
        /// </summary>
        /// <param name="length">Amount to allocate, in bytes</param>
        /// <returns>Pointer to allocated memory</returns>
        [DllImport(@"libfftw3-3.dll",
             EntryPoint = "fftw_malloc",
             ExactSpelling = true,
             CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr malloc(int length);

        /// <summary>
        /// Deallocates memory allocated by FFTW malloc
        /// </summary>
        /// <param name="mem">Pointer to memory to release</param>
        [DllImport("libfftw3-3.dll",
             EntryPoint = "fftw_free",
             ExactSpelling = true,
             CallingConvention = CallingConvention.Cdecl)]
        public static extern void free(IntPtr mem);
        
        /// <summary>
        /// Creates a plan for a 1-dimensional complex-to-complex DFT
        /// </summary>
        /// <param name="n">The logical size of the transform</param>
        /// <param name="direction">Specifies the direction of the transform</param>
        /// <param name="input">Pointer to an array of 16-byte complex numbers</param>
        /// <param name="output">Pointer to an array of 16-byte complex numbers</param>
        /// <param name="flags">Flags that specify the behavior of the planner</param>
        [DllImport("libfftw3-3.dll",
             EntryPoint = "fftw_plan_dft_1d",
             ExactSpelling = true,
             CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr dft_1d(int n, IntPtr input, IntPtr output,
            fftw_direction direction, fftw_flags flags);

        /// <summary>
        /// Creates a plan for a 1-dimensional real-to-complex DFT
        /// </summary>
        /// <param name="n">Number of REAL (input) elements in the transform</param>
        /// <param name="input">Pointer to an array of 8-byte real numbers</param>
        /// <param name="output">Pointer to an array of 16-byte complex numbers</param>
        /// <param name="flags">Flags that specify the behavior of the planner</param>
        [DllImport("libfftw3-3.dll",
             EntryPoint = "fftw_plan_dft_r2c_1d",
             ExactSpelling = true,
             CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr dft_r2c_1d(int n, IntPtr input, IntPtr output, fftw_flags flags);/// <summary>

        /// <summary>
        /// Deallocates an FFTW plan and all associated resources
        /// </summary>
        /// <param name="plan">Pointer to the plan to release</param>
        [DllImport("libfftw3-3.dll",
             EntryPoint = "fftw_destroy_plan",
             ExactSpelling = true,
             CallingConvention = CallingConvention.Cdecl)]
        public static extern void destroy_plan(IntPtr plan);

        /// <summary>
        /// Clears all memory used by FFTW, resets it to initial state. Does not replace destroy_plan and free
        /// </summary>
        /// <remarks>After calling fftw_cleanup, all existing plans become undefined, and you should not 
        /// attempt to execute them nor to destroy them. You can however create and execute/destroy new plans, 
        /// in which case FFTW starts accumulating wisdom information again. 
        /// fftw_cleanup does not deallocate your plans; you should still call fftw_destroy_plan for this purpose.</remarks>
        [DllImport("libfftw3-3.dll",
             EntryPoint = "fftw_cleanup",
             ExactSpelling = true,
             CallingConvention = CallingConvention.Cdecl)]
        public static extern void cleanup();

        /// <summary>
        /// Executes an FFTW plan, provided that the input and output arrays still exist
        /// </summary>
        /// <param name="plan">Pointer to the plan to execute</param>
        /// <remarks>execute (and equivalents) is the only function in FFTW guaranteed to be thread-safe.</remarks>
        [DllImport("libfftw3-3.dll",
             EntryPoint = "fftw_execute",
             ExactSpelling = true,
             CallingConvention = CallingConvention.Cdecl)]
        public static extern void execute(IntPtr plan);
    }
}
