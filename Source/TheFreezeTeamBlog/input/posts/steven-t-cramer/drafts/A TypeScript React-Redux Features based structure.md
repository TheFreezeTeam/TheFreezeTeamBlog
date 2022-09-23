Title: A TypeScript React-Redux Features based structure
Tags: 
  - C# 
  - Blazor 
  - dotnet 
  - Blazor-State
Author: Steven T. Cramer
Excerpt: ReduxDevTools off by default. 
Published: 03/12/2099
---

# A TypeScript React-Redux Features based structure

Directories under the src folder are:

* `components`
* `features`
* `higherOrderComponents`
* `routes`
* `types`

## `components` Folder
The `components` folder is the collection of TypeScript files used to create a styled react component.  For each component the folder name should begin with an uppercase letter and is the component name. The folder can contain the following:


* `index.ts`
* `compose.ts`
* `<FolderName>Component.tsx`
* `<FolderName>Container.tsx`
* `styles.ts`
* `messages.ts`
* other supporting files.

In React we often build components from child components.  If a component itself has any child components they will go in a directory called `components` and the child component will have its own folder. This repeats recursively as necessary.  If you would like to reuse a component then elevate that component up the structure as needed.

### `index.ts`
The `index.ts` is an index to the folder and its children if it has any.  Just like the index of a book, it **does not contain** the text of the book but is a reference to the items in the book.  In our case this is the public interface of the folder.  If anything is to be used by something outside this folder it should be exported via index.  Thus all imports should only reference a folder and NOT specific files inside the folder, and surely not deep into the child directories.

Example `index.ts`
```
export { AboutComposed as About } from './compose';
export { messages } from './messages';
```
### `<FolderName>Component.tsx`
The `<FolderName>Component.tsx` is a React Component. It is NOT connected to the Redux store. Nor is it wrapped with HOCs. This is a presentation component only.

If the Component is going to be wrapped with HOCs or is going to be connected to the Redux store then the props should be declared breaking each source down to a separate interface.

```
type Props = OwnProps & StateProps & DispatchProps & WitnI18nProps;
```

### `<FolderName>Container.tsx`

If the component is connected to the Redux store this is done in a separate file. The mapStateToProps and mapDispatchToProps names should be used and passed into connect.  The methods and the result should be strongly typed.  All actions should be bound to a single collection names `actions` similar to:

```
import { connect } from 'react-redux';
import { bindActionCreators, Dispatch } from 'redux';

import { State } from 'state';

import { actionCreators, FeatureAction, selectors } from 'features/settings';

import { DispatchProps, SettingsComponent, StateProps } from './SettingsComponent';

export const mapStateToProps = (state: State): StateProps => (
  {
    settings: selectors.settingsSelector(state),
  });

export const mapDispatchToProps = (dispatch: Dispatch<FeatureAction>): DispatchProps => ({
  actions: bindActionCreators(
    actionCreators,
    dispatch,
  ),
});

export const SettingsContainer: React.ComponentClass<object>  = connect(
  mapStateToProps,
  mapDispatchToProps,
)(SettingsComponent);

```

### `styles.ts`
The `styles.ts` is simply to put the css styling into a separate file.  This could be done in the component file itself,  yet I find this allows for easier merges and code reviews.  The UX professional can style these files and when checking them in the reviewer knows they are exclusively styling.  The styles should all be local and exported via a single object named styles similar to:

```
export const styles = {
  storeDetailsWrapper,
  detailsWrapper,
  descriptionDiv,
  h1,
  sources,
  addressWrapper,
  addressContainer
};
```

### `compose.ts`
Often in React to add functionality to a component we create higher order components that take the original component and add some functionality to it.  For example our `withIntl` HOC adds the `WithI18nProps` to the Props of any component.  Sometimes there are multiple HOCs adding functionality to the basic presentation component.  This compose.ts is a file that builds up the result of the HOCs.  By putting this into a file by itself it makes it easy to see which HOCs are being used to build this component.

No code other than the composition of the pieces should be in this file.  If a function takes parameters those should be imported and named with a meaningful name that attempts to keep with the single responsibility principle.

For example we have a withValidationCheck Hoc that takes 3 parameters.  OriginalComponent, validationRules and intialState.

The segment in `compose.ts` for this should look similar to the following

```
import { initialState, validationRules } from './validation';

const MyComponentComposed = withValidationCheck(
  MyComponent,
  validationRules, 
  initialState
```

The final assembly (composed component) should be exported and named `<FolderName>Composed`

```
export const AboutComposed =  withIntl(messages)(AboutComponent);
```

### `messages.ts`

The messages.ts file should export `keys`, `messages` and `messageNamespace`.

## `Features` Folder
The `features` folder is the collection of TypeScript files used to manage a portion of state in the Redux store.  
It consists of the following:

* `actionCreators.ts`
* `actionNames.ts`
* `Actions.ts`
* `index.ts`
* `logic.ts`
* `reducer.ts`
* `selectors.ts`
* `State.ts`
* optional other supporting files

