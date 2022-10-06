using System;
using System.Collections.Generic;
namespace MainNetworkingProject.Model.Basics
{
    public class ServiceUser
    {

        public string UserName { get; set; } = null!;

        public Socket? UserSocket { get; set; }



        #region CONSTRUCTION


        /// <summary>
        /// Default constructor;
        /// <br />
        /// Конструктор по умолчанию;
        /// </summary>
        public ServiceUser()
        {
            UserName = "Unknown";
            UserSocket = null;
        }


        #endregion CONSTRUCTION

    }
}
