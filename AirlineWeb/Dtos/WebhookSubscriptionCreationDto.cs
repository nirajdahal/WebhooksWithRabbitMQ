using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AirlineWeb.Dtos
{
    public class WebhookSubscriptionCreationDto
    {

        public string WebhookURI { get; set; }

        public string WebhookType { get; set; }

    }
}
