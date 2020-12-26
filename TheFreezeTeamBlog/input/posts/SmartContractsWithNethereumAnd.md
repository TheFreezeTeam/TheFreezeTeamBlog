Title: Superhero smart contracts.
Published: 01/01/2099
Tags: Solidity Truffle TruffleSuite Nethereum Ethereum SmartContract
Author: Steven T. Cramer
Excerpt: 
Url: TheFreezeTeam.com/superhero-smart-contracts
GUID: 5372816a-38d8-4a30-99f0-38bf5b11fb1b

---

![][SuperheroSmartContractsImage]

In the new world of crypto currencies, we super hero's, need to be ever vigilant in maintaining our health and mental acuity.  Rarely will I say,  "Truffles will help" but when it comes to testing our "Smart Contracts" a few sweets/suite hit the spot.

[TruffleSuite](https://www.trufflesuite.com/) and [Nethereum](https://nethereum.com/)

Nethereum is an open source cross platform .NET integration library for the Ethereum blockchain. [^1]

TruffleSuite is a Smart Contract development platform consisting of: [^2]
* Truffle - "A world class development environment, testing framework and asset pipeline for blockchains using the Ethereum Virtual Machine (EVM), aiming to make life as a developer easier." 
* Ganache - "A personal blockchain for Ethereum development you can use to deploy contracts, develop your applications, and run tests. It is available as both a desktop application as well as a command-line tool (formerly known as the TestRPC). Ganache is available for Windows, Mac, and Linux." 
* Drizzle = "A collection of front-end libraries that make writing dapp front-ends easier and more predictable. The core of Drizzle is based on a Redux store, so you have access to the spectacular development tools around Redux. We take care of synchronizing your contract data, transaction data and more." 

We are going to use Nethereum with Ganache in this walk through.

# Install Ganache (Local Private ETH Blockchain for testing)

Start Ganache UI and create a new workspace.

![TruffleStartScreen](input\images\SmartContracts\TruffleStartScreen.png)
Add the smart contracts truffle-config.js to the project in the settings.
![TruffleProjectSettings](input\images\SmartContracts\TruffleProjectSettings.png)

# The Smart Contract code (Solidity)

The smart contract isn't that smart but serves as a good example.

```solidity
pragma solidity ^0.5.0;

contract MultiplyContract {
    uint256 _multiplier;

    constructor(uint256 multiplier) public {
        _multiplier = multiplier;
    }

    function multiply(uint256 a) public view returns (uint256 d) {
        return a * _multiplier;
    }
}
```
truffle compile
truffle migrate

# The Nethereum Code. (C#)






>**Don't forget a good superhero keeps their code clean and tested.**

<!---
## Mentorship

If you are tired of coding alone come join us at the [FreezeTeam](https://twitter.com/TheFreezeTeam1).

-->

#### Footnotes:

[^1]: https://nethereum.com/, 2019-08-15

[^2]: https://www.trufflesuite.com/, 2019-08-15

[^3]: Here's one with multiple paragraphs and code.

    Indent paragraphs to include them in the footnote.

    `{ my code }`

    Add as many paragraphs as you like.

[SuperheroSmartContractsImage]: /../images/SuperheroSmartContractsImage.png "SuperheroSmartContractsImage"