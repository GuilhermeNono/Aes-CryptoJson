# 🔒 Aes-CryptoJson

Aes-CryptoJson is a C# project that provides an easy way to 🔐 encrypt and 🔓 decrypt the properties of a JSON file using the AES (Advanced Encryption Standard) algorithm.

## 💡 How It Works

- The user places a JSON file in a specific input folder.
- The application automatically processes the file:
  - If set to "Encrypt" mode, it outputs the encrypted JSON to an output folder.
  - If set to "Decrypt" mode, it outputs the decrypted JSON to the output folder.
- The operation mode (encrypt/decrypt) is controlled via a simple console application.

## ✨ Features

- 🛡️ **AES Encryption:** Securely encrypts JSON properties using AES.
- 🔓 **AES Decryption:** Restores encrypted JSON properties to their original values.
- 🖥️ **Console Mode Selection:** Easily choose encrypt or decrypt mode from the console

## 🚀 Usage

1. **Configure Input/Output Folders:**
   - Place the original JSON files in the designated input folder (e.g., `input/`).
   - The application will process the files and place the results in the output folder (e.g., `output/`).

2. **Run the Console App:**
   - Start the application.
   - Select the desired mode: `Encrypt` or `Decrypt`.
   - The app will watch the input folder and process JSON files as they arrive.

3. **Retrieve Results:**
   - Collect the processed (encrypted or decrypted) JSON files from the output folder.

## 🧑‍💻 Example

```bash
dotnet run
# Choose Encrypt or Decrypt in the console
# Place files in input/
# Get results from output/
```

## 🔧 Configuration

- AES key and other settings are configurable in the configuration.json.

## 🔒 When to Use

- To secure sensitive data in JSON files for storage or transfer.
- To automate encryption/decryption workflows in C# environments.
- For compliance with data security and privacy requirements.

## 📄 License

This project is licensed under the MIT License.

---

> ⚠️ **Security Tip:** Always use a strong, random key and never hard-code sensitive keys in your source code for production!
