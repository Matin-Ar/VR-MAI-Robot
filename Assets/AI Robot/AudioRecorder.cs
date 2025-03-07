using UnityEngine;

public class AudioRecorder : MonoBehaviour
{
    private AudioClip recordedClip;
    private bool isRecording = false;
    private string filePath;

    public void StartRecording()
    {
        if (!isRecording)
        {
            filePath = Application.persistentDataPath + "/user_audio.wav"; // Save in a persistent data path
            recordedClip = Microphone.Start(null, false, 600, 44100); // Record for up to 10 minutes
            isRecording = true;
            Debug.Log("Recording started...");
        }
    }

    public void StopRecording()
    {
        if (isRecording)
        {
            int recordingPosition = Microphone.GetPosition(null);
            Microphone.End(null);
            isRecording = false;

            // Trim the audio clip to the actual recorded length
            if (recordingPosition > 0)
            {
                recordedClip = TrimAudioClip(recordedClip, recordingPosition);
                SaveRecording();
                Debug.Log("Recording stopped and saved.");
            }
            else
            {
                Debug.LogError("No audio recorded or recording position is invalid.");
            }
        }
    }

    public string GetRecordedFilePath()
    {
        return filePath; // Return the file path of the recorded audio
    }

    private void SaveRecording()
    {
        // Save the recorded clip to a file (e.g., WAV format)
        SavWav.Save(filePath, recordedClip);
        Debug.Log("Audio saved to: " + filePath);
    }

    private AudioClip TrimAudioClip(AudioClip clip, int position)
    {
        if (clip == null || position <= 0)
        {
            Debug.LogError("Invalid clip or position.");
            return null;
        }

        // Create a new AudioClip with the exact length of the recorded audio
        float[] samples = new float[position * clip.channels];
        clip.GetData(samples, 0);

        AudioClip trimmedClip = AudioClip.Create("TrimmedClip", position, clip.channels, clip.frequency, false);
        trimmedClip.SetData(samples, 0);

        return trimmedClip;
    }
}