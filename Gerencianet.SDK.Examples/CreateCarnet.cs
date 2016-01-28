﻿using System;

namespace Gerencianet.SDK.Examples
{
    class CreateCarnet
    {
        public static void Execute()
        {
            dynamic endpoints = new Endpoints(Credentials.Default.ClientId, Credentials.Default.ClientSecret, Credentials.Default.Sandbox);

            var body = new
            {
                items = new[] {
                    new {
                        name = "Carnet Item 1",
                        value = 1000,
                        amount = 2
                    }
                },
                customer = new
                {
                    name = "Gorbadoc Oldbuck",
                    email = "oldbuck@gerencianet.com.br",
                    cpf = "04267484171",
                    birth = "1977-01-15",
                    phone_number = "5144916523"
                },
                repeats = 12,
                split_items = false,
                expire_at = DateTime.Now.AddDays(5).ToString("yyyy-MM-dd")
            };

            try
            {
                var response = endpoints.CreateCarnet(null, body);
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
