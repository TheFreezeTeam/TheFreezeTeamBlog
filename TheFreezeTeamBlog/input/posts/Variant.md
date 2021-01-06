Title: Generic Variance in Superheros
Published: 08/15/2019
Image: noimage.jpg
Tags: CSharp
Author: Steven T. Cramer
Excerpt: Co ends with o use out.  Con ends with n which sounds like in
GUID: 5372816a-38d8-4a30-99f0-38bf5b11fb1b
---

![][VariantGenericInterfacesImage]

A good superhero needs to learn from their ancestors (Contravariant), and keep in mind future generations (Covariant).  Here is some guidance on how to remember and use our generic variance to overcome our LIQUID foes.

## Generic Variance in C#

**variant interface**: A generic interface that has covariant or contravariant generic type parameters is called variant.

> **Memory Mnemonic**:  
> `Co` ends with `o` use `out`  
> `Con` ends with `n` which sounds like `in`

> **Memory Mnemonic 2**:  
> Your ancestors are the inputs that made you. `in`  
> Your descendants are you outputs. `out`

- Invariant - only the class specified
- Covariant - the class specified or any descendant class
- Contravariant - the class specified or any ancestor class

```csharp
interface IFoo<T>;     // Invariant
interface IFoo<out A>; // Co-Variant only outputs can use A (ancestors)
interface IFoo<in D>;  // Contra-Variant only inputs can use (descendants)
```

```csharp
// only inputs can use A 
 
interface IContravariant<in A>
{
    void SetSomething(A sampleArg);
    void DoSomething<T>() where T : A;
    // The following statement generates a compiler error.
    // A GetSomething();
}
```
```csharp
// * only outputs can use R
 
interface ICovariant<out R>
{
    R GetSomething();
    // The following statement generates a compiler error.
    // void SetSomething(R sampleArg);

}
```
## Common **Covariant** and **Contravariant** interfaces you use in dotnet.

[IEnumerable<>](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1)
[IComparer](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.icomparer-1)

```csharp
public interface IEnumerable<out T> : System.Collections.IEnumerable;
public interface IComparer<in T>;
```

See the [documentation](https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/concepts/covariance-contravariance/creating-variant-generic-interfaces#declaring-variant-generic-interfaces) for more details.

>**Don't forget a good superhero keeps their code clean and tested.**

<!---
## Mentorship

If you are tired of coding alone come join us at the [FreezeTeam](https://twitter.com/TheFreezeTeam1).

-->

[VariantGenericInterfacesImage]: /../images/VariantGenericInterfaces.png "VariantGenericInterfaces"