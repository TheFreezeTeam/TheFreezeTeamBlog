Title: Blazor-State
Published: 08/14/2021
Image: blazor-state.png
Tags: 
  - Blazor
Author: Ilyana Smith
Description: Blazor-State is an architecture designed to manage state in Blazor applications utilizing MediatR.
Excerpt: Blazor-State is an architecture designed to manage state in Blazor applications utilizing MediatR.
---

## Background

To best understand Blazor-State, you should be familiar with Blazor, MediatR, the Command Pattern, and the Chain of Responsibility Pattern.

### Blazor

Blazor is a framework that allows programmers to write client-side code in C#. Blazor provides near-native performance and requires no plugins, because it runs on top of WebAssembly. You can learn more about Blazor [here](https://ilyana.dev/blog/2021-02-21-blazor/).

### MediatR

[MediatR](https://github.com/jbogard/MediatR) is an implementation of the Mediator Pattern. The Mediator Pattern decouples objects by providing a layer through which they can communicate. A widely-used analogy to the Mediator Pattern is an air traffic controller. Instead of every plane (think object) having to talk to every other plane (object) to ensure they don't crash into one another, the planes (objects) talk to the air traffic controller (the mediator), who can then give relevant information ("Don't move onto the runway yet, Plane A! Plane B is landing right now!") to the other planes (objects). For a more in-depth explanation of the Mediator Pattern, see [this article](https://www.geeksforgeeks.org/mediator-design-pattern/).

### The Command Pattern

In the Command Pattern, clients are decoupled from commands by representing actions as objects which implement a `Command` interface. This interface requires an `execute()` method. This pattern is beneficial because it allows an application to make queueable requests. You can learn more about the Command Pattern [here](https://ilyana.dev/blog/2020-08-20-command-pattern/).

### The Chain of Responsibility Pattern (aka Pipeline)

In the Chain of Responsibility Pattern, the sender of a message sends its message to the first link in a chain of handlers. That first link can choose to either handle the message or pass it along to the second link in the chain. That second link can make the same decision, and so on down the line, until the message is either handled or the end of the chain is reached. You can learn more about the Chain of Responsibility Pattern [here](https://ilyana.dev/blog/2020-08-27-chain-of-responsibility-pattern/).

## Overview

[Blazor-State](https://github.com/TimeWarpEngineering/blazor-state) is an approach to managing state in Blazor.

## How it Works

Blazor-State implements a `Store` which has a collection of `State`. Behaviors can be registered via the MediatR pipeline and the extension method `AddBlazorState` provided by Blazor-State.

When an Action is triggered in the view, it is sent to the Mediator, which sends the Action down the Chain of Responsibility. A Handler in the Chain of Responsibility handles the Action and updates the State. Then the View updates with the updated State. This is known as unidirectional data flow.

## Use Cases

Blazor-State can be used in Blazor WebAssembly Apps in need of state management, particularly for the purposes of updating Views based on State.

## Getting Started

To learn the basics of Blazor-State, follow this [tutorial](https://timewarpengineering.github.io/blazor-state/Tutorial.html), or follow along with the video below.

[![Blazor-State Tutorial](/img/blazor-state-tutorial.png)](https://www.youtube.com/watch?v=TkgYj8BnMQM "Blazor-State Tutorial")

### Useful Links

As you go through the Getting Started video, you'll want to have the following links handy:

- [Tutorial](https://timewarpengineering.github.io/blazor-state/Tutorial.html)
- [Redux DevTools](https://chrome.google.com/webstore/detail/redux-devtools/lmhkpmbekcpmknklioeibfkpmmfibljd?hl=en)

## Terminology

Blazor-State uses three terms which can sometimes cause confusion due to similar concepts used in other contexts. For clarity, here are the definitions of those terms as used by Blazor-State:

- **Action:** An action is the same as the concept of a "command" in the Command Pattern. An action is handled on the Client.
- **Request:** A request is exactly the same as an action, except it is handled on the Server.
- **Handler:** A Handler is the code that processes an action or request and returns a response. You may also know this concept as a "reducer" (from Redux) or "executor" (from the Command Pattern)

## Resources

To learn more, explore the following resources:

- [Blazor-State documentation](https://timewarpengineering.github.io/blazor-state/Overview.html)
- [Blazor-State GitHub site](https://github.com/TimeWarpEngineering/blazor-state)
- [Blazor Basics](https://ilyana.dev/blog/2021-02-21-blazor/)
- [MediatR GitHub site](https://github.com/jbogard/MediatR)
- [Mediator Pattern article](https://www.geeksforgeeks.org/mediator-design-pattern/)
- [Command Pattern Overview](https://ilyana.dev/blog/2020-08-20-command-pattern/)
- [Chain of Responsibility Patter Overview](https://ilyana.dev/blog/2020-08-27-chain-of-responsibility-pattern/)
- [Unidirectional Data Flow Overview](https://flaviocopes.com/react-unidirectional-data-flow/) (this article is specific to React, but does a good job of explaining the concept in a general sense)
