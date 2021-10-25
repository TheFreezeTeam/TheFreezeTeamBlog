Title: มาใช้ Blazor State บน Blazor App ช่วยให้เราจัดการ State ได้ง่ายขึ้นเยอะเลย
Published: 25/10/2021
Tags: 
  - Blazor
  - C#
Author: Mike Yoshino
Image: BetterUi.webp
Description: สอนการใช้งาน Blazor State กับ Blazor Application โดยละเอียด
Excerpt: สอนการใช้งาน Blazor State กับ Blazor Application โดยละเอียด
---

เพื่อที่จะเข้าใจหลักการทำงานของตัว Blazor-State ผู้ใช้ควรมีความรู้เกี่ยวกับ MediatR, Command Pattern และ Chain of Responsibility Pattern มาก่อนจะเข้าใจง่ายขึ้นเวลาใช้งาน

#Blazor State คืออะไร?

ตัว Blazor-State เป็น State management library ที่ช่วยให้เราจัดการ State ใน Blazor Application ผ่านการใช้ประโยชน์ของ MediatR.

#Blazor State ทำงานยังไง

Blazor State ใช้คลาส Store ซึ่งเป็นที่เก็บค่า State เอาไว้ ซึ่งเราสามารถเพิ่ม State ไปยัง Store ผ่าน MediatR pipeline หรือผ่านส่วนเสริม method ชื่อว่า AddBlazorState

ในการเปลี่ยนค่าหรือเพิ่มค่าลงใน State เราจะสร้าง คลาส Action ขึ้นมาโดยคลาสตัวนี้จะถูกส่งผ่านตัว Mediator โดยแต่ละ Action จะติดกับ Handler ที่ทำหน้าอัพเดทค่า State.

#ประโยชน์ของ Blazor-State

-   ทำให้ Blazor ใช้งาน Middleware pipeline ได้ และ ทำให้เราใช้ประโยชน์จาก Pipeline ได้
-   Persist ค่า state ไว้ได้แม้ว่าจะเปลี่ยนหน้าไปยังหน้าอื่น
-   สามารถใช้กับ Clean Architecture

#ในบทความนี้ผมจะใช้ตัว Blazor Wasm เป็นตัวอย่างนะครับ จริงๆสามารถใช้ได้ทั้ง Server และ Wasm เลย :)

1.  สร้างโปรเจค Blazor Wasm ขึ้นมาครับ โดยปกติโปรเจคจะมี Count มาให้ ผมจะใช้ Blazor State กับ Count เป็นตัวอย่างนะครับ
2.  ทำการเพิ่ม Blazor State ลงไปในโปรเจค หรือติดตั้งผ่าน Nuget Package `dotnet add ./Client/Sample.Client.csproj package Blazor-State`
3.  สร้างโฟเดอร์ Features ขึ้นมาและข้างใน ก็สร้างอีกโฟเดอร์ว่า Counter ซึ่งข้างในโฟเดอร์ Counter สร้างคลาสขึ้นมาชื่อ CounterState.cs ซึ่งเราได้สร้าง State ไว้เก็บค่าของ Count หลักๆเเล้วคลาส State จะต้องเป็น

* เป็น Partial class และ สืบทอดจาก State<T> โดยที่ T ก็คือคลาส State ของเรา
* ต้องมี Method ชื่อ Initialize ซึ่งทำหน้าที่เชทค่าเริ่มต้นให้กับแต่ละ Property ในคลาส State
![BlazorStateThai01](/images/Posts/BlazorStateThai/BlazorStateThai01.png)

ต่อไปเราจะมา Config ตัว Service (Configure the services)

