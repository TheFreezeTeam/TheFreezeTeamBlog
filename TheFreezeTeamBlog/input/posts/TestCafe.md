Title: TestCafe vs Cypress
Published: 08/20/2019
Tags: CSharp
Author: Steven T. Cramer
Excerpt: If you like closures use Cypress otherwise us TestCafe
Url: TheFreezeTeam.com/testcafe
GUID: 034b3ec1-48bf-4c20-b632-7791f0fbfd4f

---
Selenium is causing my CI/CD to break this weekend because the process won't properly shutdown,
and over all the tool is old and doesn't appear to ever be going to significantly improve.
So once again I set out to see if I can find a replacement.
There are two other choices that I looked at back in my "Pizza Pizza" days.
Cypress and TestCafe, which I will revisit.

## Cypress
I started with cypress.
All looks cool. It has nice features that show step by step whats happening in the test.
One can record a test with a browser plugin.
I was thinking this is gonna be the winner.

And then I looked at the test code.
For some reason they think closures are a good way to capture variables.
I **STRONGLY** disagree.
The example they show of "the wrong way to do it" is exactly how I attempted.
Others must have attempted also as they felt the need to show the wrong way before the "right" way.

```
// ...this won't work...

// nope
const button = cy.get('button')

// nope
const form = cy.get('form')

// nope
button.click()
```

For some reason they think the following is better.

```
cy.get('button').then(($btn) => {

  // store the button's text
  const txt = $btn.text()

  // submit a form
  cy.get('form').submit()

  // compare the two buttons' text
  // and make sure they are different
  cy.get('button').should(($btn2) => {
    expect($btn2.text()).not.to.eq(txt)
  })
})
```

Straight from their docs it says:
"If you’re familiar with native Promises the Cypress .then() works the same way. You can continue to nest more Cypress commands inside of the .then()."

> **They are not real Promises** so if you are thinking in Typescript you can write async await to clean it up... you would be wrong.

My response, **"NO THANK YOU!"**

For the life of me I can't understand why this has become popular.

```
then().then().then().then()...
```

We already have a this functionality.  Its called top to bottom....
```
 Line1
 Line2
 Line3
 ```
 All done without any `then` or heavily nested code.

## TestCafe

After finding the closure paradigm in Cypress, I decided to give TestCafe a shot.

And let's just say the coding paradigm is much better.

```
import { Selector } from 'testcafe';

fixture `TestApp`
    .page `https://localhost:5001/`;
    
test('Counter Should Count', async t => {
    await t
        .click(Selector('a').withText('Counter'));

    const Button1 = Selector('[data-qa="Counter1"]').find('button');
    const Button2 = Selector('[data-qa="Counter2"]').find('button');
    const CounterDisplay1 = Selector('[data-qa="Counter1"]').find('p');
    const CounterDisplay2 = Selector('[data-qa="Counter2"]').find('p');

    await t
        .expect(CounterDisplay1.textContent).eql("Current count: 3")
        .expect(CounterDisplay2.textContent).eql("Current count: 3")
        .click(Button1)
        .expect(CounterDisplay1.textContent).eql("Current count: 8")
        .expect(CounterDisplay2.textContent).eql("Current count: 8")
        .click(Button2)
        .expect(CounterDisplay1.textContent).eql("Current count: 13")
        .expect(CounterDisplay2.textContent).eql("Current count: 13")
        .click(Button1)
        .expect(CounterDisplay1.textContent).eql("Current count: 18")
        .expect(CounterDisplay2.textContent).eql("Current count: 18");
});
```

Notice how you can actually store a selector for later use without a closure and that they use async await syntax.

TestCafe is also free if you want to write code by hand.
They have a paid version that will automate some of that with a recorder that is quite nice and it will generate the js files for you and really help with the selectors and test values, all in a nice GUI.

### How about Typescript?

"TestCafe bundles the TypeScript declaration file with the npm package, so you do not need to install it separately."

"TestCafe automatically compiles TypeScript before running tests, so you do not need to compile the TypeScript code."

[docs](https://devexpress.github.io/testcafe/documentation/test-api/typescript-support.html)

### TestCafe from CLI as part of my CI/CD

Here is my whole package.json

```
{
  "name": "test-app.end-to-end.test-cafe.tests",
  "scripts": {
    "test": "testcafe edge tests/"
  },
  "devDependencies": {
    "testcafe": "^1.4.1"
  }
}
```

to run the tests I can use 

```
npm test
```
or

```
testcafe edge tests/
```
[Cli Docs](https://devexpress.github.io/testcafe/documentation/using-testcafe/command-line-interface.html)

Oh and TestCafe supports multiple browsers.

So TestCafe wins for me as it gives me more superpowers for the effort.

>**Don't forget a good superhero keeps their code clean and tested.**
