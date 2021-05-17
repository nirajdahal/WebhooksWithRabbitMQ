using AirlineWeb.Dtos;
using AirlineWeb.Models;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AirlineWeb.Profiles
{
    public class WebhookSubscriptionProfile: Profile
    {
        public WebhookSubscriptionProfile()
        {
            CreateMap<WebhookSubscriptionCreationDto, WebhookSubscription>();

        }
    }
}