ในไฟล์ ```Program.cs``` เพิ่ม method ```ConfigureServices``` ดังนี้
```public static async Task Main(string[] args)
    {
      var builder = WebAssemblyHostBuilder.CreateDefault(args);
      builder.RootComponents.Add<App>("app");
      builder.Services.AddSingleton
      (
        new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) }
      );

      ConfigureServices(builder.Services);

      await builder.Build().RunAsync();
    }public static void ConfigureServices(IServiceCollection aServiceCollection)
    {

      aServiceCollection.AddBlazorState
      (
        (aOptions) =>

          aOptions.Assemblies =
          new Assembly[]
          {
            typeof(Program).GetTypeInfo().Assembly,
          }
      );
    }
```

#วิธีการเรียกใช้ค่าจาก State

ในโฟเดอร์ Counter สร้างโฟเดอร์ Pages ขึ้นมาและข้างในโฟเดอร์สร้าง Component ชื่อว่า Counter

![BlazorStateThai02](/images/Posts/BlazorStateThai/BlazorStateThai02.png)

โดยใน Counter เรียกใช้ Package Blazor State และสืบทอดจาก BlazorStateComponent
```
@using BlazorState
@inherits BlazorStateComponent
```
ในส่วนของโค๊ตเราจะทำการเรียกค่า CounterState ผ่าน GetState<CounterState>

```
CounterState CounterState => GetState<CounterState>();
```

เสร็จเเล้วก็ทำการรันตัวแอพและไปที่หน้า /Counter
![BlazorStateThai03](/images/Posts/BlazorStateThai/BlazorStateThai03.png)

จะเห็นว่า ค่า Current Count จะมีค่า 3 ซึ่งมาจากการที่เราเช็ทค่าใน Method initialize ใน CounterState ไว้ครับ


#ต่อไปจะเป็นวิธีการส่งอัพเดทหรือเปลี่ยนแปลงค่าใน Blazor State

ซึ่งวิธีการเดียวที่เราจะอัพเดทหรือเปลี่ยนแปลงค่าเราจะต้องกระทำผ่าน Action Class เท่านั้นครับ ตามกฏของ Mediator pattern โดยทุกๆ Action จะมี Handle ที่ทำหน้าที่อัพเดทค่า State

สร้างโฟเดอร์ชื่อว่า Actions ใน Counter ในโฟเดอร์ Actions สร้างโฟเดอร์ตั้งชื่อเกี่ยวกับ Action ที่เราจะใช้ เช่น IncreaseCountNumber

ในโฟเดอร์ IncreaseCountNumber สร้างคลาสขึ้นมาสองคลาสคือ
* IncreaseCountNumberAction.cs
* IncreaseCountNumberHandler.cs

ในคลาส IncreaseCountNumberAction.cs จะเป็นตัวกำหนดค่าที่เราจะส่งไปให้ตัว Handler โดยที่คลาส IncreaseCountNumberAction จะต้องอยู่ใน Partial Class CounterState และ inherit IAction จาก Blazor State

#ตัวอย่าง
![BlazorStateThai04](/images/Posts/BlazorStateThai/BlazorStateThai04.png)

และคลาส IncreaseCountNumberHandler โครงสร้างจะเหมือนกับ Action แต่แทนที่จะสืบทอดจาก IAction เปลี่ยนมาเป็น ActionHandler<T> โดยที่ T คือ คลาส Action ของเรา
 * รับค่าต่างๆจาก Action และอัพเดทไปที่ CounterState
 * เหมือนในตัวอย่างข้างล่างที่ Handle รับค่า Amount มาจาก Action และอัพเดทค่า Amount ไปที่ CountState

#ภาพตัวอย่าง
![BlazorStateThai05](/images/Posts/BlazorStateThai/BlazorStateThai05.png)

เรียกใช้ IncreaseCountNumberAction ใน Counter.razor ผ่าน Mediator ซึ่งเราสามารถส่งค่า Amount ไปที่ Action ได้

![BlazorStateThai06](/images/Posts/BlazorStateThai/BlazorStateThai06.png)

![BlazorStateThai07](/images/Posts/BlazorStateThai/BlazorStateThai07.gif)

เสร็จเเล้ว ที่นี้เราสามารถใช้ Blazor State ใน Blazor App ได้เเล้ว

