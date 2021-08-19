# By the Power of LinqPad 

## Join and Count in Linq


```csharp
string ownerOjbectId = "88c0ebde-f619-43b9-a942-6cc08a154b31";

var total = Tenants
	.Join
	(
		TenantCustodianWallets,
		aTenant => aTenant.TenantId,
		aTenantCustodianWallet => aTenantCustodianWallet.TenantId,
		(aTenant, aTenantCustodianWallet) => new { OwnerObjectId = aTenant.OwnerObjectId }
	)
	.Where(aItem => aItem.OwnerObjectId == ownerOjbectId)
	.Count();

x.Dump();
```


# References

https://stackoverflow.com/questions/890381/how-to-count-rows-within-entityframework-without-loading-contents

https://stackoverflow.com/questions/3217669/how-to-do-a-join-in-linq-to-sql-with-method-syntax
