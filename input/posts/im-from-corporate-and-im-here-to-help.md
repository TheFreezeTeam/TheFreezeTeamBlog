Title: I'm from Corporate and I'm here to Help!
Published: 02/26/2019
Tags: 
  - React-Native 
  - Javascript 
  - NodeJS 
Author: Stefan Bemelmans
Image: 010.jpg
Description: There's a certain threshold of size and complexity that if you keep on the right side of you can assuredly never be bothered with how your files are arranged. 
Excerpt: There's a certain threshold of size and complexity that if you keep on the right side of you can assuredly never be bothered with how your files are arranged. 

---
When I first learned web development I was focused more on the content of the files and not necessarily the where and the why the files were in the places they were. This worked out for a little while. There's a certain threshold of size and complexity that if you keep on the right side of you can assuredly never be bothered with how your files are arranged. 
 In my case, things progressed onward, upward and outward.  At the time in question my app included React Redux, multiple state sections or "user flows" with images, inputs, modals and multiple api calls that all had to work in concert and look good doing it. I started to get the feeling that the structure of my app was hindering my production. The Structure I was working with was something like this:
 ```
       .App.js
       /store/
       /reducers/
           /reducerOne/
           /reducerTwo/
           /reducerThree/
       /actions/
           actionNames.js
           /actionOne/
           /actionTwo/
           /actionThree/
       /assets/
           giantStyleFile.js
           ...bunch of images
       /screens/
           screenOne.js
           ...bunch of screens
```   

    This was roughly taught to me at the webDev bootcamp I attended and up until now had proved adequate. 
However with the growth in size and complexity, the threshold was crossed and it started to take a little too long for my compulsively, chronologically aware, and slightly paranoid self to be able to locate and navigate to, for example,  the style for the image on some of the later added screens. Or to find a particular action that manipulates some data that is supposed to be rendered on some screen so I can move it a little to the left. The pain probably could've been eased slightly if I was fluent in the hotkey's of the IDE, but I chose the path of least resistance as I recalled some distant instruction from the bootcamp I attended that went something like: "If you find yourself importing stuff like this: '../../../../../../theThing', you may want to think of restructuring your files.",  and that's exactly where I had ended up. Under the tutelage of my mentor, a veteran first generation programmer, I learned how to arrange things in a way that facilitates development ease and is significantly more intuitive. 

```
       .App.js
       /store/
           redux store 
       /reducers/
           here is the aggregation of all the reducers and export of rootReducer
       /styles/
           some global styles here
       /Navigation/
           here the aggregation of the various navigators and export of rootNavigator
       /features/
           /UserFlowOne/
               userFlowNavigation.js
               /screens/
                   userFlowScreenOne.js
                   ...more UserFlowOne Screens
               /components/
                   here are components specific to this user flow, 
               /userFlowOneActionNames/
               /userFlowOneActionCreators/
               /userFlowOneReducers/
               /styles/
                   here are styles specific to this user flow 
               /assets/
                   here are image assets specific to this user flow
           /UserFlowTwo/
            ...as above
    ```   

    Once I got over the initial shock of the new structure I found it much easier to work with and all of the imports weren't much farther away than '../'! It's also consistent, in the app in question I have 5 trees in the 'features' directory and it's growing. But no matter which tree i'm working in everything is in the same place. This speeds up familiarity with the code-base for newcomers as well as productivity in general. 
YMMV depending on the structure of your app but If you are starting to notice long time spent hunting for files I'd recommend restructuring. I'm finding it a process and am still puzzling over how to handle some aspects of global styling, but overall the experiences has been a significantly positive one.

