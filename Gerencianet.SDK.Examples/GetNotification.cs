﻿using System;

namespace Gerencianet.SDK.Examples
{
    class GetNotification
    {
        public static void Execute()
        {
            dynamic endpoints = new Endpoints(Credentials.Default.ClientId, Credentials.Default.ClientSecret, true);

            var param = new
            {
                token = "46dab29b-ccf3-4c9a-8c39-254033fe06bc"
            };

            try
            {
                var response = endpoints.GetNotification(param);
                Console.WriteLine(response);
            }
            catch (GnException e)
            {
                Console.WriteLine(e.ErrorType);
                Console.WriteLine(e.Message);
            }
        }
    }
}
