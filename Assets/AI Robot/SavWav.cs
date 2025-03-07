using System;
using System.IO;
using UnityEngine;

public static class SavWav
{
    private const int HEADER_SIZE = 44;

    public static bool Save(string filePath, AudioClip clip)
    {
        if (!filePath.ToLower().EndsWith(".wav"))
        {
            filePath += ".wav";
        }

        var directory = Path.GetDirectoryName(filePath);
        if (!Directory.Exists(directory))
        {
            Directory.CreateDirectory(directory);
        }

        using (var fileStream = new FileStream(filePath, FileMode.Create))
        {
            using (var writer = new BinaryWriter(fileStream))
            {
                writer.Write("RIFF".ToCharArray()); // RIFF header
                writer.Write(0); // File size (to be filled later)
                writer.Write("WAVE".ToCharArray()); // WAVE header
                writer.Write("fmt ".ToCharArray()); // fmt chunk
                writer.Write(16); // fmt chunk size
                writer.Write((short)1); // Audio format (1 = PCM)
                writer.Write((short)clip.channels); // Number of channels
                writer.Write(clip.frequency); // Sample rate
                writer.Write(clip.frequency * clip.channels * 2); // Byte rate
                writer.Write((short)(clip.channels * 2)); // Block align
                writer.Write((short)16); // Bits per sample
                writer.Write("data".ToCharArray()); // data chunk
                writer.Write(0); // Data size (to be filled later)

                var samples = new float[clip.samples * clip.channels];
                clip.GetData(samples, 0);

                var intData = new short[samples.Length];
                for (int i = 0; i < samples.Length; i++)
                {
                    intData[i] = (short)(samples[i] * 32767);
                }

                writer.Write(ToByteArray(intData));

                // Fill in file size
                fileStream.Seek(4, SeekOrigin.Begin);
                writer.Write((int)(fileStream.Length - 8));

                // Fill in data size
                fileStream.Seek(40, SeekOrigin.Begin);
                writer.Write((int)(fileStream.Length - HEADER_SIZE));
            }
        }

        return true;
    }

    private static byte[] ToByteArray(short[] array)
    {
        var byteArray = new byte[array.Length * 2];
        Buffer.BlockCopy(array, 0, byteArray, 0, byteArray.Length);
        return byteArray;
    }
}