using System;
using System.Collections.Specialized;
using System.Text.Json;
using System.IO;
using jurnalmodul8_103022300018;
using Newtonsoft.Json;

class Transfer
{
    public int threshold { get; set; }
    public int low_fee { get; set; }
    public int high_fee { get; set; }
}

class BankTransferConfig
{
    public string Lang { get; set; } = string.Empty;
    public Transfer Transfer { get; set; } = new Transfer();
    public string[] Method { get; set; } = Array.Empty<string>();
    public string Confirmation { get; set; } = string.Empty;

    private const string ConfigPath = "bank_transfer_config.json";

    public void BankConfig()
    {
        LoadConfig();
    }

    private void LoadConfig()
    {
        BankTransferConfig config = ReadConfigFile();
        Lang = config.Lang;
        Transfer = config.Transfer;
        Method = config.Method;
        Confirmation = config.Confirmation;
    }

    private BankTransferConfig ReadConfigFile()
    {
        BankTransferConfig config;
        config = new BankTransferConfig();
        config.Transfer = Transfer;
        config.Method = Method;
        config.Confirmation = Confirmation;
        return config;
    }

    private void WriteNewConfigFile()
    {
        var options = new JsonSerializerOptions { WriteIndented = true };
        string jsonString = System.Text.Json.JsonSerializer.Serialize(this, options);
        File.WriteAllText(ConfigPath, jsonString);
    }
}

class Program
{
    static void Main(string[] args)
    {
        BankTransferConfig config = new BankTransferConfig();
        config.BankConfig();

        Console.WriteLine(config.Lang == "en" ? "Please insert the amount of money to transfer:" : "Masukkan jumlah uang yang akan di-transfer:");
        string? amountInput = Console.ReadLine();
        if (amountInput == null)
        {
            throw new ArgumentNullException(nameof(amountInput), "Amount input cannot be null.");
        }
        int amount = int.Parse(amountInput);

        int transferFee = amount <= config.Transfer.threshold ? config.Transfer.low_fee : config.Transfer.high_fee;
        int totalAmount = amount + transferFee;

        if (config.Lang == "en")
        {
            Console.WriteLine($"Transfer fee = {transferFee}");
            Console.WriteLine($"Total amount = {totalAmount}");
            Console.WriteLine("Select transfer method:");
        }
        else
        {
            Console.WriteLine($"Biaya transfer = {transferFee}");
            Console.WriteLine($"Total biaya = {totalAmount}");
            Console.WriteLine("Pilih metode transfer:");
        }

        for (int i = 0; i < config.Method.Length; i++)
        {
            Console.WriteLine($"{i + 1}. {config.Method[i]}");
        }

        string? methodChoiceInput = Console.ReadLine();
        if (methodChoiceInput == null)
        {
            throw new ArgumentNullException(nameof(methodChoiceInput), "Method choice input cannot be null.");
        }
        else
        {
            int methodChoice = int.Parse(methodChoiceInput);
            if (methodChoice < 1 || methodChoice > config.Method.Length)
            {
                throw new ArgumentOutOfRangeException(nameof(methodChoice), "Invalid method choice.");
            }
        }

        if (config.Lang == "en")
        {
            Console.WriteLine($"Please type \"{config.Confirmation}\" to confirm the transaction:");
        }
        else
        {
            Console.WriteLine($"Ketik \"{config.Confirmation}\" untuk mengkonfirmasi transaksi:");
        }

        string? confirmationInput = Console.ReadLine();
        if (confirmationInput == null)
        {
            throw new ArgumentNullException(nameof(confirmationInput), "Confirmation input cannot be null.");
        }

        if (confirmationInput == config.Confirmation)
        {
            if (config.Lang == "en")
            {
                Console.WriteLine("The transfer is completed");
            }
            else
            {
                Console.WriteLine("Proses transfer berhasil");
            }
        }
        else
        {
            if (config.Lang == "en")
            {
                Console.WriteLine("Transfer is cancelled");
            }
            else
            {
                Console.WriteLine("Transfer dibatalkan");
            }
        }
    }
}
