using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Threading;

namespace AEC.RTA.FFTW
{
    #region complex
    public struct Complex
    {
        // --------------SECTION: Private Data members ----------- //

        private Double m_real;
        private Double m_imaginary;

        // ---------------SECTION: Necessary Constants ----------- //

        private const Double LOG_10_INV = 0.43429448190325;


        // --------------SECTION: Public Properties -------------- //

        public Double Real
        {
            get
            {
                return m_real;
            }
            set
            {
                m_real = value;
            }
        }

        public Double Imaginary
        {
            get
            {
                return m_imaginary;
            }
            set
            {
                m_imaginary = value;
            }
        }

        public Double Magnitude
        {
            get
            {
                return Complex.Abs(this);
            }
        }

        public Double Phase
        {
            get
            {
                return Math.Atan2(m_imaginary, m_real);
            }
        }

        // --------------SECTION: Attributes -------------- //

        public static readonly Complex Zero = new Complex(0.0, 0.0);
        public static readonly Complex One = new Complex(1.0, 0.0);
        public static readonly Complex ImaginaryOne = new Complex(0.0, 1.0);

        // --------------SECTION: Constructors and factory methods -------------- //

        public Complex(Double real, Double imaginary)  /* Constructor to create a complex number with rectangular co-ordinates  */
        {
            this.m_real = real;
            this.m_imaginary = imaginary;
        }

        public static Complex FromPolarCoordinates(Double magnitude, Double phase) /* Factory method to take polar inputs and create a Complex object */
        {
            return new Complex((magnitude * Math.Cos(phase)), (magnitude * Math.Sin(phase)));
        }

        public static Complex Negate(Complex value)
        {
            return -value;
        }

        public static Complex Add(Complex left, Complex right)
        {
            return left + right;
        }

        public static Complex Subtract(Complex left, Complex right)
        {
            return left - right;
        }

        public static Complex Multiply(Complex left, Complex right)
        {
            return left * right;
        }

        public static Complex Divide(Complex dividend, Complex divisor)
        {
            return dividend / divisor;
        }

        // --------------SECTION: Arithmetic Operator(unary) Overloading -------------- //
        public static Complex operator -(Complex value)  /* Unary negation of a complex number */
        {

            return (new Complex((-value.m_real), (-value.m_imaginary)));
        }

        // --------------SECTION: Arithmetic Operator(binary) Overloading -------------- //       
        public static Complex operator +(Complex left, Complex right)
        {
            return (new Complex((left.m_real + right.m_real), (left.m_imaginary + right.m_imaginary)));

        }

        public static Complex operator -(Complex left, Complex right)
        {
            return (new Complex((left.m_real - right.m_real), (left.m_imaginary - right.m_imaginary)));
        }

        public static Complex operator *(Complex left, Complex right)
        {
            // Multiplication:  (a + bi)(c + di) = (ac -bd) + (bc + ad)i
            Double result_Realpart = (left.m_real * right.m_real) - (left.m_imaginary * right.m_imaginary);
            Double result_Imaginarypart = (left.m_imaginary * right.m_real) + (left.m_real * right.m_imaginary);
            return (new Complex(result_Realpart, result_Imaginarypart));
        }

        public static Complex operator /(Complex left, Complex right)
        {
            // Division : Smith's formula.
            double a = left.m_real;
            double b = left.m_imaginary;
            double c = right.m_real;
            double d = right.m_imaginary;

            if (Math.Abs(d) < Math.Abs(c))
            {
                double doc = d / c;
                return new Complex((a + b * doc) / (c + d * doc), (b - a * doc) / (c + d * doc));
            }
            else
            {
                double cod = c / d;
                return new Complex((b + a * cod) / (d + c * cod), (-a + b * cod) / (d + c * cod));
            }
        }


        // --------------SECTION: Other arithmetic operations  -------------- //

