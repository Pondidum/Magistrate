using Ledger;
using Magistrate.Infrastructure;
using Magistrate.ReadModels;
using MediatR;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using StructureMap;
using StructureMap.Graph;

namespace Magistrate
{
	public class MagistrateRegistry : Registry
	{
		public MagistrateRegistry(IEventStore store)
		{
			Scan(a =>
			{
				a.TheCallingAssembly();
				a.WithDefaultConventions();

				a.AddAllTypesOf(typeof(IRequestHandler<,>));
				a.AddAllTypesOf(typeof(INotificationHandler<>));
			});

			For<SingleInstanceFactory>().Use<SingleInstanceFactory>(ctx => t => ctx.GetInstance(t));
			For<MultiInstanceFactory>().Use<MultiInstanceFactory>(ctx => t => ctx.GetAllInstances(t));
			For<IMediator>().Use<Mediator>();

			Policies.Add<ServicePolicy>();

			For<CollectionsReadModel>().Singleton();
			For<HistoryReadModel>().Singleton();
			For<AuthorizationReadModel>().Singleton();

			For<Projectionist>().Singleton();
			For<IEventStore>()
				.Use(context => new ProjectionStore(store, context.GetInstance<Projectionist>().Apply));

			For<JsonSerializerSettings>().Use(new JsonSerializerSettings
			{
				ContractResolver = new CamelCasePropertyNamesContractResolver()
			});
		}
	}
}
