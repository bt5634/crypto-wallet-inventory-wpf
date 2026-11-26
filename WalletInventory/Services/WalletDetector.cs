using Microsoft.Win32;
using WalletInventory.Models;

namespace WalletInventory.Services;

/// <summary>
/// Detects wallet software from Registry uninstall entries and known extension IDs.
/// Reads only display names, install dates and paths. Never opens wallets or reads secrets.
/// </summary>
public sealed class WalletDetector
{
    private static readonly string[] UninstallRoots =
    {
        @"SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall",
        @"SOFTWARE\WOW6432Node\Microsoft\Windows\CurrentVersion\Uninstall"
    };

    public IReadOnlyList<WalletRecord> ScanInstalledApps()
    {
        var results = new List<WalletRecord>();
        foreach (var root in UninstallRoots)
        using (var key = Registry.LocalMachine.OpenSubKey(root))
        {
            if (key is null) continue;
            foreach (var sub in key.GetSubKeyNames())
            using (var app = key.OpenSubKey(sub))
            {
                var name = app?.GetValue("DisplayName") as string;
                if (string.IsNullOrWhiteSpace(name) || !LooksLikeWallet(name)) continue;
                var date = app!.GetValue("InstallDate") as string;
                results.Add(new WalletRecord(name, "Desktop", true,
                    DateTime.TryParse(date, out var d) ? d : null, null, null));
            }
        }
        return results;
    }

    public IReadOnlyList<WalletRecord> ScanBrowserExtensions(string browser, string profilePath)
    {
        // Placeholder: enumerate known extension manifest names under the profile.
        // Only folder names are read; extension storage is never opened.
        return Array.Empty<WalletRecord>();
    }

    private static bool LooksLikeWallet(string name)
    {
        var n = name.ToLowerInvariant();
        return n.Contains("wallet") || n.Contains("metamask") || n.Contains("phantom")
            || n.Contains("ledger") || n.Contains("trezor") || n.Contains("coinbase");
    }
}
