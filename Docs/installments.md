## Listing installments

If you ever need to get the total value for a charge, including rates and interests, as well as each installment value, even before the payment itself, you can.

Why would I need this?

Sometimes you need to check the total for making a discount, or simple to show a combobox with the installments for your users.

Stop bragging about. Here is the code:

```c#
using Gerencianet.SDK;
...
dynamic endpoints = new Endpoints("client_id", "client_secret", true);

var param = new {
    brand = "visa",
    total = 5000
};

var response = endpoints.GetInstallments(param)
Console.WriteLine(response);
```

And the response:

```js
{
  "code": 200,
  "data": {
    "rate": 150,
    "interest_percentage": 0,
    "name": "visa",
    "installments": [
      {
        "installment": 1,
        "has_interest": true,
        "value": 5150,
        "currency": "51,50"
      },
      {
        "installment": 2,
        "has_interest": true,
        "value": 2679,
        "currency": "26,79"
      },
      {
        "installment": 3,
        "has_interest": true,
        "value": 1821,
        "currency": "18,21"
      },
      {
        "installment": 4,
        "has_interest": true,
        "value": 1393,
        "currency": "13,93"
      },
      {
        "installment": 5,
        "has_interest": true,
        "value": 1137,
        "currency": "11,37"
      },
      {
        "installment": 6,
        "has_interest": true,
        "value": 966,
        "currency": "9,66"
      },
      {
        "installment": 7,
        "has_interest": true,
        "value": 845,
        "currency": "8,45"
      },
      {
        "installment": 8,
        "has_interest": true,
        "value": 754,
        "currency": "7,54"
      },
      {
        "installment": 9,
        "has_interest": true,
        "value": 683,
        "currency": "6,83"
      },
      {
        "installment": 10,
        "has_interest": true,
        "value": 627,
        "currency": "6,27"
      },
      {
        "installment": 11,
        "has_interest": true,
        "value": 581,
        "currency": "5,81"
      },
      {
        "installment": 12,
        "has_interest": true,
        "value": 544,
        "currency": "5,44"
      }
    ]
  }
}
```

Observe that the response comes with an installments array of 12 positions at maximum. Each position matches one possible option of installment number, containing its value in currency and integer forms. The number of installments will vary according to the selected brand. Use it any way that suits your needs.
