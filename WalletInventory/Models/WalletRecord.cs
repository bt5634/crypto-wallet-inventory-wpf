namespace WalletInventory.Models;

public sealed record WalletRecord(
    string Name,
    string Kind,
    bool Installed,
    DateTime? InstallDate,
    string? Browser,
    string? Profile);
