﻿using System.Collections.Generic;
using System.Web.Mvc;
using Web.MainApplication.WebUtility;

namespace Web.MainApplication.Controllers
{
    [AplicationAuthorizationFilter]
    public class BaseController : Controller
    {
        internal void ClearMessage()
        {
           

        }
        public BaseController()
        {
        }
        
        
        public List<string> ErrorMessagesAdd(string message)
        {
            var allMessage = (List<string>)TempData["MessageError1402"];
            if (allMessage == null)
            {
                allMessage = new List<string>();
                allMessage.Add(message);
                TempData["MessageError1402"] = allMessage;
            }
            else
            {
                allMessage.Add(message);
                TempData["MessageError1402"] = allMessage;
            }
            return allMessage;
        }
        public List<string> ErrorMessages()
        {
            var allMessage = (List<string>)TempData["MessageError1402"];
            if (allMessage == null)
            {
                allMessage = new List<string>();
                TempData["MessageError1402"] = allMessage;
            }
            else
            {
                TempData["MessageError1402"] = allMessage;
            }
            return allMessage;
        }
        public List<string> WarningMessagesAdd(string message)
        {
            var allMessage = (List<string>)TempData["MessageWarning1402"];
            if (allMessage == null)
            {
                allMessage = new List<string>();
                allMessage.Add(message);
                TempData["MessageWarning1402"] = allMessage;
            }
            else
            {
                allMessage.Add(message);
                TempData["MessageWarning1402"] = allMessage;
            }
            return allMessage;
        }
        public List<string> WarningMessages()
        {
            var allMessage = (List<string>)TempData["MessageWarning1402"];
            if (allMessage == null)
            {
                allMessage = new List<string>();
                TempData["MessageWarning1402"] = allMessage;
            }
            else
            {
                TempData["MessageWarning1402"] = allMessage;
            }
            return allMessage;
        }
        public List<string> InfoMessagesAdd(string message)
        {
            var allMessage = (List<string>)TempData["MessageInfo1402"];
            if (allMessage == null)
            {
                allMessage = new List<string>();
                allMessage.Add(message);
                TempData["MessageInfo1402"] = allMessage;
            }
            else
            {
                allMessage.Add(message);
                TempData["MessageInfo1402"] = allMessage;
            }
            return allMessage;
        }
        public List<string> InfoMessages()
        {
            var allMessage = (List<string>)TempData["MessageInfo1402"];
            if (allMessage == null)
            {
                allMessage = new List<string>();
                TempData["MessageInfo1402"] = allMessage;
            }
            else
            {
                TempData["MessageInfo1402"] = allMessage;
            }
            return allMessage;
        }
        public List<string> SuccessMessagesAdd(string message)
        {
            var allMessage = (List<string>)TempData["MessageSuccess1402"];
            if (allMessage == null)
            {
                allMessage = new List<string>();
                allMessage.Add(message);
                TempData["MessageSuccess1402"] = allMessage;
            }
            else
            {
                allMessage.Add(message);
                TempData["MessageSuccess1402"] = allMessage;
            }
            return allMessage;
        }
        public List<string> SuccessMessages()
        {
            var allMessage = (List<string>)TempData["MessageSuccess1402"];
            if (allMessage == null)
            {
                allMessage = new List<string>();
                TempData["MessageSuccess1402"] = allMessage;
            }
            else
            {
                TempData["MessageSuccess1402"] = allMessage;
            }
            return allMessage;
        }
    }
}