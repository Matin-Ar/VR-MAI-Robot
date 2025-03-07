using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class APIClient : MonoBehaviour
{
    private string apiUrl = "http://localhost:8000/speech-to-speech";
    public AudioSource audioSource; // Assign in Inspector

    public IEnumerator SendAudioToAPI(string filePath, string language)
    {
        Debug.Log("Sending audio to API...");
        Debug.Log("Input File Path: " + filePath);
        Debug.Log("Input Language: " + language);

        // Create JSON data to send in the request body
        string jsonData = $"{{\"audio_file\": \"{filePath}\", \"language\": \"{language}\"}}";
        byte[] jsonBytes = System.Text.Encoding.UTF8.GetBytes(jsonData);

        // Send POST request
        using (UnityWebRequest request = new UnityWebRequest(apiUrl, "POST"))
        {
            request.uploadHandler = new UploadHandlerRaw(jsonBytes);
            request.downloadHandler = new DownloadHandlerBuffer();
            request.SetRequestHeader("Content-Type", "application/json");

            Debug.Log("Sending request to API...");
            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.LogError("API Error: " + request.error);
                Debug.LogError("Response Code: " + request.responseCode);
                Debug.LogError("Response Body: " + request.downloadHandler.text);
            }
            else
            {
                // Log the API response
                string response = request.downloadHandler.text;
                Debug.Log("API Response: " + response);

                // Parse the JSON response to extract the audio file path
                string audioFilePath = ParseAudioFilePath(response);
                if (!string.IsNullOrEmpty(audioFilePath))
                {
                    Debug.Log("Extracted Audio File Path: " + audioFilePath);
                    StartCoroutine(LoadAudio(audioFilePath));
                }
                else
                {
                    Debug.LogError("Failed to extract audio file path from API response.");
                }
            }
        }
    }

    private string ParseAudioFilePath(string jsonResponse)
    {
        try
        {
            // Parse the JSON response
            var responseData = JsonUtility.FromJson<APIResponse>(jsonResponse);
            return responseData.audio_file;
        }
        catch (System.Exception e)
        {
            Debug.LogError("Error parsing API response: " + e.Message);
            return null;
        }
    }

    private IEnumerator LoadAudio(string filePath)
    {
        using (UnityWebRequest request = UnityWebRequestMultimedia.GetAudioClip("file://" + filePath, AudioType.WAV))
        {
            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.LogError("Error loading audio: " + request.error);
            }
            else
            {
                // Get the audio clip from the response
                AudioClip responseClip = DownloadHandlerAudioClip.GetContent(request);

                // Assign the audio clip to the AudioSource and play it
                audioSource.clip = responseClip;
                audioSource.Play();
                Debug.Log("Playing response audio...");
            }
        }
    }

    // Class to represent the API response structure
    [System.Serializable]
    private class APIResponse
    {
        public string user_text;
        public string bot_response;
        public string audio_file;
    }
}