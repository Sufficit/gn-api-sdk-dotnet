﻿using System;

namespace Gerencianet.SDK.Examples
{
    class ResendParcel
    {
        public static void Execute()
        {
            dynamic endpoints = new Endpoints(Credentials.Default.ClientId, Credentials.Default.ClientSecret, true);

            var param = new
            {
                id = 1001,
                parcel = 2
            };

            var body = new
            {
                email = "oldbuck@gerencianet.com.br"
            };

            try
            {
                var response = endpoints.ResendParcel(param, body);
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
