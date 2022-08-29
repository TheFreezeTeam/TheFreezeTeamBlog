---
DocumentName: three-ways-to-create-react-stateless-component
Title: Three ways to create React Stateless Components in TypeScript
Published: 01/19/2017
Tags: 
  - Typescript 
Author: Steven T. Cramer
Excerpt: The Airbnb guidelines show three ways to implement a stateless React component using ES6 JavaScript. 
Description: The Airbnb guidelines show three ways to implement a stateless React component using ES6 JavaScript. 
---

The Airbnb [guidelines](https://github.com/airbnb/javascript/tree/master/react#class-vs-reactcreateclass-vs-stateless) show three ways to implement a stateless React component using ES6 JavaScript. They make recommendations on which to use but this post is just to show the corresponding TypeScript equivalents.

In TypeScript we will define an interface for the props as:

```typescript
interface ListingProps {
    hello: string;
};
```

#### Class based component:

* JavaScript

```javascript
class Listing extends React.Component {
  render() {
    return <div>{this.props.hello}</div>;
  }
}
```
* TypeScript
```typescript
class ListingClassComponent extends React.Component<ListingProps, object> {
    render(): JSX.Element {
        return(
            <div>{this.props.hello}</div>
        );
    }
};
```
#### Arrow Function Component

* JavaScript

```javascript
const Listing = ({ hello }) => (
  <div>{hello}</div>
);
```

* TypeScript

```typescript
const ListingArrowComponent: React.StatelessComponent<ListingProps> = props => 
(
    <div>{props.hello}</div>
);
```

#### Normal Function Component

* JavaScript

```javascript
function Listing({ hello }) {
  return <div>{hello}</div>;
}
```
* TypeScript
```typescript
function ListingFunctionComponent(props: ListingProps): JSX.Element {
    return (
        <div>{props.hello}</div>
    );
};
```
#### Simple example of all three with an event handler in props

```typescript
import * as React from 'react';

export interface ButtonProps extends React.Props<HTMLDivElement> {
    text: string;
    onClick: React.EventHandler<React.MouseEvent<HTMLButtonElement>>;
};

export class ButtonClassComponent extends React.Component<ButtonProps, object> {
    render(): JSX.Element {
        const {onClick, text} = this.props;
        return(
            <div>
                <button onClick={onClick}>{text}</button>
            </div>
        );
    }
};

export function ButtonFunctionComponent(props: ButtonProps): JSX.Element {
    return (
        <div>
            <button onClick={props.onClick}>{props.text}</button>
        </div>
    );
};

export const ButtonConstComponent: React.StatelessComponent<ButtonProps> = props => 
(
    <div>
        <button onClick={props.onClick}>{props.text}</button>
    </div>
);
```
