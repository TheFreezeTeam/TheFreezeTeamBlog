Title: Change Visual Studio 2015 Default Templates
Tags: 
  - VisualStudio5
  - C# 
  - Template 
Author: Steven T. Cramer

---
I prefer my `using` statements inside my namespace.  But the default class Template for a web application generates like the following:

```csharp
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Tft.FlashCards.Entities
{
  public class Entity
  {
  }
}
```

The template used for a WebClass is located:
`C:\Program Files (x86)\Microsoft Visual Studio 14.0\Common7\IDE\ItemTemplates\CSharp\Code\1033\WebClass\Class.cs`


The default template is:

```csharp
using System;
using System.Collections.Generic;
$if$ ($targetframeworkversion$ >= 3.5)using System.Linq;
$endif$using System.Web;

namespace $rootnamespace$
{
	public class $safeitemrootname$
	{
	}
}
```

I change this to:

```csharp
namespace $rootnamespace$
{
	using System;
	using System.Collections.Generic;
	$if$ ($targetframeworkversion$ >= 3.5)using System.Linq;
	$endif$using System.Web;

	public class $safeitemrootname$
	{
	}
}
```
Stop all Visual Studio Instances and then from an elevated Developer command window execute `devenv /installvstemplates`

Restart VS and now the generated class is:

```csharp
namespace Tft.FlashCards.Entities
{
  using System;
  using System.Collections.Generic;
  using System.Linq;
  using System.Web;

  public class Entity
  {
  }
}
```

