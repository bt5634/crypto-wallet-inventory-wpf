# Crypto Wallet Inventory (C# WPF)

Local-only desktop tool that inventories installed crypto wallet software and browser extensions. It reports only safe metadata: wallet name, install status, install date, browser and browser profile. It never opens wallet software and never reads seed phrases, private keys, balances or personal files.

## Scope
- Windows Registry uninstall keys and file-system probes
- Browser extension IDs for Chrome/Edge/Firefox/Brave profiles
- No network calls, no key material, no balance queries

## Build
`dotnet publish -c Release -r win-x64 --self-contained true /p:PublishSingleFile=true`
