![peasy](https://www.dropbox.com/s/2yajr2x9yevvzbm/peasy3.png?dl=0&raw=1)

# A sample application

A full implementation of a middle tier built with peasy and sample consumer clients (WPF, Web API, and ASP.NET MVC) can be found here.  You can clone the repo or download the entire solution as a [zip](https://github.com/peasy/samples/archive/master.zip).

The sample application is a ficticious order entry / inventory management system, and provides both WPF and ASP.NET MVC UI clients.  All efforts were made to keep these applications as simple as possible to keep the focus on how a middle tier is written with peasy and consumed by multiple clients.

![screenshot](https://www.dropbox.com/s/lw5y82r0yj4jrt3/screenshot.png?dl=0&raw=1)

The easiest way to get up and running is to set either the WPF project or the ASP.NET MVC project as the startup project and run the application.  By default, these projects are configured to use in-memory implementations of the [data proxies](https://github.com/peasy/Peasy.NET/wiki/Data-Proxy).

However, there is a multitude of configuration possibilities.  The [configurations](https://github.com/peasy/Samples#configurations) section provides details on setting up many potential configurations.

### SQL Server Setup

The sample applications can be configured to interact with a SQL Server database.  Here are the steps to setup a SQL Server database for use by the sample applications:

1.) In package manager console, select ```Orders.com.DAL.EF``` in the Default project drop down list

2.) Execute the following command: ```update-database -verbose```

### Configurations

Because these clients consume a middle tier written with peasy, they can be configured in different ways to suit your needs.  Below are multiple available configurations that serve to showcase how you might deploy applications consuming your middle tier written with peasy.

* [WPF &#8594; In Memory](https://github.com/peasy/Samples#wpf--in-memory)
* [WPF &#8594; SQL Server](https://github.com/peasy/Samples#wpf--sql-server)
* [WPF &#8594; Web API -> In Memory](https://github.com/peasy/Samples#wpf--web-api--in-memory)
* [WPF &#8594; Web API -> SQL Server](https://github.com/peasy/Samples#wpf--web-api--sql-server)
* [ASP.NET MVC &#8594; In Memory](https://github.com/peasy/Samples#aspnet-mvc--in-memory)
* [ASP.NET MVC &#8594; SQL Server](https://github.com/peasy/Samples#aspnet-mvc--sql-server)
* [ASP.NET MVC &#8594; Web API -> In Memory](https://github.com/peasy/Samples#aspnet-mvc--web-api--in-memory)
* [ASP.NET MVC &#8594; Web API -> SQL Server](https://github.com/peasy/Samples#aspnet-mvc--web-api--sql-server)
* [Multiple Clients &#8594; Web API -> (In Memory or SQL Server)](https://github.com/peasy/Samples#multiple-clients--web-api--sql-server)

#### WPF &#8594; In Memory

![WPF &#8594; In Memory](https://www.dropbox.com/s/yls7db3j3pxy4ol/WPF%20%281%29.png?dl=0&raw=1)

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
![WPF &#8594; SQL Server](https://www.dropbox.com/s/02stvqm7dby969m/WPF-SQL%20%282%29.png?dl=0&raw=1)

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
![WPF &#8594; Web API &#8594; In Memory](https://www.dropbox.com/s/4ujrou39t6jpq6i/WPF-API.png?dl=0&raw=1)

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

Notice that there are two configuration sections for data proxies; one for Entity Framework and one for In-Memory, respectively.  Simply ensure that the ```Entity Framework Data Proxies``` section is commented out and the ```In Memory Data Proxies``` section is uncommented.

To run the application, set the WPF and Web Api projects as the startup projects in the solution and run the application.

#### WPF &#8594; Web API &#8594; SQL Server
![WPF &#8594; API &#8594 In Memory](https://www.dropbox.com/s/7k1kz1j9k3tueax/WPF-API-SQL%20%281%29.png?dl=0&raw=1)

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

The classes highlighted in red represent pass-through service classes.  These classes inherit from other service classes and override command methods to bypass any command logic to simply marshal calls to the data proxy.  To learn more about the necessity of pass through commands, see []().

The next step is to configure the Web API project.  The Web API project uses dynamic dependency injection to create business services and data proxies that will be injected into the API controllers.  To configure the Web API project to consume Entity Framework data proxies, open the [DependencyInjection.config](https://github.com/peasy/Samples/blob/master/Orders.com.Web.Api/DependencyInjection.config) file.

Notice that there are two configuration sections for data proxies; one for Entity Framework and one for In-Memory, respectively.  Simply ensure that the ```Entity Framework Data Proxies``` section is uncommented and the ```In Memory Data Proxies``` section is commented out.

Before running the application, be sure to [setup SQL Server](https://github.com/peasy/Samples#sql-server-setup) after changing the configuration. 

To run the application, set the WPF and Web Api projects as the startup projects in the solution and run the application.

#### ASP.NET MVC &#8594; In Memory
![MVC &#8594; In Memory](https://www.dropbox.com/s/l3q8y2obxhxy9ca/MVC%20%281%29.png?dl=0&raw=1)

In this scenario, the ASP.NET MVC client consumes [business services](https://github.com/peasy/Peasy.NET/wiki/ServiceBase) that are injected with [data proxies](https://github.com/peasy/Peasy.NET/wiki/Data-Proxy) that communicate with in-memory data stores.  

The MVC project uses dynamic dependency injection to create business services and data proxies that will be injected into the MVC controllers.  To configure the MVC project to consume in-memory data proxies, open the [DependencyInjection.config](https://github.com/peasy/Samples/blob/master/Orders.com.Web.MVC/DependencyInjection.config) file.

Notice that there are three configuration sections for data proxies; Entity Framework, HTTP, and In-Memory, respectively.  Simply ensure that the ```In Memory Data Proxies``` section is uncommented and the others are commented out.

To run the application, ensure that the MVC project is set as the startup project.

#### ASP.NET MVC &#8594; SQL Server
![MVC &#8594; SQL Server](https://www.dropbox.com/s/9gsj1omqezv2f0b/MVC-SQL.png?dl=0&raw=1)

In this scenario, the ASP.NET MVC client consumes [business services](https://github.com/peasy/Peasy.NET/wiki/ServiceBase) that are injected with [data proxies](https://github.com/peasy/Peasy.NET/wiki/Data-Proxy) that use Entity Framework 6.0 to communicate with a SQL Server database.  

The MVC project uses dynamic dependency injection to create business services and data proxies that will be injected into the MVC controllers.  To configure the MVC project to consume Entity Framework data proxies, open the [DependencyInjection.config](https://github.com/peasy/Samples/blob/master/Orders.com.Web.MVC/DependencyInjection.config) file.

Notice that there are three configuration sections for data proxies; Entity Framework, HTTP, and In-Memory, respectively.  Simply ensure that the ```Entity Framework Data Proxies``` section is uncommented and the others are commented out.

Before running the application, be sure to [setup SQL Server](https://github.com/peasy/Samples#sql-server-setup) after changing the configuration. 

To run the application, ensure that the MVC project is set as the startup project.

#### ASP.NET MVC &#8594; Web API &#8594; In Memory
![MVC &#8594; API &#8594; In Memory](https://www.dropbox.com/s/ubegb4hp860nkjf/MVC-API.png?dl=0&raw=1)

In this scenario, the ASP.NET MVC client consumes [business services](https://github.com/peasy/Peasy.NET/wiki/ServiceBase) that are injected with [data proxies](https://github.com/peasy/Peasy.NET/wiki/Data-Proxy) that use HTTP (via HttpClient) to communicate with the Web API application.  In turn, the Web API application uses business services that are injected with data proxies that communicate with in-memory data stores.

The MVC project uses dynamic dependency injection to create business services and data proxies that will be injected into the MVC controllers.  To configure the MVC project to consume in-memory data proxies, open the [DependencyInjection.config](https://github.com/peasy/Samples/blob/master/Orders.com.Web.MVC/DependencyInjection.config) file.

Notice that there are three configuration sections for data proxies; Entity Framework, HTTP, and In-Memory, respectively.  Ensure that the ```HTTP Data Proxies``` section is uncommented and the others are commented out.

Also notice that there are two configuration sections for business services; MVC and Web API, respectively.  Ensure that the ```Business Services - Web API``` section is uncommented and the ```Business Services - MVC``` section is commented out.

An important thing to note is that the ```Business Services - Web API``` section in the configuration file uses two business services that can be referred to as pass-thru or client service classes.  Let's take a look at the config section:

![MVC-Config](https://www.dropbox.com/s/bwdslmakb9uic6j/MVC-DI-Config.png?dl=0&raw=1)

The classes highlighted in red represent pass-through service classes.  These classes inherit from other service classes and override command methods to bypass any command logic to simply marshal calls to the data proxy.  To learn more about the necessity of pass through commands, see []().

The next step is to configure the Web API project.  The Web API project uses dynamic dependency injection to create business services and data proxies that will be injected into the API controllers.  To configure the Web API project to consume in-memory data proxies, open the [DependencyInjection.config](https://github.com/peasy/Samples/blob/master/Orders.com.Web.Api/DependencyInjection.config) file.

Notice that there are two configuration sections for data proxies; one for Entity Framework and one for In-Memory, respectively.  Simply ensure that the ```Entity Framework Data Proxies``` section is commented out and the ```In Memory Data Proxies``` section is uncommented.

To run the application, set the WPF and Web Api projects as the startup projects in the solution and run the application.

#### ASP.NET MVC &#8594; Web API &#8594; SQL Server
![MVC &#8594; APS &#8594; SQL Server](https://www.dropbox.com/s/rv5rd4omoghluq9/MVC-API-SQL%20%281%29.png?dl=0&raw=1)

In this scenario, the ASP.NET MVC client consumes [business services](https://github.com/peasy/Peasy.NET/wiki/ServiceBase) that are injected with [data proxies](https://github.com/peasy/Peasy.NET/wiki/Data-Proxy) that use HTTP (via HttpClient) to communicate with the Web API application.  In turn, the Web API application uses business services that are injected with data proxies that use Entity Framework 6.0 to communicate with a SQL Server database.

The MVC project uses dynamic dependency injection to create business services and data proxies that will be injected into the MVC controllers.  To configure the MVC project to consume Entity Framework data proxies, open the [DependencyInjection.config](https://github.com/peasy/Samples/blob/master/Orders.com.Web.MVC/DependencyInjection.config) file.

Notice that there are three configuration sections for data proxies; Entity Framework, HTTP, and In-Memory, respectively.  Ensure that the ```HTTP Data Proxies``` section is uncommented and the others are commented out.

Also notice that there are two configuration sections for business services; MVC and Web API, respectively.  Ensure that the ```Business Services - Web API``` section is uncommented and the ```Business Services - MVC``` section is commented out.

An important thing to note is that the ```Business Services - Web API``` section in the configuration file uses two business services that can be referred to as pass-thru or client service classes.  Let's take a look at the config section:

![MVC-Config](https://www.dropbox.com/s/bwdslmakb9uic6j/MVC-DI-Config.png?dl=0&raw=1)

The classes highlighted in red represent pass-through service classes.  These classes inherit from other service classes and override command methods to bypass any command logic to simply marshal calls to the data proxy.  To learn more about the necessity of pass through commands, see []().

The next step is to configure the Web API project.  The Web API project uses dynamic dependency injection to create business services and data proxies that will be injected into the API controllers.  To configure the Web API project to consume in-memory data proxies, open the [DependencyInjection.config](https://github.com/peasy/Samples/blob/master/Orders.com.Web.Api/DependencyInjection.config) file.

Notice that there are two configuration sections for data proxies; one for Entity Framework and one for In-Memory, respectively.  Simply ensure that the ```Entity Framework Data Proxies``` section is uncommented and the ```In Memory Data Proxies``` section is commented out.

Before running the application, be sure to [setup SQL Server](https://github.com/peasy/Samples#sql-server-setup) after changing the configuration.

To run the application, set the WPF and Web Api projects as the startup projects in the solution and run the application.

#### Multiple Clients &#8594; Web API &#8594; SQL Server
![Multiple Clients &#8594; API &#8594; SQL Server](https://www.dropbox.com/s/2h90bdax1ilfvg0/all-clients.png?dl=0&raw=1)

In this scenario, WPF, ASP.NET MVC, and non-.NET clients (browsers, mobile devices, Java, etc.) directly communicate with the Web Api application via HTTP.  This setup provides ultimate flexibility, allowing virtually any client capable of HTTP communications to interact with our middle tier.

In the case of the .NET clients, they consume the business services, often times preventing unnecessary round trips to the Web Api upon unsuccessful business rule validations.  The Web Api application consumes these same set of business services to serve as a last line of defense against non-.NET clients.

This configuration can be accomplished by following these two configuration setup procedures:

#####[WPF &#8594; Web API &#8594; SQL Server](https://github.com/peasy/Samples#wpf--web-api--sql-server)
#####[ASP.NET MVC &#8594; Web API &#8594; SQL Server](https://github.com/peasy/Samples#aspnet-mvc--web-api--sql-server)

### Using client service proxies

Many of the configurations for the sample application involve the usage of the Web API project that results in a configuration of the following workflow: .NET Client &#8594; Business Service Command &#8594; HTTP Proxy &#8594; Web API Controller &#8594; Business Service Command &#8594; Database (or In-Memory) Proxy.

The great thing about this configuration is that the .NET clients share the same business logic as the Web API application.  Not only does this ensure that both applications consume the same business logic, but it ensures that non-.NET clients are also subjected to the business logic, workflow, and rules.

For the majority of Insert, Update, and Delete service command methods, these types of configurations work great.  This is especially true when these commands simply interact with a single data proxy upon successful validation of business rules.  Most of the service commands in the sample business layer fall into this category.  

However, things can get a bit complicated when dealing with more complex commands, especially those that are responsible for orchestrating workflows or business logic that entails updating multiple resources.

In addition, sometimes workflows such as these need to be executed atomically within the context of a transaction.  What may not be immediately obvious is that the .NET client that consumes a service command that has been configured to consume HTTP data proxies will have a diffult time orchestrating a rollback strategy in the event that one of the updates against one of the data proxies fail.  

While you could certainly inject a transaction strategy into a service command that handles transactional support against multiple out-of-band HTTP invocations, it's nothing short of trivial.

To help illustrate these issues, let's observe the ```ShipCommand``` method of  [OrderItemService](https://github.com/peasy/Samples/blob/master/Orders.com.BLL/Services/OrderItemService.cs), which is responsible for updating the status of an order item and reducing the inventory associated with this item.

```c#
public virtual ICommand<OrderItem> ShipCommand(long orderItemID)
{
    var proxy = DataProxy as IOrderItemDataProxy;
    return new ShipOrderItemCommand(orderItemID, proxy, _inventoryDataProxy, _transactionContext);
}
```

```ShipCommand``` simply returns an instance of the [ShipOrderItemCommand](https://github.com/peasy/Samples/blob/master/Orders.com.BLL/Commands/ShipOrderItemCommand.cs), and will invoke the following code when ```Execute()``` is invoked (ShipOrderItemCommand.Execute() and ShipOrderItemCommand.ExecuteAsync() will invoke ```OnExecute``` or ```OnExecuteAsync```, respectively):

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

This method is responsible for decrementing inventory followed by updating the status of the current order item to 'shipped' within an atomic transaction.

Let's take a look at a sequence diagram that entails a .NET client consuming the OrderItemService injected with HTTP data proxies, with the receiving Web API application consuming the OrderItemService injected with database (EF6) data proxies:

![service-proxies](https://www.dropbox.com/s/47sbyr8kdj0w6il/server-sequence.png?dl=0&raw=1)

In this scenario, the .NET client invokes the ```Execute``` method of the [ShipOrderItemCommand](https://github.com/peasy/Samples/blob/master/Orders.com.BLL/Commands/ShipOrderItemCommand.cs) returned by the ```ShipCommand``` method of [OrderItemService](https://github.com/peasy/Samples/blob/master/Orders.com.BLL/Services/OrderItemService.cs). 
The ShipCommand.```Execute``` method then invokes the ```Ship``` method of the injected [OrderItemsHttpDataProxy](https://github.com/peasy/Samples/blob/master/Orders.com.DAL.Http/OrderItemsHttpServiceProxy.cs), which issues an HTTP PUT request to the receiving ```Ship``` method of the [OrderItems Web Api controller](https://github.com/peasy/Samples/blob/master/Orders.com.Web.Api/Controllers/OrderItemsController.cs).  

Because the ```Ship``` method of OrderItemsController is also configured to use the ShipOrderItemCommand, this logic will be executed again, thus decrementing the inventory twice (initially done on the client and then in on the server).

To address this issue, the [OrderItemClientService](https://github.com/peasy/Samples/blob/master/Orders.com.BLL/Services/OrderItemClientService.cs) class was created.  This class extends [OrderItemService](https://github.com/peasy/Samples/blob/master/Orders.com.BLL/Services/OrderItemService.cs) and overrides the ```ShipCommand``` method to bypass the logic of the OrderItemShipCommand and directly marshal calls to the OrderItemsHttpDataProxy.Ship method, delegating the responsiblility of executing the OrderItemShipCommand logic on the server (Web API Controller).

Here is a sequence diagram illustrating a new configuration using the client version in the .NET client:

![client-proxies](https://www.dropbox.com/s/cq6jiqrvrs1ux46/client-sequence.png?dl=0&raw=1)

In this configuration, the client shares business logic with the server, excepting the shipping functionality which is now handled exclusively on the server.

One final note is that the shipping logic in the OrderItemShipCommand executes atomically within the context of a transaction.  As stated previously, it is notoriously difficult to orchestrate transactions against out-of-band HTTP invocations.  Creating a client service proxy allows us to delegate that responsibility to the server, where it can orchestrate these transactions directly against the configured database.
