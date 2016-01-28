## Creating charges

Charges have one or more items. That's it.

```c#
var body = new {
    items = new [] {
        new {
            name = "Product 1",
            value = 1000,
            amount = 2
        }
    }
};
```


### Adding shipping costs to a charge **(optional)**:

In order to be the most agnostic as possible about how the user handles shippings, the API just receives an array with the values. You can add as many as you want. Sometimes you'll want a shipping cost to be received by another person/account. In this case, you must provide the `payee_code`. The *Additional Shipping* in the example below will be passed on to the referenced account after the payment.

```c#
var body = new {
    items = new [] {
        new {
            name = "Product A",
            value = 1000,
            amount = 2
        }
    },
    shippings = new [] {
        new {
            name = "Default Shipping",
            value = 100
        },
        new {
            name = "Additional Shipping",
            value = 120,
            payee_code = "GEZTAMJYHA3DAMBQGAYDAMRYGMZTGMBRGI"
        }
    }
};
```

### Charge `metadata` attribute:

```c#
var body = new {
    items = new [] {
        new {
            name = "Product A",
            value = 1000,
            amount = 2
        }
    },
    metadata = new {
        custom_id = "my_id",
        notification_url = "http://yourdomain.com"
    }
};
```

The `notification_url` property will be used for sending notifications once things happen with charges statuses, as when it's payment was approved, for example. More about notifications [here](/Docs/notifications.md). The `custom_id` property can be used to set your own reference to the charge.

### Finally, create the charge:

```c#
var response = endpoints.CreateCharge(null, body);
```

Check out the response:

```js
{
  "code": 200,
  "data": {
    "charge_id": 1253,
    "total": 2000,
    "status": "new",
    "custom_id": null,
    "created_at": "2015-05-18 14:56:39"
  }
}
```
