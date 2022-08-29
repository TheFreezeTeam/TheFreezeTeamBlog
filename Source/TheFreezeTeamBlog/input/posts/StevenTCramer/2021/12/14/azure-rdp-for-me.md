DocumentName: azure-rdp-for-me
Title: RDP for Me
Published: 12/14/2021
Image: RDP-for-me.png
Tags: 
  - Azure
  - Rdp
  - PowerShell
Author: Steven T. Cramer
Description: PowerShell script to add RDP access to Azure VM
Excerpt: Powershell script using the az cli to remove and add my local IP to the allowed RDP rules for a VM.
---

# RDP For Me

## Scenario

Leaving RDP open to all IPs allows anyone on the internet to try and log into your VM.  So we restrict RDP access to a specific single IP when needed.

Removing and adding the rule, via the portal UI, every time your ip changes is tedious.

So if possible automate the tedious to make it less so!

## Solution

Powershell script using the az cli to remove and add my local IP to the allowed RDP rules for a VM.

A quick search on [DuckDuckGo](https://duckduckgo.com/) and I found a solution. Thanks [Mark Heath!](https://markheath.net/post/azure-cli-access-restrictions)

I added a couple functions to my PowerShell profile.

Small one to get your current public IP Address

```powershell
function Get-PublicIP {
  $myIp = (Invoke-WebRequest -uri "https://ifconfig.me/ip").Content
  return $myIp
}
```

Then Marks `Add-NsgRule` except I want to delete if the rule already exists.

```powershell
function Add-NsgRule {
  Param([String]$Sub, [String]$ResGroup, [String]$NsgName, [String]$IpAddress, [String]$RuleName)
  az account set -s $Sub
  if (!$?) {
      Write-Output "Could not select subscription"
      return
  }
  $exists = az network nsg rule list `
      --nsg-name $NsgName -g $ResGroup `
      --query "[?sourceAddressPrefix=='$IpAddress/32' || sourceAddressPrefix=='$IpAddress'].name" `
      -o tsv
  if (!$?) {
      Write-Output "Error checking if NSG rule exists"
      return
  }
  if ($exists) {
      Write-Output "Rule for NSG $NsgName already exists with name '$exists'.  Deleting...";
      az network nsg rule delete -g $ResGroup --nsg-name $NsgName -n $RuleName
  }
  else {
      Write-Output "Adding rule to NSG $NsgName";
      az network nsg rule create -g $ResGroup `
          --nsg-name $NsgName -n $RuleName `
          --priority 4096 --access Allow `
          --source-address-prefixes "$IpAddress/32" `
          --source-port-ranges '*' `
          --destination-address-prefixes '*' `
          --destination-port-ranges '*'
      if (!$?) {
          Write-Output "Failed to add NSG Rule"
      }
  }
}
```

Then I added specific functions for specific VMs

```powershell
function Set-Rdp-For-Me-FoobarVM {
  $Sub = "MySub"
  $ResGroup = "MyResourceGroup"
  $NsgName = "MyNetworkSecurityGroup"
  $IpAddress = Get-PublicIP
  $RuleName = "rdp-for-me"

  Add-NsgRule -Sub $Sub -ResGroup $ResGroup -NsgName $NsgName -IpAddress $IpAddress -RuleName $RuleName
}
```

## Usage

So now if my ip changes I can:

1. Open powershell (probably already open) 
2. login to azure `az login` 
3. run `Set-Rdp-For-Me-FoobarVM`
4. RDP into my VM!

## References
https://markheath.net/post/azure-cli-access-restrictions
https://www.prajwaldesai.com/get-public-ip-address-using-powershell/
