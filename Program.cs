using Efentityorderworker;
using Microsoft.EntityFrameworkCore;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        services.AddHostedService<Worker>();

        services.AddDbContext<MyDbContext>(x =>
        {
            x.UseSqlServer("data source=.;initial catalog=db-seeumove-test; persist security info=True;Integrated Security=SSPI;TrustServerCertificate=True")
        });

    })
    .Build();

host.Run();
