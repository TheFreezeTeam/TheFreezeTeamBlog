Title: The FlatList Cometh
Published: 02/06/2019
Tags:
  - React-Native
  - Javascript
Author: Stefan Bemelmans
Image: flatlist.jpg
Description: I needed to render a list from a variable-length array of objects. Each object contained a text name and a corresponding number value
Excerpt: I needed to render a list from a variable-length array of objects. Each object contained a text name and a corresponding number value
---
# The FlatList Cometh

or How I implemented the FlatList Component in React-Native

This is my first official blog post as a member of The Freeze Team. I'll do my best to keep it simple and straightforward.  I am going to recount my experience implementing a FlatList Component in React Native. 

I needed to render a list from a variable-length array of objects. Each object contained a text "name" and a corresponding number "value" and would be expressed in the list in a styled component I had created that accepts a name and value prop.

In the past I have used  a `ScrollView` and used a mapping function to render the list of components.  The performance benefits of the FlatList were worth experimenting with as this particular list in question would probably not be very long, 4-10 items or so a `ScrollView` would not be necessary most of the time.
Flatlist is cool because it does the rendering for you and renders the items as they are needed on the screen instead of all at once which is the case of the ScrollView. You provide the FlatList an array of data and what the items should look like and it runs it through it's own mapping function. Checkout this very basic example from the docs. 


```Html    
<Flatlist   data={[{key:'a'},{key:'b'}]}  renderItem={({item})=><Text>{item.key} 
</Text>}/>
```

If you recall above, my 'item' had 2 properties, so my finished FlatList looks something like this: 

```Html
<Flatlist 
    data={this.props.objectArray}
    renderItem={ item => {
    <CustomComponent
      label={item.value}
      name={item.name}
      onPress={this.onPress}
    />
/>
```

Easy Peasey, and a side note. If you `console.log` the contents of the `item`, as I did, depending on the length of the list the console's may negate the performance benefit.

Thanks for reading! 

