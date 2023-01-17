﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NetworkingAuxiliaryLibrary.Assets.Misc;

namespace NetworkingAuxiliaryLibrary.Packages
{
    public static class JsonMessageFactory
    {

        public static string GetJsonMessageSimplified(string sender, string reciever, object? message)
        {
            return GetJsonMessage(sender, reciever, StringAssets.NonAccessable, StringAssets.NonAccessable, message);
        }


        public static string GetJsonMessage(string sender, string reciever, string date, string time, object? message)
        {
            string sRes = string.Empty;


            JsonMessagePackage package = new(sender, reciever, date, time, message);

            sRes = JsonConvert.SerializeObject(package);

            // json parsing issues check
            if (sRes.Equals(string.Empty))
            {
                throw new InvalidDataException($"[Custom] something went wrong. Result was {sRes}.");
            }

            return sRes;
        }


        public static string GetSerializedMessage(JsonMessagePackage unserializedMessage)
        {
            return GetJsonMessage
            (
                sender: unserializedMessage.Sender, 
                reciever: unserializedMessage.Reciever, 
                date: unserializedMessage.Date,
                time: unserializedMessage.Time,
                message: unserializedMessage.Message
            );
        }


        public static JsonMessagePackage GetUnserializedPackage(string unserializedJsonString)
        {
            JsonMessagePackage jmpRes = JsonConvert.DeserializeObject<JsonMessagePackage>(unserializedJsonString, new JsonSerializerSettings() { DateFormatString = StringAssets.DateFormat });

            return jmpRes;
        }

    }
}
