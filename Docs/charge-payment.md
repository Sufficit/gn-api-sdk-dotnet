## Paying charges

There are two ways of giving sequence to a charge. You can generate a banking billet so it is payable until its due date, or can use the customer's credit card to submit the payment.

### 1. Banking billets

Setting banking billet as a charge's payment method is simple. You have to use `banking_billet` as the payment method and inform the `charge_id`.

```c#
using Gerencianet.SDK;
...
dynamic endpoints = new Endpoints("client_id", "client_secret", true);

var param = new {
    id = 123
};

var body = new {
    payment = new {
        banking_billet = new {
            expire_at = "2016-12-12",
            customer = new {
                name = "Gorbadoc Oldbuck",
                email = "oldbuck@gerencianet.com.br",
                cpf = "04267484171",
                birth = "1977-01-15",
                phone_number = "5144916523"
            }
        }
    }
};

var response = endpoints.PayCharge(param, body);
Console.WriteLine(response);
```

You'll receive the payment info in the callback, such as the barcode and the billet link:

```js
{
  "code": 200,
  "data": {
    "charge_id": 1253,
    "total": 1150,
    "payment": "banking_billet",
    "barcode": "00190.00009 01523.894002 00059.161182 9 64350000001150",
    "link": "https://visualizacao.gerencianet.com.br/emissao/28333_2139_RRABRA7/A4XB-28333-59161-BRANAE4",
    "expire_at": "2015-05-21"
  }
}
```

If you want the banking billet to have a message to customer, it's possible to send a message with a maximum of 255 caracters, just as follows:

```c#
var body = new {
    payment = new {
        banking_billet = new {
            expire_at = "2018-12-12",
            customer = new {
                name = "Gorbadoc Oldbuck",
                email = "oldbuck@gerencianet.com.br",
                cpf = "04267484171",
                birth = "1977-01-15",
                phone_number = "5144916523"
            },
            message = "The delivery time is counted in working days, in other words not inlclude Saturdays, Sundays and holidays"
        }
    }
}
```

If you want the banking billet to have own configurations. It's possible to send:
- `fine`: it's the amount charged after expiration. Ex.: If you want 2%, you must send 200.
- `interest`: it's the amount charged after expiration by day. Ex.: If you want 0.033%, you must send 33.
- `show_information`: Sets if you want the customer see your name, phone and address. This parameter is an `array` with the data that can show. If you don't want show anything, just send `null` to this parameter.

```c#
var body = new {
    payment = new {
        banking_billet = new {
            expire_at = "2018-12-12",
            customer = new {
                name = "Gorbadoc Oldbuck",
                email = "oldbuck@gerencianet.com.br",
                cpf = "04267484171",
                birth = "1977-01-15",
                phone_number = "5144916523"
            },
            configurations = new {
                fine = 200,
                interest = 33,
                show_information = new [] {"name", "address", "phone"}
            }
        }
    }
}
```


### 2. Credit card

The most common payment method is to use a credit card in order to make things happen faster. Paying a charge with a credit card in Gerencianet is as simples as generating a banking billet, as seen above.

The difference here is that we need to provide some extra information, as a `billing_address` and a `payment_token`. The former is used to make an anti-fraud analyze before accepting/appoving the payment, the latter identifies a credit card at Gerencianet, so that you don't need to bother about keeping track of credit card numbers. The `installments` attribute is self-explanatory.

To credit card is also possible to send `message` and `configurations` like in banking billet. For more information, see the previous topic.

We'll talk about getting payment tokens later. For now, let's take a look at the snipet that does the work we're aiming for:

```c#
using Gerencianet.SDK;
...
dynamic endpoints = new Endpoints("client_id", "client_secret", true);

var param = new {
    id = 123
};

var body = new {
    payment = new {
        credit_card = new {
            installments = 1,
            payment_token = "", // see credit card flow to see how to get this
            billing_address = new {
                street = "Av. JK",
                number = 909,
                neighborhood = "Bauxita",
                zipcode = "35400000",
                city = "Ouro Preto",
                state = "MG"
            },
            customer = new {
                name = "Gorbadoc Oldbuck",
                email = "oldbuck@gerencianet.com.br",
                cpf = "04267484171",
                birth = "1977-01-15",
                phone_number = "5144916523"
            }
        }
    }
};

var response = endpoints.PayCharge(param, body);
Console.WriteLine(response);
```

If everything went well, the response will come with the total value, installments number and the value of each installment:

```js
{
  "code": 200,
  "data": {
    "charge_id": 1253,
    "total": 1150,
    "payment": "credit_card",
    "installments": 1,
    "installment_value": 1150
  }
}
```

##### Payment tokens

A `payment_token` represents a credit card number at Gerencianet.

For testing purposes, you can go to your application playground in your Gerencianet's account. At the payment endpoint you'll see a button that generates one token for you. This payment token will point to a random test credit card number.

When in production, it will depend if your project is a web app or a mobile app.

For web apps you should follow this [guide](https://api.gerencianet.com.br/checkout/card). It basically consists of copying/pasting a script tag in your checkout page.

For mobile apps you should use this [SDK for Android](https://github.com/gerencianet/gn-api-sdk-android) or this [SDK for iOS](https://github.com/gerencianet/gn-api-sdk-ios).

### 3. Discount by payment method

It is possible to set discounts based on payment. You just need to add an `discount` attribute within `banking_billet` or `credit_card` tags.

The snipet below shows how to do this for credit card payments.

```c#

var body = new {
    payment = new {
        credit_card = new {
            installments = 1,
            payment_token = "", // see credit card flow to see how to get this
            billing_address = new {
                street = "Av. JK",
                number = 909,
                neighborhood = "Bauxita",
                zipcode = "35400000",
                city = "Ouro Preto",
                state = "MG"
            },
            customer = new {
                name = "Gorbadoc Oldbuck",
                email = "oldbuck@gerencianet.com.br",
                cpf = "04267484171",
                birth = "1977-01-15",
                phone_number = "5144916523"
            },
            discount = new {
                type = "currency",
                value = 1000
            }
        }
    }
};
```

Discounts for banking billets works similar to credit cards. You just need to add the `discount` attribute.

The discount may be applied as percentage or with a previous calculated value.

The `type` property is used to specify how the discount will work. It may be set as *currency* or *percentage*.

The former will discount the amount specified in `value` property as *cents*, so, in the example above the amount paid by the customer will be `(Charge's value) - R$ 10,00`.

If the discount type is set to *percentage*, the amount will be `(Charge's value) - 10%`.
