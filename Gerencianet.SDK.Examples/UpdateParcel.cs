﻿using System;

namespace Gerencianet.SDK.Examples
{
    class UpdateParcel
    {
        public static void Execute()
        {
            dynamic endpoints = new Endpoints(Credentials.Default.ClientId, Credentials.Default.ClientSecret, true);

            var param = new
            {
                id = 1001,
                parcel = 3,
            };

            var body = new {
                expire_at = "2020-12-20"
            };

            try
            {
                var response = endpoints.UpdateParcel(param, body);
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
