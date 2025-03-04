using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bogus;
using OrderMate.Core.Aggregates.OrderAggregate;
using OrderMate.Core.Aggregates.OrderAggregate.Enums;
using OrderMate.Core.Aggregates.ProductAggregate;

namespace OrderMate.UnitTests.TestHelpers.Fakes;

public static class FakeOrderFactory
{
  private static readonly Faker _faker = new();

  private static readonly OrderStatus[] _statuses =
  {
        OrderStatus.New,
        OrderStatus.InProgress,
        OrderStatus.Completed
  };

  public static Order CreateOrder(
        int? id = null,
        int? userId = null,
        OrderStatus? status = null,
        List<Product>? products = null)
  {
    var order = new Order(userId ?? _faker.Random.Int(1, 1000));

    if (status is not null)
    {
      order.ChangeStatus(status);
    }
    else
    {
      order.ChangeStatus(_faker.PickRandom(_statuses));
    }

    if (products != null)
    {
      foreach (var product in products)
      {
        order.AddProduct(product, _faker.Random.Int(1, 5));
      }
    }

    if (id.HasValue)
    {
      typeof(Order).GetProperty(nameof(Order.Id))!.SetValue(order, id.Value);
    }

    return order;
  }

  public static Order CreateOrderWithRandomProducts(
      int userId,
      int productCount = 3)
  {
    var order = new Order(userId);

    var products = Enumerable.Range(1, productCount)
        .Select(_ => FakeProductFactory.CreateProduct())
        .ToList();

    foreach (var product in products)
    {
      order.AddProduct(product, _faker.Random.Int(1, 5));
    }

    return order;
  }
}
