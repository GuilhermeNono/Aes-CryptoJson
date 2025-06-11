using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Json.Nodes;
using AesEncryptApp.Common;
using AesEncryptApp.Common.Constants;
using AesEncryptApp.Common.Enum;

while (true)
{
    var cryptoChoice = CryptoProcess.Encrypt;

    while (true)
    {
        Console.WriteLine("--- Você deseja encriptar ou decriptar? \n1(Encrypt)\n2(Decrypt)\n");

        var choose = Console.ReadLine();

        if (Enum.TryParse(choose, true, out cryptoChoice))
            break;

        Console.Clear();
    }

    await GetAndProcessInput(cryptoChoice);

    var exitChoice = ChoiceExit.No;

    while (true)
    {
        Console.WriteLine("-- Deseja fechar o console? \n1(Yes)\n2(No)\n");

        var exitChoose = Console.ReadLine();

        if (Enum.TryParse(exitChoose, true, out exitChoice))
            break;

        Console.Clear();
    }

    if (exitChoice == ChoiceExit.Yes)
    {
        Console.WriteLine("-- Saindo --");
        break;
    }

    Console.Clear();
}

async Task GetAndProcessInput(CryptoProcess choice)
{
    await using FileStream fileStream = new FileStream(PathConstant.InputFilePath, FileMode.Open, FileAccess.Read);

    var json = await JsonNode.ParseAsync(fileStream);

    Console.WriteLine("-- Encriptando as propriedades do Json --");

    if (choice is CryptoProcess.Encrypt)
        CryptoProcessElement(json, choice);

    File.WriteAllText(PathConstant.OutputFilePath,
        json.ToJsonString(new JsonSerializerOptions
            { WriteIndented = true, Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping }));

    Console.WriteLine("-- Json encriptado --\n");
}

void CryptoProcessElement(JsonNode? node, CryptoProcess process)
{
    if (node is null)
        return;

    switch (node)
    {
        case JsonObject obj:
        {
            foreach (var prop in obj.ToList())
            {
                if (prop.Value is null)
                    continue;
                CryptoProcessElement(prop.Value, process);
            }

            break;
        }
        case JsonArray array:
        {
            foreach (var item in array)
            {
                if (item is null)
                    continue;
                CryptoProcessElement(item, process);
            }

            break;
        }
        case JsonValue val:
        {
            var original = val.ToString();
            
            var str = process is CryptoProcess.Encrypt
                ? CriptoHelper.AesEncrypt(original)
                : CriptoHelper.AesDecrypt(original);
            
            node.ReplaceWith(JsonValue.Create(str));
            break;
        }
    }
}

void DecryptElement(JsonNode? node)
{
    if (node is null)
        return;

    switch (node)
    {
        case JsonObject obj:
        {
            foreach (var prop in obj.ToList())
            {
                if (prop.Value is null)
                    continue;
                DecryptElement(prop.Value);
            }

            break;
        }
        case JsonArray array:
        {
            foreach (var item in array)
            {
                if (item is null)
                    continue;
                DecryptElement(item);
            }

            break;
        }
        case JsonValue val:
        {
            var original = val.ToString();
            var encrypted = CriptoHelper.AesDecrypt(original);
            node.ReplaceWith(JsonValue.Create(encrypted));
            break;
        }
    }
}