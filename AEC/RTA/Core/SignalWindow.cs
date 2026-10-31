using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;

namespace AEC.RTA.Core
{
    public class SignalWindow
    {
        public double[] _win = null;

        private WindowFunction _wf = WindowFunction.HannWindow;
        public WindowFunction wf
        {
            get
            {
                return _wf;
            }
            set
            {
                _wf = value;
                SignalWindowHelper.UpdateWindow(this);
            }
        }

        public class SignalWindowHelper
        {
            public static void UpdateWindow(SignalWindow window)
            {
                Debug.Assert(window._win != null);
                switch (window.wf)
                {
                    case WindowFunction.HannWindow:
                        UpdateHannWindow(window);
                        break;

                    case WindowFunction.HammingWindow:
                        UpdateHammingWindow(window);
                        break;

                    case WindowFunction.RectangleWindow:
                        UpdateRectangleWindow(window);
                        break;

                    case WindowFunction.GaussianWindow:
                        UpdateGaussianWindow(window);
                        break;

                    case WindowFunction.BlackmanWindow:
                        UpdateBlackmanWindow(window);
                        break;

                    case WindowFunction.BlackmanHarrisWindow:
                        UpdateBlackmanHarrisWindow(window);
                        break;

                    case WindowFunction.FlatTopWindow:
                        UpdateFlatTopWindow(window);
                        break;
                }
            }

            private static void UpdateHannWindow(SignalWindow window)
            {
                for (int i = 0; i < window.length; ++i)
                {
                    window._win[i] = 0.5 * (1 - Math.Cos((2 * Math.PI * i) / (window.length - 1)));
                }
            }

            private static void UpdateHammingWindow(SignalWindow window)
            {
                for (int i = 0; i < window.length; ++i)
                {
                    window._win[i] = 0.54 - 0.46 * Math.Cos(2 * Math.PI * i / (window.length - 1));
                }
            }

            private static void UpdateRectangleWindow(SignalWindow window)
            {
                for (int i = 0; i < window.length; ++i)
                {
                    window._win[i] = 1.0;
                }
            }

            private static void UpdateGaussianWindow(SignalWindow window)
            {
                double k = 0.0f;
                double beta = 0.0f;
                double theta = 0.0f;
                double alpha = 2.5f;
                for (int i = 0; i < window.length; ++i)
                {
                    if (window.length % 2 == 1)
                    {
                        k = i - (window.length - 1) / 2;
                        beta = 2 * alpha * k / (window.length - 1);
                    }
                    else
                    {
                        k = i - (window.length) / 2;
                        beta = 2 * alpha * k / (window.length - 1);
                    }

                    theta = Math.Pow(beta, 2);
                    window._win[i] = Math.Exp(-1 * theta / 2);
                }
            }

            private static void UpdateBlackmanWindow(SignalWindow window)
            {
                for (int i = 0; i < window.length; ++i)
                {
                    window._win[i] = 0.42 - 0.5 * Math.Cos(2 * Math.PI * (double)i / (window.length - 1)) + 0.08f * (double)Math.Cos(4 * Math.PI * (double)i / (window.length - 1));
                }
            }

            private const double BLACKMANHARRIS_A0 = 0.35875;
            private const double BLACKMANHARRIS_A1 = 0.48829;
            private const double BLACKMANHARRIS_A2 = 0.14128;
            private const double BLACKMANHARRIS_A3 = 0.01168;

            private static void UpdateBlackmanHarrisWindow(SignalWindow window)
            {
                for (int i = 0; i < window.length; ++i)
                {
                    window._win[i] = BLACKMANHARRIS_A0 - BLACKMANHARRIS_A1 * Math.Cos(2 * Math.PI * (double)i / (window.length)) +
                                             BLACKMANHARRIS_A2 * Math.Cos(4 * Math.PI * (double)i / (window.length)) -
                                             BLACKMANHARRIS_A3 * Math.Cos(6 * Math.PI * (double)i / (window.length));
                }
            }

            private const double FLATTOPWIN_A0 = 0.215578995;
            private const double FLATTOPWIN_A1 = 0.41663158;
            private const double FLATTOPWIN_A2 = 0.277263158;
            private const double FLATTOPWIN_A3 = 0.083578947;
            private const double FLATTOPWIN_A4 = 0.006947368;

            private static void UpdateFlatTopWindow(SignalWindow window)
            {
                for (int i = 0; i < window.length; ++i)
                {
                    window._win[i] = FLATTOPWIN_A0 - FLATTOPWIN_A1 * Math.Cos(2 * Math.PI * (double)i / (window.length - 1)) +
                     FLATTOPWIN_A2 * Math.Cos(4 * Math.PI * (double)i / (window.length - 1)) -
                     FLATTOPWIN_A3 * Math.Cos(6 * Math.PI * (double)i / (window.length - 1)) +
                     FLATTOPWIN_A4 * Math.Cos(8 * Math.PI * (double)i / (window.length - 1));
                }
            }
        }

        public int length
        {
            get;
            private set;
        }

        public SignalWindow(int window_length)
        {
            length = window_length;
            _win = new double[window_length];
            wf = WindowFunction.RectangleWindow;
        }

        public void SignalWindowing(double[] signal)
        {
            Debug.Assert(signal.Length == length);
            if (this._wf == WindowFunction.RectangleWindow)
            {
                return;
            }

            for (int i = 0; i < length; ++i)
            {
                signal[i] = signal[i] * _win[i];
            }
        }
    }

    public class SignalWindowFactory
    {
        public static SignalWindow CreateWindowFunction(int length)
        {
            return new SignalWindow(length);
        }
    }
}
