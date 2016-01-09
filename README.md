![peasy](https://www.dropbox.com/s/2yajr2x9yevvzbm/peasy3.png?dl=0&raw=1)

# A sample application

A full implementation of a middle tier built with peasy and sample consumer clients (WPF, Web API, and ASP.NET MVC) can be found here.  You can clone the repo or download the entire solution as a [zip](https://github.com/peasy/samples/archive/master.zip).

The sample application is a ficticious order entry / inventory management system, and offers both WPF and ASP.NET MVC clients.  All efforts were made to keep these applications as simple as possible to keep the focus on how a middle tier is written with peasy and consumed by multiple clients.

The easiest way to get up and running is to set either the WPF project or the ASP.NET MVC project as the startup project and run the application.  By default, these projects are configured to use in-memory implementations of the [data proxies](https://github.com/peasy/Peasy.NET/wiki/Data-Proxy).  

### SQL Server Setup

The sample applications can be configured to interact with a SQL Server database.  Here are the steps to setup a SQL Server database for use by the sample applications:

1.) In package manager console, select Orders.com.DAL.EF in the Default project drop down list

2.) Execute following command: update-database -verbose

### Configurations

Because these clients consume a middle tier written with peasy, they can be configured in different ways to suit your needs.  Below are multiple available configurations that serve to showcase how you might deploy applications consuming your middle tier written with peasy.

* [WPF &#8594; In Memory](https://github.com/peasy/Samples#wpf--in-memory)
* [WPF &#8594; SQL Server](https://github.com/peasy/Samples#wpf--sql-server)
* [WPF &#8594; Web API -> In Memory](https://github.com/peasy/Samples#wpf--web-api--in-memory)
* [WPF &#8594; Web API -> SQL Server](https://github.com/peasy/Samples#wpf--web-api--sql-server)
* [ASP.NET MVC &#8594; In Memory](https://github.com/peasy/Samples#aspnet-mvc--in-memory)
* [ASP.NET MVC &#8594; SQL Server](https://github.com/peasy/Samples#aspnet-mvc--sql-server)
* [ASP.NET MVC &#8594; Web API -> SQL Server](https://github.com/peasy/Samples#aspnet-mvc--web-api--sql-server)

#### WPF &#8594; In Memory

![WPF &#8594; In Memory](https://www.dropbox.com/s/yex9qv528um3re6/WPF.png?dl=0&raw=1)

In this scenario, the WPF client consumes [business services](https://github.com/peasy/Peasy.NET/wiki/ServiceBase) that are injected with [data proxies](https://github.com/peasy/Peasy.NET/wiki/Data-Proxy) that communicate with in-memory data stores.  To configure the WPF application to use business services that are injected with in-memory data proxies, locate the ```MainWindow_Loaded``` event handler in the [MainWindow](https://github.com/peasy/Samples/blob/master/Orders.com.WPF/MainWindow.xaml.cs) class and ensure the following line exists:

```c#
void MainWindow_Loaded(object sender, RoutedEventArgs e)
{
    ConfigureInMemoryUsage();
}
```

The ```ConfigureInMemoryUsage``` method instantiates the business services and passes concrete [in-memory data proxies](https://github.com/peasy/Peasy.DataProxy.InMemory) to them for data access.

To run the application, ensure that the WPF project is set as the startup project.

#### WPF &#8594; SQL Server
![WPF &#8594; SQL Server](https://www.dropbox.com/s/s5xvkdgkasynzd6/WPF-SQL.png?dl=0&raw=1)

In this scenario, the WPF client consumes [business services](https://github.com/peasy/Peasy.NET/wiki/ServiceBase) that are injected with [data proxies](https://github.com/peasy/Peasy.NET/wiki/Data-Proxy) that use Entity Framework 6.0 to communicate with a SQL Server database.  To configure the WPF application to use business services injected with EF6 data proxies, locate the ```MainWindow_Loaded``` event handler in the [MainWindow](https://github.com/peasy/Samples/blob/master/Orders.com.WPF/MainWindow.xaml.cs) class and ensure the following line exists:

```c#
void MainWindow_Loaded(object sender, RoutedEventArgs e)
{
    ConfigureEFUsage();
}
```

The ```ConfigureEFUsage``` method instantiates the business services and passes concrete [EF6 data proxies](https://github.com/peasy/Peasy.DataProxy.EF6) to them for data access.

Before running the application, be sure to [setup SQL Server](https://github.com/peasy/Samples#sql-server-setup) after changing the configuration. 

To run the application, ensure that the WPF project is set as the startup project.

#### WPF &#8594; Web API &#8594; In Memory
![WPF &#8594; Web API &#8594; In Memory](https://www.dropbox.com/s/qzouuwj1lrec44v/WPF-WebAPI.png?dl=0&raw=1)

In this scenario, the WPF client consumes [business services](https://github.com/peasy/Peasy.NET/wiki/ServiceBase) that are injected with [data proxies](https://github.com/peasy/Peasy.NET/wiki/Data-Proxy) that use HTTP (via HttpClient) to communicate with the Web API application.  In turn, the Web API application uses business services that are injected with data proxies that communicate with in-memory data stores.

To configure the WPF application to use business services that are injected with HTTP data proxies, locate the ```MainWindow_Loaded``` event handler in the [MainWindow](https://github.com/peasy/Samples/blob/master/Orders.com.WPF/MainWindow.xaml.cs) class and ensure the following line exists:

```c#
void MainWindow_Loaded(object sender, RoutedEventArgs e)
{
    ConfigureHttpClientUsage();
}
```

An important thing to note is that the configuration code in ```ConfigureHttpClientUsage()``` uses two business services that can be referred to as pass-thru or client service classes.  Let's take a look at the code:

![wpf http config](https://www.dropbox.com/s/mfxllpsyieuutri/wpf_http_config.png?dl=0&raw=1)

The classes highlighted in red represent pass-through service classes.  These classes inherit from other service classes and override command methods to bypass any command logic to simply marshal calls to the data proxy.  For more information about the necessity of pass through commands, see []().

The next step is to configure the Web API project.  The Web API project uses dynamic dependency injection to create business services and data proxies that will be injected into the API controllers.  To configure the Web API project to consume in-memory data proxies, open the [DependencyInjection.config](https://github.com/peasy/Samples/blob/master/Orders.com.Web.Api/DependencyInjection.config) file.

Notice that there are two configuration sections for data proxies, one for Entity Framework and one for In-Memory, respectively.  Simply ensure that the Entity Framework section is commented out and the In Memory section is uncommented.

To run the application, set the WPF and Web Api as the startup projects in the solution and run the application.

#### WPF &#8594; Web API &#8594; SQL Server
![WPF &#8594; In Memory](https://www.dropbox.com/s/3jnzgut90xfoy23/WPF-API-SQL.png?dl=0&raw=1)

In this scenario, the WPF client consumes [business services](https://github.com/peasy/Peasy.NET/wiki/ServiceBase) that are injected with [data proxies](https://github.com/peasy/Peasy.NET/wiki/Data-Proxy) that use HTTP (via HttpClient) to communicate with the Web API application.  In turn, the Web API application uses business services that are injected with data proxies that use Entity Framework 6.0 to communicate with a SQL Server database.

To configure the WPF application to use business services that are injected with HTTP data proxies, locate the ```MainWindow_Loaded``` event handler in the [MainWindow](https://github.com/peasy/Samples/blob/master/Orders.com.WPF/MainWindow.xaml.cs) class and ensure the following line exists:

```c#
void MainWindow_Loaded(object sender, RoutedEventArgs e)
{
    ConfigureHttpClientUsage();
}
```

An important thing to note is that the configuration code in ```ConfigureHttpClientUsage()``` uses two business services that can be referred to as pass-thru or client service classes.  Let's take a look at the code:

![wpf http config](https://www.dropbox.com/s/mfxllpsyieuutri/wpf_http_config.png?dl=0&raw=1)

The classes highlighted in red represent pass-through service classes.  These classes inherit from other service classes and override command methods to bypass any command logic to simply marshal calls to the data proxy.  For more information about the necessity of pass through commands, see []().

The next step is to configure the Web API project.  The Web API project uses dynamic dependency injection to create business services and data proxies that will be injected into the API controllers.  To configure the Web API project to consume Entity Framework data proxies, open the [DependencyInjection.config](https://github.com/peasy/Samples/blob/master/Orders.com.Web.Api/DependencyInjection.config) file.

Notice that there are two configuration sections for data proxies, one for Entity Framework and one for In-Memory, respectively.  Simply ensure that the Entity Framework section is uncommented and the In Memory section is commented out.

Before running the application, be sure to [setup SQL Server](https://github.com/peasy/Samples#sql-server-setup) after changing the configuration. 

To run the application, set the WPF and Web Api as the startup projects in the solution and run the application.

#### ASP.NET MVC &#8594; In Memory
![WPF &#8594; In Memory](https://www.dropbox.com/s/woda85tpyk7l3ht/MVC.png?dl=0&raw=1)

#### ASP.NET MVC &#8594; SQL Server
![WPF &#8594; In Memory](https://www.dropbox.com/s/9gsj1omqezv2f0b/MVC-SQL.png?dl=0&raw=1)

#### ASP.NET MVC &#8594; Web API &#8594; SQL Server
![WPF &#8594; In Memory](https://www.dropbox.com/s/12s0xb94aj8fuyu/MVC-API-SQL.png?dl=0&raw=1)


### Using pass thru service service proxies

This is due to the fact that command methods in the business services use logic to orchestrate logic among different services within transactions, or to eliminate the potential for work to be executed twice.

To illustrate this, let's take a look at the following method from the OrderItemService.ShipCommand method:

```c#
public virtual ICommand<OrderItem> ShipCommand(long orderItemID)
{
    var proxy = DataProxy as IOrderItemDataProxy;
    return new ShipOrderItemCommand(orderItemID, proxy, _inventoryDataProxy, _transactionContext);
}
```

The ```ShipCommand``` method simply returns an instance of the ShipOrderItemCommand, and will invoke the following code when Execute() is invoked (remember that Command.Execute or Command.ExecuteAsync will invoke OnExecute or OnExecuteAsync, respectively):

```c#
protected override OrderItem OnExecute()
{
    return _transactionContext.Execute(() =>
    {
        var inventoryItem = _inventoryDataProxy.GetByProduct(CurrentOrderItem.ProductID);
        if (inventoryItem.QuantityOnHand - CurrentOrderItem.Quantity >= 0)
        {
            CurrentOrderItem.OrderStatus().SetShippedState();
            CurrentOrderItem.ShippedDate = DateTime.Now.ToUniversalTime();
            inventoryItem.QuantityOnHand -= CurrentOrderItem.Quantity;
            _inventoryDataProxy.Update(inventoryItem);
        }
        else
        {
            CurrentOrderItem.OrderStatus().SetBackorderedState();
            CurrentOrderItem.BackorderedDate = DateTime.Now.ToUniversalTime();
        }
        return _orderItemDataProxy.Ship(CurrentOrderItem);
    });
} 
```

This method is responsible for decrementing inventory followed by updating the status of the current order item to 'shipped'.  In this scenario, the application is using an HTTP order item data proxy that will marshal the call to a receiving ```Ship``` method of the OrderItems Web Api controller.  Because the Ship method of the OrderItems controller is also configured to use the ShipCommand, this logic will be executed again, thus decrementing the inventory twice.

To address this issue, the OrderItemClientService class was created that overrides the ```Ship``` method and simply marshals the call to the _orderItemDataProxy.Ship method, bypassing the logic of the ShipCommand on the client, and allowing the OrderItem Web Api Service to handle the logic.

Further notice that the ShipCommand logic executes the code atomically within a transaction.  Because it is notoriously difficult to orchestrate transactions that occur via HTTP service calls, it is best to bypass the logic on the client and allow the server to execute the code atomically against a data store.