        public static Double Abs(Complex value)
        {

            if (Double.IsInfinity(value.m_real) || Double.IsInfinity(value.m_imaginary))
            {
                return double.PositiveInfinity;
            }

            // |value| == sqrt(a^2 + b^2)
            // sqrt(a^2 + b^2) == a/a * sqrt(a^2 + b^2) = a * sqrt(a^2/a^2 + b^2/a^2)
            // Using the above we can factor out the square of the larger component to dodge overflow.


            double c = Math.Abs(value.m_real);
            double d = Math.Abs(value.m_imaginary);

            if (c > d)
            {
                double r = d / c;
                return c * Math.Sqrt(1.0 + r * r);
            }
            else if (d == 0.0)
            {
                return c;  // c is either 0.0 or NaN
            }
            else
            {
                double r = c / d;
                return d * Math.Sqrt(1.0 + r * r);
            }
        }
        public static Complex Conjugate(Complex value)
        {
            // Conjugate of a Complex number: the conjugate of x+i*y is x-i*y 

            return (new Complex(value.m_real, (-value.m_imaginary)));

        }
        public static Complex Reciprocal(Complex value)
        {
            // Reciprocal of a Complex number : the reciprocal of x+i*y is 1/(x+i*y)
            if ((value.m_real == 0) && (value.m_imaginary == 0))
            {
                return Complex.Zero;
            }

            return Complex.One / value;
        }

        // --------------SECTION: Comparison Operator(binary) Overloading -------------- //

        public static bool operator ==(Complex left, Complex right)
        {
            return ((left.m_real == right.m_real) && (left.m_imaginary == right.m_imaginary));


        }
        public static bool operator !=(Complex left, Complex right)
        {
            return ((left.m_real != right.m_real) || (left.m_imaginary != right.m_imaginary));

        }

        // --------------SECTION: Comparison operations (methods implementing IEquatable<ComplexNumber>,IComparable<ComplexNumber>) -------------- //

        public override bool Equals(object obj)
        {
            if (!(obj is Complex)) return false;
            return this == ((Complex)obj);
        }
        public bool Equals(Complex value)
        {
            return ((this.m_real.Equals(value.m_real)) && (this.m_imaginary.Equals(value.m_imaginary)));

        }

        // --------------SECTION: Type-casting basic numeric data-types to ComplexNumber  -------------- //

        public static implicit operator Complex(Int16 value)
        {
            return (new Complex(value, 0.0));
        }
        public static implicit operator Complex(Int32 value)
        {
            return (new Complex(value, 0.0));
        }
        public static implicit operator Complex(Int64 value)
        {
            return (new Complex(value, 0.0));
        }
        public static implicit operator Complex(UInt16 value)
        {
            return (new Complex(value, 0.0));
        }
        public static implicit operator Complex(UInt32 value)
        {
            return (new Complex(value, 0.0));
        }
        public static implicit operator Complex(UInt64 value)
        {
            return (new Complex(value, 0.0));
        }
        public static implicit operator Complex(SByte value)
        {
            return (new Complex(value, 0.0));
        }
        public static implicit operator Complex(Byte value)
        {
            return (new Complex(value, 0.0));
        }
        public static implicit operator Complex(Single value)
        {
            return (new Complex(value, 0.0));
        }
        public static implicit operator Complex(Double value)
        {
            return (new Complex(value, 0.0));
        }        
        public static explicit operator Complex(Decimal value)
        {
            return (new Complex((Double)value, 0.0));
        }


        


        public override Int32 GetHashCode()
        {
            Int32 n1 = 99999997;
            Int32 hash_real = this.m_real.GetHashCode() % n1;
            Int32 hash_imaginary = this.m_imaginary.GetHashCode();
            Int32 final_hashcode = hash_real ^ hash_imaginary;
            return (final_hashcode);
        }



        // --------------SECTION: Trigonometric operations (methods implementing ITrigonometric)  -------------- //

        public static Complex Sin(Complex value)
        {
            double a = value.m_real;
            double b = value.m_imaginary;
            return new Complex(Math.Sin(a) * Math.Cosh(b), Math.Cos(a) * Math.Sinh(b));
        }
        
        

