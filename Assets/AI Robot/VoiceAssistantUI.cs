using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class VoiceAssistantUI : MonoBehaviour
{
    [Header("UI References")]
    public Button startButton; // Assign in Inspector
    public Button stopButton;  // Assign in Inspector
    public TextMeshProUGUI statusText; // Assign in Inspector
    public Toggle languageToggle; // Assign in Inspector (for language selection)

    [Header("Dependencies")]
    public AudioRecorder audioRecorder; // Assign in Inspector
    public APIClient apiClient;         // Assign in Inspector

    private void Start()
    {
        if (startButton == null || stopButton == null || statusText == null || languageToggle == null)
        {
            Debug.LogError("UI elements are not assigned!");
            return;
        }

        if (audioRecorder == null || apiClient == null)
        {
            Debug.LogError("AudioRecorder or APIClient script not found!");
            return;
        }

        // Initialize UI state
        SetUIState(true, false, "Press Start to Record");

        // Add listeners to the buttons
        startButton.onClick.AddListener(OnStartButtonClicked);
        stopButton.onClick.AddListener(OnStopButtonClicked);

        // Add listener to the language toggle
        languageToggle.onValueChanged.AddListener(OnLanguageToggleChanged);

        Debug.Log("VoiceAssistantUI initialized.");
    }

    private void OnStartButtonClicked()
    {
        Debug.Log("Start Button Clicked!");
        SetUIState(false, true, "Recording Started...");
        audioRecorder.StartRecording();
    }

    private void OnStopButtonClicked()
    {
        Debug.Log("Stop Button Clicked!");
        SetUIState(false, false, "Recording Stopped...");
        audioRecorder.StopRecording();
        StartCoroutine(SendAudioToAPI());
    }

    private IEnumerator SendAudioToAPI()
    {
        // Update text to indicate loading
        statusText.text = "Loading Response...";

        // Get the selected language from the toggle
        string selectedLanguage = languageToggle.isOn ? "fa" : "en"; // 'fa' for Persian, 'en' for English

        // Send the audio to the API with the selected language
        yield return apiClient.SendAudioToAPI(audioRecorder.GetRecordedFilePath(), selectedLanguage);

        // After API response, reset UI to initial state
        SetUIState(true, false, "Press Start to Record");
    }

    private void SetUIState(bool showStartButton, bool showStopButton, string statusMessage)
    {
        // Update button visibility
        startButton.gameObject.SetActive(showStartButton);
        stopButton.gameObject.SetActive(showStopButton);

        // Update the status text
        statusText.text = statusMessage;
    }

    private void OnLanguageToggleChanged(bool isPersian)
    {
        // Log the new language state
        Debug.Log("Language Toggled: " + (isPersian ? "Fa" : "En"));
    }
}