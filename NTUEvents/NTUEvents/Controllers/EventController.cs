﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using NTUEvents.Models;

namespace NTUEvents.Controllers
{
    /* 
        Method   Functions              Routing 
     1. [Get]    GetAllEvents         - api/events 
     2. [Get]    GetUserEvents        - api/events/{id}
     3. [Post]   CreateNewEvent       - api/events/create/{userId}
     4. [Put]    UpdateNewEvent       - api/events/update/{eventId}
     5. [Put]    DeleteEvent          - api/events/delete/{eventId}
     6. [Put]    DeleteAllUserEvents  - api/events/deleteall/{userId}

     *Note
     0. Do not key incorrect userid/eventid. 
     1. When testing on postman, select the correct method
     2. For [Put] method, type "Content-type" into Key and "application/json" into Value
     3. Generate the Json string according to the Object values
     4. Insert Json string into Description.

    */

    //Launch IIS at Class path
    //Go to Properties in your solution explorer -> launchSettings.json
    //Edit the "launchUrl"

    //Default route: api/events
    [Route("api/[controller]")]
    public class EventController : Controller
    {
        private readonly NtuEventsContext ntueventsContext_db;

        public EventController(NtuEventsContext context)
        {
            ntueventsContext_db = context;
        }

        [HttpGet]
        [AllowAnonymous]
        public string GetAllEvents()
        {
            //Show all non-deleted events
            var alleventList = ntueventsContext_db.Event.Where(t => t.IsDeleted == false).ToList();
            return JsonConvert.SerializeObject(alleventList, Formatting.Indented, new JsonSerializerSettings()
            {
                ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
            });
        }

        [HttpGet]
        [Route("{userId}")]
        [AllowAnonymous]
        public string GetUserEvents(int userId)
        {
            //Get user list
            //Filter by user and show all events by him
            var usereventRegList = (ntueventsContext_db.Eventreg.Where(t => t.UserIdEventregFk == userId)).ToList();
            var usereventInfo = ntueventsContext_db.Event.Where(t => usereventRegList.FirstOrDefault(p => t.EventId == p.EventidEventregFk) != null).ToList();
            return JsonConvert.SerializeObject(usereventInfo, Formatting.Indented, new JsonSerializerSettings()
            {
                ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
            });
        }

        [HttpPost]
        [Route("create/{userId}")]
        [AllowAnonymous]
        public string CreateNewEvent([FromBody] Event eventInfo, int userId)
        {
            //Add event first
            ntueventsContext_db.Event.Add(eventInfo);
            ntueventsContext_db.SaveChanges();

            //Get last count in db = event id
            //Add event reg and save to db.
            int count = ntueventsContext_db.Event.Count();
            Eventreg eventregInfo = new Eventreg();
            eventregInfo.EventidEventregFk = count;
            eventregInfo.UserIdEventregFk = userId;
            eventregInfo.CreatedBy = userId;
            eventregInfo.CreatedDate = DateTime.Now;
            eventregInfo.IsDeleted = false;
            ntueventsContext_db.Eventreg.Add(eventregInfo);
            ntueventsContext_db.SaveChanges();
            return "Success";
        }

        [HttpPut]
        [Route("update/{eventId}")]
        [AllowAnonymous]
        public string UpdateNewEvent([FromBody] Event eventInfo, int eventId)
        {
            //Get event
            //Update event
            Event eventItem = ntueventsContext_db.Event.Single(x => x.EventId == eventId);
            eventItem.CcaidEventFk = eventInfo.CcaidEventFk;
            eventItem.Title = eventInfo.Title;
            eventItem.Type = eventInfo.Type;
            eventItem.Venue = eventInfo.Venue;
            eventItem.Description = eventInfo.Description;
            eventItem.StartDate = eventInfo.StartDate;
            eventItem.EndDate = eventInfo.EndDate;
            eventItem.Quota = eventInfo.Quota;
            eventItem.UpdatedDate = DateTime.Now;
            eventItem.UpdatedBy = eventInfo.UpdatedBy;
            ntueventsContext_db.SaveChanges();
            return "Success";
        }

        [HttpPut]
        [Route("delete/{eventId}")]
        [AllowAnonymous]
        public string DeleteEvent(int eventId)
        {
            //Update isDeleted field - Soft delete
            var eventItem = ntueventsContext_db.Event.Single(t => t.EventId == eventId);
            eventItem.IsDeleted = true;
            ntueventsContext_db.SaveChanges();
            return "Success";
        }

        [HttpPut]
        [Route("deleteall/{userId}")]
        [AllowAnonymous]
        public string DeleteAllUserEvents(int userId)
        {
            //Get all events by the user & update isDeleted field - Soft delete
            var usereventRegList = (ntueventsContext_db.Eventreg.Where(t => t.UserIdEventregFk == userId)).ToList();
            var usereventInfo = ntueventsContext_db.Event.Where(t => usereventRegList.FirstOrDefault(p => t.EventId == p.EventidEventregFk) != null).ToList();
            foreach (var userEvents in usereventInfo)
            {
                userEvents.IsDeleted = true;
            }
            ntueventsContext_db.SaveChanges();
            return "Success";
        }
    }
}