        public static Complex Cos(Complex value)
        {
            double a = value.m_real;
            double b = value.m_imaginary;
            return new Complex(Math.Cos(a) * Math.Cosh(b), -(Math.Sin(a) * Math.Sinh(b)));
        }
        
        
        public static Complex Tan(Complex value)
        {
            return (Sin(value) / Cos(value));
        }
        
        public static Complex Atan(Complex value) /* Arctan */
        {
            Complex Two = new Complex(2.0, 0.0);
            return (ImaginaryOne / Two) * (Log(One - ImaginaryOne * value) - Log(One + ImaginaryOne * value));
        }

        // --------------SECTION: Other numerical functions  -------------- //        

        public static Complex Log(Complex value) /* Log of the complex number value to the base of 'e' */
        {
            return (new Complex((Math.Log(Abs(value))), (Math.Atan2(value.m_imaginary, value.m_real))));

        }
        public static Complex Log(Complex value, Double baseValue) /* Log of the complex number to a the base of a double */
        {
            return (Log(value) / Log(baseValue));
        }
        public static Complex Log10(Complex value) /* Log to the base of 10 of the complex number */
        {

            Complex temp_log = Log(value);
            return (Scale(temp_log, (Double)LOG_10_INV));

        }
        public static Complex Exp(Complex value) /* The complex number raised to e */
        {
            Double temp_factor = Math.Exp(value.m_real);
            Double result_re = temp_factor * Math.Cos(value.m_imaginary);
            Double result_im = temp_factor * Math.Sin(value.m_imaginary);
            return (new Complex(result_re, result_im));
        }

        public static Complex Pow(Complex value, Complex power) /* A complex number raised to another complex number */
        {

            if (power == Complex.Zero)
            {
                return Complex.One;
            }

            if (value == Complex.Zero)
            {
                return Complex.Zero;
            }

            double a = value.m_real;
            double b = value.m_imaginary;
            double c = power.m_real;
            double d = power.m_imaginary;

            double rho = Complex.Abs(value);
            double theta = Math.Atan2(b, a);
            double newRho = c * theta + d * Math.Log(rho);

            double t = Math.Pow(rho, c) * Math.Pow(Math.E, -d * theta);

            return new Complex(t * Math.Cos(newRho), t * Math.Sin(newRho));
        }

        public static Complex Pow(Complex value, Double power) // A complex number raised to a real number 
        {
            return Pow(value, new Complex(power, 0));
        }



        //--------------- SECTION: Private member functions for internal use -----------------------------------//

        private static Complex Scale(Complex value, Double factor)
        {

            Double result_re = factor * value.m_real;
            Double result_im = factor * value.m_imaginary;
            return (new Complex(result_re, result_im));
        }

    }
    #endregion

    #region Single Precision
    public class fftwf_array
    {
        private IntPtr handle;
        public IntPtr Handle
        { get { return handle; } }

        // The logical length of the array (# of complex numbers, not elements)
        private int length;
        public int Length
        { get { return length; } }
        /// <summary>
        /// Creates a new array of complex numbers
        /// </summary>
        /// <param name="length">Logical length of the array</param>
        public fftwf_array(int length)
        {
            this.length = length;
            this.handle = fftwf.malloc(this.length * sizeof(float));
        }

        /// <summary>
        /// Creates an FFTW-compatible array from array of floats, initializes to single precision only
        /// </summary>
        /// <param name="data">Array of floats, alternating real and imaginary</param>
        public fftwf_array(float[] data)
        {
            this.length = data.Length;
            this.handle = fftwf.malloc(this.length * sizeof(float));

            this.SetData(data);
        }

        public void SetData(float[] data)
        {
            if (data.Length != this.length)
            {
                throw new ArgumentException("Array length mismatch!");
            }

            Marshal.Copy(data, 0, handle, this.length);
        }

