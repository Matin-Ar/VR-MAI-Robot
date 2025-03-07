# VR-MAI-Robot

## 🎮 Overview
**VR-MAI-Robot** is a **VR Multiplayer AI-powered Robot** that integrates voice-based AI interaction in a **Unity VR Multiplayer Template**. It connects to the AI core (**MAI-Robot**) via API for advanced speech processing, AI-generated responses, and interactive learning experiences.

The VR robot can:
- **Record and process speech** using Unity.
- **Send audio data to the AI server (MAI-Robot)**.
- **Receive AI-generated responses** (both text and synthesized voice).
- **Display and play AI-generated responses** in a VR multiplayer environment.

This project is designed to create an **interactive AI-powered VR assistant**.

## 🏗️ Features
- 🎙️ **Voice-to-Text Processing** (Speech Recognition)
- 🧠 **AI-Generated Responses** (Chatbot with RAG & OpenAI GPT)
- 🔊 **Text-to-Speech (TTS)** (AI Voice Playback)
- 🎮 **VR Multiplayer Compatibility**
- ⚡ **Unity-based UI for Voice Interaction**
- 🌍 **Multi-language Support (English, Persian, etc.)**

---

## 📦 Project Main Scripts
```bash
VR-MAI-Robot/
│-- Assets/  # Unity project assets
│   ├──AI Robot/ # scripts folder
│      ├── APIClient.cs  # Handles communication with AI core (MAI-Robot)
│      ├── AudioRecorder.cs  # Records and processes user speech
│      ├── SavWav.cs  # Saves recorded audio in .wav format
│      ├── VoiceAssistantUI.cs  # Manages UI interactions for the AI assistant
```

---

## 🛠️ Setup & Installation
### **1️⃣ Clone the Repository**
```sh
git clone https://github.com/Matin-Ar/VR-MAI-Robot.git
cd VR-MAI-Robot
```

### **2️⃣ Open the Project in Unity**
- Open **Unity Hub**
- Click **Open Project** → Select the **VR-MAI-Robot** folder
- Make sure you're using **Unity 2021 or later** (for VR compatibility)

### **3️⃣ Install Required Dependencies**
- **Unity XR Plugin (for VR)**
- **Unity Networking (for Multiplayer)**
- **TextMeshPro (for UI elements)**
- **Microphone Permissions (for Audio Recording)**

### **4️⃣ Setup AI Core (MAI-Robot)**
The AI core runs separately as a **FastAPI Python server**. You need to set it up:
1. Clone the MAI-Robot repository:
   ```sh
   git clone https://github.com/Matin-Ar/MAI-Robot.git
   cd MAI-Robot
   ```
2. Install dependencies:
   ```sh
   pip install -r requirements.txt
   ```
3. Run the API server:
   ```sh
   python MAI.py
   ```
4. The server will start on `http://localhost:8000`, which the Unity project will communicate with.

---

## 🚀 Usage Guide
### **1️⃣ Start the VR Multiplayer Experience**
- Launch **Unity** and run the VR scene.
- Ensure that **VR headset & controllers** are connected.

### **2️⃣ AI Voice Interaction**
- Click **Start Recording** in VR (via UI panel or controller button).
- Speak into the microphone.
- Click **Stop Recording**.
- The recorded speech will be sent to the **MAI-Robot AI Core**.
- The AI response will be **displayed as text** and **played as audio**.

### **3️⃣ Multiplayer Mode**
- Start the VR multiplayer template.
- Multiple users can interact with the AI in a shared **VR environment**.

---

## 🔧 Key Components Explained
### **1️⃣ APIClient.cs**
- **Sends recorded audio to the AI API** (`http://localhost:8000/speech-to-speech`)
- **Receives AI-generated responses** (text + audio)
- **Plays the AI-generated audio response**

### **2️⃣ AudioRecorder.cs**
- **Records user speech** using Unity’s Microphone API
- **Saves the recording as a `.wav` file**
- **Handles recording start/stop logic**

### **3️⃣ VoiceAssistantUI.cs**
- **UI for user interaction (Start/Stop recording, Language Toggle, etc.)**
- **Sends user speech data to the AI Core**
- **Displays AI-generated responses in the VR UI**

### **4️⃣ SavWav.cs**
- **Converts and saves recorded audio as a `.wav` file`** (Used for API communication)

---

## 🌍 Multi-Language Support
- The project supports both **English (en)** and **Persian (fa)**.
- Users can toggle the language using the **UI Toggle Button**.
- The AI core (`MAI-Robot`) will respond in the selected language.

---

## 🛡️ Security & Best Practices
### **1️⃣ API URL Configuration**
- The **API endpoint is set in Unity Inspector** (`APIClient.cs`).
- Do **not** hardcode sensitive API keys.

### **2️⃣ Permissions Required**
- **Microphone Access** (For voice recording)
- **File Storage Access** (For saving `.wav` files)

### **3️⃣ Error Handling**
- Logs API responses and errors in the Unity Console.
- Handles API request failures gracefully (`Debug.LogError`).

---

## 📜 License
This project is **open-source** under the **MIT License**. Feel free to use, modify, and distribute it.

---

## 🤝 Contributing
If you want to improve the project:
1. **Fork the repository**
2. **Make changes in a new branch**
3. **Submit a pull request**

---

## 📬 Contact
- **Author:** Matin
- **GitHub:** [Matin-Ar](https://github.com/Matin-Ar)
- **MAI Robot AI Core Repo:** [MAI-Robot](https://github.com/Matin-Ar/MAI-Robot)
