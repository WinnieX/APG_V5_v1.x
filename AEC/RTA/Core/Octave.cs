using System;
using System.Collections.Generic;
using System.Text;

namespace AEC.RTA.Core
{
    public class OctaveBand
    {
        public float middle;
        public float lower;
        public float upper;
        public float nominal;
        //public double left_spl = 0;
        //public double right_spl = 0;

        public OctaveBand(float mid, float low, float up)
        {
            this.middle = mid;
            this.lower = low;
            this.upper = up;
            this.nominal = mid;
        }

        public float bandWidth
        {
            get
            {
                return upper - lower;
            }
        }
    }

    public class OctaveEntity
    {
        public const float Freq1K = 1000;
        //public const int POWER_START = -1;
        //public const int POWER_END = 1;
        public const float FreqLowerLimit = 16;
        public const float FreqUpperLimit = 20000;

        public int N = 1;   //倍频程
        public OctaveBand[] bands = null;
        public string name = string.Empty;
        public float upper_freq = 1000;
        public float lower_freq = 1000;

        public OctaveEntity()
        {
            Init();
        }

        public int bandCount
        {
            get
            {
                if (bands != null)
                {
                    return bands.Length;
                }

                return 0;
            }
        }

        protected virtual void Init()
        {
            InitN();
            InitBands();
            InitNominal();
            InitName();

            //PrintTest();            
        }

        private void PrintTest()
        {
            for (int i = 0; i < this.bands.Length - 1; ++i)
            {
                //Console.WriteLine("{0} nominal freq = {1}, center freq = {2}, lower = {3}, upper = {4}", i, this.bands[i].nominal, this.bands[i].middle, this.bands[i].lower, this.bands[i].upper);
                Console.WriteLine("{0} nominal freq = {1}, center freq = {2}, delta = {3}", i, this.bands[i].nominal, this.bands[i].middle, this.bands[i + 1].middle - this.bands[i].middle);
            }
        }

        protected virtual void InitN()
        {

        }

        protected virtual void InitBands()
        {
            List<OctaveBand> obs = new List<OctaveBand>();

            int lowIndex = (int)(Math.Log10(OctaveEntity.FreqLowerLimit / OctaveEntity.Freq1K) * 10.0 / 3.0 * N) - 1;
            int upperIndex = (int)(Math.Log10(OctaveEntity.FreqUpperLimit / OctaveEntity.Freq1K) * 10.0 / 3.0 * N);

            if (N % 2 == 0)
            {
                lowIndex = (int)(Math.Log10(OctaveEntity.FreqLowerLimit / OctaveEntity.Freq1K) * (10.0 / 3.0) * 2.0 * N - 1) / 2;
                upperIndex = (int)(Math.Log10(OctaveEntity.FreqUpperLimit / OctaveEntity.Freq1K) * (10.0 / 3.0) * 2.0 * N - 1) / 2 + 1;
            }

            for (int i = lowIndex; i <= upperIndex; ++i)
            {
                double fc = OctaveEntity.Freq1K * Math.Pow(10.0, (3.0 / 10.0) * i / (double)N);

                if (N % 2 == 0)
                {
                    fc = OctaveEntity.Freq1K * Math.Pow(10.0, (3.0 / 10.0) * (2.0 * i + 1) / (2.0 * N));
                }

                double fd = Math.Pow(10.0, 3.0 / (10.0 * N * 2));

                float fcenter = (float)fc;
                float flower = (float)(fc / fd);
                float fupper = (float)(fc * fd);

                obs.Add(new OctaveBand(fcenter, flower, fupper));
            }

            bands = obs.ToArray();

            lower_freq = bands[0].lower;
            upper_freq = bands[bands.Length - 1].upper;
        }

        protected virtual void InitNominal()
        {

        }

        protected virtual void InitName()
        {

        }
    }

    public class Octave1 : OctaveEntity
    {
        protected override void InitN()
        {
            N = 1;
        }

        protected override void InitNominal()
        {
            bands[0].nominal = 16;
            bands[1].nominal = 31.5f;
            bands[2].nominal = 63;
            bands[3].nominal = 125;
            bands[4].nominal = 250;
            bands[5].nominal = 500;
            bands[6].nominal = 1000;
            bands[7].nominal = 2000;
            bands[8].nominal = 4000;
            bands[9].nominal = 8000;
            bands[10].nominal = 16000;
        }

        protected override void InitName()
        {
            name = "Octave";
        }
    }

    public class Octave3 : OctaveEntity
    {
        protected override void InitN()
        {
            N = 3;
        }

        protected override void InitNominal()
        {
            bands[0].nominal = 16;
            bands[1].nominal = 20;
            bands[2].nominal = 25;
            bands[3].nominal = 31.5f;
            bands[4].nominal = 40;
            bands[5].nominal = 50;
            bands[6].nominal = 63;
            bands[7].nominal = 80;
            bands[8].nominal = 100;
            bands[9].nominal = 125;
            bands[10].nominal = 160;
            bands[11].nominal = 200;
            bands[12].nominal = 250;
            bands[13].nominal = 315;
            bands[14].nominal = 400;
            bands[25].nominal = 500;
            bands[16].nominal = 630;
            bands[17].nominal = 800;
            bands[18].nominal = 1000;
            bands[19].nominal = 1250;
            bands[20].nominal = 1600;
            bands[21].nominal = 2000;
            bands[22].nominal = 2500;
            bands[23].nominal = 3150;
            bands[24].nominal = 4000;
            bands[25].nominal = 5000;
            bands[26].nominal = 6300;
            bands[27].nominal = 8000;
            bands[28].nominal = 10000;
            bands[29].nominal = 12500;
            bands[30].nominal = 16000;
            bands[31].nominal = 20000;
        }

        protected override void InitName()
        {
            name = "1/3 Octave";
        }
    }

    public class Octave6 : OctaveEntity
    {
        protected override void InitN()
        {
            N = 6;
        }

        protected override void InitNominal()
        {

        }

        protected override void InitName()
        {
            name = "1/6 Octave";
        }
    }

    public class Octave12 : OctaveEntity
    {
        protected override void InitN()
        {
            N = 12;
        }

        protected override void InitNominal()
        {

        }

        protected override void InitName()
        {
            name = "1/12 Octave";
        }
    }

    public class Octave24 : OctaveEntity
    {
        protected override void InitN()
        {
            N = 24;
        }

        protected override void InitNominal()
        {

        }

        protected override void InitName()
        {
            name = "1/24 Octave";
        }
    }

    public class OctaveFactory
    {
        public static OctaveEntity CreateOctave(SpectrumScale scale)
        {
            switch (scale)
            {
                case SpectrumScale.Octave:
                    return new Octave1();

                case SpectrumScale.Octave3:
                    return new Octave3();

                case SpectrumScale.Octave6:
                    return new Octave6();

                case SpectrumScale.Octave12:
                    return new Octave12();

                case SpectrumScale.Octave24:
                    return new Octave24();
            }

            return null;
        }
    }

    public class Octave
    {
        public SpectrumScale _scale = SpectrumScale.Log;
        public OctaveEntity _entity = null;

        public void UpdateScale(SpectrumScale sacle)
        {
            if (this._scale != sacle)
            {
                this._scale = sacle;
                if (this._scale != SpectrumScale.Log)
                {
                    _entity = OctaveFactory.CreateOctave(this._scale);
                }
            }
        }
    }
}
