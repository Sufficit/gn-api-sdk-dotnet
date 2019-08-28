using System;

namespace Gerencianet.SDK.Examples
{
    class OneStepBankingBillet
    {
        public static void Execute()
        {
            dynamic endpoints = new Endpoints(Credentials.Default.ClientId, Credentials.Default.ClientSecret,
                Credentials.Default.Sandbox);

            var body = new
            {
                items = new[]
                {
                    new
                    {
                        name = "Product 1",
                        value = 1000,
                        amount = 2
                    }
                },
                shippings = new[]
                {
                    new
                    {
                        name = "Default Shipping Cost",
                        value = 100
                    }
                },
                payment = new
                {
                    banking_billet = new
                    {
                        expire_at = DateTime.Now.AddDays(1).ToString("yyyy-MM-dd"),
                        customer = new
                        {
                            name = "Gorbadoc Oldbuck",
                            email = "oldbuckaa@gerencianet.com.br",
                            cpf = "94271564656",
                            birth = "1977-01-15",
                            phone_number = "5144916523"
                        }
                    }
                }
            };

            try
            {
                var response = endpoints.OneStep(null, body);
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