Title: Micro Controllers
Image: MicroControllers.png
Tags: C# MediatR
Author: Steven T. Cramer
---

# ~~Thin Controllers~~ Micro Controllers

When using MediatR and a single controller per endpoint the controller is beyond thin, it is micro.

With a base controller that looks like

```
  public class BaseController<TRequest, TResponse> : Controller
    where TRequest : IRequest<TResponse>
    where TResponse : BaseResponse
  {
    private IMediator _mediator;

    protected IMediator Mediator => _mediator ?? (_mediator = HttpContext.RequestServices.GetService<IMediator>());

    protected virtual async Task<IActionResult> Send(TRequest aRequest)
    {
      TResponse response = await Mediator.Send(aRequest);

      return Ok(response);
    }
  }
```

The specific controller becomes nothing more than a place to define your route, http verb and the type of the request.

```
  [Route("api/weatherForecast")]
  public class GetWeatherForecastsController : BaseController<GetWeatherForecastsRequest, GetWeatherForecastsResponse>
  {
    public async Task<IActionResult> Get(GetWeatherForecastsRequest aRequest) => await Send(aRequest);
  }
```

See these micro controllers utilized in the `timewarp-blazor` [template](https://timewarpengineering.github.io/blazor-state/docs/Template/TemplateOverview.html)


References:  
[Jimmy Bogard put your controllers on a diet](https://lostechies.com/jimmybogard/2013/10/10/put-your-controllers-on-a-diet-redux/)  
[Steve Ardalis Smith](https://github.com/ardalis/CleanArchitecture)  
[Jason Taylor](https://www.youtube.com/watch?v=_lwCVE_XgqI)


Tags: dotnetcore, MediatR, Mediator