### `State.ts`
Here we define the `State` structure this feature is managing. And define the initialState used to initialize the store.

Example:
```
import { Settings } from 'types';

export type State = Settings;

export const initialState: State = {
  googleMapsApiKey: undefined
};
```
Often, like above, the State we are managing is defined in the types directory to be shared across the project.

### `actionCreator.ts`
This file contains all of the individual action creators used by this feature which are exported as a collection named `actionCreators`.  An action creator is a factory method that creates an Action object (see below).

Example:
```
import { actionNames } from './actionNames';
import { Actions } from './Actions';
import { State } from './State';

const start = (): Actions.StartAction => ({
  type: actionNames.start,
});

const success = (settings: State): Actions.SuccessAction => ({
  payload: settings,
  type: actionNames.success,
});

const cancel = (): Actions.CancelAction => ({
  type: actionNames.cancel,
});

const failure = (error: string): Actions.FailureAction => ({
  type: actionNames.failure,
  error
});

export const actionCreators = {
  start,
  success,
  cancel,
  failure,
};
```

### `Actions.ts`
This file defines the action types used by the feature. Given they are all "types" the `A` is capitalized. All of these types are exported via exporting the single namespace Actions.

This file also defines a union type named `FeatureAction` of all actions this Feature supports.  

Example:
```
import { Action } from 'redux';

import { State } from './State';

import { ActionNameTypes } from './actionNames';

export namespace Actions {
  export interface StartAction extends Action {
    type: ActionNameTypes.Start;
  }

  export interface SuccessAction extends Action {
    type: ActionNameTypes.Success;
    payload: State;
  }

  export interface FailureAction extends Action {
    type: ActionNameTypes.Failure;
    error: string;
  }

  export interface CancelAction extends Action {
    type: ActionNameTypes.Cancel;
  }
}

export type FeatureAction =
  Actions.StartAction |
  Actions.SuccessAction |
  Actions.FailureAction |
  Actions.CancelAction;
```

### `actionNames.ts`
This file exports 2 things. The first exported item is `actionNames` which is a collection of all the action names defined in the file.

The second exported item is a namespace `ActionNameTypes` that contains all of the Action name types defined in this file.

```
export namespace ActionNameTypes {
  export type Login = 'Account/Login';
  export type Logout = 'Account/Logout';
}

const login: ActionNameTypes.Login = 'Account/Login';
const logout: ActionNameTypes.Logout = 'Account/Logout';

export const actionNames = {
  login,
  logout
};
```
The use of the namespace is to accomplish a consistent naming of exported items and grouping them together. Very similar to the `actionNames` collection.

The string values used to name actions are created using the following pattern:
 `<FeatureName>/<ActionName>` in Pascal case. The prefix is to avoid potential name clashes.

### `index.ts`
The `index.ts` is the same concept as in the Component folder above and will look similar to:

Example:
```
export { actionCreators } from './actionCreators';
export { Actions, FeatureAction } from './Actions';
export { actionNames, ActionNameTypes } from './actionNames';
export { logic } from './logic';
export { reducer } from './reducer';
export { selectors } from './selectors';
export { initialState, State } from './State';
```

### `reducer.ts`
This is a normal reducer file using standard switch on type. We export the single reducer constant.  If actions from other Features are to be reduced then they would be imported here 

Example:
```
import { Reducer } from 'redux';

import { actionCreators as defaultActionCreators, Actions as DefaultActions, } from 'features/default';

import { actionNames } from './actionNames';
import {  Actions, FeatureAction } from './Actions';
import { initialState, State } from './State';

type ReducerActions = FeatureAction | DefaultActions.DefaultAction;

export const reducer: Reducer<State> = (
  state: State = initialState,
  action: ReducerActions = defaultActionCreators.defaultAction()
) => {
  switch (action.type) {
    case actionNames.start:
      return state;
    case actionNames.success:
      const successAction = action as Actions.SuccessAction;
      return successAction.payload;
    case actionNames.failure:
      return state;
    case actionNames.cancel:
      return initialState;
    default:
      return state;
  }
};

```

### `selectors.ts`
This is where the Feature exposes queries to fetch data from the state.  If we always access state via a selector this gives performance improvements and flexibility when refactoring.

Typically we have a selector that gets the top data structure for the feature.  Then we have a selector for every piece of state we would like to expose.

We export a single object that contains all the selectors.

```
import { createSelector, Selector } from 'reselect';

import { State as RootState } from 'state';

import { State as FeatureState } from './State';

const settingsSelector = (state: RootState): FeatureState =>  state.settings;

const googleMapsApiKeySelector: Selector<RootState, string | undefined> = createSelector(
  settingsSelector,
  (settings: FeatureState): string | undefined => settings && settings.googleMapsApiKey
);

export const selectors = {
  settingsSelector,
  googleMapsApiKeySelector,
};

```



