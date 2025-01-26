using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NAudio.Wave;
using NAudio.Dsp;
using NAudio.CoreAudioApi;

namespace VT_Spectralizer.app
{
    public class AudioCaptureFromOutput
    {
        private WasapiCapture capture;
        // private string audioOutputDeviceGuid;
        private const int BufferSize = 2048;  // Size of the buffer for capturing audio
        private const int SampleRate = 44100; // Sample rate (standard for most systems)
        private float maxOverallAmplitude = 0f;

        // Reference to Form1 to invoke UI updates
        private readonly FormMain _form;
        private string audioOutputDeviceGuid = string.Empty;

        // Frequency bands in Hz (Sub-bass, Bass, Low midrange, Midrange, Upper midrange, Presence, Brilliance)
        private readonly int[] frequencyLimits = new int[]
        {
        60,    // Sub-bass: 20-60 Hz
        250,   // Bass: 60-250 Hz
        500,   // Low midrange: 250-500 Hz
        2000,  // Midrange: 500 Hz - 2 kHz
        4000,  // Upper midrange: 2-4 kHz
        6000,  // Presence: 4-6 kHz
        20000  // Brilliance: 6-20 kHz
        };

        public AudioCaptureFromOutput(string deviceGuid, FormMain form)
        {
            audioOutputDeviceGuid = deviceGuid;
            _form = form;
            /*
            // Create a device enumerator to get the MMDevice
            var enumerator = new MMDeviceEnumerator();
            MMDevice device = enumerator.GetDevice(audioOutputDeviceGuid);
            */
            capture = new WasapiLoopbackCapture();
            capture.WaveFormat = new WaveFormat(SampleRate, 16, 2);  // Set the sample rate and channels (stereo)
        }

        // Start capturing audio from the selected output device (loopback)
        public void StartCapture(Action<string> UpdateLogs)
        {
            // Event to handle data availability
            capture.DataAvailable += (sender, e) =>
            {
                // Convert the byte buffer to an array of float values (normalized audio samples)
                float[] audioData = new float[e.BytesRecorded / 2];
                for (int i = 0; i < e.BytesRecorded; i += 2)
                {
                    short pcmSample = (short)((e.Buffer[i + 1] << 8) | e.Buffer[i]);  // Little-endian byte order
                    audioData[i / 2] = pcmSample / 32768f;  // Normalize to range -1.0 to 1.0
                }

                // Perform FFT and calculate frequency band volumes
                var frequencyVolumes = CalculateFrequencyBands(audioData);

                // Call the update action with the calculated volumes for the 7 frequency bands
                // Update UI controls with the calculated volumes on the main UI thread
                try
                {
                    _form.Invoke(new Action(() =>
                    {
                        // Update Form1 UI controls (labels) from the background thread
                        _form.UpdateFrequencyBands(frequencyVolumes);
                    }));
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            };
            // Start the capture process
            capture.StartRecording();
            UpdateLogs("Audio Capture Started");
        }

        // Stop capturing the audio
        public void StopCapture()
        {
            capture?.StopRecording();
            capture?.Dispose();
            maxOverallAmplitude = 0f;
        }

        // Calculate volumes for each frequency band
        private float[] CalculateFrequencyBands(float[] audioData)
        {
            // Determine the size of fftData based on audioData length
            int fftSize = audioData.Length;  // Use the length of audioData
            if (fftSize < BufferSize)
            {
                fftSize = BufferSize;  // Ensure fftData is large enough for FFT
            }
            // Create the FFT data array
            Complex[] fftData = new Complex[fftSize];
            for (int i = 0; i < audioData.Length; i++)
            {
                fftData[i].X = audioData[i];
                fftData[i].Y = 0;
            }

            // If fftData is larger than audioData, pad the remaining part with zeros
            for (int i = audioData.Length; i < fftData.Length; i++)
            {
                fftData[i].X = 0;
                fftData[i].Y = 0;
            }

            // Perform FFT on the captured audio data
            FastFourierTransform.FFT(true, (int)Math.Log(fftSize, 2), fftData);

            // Calculate energy for each frequency band
            float[] bandAmplitudes = new float[7];

            int numBins = fftData.Length / 2;  // Number of frequency bins (half the FFT size)

            float threshold = 0.0001f;

            for (int i = 0; i < numBins; i++)
            {
                float binFrequency = i * (SampleRate / (float)numBins);
                float amplitude = (float)Math.Sqrt(fftData[i].X * fftData[i].X + fftData[i].Y * fftData[i].Y);

                // Apply the threshold to ignore low amplitudes
                if (amplitude < threshold)
                {
                    amplitude = 0f;
                }

                // Accumulate the amplitude in the correct frequency band
                for (int band = 0; band < frequencyLimits.Length; band++)
                {
                    if (binFrequency <= frequencyLimits[band])
                    {
                        bandAmplitudes[band] += amplitude;
                    }
                }
            }

            // Find the max amplitude for the current loop and compare it to the global max
            float currentMaxAmplitude = bandAmplitudes.Max();

            // Update the global max amplitude if the current one is larger
            if (currentMaxAmplitude > maxOverallAmplitude)
            {
                maxOverallAmplitude = currentMaxAmplitude;
            }


            if (maxOverallAmplitude < 0.005f)  // Set this threshold to suit your needs
            {
                return new float[] { 0f, 0f, 0f, 0f, 0f, 0f, 0f };  // Silence
            }
            // Apply exponential smoothing to the frequency bands
            // float[] smoothedVolumes = new float[7];  // Store smoothed values
            float[] frequencyVolumes = new float[7];

            for (int i = 0; i < bandAmplitudes.Length; i++)
            {
                // Apply exponential smoothing to the frequencies (smoothing factor of 0.2 or 0.3)
                // smoothedVolumes[i] = smoothedVolumes[i] * 0.8f + bandAmplitudes[i] * 0.2f; // Adjust 0.2f for more/less smoothing
                frequencyVolumes[i] = (bandAmplitudes[i] / maxOverallAmplitude) * 100;

                // If the amplitude is really small, consider it as zero.
                if (frequencyVolumes[i] < 1)
                {
                    frequencyVolumes[i] = 0;
                }
            }
            return frequencyVolumes;
        }
    }
}
