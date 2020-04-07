﻿using System;

namespace EventHub.Service.Queries.ViewModels
{
    public class EventView
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public DateTime StartDateTime { get; set; }

        public DateTime EndDateTime { get; set; }
    }
}