        public void SetData(double[] data)
        {
            if (data.Length != this.length)
            {
                throw new ArgumentException("Array length mismatch!");
            }

            float[] temp = new float[data.Length];
            for (int i = 0; i < temp.Length; ++i)
            {
                temp[i] = (float)data[i];
            }

            Marshal.Copy(data, 0, handle, this.length);
        }

        ~fftwf_array()
        {
            fftwf.free(handle);
        }

    }
    /// <summary>
    /// To simplify FFTW memory management
    /// </summary>
    /// 
    public class fftwf_complexarray
    {
        private IntPtr handle;
        public IntPtr Handle
        { get { return handle; } }

        // The logical length of the array (# of complex numbers, not elements)
        private int length;
        public int Length
        { get { return length; } }

        /// <summary>
        /// Creates a new array of complex numbers
        /// </summary>
        /// <param name="length">Logical length of the array</param>
        public fftwf_complexarray(int length)
        {
            this.length = length;
            this.handle = fftwf.malloc(this.length * sizeof(float) * 2);
        }

        /// <summary>
        /// Creates an FFTW-compatible array from array of floats, initializes to single precision only
        /// </summary>
        /// <param name="data">Array of floats, alternating real and imaginary</param>
        public fftwf_complexarray(float[] data)
        {
            this.length = data.Length / 2;
            this.handle = fftwf.malloc(this.length * sizeof(float) * 2);

            this.SetData(data);
        }

        /// <summary>
        /// Set the data to an array of complex numbers (real + imaginary floats following eachother)
        /// </summary>
        public void SetData(float[] data)
        {
            if (data.Length / 2 != this.length)
                throw new ArgumentException("Array length mismatch!");

            Marshal.Copy(data, 0, handle, this.length * 2);
        }

        /// <summary>
        /// Set the data to zeros
        /// </summary>
        public void SetZeroData()
        {
            float[] data_in = new float[this.Length * 2];
            // C# arrays always initialized to 0
            Marshal.Copy(data_in, 0, handle, this.length * 2);
        }

        /// <summary>
        /// Get the real elements out (imaginary parts are discarded)
        /// </summary>
        public float[] GetData_Real()
        {
            float[] dataf = new float[length * 2];
            Marshal.Copy(handle, dataf, 0, length * 2);
            float[] data = new float[length];

            for (int i = 0; i < length; i++)
            {
                data[i] = dataf[2 * i];
            }

            return data;
        }

        /// <summary>
        /// Get the full array of floats out (alternating real and imaginary)
        /// </summary>
        public float[] GetData_Float()
        {
            float[] dataf = new float[length * 2];
            Marshal.Copy(handle, dataf, 0, length * 2);

            return dataf;
        }

        /// <summary>
        /// Get the data out as Complex numbers
        /// </summary>
        public Complex[] GetData_Complex()
        {
            float[] dataf = new float[length * 2];
            Marshal.Copy(handle, dataf, 0, length * 2);
            Complex[] data = new Complex[length];

            for (int i = 0; i < length; i++)
            {
                data[i] = new Complex(dataf[2 * i], dataf[2 * i + 1]);
            }

            return data;
        }

        ~fftwf_complexarray()
        {
            fftwf.free(handle);
        }
    }

    /// <summary>
    /// Creates, stores, and destroys fftw plans
    /// </summary>
    public class fftwf_plan
    {
        static Mutex FFTW_Lock = new Mutex();

        protected IntPtr handle;
        public IntPtr Handle
        { get { return handle; } }

        public void Execute()
        {
            fftwf.execute(handle);
        }

        ~fftwf_plan()
        {
            fftwf.cleanup();
            fftwf.destroy_plan(handle);
        }

        #region Plan Creation
        //Complex<->Complex transforms
        public static fftwf_plan dft_1d(int n, fftwf_complexarray input, fftwf_complexarray output, fftw_direction direction, fftw_flags flags)
        {
            FFTW_Lock.WaitOne();
            fftwf_plan p = new fftwf_plan();
            p.handle = fftwf.dft_1d(n, input.Handle, output.Handle, direction, flags);
            FFTW_Lock.ReleaseMutex();

            return p;
        }

