---
DocumentName: azure-resize-os-drive
Title: Expand the OS Drive on your Azure Resource Manager VM
Published: 01/15/2017
Tags: 
  - CSharp 
Author: Steven T. Cramer
Description: This can be done via the portal by simply Stop and Deallocate the system and then select the drive and choose a larger size.
Excerpt: This can be done via the portal by simply Stop and Deallocate the system and then select the drive and choose a larger size.
---

This can be done via the portal by simply Stop and Deallocate the system and then select the drive and choose a larger size.

![](2017-01-20_1658.png)

To script this via PowerShell you can do the following:

A bit more information on following the instructions from Microsoft found
[here](https://docs.microsoft.com/en-us/azure/virtual-machines/virtual-machines-windows-expand-os-disk).

* Sign-in to your Microsoft Azure account in resource management mode and select your subscription as follows:

```Powershell
Login-AzureRmAccount
```

This will pop up the Azure Login window show below
![](2017-01-15_1119.png)

After successfully entering my account information I see:

```Powershell
λ  Login-AzureRmAccount

Environment           : AzureCloud
Account               : StevenTCramer@xxx.com
TenantId              : xxxxxxxx-xxxx-xxxx-xxxx-xxxxxxxxxxxx
SubscriptionId        : xxxxxxxx-xxxx-xxxx-xxxx-xxxxxxxxxxxx
SubscriptionName      : Visual Studio Enterprise
CurrentStorageAccount :
```

You can see the SubscriptionName above:  But to get a list of subscriptions you can use `Get-AzureRmSubscription`

* To Change the subscription:

```Powershell
Select-AzureRmSubscription –SubscriptionName 'my-subscription-name'
```

* Set your resource group name and VM name as follows:

```Powershell
$rgName = 'my-resource-group-name'
$vmName = 'my-vm-name'
```

To get a list of you resource groups use `Get-AzureRmResourceGroup`.  To get list of your Azure VMs use `Get-azureRMVM`

* Obtain a reference to your VM as follows:

```Powershell
$vm = Get-AzureRmVM -ResourceGroupName $rgName -Name $vmName
```

* Stop the VM before resizing the disk as follows:

```Powershell
 Stop-AzureRmVM -ResourceGroupName $rgName -Name $vmName
```

Output

```PowerShell
λ  Stop-AzureRmVM -ResourceGroupName $rgName -Name $vmName

Virtual machine stopping operation
This cmdlet will stop the specified virtual machine. Do you want to continue?
[Y] Yes  [N] No  [S] Suspend  [?] Help (default is "Y"): Y


OperationId :
Status      : Succeeded
StartTime   : 1/15/2017 11:49:08 AM
EndTime     : 1/15/2017 11:51:31 AM
Error       :
```

* And here comes the moment we’ve been waiting for! Set the size of the OS disk to the desired value and update the VM as follows:

```PowerShell
$vm.StorageProfile.OSDisk.DiskSizeGB = 1023
Update-AzureRmVM -ResourceGroupName $rgName -VM $vm
```

Output

```PowerShell
λ  $vm.StorageProfile.OSDisk.DiskSizeGB = 1023
λ  Update-AzureRmVM -ResourceGroupName $rgName -VM $vm

RequestId IsSuccessStatusCode StatusCode ReasonPhrase
--------- ------------------- ---------- ------------
                         True         OK OK
```

* Updating the VM may take a few seconds. Once the command finishes executing, restart the VM as follows:

```PowerShell
Start-AzureRmVM -ResourceGroupName $rgName -Name $vmName
```

Open the VM and start DiskManagment application then right click on the drive and then choose `extend`.

![](2017-01-15_1359.png)
