using AirlineWeb.Data;
using AirlineWeb.Dtos;
using AirlineWeb.Models;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AirlineWeb.Controllers
{
    [ApiController]
    [Route("api/webhooksubscription")]
    public class WebhookSubscriptionController : ControllerBase
    {
        private readonly RepositoryContext _repo;
        private readonly IMapper _mapper;
        public WebhookSubscriptionController(RepositoryContext repo, IMapper mapper)
        {
            _mapper = mapper;
            _repo = repo;
        }
       
        [HttpPost]
        public async Task<IActionResult> CreateWebhook([FromBody] WebhookSubscriptionCreationDto webHookToCreate)
        {
            try
            {
                if (webHookToCreate == null)
                {
                    return BadRequest("The object sent from client is null");
                }

                var webhookExist = await _repo.WebhookSubscriptions.Where(x => 
                (x.WebhookURI.Equals(webHookToCreate.WebhookURI)) && (x.WebhookType.Equals(webHookToCreate.WebhookType))).SingleOrDefaultAsync();

                if(webhookExist != null) {

                    return BadRequest();
                }

                var webhook = _mapper.Map<WebhookSubscription>(webHookToCreate);
                webhook.Secret = Guid.NewGuid().ToString();
                webhook.WebhookPublisher = "PanAus";
                await _repo.WebhookSubscriptions.AddAsync(webhook);
                await _repo.SaveChangesAsync();
                return StatusCode(201, new { Secret = webhook.Secret, Publisher = webhook.WebhookPublisher });
               
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex);
                return StatusCode(500);
            }
          

        }
    }
}
