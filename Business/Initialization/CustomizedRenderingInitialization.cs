// using EPiServer.Framework;
// using EPiServer.Framework.Initialization;
// using EPiServer.ServiceLocation;
// using EPiServer.Web;
// using EPiServer.Web.Mvc.Html;
// using realloy.Business.Rendering;

// namespace realloy.Business.Initialization;

// [ModuleDependency(typeof(InitializationModule))]
// public class CustomizedRenderingInitialization : IConfigurableModule
// {
//     public void ConfigureContainer(ServiceConfigurationContext context)
//     {
//         // context.ConfigurationComplete += (sender, e)
//         //     => context.Services.AddTransient<ContentAreaRenderer, ReAlloyContentAreaRender>();
//     }

//     public void Initialize(InitializationEngine context) =>
//         context.Locate.Advanced.GetInstance<ITemplateResolverEvents>().TemplateResolved += TemplateCoordinator.OnTemplateResolved;

//     public void Uninitialize(InitializationEngine context) =>
//         context.Locate.Advanced.GetInstance<ITemplateResolverEvents>().TemplateResolved -= TemplateCoordinator.OnTemplateResolved;
// }