        //Real->Complex transforms
        public static fftwf_plan dft_r2c_1d(int n, fftwf_array input, fftwf_complexarray output, fftw_flags flags)
        {
            FFTW_Lock.WaitOne();
            fftwf_plan p = new fftwf_plan();
            p.handle = fftwf.dft_r2c_1d(n, input.Handle, output.Handle, flags);
            FFTW_Lock.ReleaseMutex();

            return p;
        }
        #endregion
    }
    #endregion

    #region Double Precision
    public class fftw_array
    {
        private IntPtr handle;
        public IntPtr Handle
        { get { return handle; } }

        // The logical length of the array (# of complex numbers, not elements)
        private int length;
        public int Length
        { get { return length; } }
        /// <summary>
        /// Creates a new array of complex numbers
        /// </summary>
        /// <param name="length">Logical length of the array</param>
        public fftw_array(int length)
        {
            this.length = length;
            this.handle = fftw.malloc(this.length * sizeof(double));
        }

        /// <summary>
        /// Creates an FFTW-compatible array from array of floats, initializes to single precision only
        /// </summary>
        /// <param name="data">Array of floats, alternating real and imaginary</param>
        public fftw_array(double[] data)
        {
            this.length = data.Length;
            this.handle = fftw.malloc(this.length * sizeof(float));

            this.SetData(data);
        }

        public void SetData(double[] data)
        {
            if (data.Length != this.length)
            {
                throw new ArgumentException("Array length mismatch!");
            }

            Marshal.Copy(data, 0, handle, this.length);
        }

        public void SetData(float[] data)
        {
            if (data.Length != this.length)
            {
                throw new ArgumentException("Array length mismatch!");
            }

            double[] temp = new double[data.Length];
            for (int i = 0; i < temp.Length; ++i)
            {
                temp[i] = data[i];
            }

            Marshal.Copy(temp, 0, handle, this.length);
        }

        ~fftw_array()
        {
            fftw.free(handle);
        }
    }
    /// <summary>
    /// To simplify FFTW memory management
    /// </summary>
    /// 
    public class fftw_complexarray
    {
        private IntPtr handle;
        public IntPtr Handle
        { get { return handle; } }

        // The logical length of the array (# of complex numbers, not elements)
        private int length;
        public int Length
        { get { return length; } }

        /// <summary>
        /// Creates a new array of complex numbers
        /// </summary>
        /// <param name="length">Logical length of the array</param>
        public fftw_complexarray(int length)
        {
            this.length = length;
            this.handle = fftw.malloc(this.length * sizeof(double) * 2);
        }

        /// <summary>
        /// Creates an FFTW-compatible array from array of floats, initializes to single precision only
        /// </summary>
        /// <param name="data">Array of floats, alternating real and imaginary</param>
        public fftw_complexarray(double[] data)
        {
            this.length = data.Length / 2;
            this.handle = fftw.malloc(this.length * sizeof(double) * 2);

            this.SetData(data);
        }

        /// <summary>
        /// Set the data to an array of complex numbers (real + imaginary floats following eachother)
        /// </summary>
        public void SetData(double[] data)
        {
            if (data.Length / 2 != this.length)
                throw new ArgumentException("Array length mismatch!");

            Marshal.Copy(data, 0, handle, this.length * 2);
        }

        public void SetData_Real(double[] data)
        {
            if (data.Length != this.length)
                throw new ArgumentException("Array length mismatch!");

            double[] temp = new double[this.length * 2];
            for (int i = 0; i < data.Length; ++i)
            {
                temp[2 * i] = data[i];
            }

            Marshal.Copy(temp, 0, handle, this.length * 2);
        }

        public void SetData_Real(float[] data)
        {
            if (data.Length != this.length)
                throw new ArgumentException("Array length mismatch!");

            double[] temp = new double[this.length * 2];
            for (int i = 0; i < data.Length; ++i)
            {
                temp[2 * i] = data[i];
            }

            Marshal.Copy(temp, 0, handle, this.length * 2);
        }

