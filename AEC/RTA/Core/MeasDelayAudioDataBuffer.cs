using System;
using System.Collections.Generic;
using System.Text;
using AEC.RTA.Utils;

namespace AEC.RTA.Core
{
    public class MeasDelayAudioDataBuffer
    {
        public enum MeasDelayOverlap
        {
            Overlap_0Percent = 0,       //0%
            Overlap_25Percent,      //25%
            Overlap_50Percent,      //50%
            Overlap_75Percent,      //75%
        };

        private bool _isRecvData = false;
        private int _repeatTimes = 4;
        private MeasDelayOverlap _overlap = MeasDelayOverlap.Overlap_25Percent;
        private int _fftSize = AudioRTAParam.FFTSize32K;
        private int _audioLengthToCalcDelay = 0;
        private int _recvDataLength = 0;
        //public double[][] _audioDataBuffer = null;
        public StereoBuffer _audioDataBuffer = null;

        public MeasDelayAudioDataBuffer()
        {
            //_audioLengthToCalcDelay = _FFTSize * _repeatTimes - (_repeatTimes - 1) * (int)_overlap * _FFTSize / 4 ;
            _audioLengthToCalcDelay = _fftSize * _repeatTimes - (_repeatTimes - 1) * (int)_overlap * _fftSize / 4;

            _audioDataBuffer = new StereoBuffer(_audioLengthToCalcDelay);
        }

        public int audioLength
        {
            get
            {
                return _audioLengthToCalcDelay;
            }
        }

        public int recvDataLength
        {
            get
            {
                return _recvDataLength;
            }
        }

        public float progress
        {
            get
            {
                return (float)_recvDataLength / (float)_audioLengthToCalcDelay;
            }
        }

        public int fftSize
        {
            get
            {
                return _fftSize;
            }
        }

        public int repeatTimes
        {
            get
            {
                return _repeatTimes;
            }
        }

        public void StartRecvAudioData()
        {
            if (!_isRecvData)
            {
                _recvDataLength = 0;
                _isRecvData = true;
            }
        }

        public void StopRecvAudioData()
        {
            _isRecvData = false;
            _recvDataLength = 0;
        }

        /// <summary>
        /// 准备接收两个通道的音频数据，如果
        /// </summary>
        /// <param name="leftCh">左通道数据</param>
        /// <param name="rightCh">右通道数据</param>
        /// <param name="length">当前数据长度</param>
        /// <returns></returns>
        public bool RecvData(double[] leftCh, double[] rightCh, int length)
        {
            if (!_isRecvData)
            {
                return true;
            }

            if (length < _audioLengthToCalcDelay)
            {
                if (_recvDataLength + length < _audioLengthToCalcDelay)
                {
                    Utility.CopyData2ArrayEnd(leftCh, _audioDataBuffer.left);
                    Utility.CopyData2ArrayEnd(rightCh, _audioDataBuffer.right);

                    _recvDataLength += length;

                    return false;
                }
                else
                {
                    //int extra = _recvDataLength + length - _audioLengthToCalcDelay;//多余的
                    //int effective = length - extra; //有效的
                    int effective = _audioLengthToCalcDelay - _recvDataLength;
                    if (effective < length)
                    {
                        double[] left = new double[effective];
                        double[] right = new double[effective];

                        Array.Copy(leftCh, left, effective);
                        Array.Copy(rightCh, right, effective);

                        Utility.CopyData2ArrayEnd(left, _audioDataBuffer.left);
                        Utility.CopyData2ArrayEnd(right, _audioDataBuffer.right);
                    }
                    else
                    {
                        Utility.CopyData2ArrayEnd(leftCh, _audioDataBuffer.left);
                        Utility.CopyData2ArrayEnd(rightCh, _audioDataBuffer.right);
                    }

                    _recvDataLength = _audioLengthToCalcDelay;

                    //数据已经采集完毕
                    _isRecvData = false;
                    return true;
                }
            }
            else
            {
                //如果此次数据长度大于需要长度，则拷贝数据
                Array.Copy(leftCh, _audioDataBuffer.left, _audioLengthToCalcDelay);
                Array.Copy(rightCh, _audioDataBuffer.right, _audioLengthToCalcDelay);

                _isRecvData = false;
                return true;
            }
        }

        public double[] GetMonoData(int index, AudioChannel channel)
        {
            if (index >= _repeatTimes)
            {
                return null;
            }

            int startIndex = (_fftSize - _fftSize / 4 * (int)_overlap) * index;
            int ch = (int)channel;
            double[] mono = new double[_fftSize];
            double[] buffer = channel == AudioChannel.LeftCh ? _audioDataBuffer.left : _audioDataBuffer.right;
            Array.Copy(buffer, startIndex, mono, 0, _fftSize);


            return mono;
        }

        public bool isRecvData
        {
            get
            {
                return _isRecvData;
            }
        }
    }
}
