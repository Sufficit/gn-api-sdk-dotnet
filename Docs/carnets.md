## Creating carnet billets

Carnet is a payment method that generates a bundle of charges with the same payment information and customer.

In order to generate a carnet, you'll need the items, the customer, the expiration date of the first charge and the number of repeats (or parcels).

The carnets can also be generated with the `metadata` attribute, just like in the banking billet, containing the `notification_url` and/or `custom_id`

There are other optional params:

- `split_items`, identifying if the total value must be splitted among the charges (defaults to `false`)
- The carnet `instructions`

### Required properties:

```c#
var body = new {
    items = new [] {
        new {
            name = "Carnet Item 1",
            value = 1000,
            amount = 2
        }
    },
    customer = new {
        name = "Gorbadoc Oldbuck",
        email = "oldbuck@gerencianet.com.br",
        cpf = "04267484171",
        birth = "1977-01-15",
        phone_number = "5144916523"
    },
    repeats = 4,
    expire_at = "2020-12-12"
};
```

### Required properties plus metadata **(optional)**:

```c#
var body = new {
    items = new [] {
        new {
            name = "Carnet Item 1",
            value = 1000,
            amount = 2
        }
    },
    customer = new {
        name = "Gorbadoc Oldbuck",
        email = "oldbuck@gerencianet.com.br",
        cpf = "04267484171",
        birth = "1977-01-15",
        phone_number = "5144916523"
    },
    repeats = 4,
    expire_at = "2020-12-12",
    metadata = new {
        custom_id = "my_id",
        notification_url = "http://yourdomain.com"
    }
};
```

The `notification_url` property will be used for sending notifications once things happen with charges statuses, as when it's payment was approved, for example. More about notifications [here](/Docs/notifications.md). The `custom_id` property can be used to set your own reference to the carnet.


### Required properties plus post office service information **(optional)**:

If you want the carnet to arrive at your house or at your client's house, you can count on Gerencianet's post office service. Just send an extra attribute:

```c#
var body = new {
    items = new [] {
        new {
            name = "Carnet Item 1",
            value = 1000,
            amount = 2
        }
    },
    customer = new {
        name = "Gorbadoc Oldbuck",
        email = "oldbuck@gerencianet.com.br",
        cpf = "04267484171",
        birth = "1977-01-15",
        phone_number = "5144916523"
    },
    repeats = 4,
    expire_at = "2020-12-12"
};
```

If `send_to` is set to *customer*, the carnet arrives at you customer's. If it is set to *seller*, just wait for it to arrive at your place!


### split_items attribute **(optional)**

By default, each parcel has the total value of the carnet. If you want to divide the total value among the parcels, change `split_items` property to *true*.

```c#
var body = new {
    items = new [] {
        new {
            name = "Carnet Item 1",
            value = 1000,
            amount = 2
        }
    },
    customer = new {
        name = "Gorbadoc Oldbuck",
        email = "oldbuck@gerencianet.com.br",
        cpf = "04267484171",
        birth = "1977-01-15",
        phone_number = "5144916523"
    },
    repeats = 4,
    expire_at = "2020-12-12",
    split_items = true
};
```

### Setting instructions **(optional)**

If you want the carnet billet to have extra instructions, it's possible to send a maximum of 4 different instructions with a maximum of 90 caracters, just as follows:

```c#
var body = new {
    items = new [] {
        new {
            name = "Carnet Item 1",
            value = 1000,
            amount = 2
        }
    },
    customer = new {
        name = "Gorbadoc Oldbuck",
        email = "oldbuck@gerencianet.com.br",
        cpf = "04267484171",
        birth = "1977-01-15",
        phone_number = "5144916523"
    },
    repeats = 4,
    expire_at = "2020-12-12",
    instructions = new [] {
        "Pay only with money",
        "Do not pay with gold"
    }
};
```

### Finally, create the carnet:

```c#
var response = endpoints.CreateCarnet(null, body);
```

Check out the response:

```js
{
  "code": 200,
  "data": {
    "carnet_id": 1000,
    "cover": "https://visualizacao.gerencianet.com.br/emissao/28333_2385_ZEMAL5/A5CC-28333-61428-LEENA9/28333-61428-LEENA9",
    "charges": [{
        "charge_id": 357,
        "parcel": "1",
        "status": "waiting",
        "value": 2000,
        "expire_at": "2020-12-12",
        "url": "https://visualizacao.gerencianet.com.br/emissao/28333_2385_ZEMAL5/A5CL-28333-61428-LEENA9/28333-61428-LEENA9",
        "barcode": "00190.00009 01523.894002 00061.428181 1 64780000002000"
      }, {
        "charge_id": 358,
        "parcel": "2",
        "status": "waiting",
        "value": 2000,
        "expire_at": "2021-01-12",
        "url": "https://visualizacao.gerencianet.com.br/emissao/28333_2385_ZEMAL5/A5CL-28333-61428-LEENA9/28333-61429-CORZE4",
        "barcode": "00190.00009 01523.894002 00061.428181 8 65090000002000"
      }, {
        "charge_id": 359,
        "parcel": "3",
        "status": "waiting",
        "value": 2000,
        "expire_at": "2021-02-12",
        "url": "https://visualizacao.gerencianet.com.br/emissao/28333_2385_ZEMAL5/A5CL-28333-61428-LEENA9/28333-61430-HIRRA4",
        "barcode": "00190.00009 01523.894002 00061.428181 7 65400000002000"
      }, {
        "charge_id": 360,
        "parcel": "4",
        "status": "waiting",
        "value": 2000,
        "expire_at": "2021-03-12",
        "url": "https://visualizacao.gerencianet.com.br/emissao/28333_2385_ZEMAL5/A5CL-28333-61428-LEENA9/28333-61431-HIRRA4",
        "barcode": "00190.00009 01523.894002 00061.428181 5 65400000002000"
      }
    ]
  }
}
```

Notice that, as the `repeats` were set to 4, the output contains 4 charges with `waiting` status. The value of each charge is the sum of the items values, because the `split_items` property was set to *false*. Also notice that `expire_at` increases monthly.