        public void SetData(Complex[] data)
        {
            if (data.Length != this.length)
                throw new ArgumentException("Array length mismatch!");

            double[] temp = new double[this.length * 2];
            for (int i = 0; i < data.Length; ++i)
            {
                temp[2 * i + 0] = data[i].Real;
                temp[2 * i + 1] = data[i].Imaginary;
            }

            Marshal.Copy(temp, 0, handle, this.length * 2);
        }

        /// <summary>
        /// Set the data to zeros
        /// </summary>
        public void SetZeroData()
        {
            double[] data_in = new double[this.Length * 2];
            // C# arrays always initialized to 0
            Marshal.Copy(data_in, 0, handle, this.length * 2);
        }

        /// <summary>
        /// Get the real elements out (imaginary parts are discarded)
        /// </summary>
        public double[] GetData_Real()
        {
            double[] dataf = new double[length * 2];
            Marshal.Copy(handle, dataf, 0, length * 2);
            double[] data = new double[length];

            for (int i = 0; i < length; i++)
            {
                data[i] = dataf[2 * i];
            }

            return data;
        }

        public void GetData_Real(double[] data)
        {
            double[] dataf = new double[length * 2];
            Marshal.Copy(handle, dataf, 0, length * 2);
            for (int i = 0; i < length; i++)
            {
                data[i] = dataf[2 * i];
            }
        }

        /// <summary>
        /// Get the full array of floats out (alternating real and imaginary)
        /// </summary>
        public double[] GetData_Float()
        {
            double[] dataf = new double[length * 2];
            Marshal.Copy(handle, dataf, 0, length * 2);

            return dataf;
        }

        /// <summary>
        /// Get the data out as Complex numbers
        /// </summary>
        public Complex[] GetData_Complex()
        {
            double[] dataf = new double[length * 2];
            Marshal.Copy(handle, dataf, 0, length * 2);
            Complex[] data = new Complex[length];

            for (int i = 0; i < length; i++)
            {
                data[i] = new Complex(dataf[2 * i], dataf[2 * i + 1]);
            }

            return data;
        }

        public void GetComplexs(Complex[] data)
        {
            double[] dataf = new double[length * 2];
            Marshal.Copy(handle, dataf, 0, length * 2);
            for (int i = 0; i < length; i++)
            {
                data[i] = new Complex(dataf[2 * i], dataf[2 * i + 1]);
            }
        }
        

        ~fftw_complexarray()
        {
            fftw.free(handle);
        }
    }

    /// <summary>
    /// Creates, stores, and destroys fftw plans
    /// </summary>
    public class fftw_plan
    {
        static Mutex FFTW_Lock = new Mutex();

        protected IntPtr handle;
        public IntPtr Handle
        { get { return handle; } }

        public void Execute()
        {
            fftw.execute(handle);
        }

        ~fftw_plan()
        {
            //fftw.cleanup();
            fftw.destroy_plan(handle);
        }

        #region Plan Creation
        //Complex<->Complex transforms
        public static fftw_plan dft_1d(int n, fftw_complexarray input, fftw_complexarray output, fftw_direction direction, fftw_flags flags)
        {
            FFTW_Lock.WaitOne();
            fftw_plan p = new fftw_plan();
            p.handle = fftw.dft_1d(n, input.Handle, output.Handle, direction, flags);
            FFTW_Lock.ReleaseMutex();

            return p;
        }

        //Real->Complex transforms
        public static fftw_plan dft_r2c_1d(int n, fftw_array input, fftw_complexarray output, fftw_flags flags)
        {
            FFTW_Lock.WaitOne();
            fftw_plan p = new fftw_plan();
            p.handle = fftw.dft_r2c_1d(n, input.Handle, output.Handle, flags);
            //Console.WriteLine("dft_1d plan:{0}", p.handle);
            FFTW_Lock.ReleaseMutex();

            return p;
        }
        #endregion
    }
    #endregion
}
