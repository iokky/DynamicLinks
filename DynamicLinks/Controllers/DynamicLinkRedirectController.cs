﻿using DynamicLinks.Domain.Entity;
using DynamicLinks.Domain.Requests;
using DynamicLinks.Services;
using Microsoft.AspNetCore.Mvc;
using Ng.Services;

namespace DynamicLinks.Controllers
{
    [Route("/")]
    [ApiController]
    public class DynamicLinkRedirectController : ControllerBase
    {
        private readonly IRedirectService _redirectHandlerService;
        private readonly UserAgentService _userAgent;

        public DynamicLinkRedirectController(IRedirectService redirectHandlerService)
        {
            _userAgent = new UserAgentService(new UserAgentSettings()
            {
                CacheSizeLimit = 20000,
                CacheSlidingExpiration = TimeSpan.FromDays(1),
                UaStringSizeLimit = 256,
            });
            _redirectHandlerService = redirectHandlerService;

        }

        //For Debug only
        [HttpGet("headers")]
        public IActionResult GetHeaders()
        {
            return Ok(Request.Headers);
        }


        [HttpGet("{shortLink}")]
        public IActionResult RedirectHandler([FromRoute] string shortLink)
        {

            var data = _userAgent.Parse(Request.Headers["User-Agent"].ToString());
            var link = _redirectHandlerService.GetLinkResponse(new GetLinkRequest()
            {
                Mobile = data.IsMobile,
                Platform = data.Platform,
                ShortLink = shortLink,
            });

            return Redirect(link.Url);
        }

        [HttpGet("/ss")]
        public IActionResult CreateLinkHandler([FromBody] DynamicLinkEntity link)
        {
            
            return Ok();
        }
    }
}
