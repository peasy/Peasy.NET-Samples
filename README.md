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

In this scenario, the WPF client consumes [business services](https://github.com/peasy/Peasy.NET/wiki/ServiceBase) that are injected with in-memory [data proxies](https://github.com/peasy/Peasy.NET/wiki/Data-Proxy).  To configure the WPF application to use business services that are injected with in-memory data proxies, locate the ```MainWindow_Loaded``` event handler in the [MainWindow](https://github.com/peasy/Samples/blob/master/Orders.com.WPF/MainWindow.xaml.cs) class and ensure the following line exists:

```c#
void MainWindow_Loaded(object sender, RoutedEventArgs e)
{
    ConfigureInMemoryUsage();
}
```

The ```ConfigureInMemoryUsage``` method instantiates the business services and passes concrete [in-memory data proxies](https://github.com/peasy/Peasy.DataProxy.InMemory) to them for data access.

#### WPF &#8594; SQL Server
![WPF &#8594; SQL Server](https://www.dropbox.com/s/s5xvkdgkasynzd6/WPF-SQL.png?dl=0&raw=1)

In this scenario, the WPF client consumes [business services](https://github.com/peasy/Peasy.NET/wiki/ServiceBase) that are injected with Entity Framework 6.0 [data proxies](https://github.com/peasy/Peasy.NET/wiki/Data-Proxy) configured to communicate with a SQL Server database.  To configure the WPF application to use business services injected with EF6 data proxies, locate the ```MainWindow_Loaded``` event handler in the [MainWindow](https://github.com/peasy/Samples/blob/master/Orders.com.WPF/MainWindow.xaml.cs) class and ensure the following line exists:

```c#
void MainWindow_Loaded(object sender, RoutedEventArgs e)
{
    ConfigureEFUsage();
}
```

The ```ConfigureEFUsage``` method instantiates the business services and passes concrete [EF6 data proxies](https://github.com/peasy/Peasy.DataProxy.EF6) to them for data access.

Before running the application, be sure to [setup SQL Server](https://github.com/peasy/Samples#sql-server-setup) after changing the configuration. 

#### WPF &#8594; Web API &#8594; In Memory
![WPF &#8594; Web API &#8594; In Memory](https://www.dropbox.com/s/qzouuwj1lrec44v/WPF-WebAPI.png?dl=0&raw=1)

In this scenario, the WPF client consumes [business services](https://github.com/peasy/Peasy.NET/wiki/ServiceBase) that are injected with HTTP [data proxies](https://github.com/peasy/Peasy.NET/wiki/Data-Proxy) configured to communicate with the Web API project.  In turn, the Web API project is configured to use in-memory data proxies by default.

To configure the WPF application to use business services that are injected with HTTP data proxies, locate the ```MainWindow_Loaded``` event handler in the [MainWindow](https://github.com/peasy/Samples/blob/master/Orders.com.WPF/MainWindow.xaml.cs) class and ensure the following line exists:

```c#
void MainWindow_Loaded(object sender, RoutedEventArgs e)
{
    ConfigureHttpClientUsage();
}
```

An important thing to note is that the configuration in ```ConfigureHttpClientUsage()``` uses a few business services that can be referred to as pass-thru classes.

By default, the web api project has been configured to use in-memory data proxies.  However, you can configure the Web Api project to consume EF6 data proxies by changing the configuration.  In the Web Api project you will find a DependencyInjection.config file that contains 2 sections for possible configurations for data proxies, EF6 and In-Memory, respectively.

To run, set the WPF and Web Api as the startup projects and run the application.

#### WPF &#8594; Web API &#8594; SQL Server
![WPF &#8594; In Memory](https://www.dropbox.com/s/3jnzgut90xfoy23/WPF-API-SQL.png?dl=0&raw=1)

In this scenario, the WPF client consumes peasy [business services](https://github.com/peasy/Peasy.NET/wiki/ServiceBase) that are injected with HTTP [data proxies](https://github.com/peasy/Peasy.NET/wiki/Data-Proxy) and communicate with the Web API project.  In turn, the Web API project can be configured to use in-memory or EF6 data proxies.

To configure the WPF application to use business services that are injected with HTTP data proxies, locate the ```MainWindow_Loaded``` event handler in the [MainWindow](https://github.com/peasy/Samples/blob/master/Orders.com.WPF/MainWindow.xaml.cs) class and ensure the following line exists:

```c#
void MainWindow_Loaded(object sender, RoutedEventArgs e)
{
    ConfigureHttpClientUsage();
}
```

An important thing to note is that the configuration in ```ConfigureHttpClientUsage()``` uses a few business services that can be referred to as pass-thru classes.

![wpf http config](https://www.dropbox.com/s/mfxllpsyieuutri/wpf_http_config.png?dl=0&raw=1)

By default, the web api project has been configured to use in-memory data proxies.  However, you can configure the Web Api project to consume EF6 data proxies by changing the configuration.  In the Web Api project you will find a DependencyInjection.config file that contains 2 sections for possible configurations for data proxies, EF6 and In-Memory, respectively.

To run, set the WPF and Web Api as the startup projects and run the application.


#### ASP.NET MVC &#8594; In Memory
![WPF &#8594; In Memory](https://www.dropbox.com/s/woda85tpyk7l3ht/MVC.png?dl=0&raw=1)

#### ASP.NET MVC &#8594; SQL Server
![WPF &#8594; In Memory](https://www.dropbox.com/s/9gsj1omqezv2f0b/MVC-SQL.png?dl=0&raw=1)

#### ASP.NET MVC &#8594; Web API &#8594; SQL Server
![WPF &#8594; In Memory](https://www.dropbox.com/s/12s0xb94aj8fuyu/MVC-API-SQL.png?dl=0&raw=1)
