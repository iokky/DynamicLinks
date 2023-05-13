using DynamicLinks.Dal.Repositories.Interfaces;
using DynamicLinks.Domain.Entity;
using DynamicLinks.Domain.Requests;
using DynamicLinks.Services;
using Microsoft.AspNetCore.Mvc;
using Ng.Services;
using System.Diagnostics;

namespace DynamicLinks.Controllers
{
    [Route("/")]
    [ApiController]
    public class DynamicLinkRedirectController : ControllerBase
    {
        private readonly IRepository<DynamicLinkEntity> _repository;
        private readonly IManagedService _redirectHandlerService;
        private readonly UserAgentService _userAgent;

        public DynamicLinkRedirectController(
            IRepository<DynamicLinkEntity> repository,
            IManagedService redirectHandlerService)
        {
            _repository = repository;
            _userAgent = new UserAgentService(new UserAgentSettings()
            {
                CacheSizeLimit = 20000,
                CacheSlidingExpiration = TimeSpan.FromDays(1),
                UaStringSizeLimit = 256,
            });
            _redirectHandlerService = redirectHandlerService;

        }

        //For Debug only
        [HttpGet("all")]
        public IList<DynamicLinkEntity> Index()
        {
            return _repository.GetAll().Result.ToList();

        }

        //For Debug only
        [HttpGet("ua-parse")]
        public IActionResult GetUa()
        {
            var data = _userAgent.Parse(Request.Headers["User-Agent"].ToString());
            return Ok(data);
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


        //For Debug only
        [HttpPost("/create")]
        public IActionResult Create([FromBody] DynamicLinkEntity link) => Ok(_repository.Create(link).Result);

    }
}
