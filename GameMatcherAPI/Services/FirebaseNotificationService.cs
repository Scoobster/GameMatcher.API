using FirebaseAdmin;
using FirebaseAdmin.Messaging;
using GameMatcher.EntityFramework.Models;
using GameMatcherAPI.Models;
using Google.Apis.Auth.OAuth2;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Web;

namespace GameMatcherAPI.Services
{
    public class FirebaseNotificationService
    {
        private readonly string NOTIFICATION_TYPE = "type";
        private readonly string REQUEST_NOTIFICATION_TYPE = "REQUEST_NOTIFICATION";
        private readonly string CONFIRMATION_NOTIFICATION_TYPE = "CONFIRMATION_NOTIFICATION";

        public void SendRequestNotifications(MatchRequest matchRequest, List<string> fcmTokens)
        {
            FirebaseMessaging messaging = FirebaseMessaging.DefaultInstance;

            // Notification to send
            Notification notification = new Notification()
            {
                Title = "There is a new match request!",
                Body = "A \"" + GetSquashGrade(matchRequest.HostPlayer.Ability) + "\" level ability player wants a match at "
                    + matchRequest.MatchStartTime.Value.ToString("ddd d MMM hh:mm tt")
            };

            // Match request data to be sent
            MatchRequestNotificationDto requestDto = new MatchRequestNotificationDto
            {
                Id = matchRequest.Id,
                HostPlayerId = matchRequest.HostPlayerId,
                Created = matchRequest.Created.Value,
                MatchStartTime = matchRequest.MatchStartTime.Value,
                LengthInMins = matchRequest.LengthInMins,
                HostPlayerAbility = matchRequest.HostPlayer.Ability
            };
            Dictionary<string, string> requestData = requestDto.GetType()
                .GetProperties(BindingFlags.Public | BindingFlags.Instance)
                .ToDictionary(prop => prop.Name, prop => prop.GetValue(requestDto, null).ToString());

            requestData[NOTIFICATION_TYPE] = REQUEST_NOTIFICATION_TYPE;

            // Create list of messages to send (only variance in message is Token)
            List<Message> messages = new List<Message>();
            foreach (string token in fcmTokens)
            {
                messages.Add(new Message()
                {
                    Token = token,
                    Notification = notification,
                    Data = requestData,
                    Android = new AndroidConfig()
                    {
                        Priority = Priority.High
                    }
                });
            }

            // SEND THEM ALL
            messaging.SendAllAsync(messages);
        }

        public void SendConfirmationOfMatchNotification(MatchConfirmed matchConfirmed, string hostPlayerToken)
        {
            FirebaseMessaging messaging = FirebaseMessaging.DefaultInstance;

            // Notification to send
            Notification notification = new Notification()
            {
                Title = "Your match request has been accepted!",
                Body = "A \"" + GetSquashGrade(matchConfirmed.GuestPlayer.Ability) + "\" level ability player has accepted your match at "
                    + matchConfirmed.MatchStartTime.Value.ToString("ddd d MMM hh:mm tt")
            };

            Dictionary<string, string> notificationData = new Dictionary<string, string>();
            notificationData[NOTIFICATION_TYPE] = CONFIRMATION_NOTIFICATION_TYPE;

            // Create list of messages to send (only variance in message is Token)
            Message message = new Message()
            {
                Token = hostPlayerToken,
                Notification = notification,
                Data = notificationData,
                Android = new AndroidConfig()
                {
                    Priority = Priority.High
                }
            };

            // SEND THEM ALL
            messaging.SendAsync(message);
        }

        private string GetSquashGrade(short abilityLevel)
        {
            switch (abilityLevel)
            {
                case 12:
                    return "A1";
                case 11:
                    return "A2";
                case 10:
                    return "B1";
                case 9:
                    return "B2";
                case 8:
                    return "C1";
                case 7:
                    return "C2";
                case 6:
                    return "D1";
                case 5:
                    return "D2";
                case 4:
                    return "E1";
                case 3:
                    return "E2";
                case 2:
                    return "F";
                default:
                    return "Beginner";
            }
        }
    }
}