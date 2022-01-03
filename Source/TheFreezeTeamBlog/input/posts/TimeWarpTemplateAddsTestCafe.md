DocumentName: TimeWarpTemplateAddsTestCafe
Title: TimeWarp-Blazor Template adds TestCafe
Published: 08/27/2019
Tags: CSharp Blazor
Author: Steven T. Cramer
Excerpt: We have added an End to End testing project using TestCafe as an option to existing Selenium test project.
Description: We have added an End to End testing project using TestCafe as an option to existing Selenium test project.
---

Today we shipped a new updated timewarp-blazor template that now includes a TestCafe End-To-End test project.

Selenium is causing my CI/CD to break this weekend because the process won't properly shutdown, and over all the tool is old and doesn't appear to ever be going to significantly improve.
So once again I set out to see if I can find a replacement.

## TestCafe

I decided to give TestCafe a shot.

The code is written in TypeScript and is supported without any extra effort.
Here 

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
