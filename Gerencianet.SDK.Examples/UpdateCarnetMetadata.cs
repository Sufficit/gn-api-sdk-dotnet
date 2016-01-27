﻿using System;

namespace Gerencianet.SDK.Examples
{
    class UpdateCarnetMetadata
    {
        public static void Execute()
        {
            dynamic endpoints = new Endpoints(Credentials.Default.ClientId, Credentials.Default.ClientSecret, true);

            var param = new
            {
                id = 1001
            };

            var body = new
            {
                notification_url = "http://yourdomain.com",
                custom_id = "my_new_id"
            };

            try
            {
                var response = endpoints.UpdateCarnetMetadata(param, body);
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
