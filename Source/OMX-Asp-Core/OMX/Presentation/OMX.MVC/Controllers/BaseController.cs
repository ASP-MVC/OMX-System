﻿namespace OMX.MVC.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    public abstract class BaseController : Controller
    {
        // private IMediator _mediator;

        // protected IMediator Mediator => _mediator ?? (_mediator = HttpContext.RequestServices.GetService<IMediator>());
    }
}
