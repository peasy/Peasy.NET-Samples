![peasy](https://www.dropbox.com/s/2yajr2x9yevvzbm/peasy3.png?dl=0&raw=1)

# A sample application

A full implementation of a middle tier built with peasy and sample consumer clients (WPF, Web API, and ASP.NET MVC) can be found here.  You can clone the repo or download the entire project as a [zip](https://github.com/peasy/samples/archive/master.zip).

Once downloaded, open Orders.com.sln with Visual Studio, set the WPF project and Web Api projects as the startup projects and run.

The sample application is a ficticious order entry / inventory management system, and offers a WPF as well as an ASP.NET MVC client.  All efforts were made to keep these applications as simple as possible to keep the focus more on how a middle tier is both written and consumed.

The easiest way to get up and running is to set either the WPF project or the ASP.NET MVC project as the startup project and run the application.  Each project is configured to use in-memory [data proxies]().  However, the sample applications can be configured to interact with a SQL Server database or HTTP proxies (via Web Api) as well.

### SQL Server Setup

### Configurations

Because these clients consume a middle tier written with peasy, they can be configured in different ways to fulfill your needs.

#### WPF -> In Memory

![WPF -> In Memory](https://www.dropbox.com/s/yex9qv528um3re6/WPF.png?dl=0&raw=1)

#### WPF -> SQL Server
![WPF -> SQL Server](https://www.dropbox.com/s/s5xvkdgkasynzd6/WPF-SQL.png?dl=0&raw=1)

#### WPF -> Web API -> SQL Server
![WPF -> In Memory](https://www.dropbox.com/s/3jnzgut90xfoy23/WPF-API-SQL.png?dl=0&raw=1)

#### ASP.NET MVC -> In Memory
![WPF -> In Memory](https://www.dropbox.com/s/woda85tpyk7l3ht/MVC.png?dl=0&raw=1)

#### ASP.NET MVC -> SQL Server
![WPF -> In Memory](https://www.dropbox.com/s/9gsj1omqezv2f0b/MVC-SQL.png?dl=0&raw=1)

#### ASP.NET MVC -> Web API -> SQL Server
![WPF -> In Memory](https://www.dropbox.com/s/12s0xb94aj8fuyu/MVC-API-SQL.png?dl=0&raw=1)
